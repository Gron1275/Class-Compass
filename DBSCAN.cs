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
        private bool getCoresRun = false;

        public DBSCAN(List<Point> inputDataset, double inputEpsilon, int inputMinNeighbor, string inMetric = "Euclidean")
        {
            this.points = inputDataset;
            this.epsilon = inputEpsilon;
            this.minNeighbor = inputMinNeighbor;
            GetCores();
        }
        double DistCalc(double x1, double x2) => Math.Sqrt(Math.Pow((x1 - x2), 2)); //Currently only accounts for scalars, cant do matrices yet
        bool ExpandCluster(ref Point point, int ClusterID)
        {
            List<Point> epsilonNeighborhood = new List<Point>();

            List<Point> findNeighborsOf(Point queryPoint) => this.points.Where(currentPoint => DistCalc(queryPoint.Value, currentPoint.Value) <= this.epsilon).ToList();

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
            for (int i = 0; i < this.points.Count; i++)
            {
                Point currentPoint = this.points[i];
                if (currentPoint.clID == null)
                {
                    if (ExpandCluster(ref currentPoint, ClusterID) == true) //IDK if ref is necessary, I need to make sure value of point is changed 
                                                                            //and the change is present in the this.points
                    {
                        ClusterID += 1;
                    }
                }
            }
            this.getCoresRun = true;
        }
        public List<Point> ReturnClusteredPoints()
        {
            if (this.getCoresRun == true)
            {
                return this.points;
            }
            else
            {
                Console.WriteLine("Points are currently unclustered");
                List<Point> nullList = new List<Point>();
                return nullList;
            } 
        }
    }
}