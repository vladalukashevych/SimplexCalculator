using Fractions;

namespace SimplexCalculator
{
    public class Problem
    {
        public Fraction[][] ConstraintMatrix { get; private set; }
        public string[] Signs { get; private set; }
        public Fraction[] FreeVariables { get; private set; }
        public Fraction[] FunctionVariables { get; private set; }
        public Fraction C { get; private set; }
        public bool IsExtrMax { get; private set; }

        public Problem(Fraction[][] constraintMatrix, string[] signs, Fraction[] freeVariables, Fraction[] functionVariables, Fraction c, bool isExtrMax)
        {
            this.ConstraintMatrix = constraintMatrix;
            this.Signs = signs;
            this.FreeVariables = freeVariables;
            this.FunctionVariables = functionVariables;
            this.C = c;
            this.IsExtrMax = isExtrMax;
        }
    }
}
