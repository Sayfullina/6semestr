//Program.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace pp3
{
    class Program
    {
       
        public static void Main(string[] args)
        {
            IO io = new IO();
            io.LoadDetectors("Dano11.csv"); //dano
            io.SaveEvents("C.csv");
            Console.ReadKey();
        }
    }
}

//Detector

namespace pp3
{
    class Detector
    {
        private int id;
        private string name;
        private int kod;
        private int year;
        private double people;

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }
        public int Kod
        {
            get
            {
                return kod;
            }
        }
        public int Year
        {
            get
            {
                return year;
            }
        }
        public double People
        {
            get
            {
                return people;
            }
        }

        public Detector(int id, string name,int kod, int year,double people)
        {
            this.id = id;
            this.name = name;
            this.kod=kod;
            this.year=year;
            this.people=people;
        }
    }
}


//Result.cs

namespace pp3
{
    class Result
    {
        private int detectorId;
        private string detectorName;
        private int detectorKod;
        private int detectorYear;
        private double detectorPeople;

        public int SensorId
        {
            get
            {
                return detectorId;
            }
        }

        public string SensorName
        {
            get
            {
                return detectorName;
            }
        }
        public int SensorKod
        {
            get
            {
                return detectorKod;
            }
        }
        public int SensorYear
        {
            get
            {
                return detectorYear;
            }
        }
        public double SensorPeople
        {
            get
            {
                return detectorPeople;
            }
        }

        public Result(int sensorId, string sensorName, int sensorKod, int sensorYear, double sensorPeople)
        {
            this.detectorId = sensorId;
            this.detectorName = sensorName;
            this.detectorKod = sensorKod;
            this.detectorYear = sensorYear;
            this.detectorPeople = sensorPeople;
        }
    }
}

//IO.cs

namespace pp3
{
    public class IO
    {
        Dictionary<int, Detector> detectors;
        List<Detector> values;

        public bool LoadDetectors(string filename)
        {
            if (!File.Exists(filename))
            {
                System.Console.WriteLine("Ошибка при загрузке файла A: файл отсутствует");
                return false;
            }
            try
            {
                detectors = new Dictionary<int, Detector>();
                values = new List<Detector>();
                using (StreamReader reader = File.OpenText(filename))
                {
                    while (!reader.EndOfStream)
                    {
                        string text = reader.ReadLine();
                        string[] elems = text.Split(';');
                        int Idd = int.Parse(elems[0]);
                        string Nname = elems[1];
                        int Kkod = int.Parse(elems[2]);
                        int Yyear = int.Parse(elems[3]);
                        double Ppeople = double.Parse(elems[4]);
                        detectors.Add(Idd, new Detector(Idd, Nname, Kkod, Yyear, Ppeople));
                        values.Add(new Detector(Idd, Nname, Kkod, Yyear, Ppeople));
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Ошибка при чтении файла A(возможно, данные введены некорректно)");
                System.Console.WriteLine(e);
                return false;
            }
        }
     
       // 

        public bool SaveEvents(string filename)
        {
            List<Result> results = new List<Result>();
            foreach (Detector m in values)
            {
                  Detector s;
                    detectors.TryGetValue(m.Id, out s);
                    results.Add(new Result(s.Id, s.Name, s.Kod, s.Year, s.People));
                
            }
            results.Sort(delegate(Result r1, Result r2) { return r1.SensorPeople.CompareTo(r2.SensorPeople); });
            try
            {
                using (StreamWriter writer = File.CreateText(filename))
                {
                    for (int i = 0; i < results.Count; i++)
                    {
                        writer.WriteLine(string.Format("{0};{1};{2};{3};{4};{5}", i + 1, results[i].SensorId, results[i].SensorName, results[i].SensorKod, results[i].SensorYear, results[i].SensorPeople));
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Ошибка при записи результатов!");
                System.Console.WriteLine(e);
                return false;
            }
        }
    }
}


