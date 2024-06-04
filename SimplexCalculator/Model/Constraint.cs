using Fractions;
using System;
namespace SimplexCalculator
{
    public class Constraint
    {
        public Fraction[] Variables { get; private set; }
        public Fraction B { get; private set; }
        public string Sign { get; private set; }

        public Constraint(Fraction[] variables, Fraction b, string sign)
        {
            if (sign == "=" || sign == "<=" || sign == ">=")
            {
                this.Variables = variables;
                this.B = b;
                this.Sign = sign;

            }
            else
            {
                throw new ArgumentException("Wrong sign");
            }
        }
    }
}
