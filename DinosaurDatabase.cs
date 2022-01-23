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
    }
}














