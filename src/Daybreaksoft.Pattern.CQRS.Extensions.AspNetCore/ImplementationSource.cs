using System.Reflection;

namespace Daybreaksoft.Pattern.CQRS.Extensions
{
    /// <summary>
    /// Specifies the assemblies and namespace which the implementation of the interface is executed.
    /// </summary>
    public class ImplementationSource
    {
        public ImplementationSource(Assembly[] assemblies, string[] underNamespaces = null)
        {
            Assemblies = assemblies;

            UnderNamespaces = underNamespaces;
        }

        /// <summary>
        /// Specifies the assemblies which the implementation of the interface is executed.
        /// </summary>
        public Assembly[] Assemblies { get; set; }

        /// <summary>
        /// Specifies the namespaces which the implementation of the interface is executed.
        /// </summary>
        public string[] UnderNamespaces { get; set; }
    }
}
