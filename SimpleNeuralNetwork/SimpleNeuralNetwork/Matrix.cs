using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace SimpleNeuralNetwork
{
    public class Matrix
    {
        private readonly double[,] _matrix;

        public int Rows => _matrix.GetLength(0);
        public int Columns => _matrix.GetLength(1);
        public bool IsMatrixSquare => Rows == Columns;

        public Matrix(double[,] matrix)
        {
            if (matrix == null)
            {
                _matrix = new double[0, 0];
                return;
            }

            int matrixRows = matrix.GetLength(0);
            int matrixColumns = matrix.GetLength(1);

            double[,] copy = new double[matrixRows, matrixColumns];

            for (int i = 0; i < matrixRows; i++)
            {
                for (int j = 0; j < matrixColumns; j++)
                {
                    copy[i, j] = matrix[i, j];
                }
            }

            _matrix = copy;
        }

        public Matrix(double[] inputArray)
        {
            if (inputArray == null)
            {
                _matrix = new double[0, 0];
                return;
            }

            int matrixColumns = inputArray.Length;

            double[,] copy = new double[1, matrixColumns];

            for (int j = 0; j < matrixColumns; j++)
            {
                copy[0, j] = inputArray[j];
            }

            _matrix = copy;
        }

        public Matrix(int[] inputArray)
        {
            if (inputArray == null)
            {
                _matrix = new double[0, 0];
                return;
            }

            int matrixColumns = inputArray.Length;

            double[,] copy = new double[1, matrixColumns];

            for (int j = 0; j < matrixColumns; j++)
            {
                copy[0, j] = inputArray[j];
            }

            _matrix = copy;
        }

        public Matrix Sum(Matrix other)
        {
            return Sum(this, other);
        }

        public static Matrix Sum(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
            {
                throw new Exception("Matrix did not match sum rules.");
            }

            double[,] resultMatrix = new double[m1.Rows, m1.Columns];

            for (int i = 0; i < m1.Rows; i++)
            {
                double[] firstMatrixRow = GetMatrixRow(m1, i);
                double[] secondMatrixRow = GetMatrixRow(m2, i);

                double[] sumArray = SumArrays(firstMatrixRow, secondMatrixRow);

                for (int j = 0; j < sumArray.Length; j++)
                {
                    resultMatrix[i, j] = sumArray[j];
                }
            }

            return new Matrix(resultMatrix);
        }

        public Matrix Subtract(Matrix other)
        {
            return Subtract(this, other);
        }

        public static Matrix Subtract(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
            {
                throw new Exception("Matrix did not match subtraction rules.");
            }

            double[,] resultMatrix = new double[m1.Rows, m1.Columns];

            for (int i = 0; i < m1.Rows; i++)
            {
                double[] firstMatrixRow = GetMatrixRow(m1, i);
                double[] secondMatrixRow = GetMatrixRow(m2, i).Select(x => -x).ToArray();

                double[] sumArray = SumArrays(firstMatrixRow, secondMatrixRow);

                for (int j = 0; j < sumArray.Length; j++)
                {
                    resultMatrix[i, j] = sumArray[j];
                }
            }

            return new Matrix(resultMatrix);
        }

        public Matrix Multiply(Matrix other)
        {
            return Multiply(this, other);
        }

        public static Matrix Multiply(Matrix m1, Matrix m2)
        {
            if (m1.Columns != m2.Rows)
            {
                throw new Exception("Matrix did not match multiplication rules.");
            }

            double[,] resultMatrix = new double[m1.Rows, m2.Columns];

            for (int i = 0; i < resultMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultMatrix.GetLength(1); j++)
                {
                    double[] firstMatrixRow = GetMatrixRow(m1, i);
                    double[] secondMatrixColumn = GetMatrixColumn(m2, j);

                    double[] multipliedArray = MultiplyArrays(firstMatrixRow, secondMatrixColumn);
                    double multipliedArraySum = multipliedArray.Sum();

                    resultMatrix[i, j] = multipliedArraySum;
                }
            }

            return new Matrix(resultMatrix);
        }

        public Matrix Multiply(double amount)
        {
            return Multiply(this, amount);
        }

        public static Matrix Multiply(Matrix m, double amount)
        {
            if (m == null)
            {
                throw new Exception("Matrix was null");
            }

            double[,] matrix = m.GetMatrix();

            for (int i = 0; i < m.Rows; i++)
            {
                for (int j = 0; j < m.Columns; j++)
                {
                    matrix[i, j] = amount * matrix[i, j];
                }
            }

            return new Matrix(matrix);
        }

        private static double[] GetMatrixRow(Matrix m, int rowIndex)
        {
            if (m == null)
            {
                throw new Exception("Matrix was null");
            }

            if (rowIndex >= m.Rows || rowIndex < 0)
            {
                throw new Exception("\"rowIndex\" was outside matrix' bounds");
            }

            double[,] matrix = m.GetMatrix();
            double[] res = new double[m.Columns];

            for (int j = 0; j < m.Columns; j++)
            {
                res[j] = matrix[rowIndex, j];
            }

            return res;
        }

        private static double[] GetMatrixColumn(Matrix m, int columnIndex)
        {
            if (m == null)
            {
                throw new Exception("Matrix was null");
            }

            if (columnIndex >= m.Columns || columnIndex < 0)
            {
                throw new Exception("\"columnIndex\" was outside matrix' bounds");
            }

            double[,] matrix = m.GetMatrix();
            double[] res = new double[m.Rows];

            for (int i = 0; i < m.Rows; i++)
            {
                res[i] = matrix[i, columnIndex];
            }

            return res;
        }

        private static double[] SumArrays(double[] array1, double[] array2)
        {
            if (array1 == null || array2 == null)
            {
                throw new Exception("Array was null");
            }

            if (array1.Length != array2.Length)
            {
                throw new Exception("Arrays did not match sum rules");
            }

            double[] res = new double[array1.Length];

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = array1[i] + array2[i];
            }

            return res;
        }

        private static double[] MultiplyArrays(double[] array1, double[] array2)
        {
            if (array1 == null || array2 == null)
            {
                throw new Exception("Array was null");
            }

            if (array1.Length != array2.Length)
            {
                throw new Exception("Arrays did not match multiplication rules");
            }

            double[] res = new double[array1.Length];

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = array1[i] * array2[i];
            }

            return res;
        }

        public Matrix Transpose()
        {
            return Transpose(this);
        }

        public static Matrix Transpose(Matrix m)
        {
            if (m == null)
            {
                throw new Exception("Matrix was null");
            }

            double[,] transposedMatrix = new double[m.Columns, m.Rows];
            double[,] matrixToTranspose = m.GetMatrix();

            for (int i = 0; i < transposedMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < transposedMatrix.GetLength(1); j++)
                {
                    transposedMatrix[i, j] = matrixToTranspose[j, i];
                }
            }

            return new Matrix(transposedMatrix);
        }

        public double[,] GetMatrix()
        {
            int columnsCount = _matrix.GetLength(0);
            int rowsCount = _matrix.GetLength(1);
            double[,] copy = new double[columnsCount, rowsCount];

            for (int i = 0; i < columnsCount; i++)
            {
                for (int j = 0; j < rowsCount; j++)
                {
                    copy[i, j] = _matrix[i, j];
                }
            }

            return copy;
        }

        public double this[int i, int j] => _matrix[i, j];

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                sb.Append($"{"(",-3}");

                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    sb.Append($"{_matrix[i, j],-3} ");
                }

                sb.Append(")\n");
            }

            return sb.ToString();
        }
    }
}