using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;
using CsvHelper;

namespace JurassicPark
{
    class Program
    {
        static void DisplayGreeting()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("    Welcome to Jurassic Park    ");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
        }

        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine().ToUpper();
            return userInput;
        }
        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);
            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input. I'm using 0 as your answer. ");
                return 0;
            }

        }
        static string PromptForDiet(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine().ToUpper();
            if (userInput == "C" || userInput == "H")
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry that's not a valid response. I'm using UNKNOWN as your answer.");
                return "UNKNOWN";
            }

        }

        static void Main(string[] args)
        {
            DisplayGreeting();

            var database = new DinosaurDatabase();

            //  var dinosaurs = new List<Dinosaur>();

            database.LoadDinosaurs();

            var keepGoing = true;

            while (keepGoing)
            {
                Console.WriteLine("");

                Console.WriteLine("Please choose an option: ");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("(V)iew all of the dinosaurs ");
                Console.WriteLine("(A)dd a dinosaur to the park ");
                Console.WriteLine("(R)emove a dinosaur from the park entirely ");
                Console.WriteLine("(T)ransfer a dinosaur to a different enclosure ");
                Console.WriteLine("(S)ummarize dinosaur diet types ");
                Console.WriteLine("(Q)uit ");
                Console.WriteLine("-------------------------------------------");


                var choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "V":
                        Console.WriteLine();
                        var viewPreference = PromptForString("Would you like to view the dinosaurs by (N)ame or (E)enclosure? ").ToUpper(); //<--- add this AFTER message for no dino's
                        Console.WriteLine();
                        var viewByName = database.Dinosaurs.OrderBy(dinosaur => dinosaur.Name);
                        var viewByEnclosureNumber = database.Dinosaurs.OrderBy(dinosaur => dinosaur.EnclosureNumber);

                        if (database.Dinosaurs.Count == 0)
                        {
                            Console.WriteLine("Sorry, but we're all out of dinosaurs at the moment.");
                        }
                        else if (viewPreference == "N")
                        {
                            foreach (var viewDinosaur in viewByName)
                            {
                                viewDinosaur.DisplayDinosaurs();
                            }
                        }
                        else if (viewPreference == "E")
                        {
                            foreach (var viewDinosaur in viewByEnclosureNumber)
                            {
                                viewDinosaur.DisplayDinosaurs();
                            }
                        }
                        break;

                    case "A":
                        var dinosaur = new Dinosaur();
                        Console.WriteLine();
                        dinosaur.Name = PromptForString("What is the dinosaurs name? ").ToUpper();
                        dinosaur.DietType = PromptForDiet("Is this dinosaur an (H)erbivore or a (C)arnivore? ").ToUpper();
                        dinosaur.Weight = PromptForInteger("How much does your dinosaur weigh, in pounds? ");
                        dinosaur.EnclosureNumber = PromptForInteger("Please assign an enclosure number to this dinosaur: ");
                        dinosaur.WhenAcquired = DateTime.Now;
                        database.Dinosaurs.Add(dinosaur);
                        break;

                    case "R":
                        Console.WriteLine();
                        var nameToRemove = PromptForString("What is the name of the dinosaur you'd like to remove? ");
                        Console.WriteLine();
                        Dinosaur foundDinosaur = database.Dinosaurs.FirstOrDefault(dinosaur => dinosaur.Name == nameToRemove);
                        if (foundDinosaur == null)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("That dinosaur isn't in our record.");
                            Console.WriteLine();
                        }
                        else
                        {
                            var confirmRemoval = PromptForString($"Are you sure you want to remove {foundDinosaur.Name} from the park? (Y)es or (N)o ").ToUpper();
                            if (confirmRemoval == "Y")
                            {
                                database.Dinosaurs.Remove(foundDinosaur);
                                Console.WriteLine();
                                Console.WriteLine($"{foundDinosaur.Name} has been removed from the park register.");
                                Console.WriteLine();
                            }
                        }
                        break;

                    case "T":
                        Console.WriteLine();
                        var nameToTransfer = PromptForString("What is the name of the dinosaur you'd like to transfer? ").ToUpper();
                        Dinosaur moveDinosaur = database.Dinosaurs.FirstOrDefault(dinosaur => dinosaur.Name == nameToTransfer);
                        if (moveDinosaur == null)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Sorry, we don't have a dinosaur registered by that name. ");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine($"{moveDinosaur.Name} is currently registered to Enclosure {moveDinosaur.EnclosureNumber}.");
                            Console.WriteLine();
                            moveDinosaur.EnclosureNumber = PromptForInteger($"Please enter {moveDinosaur.Name}'s new enclosure number: ");
                            Console.WriteLine("");
                        }
                        break;

                    case "S":
                        break;

                    case "Q":
                        keepGoing = false;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("That is not a valid response. Please try again.");
                        break;


                }
            }
            database.SaveDinosaurs();

            var fileWriter = new StreamWriter("Dinosaurs.csv");

            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);

            csvWriter.WriteRecords(Dinosaurs);

            fileWriter.Close();
        }
    }
}














