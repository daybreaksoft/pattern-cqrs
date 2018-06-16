using System.Reflection;

namespace Daybreaksoft.Pattern.CQRS.Extensions.AspNetCore
{
    /// <summary>
    /// Specifies the assemblies and namespace which the implementation of the interface is executed.
    /// </summary>
    public class ImplementationSource
    {
        public ImplementationSource(Assembly assembly, string underNamespace = "")
        {
            Assembly = assembly;

            UnderNamespace = underNamespace;
        }

        /// <summary>
        /// Specifies the assemblies which the implementation of the interface is executed.
        /// </summary>
        public Assembly Assembly { get; set; }

        /// <summary>
        /// Specifies the namespaces which the implementation of the interface is executed.
        /// </summary>
        public string UnderNamespace { get; set; }
    }
}
