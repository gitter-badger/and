// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NodeString.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And.Parser
{
    public class NodeString : AstNode
    {
        public string Value { get; set; }

        public NodeString(string value, int position)
            : base(position)
        {
            Value = value;
        }

        public override void Visit(IAstVisitor visitor)
            => visitor.Visit(this);
    }
}