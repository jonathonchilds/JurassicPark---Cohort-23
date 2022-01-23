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
    }
    class Dinosaur
    {
        public string Name { get; set; }
        public string DietType { get; set; }
        public DateTime WhenAcquired { get; set; }
        public int Weight { get; set; }
        public int EnclosureNumber { get; set; }
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
                Console.WriteLine("Sorry, that isn't a valid input, I'm using 0 as your answer. ");
                return 0;
            }
        }

        static void Main(string[] args)
        {
            DisplayGreeting();

            var dinosaurs = new List<Dinosaur>();

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
                        break;
                    case "A":
                        var dino = new Dinosaur();
                        dino.Name = PromptForString("What is the dinosaurs name? ");
                        dino.DietType = PromptForString("Is this dinosaur an (H)erbivore or a (C)arnivore? ");
                        dino.Weight = PromptForInteger("How much does your dinosaur weigh, in pounds? ");
                        dino.EnclosureNumber = PromptForInteger("Please assign an enclosure number to this dinosaur: ");
                        dino.WhenAcquired = DateTime.Now;
                        dinosaurs.Add(dino);
                        break;
                    case "R":
                        Console.WriteLine();
                        var name = PromptForString("What is the name of the dinosaur you'd like to remove? ");
                        Console.WriteLine();
                        Dinosaur foundDino = dinosaurs.FirstOrDefault(dinosaur => dinosaur.Name == name);
                        if (foundDino == null)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("That dinosaur isn't in our record.");
                            Console.WriteLine();
                        }
                        else
                        {
                            var confirmRemoval = PromptForString($"Are you sure you want to remove {foundDino.Name} from the park? (Y)es or (N)o ").ToUpper();
                            if (confirmRemoval == "Y")
                            {
                                dinosaurs.Remove(foundDino);
                                Console.WriteLine();
                                Console.WriteLine($"{foundDino.Name} has been removed from the park register.");
                                Console.WriteLine();

                            }
                        }
                        break;
                    case "T":
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














