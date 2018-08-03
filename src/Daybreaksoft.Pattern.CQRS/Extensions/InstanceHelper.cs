using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Daybreaksoft.Pattern.CQRS.Extensions
{
    public static class InstanceHelper
    {
        public static object CreateInstance(Type type)
        {
            // Try to get constructors
            var ctor = type.GetConstructors().FirstOrDefault(p => !p.IsStatic);
            if (ctor != null)
            {
                // Create aggregate instance
                var instance = ctor.Invoke(GetDefaultValues(ctor));

                return instance;
            }

            throw new Exception($"Cannot found any public constructor in {type.FullName}.");
        }

        private static object[] GetDefaultValues(ConstructorInfo ctor)
        {
            var parameters = ctor.GetParameters();

            var values = new List<object>();

            foreach (var parameter in parameters)
            {
#if !NetStandard13
                if (!parameter.ParameterType.IsValueType || parameter.ParameterType.IsGenericType)
                {
                    values.Add(null);
                }
#else
                if (!parameter.ParameterType.GetTypeInfo().IsValueType)
                {
                    values.Add(null);
                }
#endif
                else
                {
                    if (parameter.ParameterType == typeof(Int16) ||
                        parameter.ParameterType == typeof(Int32) ||
                        parameter.ParameterType == typeof(Int64) ||
                        parameter.ParameterType == typeof(Single) ||
                        parameter.ParameterType == typeof(Double))
                    {
                        values.Add(0);
                    }
                    else if (parameter.ParameterType == typeof(Decimal))
                    {
                        values.Add(Decimal.MinValue);
                    }
                    else if (parameter.ParameterType == typeof(Boolean))
                    {
                        values.Add(false);
                    }
                    else if (parameter.ParameterType == typeof(DateTime))
                    {
                        values.Add(DateTime.MinValue);
                    }
                    else
                    {
                        throw new Exception($"Not support to set default value for {parameter.ParameterType}.");
                    }
                }
            }

            return values.ToArray();
        }
    }
}
