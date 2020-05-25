using System;
using System.Collections.Generic;

namespace wholeCodeNamespace
{
    public enum PointType
    {
        noise = 0,
        border = 1, 
        core = 2
    };
    public class Point
    {
        #region Assets
        public int? clID;
        public PointType? pointType;
        public double[] FeatureArray { get; set; }
        public int PointID { get; set; }
        private bool parity;

        #endregion

        //For pointType, 0 will signify noise, 1 a border point, and 2 a core point
        //For clID, "n" will signify which cluster the point is in
        public Point(int inPID, double[] inFeatureArray)
        {
            this.FeatureArray = inFeatureArray;
            this.PersonID = inPID;
            this.clID = null;
            this.pointType = null;
        }
        public string ShowArray() => string.Join(",", this.FeatureArray);
        // if currentPoint.DistanceTo(point[i]) <= eps ...
        /// <summary>
        /// Check the distance between the objects array and the yPoint array
        /// </summary>
        /// <param name="yPoint"></param>
        public double DistanceTo(Point yPoint)
        {
            double sigma = 0.0;
            for (int i = 0; i < this.FeatureArray.Length; i++)
            {
                sigma += Math.Pow(this.FeatureArray[i] - yPoint.FeatureArray[i], 2);
            }
            double distance = Math.Sqrt(sigma);
            return distance;
        }
        public void ParityCheck()
        {
            double sigma = 0.0;
            for (int i = 0; i < this.FeatureArray.Length; i++)
            {
                sigma += this.FeatureArray[i];
            }
            if (((int)sigma % 2) == 0)
            {
                this.parity = true;
            }
            else
            {
                this.parity = false;
            }
        }
    }
}
