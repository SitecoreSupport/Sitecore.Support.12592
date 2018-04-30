using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Extensions;
using Sitecore.XA.Foundation.RenderingVariants;
using Sitecore.XA.Foundation.RenderingVariants.Fields;
using Sitecore.XA.Foundation.SitecoreExtensions.Extensions;
using Sitecore.XA.Foundation.Variants.Abstractions.Fields;
using Sitecore.XA.Foundation.Variants.Abstractions.Pipelines.ParseVariantFields;
using Sitecore.XA.Foundation.Variants.Abstractions.Services;


namespace Sitecore.Support.XA.Foundation.RenderingVariants.Pipelines.ParseVariantFields
{
  public class ParseSection:Sitecore.XA.Foundation.RenderingVariants.Pipelines.ParseVariantFields.ParseSection
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
         .FirstOrDefault(item=>item.Template.DoesTemplateInheritFrom(Templates.VariantDefinition.ID));
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

  public class ParseField : Sitecore.XA.Foundation.RenderingVariants.Pipelines.ParseVariantFields.ParseField
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
      args.TranslatedField = new VariantField(args.VariantItem)
      {
        ItemName = args.VariantItem.Name,
        Tag = args.VariantItem.Fields[Sitecore.XA.Foundation.RenderingVariants.Templates.VariantField.Fields.Tag].GetEnumValue(),
        CssClass = ((BaseItem)args.VariantItem)[Sitecore.XA.Foundation.RenderingVariants.Templates.VariantField.Fields.CssClass],
        Prefix = ((BaseItem)args.VariantItem)[Sitecore.XA.Foundation.RenderingVariants.Templates.VariantField.Fields.Prefix],
        Suffix = ((BaseItem)args.VariantItem)[Sitecore.XA.Foundation.RenderingVariants.Templates.VariantField.Fields.Suffix],
        FieldName = ((BaseItem)args.VariantItem)[Sitecore.XA.Foundation.RenderingVariants.Templates.VariantField.Fields.FieldName],
        IsLink = (((BaseItem)args.VariantItem)[Sitecore.XA.Foundation.RenderingVariants.Templates.VariantField.Fields.IsLink] == "1"),
        IsDownloadLink = (((BaseItem)args.VariantItem)[Sitecore.XA.Foundation.RenderingVariants.Templates.VariantField.Fields.IsDownloadLink] == "1"),
        IsPrefixLink = (((BaseItem)args.VariantItem)[Sitecore.XA.Foundation.RenderingVariants.Templates.VariantField.Fields.IsPrefixLink] == "1"),
        IsSuffixLink = (((BaseItem)args.VariantItem)[Sitecore.XA.Foundation.RenderingVariants.Templates.VariantField.Fields.IsSuffixLink] == "1"),
        LinkField = linkField,
        UseFieldRenderer = (((BaseItem)args.VariantItem)[Sitecore.XA.Foundation.RenderingVariants.Templates.VariantField.Fields.UseFieldRenderer] == "1"),
        RenderIfEmpty = (((BaseItem)args.VariantItem)[Sitecore.XA.Foundation.RenderingVariants.Templates.VariantField.Fields.RenderIfEmpty] == "1"),
        FallbackFields = ((args.VariantItem.Children.Count > 0) ? ServiceLocator.ServiceProvider.GetService<IVariantFieldParser>().ParseVariantFields(args.VariantItem, false) : new List<BaseVariantField>())
      };
    }
  }
}