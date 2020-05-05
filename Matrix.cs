namespace RecommendationEngine
{
    public class Matrix : ILinearAlgebra
    {
        //<summary>
        //I created this class because, while at it's core it is little more than an 2d integer array (int[,]),
        //I wanted the freedom to use these arrays in particular ways and be able to name them Matrices for clarity 
        //</summary>
        private int[,] matrix;
        private int rows;
        private int columns;
        public Matrix(int xDimension, int yDimension)//Creating an empty matrix of (m, n) size
        {
            matrix = new int[xDimension, yDimension];
            rows = GetRows();//Accesses the internal "GetRows()" method for the private matrix array
            columns = GetColumns();//Same as above but for the columns
        }
        public Matrix(int[,] entries)//Creating a matrix given an array of integers
        {
            rows = GetRows(entries);//This is the method for the external getRows of something unknown to the object instance
            columns = GetColumns(entries);//Same as above but for the columns
            matrix = new int[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = entries[i, j];//Copy contents of array into the matrix object
                }
            }
        }
        public Matrix(Point[,] entries)
        {

        }
        public int this[int Xindex, int Yindex]
        {
            get => matrix[Xindex, Yindex];//Not in standard { get; set; } form b/c I might alter index protection
            set => matrix[Xindex, Yindex] = value;
        }
        public Matrix Transpose()
        {
            Matrix transposedMatrix = new Matrix(columns, rows);//For a matrix of dimensions (m, n), create matrix of dimensions (n, m)
            for (int i = 0; i < rows; i++) //Iterator for the rows of the matrix
            {
                for (int j = 0; j < columns; j++)// Iterator for the cio
                {
                    transposedMatrix[j, i] = matrix[i, j];//"Swap" values to change matrix shape from (m, n) to (n, m)
                }
            }
            return transposedMatrix;
        }
        public Vector DotProduct(Vector inVector)
        {
            int sigma = 0; //Used in finding summation (sigma) of each row

            Vector final = new Vector(rows);//Create a vector in which you can store the values

            // Now that the data matrix is transposed to be the shape of the vector, we can find the dot product
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    sigma += inVector[j, 0] * matrix[j, i]; //Summation of the product of n row of vector and nsubm row of matrix
                }
                final[i, 0] = sigma;
                sigma = 0;//Reset the summation for every new row
            }
            return final; //Return array of the summations of each row
        }
        public double CalculateSimilarity(Matrix inputMatrix)
        {
            return 0.0;
        }
        public int GetColumns() => matrix.Length / matrix.GetLength(0); //Divid the total amt of elements by a column length to get row length
        public int GetRows() => matrix.GetLength(0);
        public int GetColumns(int[,] input)//same as get columns but used for input
        {
            return input.Length / input.GetLength(0);
        }
        public int GetRows(int[,] input)
        {
            return input.GetLength(0);
        }
        public int GetColumns(Point[,] input)
        {
            return input.Length / input.GetLength(0);
        }
        public int GetRows(Point[,] input)
        {
            return input.GetLength(0);
        }
    }
}