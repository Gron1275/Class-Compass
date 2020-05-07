using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RecommendationEngine
{
    class Program
    {

        static void Main(string[] args)
        {
            //Vector userData = GetUserData("filepath to user.csv");
            //Matrix classData = GetClassData("filepath to class.csv");

            //Vector userData = new Vector(GetUserData("sampleUser.csv"));
            //List<Point> userDatabase = new List<Point>();
            //DBSCAN dbscan = new DBSCAN(userDatabase, 0.4, 5);
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
                double doubleRand = (double)rand.Next(70, 100) / 100;
                StudentPointList.Add(new Point(i, doubleRand));
            }
            DBSCAN dbScan = new DBSCAN(StudentPointList, inputEpsilon: 0.05, inputMinNeighbor: 5);
            //dbScan.ClusterIDChanged +=
            List<Point> clusteredPoints = dbScan.ReturnClusteredPoints();
            /*
            foreach (Point point in clusteredPoints)
            {
                Console.WriteLine($"{point.stID} [{point.value}] : {point.clID}");
            }\*/
            
            List<Point> NOISE = (clusteredPoints.Where(point1 => point1.clID == -1)).ToList();
            foreach (var item in NOISE)
            {
                if (NOISE.Count != 0)
                {
                    Console.WriteLine("Uh oh why isn't there any noise");
                }
                else
                {
                    Console.WriteLine($"[NOISE] Student ID: {item.stID}; Value: {item.value}");
                }
            }
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