﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Lexer.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Text;

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
        
        public LinkedList<Token> Tokenize()
        {
            var tokens = new LinkedList<Token>();
            
            SkipWhitespace();

            while (CanAdvance())
            {
                // --> Number
                if (char.IsDigit((char)Peek()))
                    tokens.AddLast(ScanNumber());

                // --> Identifier
                else if (char.IsLetter((char) Peek()))
                    tokens.AddLast(ScanIdentifier());

                // --> String
                else if ((char)Peek() == '\"')
                    tokens.AddLast(ScanString());

                // -->   #
                else if ((char)Peek() == '#')
                    tokens.AddLast(SingleLineComment());

                // -->   /*
                else if ((char)Peek() == '/' && ((char)Peek(1) == '*'))
                    tokens.AddLast(MultiLineComment());

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
                else if ((char)Peek() == '&' && (char)Peek(1) == '&')
                    tokens.AddLast(new Token(TokenType.Comparison, (char)Read() + ((char)Read()).ToString()));

                // --> ||
                else if ((char)Peek() == '|' && (char)Peek(1) == '|')
                    tokens.AddLast(new Token(TokenType.Comparison, (char)Read() + ((char)Read()).ToString()));

                // -->   (, )
                else if ((char)Peek() == '(' || (char)Peek() == ')') {
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
                    tokens.AddLast((char)Peek(1) == '='
                        ? new Token(TokenType.Comparison, (char)Read() + ((char)Read()).ToString())
                        : new Token(TokenType.AssignOperator, ((char)Read()).ToString()));

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
                    Console.WriteLine($"Unexpected token: '{(char)Peek()}'");
                    Read();
                }
                SkipWhitespace();
            }

            return tokens;
        }

        private Token ScanIdentifier()
        {
            string temp = string.Empty;
            while(_position < _sourceCode.Length 
                  && (char.IsLetterOrDigit((char)Peek())
                  || "_".Contains(((char)Peek()).ToString())))

                temp += (char)Read();

            return new Token(TokenType.Identifier, temp);
        }
        
        private Token SingleLineComment()
        {
            Read();
            string temp = string.Empty;
            while (Peek() != '\n' && _position < _sourceCode.Length) {
                temp += ((char)Read()).ToString();
            }

            Read();
            return new Token(TokenType.Comment, temp);
        }

        private Token MultiLineComment()
        {
            Read(2);
            string temp = string.Empty;
            while (Peek() != '*' && Peek(1) != '/' && _position < _sourceCode.Length)
                    temp += ((char)Read()).ToString();
            
            Read(2);
            return new Token(TokenType.Comment, temp);
        }

        private Token ScanNumber()
        {
            string temp = string.Empty;
            while (CanAdvance() && char.IsDigit((char) Peek()))
                temp += ((char) Read()).ToString();

            return new Token(TokenType.Number, temp);
        }
        
        private Token ScanString() {
            int begin = Read();
            var temp = new StringBuilder();

            while (CanAdvance() && Peek() != begin)
            {
                switch (Peek())
                {
                    case '\\':
                        if (begin == '"')
                            switch (Peek(1))
                            {
                                case 'n':   // New line
                                    temp.Append("\n");
                                    Read(2);
                                    break;
                                case 't':   // Horizontal tab
                                    temp.Append("\t");
                                    Read(2);
                                    break;
                                case 'r':   // Carriage return
                                    temp.Append("\r");
                                    Read(2);
                                    break;
                                case 'a':   // Bell (alert)
                                    temp.Append("\a");
                                    Read(2);
                                    break;
                                case 'b':   // Backspace
                                    temp.Append("\b");
                                    Read(2);
                                    break;
                                default:
                                    Console.WriteLine(@"Unexpected escape sequence: \" + (char)Peek(1));
                                    break;
                            }
                        else
                            temp.Append((char)Read(2));
                        break;
                }
                temp.Append(((char)Read()).ToString());
            }

            Read();
            return new Token(TokenType.String, temp.ToString());
        }

        private void SkipWhitespace() {
            while (CanAdvance() && char.IsWhiteSpace((char)Peek()))
                Read();
        }

        private int Read()
            => _position < _sourceCode.Length
               ? _sourceCode[_position++]
               : '\0'; // if not, return null char instead of -1

        private int Read(byte iter = 1)
            => _position < _sourceCode.Length
               ? _sourceCode[_position += iter]
               : '\0';

        private int Peek(byte iter = 0) 
            => _position + iter < _sourceCode.Length 
               ? _sourceCode[_position + iter]
               : '\0';

        private bool CanAdvance(byte count = 1) 
            => _position + count < _sourceCode.Length;
    }
}