using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SharkParser
{
    public class Function : ExpressionElement
    {
        ExpressionElement argument;
        string Name;
        Func<double, double> operation;

        public Function(ExpressionElement input, Func<double, double> op)
        {
            argument = input;
            operation = op;
        }
        public override double Evaluate(ref Context c)
        {
            return operation(argument.Evaluate(ref c));
        }
    }
}
