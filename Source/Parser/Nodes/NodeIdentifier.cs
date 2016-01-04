// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NodeIdentifier.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using And.Lexer;

namespace And.Parser
{
    public class NodeIdentifier : AstNode
    {
        public string Identifier { get; set; }

        public NodeIdentifier(int position, string identifier)
            : base(position)
        {
            Identifier = identifier;
        }

        public static AstNode Parse(Parser parser)
            => new NodeIdentifier(parser.Position, parser.Stream.Expect(TokenType.Identifier).Value);

        public override string ToString() => Identifier;

        public override void Visit(IAstVisitor visitor)
            => visitor.Visit(this);
    }
}
