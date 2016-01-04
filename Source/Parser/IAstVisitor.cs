// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAstVisitor.cs" author="Ertuğrul Seyhan">
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace And.Parser {
    public interface IAstVisitor
    {
        void Visit(CodeBlock node);
        void Visit(NodeExpr node);
        void Visit(NodeString node);
        void Visit(NodeNumber node);
        void Visit(NodeFuncDecl node);
        void Visit(NodeFuncCall node);
        void Visit(NodeStatement node);
        void Visit(NodeIdentifier node);
    }
}