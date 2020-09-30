using System;
using System.Collections.Generic;
using System.Text;

namespace SharkParser
{
    public class Expression
    {
        ExpressionElement exprInternal;

        public Expression(string inputString)
        {
            var v = Tokenizer.Tokenize(inputString);



            exprInternal = Parser.ParseExpression(v);
        }
        public double Evaluate()
        {

            Context d = new Context();
            d.AddDefaults();

            return exprInternal.Evaluate(ref d);
        }
        public double Evaluate(Context context)
        {

            return exprInternal.Evaluate(ref context);
        }

        public ExpressionElement ReturnTree() => exprInternal;
    }
}
