namespace RecommendationEngine
{
    public class Vector //Might not need this to have the interface linear algebra anymore
    {
        //<summary>
        //This data type represents a vector (m, 1) and is basically a 2d integer array but vertical rather than horizontal
        //This allows me to do calculations with matrices as if they were actual instances of vectors and matrices in linear algebra
        //</summary>
        private double[,] vectorArray;

        public Vector(int dimensions, string plane = "vertical")//Initialize empty vector of dimension (m,1)
        {
            if (plane == "vertical")
            {
                this.vectorArray = new double[dimensions, 1];
            }
            else
            {
                this.vectorArray = new double[1, dimensions];
            }
        }
        public Vector(double[,] entries)//Intialize vector comprised of an inputted 2d integer array
        {
            int xdimension = GetRows(entries);
            int yDimension = GetColumns(entries);
            vectorArray = new double[xdimension, yDimension];
            for (int i = 0; i < xdimension; i++)
            {
                for (int j = 0; j < yDimension; j++)
                {
                    vectorArray[i, j] = entries[i, j];
                }
            }
        }
        public double this[int Xindex, int Yindex]//Index the vector like you would any kind of array
        {
            get { return vectorArray[Xindex, Yindex]; }//Again, idk why I don't just do this shorthand
            set { vectorArray[Xindex, Yindex] = value; }
        }
        public Vector DotProduct(Vector inVector)//Not rly needed, might be useful with userBased RecEngine but only here rn so i can interface ILinAlg
        {
            return inVector;
        }

        public int GetColumns() => vectorArray.Length / (vectorArray.GetLength(0));
        public int GetRows() => vectorArray.GetLength(0);

        public int GetRows(double[,] input)
        {
            return input.GetLength(0);
        }
        public int GetColumns(double[,] input)
        {
            return input.Length / input.GetLength(0);
        }
    }
}