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



            return exprInternal.Evaluate(ref Context.DefaultContext);
        }
        public double Evaluate(Context context)
        {

            context.AddContext(ref Context.DefaultContext);

            return exprInternal.Evaluate(ref context);
        }

        public ExpressionElement ReturnTree() => exprInternal;
    }
}
