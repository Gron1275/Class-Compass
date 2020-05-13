using System;
using System.Text;
using System.IO;
using CsvHelper;
using System.Collections.Generic;
namespace RecommendationEngine
{
    public class CsvService //Prolly should change. bad naming convention to start with "I" if not an interface 
    {
        private List<Point> localPoints;
        public CsvService(List<Point> inputList)
        {
            this.localPoints = inputList;
        }
        public void WriteListToFile(string filePath)
        {
            using (MemoryStream memory = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(memory))
            using (CsvWriter csvWriter = new CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
            {
                csvWriter.Configuration.Delimiter = ",";
                csvWriter.WriteField("StudentID");
                csvWriter.WriteField("FirstPoint");
                csvWriter.WriteField("SecondPoint");
                csvWriter.WriteField("ThirdPoint");
                csvWriter.NextRecord();

                foreach (Point point in this.localPoints)
                {
                    csvWriter.WriteField(point.stID);
                    csvWriter.WriteField(point.featMatrix[0, 0]);
                    csvWriter.WriteField(point.featMatrix[0, 1]);
                    csvWriter.WriteField(point.featMatrix[0, 2]);
                    csvWriter.NextRecord();
                }

                writer.Flush();
                string result = Encoding.UTF8.GetString(memory.ToArray());
                File.WriteAllText(filePath, result);
            }
        }
        public void ReadListFromFile(string filePath)
        {

        }
    }
}