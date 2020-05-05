namespace RecommendationEngine
{
    public class Point //Could substitute person for point in final
    {

        public double Value { get; set; } //Could make this a vector w/ all the diff grades & classes
        public int? clID;
        public PointType? pointType;
        private double value;

        //For ClID, 0 will signify noise, 1 a border point, and 2 a core point
        Point()
        {
            this.clID = null;
            this.pointType = null;
        }

    }
}
