﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace Microsoft.CodeAnalysis.CSharp
{
    internal partial class DeconstructionLocalPendingInference
    {
        public BoundLocal SetInferredType(TypeSymbol type, bool success)
        {
            Debug.Assert(type != null);

            var syntaxNode = (VariableDeclaratorSyntax)this.Syntax;

            Binder.DeclareLocalVariable((SourceLocalSymbol)this.LocalSymbol, syntaxNode.Identifier, type);

            return new BoundLocal(syntaxNode, this.LocalSymbol, constantValueOpt: null, type: type, hasErrors: this.HasErrors || !success);
        }

        public BoundLocal FailInference(Binder binder)
        {
            return this.SetInferredType(binder.CreateErrorType("var"), success: false);
        }
    }
}