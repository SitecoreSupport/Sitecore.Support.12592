using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.XA.Foundation.RenderingVariants;
using Sitecore.XA.Foundation.RenderingVariants.Fields;
using Sitecore.XA.Foundation.SitecoreExtensions.Extensions;
using Sitecore.XA.Foundation.Variants.Abstractions.Fields;
using Sitecore.XA.Foundation.Variants.Abstractions.Pipelines.ParseVariantFields;
using Sitecore.XA.Foundation.Variants.Abstractions.Services;

namespace Sitecore.Support.XA.Foundation.RenderingVariants.Pipelines.ParseVariantFields
{
  public class ParseSection : Sitecore.XA.Foundation.RenderingVariants.Pipelines.ParseVariantFields.ParseSection
  {
    public override void TranslateField(ParseVariantFieldArgs args)
    {

      string linkField;
      bool isRealVariantRootItem = args.VariantRootItem.Template.DoesTemplateInheritFrom(Templates.VariantDefinition.ID);
      if (isRealVariantRootItem)
      {
        linkField = args.VariantRootItem[
          Sitecore.XA.Foundation.Variants.Abstractions.Templates.IVariantDefinition.Fields.LinkField];
      }
      else
      {
        Item realVariantRootItem = args.VariantRootItem.Axes.GetAncestors()
         .FirstOrDefault(item => item.Template.DoesTemplateInheritFrom(Templates.VariantDefinition.ID));
        if (realVariantRootItem.Equals(null))
        {
          linkField = args.VariantRootItem[
            Sitecore.XA.Foundation.Variants.Abstractions.Templates.IVariantDefinition.Fields.LinkField];
        }
        else
        {
          linkField = realVariantRootItem[
            Sitecore.XA.Foundation.Variants.Abstractions.Templates.IVariantDefinition.Fields.LinkField];
        }
      }
      args.TranslatedField = new VariantSection(args.VariantItem)
      {
        ItemName = args.VariantItem.Name,
        Tag = args.VariantItem.Fields[Sitecore.XA.Foundation.RenderingVariants.Templates.VariantSection.Fields.Tag].GetEnumValue(),
        CssClass = ((BaseItem)args.VariantItem)[Sitecore.XA.Foundation.RenderingVariants.Templates.VariantSection.Fields.CssClass],
        LinkField = linkField,
        IsLink = (((BaseItem)args.VariantItem)[Sitecore.XA.Foundation.RenderingVariants.Templates.VariantSection.Fields.IsLink] == "1"),
        SectionFields = ((args.VariantItem.Children.Count > 0) ? ServiceLocator.ServiceProvider.GetService<IVariantFieldParser>().ParseVariantFields(args.VariantItem, false) : new List<BaseVariantField>())
      };
    }
  }
}