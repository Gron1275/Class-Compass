using System;
using System.Collections.Generic;

namespace RecommendationEngine
{
    class SimilarityCalculator
    {
        private List<Point> clusterList;
        private Point pointInQuestion;
        private string metric;
        private List<Point> similarityList;

        public SimilarityCalculator(Point inputPoint,List<Point> inputClustList)
        {
            this.clusterList = inputClustList;
            this.pointInQuestion = inputPoint;
        }

        public void CalculatePointSimilarity()
        {
            //Put in pierson correlation equation and maybe manhatten equation as well.
        }
    }
}
