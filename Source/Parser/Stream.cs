// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParserStream.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using And.Lexer;

namespace And.Parser
{
    using System.Collections.Generic;

    public sealed class Stream
    {
        private readonly List<Token> _tokens;
        public int Position;

        public Stream(List<Token> tokens) {
            _tokens = tokens;
        }

        public Token Current()
            => Position >= _tokens.Count ? new Token(TokenType.Identifier, string.Empty) : _tokens[Position];

        public Token Previous(int dec = 1)
            => _tokens[Position - dec];

        public Token Next(int iter = 1)
            => _tokens[Position + iter];

        public bool EndOfStream => _tokens.Count <= Position;

        public bool Match(TokenType type)
            => Position < _tokens.Count && _tokens[Position].Type == type;

        public bool Match(TokenType type, string value)
            => Position < _tokens.Count && _tokens[Position].Type == type && _tokens[Position].Value == value;

        public Token Expect(TokenType type)
            => !Match(type) ? new Token(TokenType.Undefined, "Tokens didn't match!") : _tokens[Position++];

        public Token Expect(TokenType type, string value)
            => !Match(type, value) ? new Token(TokenType.Undefined, "Tokens didn't match!") : _tokens[Position++];

        public bool Accept(TokenType type) {
            if (!Match(type)) return false;
            Position++;
            return true;
        }

        public bool Accept(TokenType type, string value) {
            if (!Match(type, value)) return false;
            Position++;
            return true;
        }
    }
}
