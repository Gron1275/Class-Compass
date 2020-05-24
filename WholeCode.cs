using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace wholeCodeNamespace
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
        public double[] FeatureArray { get; set; }
        public int StudentID { get; set; }

        #endregion

        //For pointType, 0 will signify noise, 1 a border point, and 2 a core point
        //For clID, "n" will signify which cluster the point is in
        public Point(int inStID, double[] inFeatureArray)
        {
            this.FeatureArray = inFeatureArray;
            this.StudentID = inStID;
            this.clID = null;
            this.pointType = null;
        }
        public string ShowArray() => string.Join(",", this.FeatureArray);
        // if currentPoint.DistanceTo(point[i]) <= eps ...
        /// <summary>
        /// Check the distance between the objects array and the yPoint array
        /// </summary>
        /// <param name="yPoint"></param>
        public double DistanceTo(Point yPoint)
        {
            double sigma = 0.0;
            for (int i = 0; i < this.FeatureArray.Length; i++)
            {
                sigma += Math.Pow(this.FeatureArray[i] - yPoint.FeatureArray[i], 2);
            }
            double distance = Math.Sqrt(sigma);
            return distance;
        }
    }
    class ClusterLogger
    {
        #region Assets
        public int clusterCount = 0;
        private List<Point> internalPoints;
        public Dictionary<int, List<Point>> clusterList;
        public List<Point> Noise
        {
            get
            {
                List<Point> noise = this.internalPoints.Where(qPoint => qPoint.pointType == 0).ToList();
                return noise;
            }
        }
        #endregion
        public ClusterLogger()
        {
            clusterList = new Dictionary<int, List<Point>>();
        }
        #region Events
        /// <summary>
        /// Events raised after portions of the DBSCAN are completed.
        /// OnClusterIDChanged increments the total amount of clusters in the data for later use
        /// OnDBScanFinished internalizes the clustered points from the DBSCAB algorithm and then partions based on Cluster ID
        /// </summary>
        public void OnClusterIDChanged(object source, EventArgs e) => this.clusterCount += 1;
        public void OnDBScanFinished(object source, ClusterEventArgs e)
        {
            this.internalPoints = e.Points;
            this.AggregateClusterList();
        }
        #endregion
        private void AggregateClusterList()
        {
            for (int i = 0; i < this.clusterCount; i++)
            {
                List<Point> ListOfNCLuster = (this.internalPoints.Where(currentPoint => currentPoint.clID == i)).ToList();
                this.clusterList.Add(i, ListOfNCLuster);
            }
        }
    }
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

        public Dictionary<int, List<Point>> ClusteredPoints => this.clusterLogger.clusterList;

        public double timeElapsed;
        #endregion

        /// <summary>
        /// Constructor for DBSCAN Class. 
        /// </summary>
        /// <param name="inputDataset"></param>
        /// <param name="inputEpsilon"></param>
        /// <param name="inputMinNeighbor"></param>
        /// <remarks>
        ///          inputDataset is the list of points to use for clustering,
        ///          inputEpsilon is the radius in which other points must be to be part of the "neighborhood"
        ///          inputMinNeighbor is the amount of points inside of the radius defined by inputEpsilon param that constitutes a "core point"
        ///          inMetric is the equation used for calculating the distance between points
        /// </remarks>
        public DBSCAN(List<Point> inputDataset, double inputEpsilon, int inputMinNeighbor)
        {
            this.points = inputDataset;
            this.epsilon = inputEpsilon;

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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int ClusterID = 0;
            for (int i = 0; i < this.points.Count; i++)
            {
                Point currentPoint = this.points[i];
                if (currentPoint.clID == null)
                {
                    if (ExpandCluster(ref currentPoint, ClusterID) == true)
                    {
                        OnClusterIDChanged();

                        ClusterID += 1;

                        this.getCoresRun = true;
                    }
                }
            }
            stopwatch.Stop();
            this.timeElapsed = stopwatch.ElapsedMilliseconds * 0.001;
            OnDBScanFinished(this.points);
        }
        double DistanceTo(Point xPoint, Point yPoint)
        {
            double sigma = 0.0;
            for (int i = 0; i < this.FeatureArray.Length; i++)
            {
                sigma += Math.Pow(xPoint.FeatureArray[i] - yPoint.FeatureArray[i], 2);
            }
            double distance = Math.Sqrt(sigma);
            return distance;
        }
        List<Point> findNeighborsOf(Point queryPoint)
        {
            List<Point> tempList = new List<Point>();
            foreach (Point currentPoint in this.points)
            {
                if (DistanceTo(queryPoint, currentPoint) <= this.epsilon)
                {
                    tempList.Add(currentPoint);
                }
            }
        }
        bool ExpandCluster(ref Point point, int ClusterID)
        {
            List<Point> epsilonNeighborhood = new List<Point>();
            
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
    }
    class Program
    {
        static void GeneratePoints(ref List<Point> StudentPointList, int numPoints)
        {
            Console.WriteLine($"Generating {numPoints} points...");
            Random rand = new Random();

            double RandDouble() => rand.Next(50, 101) / 100.0;

            for (int i = 0; i < numPoints; i++)
            {
                double doubleRandOne = RandDouble();
                double doubleRandTwo = RandDouble();
                double doubleRandThree = RandDouble();
                double[] array = new double[] { doubleRandOne, doubleRandTwo, doubleRandThree };

                StudentPointList.Add(new Point(i, array));
            }
            Console.WriteLine("Points Generated.");
        }
        static void Main()
        {
            List<Point> StudentPointList = new List<Point>();

            Console.WriteLine("Welcome, User");
            Console.Write("\nHow many test points would you like to generate?: ");

            int numPoints = Convert.ToInt32(Console.ReadLine());

            GeneratePoints(ref StudentPointList, numPoints);

            Console.Clear();
            Console.WriteLine("Running clustering algorithm on points...");

            #region DBSCAN Code
            /// <summary>
            /// Create an instance of the DBSCAN class and find the ideal value for minK before running
            /// </summary>
            int idealMinK = StudentPointList[0].FeatureArray.Length * 2;
            DBSCAN dbScan = new DBSCAN(StudentPointList, inputEpsilon: 0.05, inputMinNeighbor: idealMinK);
            //Best value for minK is 2 * the amt of dimensions
            dbScan.Run();
            #endregion

            #region Diagnostics & Testing Relevant Output
            /// <summary>
            /// Used for separating out the clustered lists and printing their contents before displaying amt of noise
            /// </summary>
            Dictionary<int, List<Point>> clusteredPoints = dbScan.ClusteredPoints;

            List<Point> GetList(int listID) => clusteredPoints[listID];

            void PrintList(int listID)
            {
                Console.WriteLine($"Cluster: {listID}");
                foreach (Point current in GetList(listID))
                {
                    Console.WriteLine($"ID: {current.StudentID} - Array: {current.ShowArray()}");
                }
            }
            Console.WriteLine($"Clusters created: {dbScan.ClusterAmount}");
            Console.WriteLine($"DBSCAN RUNTIME with {StudentPointList.Count} points: {dbScan.timeElapsed}");
            Console.WriteLine($"Noise Amount: {dbScan.NoiseAmount}");
            double noisePercentage = ((double)dbScan.NoiseAmount / StudentPointList.Count) * 100;
            Console.WriteLine($"Noise Percentage: {Math.Floor(noisePercentage)}%");
            Console.Write("Would you like to view contents of a cluster? [Y/N]: ");
            string input = Console.ReadLine();
            while (input == "Y" || input == "y")
            {
                Console.Clear();
                Console.WriteLine($"Amount of clusters: {dbScan.ClusterAmount}");
                Console.Write("Input cluster id to view: ");
                int clusterToView = Convert.ToInt32(Console.ReadLine());
                if (clusterToView < dbScan.ClusterAmount) { PrintList(clusterToView); } else { Console.WriteLine("Error: Index out of Range."); }
                Console.Write("\nView another cluster? [Y/N]: ");
                input = Console.ReadLine();
            }
            Console.Clear();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            #endregion
        }
    }
}
