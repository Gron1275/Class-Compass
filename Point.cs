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
        public double[] featureArray;
        public int StudentID { get; set; }

        //For pointType, 0 will signify noise, 1 a border point, and 2 a core point
        //For clID, "n" will signify which cluster the point is in
        public Point(int inStID, double inValue)
        {
            this.StudentID = inStID;
            this.value = inValue;
            this.clID = null;
            this.pointType = null;
        }

        public Point(int inStID, double[] inFeatureArray)
        {
            this.featureArray = inFeatureArray;
            this.StudentID = inStID;
            this.clID = null;
            this.pointType = null;
        }
        public Point()
        {
            this.clID = null;
            this.pointType = null;
        }

        public string ShowArray() => string.Join(",", this.featureArray);
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
                double sigma = 0.0;

                for (int i = 0; i < this.featureArray.Length; i++)
                {
                    sigma += Math.Pow(this.featureArray[i] - yPoint.featureArray[i], 2);
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
