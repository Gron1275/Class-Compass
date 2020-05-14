using System;
using System.Collections.Generic;

namespace RecommendationEngine
{
    class Program
    {
        static void AskForPoints(ref List<Point> StudentPointList)
        {
            CsvService csvService = new CsvService();

            Console.Write("\nPath to csv list: ");
            string filePath = Console.ReadLine();
            StudentPointList = csvService.ReadListFromFile(filePath);//Doesn't actually work yet :(

        }
        static void GeneratePoints(ref List<Point> StudentPointList, int numPoints)
        {
            CsvService csvService = new CsvService();
            System.Console.WriteLine($"Generating {numPoints} points...");
            Random rand = new Random();

                double RandDouble()
                {
                    double randNum = rand.Next(50, 101);
                    if (randNum < 49)
                    {
                        randNum = 0;
                        return randNum;
                    }
                    else
                    {
                        return randNum / 100;
                    }
                }
                for (int i = 0; i < numPoints; i++)
                {
                    double doubleRandOne = RandDouble();
                    double doubleRandTwo = RandDouble();
                    double doubleRandThree = RandDouble();
                    double[] array = new double[] { doubleRandOne, doubleRandTwo, doubleRandThree };

                    StudentPointList.Add(new Point(i, array));
                }
            System.Console.WriteLine("Points Generated.");
            Console.Write("Would you like to save points to file? [Y/N]: ");
            string answer = Console.ReadLine();
            if (answer == "Y" || answer == "y")
            {
                System.Console.Write("Save points to (filepath): ");
                string filePath = Console.ReadLine();
                csvService.WriteListToFile(StudentPointList, filePath);
            }  
        }
        static void Main()
        {
            System.Console.WriteLine("Welcome, User");
            System.Console.Write("Would you like to load points? [Y/N]: ");
            string response = Console.ReadLine();
            List<Point> StudentPointList = new List<Point>();

            switch (response)
            {
                case "yes":
                case "Yes":
                case "y":
                case "Y":
                    AskForPoints(ref StudentPointList);
                    break;
                default:
                    Console.Write("\nHow many test points would you like to generate?: ");
                    int numPoints = Convert.ToInt32(Console.ReadLine());
                    GeneratePoints(ref StudentPointList, numPoints);
                    break;
            }

            Console.Clear();
            System.Console.WriteLine("Running clustering algorithm on points...");            
            
            #region DBSCAN Code
            /// <summary>
            /// Create an instance of the DBSCAN class and find the ideal value for minK before running
            /// </summary>
            int idealMinK = StudentPointList[0].FeatureArray.Length * 2;
            DBSCAN dbScan = new DBSCAN(StudentPointList, inputEpsilon: 0.05, inputMinNeighbor: idealMinK);
            //Best value for minK is 2 * the amt of dimensions
            dbScan.Run();
            #endregion

            #region Diagnostics & Testing Relevant Output
            /// <summary>
            /// Used for separating out the clustered lists and printing their contents before displaying amt of noise
            /// </summary>
            Dictionary<int, List<Point>> clusteredPoints = dbScan.ReturnClusteredPoints();

            //List<Point> GetList(int listID)
            //{
            //    try
            //    {
            //        clusteredPoints[listID];
            //    }
            //    catch (IndexOutOfRangeException e)
            //    {
            //        Console.WriteLine("Index out of range");
            //        throw e;
            //    }
            //}
            List<Point> GetList(int listID) => clusteredPoints[listID];


            void PrintList(int listID)
            {
                Console.WriteLine($"Cluster: {listID}");
                foreach (Point current in GetList(listID))
                {
                    Console.WriteLine($"ID: {current.StudentID} - Array: {current.ShowArray()}");
                }
            }
            System.Console.WriteLine($"Clusters created: {dbScan.ClusterAmount}");
            Console.WriteLine($"DBSCAN RUNTIME with {StudentPointList.Count} points: {dbScan.timeElapsed}");
            Console.WriteLine($"Noise Amount: {dbScan.NoiseAmount}");
            double noisePercentage = ((double)dbScan.NoiseAmount / StudentPointList.Count) * 100;
            Console.WriteLine($"Noise Percentage: {Math.Floor(noisePercentage)}%");
            System.Console.Write("Would you like to view contents of a cluster? [Y/N]: ");
            string input = Console.ReadLine();
            while (input == "Y" || input == "y")
            {
                Console.Clear();
                Console.WriteLine($"Amount of clusters: {dbScan.ClusterAmount}");
                Console.Write("Input cluster id to view: ");
                int clusterToView = Convert.ToInt32(Console.ReadLine());
                if (clusterToView < dbScan.ClusterAmount) { PrintList(clusterToView); } else { Console.WriteLine("Error: Index out of Range."); }
                Console.Write("\nView another cluster? [Y/N]: ");
                input = Console.ReadLine();
            }
            #endregion

            //SimilarityCalculator similarityCalculator = new SimilarityCalculator(clusteredPoints[1], clusteredPoints);

            

            //for (double epsK = 0.1; epsK < 1.0; epsK += 0.1)
            //{
            //    for (int minK = 5; minK < 10; minK++)
            //    {
            //        DBSCAN findValsDBSCAN = new DBSCAN(StudentPointList, epsK, minK);
            //        findValsDBSCAN.Run();
            //        Console.WriteLine($"");
            //    }
            //}
            
        }
    }
}