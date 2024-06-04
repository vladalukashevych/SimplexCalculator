using Fractions;
using System.Linq;

namespace SimplexCalculator
{
    public class SimplexSnap
    {
        public Fraction[] B { get; private set; }
        public Fraction[][] Matrix { get; private set; }
        public Fraction[] M { get; private set; }
        public Fraction[] F { get; private set; }
        public int[] C { get; private set; }
        public Fraction FunctionValue { get; private set; }
        public Fraction[] FunctionVariables { get; private set; }
        public bool IsArtificialDone { get; private set; }
        public bool[] IsArtificial { get; private set; }

        public SimplexSnap(Fraction[] b, Fraction[][] matrix, Fraction[] M, Fraction[] F, int[] C, Fraction[] fVars, bool isMDone, bool[] m)
        {
            this.B = Copy(b);
            this.Matrix = Copy(matrix);
            this.M = Copy(M);
            this.F = Copy(F);
            this.C = Copy(C);
            this.IsArtificialDone = isMDone;
            this.IsArtificial = Copy(m);
            this.FunctionVariables = Copy(fVars);
            FunctionValue = 0;
            for (int i = 0; i < C.Length; i++)
            {
                FunctionValue += fVars[C[i]] * b[i];
            }
        }

        T[] Copy<T>(T[] array)
        {
            T[] newArr = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                newArr[i] = array[i];
            }
            return newArr;
        }

        T[][] Copy<T>(T[][] matrix)
        {
            T[][] newMatr = new T[matrix.Length][];
            for (int i = 0; i < matrix.Length; i++)
            {
                newMatr[i] = new T[matrix.First().Length];
                for (int j = 0; j < matrix.First().Length; j++)
                {
                    newMatr[i][j] = matrix[i][j];
                }
            }
            return newMatr;
        }
    }
}
