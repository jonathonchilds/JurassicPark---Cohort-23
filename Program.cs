using System;
using System.Collections.Generic;
using System.Linq;

namespace JurassicPark
{

    class DinosaurDatabase

    {
        private List<Dinosaur> Dinosaurs { get; set; } = new List<Dinosaur>();

        public void AddDinosaur(Dinosaur add)   //<--- Can't this just be one line, somehow?
        {
            Dinosaurs.Add(add);
        }
        public void RemoveDinosaur(Dinosaur remove) //<--- Can't this just be one line, somehow? 
        {
            Dinosaurs.Remove(remove);
        }
        public Dinosaur ViewOneDinosaur(string dinoToFind)  //<---- Why written like this?
        {
            Dinosaur foundDinosaur = Dinosaurs.FirstOrDefault(dinosaur => dinosaur.Name.ToUpper().Contains(dinoToFind.ToUpper()));
            return foundDinosaur;
        }
        public List<Dinosaur> ViewAllDinosaurs()
        {
            return Dinosaurs;
        }
    }
    class Dinosaur
    {
        public string Name { get; set; }
        public string DietType { get; set; }
        public DateTime WhenAcquired { get; set; }
        public int Weight { get; set; }
        public int EnclosureNumber { get; set; }

        public void DisplayDinosaurs()
        {
            Console.WriteLine($"Name: {Name} ");
            Console.WriteLine($"Diet: {DietType} ");
            Console.WriteLine($"Acquired: {WhenAcquired} ");
            Console.WriteLine($"Weight: {Weight} lbs ");
            Console.WriteLine($"Enclosure #: {EnclosureNumber} ");
        }
    }

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

            var dinosaurs = new List<Dinosaur>();
            {
                new Dinosaur()
                {
                    Name = "Jeff",
                    DietType = "Carnivore",
                    WhenAcquired = DateTime.Now,
                    Weight = 10962,
                    EnclosureNumber = 1,
                };
                new Dinosaur()
                {
                    Name = "George",
                    DietType = "Herbivore",
                    WhenAcquired = DateTime.Now,
                    Weight = 67,
                    EnclosureNumber = 2,
                };
                new Dinosaur()
                {
                    Name = "Betty",
                    DietType = "Carnivore",
                    WhenAcquired = DateTime.Now,
                    Weight = 15589,
                    EnclosureNumber = 3,
                };
            };

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
                        ViewAllDinosaurs(database); //<----- what, why?
                        break;

                    case "A":
                        var dinosaur = new Dinosaur();
                        Console.WriteLine();
                        dinosaur.Name = PromptForString("What is the dinosaurs name? ").ToUpper();
                        dinosaur.DietType = PromptForDiet("Is this dinosaur an (H)erbivore or a (C)arnivore? ").ToUpper();
                        dinosaur.Weight = PromptForInteger("How much does your dinosaur weigh, in pounds? ");
                        dinosaur.EnclosureNumber = PromptForInteger("Please assign an enclosure number to this dinosaur: ");
                        dinosaur.WhenAcquired = DateTime.Now;
                        dinosaurs.Add(dinosaur);
                        break;

                    case "R":
                        Console.WriteLine();
                        var nameToRemove = PromptForString("What is the name of the dinosaur you'd like to remove? ");
                        Console.WriteLine();
                        Dinosaur foundDinosaur = dinosaurs.FirstOrDefault(dinosaur => dinosaur.Name == nameToRemove);
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
                                dinosaurs.Remove(foundDinosaur);
                                Console.WriteLine();
                                Console.WriteLine($"{foundDinosaur.Name} has been removed from the park register.");
                                Console.WriteLine();
                            }
                        }
                        break;

                    case "T":
                        Console.WriteLine();
                        var nameToTransfer = PromptForString("What is the name of the dinosaur you'd like to transfer? ").ToUpper();
                        Dinosaur moveDinosaur = dinosaurs.FirstOrDefault(dinosaur => dinosaur.Name == nameToTransfer);
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
        }
    }
}














