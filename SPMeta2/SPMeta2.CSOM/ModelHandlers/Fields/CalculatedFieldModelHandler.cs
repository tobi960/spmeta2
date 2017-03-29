﻿using System;
using System.Xml.Linq;
using Microsoft.SharePoint.Client;
using SPMeta2.Definitions;
using SPMeta2.Definitions.Fields;
using SPMeta2.Enumerations;
using SPMeta2.Services;
using SPMeta2.Utils;

namespace SPMeta2.CSOM.ModelHandlers.Fields
{
    public class CalculatedFieldModelHandler : FieldModelHandler
    {
        #region properties

        public override Type TargetType
        {
            get { return typeof(CalculatedFieldDefinition); }
        }

        protected override Type GetTargetFieldType(FieldDefinition model)
        {
            return typeof(FieldCalculated);
        }

        #endregion

        #region methods

        protected override void ProcessFieldProperties(Field field, FieldDefinition fieldModel)
        {
            // let base setting be setup
            base.ProcessFieldProperties(field, fieldModel);

            var typedFieldModel = fieldModel.WithAssertAndCast<CalculatedFieldDefinition>("model", value => value.RequireNotNull());
            var typedField = field.Context.CastTo<FieldCalculated>(field);

            if (!string.IsNullOrEmpty(typedFieldModel.Formula))
            {
                // can't really validate it automatically
                // Improve CalculatedFieldDefinition with field ref check
                // https://github.com/SubPointSolutions/spmeta2/issues/648
                TraceService.Verbose((int)LogEventId.ModelProvisionCoreCall, "Updating formula for a CalculatedField. Ensure FieldReferences are correct.");

                typedField.Formula = typedFieldModel.Formula;

                typedField.DateFormat = (DateTimeFieldFormatType)
                    Enum.Parse(typeof(DateTimeFieldFormatType), typedFieldModel.DateFormat);
            }

            if (!string.IsNullOrEmpty(typedFieldModel.OutputType))
                typedField.OutputType = (FieldType)Enum.Parse(typeof(FieldType), typedFieldModel.OutputType);
        }

        protected override void ProcessSPFieldXElement(XElement fieldTemplate, FieldDefinition fieldModel)
        {
            base.ProcessSPFieldXElement(fieldTemplate, fieldModel);

            var typedFieldModel = fieldModel.WithAssertAndCast<CalculatedFieldDefinition>("model", value => value.RequireNotNull());

            if (typedFieldModel.CurrencyLocaleId.HasValue)
                fieldTemplate.SetAttribute(BuiltInFieldAttributes.LCID, typedFieldModel.CurrencyLocaleId);

            // can't really validate it automatically
            // Improve CalculatedFieldDefinition with field ref check
            // https://github.com/SubPointSolutions/spmeta2/issues/648
            TraceService.Verbose((int)LogEventId.ModelProvisionCoreCall, "Crafting formula for a CalculatedField. Ensure FieldReferences are correct.");

            // should be a new XML node
            var formulaNode = new XElement(BuiltInFieldAttributes.Formula, typedFieldModel.Formula);
            fieldTemplate.Add(formulaNode);

            // must be enum name, actually

            // Format="0" when provisioning CalculatedField #969
            // https://github.com/SubPointSolutions/spmeta2/issues/969
            fieldTemplate.SetAttribute(BuiltInFieldAttributes.Format, typedFieldModel.DateFormat);

            if (typedFieldModel.ShowAsPercentage.HasValue)
                fieldTemplate.SetAttribute(BuiltInFieldAttributes.Percentage, typedFieldModel.ShowAsPercentage.Value.ToString().ToUpper());

            if (!string.IsNullOrEmpty(typedFieldModel.DisplayFormat))
                fieldTemplate.SetAttribute(BuiltInFieldAttributes.Decimals, NumberFieldModelHandler.GetDecimalsValue(typedFieldModel.DisplayFormat));

            fieldTemplate.SetAttribute(BuiltInFieldAttributes.ResultType, typedFieldModel.OutputType);

            if (typedFieldModel.FieldReferences.Count > 0)
            {
                var fieldRefsNode = new XElement("FieldRefs");

                foreach (var fieldRef in typedFieldModel.FieldReferences)
                {
                    var fieldRefNode = new XElement("FieldRef");

                    fieldRefNode.SetAttribute("Name", fieldRef);
                    fieldRefsNode.Add(fieldRefNode);
                }

                fieldTemplate.Add(fieldRefsNode);
            }
        }

        #endregion
    }
}
