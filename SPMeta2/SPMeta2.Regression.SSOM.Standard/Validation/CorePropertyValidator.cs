using System;
using Microsoft.Office.Server.UserProfiles;
using SPMeta2.Definitions;
using SPMeta2.Regression.SSOM.Validation;
using SPMeta2.SSOM.ModelHosts;
using SPMeta2.SSOM.Standard.ModelHandlers;
using SPMeta2.Standard.Definitions;
using SPMeta2.Utils;

namespace SPMeta2.Regression.SSOM.Standard.Validation
{
    public class CorePropertyValidator : CorePropertyModelHandler
    {
        #region properties

        public override Type TargetType
        {
            get { return typeof(CorePropertyDefinition); }
        }

        #endregion

        #region methods

        public override void DeployModel(object modelHost, DefinitionBase model)
        {
            var host = modelHost.WithAssertAndCast<SiteModelHost>("modelHost", value => value.RequireNotNull());
            var typedModel = model.WithAssertAndCast<CorePropertyDefinition>("model", value => value.RequireNotNull());

            CoreProperty spObject = GetCurrentCoreProperty(host.HostSite, typedModel);

            var assert = ServiceFactory.AssertService
                                       .NewAssert(typedModel, typedModel)
                                       .ShouldNotBeNull(spObject);


            assert.ShouldBeEqual(m => m.Name, o => o.Name);
            assert.ShouldBeEqual(m => m.DisplayName, o => o.DisplayName);
            assert.ShouldBeEqual(m => m.Type, o => o.Type);

            assert.ShouldBeEqual(m => m.Length, o => o.Length);

            if (typedModel.IsAlias.HasValue)
                assert.ShouldBeEqual(m => m.IsAlias, o => o.IsAlias);
            else
                assert.SkipProperty(m => m.IsAlias, "IsAlias is null or empty");

            if (typedModel.IsMultivalued.HasValue)
                assert.ShouldBeEqual(m => m.IsMultivalued, o => o.IsMultivalued);
            else
                assert.SkipProperty(m => m.IsMultivalued, "IsMultivalued is null or empty");

            if (typedModel.IsSearchable.HasValue)
                assert.ShouldBeEqual(m => m.IsSearchable, o => o.IsSearchable);
            else
                assert.SkipProperty(m => m.IsSearchable, "IsSearchable is null or empty");

            if (!string.IsNullOrEmpty(typedModel.Description))
                assert.ShouldBeEqual(m => m.Description, o => o.Description);
            else
                assert.SkipProperty(m => m.Description);
        }

        #endregion
    }
}
