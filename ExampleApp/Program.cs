using System;


using SharkParser;


namespace ExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            // Initialize a new Context object, where we store contextual information such as variable names, values, function definitions, operation definitions and aliases.
            Context c = new Context();

            // To add a definition to the context simply input a string expression using generic mathematical expressions
            c.AddDefinition("a=7.1");
            c.AddDefinition("b=4.669");

            // Next we create an expression object which encapsulates the internal tree structure generated from the input expression given by the specified grammar. If none selected - regular mathematical grammar will be used.
            Expression x = new Expression("(2*a+b)^3");
            Expression y = new Expression("(a*b+1)^2");


            // Evaluate our expression using the context our context and store the result. The expression is internally parsed into a tree as soon at is created thus making it immutable and completely functional. It can be applied to any set of inputs using any context, given that the context provides the necessary definitions.
            double first_result = x.Evaluate(c);
            double second_result = y.Evaluate(c);


            // Output our two result
            Console.WriteLine($"The first result is: {first_result}");
            Console.WriteLine($"The second result is: {second_result}");

        }
    }
}
