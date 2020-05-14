namespace RecommendationEngine
{
    public class Matrix
    {
        //<summary>
        //I created this class because, while at it's core it is little more than an 2d double array (int[,]),
        //I wanted the freedom to use these arrays in particular ways and be able to name them Matrices for clarity 
        //</summary>
        private double[,] matrix;
        private int rows;
        private int columns;
        public Matrix(int xDimension, int yDimension)//Creating an empty matrix of (m, n) size
        {
            this.matrix = new double[xDimension, yDimension];
            this.rows = this.GetRows();//Accesses the internal "GetRows()" method for the private matrix array
            this.columns = this.GetColumns();//Same as above but for the columns
        }
        public Matrix(double[,] entries)//Creating a matrix given an array of integers
        {
            this.rows = this.GetRows(entries);//This is the method for the external getRows of something unknown to the object instance
            this.columns = this.GetColumns(entries);//Same as above but for the columns
            this.matrix = new double[this.rows, this.columns];
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.columns; j++)
                {
                    this.matrix[i, j] = entries[i, j];//Copy contents of array into the matrix object
                }
            }
        }
        public double this[int Xindex, int Yindex]
        {
            get => this.matrix[Xindex, Yindex];//Not in standard { get; set; } form b/c I might alter index protection
            set => this.matrix[Xindex, Yindex] = value;
        }
        public Matrix Transpose()
        {
            Matrix transposedMatrix = new Matrix(this.columns, this.rows);//For a matrix of dimensions (m, n), create matrix of dimensions (n, m)
            for (int i = 0; i < this.rows; i++) //Iterator for the rows of the matrix
            {
                for (int j = 0; j < this.columns; j++)// Iterator for the cio
                {
                    transposedMatrix[j, i] = this.matrix[i, j];//"Swap" values to change matrix shape from (m, n) to (n, m)
                }
            }
            return transposedMatrix;
        }
        public Vector DotProduct(Vector inVector)
        {
            double sigma = 0; //Used in finding summation (sigma) of each row

            Vector final = new Vector(this.rows);//Create a vector in which you can store the values

            // Now that the data matrix is transposed to be the shape of the vector, we can find the dot product
            for (int i = 0; i < this.columns; i++)
            {
                for (int j = 0; j < this.rows; j++)
                {
                    sigma += inVector[j, 0] * this.matrix[j, i]; //Summation of the product of n row of vector and nsubm row of matrix
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
        public int GetColumns() => this.matrix.Length / this.matrix.GetLength(0); //Divid the total amt of elements by a column length to get row length
        public int GetRows() => this.matrix.GetLength(0);
        public int GetColumns(double[,] input)//same as get columns but used for input
        {
            return input.Length / input.GetLength(0);
        }
        public int GetRows(double[,] input)
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