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

        static void Main(string[] args)
        {
            DisplayGreeting();

            var database = new DinosaurDatabase();

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
                        DinosaurDatabase.View(database);
                        break;

                    case "A":
                        DinosaurDatabase.Add(database);
                        break;

                    case "R":
                        DinosaurDatabase.Remove(database);
                        break;

                    case "T":
                        DinosaurDatabase.Transfer(database);
                        break;

                    case "S":
                        DinosaurDatabase.Summarize(database);
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
                database.SaveDinosaurs();
            }
        }
    }
}














