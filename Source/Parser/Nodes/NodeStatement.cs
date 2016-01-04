// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NodeStatement.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And.Parser
{
    public class NodeStatement : AstNode {
        public override void Visit(IAstVisitor visitor)
            => visitor.Visit(this);
    }
}