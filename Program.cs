using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace RecommendationEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            //TableCreator tableCreator = new TableCreator();
            //List<Point> StudentPointList = tableCreator.GenerateStudentPointList("placeholder");
            Random rand = new Random();

            List<Point> StudentPointList = new List<Point>();

            double RandDouble()
            {
                double randNum = rand.Next(50, 101);
                if(randNum < 49)
                {
                    randNum = 0;
                    return randNum;
                }
                else
                {
                    return randNum / 100;
                }
            }
            for (int i = 0; i < 1000; i++)
            {
                double doubleRandOne = RandDouble();
                double doubleRandTwo = RandDouble();
                double doubleRandThree = RandDouble();
                double doubleRandFour = RandDouble();
                Matrix matrix = new Matrix(new double[,] { { doubleRandOne, doubleRandTwo, doubleRandThree } });
                StudentPointList.Add(new Point(i, matrix));
            }

            DBSCAN dbScan = new DBSCAN(StudentPointList, inputEpsilon: 0.08, inputMinNeighbor: 10);

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

            Console.WriteLine($"Noise Count: {dbScan.NoiseAmount}");

            
            //SimilarityCalculator similarityCalculator = new SimilarityCalculator(clusteredPoints[1], clusteredPoints);

            /*
            List<List<Point>> listOfListOfPoints = new List<List<Point>>();

            for (double epsK = 0.1; epsK < 1.0; epsK += 0.1)
            {
                for (int minK = 5; minK < 10; minK++)
                {
                    DBSCAN findValsDBSCAN = new DBSCAN(StudentPointList, epsK, minK);
                    listOfListOfPoints.Add(findValsDBSCAN.ReturnClusteredPoints());
                }
            }
            */
        }
    }
}