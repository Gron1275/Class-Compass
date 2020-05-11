using System;
using System.Collections.Generic;
using System.Linq;

namespace RecommendationEngine
{
    class ClusterLogger
    {
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

        public ClusterLogger()
        {
            clusterList = new Dictionary<int, List<Point>>();
        }

        public void OnClusterIDChanged(object source, EventArgs e) => this.clusterCount += 1;

        public void DisplayClusterCount()
        {
            Console.WriteLine($"Number of clusters generated: {this.clusterCount}");
        }
        public void GetAvgClusterCapacity()
        {
            //write code to find average cluster size. could also use this later for finding best vals of eps and minK w/ inertia
            //SHould implement sillohuete scores tho bc its better for the dbscan algo
        }
        public void OnDBScanFinished(object source, ClusterEventArgs e)
        {
            this.internalPoints = e.Points;
            this.AggregateClusterList();
        }
        private void AggregateClusterList()
        {
            for (int i = 0; i < this.clusterCount; i++)
            {
                List<Point> ListOfNCLuster = (this.internalPoints.Where(currentPoint => currentPoint.clID == i)).ToList();
                this.clusterList.Add(i, ListOfNCLuster);
            }
        }
        
    }
}