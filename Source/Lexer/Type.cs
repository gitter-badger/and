// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Type.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And {
    public enum TokenType {
        // TODO: Support for Bitwise operators and more Assignment operators. 
        Not,
        Comma,
        String,
        Number,
        Comment,
        OpenParen,
        OpenBrace,
        Identifier,
        CloseParen,
        CloseBrace,
        AssignOperator,
        CloseBracket,
        OpenBracket,
        Comparison,
        Undefined,
        SemiColon,
        Operator,
        Postfix,
        Modulus,
        Colon,
    }
}