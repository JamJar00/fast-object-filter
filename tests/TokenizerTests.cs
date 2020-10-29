using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FastObjectFilter.Tests
{
    [TestClass]
    public class TokenizerTests
    {
        [TestMethod]
        public void TestTokenizeNumber()
        {
            // GIVEN a filter string
            string filter = "789";

            // WHEN the filter string is tokenized
            Token[] result = new Tokenizer(filter).Tokenize();

            // THEN the tokens are correct
            Assert.AreEqual(1, result.Length);
            AssertToken(TokenType.Number, "789", 0, result[0]);
        }

        [TestMethod]
        public void TestTokenizeIdentifier()
        {
            // GIVEN a filter string
            string filter = "asdf";

            // WHEN the filter string is tokenized
            Token[] result = new Tokenizer(filter).Tokenize();

            // THEN the tokens are correct
            Assert.AreEqual(1, result.Length);
            AssertToken(TokenType.Identifier, "asdf", 0, result[0]);
        }

        [TestMethod]
        public void TestTokenizeTrue()
        {
            // GIVEN a filter string
            string filter = "true";

            // WHEN the filter string is tokenized
            Token[] result = new Tokenizer(filter).Tokenize();

            // THEN the tokens are correct
            Assert.AreEqual(1, result.Length);
            AssertToken(TokenType.Bool, "true", 0, result[0]);
        }

        [TestMethod]
        public void TestTokenizeFalse()
        {
            // GIVEN a filter string
            string filter = "false";

            // WHEN the filter string is tokenized
            Token[] result = new Tokenizer(filter).Tokenize();

            // THEN the tokens are correct
            Assert.AreEqual(1, result.Length);
            AssertToken(TokenType.Bool, "false", 0, result[0]);
        }

        [TestMethod]
        public void TestTokenizeNull()
        {
            // GIVEN a filter string
            string filter = "null";

            // WHEN the filter string is tokenized
            Token[] result = new Tokenizer(filter).Tokenize();

            // THEN the tokens are correct
            Assert.AreEqual(1, result.Length);
            AssertToken(TokenType.Null, null, 0, result[0]);
        }

        [TestMethod]
        public void TestTokenizeStringWithDoubleQuotes()
        {
            // GIVEN a filter string
            string filter = "\"'asdf\"";

            // WHEN the filter string is tokenized
            Token[] result = new Tokenizer(filter).Tokenize();

            // THEN the tokens are correct
            Assert.AreEqual(1, result.Length);
            AssertToken(TokenType.String, "'asdf", 0, result[0]);
        }

        [TestMethod]
        public void TestTokenizeStringWithSingleQuotes()
        {
            // GIVEN a filter string
            string filter = "'\"asdf'";

            // WHEN the filter string is tokenized
            Token[] result = new Tokenizer(filter).Tokenize();

            // THEN the tokens are correct
            Assert.AreEqual(1, result.Length);
            AssertToken(TokenType.String, "\"asdf", 0, result[0]);
        }

        [TestMethod]
        public void TestTokenizeEqual()
        {
            // GIVEN a filter string
            string filter = "==";

            // WHEN the filter string is tokenized
            Token[] result = new Tokenizer(filter).Tokenize();

            // THEN the tokens are correct
            Assert.AreEqual(1, result.Length);
            AssertToken(TokenType.Equal, null, 0, result[0]);
        }

        [TestMethod]
        public void TestTokenizeLessThanOrEquao()
        {
            // GIVEN a filter string
            string filter = "<=";

            // WHEN the filter string is tokenized
            Token[] result = new Tokenizer(filter).Tokenize();

            // THEN the tokens are correct
            Assert.AreEqual(1, result.Length);
            AssertToken(TokenType.LessThanOrEqual, null, 0, result[0]);
        }

        [TestMethod]
        public void TestTokenizeGreaterThanOrEqual()
        {
            // GIVEN a filter string
            string filter = ">=";

            // WHEN the filter string is tokenized
            Token[] result = new Tokenizer(filter).Tokenize();

            // THEN the tokens are correct
            Assert.AreEqual(1, result.Length);
            AssertToken(TokenType.GreaterThanOrEqual, null, 0, result[0]);
        }

        [TestMethod]
        public void TestTokenizeLessThan()
        {
            // GIVEN a filter string
            string filter = "<";

            // WHEN the filter string is tokenized
            Token[] result = new Tokenizer(filter).Tokenize();

            // THEN the tokens are correct
            Assert.AreEqual(1, result.Length);
            AssertToken(TokenType.LessThan, null, 0, result[0]);
        }

        [TestMethod]
        public void TestTokenizeGreaterThan()
        {
            // GIVEN a filter string
            string filter = ">";

            // WHEN the filter string is tokenized
            Token[] result = new Tokenizer(filter).Tokenize();

            // THEN the tokens are correct
            Assert.AreEqual(1, result.Length);
            AssertToken(TokenType.GreaterThan, null, 0, result[0]);
        }

        [TestMethod]
        public void TestTokenizeNotEqual()
        {
            // GIVEN a filter string
            string filter = "!=";

            // WHEN the filter string is tokenized
            Token[] result = new Tokenizer(filter).Tokenize();

            // THEN the tokens are correct
            Assert.AreEqual(1, result.Length);
            AssertToken(TokenType.NotEqual, null, 0, result[0]);
        }

        [TestMethod]
        public void TestTokenizeOr()
        {
            // GIVEN a filter string
            string filter = "||";

            // WHEN the filter string is tokenized
            Token[] result = new Tokenizer(filter).Tokenize();

            // THEN the tokens are correct
            Assert.AreEqual(1, result.Length);
            AssertToken(TokenType.Or, null, 0, result[0]);
        }

        [TestMethod]
        public void TestTokenizeAnd()
        {
            // GIVEN a filter string
            string filter = "&&";

            // WHEN the filter string is tokenized
            Token[] result = new Tokenizer(filter).Tokenize();

            // THEN the tokens are correct
            Assert.AreEqual(1, result.Length);
            AssertToken(TokenType.And, null, 0, result[0]);
        }

        [TestMethod]
        public void TestTokenizeWhitespace()
        {
            // GIVEN a filter string
            string filter = "   \t \n";

            // WHEN the filter string is tokenized
            Token[] result = new Tokenizer(filter).Tokenize();

            // THEN the tokens are correct
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void TestTokenizeDot()
        {
            // GIVEN a filter string
            string filter = ".";

            // WHEN the filter string is tokenized
            Token[] result = new Tokenizer(filter).Tokenize();

            // THEN the tokens are correct
            Assert.AreEqual(1, result.Length);
            AssertToken(TokenType.Dot, null, 0, result[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(FilterStringSyntaxException), "Unexpected token '}}}' at position 0")]
        public void TestTokenizeUnknownToken()
        {
            // GIVEN a filter string
            string filter = "}}}";

            // WHEN the filter string is tokenized
            new Tokenizer(filter).Tokenize();

            // THEN an exception is thrown
        }

        private static void AssertToken(TokenType expectedTokenType, string? expectedValue, int expectedPosition, Token actual)
        {
            Assert.AreEqual(expectedTokenType, actual.TokenType);
            Assert.AreEqual(expectedValue, actual.Value);
            Assert.AreEqual(expectedPosition, actual.Position);
        }
    }
}
