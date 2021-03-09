using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaymentApi.Core;
using PaymentApi.Core.Repositories;
using PaymentApi.Core.ExternalServices;
using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using PaymentApi.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using Hangfire;

namespace PaymentApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PaymentService(IPaymentRepository paymentRepository,
            IPaymentStatusRepository paymentStatusRepository,
            IBackgroundJobClient backgroundJobClient,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _paymentStatusRepository = paymentStatusRepository;
            _backgroundJobClient = backgroundJobClient;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PaymentResource>> GetPayments()
        {
            var payment =  await _paymentStatusRepository.GetPayments();

            var paymentResource = _mapper.Map<List<PaymentResource>>(payment);

            return paymentResource;
        }

        public async Task<PaymentResource> GetPayment(Guid id)
        {
            var payment = await _paymentStatusRepository.GetPayment(id);

            var paymentResource = _mapper.Map<PaymentResource>(payment);

            return paymentResource;
        }

        public async Task<PaymentResource> CreatePayment(PaymentRequest paymentRequest)
        {

            var payment = _mapper.Map<Payment>(paymentRequest);

            payment.DateCreated = DateTime.Now;
            payment.DateUpdated = DateTime.Now;

            _paymentRepository.CreatePayment(payment);
            int val = await _unitOfWork.CompleteAsync();

            PaymentStatus paymentStatus = new PaymentStatus
            {
                PaymentId = payment.Id,
                paymentState = PaymentState.Pending
            };

            _paymentStatusRepository.CreatePaymentStatus(paymentStatus);
            await _unitOfWork.CompleteAsync();

            PaymentResource paymentAdded = new PaymentResource();
            try 
            {
                paymentAdded = _mapper.Map<PaymentResource>(payment);
            } catch(Exception e)
            {
                var m = e;
            }

            _backgroundJobClient.Enqueue<IProcessPaymentService>(e => e.ProcessPayment(paymentStatus));

            return paymentAdded;
        }

    }
}
