﻿namespace VetClinic.Data.Models
{
    using Common.Enums;
    using Abstractions;

    public class Hamster : Animal
    {
        public Hamster(string name, GenderType gender) 
            : base(name, gender)
        {
            this.Type = AnimalType.Hamster;
        }


    }
}
