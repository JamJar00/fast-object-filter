using System;

namespace FastObjectFilter
{
    internal class Token
    {
        public TokenType TokenType { get; set; }
        public string? Value { get; set; }

        public int Position { get; }

        internal Token(TokenType tokenType, string? value, int position)
        {
            this.TokenType = tokenType;
            this.Value = value;
            this.Position = position;
        }
    }
}
