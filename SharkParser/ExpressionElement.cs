using System;
using System.Collections.Generic;
using System.Text;

namespace SharkParser
{ 
    public abstract class ExpressionElement
    {
        public abstract double Evaluate(ref Context c);
    }
}
