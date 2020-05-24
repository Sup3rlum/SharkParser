using System;
using System.Collections.Generic;
using System.Text;

namespace SharkParser
{
    public class Variable : ExpressionElement
    {

        string Name;

        public Variable(string name)
        {
            Name = name;
        }

        public override double Evaluate(ref Context c)
        {
            if (!c.definitions.ContainsKey(Name))
                throw new Exception();

            return c.definitions[Name].Evaluate(ref c);
        }
    }
}
