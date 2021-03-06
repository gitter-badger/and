﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Parser.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And.Parser
{
    public class Parser {
        public readonly Stream Stream;
        public int Position;

        public Parser(Stream stream) {
            Stream = stream;
        }
    }
}