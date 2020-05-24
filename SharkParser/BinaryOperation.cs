using System;
using System.Collections.Generic;
using System.Text;

namespace SharkParser
{
    public class BinaryOperation : ExpressionElement
    {
        ExpressionElement left, right;
        Func<double, double, double> operation;

        public BinaryOperation(ExpressionElement left, ExpressionElement right, Func<double, double, double> operation)
        {
            this.left = left;
            this.right = right;
            this.operation = operation;
        }
        public override double Evaluate(ref Context c)
        {
            return operation(left.Evaluate(ref c), right.Evaluate(ref c));
        }
    }
}
