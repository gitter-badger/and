// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Main.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And {
    using System;
    using System.IO;
    using System.Linq;
    using System.Diagnostics;
    using static System.Console;
    using System.Collections.Generic;

    public class And
    {
        private static void Main(string[] args)
        {
            if (args.Length > 0) {
                if (!File.Exists(args[0]))
                    return;

                var sw = new Stopwatch();
                sw.Start();

                    string tokens = GetTokens(File.ReadAllText(args[0]));
                
                sw.Stop();
                ElapsedTime = sw.ElapsedMilliseconds;
                Write(tokens.Substring(0, tokens.Length - 2) + $"\n\nElapsed Time: {ElapsedTime} ms.\n" +
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
                            goto inputSource;
                        }

                        if (Path.GetExtension(inputSource) != ".and") {
                            Write("Incorrect file. File extension must be .and\n\n");
                            goto inputSource;
                        }
                        break;
                }
                
                Write(Environment.NewLine);
                string tokens = GetTokens(File.ReadAllText(inputSource));
                WriteLine(tokens.Substring(0, tokens.Length - 2) /* This will remove the last empty lines */ 
                                                                 + $"\n\nElapsed Time: {ElapsedTime} milisecond.\n"
                                                                 + "Done.\n");

            } while (args.Length == 0);
        }

        static string GetTokens(string sourceCode)
        {
            var sw = new Stopwatch();
            sw.Start();

                var lexer = new Lexer(sourceCode);
                LinkedList<Token> tokens = lexer.Tokenize();

            sw.Stop();
            ElapsedTime = sw.ElapsedMilliseconds;

            return tokens.Aggregate(string.Empty, (current, i) => current + $"Type: {i.Type}\n" + $"Value: {i.Value}\n\n");
        }

        private static double ElapsedTime { get; set; }
    }
}