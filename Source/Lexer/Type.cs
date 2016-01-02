// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Type.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And {
    public enum TokenType {
        // TODO: Support for Bitwise operators and more Assignment operators. 
        Comma,
        Number,
        String,
        Comment,
        Keyword,
        OpenParen,
        OpenBrace,
        Identifier,
        CloseParen,
        CloseBrace,
        CloseBracket,
        OpenBracket,
        Comparison,
        Undefined,
        SemiColon,
        Operator,
        Postfix,
        Colon,
        Float
    }
}