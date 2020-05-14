using System;
using System.Text;
using System.IO;
using CsvHelper;
using System.Collections.Generic;
using System.Linq;
namespace RecommendationEngine
{
    public class CsvService //Prolly should change. bad naming convention to start with "I" if not an interface 
    {
        private List<Point> localPoints;
        public CsvService(List<Point> inputList)
        {
            this.localPoints = inputList;
        }
        public CsvService()
        {

        }
        public void WriteListToFile(List<Point> inputList, string filePath)
        {
            using (MemoryStream memory = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(memory))
            using (CsvWriter csvWriter = new CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
            {
                csvWriter.Configuration.Delimiter = ",";
                csvWriter.WriteField("StudentID");
                csvWriter.WriteField("PartOne");
                csvWriter.WriteField("PartTwo");
                csvWriter.WriteField("PartThree");
                csvWriter.NextRecord();

                foreach (Point point in inputList)
                {
                    csvWriter.WriteField(point.StudentID);
                    csvWriter.WriteField(point.featureArray[0]);
                    csvWriter.WriteField(point.featureArray[1]);
                    csvWriter.WriteField(point.featureArray[2]);
                    csvWriter.NextRecord();
                }

                writer.Flush();
                string result = Encoding.UTF8.GetString(memory.ToArray());
                File.WriteAllText(filePath, result);
            }
        }
        public List<Point> ReadListFromFile(string filePath)
        {
            //var file = File.ReadAllText(filePath);
            //var csvFile = File.OpenRead(filePath);
            //using (MemoryStream memory = new MemoryStream())
            //using (StreamReader reader = new StreamReader(memory))
            //using (CsvReader csvReader = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture))
            //{

            //}
            //Console.WriteLine(file);
            List<Point> localPointList = new List<Point>();
            using (var sr = new StreamReader(filePath))
            {
                var reader = new CsvReader(sr, System.Globalization.CultureInfo.CurrentCulture);

                //CSVReader will now read the whole file into an enumerable
                IEnumerable<Point> records = reader.GetRecords<Point>();
                
                foreach (Point record in records)
                {
                    localPointList.Add(record);
                }
            }
            return localPointList;
        }
    }
}