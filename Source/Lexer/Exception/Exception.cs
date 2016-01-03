// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Exception.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And.Lexer.Exception
{
    public class UnexpectedTokenException : System.Exception {
        public UnexpectedTokenException(string message)
            : base(message)
        {}
    }

    public class UnexpectedEscapeSequenceException : System.Exception {
        public UnexpectedEscapeSequenceException(string message)
            : base (message)
        {}
    }
}