﻿namespace VetClinic.Commands.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using VetClinic.Commands.Contracts;
    using VetClinic.Common;
    using VetClinic.Data.Contracts;
    using VetClinic.Data.Repositories.Contracts;

    public class CashRegisterCommand : VetClinicEventHandler, ICashRegisterCommand
    {
        private decimal balance;
        private readonly IServiceRepository serviceDb;

        private readonly ICollection<IService> bookedServices;

        public CashRegisterCommand(IServiceRepository serviceDb, decimal balance = 0.00m)
        {
            this.balance = balance;
            this.serviceDb = serviceDb;
            bookedServices = new List<IService>();
        }

        public decimal Balance { get; private set; }

        public ICollection<IService> BookedServices => new List<IService>(this.bookedServices);

        public decimal UpdateBalance(IList<string> parameters)
        {
            var amount = Decimal.Parse(parameters[1]);

            return this.Balance += amount;
        }

        public void AddBookedService(IList<string> parameters)
        {
            var serviceName = parameters[1];

            var service = this.serviceDb.Services.FirstOrDefault(s => s.Name == serviceName);

            if (service == null)
            {
                throw new ArgumentException($"Service {serviceName} does not exists yet. Please create service {serviceName} first");
            }

            this.bookedServices.Add(service);
            this.OnMessage($"Service {serviceName} successfully booked");
        }


            public void PrintBookedServices()
            {
                var sb = new StringBuilder();

                sb.AppendLine("Booked service History:");

                foreach (var service in bookedServices)
                {
                    sb.AppendLine($"{service.Name} - ${service.Price}");
                }

                sb.AppendLine($"TOTAL: ${Math.Round(this.Balance, 2)}"); // TODO Math.Round slower than string.Format
            }

            public void PrintBalance()
            {
                Console.WriteLine(string.Format("{0:F2}", this.Balance));
            }
        }
    }
