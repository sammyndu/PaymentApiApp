using AutoMapper;
using PaymentApi.Core;
using PaymentApi.Core.ExternalServices;
using PaymentApi.Core.Repositories;
using PaymentApi.Core.Services;
using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentApi.Services
{
    public class ProcessPaymentService : IProcessPaymentService
    {
        private IPaymentRepository _paymentRepository;
        private IPaymentStatusRepository _paymentStatusRepository;
        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;
        private readonly IPremiumPaymentService _premiumPaymentService;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProcessPaymentService(IPaymentRepository paymentRepository,
            IPaymentStatusRepository paymentStatusRepository,
            ICheapPaymentGateway cheapPaymentGateway,
            IExpensivePaymentGateway expensivePaymentGateway,
            IPremiumPaymentService premiumPaymentService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _paymentStatusRepository = paymentStatusRepository;
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
            _premiumPaymentService = premiumPaymentService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task ProcessPayment(PaymentStatus paymentStatus)
        {
            var paymentRequest = _mapper.Map<PaymentRequest>(paymentStatus);

            var response = GetPaymentResponse(paymentRequest);

            await UpdatePayment(paymentStatus, response);

        }

        private async Task UpdatePayment(PaymentStatus paymentStatus, HttpResponseMessage response)
        {
            paymentStatus = await _paymentStatusRepository.GetPaymentStatus(paymentStatus.Id);

            paymentStatus.paymentState = response.IsSuccessStatusCode ? PaymentState.Processed : PaymentState.Failed;

            var payment = await _paymentRepository.GetPayment(paymentStatus.PaymentId);

            payment.DateUpdated = DateTime.Now;

            _paymentStatusRepository.UpdatePaymentStatus(paymentStatus);

            _paymentRepository.UpdatePayment(payment);

            await _unitOfWork.CompleteAsync();
        }

        private HttpResponseMessage GetPaymentResponse(PaymentRequest paymentRequest)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            if (paymentRequest.Amount <= 20)
            {
                response = _cheapPaymentGateway.ProcessPayment(paymentRequest);
            }
            else if (paymentRequest.Amount > 21 && paymentRequest.Amount <= 500)
            {
                response = _expensivePaymentGateway.ProcessPayment(paymentRequest);
                if (!response.IsSuccessStatusCode)
                {
                    response = _cheapPaymentGateway.ProcessPayment(paymentRequest);
                }
            }
            else
            {
                response = RetryService(_premiumPaymentService.ProcessPayment, paymentRequest);
            }

            return response;
        }

        private HttpResponseMessage RetryService(Func<PaymentRequest, HttpResponseMessage> action, PaymentRequest paymentRequest)
        {
            int currentRetry = 0;
            int retryCount = 3;
            TimeSpan delay = TimeSpan.FromSeconds(5);
            HttpResponseMessage response = new HttpResponseMessage();

            for (; ; )
            {
                response = action(paymentRequest);

                if (!response.IsSuccessStatusCode)
                {
                    currentRetry++;
                    if (currentRetry > retryCount)
                    {
                        return response;
                    }
                }
                Task.Delay(delay);
            }
        }
    }
}
