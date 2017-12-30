﻿namespace VetClinic.Data.Models.Abstractions
{
    using Bytes2you.Validation;
    using Common.Enums;
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class Animal : IAnimal
    {
        private readonly string id;
        private readonly string name;
        private readonly int age;
        private readonly AnimalGenderType gender;
        private readonly ICollection<IService> services;

        public Animal(string name, AnimalGenderType gender, AnimalType type, int age)
        {
            Guard.WhenArgument(name, "Invalid name").IsNull().Throw();
            Guard.WhenArgument(name.Length, "Invalid name length").IsLessThan(2).IsGreaterThan(15).Throw();

            this.id = Guid.NewGuid().ToString();
            this.name = name;
            this.gender = gender;
            this.age = age;
            this.Type = type;
            this.services = new List<IService>();
        }

        public string Id => this.id;

        public string Name => this.name;

        public int Age => this.age;

        public AnimalGenderType Gender => this.gender;

        public string OwnerPhoneNumber { get; set; }

        public AnimalType Type { get; protected set; }

        public ICollection<IService> Services => this.services;

        public void AddServices(IService service)
        {
            if (service == null)
            {
                throw new ArgumentException("The services cannot be null");
            }

            this.services.Add(service);
        }

        public virtual string PrintInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Pet Type: {this.Type}");
            sb.AppendLine($"Name: {this.Name}");
            sb.AppendLine($"Id: {this.Id}");
            sb.AppendLine($"Gender: {this.Gender}");

            return sb.ToString();
        }
    }
}
