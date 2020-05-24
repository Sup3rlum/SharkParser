using System;
using System.Collections.Generic;

using SharkParser;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            Context c = new Context();
            c.AddDefinition("a=1.1");
            c.AddDefinition("b=1.1");
            c.AddDefinition("d=1.1");
            c.AddDefinition("e=1.1");
            c.AddDefinition("r=1.1");
            c.AddDefinition("t=1.1");
            c.AddDefinition("n=1.1");

            Expression x = new Expression("((a + b) * 3 + (d-e) / r) * (t+n) / 3");

            var v = x.ReturnTree();

            Console.WriteLine(x.Evaluate(c));


        }
    }
}
