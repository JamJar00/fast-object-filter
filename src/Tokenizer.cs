using System.Collections.Generic;

namespace FastObjectFilter
{
    internal class Tokenizer
    {
        private readonly string expression;

        private int ptr;

        private readonly List<Token> tokens = new List<Token>();

        public Tokenizer(string expression)
        {
            this.expression = expression;
        }

        public Token[] Tokenize()
        {
            while (ptr < expression.Length)
            {
                char current = expression[ptr];
                char? next = ptr + 1 < expression.Length ? (char?)expression[ptr + 1] : null;

                if (char.IsDigit(current))
                {
                    ParseNumber();
                }
                else if (char.IsLetter(current))
                {
                    ParseIdentifier();
                }
                else if (current == '"' || current == '\'')
                {
                    ParseString();
                }
                else if (current == '=' && next.HasValue && next.Value == '=')
                {
                    tokens.Add(new Token(TokenType.Equal, null, ptr));
                    ptr += 2;
                }
                else if (current == '<' && next.HasValue && next.Value == '=')
                {
                    tokens.Add(new Token(TokenType.LessThanOrEqual, null, ptr));
                    ptr += 2;
                }
                else if (current == '>' && next.HasValue && next.Value == '=')
                {
                    tokens.Add(new Token(TokenType.GreaterThanOrEqual, null, ptr));
                    ptr += 2;
                }
                else if (current == '<')
                {
                    tokens.Add(new Token(TokenType.LessThan, null, ptr));
                    ptr++;
                }
                else if (current == '>')
                {
                    tokens.Add(new Token(TokenType.GreaterThan, null, ptr));
                    ptr++;
                }
                else if (current == '!' && next.HasValue && next.Value == '=')
                {
                    tokens.Add(new Token(TokenType.NotEqual, null, ptr));
                    ptr += 2;
                }
                else if (current == '|' && next.HasValue && next.Value == '|')
                {
                    tokens.Add(new Token(TokenType.Or, null, ptr));
                    ptr += 2;
                }
                else if (current == '&' && next.HasValue && next.Value == '&')
                {
                    tokens.Add(new Token(TokenType.And, null, ptr));
                    ptr += 2;
                }
                else if (char.IsWhiteSpace(current))
                {
                    ptr++;
                }
                else if (current == '.')
                {
                    tokens.Add(new Token(TokenType.Dot, null, ptr));
                    ptr++;
                }
                else
                {
                    throw new FilterStringSyntaxException($"Unexpected token '{current}' at position {ptr}");
                }
            }

            return tokens.ToArray();
        }

        private void ParseNumber()
        {
            string number = "";
            while (ptr < expression.Length && char.IsDigit(expression[ptr]))
                number += expression[ptr++];
            tokens.Add(new Token(TokenType.Number, number, ptr));
        }

        private void ParseIdentifier()
        {
            string identifier = "";
            while (ptr < expression.Length && char.IsLetterOrDigit(expression[ptr]))
                identifier += expression[ptr++];

            // Handle things that look like identifiers but are actually reserved words
            if (identifier == "true" || identifier == "false")
                tokens.Add(new Token(TokenType.Bool, identifier, ptr));
            else if (identifier == "null")
                tokens.Add(new Token(TokenType.Null, null, ptr));
            else
                tokens.Add(new Token(TokenType.Identifier, identifier, ptr));
        }

        private void ParseString()
        {
            char startChar = expression[ptr];
            ptr++;

            string str = "";
            while (ptr < expression.Length && expression[ptr] != startChar)
                str += expression[ptr++];
            tokens.Add(new Token(TokenType.String, str, ptr));

            ptr++;
        }
    }
}
