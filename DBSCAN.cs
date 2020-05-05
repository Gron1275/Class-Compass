using System;
using System.Collections.Generic;

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
                List<Point> tempList = new List<Point>();
                foreach (Point currentPoint in this.points)
                {
                    if (distCalc(queryPoint.Value, currentPoint.Value) <= this.epsilon)
                    {
                        tempList.Add(currentPoint);
                    }
                }
                return tempList;
            }
            epsilonNeighborhood = findNeighborsOf(point);

            if (epsilonNeighborhood.Count < this.minNeighbor)
            {
                point.pointType = PointType.noise;
                return false;
            }
            else
            {
                foreach (Point nPoint in this.points)
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

                    }
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