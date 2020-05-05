namespace RecommendationEngine
{
    class RecEngine
    {
        Vector uVector; //User vector with dimensions        -->      (n, 1)
        Vector finalDecisionMatrix; //Final vector after transposition and dot product
        Matrix dMatrix; //Data tag matrix with dimensions    -->      (m, n)
        Matrix TdMatrix; //Transposed data tag matrix with dimensions (n, m)
        int counter;
        public RecEngine(Vector inVector, Matrix inMatrix)
        {
            this.uVector = inVector;//Local vector
            this.dMatrix = inMatrix;//Local matrix

            this.TdMatrix = dMatrix.Transpose();//Transpose local matrix to prepare for dot product

            this.finalDecisionMatrix = CalculateDMatrix();//Find the dot product of the local vector and transposed local matrix
        }
        public Vector CalculateDMatrix() => TdMatrix.DotProduct(uVector);//Calculate dot product (might remove bc this could be done without a method)
        public void ShowRecs()//Output the final decisions
        {                     //This will get more advanced as it grows to outputting actual class names
            counter = uVector.GetRows();//counter to output the final decisions

            System.Console.WriteLine($"{finalDecisionMatrix.GetRows()}, {finalDecisionMatrix.GetColumns()}");
            System.Console.Write("\n [");
            for (int i = 0; i < counter; i++)
            {
                System.Console.Write($" {finalDecisionMatrix[i, 0]}");
            }
            System.Console.Write("]\n");
        }
    }
}