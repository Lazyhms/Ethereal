// Copyright (c) Ethereal. All rights reserved.

using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc.ModelBinding
{
    /// <summary>
    /// Guid ModelBinder
    /// </summary>
    public class GuidModelBinder : IModelBinder
    {
        private readonly ILogger _logger;

        /// <summary>
        /// GuidModelBinder
        /// </summary>
        public GuidModelBinder(ILoggerFactory loggerFactory)
            => _logger = loggerFactory.CreateLogger<GuidModelBinder>();

        /// <inheritdoc/>
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Check.NotNull(bindingContext, nameof(bindingContext));

            _logger.AttemptingToBindModel(bindingContext);

            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                _logger.FoundNoValueInRequest(bindingContext);

                // no entry
                _logger.DoneAttemptingToBindModel(bindingContext);
                return;
            }

            var modelState = bindingContext.ModelState;
            modelState.SetModelValue(modelName, valueProviderResult);

            var metadata = bindingContext.ModelMetadata;
            var type = metadata.UnderlyingOrModelType;

            try
            {
                var value = valueProviderResult.FirstValue;

                object? model;
                if (value is null)
                {
                    model = null;
                }
                else if (type == typeof(Guid))
                {
                    Guid.TryParse(value, out var actValue);
                    model = actValue;
                }
                else
                {
                    throw new NotSupportedException();
                }

                if (model == null && !metadata.IsReferenceOrNullableType)
                {
                    modelState.TryAddModelError(
                        modelName,
                        metadata.ModelBindingMessageProvider.ValueMustNotBeNullAccessor(
                            valueProviderResult.ToString()));
                }
                else
                {
                    bindingContext.Result = ModelBindingResult.Success(model);
                }
            }
            catch (Exception exception)
            {
                modelState.TryAddModelError(modelName, exception, metadata);
            }

            _logger.DoneAttemptingToBindModel(bindingContext);
            await Task.CompletedTask;
        }
    }
}