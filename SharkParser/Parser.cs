using System;
using System.Collections.Generic;
using System.Text;

namespace SharkParser
{
    public class Parser
    {
        public static bool VerifyScopes(List<Token> tokenCollection)
        {
            int lCount = 0;
            int rCount = 0;

            for (int i = 0; i < tokenCollection.Count; i++)
            {
                if (tokenCollection[i].Type == TokenType.OpenBracket)
                    lCount++;

                else if (tokenCollection[i].Type == TokenType.CloseBracket)
                {
                    if (lCount > rCount)
                        rCount++;
                    else
                        return false;
                }
            }

            return (rCount == lCount);
        }

        public static ExpressionElement ParseExpression(List<Token> input)
        {

            if (!VerifyScopes(input))
                throw new ArgumentException($"'{nameof(input)}' is not a valid expression token stream");

            List<Token> v = InfixToRPN(input);


            Stack<ExpressionElement> s = new Stack<ExpressionElement>();

            for (int i = 0; i < v.Count; i++)
            {
                if (v[i].Type == TokenType.Constant)
                {
                    s.Push(new Constant(Double.Parse(v[i].Value)));
                }
                if (v[i].Type == TokenType.Variable)
                {
                    s.Push(new Variable(v[i].Value));
                }
                if (v[i].Type == TokenType.Operator)
                {
                    var b = s.Pop();
                    var a = s.Pop();

                    s.Push(new BinaryOperation(a, b, v[i].Operation()));
                }
            }

            if (s.Count != 1)
                throw new Exception();

            return s.Pop();

        }
        private static List<Token> InfixToRPN(List<Token> input)
        {

            List<Token> output = new List<Token>();

            Stack<Token> opStack = new Stack<Token>();

            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].Type == TokenType.Constant || input[i].Type == TokenType.Variable)
                {
                    output.Add(input[i]);

                }

                if (input[i].Type == TokenType.Operator)
                {
                    while ((opStack.Count != 0) && ((input[i].Precedence() <= opStack.Peek().Precedence()) || (input[i].Precedence() == opStack.Peek().Precedence() && !input[i].RightAssociativity())) && opStack.Peek().Type != TokenType.OpenBracket)
                    {
                        output.Add(opStack.Pop());
                    }

                    opStack.Push(input[i]);
                }

                if (input[i].Type == TokenType.OpenBracket)
                {
                    opStack.Push(input[i]);
                }
                if (input[i].Type == TokenType.CloseBracket)
                {
                    while (opStack.Count != 0 && opStack.Peek().Type != TokenType.OpenBracket)
                    {
                        output.Add(opStack.Pop());
                    }
                    if (opStack.Count != 0 && opStack.Peek().Type == TokenType.OpenBracket)
                    {
                        opStack.Pop();
                    }
                }


            }
            while (opStack.Count != 0)
            {
                output.Add(opStack.Pop());
            }

            return output;
        }

    }
}
