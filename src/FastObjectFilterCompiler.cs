using System;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FastObjectFilter.Test")]

namespace FastObjectFilter
{
    public class FastObjectFilterCompiler
    {
        private const BindingFlags BINDING_FLAGS = BindingFlags.Instance | BindingFlags.Public;

        public Func<T, bool> Compile<T>(string expression)
        {
            Token[] tokens = new Tokenizer(expression).Tokenize();

            //Console.WriteLine("Parsed out [" + string.Join(", ", tokens.Select(t => t.TokenType + (string.IsNullOrEmpty(t.Value) ? "" : "('" + t.Value + "')"))) + "]");

            return new Parser<T>(tokens, BINDING_FLAGS).Compile();
        }
    }
}
