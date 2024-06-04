using Fractions;
using System;

namespace SimplexCalculator
{
    public class CustomProblem
    {
        public static Problem GetCustomProblem()
        {
            string[][] constrVars = new string[4][];
            constrVars[0] = new string[] { "0.45", "0.10", "0.40", "0.35", "0.20" };
            constrVars[1] = new string[] { "0.40", "0.80", "0.30", "0.25", "0.70"};
            constrVars[2] = new string[] { "0.15", "0.10", "0.30", "0.40", "0.10" };
            constrVars[3] = new string[] { "1", "1", "1", "1", "1"};
            
            Fraction[][] constrMatrx = new Fraction[4][];
            int i = 0;
            foreach (var row in constrVars)
            {
                int j = 0;
                Fraction[] arr = new Fraction[5];
                foreach (var elem in row)
                {
                    arr[j] = Fraction.FromString(elem);
                    j++;
                }
                constrMatrx[i] = arr;
                i++;
            }

            string[] signs = { "<=", ">=", ">=", "=" };
            string[] freeVars = { "0.40", "0.20", "0", "1" };            
            Fraction[] freeVarsFract = Array.ConvertAll(freeVars, new Converter<string, Fraction>(Fraction.FromString));

            string[] funcVars = { "8", "17", "10", "12", "15" };
            Fraction[] funcVarsFract = Array.ConvertAll(funcVars, new Converter<string, Fraction>(Fraction.FromString));

            return new Problem(constrMatrx, signs, freeVarsFract, funcVarsFract, 0, false);
        }
    }
}
