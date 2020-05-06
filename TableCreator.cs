using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RecommendationEngine
{
    class TableCreator
    {
        private DataTable uncircData;
        public TableCreator()
        {

        }
        public List<Point> GenerateStudentPointList(string filePath)
        {
            this.uncircData = new DataTable("RawData");
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
            this.uncircData.Columns.Add(dataColumn);

            dataColumn = new DataColumn //I wanted this to be of Dict type so i could do <ClassName, Grade> but it doesn't support dict. I'm using int array for testing
            {
                //DataType = Type.GetType("System.Collections.Generic.Dictionary<string, double>"),
                DataType = Type.GetType("System.Int32[]"),
                //DataType = Type.GetType("Dictionary<TKey, TValue>"), //This isnt working :( the type returns invalid but like, its valid???
                ColumnName = "classes",
                AutoIncrement = true,
                Caption = "Classes taken by student"
            };
            this.uncircData.Columns.Add(dataColumn);

            DataColumn[] primaryKey = new DataColumn[1];
            primaryKey[0] = this.uncircData.Columns["studID"];
            this.uncircData.PrimaryKey = primaryKey;
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
            StudentCreator();


            void StudentCreator()
            {
                string[] allowedClasses = new string[] { "AP Calculus", "AP Physics", "English 11", "ROPE", "AP US History", "AP Literature", "Big History", "Coding 1" };
                Random rand = new Random();
                for (int i = 0; i < 10; i++)
                {
                    dataRow = this.uncircData.NewRow();
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
                DataRow studentRow = this.uncircData.Rows.Find(studentID);
                if (studentRow != null)
                {
                    Dictionary<string, double> preProcessedMatrix = (Dictionary<string, double>)studentRow[1];

                    int numItems = preProcessedMatrix.Count;

                    Matrix featureMatrix = new Matrix(1, numItems);

                    List<double> valueList = (preProcessedMatrix.Values).ToList();

                    for (int matrixInput = 0; matrixInput < valueList.Count; matrixInput++)
                    {
                        featureMatrix[0, matrixInput] = valueList[matrixInput];
                    }
                }
                return studentPoint;
            }

            for (int i = 0; i < 10; i++)
            {
                stPointList.Add(MakePoint(i));
            }

            return stPointList;
        }
    }
}