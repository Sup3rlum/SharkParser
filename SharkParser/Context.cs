using System;
using System.Collections.Generic;
using System.Text;

namespace SharkParser
{
    public class Context
    {

        private Dictionary<string, ExpressionElement> definitions;

        static Context()
        {
            DefaultContext = new Context();

            DefaultContext.AddDefinition("pi=3.1415926535");
        }

        public Context()
        {
            definitions = new Dictionary<string, ExpressionElement>();
        }
        public void AddDefinition(string s)
        {
            var v = Tokenizer.Tokenize(s);

            List<Token> lhs = new List<Token>();
            List<Token> rhs = new List<Token>();

            bool hasEq = false;

            for (int i = 0; i < v.Count; i++)

            {
                if (v[i].Type == TokenType.Eq)
                {

                    if (hasEq)
                        throw new ArgumentException();

                    hasEq = true;


                    lhs = v.GetRange(0, i);
                    rhs = v.GetRange(i + 1, v.Count - i - 1);
                     
                }
            }

            if (lhs.Count != 1)
                throw new NotImplementedException();

            if (lhs[0].Type != TokenType.Variable)
                throw new NotImplementedException();


            definitions.Add(lhs[0].Value, Parser.ParseExpression(rhs));

        }

        public void AddContext(ref Context c)
        {
            foreach (KeyValuePair<string, ExpressionElement> f in c.definitions)
            {
                definitions.Add(f.Key, f.Value);
            }
        }

        public static Context DefaultContext;
    }
}
