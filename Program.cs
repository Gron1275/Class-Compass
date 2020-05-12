using System;
using System.Collections.Generic;

namespace RecommendationEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            //TableCreator tableCreator = new TableCreator();
            //List<Point> StudentPointList = tableCreator.GenerateStudentPointList("placeholder");

            /*
            PrimitiveDataFrameColumn<int> StudentID = new PrimitiveDataFrameColumn<int>("Student ID");
            PrimitiveDataFrameColumn<double> mathComp = new PrimitiveDataFrameColumn<double>("Mathematical Competency");
            PrimitiveDataFrameColumn<double> litComp = new PrimitiveDataFrameColumn<double>("Literary Competency");
            PrimitiveDataFrameColumn<double> socComp = new PrimitiveDataFrameColumn<double>("Social Studies Competency");
            PrimitiveDataFrameColumn<double> scienceComp = new PrimitiveDataFrameColumn<double>("Science Competency");

            DataFrame dataFrame = new DataFrame(StudentID, mathComp, litComp, socComp, scienceComp);
            */
            List<Point> StudentPointList = new List<Point>();
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
                double dRF = RandDouble();
                double dRFi = RandDouble();
                double dFS = RandDouble();
                Matrix matrix = new Matrix(new double[,] { { doubleRandOne, doubleRandTwo, doubleRandThree, dRF } });
                StudentPointList.Add(new Point(i, matrix));
            }

            int idealMinK = StudentPointList[0].featMatrix.GetColumns() * 2;
            DBSCAN dbScan = new DBSCAN(StudentPointList, inputEpsilon: 0.089, inputMinNeighbor: idealMinK);
            //Best value for minK is 2 * the amt of dimensions
            dbScan.Run();

            Dictionary<int, List<Point>> clusteredPoints = dbScan.ReturnClusteredPoints();

            List<Point> GetList(int listID) => clusteredPoints[listID];

            void PrintList(int listID)
            {
                Console.WriteLine($"Cluster: {listID}");
                foreach (Point current in GetList(listID))
                {
                    Console.WriteLine($"ID: {current.stID} - Matrix: {current.ShowMatrix()}");
                }
            }
            for (int i = 0; i < dbScan.ClusterAmount; i++)
            {
                PrintList(i);
            }
            Console.WriteLine($"Noise Amount: {dbScan.NoiseAmount}");
            double noisePercentage = ((double)dbScan.NoiseAmount / StudentPointList.Count) * 100;
            Console.WriteLine($"Noise Percentage: {Math.Floor(noisePercentage)}%");


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