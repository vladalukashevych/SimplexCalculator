using Fractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplexCalculator
{
    public enum SimplexResult { Unbounded, Found, NotYetFound }

    public class SimplexCalculator
    {

        private Function Function;
        public Fraction[] FunctionVariables { get; private set; }
        public Fraction[][] Matrix { get; private set; }
        public Fraction[] B { get; private set; }
        public Fraction[] M { get; private set; }
        public Fraction[] F { get; private set; }
        public int[] C { get; private set; }
        public bool[] IsArtificial { get; private set; }
        public bool IsArtificialDone { get; private set; } = false;
        private int round;

        public SimplexCalculator(Function function, Constraint[] constraints)
        {
            if (function.IsExtrMax)
            {
                this.Function = function;
            }
            else
            {
                this.Function = CanonizeFunction(function);
            }

            GetMatrix(constraints);
            GetFunctionArray();
            GetMandFValues();

            for (int i = 0; i < F.Length; i++)
            {
                F[i] = -FunctionVariables[i];
            }

        }

        public Tuple<List<SimplexSnap>, SimplexResult> GetResult()
        {
            List<SimplexSnap> snaps = new List<SimplexSnap>();
            snaps.Add(new SimplexSnap(B, Matrix, M, F, C, FunctionVariables, IsArtificialDone, IsArtificial));

            SimplexIndexResult result = nextStep();
            round = 1;
            while (result.Result == SimplexResult.NotYetFound && round <= 45)
            {
                CalculateSimplexTable(result.Index);
                snaps.Add(new SimplexSnap(B, Matrix, M, F, C, FunctionVariables, IsArtificialDone, IsArtificial));
                result = nextStep();
                round++;
            }

            return new Tuple<List<SimplexSnap>, SimplexResult>(snaps, result.Result);
        }

        private void CalculateSimplexTable(Tuple<int, int> Xij)
        {
            Fraction[][] newMatrix = new Fraction[Matrix.Length][];

            C[Xij.Item2] = Xij.Item1;

            Fraction[] newJRow = new Fraction[Matrix.Length];

            for (int i = 0; i < Matrix.Length; i++)
            {
                newJRow[i] = Matrix[i][Xij.Item2] / Matrix[Xij.Item1][Xij.Item2];
            }

            Fraction[] newB = new Fraction[B.Length];

            for (int i = 0; i < B.Length; i++)
            {
                if (i == Xij.Item2)
                {
                    newB[i] = B[i] / Matrix[Xij.Item1][Xij.Item2];
                }
                else
                {
                    newB[i] = B[i] - B[Xij.Item2] / Matrix[Xij.Item1][Xij.Item2] * Matrix[Xij.Item1][i];
                }
            }

            B = newB;

            for (int i = 0; i < Matrix.Length; i++)
            {
                newMatrix[i] = new Fraction[C.Length];
                for (int j = 0; j < C.Length; j++)
                {
                    if (j == Xij.Item2)
                    {
                        newMatrix[i][j] = newJRow[i];
                    }
                    else
                    {
                        newMatrix[i][j] = Matrix[i][j] - newJRow[i] * Matrix[Xij.Item1][j];
                    }
                }
            }

            Matrix = newMatrix;
            GetMandFValues();
        }

        private void GetMandFValues()
        {
            M = new Fraction[Matrix.Length];
            F = new Fraction[Matrix.Length];

            for (int i = 0; i < Matrix.Length; i++)
            {
                Fraction sumF = 0;
                Fraction sumM = 0;
                for (int j = 0; j < Matrix.First().Length; j++)
                {
                    if (IsArtificial[C[j]])
                    {
                        sumM -= Matrix[i][j];
                    }
                    else
                    {
                        sumF += FunctionVariables[C[j]] * Matrix[i][j];
                    }
                }
                M[i] = IsArtificial[i] ? sumM + 1 : sumM;
                F[i] = sumF - FunctionVariables[i];
            }
        }

        private SimplexIndexResult nextStep()
        {

            int columnM = GetIndexOfMaxNegative(M);

            if (IsArtificialDone || columnM == -1)
            {
                //M doesn't have negative values
                IsArtificialDone = true;
                int columnF = GetIndexOfMaxNegative(F);

                if (columnF != -1) //Has at least 1 negative value
                {
                    int row = GetIndexOfMinimalRatio(Matrix[columnF], B);

                    if (row != -1)
                    {
                        return new SimplexIndexResult(new Tuple<int, int>(columnF, row), SimplexResult.NotYetFound);
                    }
                    else
                    {
                        return new SimplexIndexResult(null, SimplexResult.Unbounded);
                    }
                }
                else
                {
                    return new SimplexIndexResult(null, SimplexResult.Found);
                }

            }
            else
            {
                int row = GetIndexOfMinimalRatio(Matrix[columnM], B);

                if (row != -1)
                {
                    return new SimplexIndexResult(new Tuple<int, int>(columnM, row), SimplexResult.NotYetFound);
                }
                else
                {
                    return new SimplexIndexResult(null, SimplexResult.Unbounded);
                }
            }
        }

        private int GetIndexOfMaxNegative(Fraction[] array)
        {
            int index = -1;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < 0)
                {
                    if (!IsArtificialDone || IsArtificialDone && !IsArtificial[i])
                    {
                        if (index == -1)
                        {
                            index = i;
                        }
                        else if (Math.Abs(array[i].ToDouble()) > Math.Abs(array[index].ToDouble()))
                        {
                            index = i;
                        }
                    }

                }
            }
            return index;
        }

        int GetIndexOfMinimalRatio(Fraction[] column, Fraction[] b)
        {
            int index = -1;

            for (int i = 0; i < column.Length; i++)
            {
                if (column[i] > 0 && b[i] > 0 || column[i] < 0 && b[i] < 0 || round == 0)
                {
                    if (index == -1)
                    {
                        index = i;
                    }
                    else if (b[i] / column[i] < b[index] / column[index])
                    {
                        index = i;
                    }
                }
            }

            return index;
        }

        public void GetFunctionArray()
        {
            Fraction[] funcVars = new Fraction[Matrix.Length];
            for (int i = 0; i < Matrix.Length; i++)
            {
                funcVars[i] = i < Function.Variables.Length ? Function.Variables[i] : 0;
            }
            this.FunctionVariables = funcVars;
        }

        public Function CanonizeFunction(Function function)
        {
            Fraction[] newFuncVars = new Fraction[function.Variables.Length];

            for (int i = 0; i < function.Variables.Length; i++)
            {
                newFuncVars[i] = -function.Variables[i];
            }
            return new Function(newFuncVars, -function.C, true);
        }

        private Fraction[][] AppendColumn(Fraction[][] matrix, Fraction[] column)
        {
            Fraction[][] newMatrix = new Fraction[matrix.Length + 1][];
            for (int i = 0; i < matrix.Length; i++)
            {
                newMatrix[i] = matrix[i];
            }
            newMatrix[matrix.Length] = column;
            return newMatrix;
        }

        T[] append<T>(T[] array, T element)
        {
            T[] newArray = new T[array.Length + 1];
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }
            newArray[array.Length] = element;
            return newArray;
        }

        private Fraction[] GetColumn(Fraction value, int place, int length)
        {
            Fraction[] newColumn = new Fraction[length];

            for (int k = 0; k < length; k++)
            {
                newColumn[k] = k == place ? value : 0;
            }

            return newColumn;
        }

        public void GetMatrix(Constraint[] constraints)
        {
            for (int i = 0; i < constraints.Length; i++)
            {
                if (constraints[i].B < 0)
                {
                    Fraction[] cVars = new Fraction[constraints[i].Variables.Length];

                    for (int j = 0; j < constraints[i].Variables.Length; j++)
                    {
                        cVars[j] = -constraints[i].Variables[j];
                    }

                    string sign = constraints[i].Sign;

                    if (sign == ">=")
                    {
                        sign = "<=";
                    }
                    else if (sign == "<=")
                    {
                        sign = ">=";
                    }

                    Constraint cNew = new Constraint(cVars, -constraints[i].B, sign);
                    constraints[i] = cNew;
                }
            }

            Fraction[][] matrix = new Fraction[constraints.First().Variables.Length][];

            for (int i = 0; i < constraints.First().Variables.Length; i++)
            {
                matrix[i] = new Fraction[constraints.Length];
                for (int j = 0; j < constraints.Length; j++)
                {
                    matrix[i][j] = constraints[j].Variables[i];
                }
            }

            Fraction[][] appendixMatrix = new Fraction[0][];
            Fraction[] Bs = new Fraction[constraints.Length];

            for (int i = 0; i < constraints.Length; i++)
            {
                Constraint current = constraints[i];

                Bs[i] = current.B;

                if (current.Sign == ">=")
                {
                    appendixMatrix = AppendColumn(appendixMatrix, GetColumn(-1, i, constraints.Length));
                }
                else if (current.Sign == "<=")
                {
                    appendixMatrix = AppendColumn(appendixMatrix, GetColumn(1, i, constraints.Length));
                }
            }

            Fraction[][] newMatrix = new Fraction[constraints.First().Variables.Length + appendixMatrix.Length][];

            for (int i = 0; i < constraints.First().Variables.Length; i++)
            {
                newMatrix[i] = matrix[i];
            }

            for (int i = constraints.First().Variables.Length; i < constraints.First().Variables.Length + appendixMatrix.Length; i++)
            {
                newMatrix[i] = appendixMatrix[i - constraints.First().Variables.Length];
            }

            bool[] hasBasicVar = new bool[constraints.Length];

            for (int i = 0; i < constraints.Length; i++)
            {
                hasBasicVar[i] = false;
            }

            C = new int[constraints.Length];

            int ci = 0;
            for (int i = 0; i < newMatrix.Length; i++)
            {


                bool hasOnlyNulls = true;
                bool hasOne = false;
                Tuple<int, int> onePosition = new Tuple<int, int>(0, 0);
                for (int j = 0; j < constraints.Length; j++)
                {


                    if (newMatrix[i][j] == 1)
                    {
                        if (hasOne)
                        {
                            hasOnlyNulls = false;
                            break;
                        }
                        else
                        {
                            hasOne = true;
                            onePosition = new Tuple<int, int>(i, j);
                        }
                    }
                    else if (newMatrix[i][j] != 0)
                    {
                        hasOnlyNulls = false;
                        break;
                    }


                }

                if (hasOnlyNulls && hasOne)
                {
                    hasBasicVar[onePosition.Item2] = true;
                    C[ci] = onePosition.Item1;
                    ci++;
                }

            }

            IsArtificial = new bool[newMatrix.Length];

            for (int i = 0; i < newMatrix.Length; i++)
            {
                IsArtificial[i] = false;
            }

            for (int i = 0; i < constraints.Length; i++)
            {

                if (!hasBasicVar[i])
                {

                    Fraction[] basicColumn = new Fraction[constraints.Length];

                    for (int j = 0; j < constraints.Length; j++)
                    {
                        basicColumn[j] = j == i ? 1 : 0;
                    }

                    newMatrix = AppendColumn(newMatrix, basicColumn);
                    IsArtificial = append(IsArtificial, true);
                    C[ci] = newMatrix.Length - 1;
                    ci++;
                }

            }

            this.B = Bs;
            this.Matrix = newMatrix;
        }
    }
}
