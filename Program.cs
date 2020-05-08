using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace RecommendationEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            //Vector userData = GetUserData("filepath to user.csv");
            //Matrix classData = GetClassData("filepath to class.csv");

            //Vector userData = new Vector(GetUserData("sampleUser.csv"));;
            //Matrix classData = new Matrix(new int[,] { { 0, 1, 0, 1, 1 },
            //                                           { 1, 1, 0, 0, 0 },
            //                                           { 0, 0, 1, 1, 0 }
            //                               });
            //Matrix classData = new Matrix(GetClassData("sampleClasses.csv"));
            //RecEngine recommender = new RecEngine(userData, classData);
            //recommender.ShowRecs();
            //Console.ReadKey();
            //TableCreator tableCreator = new TableCreator();
            //List<Point> StudentPointList = tableCreator.GenerateStudentPointList("placeholder");
            Random rand = new Random();

            List<Point> StudentPointList = new List<Point>();

            for (int i = 0; i < 1000; i++)
            {
                double doubleRandOne = (double)rand.Next(50, 100) / 100;
                double doubleRandTwo = (double)rand.Next(50,100) / 100;
                double doubleRandThree = (double)rand.Next(50, 100) / 100;
                Matrix matrix = new Matrix(new double[,] { { doubleRandOne, doubleRandTwo, doubleRandThree } });
                StudentPointList.Add(new Point(i, matrix));
            }

            DBSCAN dbScan = new DBSCAN(StudentPointList, inputEpsilon: 0.1, inputMinNeighbor: 10);

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
            for (int i = 0; i < dbScan.ReturnClusterAmount(); i++)
            {
                PrintList(i);
            }

            


            //SimilarityCalculator similarityCalculator = new SimilarityCalculator(clusteredPoints[1], clusteredPoints);
            
            
            //List<Point> clusterOne = clusteredPoints.Where(point => point.clID == 1).ToList();
            //foreach (Point point in clusterOne)
            //{
            //    Console.WriteLine($"ID: {point.stID} - Matrix: {point.ShowMatrix()} - Cluster {point.clID}");
            //}
            //Console.WriteLine($"NUmber of points in cluster one: {clusterOne.Count}");
            //List<Point> NOISE = (clusteredPoints.Where(point1 => point1.pointType == 0)).ToList();
            
            //if (NOISE.Count != 0)
            //{
            //    foreach (var item in NOISE)
            //    {
            //        Console.WriteLine($"[NOISE] Student ID: {item.stID}; Value: {item.ShowMatrix()}");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Uh oh why isn't there any noise");
            //}
            
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
        static int[,] GetUserData(string filePath)
        {
            //Access csv file that has been downloaded from google forms
            int[,] placeholder = new int[1, 9];
            return placeholder;
        }
        static int[,] GetClassData(string filePath)
        {
            //Access csv file that stores the tags for each class
            int[,] placeholder = new int[4, 9];
            return placeholder;
        }
    }
}