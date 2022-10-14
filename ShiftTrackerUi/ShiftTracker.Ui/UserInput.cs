// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Net;
using RestSharp;

namespace ShiftTracker.Ui
{
    internal class UserInput
    {
        private ShiftsService shiftsService = new();

        internal void MainMenu()
        {
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close Application.");
                Console.WriteLine("Type 1 to View Shifts");
                Console.WriteLine("Type 2 to Add Shift");
                Console.WriteLine("Type 3 to Delete shift");
                Console.WriteLine("Type 4 to Update shift");
                //Console.WriteLine("Type 5 to View Contacts");
                //Console.WriteLine("Type 6 to Add Contacts");
                //Console.WriteLine("Type 7 to Delete Contact");
                //Console.WriteLine("Type 8 to Update Contact");
                //Console.WriteLine("Type 9 to View Contacts of One shift");

                string commandInput = Console.ReadLine();
                while (string.IsNullOrEmpty(commandInput) || !int.TryParse(commandInput, out _))
                {
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 2.\n");
                    commandInput = Console.ReadLine();
                }

                int command = Convert.ToInt32(commandInput);

                switch (command)
                {
                    case 0:
                        closeApp = true;
                        break;
                    case 1:
                        shiftsService.GetShifts();
                        break;
                    case 2:
                        ProcessAddShift();
                        break;
                    case 3:
                        ProcessDeleteShift();
                        break;
                    case 4:
                        ProcessUpdateShift();
                        break;
                    //case 5:
                    //    contactsController.ViewContacts();
                    //    break;
                    //case 6:
                    //    ProcessAddContact();
                    //    break;
                    //case 7:
                    //    ProcessDeleteContact();
                    //    break;
                    //case 8:
                    //    ProcessContactUpdate();
                    //    break;
                    //case 9:
                    //    ProcessContactsByshift();
                    //    break;



                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 8.\n");
                        break;
                }
            }
        }

        // https://stackoverflow.com/questions/36926867/deserializing-json-into-a-list-of-objects-cannot-create-and-populate-list-type

        private void ProcessAddShift()
        {
            shiftsService.GetShifts();

            Shift shift = new();
            shift.Start = GetDateTimeInput("Please add shift start");
            shift.End = GetDateTimeInput("Please add shift end");

            while (!Validator.IsEndDateValid(shift.Start, shift.End))
                shift.End = GetDateTimeInput("End date has to be after start date. Try again.");

            shift.Location = GetStringInput("Please add shift location.");
            shift.Minutes = Helpers.CalculateDuration(shift.Start, shift.End);
            shift.Pay = GetMoneyInput("Please add your pay for this shift.");

            shiftsService.AddShift(shift);
        }

        private void ProcessDeleteShift()
        {
            shiftsService.GetShifts();

            int shiftId = GetIntegerInput("Please add id of the shift you want to delete.");

            var shiftResponse = shiftsService.DeleteShift(shiftId);

            while (shiftResponse.StatusCode == HttpStatusCode.NotFound)
            {
                shiftId = GetIntegerInput($"A shift with the id {shiftId} doesn't exist. Try again.");
            }
        }

        private void ProcessUpdateShift()
        {
            shiftsService.GetShifts();
            var shiftToUpdate = ProcessGetShiftById();

            shiftToUpdate.Start =  GetDateTimeInput("Please enter new start date or type 0 to keep start date", shiftToUpdate.Start);

            shiftToUpdate.End = GetDateTimeInput("Please enter new end date or type 0 to keep end date", shiftToUpdate.End);

            while (!Validator.IsEndDateValid(shiftToUpdate.End, shiftToUpdate.Start))
            {
                shiftToUpdate.End = GetDateTimeInput("End date has to be after start date. Try again.", shiftToUpdate.End);
            }

            shiftToUpdate.Pay = GetMoneyInput("Please enter new pay or type 0 to keep pay value", shiftToUpdate.Pay);

            shiftToUpdate.Location = GetStringInput("Please enter new location or type 0 to keep location", shiftToUpdate.Location);
            
            shiftToUpdate.Minutes = Helpers.CalculateDuration(shiftToUpdate.Start, shiftToUpdate.End);

            shiftsService.UpdateShift(shiftToUpdate);
        }

        private Shift ProcessGetShiftById()
        {
            shiftsService.GetShifts();

            int shiftId = GetIntegerInput("Please add id of the shift");

            var shiftResponse = shiftsService.GetShiftById(shiftId);

            while (shiftResponse.StatusCode == HttpStatusCode.NotFound)
            {
                shiftId = GetIntegerInput($"A shift with the id {shiftId} doesn't exist. Try again.");
                shiftResponse = shiftsService.GetShiftById(shiftId);
            }

            return shiftResponse.Data;
        }

        private string GetStringInput(string message, string currentString = default)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();

            if (currentString != default & input == "0")
                return currentString;

            while (!Validator.IsStringValid(input))
            {
                Console.WriteLine("\nInvalid input");
                input = Console.ReadLine();

                if (input == "0")
                {
                    return currentString;
                }
            }

            return input;
        }

        private int GetIntegerInput(string message)
        {
            Console.WriteLine(message);
            string idInput = Console.ReadLine();

            while (!Validator.IsIdValid(idInput))
            {
                Console.WriteLine("\nInvalid id. Try again.");
                idInput = Console.ReadLine();
            }

            return Int32.Parse(idInput);
        }

        private DateTime GetDateTimeInput(string message, DateTime currentDate = default)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();

            if (currentDate != default & input == "0")
                return currentDate;

            while (!Validator.IsDateTimeValid(input))
            {
                Console.WriteLine("\nInvalid date. Try again");
                input = Console.ReadLine();

                if (input == "0")
                {
                    return currentDate;
                }
            }

            return DateTime.Parse(input);
        }

        private decimal GetMoneyInput(string message, decimal currentMoney = default)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();

            if (currentMoney != default & input == "0")
                return currentMoney;

            {
                while (!Validator.IsMoneyValid(input))
                {
                    Console.WriteLine("\nInvalid value. Try again");
                    input = Console.ReadLine();

                    if (input == "0")
                    {
                        return currentMoney;
                    }
                }
            }

            return decimal.Parse(input);
        }

        //private void ProcessshiftUpdate()
        //{
        //    contactsController.ViewCategories();

        //    int id = GetIntegerInput("Please add id of the shift you want to update.");
        //    var cat = contactsController.GetshiftById(id);

        //    while (cat == null)
        //    {
        //        id = GetIntegerInput($"A shift with the id {id} doesn't exist. Try again.");
        //    }

        //    string name = GetStringInput("Please enter new name for shift.");
        //    contactsController.Updateshift(id, name);
        //}

        //private void ProcessAddContact()
        //{
        //    contactsController.ViewCategories();
        //    Contact contact = new();
        //    contact.shiftId = GetIntegerInput("Please add shiftId for contact.");
        //    contact.FirstName = GetStringInput("Please type first name.");
        //    contact.LastName = GetStringInput("Please type last name.");
        //    contact.Number = GetPhoneInput("Please type phone number.");

        //    contactsController.AddContact(contact);
        //}
    }
}