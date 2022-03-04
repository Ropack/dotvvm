using System.Reflection;
using DotVVM.Framework.Controls.DynamicData.Metadata;

namespace DotVVM.Framework.Controls.DynamicData.PropertyHandlers.GridColumns
{
    public class TextGridColumnProvider : GridColumnProviderBase
    {
        public override bool CanHandleProperty(PropertyDisplayMetadata property, DynamicDataContext context)
        {
            return TextBoxHelper.CanHandleProperty(property.Type);
        }

        protected override GridViewColumn CreateColumnCore(PropertyDisplayMetadata property, DynamicGridColumn.Props props, DynamicDataContext context)
        {
            var column = new GridViewTextColumn();
            column.FormatString = property.FormatString;
            column.SetBinding(GridViewTextColumn.ValueBindingProperty, context.CreateValueBinding(property));
            column.IsEditable = property.IsEditable;
            return column;
        }
    }
}
