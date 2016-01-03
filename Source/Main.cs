// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Main.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using And.Lexer;

namespace And {
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Diagnostics;
    using System.Collections.Generic;
    using static System.Console;

    public class And
    {
        private static void Main(string[] args)
        {
            if (args.Length > 0) {
                if (!File.Exists(args[0]))
                    return;

                var sw = new Stopwatch();
                sw.Start();

                    string tokens = GetTokens(File.ReadAllText(args[0], Encoding.UTF8));
                
                sw.Stop();
                ElapsedTime = sw.Elapsed;
                Write(tokens.Substring(0, tokens.Length - 2) + $"\n\nElapsed Time: {ElapsedTime}\n" +
                                                                "Done.\n");
                return;
            }

            Write("Please specify an input source or start with an argument\n");
            do
            {
                inputSource:
                Write(">>> ");
                var inputSource = ReadLine();

                switch (inputSource) {
                  #if DEBUG
                    case "lexical.and":
                        inputSource = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Github\\And\\Tests\\lexical.and";
                        goto default;
                  #endif
                    case "":
                        goto inputSource;
                    case "cls":
                    case "clear":
                        Clear();
                        Main(new string[] {});
                        break;
                    case "exit":
                    case "quit":
                        Environment.Exit(-1);
                        break;
                    default:
                        if (!File.Exists(inputSource)) {
                            Write($"File '{Path.GetFileName(inputSource)}' doesn't exists.\n\n");
                        }
                        else if (Path.GetExtension(inputSource) != ".and") {
                            Write("File extension must be .and\n\n");
                        } else {
                            string tokens = GetTokens(File.ReadAllText(inputSource, Encoding.UTF8));
                            if (tokens.Length == 0) break;

                            Write(Environment.NewLine);
                            WriteLine(tokens.Substring(0, tokens.Length - 2) /* This will remove the last empty lines */
                                                                 + $"\n\nElapsed Time: {ElapsedTime}\n"
                                                                 + "Done.\n");
                        }
                        break;
                }
            } while (args.Length == 0);
        }

        static string GetTokens(string sourceCode)
        {
            var sw = new Stopwatch();
            sw.Start();

                var lexer = new LexicalAnalyser(sourceCode);
                LinkedList<Token> tokens = lexer.Tokenize();

            sw.Stop();
            ElapsedTime = sw.Elapsed;

            return tokens.Aggregate(string.Empty, (current, i) => current + $"Type: {i.Type}\n" + $"Value: {i.Value}\n\n");
        }

        private static TimeSpan ElapsedTime { get; set; }
    }
}