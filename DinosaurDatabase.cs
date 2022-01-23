using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace JurassicPark
{
    class DinosaurDatabase

    {
        public List<Dinosaur> Dinosaurs { get; set; } = new List<Dinosaur>();

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
        public void LoadDinosaurs()
        {
            if (File.Exists("Dinosaurs.csv"))
            {
                var fileReader = new StreamReader("Dinosaurs.csv");
                var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
                Dinosaurs = csvReader.GetRecords<Dinosaur>().ToList();
                fileReader.Close();
            }
        }
        public void SaveDinosaurs()
        {
            var fileWriter = new StreamWriter("Dinosaurs.csv");
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(Dinosaurs);
            fileWriter.Close();
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
        public static void View(DinosaurDatabase database)
        {
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
        }
        public static void Remove(DinosaurDatabase database)
        {
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
        }
        public static void Transfer(DinosaurDatabase database)
        {
            Console.WriteLine();
            var nameToTransfer = PromptForString("What is the name of the dinosaur you'd like to transfer? ").ToUpper();
            Dinosaur moveDinosaur = database.Dinosaurs.FirstOrDefault(dinosaur => dinosaur.Name == $"{nameToTransfer}");
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

        }
        public static void Add(DinosaurDatabase database)
        {
            var dinosaur = new Dinosaur();
            Console.WriteLine();
            dinosaur.Name = PromptForString("What is the dinosaurs name? ").ToUpper();
            dinosaur.DietType = PromptForDiet("Is this dinosaur an (H)erbivore or a (C)arnivore? ").ToUpper();
            dinosaur.Weight = PromptForInteger("How much does your dinosaur weigh, in pounds? ");
            dinosaur.EnclosureNumber = PromptForInteger("Please assign an enclosure number to this dinosaur: ");
            dinosaur.WhenAcquired = DateTime.Now;
            database.Dinosaurs.Add(dinosaur);
        }
    }
}














