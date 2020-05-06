using System;
using System.Data;
using System.Collections.Generic;

namespace RecommendationEngine
{
    class TableCreator
    {
        public TableCreator()
        {
            
        }
        public List<Point> GenerateStudentPointList(string filePath)
        {
            DataTable uncircData = new DataTable("RawData");
            DataColumn dataColumn;
            DataRow dataRow;

            dataColumn = new DataColumn
            {
                DataType = Type.GetType("System.Int32"),
                ColumnName = "studID",
                AutoIncrement = true,
                Caption = "Student ID",
                Unique = true
            };
            uncircData.Columns.Add(dataColumn);

            dataColumn = new DataColumn
            {
                DataType = Type.GetType("System.Collections.Generic.IDictionary<string, double>"),
                ColumnName = "classes",
                AutoIncrement = true,
                Caption = "Classes taken by student"
            };
            uncircData.Columns.Add(dataColumn);

            DataColumn[] primaryKey = new DataColumn[1];
            primaryKey[0] = uncircData.Columns["studID"];
            uncircData.PrimaryKey = primaryKey;
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
            List<Point> stPointList = new List<Point>();
            Point MakePoint(int studentID)
            {
                Point studentPoint = new Point();
                DataRow studentRow = uncircData.Rows.Find(studentID);
                if (studentRow != null)
                {
                    Dictionary<string, double> preProcessedMatrix = (Dictionary<string, double>)studentRow[1];
                    
                    foreach (KeyValuePair<string, double> entry in preProcessedMatrix)
                    {
                        var placeholder = entry.Value;
                    }
                }
                return studentPoint; //This is incomplete, doesn't actually work yet
                
            }
            stPointList.Add(MakePoint(1));

            return stPointList;
        }
    }
}