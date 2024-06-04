using Fractions;

namespace SimplexCalculator
{
    public class Function
    {
        public Fraction[] Variables { get; private set; }
        public Fraction C { get; private set; }
        public bool IsExtrMax { get; private set; }

        public Function(Fraction[] variables, Fraction c, bool isExtrMax)
        {
            this.Variables = variables;
            this.C = c;
            this.IsExtrMax = isExtrMax;
        }
    }
}
