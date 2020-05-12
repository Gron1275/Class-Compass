using System.Collections.Generic;
using System.Linq;

namespace RecommendationEngine
{
    class SimilarityCalculator
    {
        //Could maybe just have the point similarity be within the point class so there would be less files. Might not be good b/c a list of points is required for input
        private List<Point> clusterList;
        private Point pointInQuestion;
        private string metric;
        private double inputHat;
        private List<Point> similarityList;

        public SimilarityCalculator(Point inputPoint, List<Point> inputClustList) //inputClustList should be a single cluster's list, not the dict of all clusters
        {
            this.clusterList = inputClustList;
            this.pointInQuestion = inputPoint;
        }

        public void CalculatePointSimilarity()
        {
            //Put in pierson correlation equation and maybe manhatten equation as well.
            List<Point> tempList = new List<Point>(); //Create list that doesn't contain pointInQuestion
            tempList = this.clusterList.Where(qPoint => qPoint != this.pointInQuestion).ToList();
            double GetHat(Matrix inputPointMatrix)
            {
                double sigma = 0.0;
                double average;
                for (int i = 0; i < inputPointMatrix.GetColumns(); i++)
                {
                    sigma += inputPointMatrix[0, i];
                }
                average = sigma / (double)inputPointMatrix.GetColumns();
                return average;
            }//I'm gonna need to abstract this out and have a single method running the correlation
             //for the inputPoint and whatever row of the list that the iterator is on.
            this.inputHat = GetHat(this.pointInQuestion.featMatrix);

            //double topSigma = 0.0;

            for (int i = 0; i < tempList.Count; i++) //Top half of pierson
            {
                double xDifference = i;//shouldnt equal i its just placeholder
            }
        }
    }
}
