using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SPMeta2.CSOM.ModelHosts;
using SPMeta2.Definitions;
using SPMeta2.Definitions.Base;
using SPMeta2.Definitions.Webparts;
using SPMeta2.Enumerations;
using SPMeta2.Services;
using SPMeta2.Utils;

namespace SPMeta2.CSOM.ModelHandlers.Webparts
{
    public class SimpleFormWebPartModelHandler : WebPartModelHandler
    {
        #region properties

        public override Type TargetType
        {
            get { return typeof(SimpleFormWebPartDefinition); }
        }

        #endregion

        #region methods

        protected override string GetWebpartXmlDefinition(ListItemModelHost listItemModelHost, WebPartDefinitionBase webPartModel)
        {
            var typedModel = webPartModel.WithAssertAndCast<SimpleFormWebPartDefinition>("model", value => value.RequireNotNull());

            var wpXml = WebpartXmlExtensions.LoadWebpartXmlDocument(BuiltInWebPartTemplates.SimpleFormWebPart);

            // TODO, specific XML processing

            return wpXml.ToString();
        }

        #endregion
    }
}
