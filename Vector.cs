namespace RecommendationEngine
{
    public class Vector : ILinearAlgebra
    {
        //<summary>
        //This data type represents a vector (m, 1) and is basically a 2d integer array but vertical rather than horizontal
        //This allows me to do calculations with matrices as if they were actual instances of vectors and matrices in linear algebra
        //</summary>
        private int[,] vectorArray;

        public Vector(int dimensions, string plane="vertical")//Initialize empty vector of dimension (m,1)
        {
            if (plane == "vertical")
            {
                this.vectorArray = new int[dimensions, 1];
            }
            else
            {
                this.vectorArray = new int[1, dimensions];
            }
        }
        public Vector(int[,] entries)//Intialize vector comprised of an inputted 2d integer array
        {
            int xdimension = GetRows(entries);
            int yDimension = GetColumns(entries);
            vectorArray = new int[xdimension, yDimension];
            for (int i = 0; i < xdimension; i++)
            {
                for (int j = 0; j < yDimension; j++)
                {
                    vectorArray[i, j] = entries[i, j];
                }
            }
        }
        public int this[int Xindex, int Yindex]//Index the vector like you would any kind of array
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

        public int GetRows(int[,] input)
        {
            return input.GetLength(0);
        }
        public int GetColumns(int[,] input)
        {
            return input.Length / input.GetLength(0);
        }
    }
}