
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace SampleMicroserviceApp.Identity.Web.API.Extensions;

public static class ODataEdmModelProvider
{
    public static IEdmModel GetEdmModel()
    {
        ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
        // modelBuilder.EntitySet<InsuredDto>("AllInsureds");
        // modelBuilder.EntitySet<CostDto>("AllCosts");
        return modelBuilder.GetEdmModel();
    }
}