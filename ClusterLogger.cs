using System;

namespace RecommendationEngine
{
    class ClusterLogger
    {
        public int clusterCount = 0;
        public void OnClusterIDChanged(object source, EventArgs e)
        {
            //Console.WriteLine($"Current amount of clusters: {clusterCount}");
            this.clusterCount += 1;
        }
        public void DisplayClusterCount()
        {
            Console.WriteLine($"Number of clusters generated: {this.clusterCount}");
        }
        public void GetAvgClusterCapacity()
        {
            //write code to find average cluster size. could also use this later for finding best vals of eps and minK w/ inertia
            //SHould implement sillohuete scores tho bc its better for the dbscan algo
        }
        public void OnDBScanFinished(object source, EventArgs e)
        {

        }
        private void AggregateClusterList()
        {

        }
        
    }
}