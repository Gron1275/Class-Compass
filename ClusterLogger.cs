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
    }
}