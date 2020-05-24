using System;
using System.Collections.Generic;

namespace RecommendationEngine
{
    public enum PointType
    {
        noise = 0,
        border = 1, //Actually designate which points are border points the suggestions given to them from their cluster are weighted less than their interests 
                    //b/c they are less similar so their recommendations should be based more on the item based part of the algo than the user similarity
        core = 2
    };
    public class Point
    {
        #region Assets
        public int? clID;
        public PointType? pointType;
        private double[] featureArray;
        public double[] FeatureArray { get; set; }
        public int StudentID { get; set; }
        #endregion
        //For pointType, 0 will signify noise, 1 a border point, and 2 a core point
        //For clID, "n" will signify which cluster the point is in
        public Point(int inStID, double[] inFeatureArray)
        {
            this.featureArray = inFeatureArray;
            this.StudentID = inStID;
            this.clID = null;
            this.pointType = null;
        }
        public string ShowArray() => string.Join(",", this.featureArray);
    }
}
