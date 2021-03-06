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
        #region Assets
        private double epsilon;

        private int minNeighbor;

        private List<Point> points;

        private bool getCoresRun = false; //keep for time being

        private ClusterLogger clusterLogger;

        public int ClusterAmount => this.clusterLogger.clusterCount;

        public int NoiseAmount => this.clusterLogger.Noise.Count;
        #endregion

        /// <summary>
        /// Constructor for DBSCAN Class. 
        /// </summary>
        /// <param name="inputDataset"></param>
        /// <param name="inputEpsilon"></param>
        /// <param name="inputMinNeighbor"></param>
        /// <param name="inMetric"></param>
        /// <remarks>
        ///          inputDataset is the list of points to use for clustering,
        ///          inputEpsilon is the radius in which other points must be to be part of the "neighborhood"
        ///          inputMinNeighbor is the amount of points inside of the radius defined by inputEpsilon param that constitutes a "core point"
        ///          inMetric is the equation used for calculating the distance between points
        /// </remarks>
        public DBSCAN(List<Point> inputDataset, double inputEpsilon, int inputMinNeighbor, string inMetric = "Euclidean")
        {
            this.points = inputDataset;
            if (inputEpsilon > 0)
            {
                this.epsilon = inputEpsilon;
            }
            else
            {
                Console.WriteLine("epsilon value must be greater than zero");
            }

            this.minNeighbor = inputMinNeighbor;

            this.clusterLogger = new ClusterLogger();

            this.ClusterIDChanged += clusterLogger.OnClusterIDChanged;
            this.DBScanFinished += clusterLogger.OnDBScanFinished;
        }

        #region Event Handlers for partioning clusters and diagnostics
        /// <summary>
        /// Two events for counting the amount of clusters created and a second event that sends the clustered points to the 
        /// ClusterLogger for partioning into lists of same cluster ID
        /// </summary>
        public delegate void ClusterIDChangedEventHandler(object source, EventArgs args);

        public event ClusterIDChangedEventHandler ClusterIDChanged;

        protected virtual void OnClusterIDChanged() => ClusterIDChanged?.Invoke(this, EventArgs.Empty);


        public delegate void DBScanFinishedEventHandler(object source, ClusterEventArgs args);

        public event DBScanFinishedEventHandler DBScanFinished;

        protected virtual void OnDBScanFinished(List<Point> points) => DBScanFinished?.Invoke(this, new ClusterEventArgs() { Points = points });
        #endregion

        #region Clustering Algorithm
        /// <summary>
        /// Region contains the actual clustering of the points in the data set.
        /// After the main for loop has finished an iteration and ExpandCluster is true 
        /// it increments the ClusterID for the rest of the points.
        /// </summary>
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
        bool ExpandCluster(ref Point point, int ClusterID)
        {
            List<Point> epsilonNeighborhood = new List<Point>();

            List<Point> findNeighborsOf(Point queryPoint) => this.points.Where(currentPoint => currentPoint.DistanceTo(queryPoint) <= this.epsilon).ToList();
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
        #endregion

        public Dictionary<int, List<Point>> ReturnClusteredPoints()
        {
            if (this.getCoresRun == true)
            {
                return this.clusterLogger.clusterList;
            }
            else
            {
                Console.WriteLine("Points are currently unclustered");
                Dictionary<int, List<Point>> nullList = null;
                return nullList;
            }
        }
    }
}