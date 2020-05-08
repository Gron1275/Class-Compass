using System;
using System.Collections.Generic;
using System.Linq;

namespace RecommendationEngine
{
    public class ClusterEventArgs : EventArgs
    {
        public List<Point> Points { get; set; }
    }
    public class DBSCAN
    {
        private double epsilon;
        private int minNeighbor;
        private List<Point> points;
        private bool getCoresRun = false; //keep for time being

        public DBSCAN(List<Point> inputDataset, double inputEpsilon, int inputMinNeighbor, string inMetric = "Euclidean")
        {
            this.points = inputDataset;
            
            //Console.WriteLine($"localized point set first val {this.points[0].value}");
            //Console.WriteLine($"inputdata set first val {inputDataset[0].value}");
            this.epsilon = inputEpsilon;
            this.minNeighbor = inputMinNeighbor;

            ClusterLogger clusterLogger = new ClusterLogger();

            this.ClusterIDChanged += clusterLogger.OnClusterIDChanged;
            this.DBScanFinished += clusterLogger.OnDBScanFinished;
        }

        /////////////////////////////////////////////////////////////////////////////////////
        public delegate void ClusterIDChangedEventHandler(object source, EventArgs args);

        public event ClusterIDChangedEventHandler ClusterIDChanged;

        protected virtual void OnClusterIDChanged() => ClusterIDChanged?.Invoke(this, EventArgs.Empty);

        public delegate void DBScanFinishedEventHandler(object source, ClusterEventArgs args);

        public event DBScanFinishedEventHandler DBScanFinished;

        protected virtual void OnDBScanFinished(List<Point> points) => DBScanFinished?.Invoke(this, new ClusterEventArgs() { Points = points });
        ////////////////////////////////////////////////////////////////////////////////////
        
        double DistCalc(double x1, double x2) => Math.Sqrt(Math.Pow((x1 - x2), 2)); //Currently only accounts for scalars, cant do matrices yet
        bool ExpandCluster(ref Point point, int ClusterID)
        {
            List<Point> epsilonNeighborhood = new List<Point>();

            //List<Point> findNeighborsOf(Point queryPoint) => this.points.Where(currentPoint => DistCalc(queryPoint.value, currentPoint.value) <= this.epsilon).ToList();

            List<Point> findNeighborsOf(Point queryPoint) => this.points.Where(currentPoint => currentPoint.DistanceTo(queryPoint) <= this.epsilon).ToList();
            epsilonNeighborhood = findNeighborsOf(point);
            //Console.WriteLine(epsilonNeighborhood.Count);
            if (epsilonNeighborhood.Count < this.minNeighbor)
            {
                //Console.WriteLine("Point Failure");
                point.pointType = PointType.noise;
                return false;
            }
            else
            {
                foreach (Point nPoint in epsilonNeighborhood)
                {
                    nPoint.clID = ClusterID;
                    //Console.WriteLine($"ClusterID {ClusterID} assigned to point {nPoint.stID}");
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
        public void Run()
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
                        OnClusterIDChanged();

                        ClusterID += 1;
                        
                        this.getCoresRun = true;
                    }
                    else
                    {
                        //Console.WriteLine($"Point {i} failed the vibe check");
                    }
                }
            }
            OnDBScanFinished(this.points);
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