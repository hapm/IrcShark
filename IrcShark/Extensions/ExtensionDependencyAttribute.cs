/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 16.10.2009
 * Zeit: 17:06
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace IrcShark.Extensions
{
    using System;

    /// <summary>
    /// Describes the level of the dependency.
    /// </summary>
    public enum DependencyLevel 
    {
        /// <summary>
        /// If the Extension needs the other Extension to run, 
        /// it is a strict dependency .
        /// </summary>
        Strict,
        
        /// <summary>
        /// If the dependency can be used but didn't need to be used
        /// it is an optional dependency.
        /// </summary>
        /// <remarks>
        /// Use this only if you don't have direct references to the 
        /// assembly of the other extension.
        /// </remarks>
        Optional
    }
    
    /// <summary>
    /// The ExtensionDependencyAttribute allows to define dependencies of extensions.
    /// </summary>
    /// <remarks>
    /// By applying this Attribute to an Extension class, you can define an dependency
    /// to another Extension type. You only need the full type name of the other Extension
    /// class. If the dependency should be strict (your Extension does not run without the 
    /// other Extension, use the DependencyLevel.Strict else use DependencyLevel.Optional.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ExtensionDependencyAttribute : Attribute
    {
        /// <summary>
        /// Saves the type name.
        /// </summary>
        private string typeName;
        
        /// <summary>
        /// Saves the level.
        /// </summary>
        private DependencyLevel level;
        
        /// <summary>
        /// Initializes a new instance of the ExtensionDependencyAttribute class with the given type name and level.
        /// </summary>
        /// <remarks>
        /// The typename isn't checked but will be compare to the real typenames of existing extensions.
        /// </remarks>
        /// <param name="fullTypeName">The full type name of the Extension, your Extension depends on.</param>
        /// <param name="level">The level of the dependency.</param>
        public ExtensionDependencyAttribute(string fullTypeName, DependencyLevel level)
        {
            typeName = fullTypeName;
            this.level = level;
        }
        
        /// <summary>
        /// Gets the full type name of the Extension depended on by the Extension this Attribute was applied to.
        /// </summary>
        /// <value>The full type name as a string.</value>
        public string TypeName 
        {
            get { return typeName; }
        }
        
        /// <summary>
        /// Gets the dependency level of this dependency.
        /// </summary>
        /// <value>The dependency level.</value>
        public DependencyLevel Level 
        {
            get { return level; }
        }
    }
}
