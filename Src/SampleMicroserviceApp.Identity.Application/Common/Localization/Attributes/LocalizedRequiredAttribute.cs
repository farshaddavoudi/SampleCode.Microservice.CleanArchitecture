using System.ComponentModel.DataAnnotations;

namespace SampleMicroserviceApp.Identity.Application.Common.Localization.Attributes
{
    public class LocalizedRequiredAttribute : RequiredAttribute
    {
        public LocalizedRequiredAttribute()
        {
            //ErrorMessageResourceType = typeof(DataAnnotationStrings);
            //ErrorMessageResourceName = nameof(DataAnnotationStrings.RequiredAttribute_ValidationError);
        }

        public LocalizedRequiredAttribute(string dataAnnotationStringsResourceKey)
        {
            //ErrorMessageResourceType = typeof(DataAnnotationStrings);
            //ErrorMessageResourceName = dataAnnotationStringsResourceKey;
        }
    }
}