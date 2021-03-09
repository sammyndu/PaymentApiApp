using AutoMapper;
using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Bootstrap
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resource
            CreateMap<PaymentStatus, PaymentResource>()
                .ForMember(pr => pr.Id, opt => opt.MapFrom(p => p.Payment.Id))
                .ForMember(pr => pr.SecurityCode, opt => opt.MapFrom(p => p.Payment.SecurityCode))
                .ForMember(pr => pr.CreditCardNumber, opt => opt.MapFrom(p => p.Payment.CreditCardNumber))
                .ForMember(pr => pr.CardHolder, opt => opt.MapFrom(p => p.Payment.CardHolder))
                .ForMember(pr => pr.ExpirationDate, opt => opt.MapFrom(p => p.Payment.ExpirationDate))
                .ForMember(pr => pr.Amount, opt => opt.MapFrom(p => p.Payment.Amount))
                .ForMember(pr => pr.DateCreated, opt => opt.MapFrom(p => p.Payment.DateCreated))
                .ForMember(pr => pr.DateUpdated, opt => opt.MapFrom(p => p.Payment.DateUpdated));

            CreateMap<PaymentRequest, Payment>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.DateCreated, opt => opt.Ignore())
                .ForMember(p => p.DateUpdated, opt => opt.Ignore());

            CreateMap<PaymentStatus, PaymentRequest>()
                .ForMember(pr => pr.SecurityCode, opt => opt.MapFrom(p => p.Payment.SecurityCode))
                .ForMember(pr => pr.CreditCardNumber, opt => opt.MapFrom(p => p.Payment.CreditCardNumber))
                .ForMember(pr => pr.CardHolder, opt => opt.MapFrom(p => p.Payment.CardHolder))
                .ForMember(pr => pr.ExpirationDate, opt => opt.MapFrom(p => p.Payment.ExpirationDate))
                .ForMember(pr => pr.Amount, opt => opt.MapFrom(p => p.Payment.Amount));

            CreateMap<Payment, PaymentResource>();
        }
    }
}
