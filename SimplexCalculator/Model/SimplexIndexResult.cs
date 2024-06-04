using System;

namespace SimplexCalculator
{
    public class SimplexIndexResult
    {
        public Tuple<int, int> Index { get; private set; }
        public SimplexResult Result { get; private set; }

        public SimplexIndexResult(Tuple<int, int> index, SimplexResult result)
        {
            this.Index = index;
            this.Result = result;
        }
    }
}
