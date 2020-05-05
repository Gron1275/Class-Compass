using System;
using System.Collections.Generic;

namespace RecommendationEngine
{

    class Program
    {

        static void Main(string[] args)
        {
            //Vector userData = GetUserData("filepath to user.csv");
            //Matrix classData = GetClassData("filepath to class.csv");

            Vector userData = new Vector(GetUserData("sampleUser.csv"));
            List<Point> userDatabase = new List<Point>();
            //Matrix classData = new Matrix(new int[,] { { 0, 1, 0, 1, 1 },
            //                                           { 1, 1, 0, 0, 0 },
            //                                           { 0, 0, 1, 1, 0 }
            //                               });
            Matrix classData = new Matrix(GetClassData("sampleClasses.csv"));
            RecEngine recommender = new RecEngine(userData, classData);
            recommender.ShowRecs();
            Console.ReadKey();
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
        //static int[,] GetInput()           used for testing
        //{
        //    List<string> classes = new List<string> { "math", "science", "literature", "social studies", "technology" };
        //    int[,] userInput = new int[5, 1];

        //    for (int i = 0; i < 5; i++)
        //    {
        //        Console.Write($"\nRate from 1 (not interested) to 5 (very interested) how much interest you have in {classes[i]}?: ");

        //        userInput[i, 0] = Convert.ToUInt16(Console.ReadLine());
        //    }
        //    return userInput;
        //}
    }
}
