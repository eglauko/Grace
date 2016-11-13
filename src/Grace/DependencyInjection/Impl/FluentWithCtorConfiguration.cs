﻿using System;

namespace Grace.DependencyInjection.Impl
{
    /// <summary>
    /// Constructor parameter configuration
    /// </summary>
    /// <typeparam name="TParam"></typeparam>
    public class FluentWithCtorConfiguration<TParam> : ProxyFluentExportStrategyConfiguration,
        IFluentWithCtorConfiguration<TParam>
    {
        private readonly ConstructorParameterInfo _constructorParameterInfo;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="constructorParameterInfo"></param>
        public FluentWithCtorConfiguration(IFluentExportStrategyConfiguration strategy, ConstructorParameterInfo constructorParameterInfo) : base(strategy)
        {
            _constructorParameterInfo = constructorParameterInfo;
        }

        /// <summary>
        /// Name of the parameter being configured
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IFluentWithCtorConfiguration<TParam> Named(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            _constructorParameterInfo.ParameterName = name;

            return this;
        }

        /// <summary>
        /// Applies a filter to be used when resolving a parameter constructor
        /// It will be called each time the parameter is resolved
        /// </summary>
        /// <param name="filter">filter delegate to be used when resolving parameter</param>
        /// <returns>configuration object</returns>
        public IFluentWithCtorConfiguration<TParam> Consider(ExportStrategyFilter filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            _constructorParameterInfo.ExportStrategyFilter = filter;

            return this;
        }

        /// <summary>
        /// Assign a default value if no better option is found
        /// </summary>
        /// <param name="defaultValue">default value</param>
        /// <returns>configuration object</returns>
        public IFluentWithCtorConfiguration<TParam> DefaultValue(TParam defaultValue)
        {
            _constructorParameterInfo.DefaultValue = defaultValue;

            return this;
        }

        /// <summary>
        /// Default value func
        /// </summary>
        /// <param name="defaultValueFunc"></param>
        /// <returns></returns>
        public IFluentWithCtorConfiguration<TParam> DefaultValue(Func<TParam> defaultValueFunc)
        {
            _constructorParameterInfo.DefaultValue = defaultValueFunc;

            return this;
        }

        /// <summary>
        /// Default value func
        /// </summary>
        /// <param name="defaultValueFunc"></param>
        /// <returns></returns>
        public IFluentWithCtorConfiguration<TParam> DefaultValue(Func<IExportLocatorScope, StaticInjectionContext, IInjectionContext, TParam> defaultValueFunc)
        {
            _constructorParameterInfo.DefaultValue = defaultValueFunc;

            return this;
        }

        /// <summary>
        /// Is the parameter required when resolving the type
        /// </summary>
        /// <param name="isRequired">is the parameter required</param>
        /// <returns>configuration object</returns>
        public IFluentWithCtorConfiguration<TParam> IsRequired(bool isRequired = true)
        {
            _constructorParameterInfo.IsRequired = isRequired;

            return this;
        }

        /// <summary>
        /// Locate with a particular key
        /// </summary>
        /// <param name="locateKey">ocate key</param>
        /// <returns>configuration object</returns>
        public IFluentWithCtorConfiguration<TParam> LocateWithKey(object locateKey)
        {
            if (locateKey == null) throw new ArgumentNullException(nameof(locateKey));

            _constructorParameterInfo.LocateWithKey = locateKey;

            return this;
        }

        /// <summary>
        /// Mark the parameter as dynamic
        /// </summary>
        /// <param name="isDynamic"></param>
        /// <returns>configuration object</returns>
        public IFluentWithCtorConfiguration<TParam> IsDynamic(bool isDynamic = true)
        {
            _constructorParameterInfo.IsDynamic = true;

            return this;
        }
    }

    /// <summary>
    /// Configuration object for constructor parameter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TParam"></typeparam>
    public class FluentWithCtorConfiguration<T, TParam> : FluentExportStrategyConfiguration<T>, IFluentWithCtorConfiguration<T, TParam>
    {
        private readonly ConstructorParameterInfo _constructorParameterInfo;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="exportConfiguration"></param>
        /// <param name="exportFunc"></param>
        public FluentWithCtorConfiguration(ICompiledExportStrategy exportConfiguration, Func<IExportLocatorScope, StaticInjectionContext, IInjectionContext, TParam> exportFunc) : base(exportConfiguration)
        {
            exportConfiguration.ConstructorParameter(_constructorParameterInfo = new ConstructorParameterInfo(exportFunc) { ParameterType = typeof(TParam) });
        }


        /// <summary>
        /// Applies a filter to be used when resolving a parameter constructor
        /// It will be called each time the parameter is resolved
        /// </summary>
        /// <param name="filter">filter delegate to be used when resolving parameter</param>
        /// <returns>configuration object</returns>
        public IFluentWithCtorConfiguration<T, TParam> Consider(ExportStrategyFilter filter)
        {
            _constructorParameterInfo.ExportStrategyFilter = filter;

            return this;
        }

        /// <summary>
        /// Assign a default value if no better option is found
        /// </summary>
        /// <param name="defaultValue">default value</param>
        /// <returns>configuration object</returns>
        public IFluentWithCtorConfiguration<T, TParam> DefaultValue(TParam defaultValue)
        {
            _constructorParameterInfo.DefaultValue = defaultValue;

            return this;
        }

        /// <summary>
        /// Default value func
        /// </summary>
        /// <param name="defaultValueFunc"></param>
        /// <returns></returns>
        public IFluentWithCtorConfiguration<T, TParam> DefaultValue(Func<TParam> defaultValueFunc)
        {
            return DefaultValue((locator, staticContext, provider) => defaultValueFunc());
        }

        /// <summary>
        /// Default value func
        /// </summary>
        /// <param name="defaultValueFunc"></param>
        /// <returns></returns>
        public IFluentWithCtorConfiguration<T, TParam> DefaultValue(Func<IExportLocatorScope, StaticInjectionContext, IInjectionContext, TParam> defaultValueFunc)
        {
            _constructorParameterInfo.DefaultValue = defaultValueFunc;

            return this;
        }

        /// <summary>
        /// Mark the parameter as dynamic (will be located from child scopes)
        /// </summary>
        /// <param name="isDynamic"></param>
        /// <returns></returns>
        public IFluentWithCtorConfiguration<T, TParam> IsDynamic(bool isDynamic = true)
        {
            _constructorParameterInfo.IsDynamic = isDynamic;

            return this;
        }

        /// <summary>
        /// Is the parameter required when resolving the type
        /// </summary>
        /// <param name="isRequired">is the parameter required</param>
        /// <returns>configuration object</returns>
        public IFluentWithCtorConfiguration<T, TParam> IsRequired(bool isRequired = true)
        {
            _constructorParameterInfo.IsRequired = isRequired;

            return this;
        }

        /// <summary>
        /// Locate with a particular key
        /// </summary>
        /// <param name="locateKey">ocate key</param>
        /// <returns>configuration object</returns>
        public IFluentWithCtorConfiguration<T, TParam> LocateWithKey(object locateKey)
        {
            _constructorParameterInfo.LocateWithKey = locateKey;

            return this;
        }

        /// <summary>
        /// Name of the parameter being configured
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IFluentWithCtorConfiguration<T, TParam> Named(string name)
        {
            _constructorParameterInfo.ParameterName = name;

            return this;
        }
    }
}
