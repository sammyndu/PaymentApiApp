using Microsoft.EntityFrameworkCore;
using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Data
{
    public class PaymentAppDbContext : DbContext
    {
        public PaymentAppDbContext(DbContextOptions<PaymentAppDbContext> options) : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<PaymentStatus> PaymentStatus { get; set; }

    }
}
