using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SPMeta2.Definitions.Webparts;
using SPMeta2.Models;
using SPMeta2.Standard.Definitions.Webparts;
using SPMeta2.Syntax.Default;

namespace SPMeta2.Standard.Syntax
{

    [Serializable]
    [DataContract]
    public class SocialCommentWebPartModelNode : WebPartModelNode
    {

    }

    public static class SocialCommentWebPartDefinitionSyntax
    {
        #region methods

        public static TModelNode AddSocialCommentWebPart<TModelNode>(this TModelNode model, SocialCommentWebPartDefinition definition)
            where TModelNode : ModelNode, IWebpartHostModelNode, new()
        {
            return AddSocialCommentWebPart(model, definition, null);
        }

        public static TModelNode AddSocialCommentWebPart<TModelNode>(this TModelNode model, SocialCommentWebPartDefinition definition,
            Action<SocialCommentWebPartModelNode> action)
            where TModelNode : ModelNode, IWebpartHostModelNode, new()
        {
            return model.AddTypedDefinitionNode(definition, action);
        }

        #endregion

        #region array overload

        public static TModelNode AddSocialCommentWebParts<TModelNode>(this TModelNode model, IEnumerable<SocialCommentWebPartDefinition> definitions)
           where TModelNode : ModelNode, IWebpartHostModelNode, new()
        {
            foreach (var definition in definitions)
                model.AddDefinitionNode(definition);

            return model;
        }

        #endregion
    }
}
