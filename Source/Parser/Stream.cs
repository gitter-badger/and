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
        private int _position;

        public Stream(List<Token> tokens) {
            _tokens = tokens;
        }

        public Token Current()
            => _position >= _tokens.Count ? new Token(TokenType.Identifier, string.Empty) : _tokens[_position];

        public Token Previous(int dec = 1)
            => _tokens[_position - dec];

        public Token Next(int iter = 1)
            => _tokens[_position + iter];

        public bool EndOfStream => _tokens.Count <= _position;

        public bool Match(TokenType type)
            => _position < _tokens.Count && _tokens[_position].Type == type;

        public bool Match(TokenType type, string value)
            => _position < _tokens.Count && _tokens[_position].Type == type && _tokens[_position].Value == value;

        public Token Expect(TokenType type)
            => !Match(type) ? new Token(TokenType.Undefined, "Tokens didn't match!") : _tokens[_position++];

        public Token Expect(TokenType type, string value)
            => !Match(type, value) ? new Token(TokenType.Undefined, "Tokens didn't match!") : _tokens[_position++];

        public bool Accept(TokenType type) {
            if (!Match(type)) return false;
            _position++;
            return true;
        }

        public bool Accept(TokenType type, string value) {
            if (!Match(type, value)) return false;
            _position++;
            return true;
        }
    }
}
