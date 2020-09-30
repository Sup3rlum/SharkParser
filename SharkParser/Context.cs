using System;
using System.Collections.Generic;
using System.Text;

namespace SharkParser
{
    public class Context
    {

        public Dictionary<string, ExpressionElement> definitions;


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
        public void AddDefaults()
        {
            this.AddDefinition("pi=3.1415926535");
        }
        public void AddContext(ref Context c)
        {
            foreach (KeyValuePair<string, ExpressionElement> f in c.definitions)
            {
                definitions.Add(f.Key, f.Value);
            }
        }
    }
}
