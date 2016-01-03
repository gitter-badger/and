// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Token.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And.Lexer {
    public class Token
    {
        public TokenType Type { get; }
        public string Value { get; }

        public Token(TokenType type, string value) {
            Type = type;
            Value = value;
        }

        public override string ToString() => Value;
    }
}