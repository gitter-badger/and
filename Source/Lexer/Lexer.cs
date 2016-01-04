// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Lexer.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using And.Lexer.Exception;

namespace And.Lexer {
    using System.Text;
    using System.Collections.Generic;

    public class LexicalAnalyser
    {
        private readonly string _sourceCode;
        private int _position;

        public LexicalAnalyser(string sourceCode) {
            _sourceCode = sourceCode;
        }

        private Token ScanToken()
        {
            char currentChar = (char)Peek();
            switch (currentChar) {
                case '#':
                    return ScanComment();
                case '\'':
                case '"':
                    return ScanString();
                case '0': case '1': case '2': case '3': case '4':
                case '5': case '6': case '7': case '8': case '9':
                    return ScanNumber();
                case '+': case '-': case '*': case '/':
                case '.': case '=': case '%':
                case '<': case '>':
                case '&': case '|':
                case '!': case '?':
                    return ScanOperator();
                case ';':
                    Read();
                    return new Token(TokenType.SemiColon, ";");
                case ':':
                    Read();
                    return new Token(TokenType.Colon, ":");
                case ',':
                    Read();
                    return new Token(TokenType.Comma, ",");
                case '(':
                    Read();
                    return new Token(TokenType.OpenParen, "(");
                case ')':
                    Read();
                    return new Token(TokenType.CloseParen, ")");
                case '[':
                    Read();
                    return new Token(TokenType.OpenBracket, "[");
                case ']':
                    Read();
                    return new Token(TokenType.CloseBracket, "]");
                case '{':
                    Read();
                    return new Token(TokenType.OpenBrace, "{");
                case '}':
                    Read();
                    return new Token(TokenType.CloseBrace, "}");
                default:
                    if (char.IsLetter(currentChar))
                        return Identifier();

                    throw new UnexpectedTokenException($"Unexpected token: '{(char)Read()}'");
            }
        }

        private Token ScanOperator()
        {
            char currentChar = (char)Read();
            string twoChar = currentChar + ((char)Peek()).ToString();

            switch (twoChar) {
                case "/*":
                    Read();
                    return ScanComment(true);
                case "&&": case "||":
                case "==": case "!=":
                case "<=": case ">=":
                    Read();
                    return new Token(TokenType.Comparison, twoChar);
                case "++": case "--":
                    Read();
                    return new Token(TokenType.Postfix, twoChar);
            }

            switch (currentChar) {
                case '=': case '+': case '-': case '*': case '/': case '.':
                case '%': case '?': case '!': case '<': case '>':
                    return new Token(TokenType.Operator, currentChar.ToString());

                default:
                    throw new UnexpectedTokenException($"Unexpected token: {currentChar}");
            }
        }
        
        public LinkedList<Token> Tokenize()
        {
            var tokens = new LinkedList<Token>();
            SkipWhitespace();
            while (CanAdvance()) {
                Token token = ScanToken();
                if (token != null)
                    tokens.AddLast(token);
                SkipWhitespace();
            }

            return tokens;
        }

        private Token Identifier()
        {
            var temp = new StringBuilder();
            while(_position < _sourceCode.Length 
                  && (char.IsLetterOrDigit((char)Peek())
                  || "_".Contains(((char)Peek()).ToString())))

                temp.Append((char)Read());

            switch (temp.ToString().ToLower()) {
                case "if":
                case "use":
                case "for":
                case "func":
                case "else":
                case "true":
                case "break":
                case "false":
                case "while":
                case "return":
                case "default":
                case "foreach":
                case "continue":
                    return new Token(TokenType.Keyword, temp.ToString());
                default:
                    return new Token(TokenType.Identifier, temp.ToString());
            }
        }

        private Token ScanComment(bool multiline = false)
        {
            var temp = new StringBuilder();
            if (multiline) {
                while (Peek() != '*' && Peek(1) != '/' && _position < _sourceCode.Length)
                    temp.Append((char) Read());

                Read();
                return new Token(TokenType.Comment, temp.ToString());
            }

            Read();
            while (Peek() != '\n' && _position < _sourceCode.Length)
                temp.Append((char) Read());
                
            return new Token(TokenType.Comment, temp.ToString());
        }

        private Token ScanNumber()
        {
            var temp = new StringBuilder();
            while (CanAdvance() && char.IsDigit((char)Peek()) || (char)Peek() == '.') {
                if ((char)Peek() == '.') {
                    Read(); temp.Append(".");
                    while(char.IsDigit((char)Peek()))
                        temp.Append((char)Read());

                    return new Token(TokenType.Float, temp.ToString());
                }

                temp.Append((char)Read());
            }

            return new Token(TokenType.Number, temp.ToString());
        }

        private Token ScanString() {
            int begin = Read();
            var temp = new StringBuilder();

            while(CanAdvance() && Peek() != begin) {
                if(Peek() == '\\') {
                    Read();
                    char escapeSequence = (char)Read();
                    switch (escapeSequence) {
                        case 'n': // New line
                            temp.Append("\n");
                            break;
                        case 't': // Horizontal tab
                            temp.Append("\t");
                            break;
                        case 'r': // Carriage return
                            temp.Append("\r");
                            break;
                        case 'a': // Bell (alert)
                            temp.Append("\a");
                            break;
                        case 'b': // Backspace
                            temp.Append("\b");
                            break;
                        default:
                            throw new UnexpectedEscapeSequenceException(@"Unexpected escape sequence: \" + escapeSequence);
                    }
                }
                else temp.Append((char)Read());
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
               : '\0'; // if not, return null char

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