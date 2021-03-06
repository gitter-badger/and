﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AstNode.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And.Parser
{
    using System.Collections.Generic;

    public abstract class AstNode
    {
        public List<AstNode> Children { get; }
        public int Position { get; set; }
        public abstract void Visit(IAstVisitor visitor);

        protected AstNode() : this(-1) {}
        protected AstNode(int position) {
            Children = new List<AstNode>();
            Position = position;
        }
    }
}