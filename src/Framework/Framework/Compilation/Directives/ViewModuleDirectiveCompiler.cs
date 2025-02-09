﻿using System.Collections.Generic;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser;
using System.Linq;
using DotVVM.Framework.Binding;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Framework.Compilation.Directives
{
    public class ViewModuleDirectiveCompiler : DirectiveCompiler<IAbstractViewModuleDirective, ViewModuleCompilationResult?>
    {
        private readonly bool isMarkupControl;
        private readonly DotvvmResourceRepository resourceRepo;

        public ViewModuleDirectiveCompiler(IReadOnlyDictionary<string, IReadOnlyList<DothtmlDirectiveNode>> directiveNodesByName, IAbstractTreeBuilder treeBuilder, bool isMarkupControl, DotvvmResourceRepository resourceRepo)
            : base(directiveNodesByName, treeBuilder)
        {
            this.isMarkupControl = isMarkupControl;
            this.resourceRepo = resourceRepo;
        }

        public override string DirectiveName => ParserConstants.ViewModuleDirective;

        protected override ViewModuleCompilationResult? CreateArtefact(IReadOnlyList<IAbstractViewModuleDirective> resolvedDirectives)
        {
            return ResolveImportedViewModules(resolvedDirectives);
        }

        private ViewModuleCompilationResult? ResolveImportedViewModules(IReadOnlyList<IAbstractViewModuleDirective> moduleDirectives)
        {
            if (moduleDirectives.Count == 0)
            {
                return null;
            }

            var resources =
                moduleDirectives
                .Select(x => {
                    if (resourceRepo is object && x.DothtmlNode is object)
                    {
                        var resource = resourceRepo.FindResource(x.ImportedResourceName);
                        var node = (x.DothtmlNode as DothtmlDirectiveNode)?.ValueNode ?? x.DothtmlNode;
                        if (resource is null)
                        {
                            node.AddError($"Cannot find resource named '{x.ImportedResourceName}' referenced by the @js directive!");
                        }
                        else if (!(resource is ScriptModuleResource))
                        {
                            node.AddError($"The resource named '{x.ImportedResourceName}' referenced by the @js directive must be of the ScriptModuleResource type!");
                        }
                    }
                    return x.ImportedResourceName;
                })
                .ToArray();

            return new ViewModuleCompilationResult(
                new JsExtensionParameter(null, isMarkupControl),
                new ViewModuleReferenceInfo(null, resources, isMarkupControl));
        }

        protected override IAbstractViewModuleDirective Resolve(DothtmlDirectiveNode directiveNode) =>
            TreeBuilder.BuildViewModuleDirective(directiveNode, modulePath: directiveNode.Value, resourceName: directiveNode.Value);
    }

}
