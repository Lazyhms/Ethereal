// Copyright (c) Ethereal. All rights reserved.

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.AspNetCore.Mvc
{
    internal static class MvcCoreLoggerExtensions
    {
        private static readonly Action<ILogger, Type, string, Exception?> _attemptingToBindModel;
        private static readonly Action<ILogger, string?, Type, string?, Exception?> _attemptingToBindParameterModel;
        private static readonly Action<ILogger, Type?, string?, Type, string?, Exception?> _attemptingToBindPropertyModel;
        private static readonly Action<ILogger, Type, string, Exception?> _doneAttemptingToBindModel;
        private static readonly Action<ILogger, string?, Type, Exception?> _doneAttemptingToBindParameterModel;
        private static readonly Action<ILogger, Type?, string?, Type?, Exception?> _doneAttemptingToBindPropertyModel;
        private static readonly Action<ILogger, string, string?, Type, Exception?> _foundNoValueForParameterInRequest;
        private static readonly Action<ILogger, string, Type?, string?, Type?, Exception?> _foundNoValueForPropertyInRequest;
        private static readonly Action<ILogger, string, Type, Exception?> _foundNoValueInRequest;

        static MvcCoreLoggerExtensions()
        {
            _attemptingToBindParameterModel = LoggerMessage.Define<string?, Type, string?>(
                LogLevel.Debug,
                new EventId(44, "AttemptingToBindParameterModel"),
                "Attempting to bind parameter '{ParameterName}' of type '{ModelType}' using the name '{ModelName}' in request data ...");
            _attemptingToBindPropertyModel = LoggerMessage.Define<Type?, string?, Type, string?>(
                LogLevel.Debug,
                new EventId(13, "AttemptingToBindPropertyModel"),
                "Attempting to bind property '{PropertyContainerType}.{PropertyName}' of type '{ModelType}' using the name '{ModelName}' in request data ...");
            _attemptingToBindModel = LoggerMessage.Define<Type, string>(
               LogLevel.Debug,
               new EventId(24, "AttemptingToBindModel"),
               "Attempting to bind model of type '{ModelType}' using the name '{ModelName}' in request data ...");
            _doneAttemptingToBindParameterModel = LoggerMessage.Define<string?, Type>(
               LogLevel.Debug,
                new EventId(45, "DoneAttemptingToBindParameterModel"),
               "Done attempting to bind parameter '{ParameterName}' of type '{ModelType}'.");
            _doneAttemptingToBindPropertyModel = LoggerMessage.Define<Type?, string?, Type?>(
               LogLevel.Debug,
                new EventId(14, "DoneAttemptingToBindPropertyModel"),
               "Done attempting to bind property '{PropertyContainerType}.{PropertyName}' of type '{ModelType}'.");
            _doneAttemptingToBindModel = LoggerMessage.Define<Type, string>(
               LogLevel.Debug,
               new EventId(25, "DoneAttemptingToBindModel"),
               "Done attempting to bind model of type '{ModelType}' using the name '{ModelName}'.");
            _foundNoValueForPropertyInRequest = LoggerMessage.Define<string, Type?, string?, Type?>(
              LogLevel.Debug,
               new EventId(15, "FoundNoValueForPropertyInRequest"),
              "Could not find a value in the request with name '{ModelName}' for binding property '{PropertyContainerType}.{ModelFieldName}' of type '{ModelType}'.");
            _foundNoValueForParameterInRequest = LoggerMessage.Define<string, string?, Type>(
               LogLevel.Debug,
                new EventId(16, "FoundNoValueForParameterInRequest"),
               "Could not find a value in the request with name '{ModelName}' for binding parameter '{ModelFieldName}' of type '{ModelType}'.");
            _foundNoValueInRequest = LoggerMessage.Define<string, Type?>(
               LogLevel.Debug,
                new EventId(46, "FoundNoValueInRequest"),
               "Could not find a value in the request with name '{ModelName}' of type '{ModelType}'.");
        }

        public static void AttemptingToBindModel(this ILogger logger, ModelBindingContext bindingContext)
        {
            if (!logger.IsEnabled(LogLevel.Debug))
            {
                return;
            }

            var modelMetadata = bindingContext.ModelMetadata;
            switch (modelMetadata.MetadataKind)
            {
                case ModelMetadataKind.Parameter:
                    _attemptingToBindParameterModel(
                        logger,
                        modelMetadata.ParameterName,
                        modelMetadata.ModelType,
                        bindingContext.ModelName,
                        null);
                    break;

                case ModelMetadataKind.Property:
                    _attemptingToBindPropertyModel(
                        logger,
                        modelMetadata.ContainerType,
                        modelMetadata.PropertyName,
                        modelMetadata.ModelType,
                        bindingContext.ModelName,
                        null);
                    break;

                case ModelMetadataKind.Type:
                    _attemptingToBindModel(logger, bindingContext.ModelType, bindingContext.ModelName, null);
                    break;
            }
        }

        public static void DoneAttemptingToBindModel(this ILogger logger, ModelBindingContext bindingContext)
        {
            if (!logger.IsEnabled(LogLevel.Debug))
            {
                return;
            }

            var modelMetadata = bindingContext.ModelMetadata;
            switch (modelMetadata.MetadataKind)
            {
                case ModelMetadataKind.Parameter:
                    _doneAttemptingToBindParameterModel(
                        logger,
                        modelMetadata.ParameterName,
                        modelMetadata.ModelType,
                        null);
                    break;

                case ModelMetadataKind.Property:
                    _doneAttemptingToBindPropertyModel(
                        logger,
                        modelMetadata.ContainerType,
                        modelMetadata.PropertyName,
                        modelMetadata.ModelType,
                        null);
                    break;

                case ModelMetadataKind.Type:
                    _doneAttemptingToBindModel(logger, bindingContext.ModelType, bindingContext.ModelName, null);
                    break;
            }
        }

        public static void FoundNoValueInRequest(this ILogger logger, ModelBindingContext bindingContext)
        {
            if (!logger.IsEnabled(LogLevel.Debug))
            {
                return;
            }

            var modelMetadata = bindingContext.ModelMetadata;
            switch (modelMetadata.MetadataKind)
            {
                case ModelMetadataKind.Parameter:
                    _foundNoValueForParameterInRequest(
                        logger,
                        bindingContext.ModelName,
                        modelMetadata.ParameterName,
                        bindingContext.ModelType,
                        null);
                    break;

                case ModelMetadataKind.Property:
                    _foundNoValueForPropertyInRequest(
                        logger,
                        bindingContext.ModelName,
                        modelMetadata.ContainerType,
                        modelMetadata.PropertyName,
                        bindingContext.ModelType,
                        null);
                    break;

                case ModelMetadataKind.Type:
                    _foundNoValueInRequest(
                        logger,
                        bindingContext.ModelName,
                        bindingContext.ModelType,
                        null);
                    break;
            }
        }
    }
}