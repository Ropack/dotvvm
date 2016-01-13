using DotVVM.Framework.Runtime.ControlTree;

namespace DotVVM.Framework.Runtime
{
    public interface IControlType
    {

        ITypeDescriptor Type { get; }
        
        string VirtualPath { get; }

        ITypeDescriptor DataContextRequirement { get; }

    }
}