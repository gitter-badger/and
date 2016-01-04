// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeBlock.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And.Parser {
    public class CodeBlock : AstNode
    {
        public CodeBlock(int position) : base(position) { }

        public override void Visit(IAstVisitor visitor)
            => visitor.Visit(this);
    }
}