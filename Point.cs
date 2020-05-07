using System;

namespace RecommendationEngine
{
    public class Point //Could substitute person for point in final
    {

         //Could make this (double value vafriable) a vector w/ all the diff grades & classes
        public int? clID;
        public PointType? pointType;
        public double value;

        public int stID;

        public Matrix featMatrix;
        //For pointType, 0 will signify noise, 1 a border point, and 2 a core point
        //For clID, "n" will signify which cluster the point is in
        public Point(int inStID, double inValue)
        {
            this.stID = inStID;
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
        public Point()
        {
            this.clID = null;
            this.pointType = null;
        }
        // if currentPoint.DistanceTo(point[i]) <= eps ...
        public double DistanceTo(Point yPoint)
        {
            double distance;

            int rows = (this.featMatrix.GetRows() == yPoint.featMatrix.GetRows()) ? this.featMatrix.GetRows() : 0;
            //int columns = (this.featMatrix.GetColumns() == yPoint.featMatrix.GetColumns()) ? this.featMatrix.GetColumns() : 0;
            //might not need columns bc its only going thru one row of the big matrix. however maybe i hafta iterate thru columns rather than rows.
            double sigma = 0.0;

            for (int i = 0; i < rows; i++)
            {
                sigma += Math.Pow((this.featMatrix[i, 0] - yPoint.featMatrix[i, 0]), 2);
            }
            distance = Math.Sqrt(sigma);


            return distance;
        }
    }
}
