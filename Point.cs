using System;
using System.Collections.Generic;

namespace RecommendationEngine
{
    public class Point //Could substitute person for point in final
    {

        //Could make this (double value vafriable) a vector w/ all the diff grades & classes
        public int? clID;
        public PointType? pointType;
        public double value;
        private int studentID;
        public double PartOne { get; set; }
        public double PartTwo { get; set; }
        public double PartThree { get; set; }
        public int StudentID { get => this.studentID; set => this.studentID = value; }

        public Matrix featMatrix;


        public double[] classArray;
        //For pointType, 0 will signify noise, 1 a border point, and 2 a core point
        //For clID, "n" will signify which cluster the point is in
        public Point(int inStID, double inValue)
        {
            this.StudentID = inStID;
            this.value = inValue;
            this.clID = null;
            this.pointType = null;
        }
        public Point(Matrix inMatrix)
        {
            this.featMatrix = inMatrix ?? throw new ArgumentNullException(nameof(inMatrix));
            this.clID = null;
            this.pointType = null;
        }
        public Point(int inStID, Matrix inMatrix)
        {
            this.featMatrix = inMatrix;
            this.StudentID = inStID;
            this.clID = null;
            this.pointType = null;
        }
        public Point()
        {
            this.clID = null;
            this.pointType = null;
        }
        public string ShowMatrix()
        {
            List<double> final = new List<double>();
            int len = this.featMatrix.GetColumns();
            string finalString;
            for (int i = 0; i < len; i++)
            {
                final.Add(this.featMatrix[0, i]);
            }
            finalString = string.Join(",", final.ToArray());

            return finalString;
        }
        // if currentPoint.DistanceTo(point[i]) <= eps ...
        /// <summary>
        /// Check the distance between the objects array and the yPoint array
        /// </summary>
        /// <param name="yPoint"></param>
        /// <param name="equation"></param>
        /// <returns></returns>
        public double DistanceTo(Point yPoint, string equation = "euclidean")
        {
            double distance;

            if (equation == "euclidean")
            {
                int columns = (this.featMatrix.GetColumns() == yPoint.featMatrix.GetColumns()) ? this.featMatrix.GetColumns() : 0;
                //Console.WriteLine($"Rows of matrix: {rows} - Columns of matrix: {this.featMatrix.GetColumns()}");
                //int columns = (this.featMatrix.GetColumns() == yPoint.featMatrix.GetColumns()) ? this.featMatrix.GetColumns() : 0;
                //might not need columns bc its only going thru one row of the big matrix. however maybe i hafta iterate thru columns rather than rows.
                double sigma = 0.0;

                for (int i = 0; i < columns; i++)
                {
                    sigma += Math.Pow((this.featMatrix[0, i] - yPoint.featMatrix[0, i]), 2);
                }
                distance = Math.Sqrt(sigma);
                return distance;
            }
            else if (equation == "minkowski")
            {
                // put in minkowski code
                return 0;
            }
            else
            {
                return 0;
            }
        }
    }
}
