using System;
using System.Collections.Generic;
using System.Text;

namespace SharkParser
{
    public class Constant : ExpressionElement
    {
        double Value;

        public Constant(double Value)
        {
            this.Value = Value;
        }

        public override double Evaluate(ref Context c)
        {
            return Value;
        }
    }
}
