﻿namespace VetClinic
{
    using System;
    using System.Linq;
    using VetClinic.Commands.Contracts;

    public class Engine : IEngine
    {
        private readonly IUserCommand userCommands;
        private readonly IAnimalCommand animalCommands;
        private readonly IEmployeeCommand employeeCommands;
        private readonly IServiceCommand serviceCommands;
        private readonly IEngineCommand engineCommands;
        private readonly ICashRegisterCommand cashRegister;

        public Engine(IUserCommand userCommands, IAnimalCommand animalCommands, IEmployeeCommand employeeCommands, IServiceCommand serviceCommands, IEngineCommand engineCommands, ICashRegisterCommand cashRegister)
        {
            this.userCommands = userCommands;
            this.animalCommands = animalCommands;
            this.employeeCommands = employeeCommands;
            this.serviceCommands = serviceCommands;
            this.engineCommands = engineCommands;
            this.cashRegister = cashRegister;
        }

        public void Start()
        {
            Console.WriteLine("System running...");

            while (true)
            {
                var command = Console.ReadLine();

                ProcessCommand(command);

                Console.WriteLine("Waiting for command...");
            }
        }

        private void ProcessCommand(string command)
        {
            var commandParts = command.Split(new[] { ' ', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (commandParts.Count() == 0)
            {
                Console.WriteLine("Please add a valid command!");
                return;
            }
            try
            {
                switch (commandParts[0])
                {

                    // var report = this.ProcessSingleCommand(command);
                    //reports.Add(report);

                    // User
                    case "registerUser": this.userCommands.CreateUser(commandParts); break;
                    case "deleteUser": this.userCommands.DeleteUser(commandParts); break;
                    case "listUserPets": this.userCommands.ListUserPets(commandParts); break;
                    case "searchUserByPhone": this.userCommands.SearchUserByPhone(commandParts); break;
                    case "allUsers": this.userCommands.ListUsers(); break;

                    // Employee
                    case "registerEmployee": this.employeeCommands.CreateEmployee(commandParts); break;
                    case "deleteEmployee": this.employeeCommands.DeleteEmployee(commandParts); break;
                    case "listEmployees": this.employeeCommands.ListEmployees(); break;
                    case "searchEmployeeByPhone": this.employeeCommands.SearchEmployeeByPhone(commandParts); break;

                    // Animal
                    case "registerAnimal": this.animalCommands.CreateAnimal(commandParts); break;
                    case "deleteAnimal": this.animalCommands.DeleteAnimal(commandParts); break;
                    case "listAnimals": this.animalCommands.ListPets(); break;

                    // Services
                    case "createService": this.serviceCommands.CreateService(commandParts); break;
                    case "deleteService": this.serviceCommands.DeleteService(commandParts); break;
                    case "listServices": this.serviceCommands.ListServices(commandParts); break;
                    case "performServices":
                        this.serviceCommands.PerformService(commandParts);
                        this.cashRegister.AddBookedService(commandParts); break;


                    // Commands
                    case "createCommand": this.engineCommands.CreateCommand(commandParts); break;
                    case "deleteCommand": this.engineCommands.DeleteCommand(commandParts); break;
                    case "help": this.engineCommands.Help(); break;

                    // Accounting
                    //  case "addBookedService":  break;
                    // case "updateBalance": this.cashRegister.UpdateBalance(commandParts); break;
                    case "updateBalance":
                        this.cashRegister.UpdateBalance(
                        this.serviceCommands.closeAccount(commandParts)); break;
                    case "printBalance": this.cashRegister.PrintBalance(); break;
                    case "printBookedServices": this.cashRegister.PrintBookedServices(); break;

                    default: Console.WriteLine("Invalid command! To read about all commmands, write help and press enter"); break;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine($"Command {commandParts[0]} completed successfully.");
        }
    }
}
