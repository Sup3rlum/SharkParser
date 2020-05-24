using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SharkParser
{
    public enum TokenType
    {
        Variable,
        Function,
        Constant,
        Name,
        Operator,
        Eq,
        OpenBracket,
        CloseBracket,
        Comma,
        Unknown,
        EOF
    }
    public class Token
    {
        public TokenType Type;
        public string Value;

        public Token(TokenType t, string v)
        {
            Type = t;
            Value = v;
        }
        public Token(TokenType t) : this(t, "") { }

        public int Precedence() => Value switch
        {
            "^" => 4,
            "*" => 3,
            "/" => 3,
            "+" => 2,
            "-" => 2,
            _ => 0
        };

        public bool RightAssociativity() => Value switch
        {
            "^" => true,
            "*" => false,
            "/" => false,
            "+" => false,
            "-" => false,
            _ => false
        };

        public Func<double, double, double> Operation() => Value switch
        {
            "^" => (x, y) => Math.Pow(x, y),
            "*" => (x, y) => x * y,
            "/" => (x, y) => x / y,
            "+" => (x, y) => x + y,
            "-" => (x, y) => x - y,
            _ => (x, y) => 0
        };
    }
    public class Tokenizer
    {
        public static List<Token> Tokenize(string input)
        {

            List<Token> it = new List<Token>();

            for (int i = 0; i < input.Length; i++)
            {
                Token t = input[i] switch
                {
                    '*' => new Token(TokenType.Operator, "*"),
                    '+' => new Token(TokenType.Operator, "+"),
                    '-' => new Token(TokenType.Operator, "-"),
                    '/' => new Token(TokenType.Operator, "/"),
                    '(' => new Token(TokenType.OpenBracket, "("),
                    ')' => new Token(TokenType.CloseBracket, ")"),
                    '=' => new Token(TokenType.Eq, "="),
                    '^' => new Token(TokenType.Operator, "^"),
                    '%' => new Token(TokenType.Operator, "%"),
                    ',' => new Token(TokenType.Comma),
                    '\0' => new Token(TokenType.EOF),

                    _ => new Func<Token>(() => 
                    {
                        if (IsLetter(input[i]))
                        {

                            int e = 0;
                            string v = "";

                            do
                            {
                                v += input[i + e];
                                e++;
                            }
                            while (i + e < input.Length && IsLetter(input[i + e]));

                            i += e - 1;

                            if (i + 1 < input.Length && input[i + 1] == '(')
                            {
                                return new Token(TokenType.Function, v);
                            }

                            return new Token(TokenType.Variable, v);
                        }

                        if (IsDigit(input[i]))
                        {
                            int e = 0;
                            string v = "";

                            do
                            {
                                v += input[i + e];
                                e++;
                            }
                            while (i + e < input.Length && IsDigit(input[i + e]));

                            i += e - 1;

                            return new Token(TokenType.Constant, v);
                        }

                        return new Token(TokenType.Unknown, input[i].ToString());

                        
                    })(),
                };


                if (t.Type == TokenType.Unknown)
                {
                    
                    continue;
                }


                it.Add(t);
            }

            return it;
        }

        private static bool IsLetter(char c) => ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') );
        private static bool IsDigit(char c) => ((c >= '0' && c <= '9') || c == '.');

    }
}
