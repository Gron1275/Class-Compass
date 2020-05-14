using System;
using System.Collections.Generic;

namespace RecommendationEngine
{
    class Program
    {
        static void GeneratePoints(ref List<Point> StudentPointList, int numPoints)
        {
            Console.WriteLine($"Generating {numPoints} points...");
            Random rand = new Random();

            double RandDouble() => rand.Next(50, 101) / 100.0;

            for (int i = 0; i < numPoints; i++)
            {
                double doubleRandOne = RandDouble();
                double doubleRandTwo = RandDouble();
                double doubleRandThree = RandDouble();
                double[] array = new double[] { doubleRandOne, doubleRandTwo, doubleRandThree };

                StudentPointList.Add(new Point(i, array));
            }
            Console.WriteLine("Points Generated.");
        }
        static void Main()
        {
            List<Point> StudentPointList = new List<Point>();

            Console.WriteLine("Welcome, User");
            Console.Write("\nHow many test points would you like to generate?: ");
            
            int numPoints = Convert.ToInt32(Console.ReadLine());
            
            GeneratePoints(ref StudentPointList, numPoints);

            Console.Clear();
            Console.WriteLine("Running clustering algorithm on points...");            
            
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
            Dictionary<int, List<Point>> clusteredPoints = dbScan.ClusteredPoints;

            List<Point> GetList(int listID) => clusteredPoints[listID];

            void PrintList(int listID)
            {
                Console.WriteLine($"Cluster: {listID}");
                foreach (Point current in GetList(listID))
                {
                    Console.WriteLine($"ID: {current.StudentID} - Array: {current.ShowArray()}");
                }
            }
            Console.WriteLine($"Clusters created: {dbScan.ClusterAmount}");
            Console.WriteLine($"DBSCAN RUNTIME with {StudentPointList.Count} points: {dbScan.timeElapsed}");
            Console.WriteLine($"Noise Amount: {dbScan.NoiseAmount}");
            double noisePercentage = ((double)dbScan.NoiseAmount / StudentPointList.Count) * 100;
            Console.WriteLine($"Noise Percentage: {Math.Floor(noisePercentage)}%");
            Console.Write("Would you like to view contents of a cluster? [Y/N]: ");
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
            Console.Clear();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            #endregion
        }
    }
}