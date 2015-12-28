// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AstNode.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And
{
    using System.Collections.Generic;

    public abstract class AstNode
    {
        public List<AstNode> Children { get; private set;}

        protected AstNode() {
            Children = new List<AstNode>();
        }
    }
}