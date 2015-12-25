// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Parser.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And
{
    using System.Collections.Generic;

    public class Parser
    {
        private readonly LinkedList<Token> _tokens;
        public int Position;

        public Parser(LinkedList<Token> tokens) {
            _tokens = tokens;
        }
    }
}