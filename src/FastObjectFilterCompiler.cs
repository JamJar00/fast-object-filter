using System.Linq;
using System;
using System.Diagnostics;
using System.Reflection;

namespace FastObjectFilter
{
    public class FastObjectFilterCompiler
    {
        // TODO removed IgnoreCase from here, should reflect in fast-string-format
        private const BindingFlags BINDING_FLAGS = BindingFlags.Instance | BindingFlags.Public;

        public Func<T, bool> Compile<T>(string expression)
        {
            Token[] tokens = new Tokenizer(expression).Tokenize();

            //Console.WriteLine("Parsed out [" + string.Join(", ", tokens.Select(t => t.TokenType + (string.IsNullOrEmpty(t.Value) ? "" : "('" + t.Value + "')"))) + "]");

            return new Parser<T>(tokens, BINDING_FLAGS).Compile();
        }
    }
}
