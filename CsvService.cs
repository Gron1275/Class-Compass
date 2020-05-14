using System;
using System.Text;
using System.IO;
using CsvHelper;
using System.Collections.Generic;
using System.Linq;
namespace RecommendationEngine
{
    public class CsvService
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
            using (StreamWriter writer = new StreamWriter(filePath))
            using (CsvWriter csvWriter = new CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
            {
                csvWriter.Configuration.Delimiter = ";";
                //csvWriter.WriteHeader("StudentID");
                csvWriter.WriteField("FeatureArray 1");
                csvWriter.WriteField("FeatureArray 1");
                csvWriter.WriteField("FeatureArray 1");
                csvWriter.NextRecord();

                foreach (Point point in inputList)
                {
                    csvWriter.WriteField(point.StudentID); //issue is is that csvwriter is interpreting array as csvs and not working
                    csvWriter.WriteField(point.FeatureArray[0]);
                    csvWriter.WriteField(point.FeatureArray[1]);
                    csvWriter.WriteField(point.FeatureArray[2]);
                    csvWriter.NextRecord();
                }
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

                while (reader.Read())
                {
                    localPointList.Add(reader.GetRecord<Point>());
                }
            }
            return localPointList;
        }
    }
}