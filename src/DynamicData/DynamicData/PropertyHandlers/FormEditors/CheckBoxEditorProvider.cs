﻿using System;
using System.Reflection;
using DotVVM.Framework.Controls.DynamicData.Metadata;
using DotVVM.Framework.Utils;

namespace DotVVM.Framework.Controls.DynamicData.PropertyHandlers.FormEditors
{
    /// <summary>
    /// Renders a CheckBox control for properties of boolean type.
    /// </summary>
    public class CheckBoxEditorProvider : FormEditorProviderBase
    {
        public override bool CanHandleProperty(PropertyDisplayMetadata property, DynamicDataContext context)
        {
            return ReflectionUtils.UnwrapNullableType(property.Type) == typeof(bool);
        }
        
        public override DotvvmControl CreateControl(PropertyDisplayMetadata property, DynamicEditor.Props props, DynamicDataContext context)
        {
            var checkBox = new CheckBox()
                .AddCssClasses(ControlCssClass, property.Styles?.FormControlCssClass)
                .SetProperty(c => c.Changed, props.Changed)
                .SetProperty(c => c.Checked, props.Property)
                .SetProperty(c => c.Text, property.GetDisplayName().ToBinding(context))
                .SetProperty(c => c.Enabled, props.Enabled);
            return checkBox;
        }
    }
}
