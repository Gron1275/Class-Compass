using System;
using System.Collections.Generic;
using System.Data;

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
            TableCreator tableCreator = new TableCreator();
            List<Point> StudentPointList = tableCreator.GenerateStudentPointList("placeholder");
            DBSCAN dbScan = new DBSCAN(StudentPointList, inputEpsilon: 0.4, inputMinNeighbor: 5);
            List<Point> clusteredPoints = dbScan.ReturnClusteredPoints();

            List<List<Point>> listOfListOfPoints = new List<List<Point>>();

            for (double epsK = 0.1; epsK < 1.0; epsK += 0.1)
            {
                for (int minK = 5; minK < 10; minK++)
                {
                    DBSCAN findValsDBSCAN = new DBSCAN(StudentPointList, epsK, minK);
                    listOfListOfPoints.Add(findValsDBSCAN.ReturnClusteredPoints());
                }
            }
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
        static List<Point> LoadStudentDatabase(string filePath)
        {
            DataTable uncircData = new DataTable("RawData");
            DataColumn dataColumn;
            DataRow dataRow;

            dataColumn = new DataColumn();
            dataColumn.DataType = Type.GetType("System.Int32");
            dataColumn.ColumnName = "studID";
            dataColumn.AutoIncrement = true;
            dataColumn.Caption = "Student ID";
            uncircData.Columns.Add(dataColumn);

            dataColumn = new DataColumn();
            dataColumn.DataType = Type.GetType("System.Collections.Generic.IDictionary<string, double>");
            dataColumn.ColumnName = "classes";
            dataColumn.AutoIncrement = true;
            dataColumn.Caption = "Classes taken by student";
            uncircData.Columns.Add(dataColumn);
            /*
            dataRow = uncircData.NewRow(); //Might be an issue: classes like english 11 will be 
                                            //taken by everyone and will therefore be high on the 
                                            //recommendation list. Might be fine bc it's required 
                                            //anyway.
            dataRow["studID"] = 134516;
            Dictionary<string, double> userClasses = new Dictionary<string, double>(){ {"AP Calculus", 0.98},{"AP Physics", 0.8}, {"English 11", 0.97} };
            dataRow["classes"] = userClasses;
            uncircData.Rows.Add(dataRow);
            */
            StudentCreator(ref uncircData);


            void StudentCreator(ref DataTable uncircData)
            {
                string[] allowedClasses = new string[]{"AP Calculus", "AP Physics", "English 11", "ROPE", "AP US History", "AP Literature", "Big History", "Coding 1"};
                Random rand = new Random();
                for (int i = 0; i < 10; i++)
                {
                    dataRow = uncircData.NewRow();
                    dataRow["studID"] = i;
                    Dictionary<string, double> studentClasses = new Dictionary<string, double>();
                    for (int j = 0; j < 3; j++)
                    {
                        string currentClass = allowedClasses[rand.Next(50, 100) % allowedClasses.Length];
                        if (!studentClasses.ContainsKey(currentClass))
                        {
                            studentClasses.Add(currentClass, rand.Next(70, 100) / 100);
                        }
                    }
                    uncircData.Rows.Add(dataRow);
                }
            }
            /*
            Point MakePoint()
            {

            }
            */
            List<Point> name = new List<Point>();

            return name;
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