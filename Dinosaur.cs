using System;

namespace JurassicPark
{
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
            Console.WriteLine($"Enclosure: {EnclosureNumber} ");
        }
    }
}














