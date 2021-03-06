using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SPMeta2.Definitions.Webparts;
using SPMeta2.Models;

namespace SPMeta2.Syntax.Default
{

    [Serializable]
    [DataContract]
    public class SimpleFormWebPartModelNode : WebPartModelNode
    {

    }

    public static class SimpleFormWebPartDefinitionSyntax
    {
        #region methods

        public static TModelNode AddSimpleFormWebPart<TModelNode>(this TModelNode model, SimpleFormWebPartDefinition definition)
            where TModelNode : ModelNode, IWebpartHostModelNode, new()
        {
            return AddSimpleFormWebPart(model, definition, null);
        }

        public static TModelNode AddSimpleFormWebPart<TModelNode>(this TModelNode model, SimpleFormWebPartDefinition definition,
            Action<SimpleFormWebPartModelNode> action)
            where TModelNode : ModelNode, IWebpartHostModelNode, new()
        {
            return model.AddTypedDefinitionNode(definition, action);
        }

        #endregion

        #region array overload

        public static TModelNode AddSimpleFormWebParts<TModelNode>(this TModelNode model, IEnumerable<SimpleFormWebPartDefinition> definitions)
           where TModelNode : ModelNode, IWebpartHostModelNode, new()
        {
            foreach (var definition in definitions)
                model.AddDefinitionNode(definition);

            return model;
        }

        #endregion
    }
}
