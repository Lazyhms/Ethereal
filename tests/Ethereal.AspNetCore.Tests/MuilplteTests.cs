using Ethereal.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading.Tasks;
using Xunit;

namespace Ethereal.AspNetCore.Tests
{
    public class MuilplteTests
    {
        [Fact]
        public async Task ModelBinder_Tests()
        {
            var bindingContext = GetBindingContext();

            bindingContext.ValueProvider = new SimpleValueProvider(new CultureInfo("fr-FR"))
            {
                { "theModelName", "null"  }
            };
            var binder = GetBinder(DateTimeStyles.AssumeLocal);

            await binder.BindModelAsync(bindingContext);
        }

        private IModelBinder GetBinder(DateTimeStyles? dateTimeStyles = null) => new GuidModelBinder(null);

        private static DefaultModelBindingContext GetBindingContext(Type modelType = null)
        {
            modelType ??= typeof(Guid);
            return new DefaultModelBindingContext
            {
                ModelMetadata = new EmptyModelMetadataProvider().GetMetadataForType(modelType),
                ModelName = "theModelName",
                ModelState = new ModelStateDictionary(),
                ValueProvider = new SimpleValueProvider() // empty
            };
        }

        [Fact]
        public void Valiad()
        {
            var t = new T { MyProperty = new FormFile(null, 10, 1000000000000, "dada", "abc.txt") { } };
            var validationResults = new List<ValidationResult>();
            var ttt1 = Validator.TryValidateObject(t, new ValidationContext(t), validationResults, true);
        }

    }

    internal class T
    {
        [FileLength(10L)]
        public IFormFile MyProperty { get; set; }
    }
}
