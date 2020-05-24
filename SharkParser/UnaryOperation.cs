using System;
using System.Collections.Generic;
using System.Text;

namespace SharkParser
{
    public class UnaryOperation : ExpressionElement
    {

        ExpressionElement element;
        Func<double, double> operation;

        public UnaryOperation(ExpressionElement element, Func<double, double> operation)
        {
            this.element = element;
            this.operation = operation;
        }
        public override double Evaluate(ref Context c)
        {
            return operation(element.Evaluate(ref c));
        }
    }
}
