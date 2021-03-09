using Autofac.Extras.Moq;
using AutoMapper;
using FluentAssertions;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Moq;
using PaymentApi.Core;
using PaymentApi.Core.ExternalServices;
using PaymentApi.Core.Repositories;
using PaymentApi.Core.Services;
using PaymentApi.Models;
using PaymentApi.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class PaymentTests
    {
        [Fact]
        public async Task CreatePayment_Return_New_Payment_Id()
        {
            var paymentId = Guid.NewGuid();
            var paymentStatus = new PaymentStatus { Id = Guid.NewGuid(), PaymentId = paymentId, paymentState = PaymentState.Pending };
            var paymentResource = new PaymentResource { Id = paymentId, CreditCardNumber = "5199110727158123", CardHolder = "Samuel Ndu", Amount = 100 };
            var paymentRequest = new PaymentRequest { Amount = 100, CreditCardNumber = "5199110727158123", CardHolder = "Samuel Ndu" };
            var payment = new Payment { Id = paymentId, CreditCardNumber = "5199110727158123", CardHolder = "Samuel Ndu", Amount= 100 };

            using (var mock = AutoMock.GetLoose())
            {

                var paymentService = mock.Create<PaymentService>();
                var processPaymentService = mock.Create<ProcessPaymentService>();

                mock.Mock<IPaymentRepository>()
                    .Setup(x => x.CreatePayment(payment));

                mock.Mock<IMapper>()
                    .Setup(x => x.Map<Payment>(paymentRequest))
                    .Returns(payment);

                mock.Mock<IMapper>()
                    .Setup(x => x.Map<PaymentResource>(payment))
                    .Returns(paymentResource);

                mock.Mock<IUnitOfWork>()
                    .Setup(x => x.CompleteAsync()).ReturnsAsync(1);

                var result = await paymentService.CreatePayment(paymentRequest);

                result.Id.Should().Be(paymentId);
            }
        }

        [Fact]
        public async Task CreatePayment_Returns_Type_PaymentResource()
        {
            var paymentId = Guid.NewGuid();
            var paymentStatus = new PaymentStatus { Id = Guid.NewGuid(), PaymentId = paymentId, paymentState = PaymentState.Pending };
            var paymentResource = new PaymentResource { Id = paymentId, CreditCardNumber = "5199110727158123", CardHolder = "Samuel Ndu", Amount = 100 };
            var paymentRequest = new PaymentRequest { Amount = 100, CreditCardNumber = "5199110727158123", CardHolder = "Samuel Ndu" };
            var payment = new Payment { Id = paymentId, CreditCardNumber = "5199110727158123", CardHolder = "Samuel Ndu", Amount = 100 };

            using (var mock = AutoMock.GetLoose())
            {

                var paymentService = mock.Create<PaymentService>();
                var processPaymentService = mock.Create<ProcessPaymentService>();

                mock.Mock<IPaymentRepository>()
                    .Setup(x => x.CreatePayment(payment));

                mock.Mock<IMapper>()
                    .Setup(x => x.Map<Payment>(paymentRequest))
                    .Returns(payment);

                mock.Mock<IMapper>()
                    .Setup(x => x.Map<PaymentResource>(payment))
                    .Returns(paymentResource);

                mock.Mock<IUnitOfWork>()
                    .Setup(x => x.CompleteAsync()).ReturnsAsync(1);

                var result = await paymentService.CreatePayment(paymentRequest);

                Assert.IsType<PaymentResource>(result);
            }
        }

        [Fact]
        public async Task ProcessPayment_Should_Be_Queued()
        {
            var client = new Mock<IBackgroundJobClient>();
            var paymentId = Guid.NewGuid();
            var paymentStatusId = Guid.NewGuid();
            var response = new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.OK };
            var paymentStatus = new PaymentStatus { Id = paymentStatusId, PaymentId = paymentId, paymentState = PaymentState.Pending };
            var paymentResource = new PaymentResource { Id = paymentId, CreditCardNumber = "5199110727158123", CardHolder = "Samuel Ndu", Amount = 100 };
            var paymentRequest = new PaymentRequest { Amount = 100, CreditCardNumber = "5199110727158123", CardHolder = "Samuel Ndu" };
            var payment = new Payment { Id = paymentId, CreditCardNumber = "5199110727158123", CardHolder = "Samuel Ndu", Amount = 100 };

            using (var mock = AutoMock.GetLoose())
            {

                var paymentService = mock.Create<PaymentService>();
                var processPaymentService = mock.Create<ProcessPaymentService>();

                var paymentRepository = mock.Mock<IPaymentRepository>();

                var paymentStatusRepository = mock.Mock<IPaymentStatusRepository>();


                paymentRepository.Setup(x => x.CreatePayment(payment));
                paymentRepository.Setup(x => x.GetPayment(paymentId)).ReturnsAsync(payment);
                paymentRepository.Setup(x => x.UpdatePayment(payment));

                paymentStatusRepository.Setup(x => x.CreatePaymentStatus(paymentStatus));
                paymentStatusRepository.Setup(x => x.GetPaymentStatus(paymentStatusId)).ReturnsAsync(paymentStatus);
                paymentStatusRepository.Setup(x => x.UpdatePaymentStatus(paymentStatus));

                mock.Mock<IMapper>()
                    .Setup(x => x.Map<Payment>(paymentRequest))
                    .Returns(payment);

                mock.Mock<IMapper>()
                    .Setup(x => x.Map<PaymentRequest>(paymentStatus))
                    .Returns(paymentRequest);

                mock.Mock<IMapper>()
                    .Setup(x => x.Map<PaymentResource>(payment))
                    .Returns(paymentResource);

                mock.Mock<IUnitOfWork>()
                    .Setup(x => x.CompleteAsync()).ReturnsAsync(1);

                mock.Mock<IExpensivePaymentGateway>()
                   .Setup(x => x.ProcessPayment(paymentRequest)).Returns(response);

                mock.Mock<IProcessPaymentService>()
                    .Setup(x => x.ProcessPayment(paymentStatus));

                client.Setup(x => x.Create(It.IsAny<Job>(), It.IsAny<EnqueuedState>()));

                client.Invoking(e => e.Verify(x => x.Create(

                    It.Is<Job>(job => job.Type.Name == "ProcessPaymentService" && job.Method.Name == "ProcessPayment" && job.Args[0] == paymentStatus),
                    It.IsAny<EnqueuedState>()), Times.Once));

                var result = await paymentService.CreatePayment(paymentRequest);
            }
        }
    }
}
