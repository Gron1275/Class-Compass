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
            return tempList;
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
