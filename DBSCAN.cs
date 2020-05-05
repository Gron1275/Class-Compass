using System;
using System.Collections.Generic;
using System.Linq;

namespace RecommendationEngine
{
    class DBSCAN
    {
        private double epsilon;
        private int minNeighbor;
        private List<Point> points;

        public DBSCAN(List<Point> inputDataset, int inputEpsilon, int inputMinNeighbor, string inMetric = "Euclidean")
        {
            this.points = inputDataset;
            this.epsilon = inputEpsilon;
            this.minNeighbor = inputMinNeighbor;
        }
        double distCalc(double x1, double x2) => Math.Sqrt(Math.Pow((x1 - x2), 2)); //Currently only accounts for scalars, cant do matrices yet

        bool ExpandCluster(Point point, int ClusterID)
        {
            List<Point> epsilonNeighborhood = new List<Point>();

            List<Point> findNeighborsOf(Point queryPoint)
            {
                return this.points.Where(currentPoint => distCalc(queryPoint.Value, currentPoint.Value) <= this.epsilon).ToList();
            }

            epsilonNeighborhood = findNeighborsOf(point);

            if (epsilonNeighborhood.Count < this.minNeighbor)
            {
                point.pointType = PointType.noise;
                return false;
            }
            else
            {
                foreach (Point nPoint in epsilonNeighborhood)
                {
                    nPoint.clID = ClusterID;
                }
                epsilonNeighborhood.Remove(point);
                while (epsilonNeighborhood.Count != 0)
                {
                    Point currentPoint = epsilonNeighborhood[0];
                    List<Point> result = findNeighborsOf(currentPoint);
                    if (result.Count >= this.minNeighbor)
                    {
                        Point currentResult = result[0];

                        switch (currentResult.clID)
                        {
                            case null:
                                currentResult.clID = ClusterID;
                                epsilonNeighborhood.Add(currentResult);
                                break;
                            case 0:
                                break;
                            default:
                                break;
                        }
                    }
                    epsilonNeighborhood.Remove(currentPoint);
                }
                return true;
            }
        }
        private void GetCores()
        {
            int ClusterID = 0;
            for (int i = 0; i < points.Count; i++)
            {
                Point currentPoint = points[i];
                if (currentPoint.clID == null)
                {
                    if (ExpandCluster(currentPoint, ClusterID) == true)
                    {
                        ClusterID += 1;
                    }
                }
            }
        }
    }
}