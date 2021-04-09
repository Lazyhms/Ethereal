// Copyright (c) Ethereal. All rights reserved.

using Ethereal.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc.ModelBinding
{
    /// <summary>
    /// DateTimeOffsetModelBinder
    /// </summary>
    public class DateTimeOffsetModelBinder : IModelBinder
    {
        private readonly DateTimeOffsetBinderOptions? _dateTimeOffsetBinderOptions;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeOffsetModelBinder"/> class.
        /// </summary>
        public DateTimeOffsetModelBinder(
            DateTimeOffsetBinderOptions? dateTimeOffsetBinderOptions,
            ILoggerFactory loggerFactory)
        {
            _dateTimeOffsetBinderOptions = dateTimeOffsetBinderOptions;
            _logger = loggerFactory.CreateLogger<DateTimeOffsetModelBinder>();
        }

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
                else if (type == typeof(DateTimeOffset))
                {
                    if (DateTimeOffset.TryParse(value, valueProviderResult.Culture, DateTimeStyles.None | DateTimeStyles.AllowWhiteSpaces, out var actValue))
                    {
                        model = actValue;
                    }
                    else
                    {
                        model = null;
                    }

                    if (_dateTimeOffsetBinderOptions.HasValue && long.TryParse(value, out var timestamp))
                    {
                        switch (_dateTimeOffsetBinderOptions)
                        {
                            case DateTimeOffsetBinderOptions.SupportSeconds:
                                model = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                                break;

                            case DateTimeOffsetBinderOptions.SupportMilliseconds:
                                model = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
                                break;

                            default:
                                model = null;
                                break;
                        }
                    }
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