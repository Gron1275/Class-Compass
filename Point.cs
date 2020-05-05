namespace RecommendationEngine
{
    public class Point
    {

        public double Value { get; set; }
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
    public enum PointType { noise = 0, border = 1, core = 2 };
}