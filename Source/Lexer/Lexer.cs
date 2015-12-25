// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Lexer.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And {
    using System;
    using System.Collections.Generic;

    public class Lexer
    {
        private readonly string _sourceCode;
        private int _position;

        public Lexer(string sourceCode) {
            _sourceCode = sourceCode;
        }
        
        // TODO: Convert If-else to Switch statement.
        public LinkedList<Token> Tokenize()
        {
            var tokens = new LinkedList<Token>();
            
            SkipWhitespace();

            while (CanAdvance())
            {
                // --> Number
                if (char.IsDigit((char)Peek()))
                    tokens.AddLast(ScanNumber());

                // --> String
                else if ((char)(Peek()) == '\"')
                    tokens.AddLast(ScanString());

                // -->   #
                else if ((char)Peek() == '#')
                    tokens.AddLast(new Token(TokenType.Comment, ((char)Read()).ToString()));

                // -->   ;
                else if ((char)Peek() == ';')
                    tokens.AddLast(new Token(TokenType.SemiColon, ((char)Read()).ToString()));

                // -->   :
                else if ((char)Peek() == ':')
                    tokens.AddLast(new Token(TokenType.Colon, ((char)Read()).ToString()));

                // -->   ,
                else if ((char)Peek() == ',')
                    tokens.AddLast(new Token(TokenType.Comma, ((char)Read()).ToString()));

                // -->   /
                else if ((char)Peek() == '/')
                    tokens.AddLast(new Token(TokenType.Operator, ((char)Read()).ToString()));

                // -->   *
                else if ((char)Peek() == '*')
                    tokens.AddLast(new Token(TokenType.Operator, ((char)Read()).ToString()));

                // -->   %
                else if ((char)Peek() == '%')
                    tokens.AddLast(new Token(TokenType.Modulus, ((char)Read()).ToString()));

                // -->   ?
                else if ((char)Peek() == '?')
                    tokens.AddLast(new Token(TokenType.Operator, ((char)Read()).ToString()));

                // --> &&
                else if ((char)Peek() == '&' && (char)Peek(1) == '&') {
                    tokens.AddLast(new Token(TokenType.Comparison, (char)Read() + ((char)Read()).ToString()));
                }

                // --> ||
                else if ((char)Peek() == '|' && (char)Peek(1) == '|') {
                    tokens.AddLast(new Token(TokenType.Comparison, (char)Read() + ((char)Read()).ToString()));
                }

                // -->   (, )
                else if ((char) Peek() == '(' || (char) Peek() == ')') {
                    TokenType type = Peek() == '(' ? TokenType.OpenParen : TokenType.CloseParen;
                    tokens.AddLast(new Token(type, ((char)Read()).ToString()));
                }

                // -->   [, ]
                else if ((char)Peek() == '[' || (char)Peek() == ']') {
                    TokenType type = Peek() == '[' ? TokenType.OpenBracket : TokenType.CloseBracket;
                    tokens.AddLast(new Token(type, ((char)Read()).ToString()));
                }

                // -->   {, }
                else if ((char)Peek() == '{' || (char)Peek() == '}') {
                    TokenType type = Peek() == '{' ? TokenType.OpenBrace : TokenType.CloseBrace;
                    tokens.AddLast(new Token(type, ((char)Read()).ToString()));
                }

                // -->   =, ==
                else if ((char)Peek() == '=')
                    tokens.AddLast((char) Peek(1) == '='
                        ? new Token(TokenType.Comparison, (char) Read() + ((char) Read()).ToString())
                        : new Token(TokenType.AssignOperator, ((char) Read()).ToString()));

                // -->   !, !=
                else if ((char)Peek() == '!')
                    tokens.AddLast((char)Peek(1) == '='
                        ? new Token(TokenType.Comparison, (char)Read() + ((char)Read()).ToString())
                        : new Token(TokenType.Not, ((char)Read()).ToString()));

                // -->   <, <=
                else if ((char)Peek() == '<')
                    tokens.AddLast((char)Peek(1) == '='
                        ? new Token(TokenType.Comparison, (char)Read() + ((char)Read()).ToString())
                        : new Token(TokenType.Comparison, ((char)Read()).ToString()));

                // -->   <, <=
                else if ((char)Peek() == '<')
                    tokens.AddLast((char)Peek(1) == '='
                        ? new Token(TokenType.Comparison, (char)Read() + ((char)Read()).ToString())
                        : new Token(TokenType.Comparison, ((char)Read()).ToString()));

                // -->   >, >=
                else if ((char)Peek() == '>')
                    tokens.AddLast((char)Peek(1) == '='
                        ? new Token(TokenType.Comparison, (char)Read() + ((char)Read()).ToString())
                        : new Token(TokenType.Comparison, ((char)Read()).ToString()));

                // -->   +, ++
                else if ((char)Peek() == '+')
                    tokens.AddLast((char)Peek(1) == '+'
                        ? new Token(TokenType.Postfix, (char)Read() + ((char)Read()).ToString())
                        : new Token(TokenType.Operator, ((char)Read()).ToString()));

                // -->   -, --
                else if ((char)Peek() == '-')
                    tokens.AddLast((char)Peek(1) == '-'
                        ? new Token(TokenType.Postfix, (char)Read() + ((char)Read()).ToString())
                        : new Token(TokenType.Operator, ((char)Read()).ToString()));

               else {
                    Console.WriteLine($"Unexpected {(char)Peek()} encountered.");
                    Read();
                }

                SkipWhitespace();
            }

            return tokens;
        }

        // TODO: Single and multi line comments
        private Token SingleLineComment() => null;
        private Token MultilineComment() => null;

        private Token ScanNumber()
        {
            string temp = string.Empty;
            while (CanAdvance() && char.IsDigit((char) Peek()))
                temp += ((char) Read()).ToString();

            return new Token(TokenType.Number, temp);
        }

        // TODO: Catch escape characters.
        private Token ScanString() {
            Read();
            string temp = string.Empty;

            while (CanAdvance() && Peek() != '\"')
                temp += ((char)Read()).ToString();

            Read();
            return new Token(TokenType.String, temp);
        }

        private void SkipWhitespace() {
            while (CanAdvance() && char.IsWhiteSpace((char)Peek()))
                Read();
        }

        private int Read() 
            => _position < _sourceCode.Length ? _sourceCode[_position++] : -1;

        private int Peek() 
            => _position < _sourceCode.Length ? _sourceCode[_position] : -1;

        private int Peek(int iter) 
            => _position + iter < _sourceCode.Length ? _sourceCode[_position + iter] : -1;

        private bool CanAdvance(int count = 1) 
            => _position + count < _sourceCode.Length;
    }
}