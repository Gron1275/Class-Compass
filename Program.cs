using System;
using System.Collections.Generic;
using Microsoft.Data.Analysis;

namespace RecommendationEngine
{
    class Program
    {
        static void Main()
        {
            #region DataFrame Code
            PrimitiveDataFrameColumn<int> StudentID = new PrimitiveDataFrameColumn<int>("Student ID");
            PrimitiveDataFrameColumn<double> mathComp = new PrimitiveDataFrameColumn<double>("Mathematical Competency");
            PrimitiveDataFrameColumn<double> litComp = new PrimitiveDataFrameColumn<double>("Literary Competency");
            PrimitiveDataFrameColumn<double> socComp = new PrimitiveDataFrameColumn<double>("Social Studies Competency");
            PrimitiveDataFrameColumn<double> scienceComp = new PrimitiveDataFrameColumn<double>("Science Competency");       
            DataFrame dataFrame = new DataFrame(StudentID, mathComp, litComp, socComp, scienceComp);
            #endregion

            #region Point Generation
            /// <summary>
            /// Generates random points to be used for testing the unsupervised learning algorithm.
            /// Then it adds them to the StudentPointList for use in the dbscan
            /// OR, read points from a csv file and add them to the StudentPointList instead
            /// </summary>
            bool needPoints = true;
            CsvService csvService = new CsvService();

            List<Point> StudentPointList = new List<Point>();
            if (needPoints)
            {
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
                for (int i = 0; i < 2000; i++)
                {
                    double doubleRandOne = RandDouble();
                    double doubleRandTwo = RandDouble();
                    double doubleRandThree = RandDouble();
                    double[] array = new double[] { doubleRandOne, doubleRandTwo, doubleRandThree };

                    StudentPointList.Add(new Point(i, array));
                }
                csvService.WriteListToFile(StudentPointList, @"C:\Users\Grennon\source\repos\RecommendationEngine\outfile.csv");
            }

            ////CsvService csvService = new CsvService();
            //csvService.WriteListToFile(StudentPointList, @"C:\Users\Grennon\source\repos\RecommendationEngine\outfile.csv");
            //csvService.ReadListFromFile(@"C:\Users\Grennon\source\repos\RecommendationEngine\outfile.csv");

            
            #endregion

            #region DBSCAN Code
            /// <summary>
            /// Create an instance of the DBSCAN class and find the ideal value for minK before running
            /// </summary>
            int idealMinK = StudentPointList[0].featureArray.Length * 2;
            DBSCAN dbScan = new DBSCAN(StudentPointList, inputEpsilon: 0.05, inputMinNeighbor: idealMinK);
            //Best value for minK is 2 * the amt of dimensions
            dbScan.Run();
            #endregion

            #region Diagnostics & Testing Relevant Output
            /// <summary>
            /// Used for separating out the clustered lists and printing their contents before displaying amt of noise
            /// </summary>
            Dictionary<int, List<Point>> clusteredPoints = dbScan.ReturnClusteredPoints();

            List<Point> GetList(int listID) => clusteredPoints[listID];

            void PrintList(int listID)
            {
                Console.WriteLine($"Cluster: {listID}");
                foreach (Point current in GetList(listID))
                {
                    Console.WriteLine($"ID: {current.StudentID} - Array: {current.ShowArray()}");
                }
            }
            for (int i = 0; i < dbScan.ClusterAmount; i++)
            {
                PrintList(i);
            }
            Console.WriteLine($"Noise Amount: {dbScan.NoiseAmount}");
            double noisePercentage = ((double)dbScan.NoiseAmount / StudentPointList.Count) * 100;
            Console.WriteLine($"Noise Percentage: {Math.Floor(noisePercentage)}%");
            #endregion

            //SimilarityCalculator similarityCalculator = new SimilarityCalculator(clusteredPoints[1], clusteredPoints);

            /*
            List<List<Point>> listOfListOfPoints = new List<List<Point>>();

            for (double epsK = 0.1; epsK < 1.0; epsK += 0.1)
            {
                for (int minK = 5; minK < 10; minK++)
                {
                    DBSCAN findValsDBSCAN = new DBSCAN(StudentPointList, epsK, minK);
                    Console.WriteLine($"Parameters: eps [{epsK}], minK [{minK}] | NOISE: {findValsDBSCAN.Noise}");
                }
            }
            */
        }
    }
}