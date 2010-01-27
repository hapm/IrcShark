/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 28.09.2009
 * Zeit: 21:31
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace IrcShark
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Security;
    using System.Security.Permissions;
    using System.Security.Policy;

    using IrcShark.Extensions;

    /// <summary>
    ///  Analyze a .NET dll to find extensions in it.
    /// </summary>
    /// <remarks>
    /// The analyzer uses a sperated AppDomain to be able to unload the assembly after
    /// analyzing it. The AppDomain has very low permissions and uses reflection only load.
    /// </remarks>
    public class ExtensionAnalyzer
    {
        /// <summary>
        /// Saves all analyzed ExtensionInfos.
        /// </summary>
        private List<ExtensionInfo> extensions;

        /// <summary>
        /// Initializes a new instance of the ExtensionAnalyzer class for the given dll file.
        /// </summary>
        /// <param name="fileToAnalyze">The path to the file to analyze.</param>
        public ExtensionAnalyzer(string fileToAnalyze/*, string[] recoveryPathes*/)
        {
            string asmName;
            string typeName;
            AppDomain domain;
            ExtensionInfoBuilder extBuilder;
            extensions = new List<ExtensionInfo>();
            domain = CreateAnalyzerDomain(/*recoveryPathes*/);
            asmName = GetType().Assembly.FullName;
            typeName = typeof(ExtensionInfoBuilder).FullName;
            extBuilder = (ExtensionInfoBuilder)domain.CreateInstanceAndUnwrap(asmName, typeName, false, BindingFlags.CreateInstance, null, new object[] { fileToAnalyze }, null, null, null);
            foreach (ExtensionInfo p in extBuilder.Extensions)
                extensions.Add(p);
            AppDomain.Unload(domain);
        }

        /// <summary>
        /// Gets list of extensions found in the given .NET dll.
        /// </summary>
        /// <value>
        /// An array of all ExtensionInfo instances.
        /// </value>
        public ExtensionInfo[] Extensions
        {
            get { return extensions.ToArray(); }
        }

        /// <summary>
        /// Creates an AppDomain for analyzation purpose.
        /// </summary>
        /// <returns>
        /// The created AppDomain.
        /// </returns>
        /// <remarks>
        /// The created AppDomain has very low permissions and is unloaded after analyzation is done.
        /// </remarks>
        private static AppDomain CreateAnalyzerDomain(/*string[] recoveryPathes*/)
        {
            AppDomainSetup ads = new AppDomainSetup();
            AppDomain result;
            PermissionSet perms;
            ads.ApplicationBase = Environment.CurrentDirectory;
            ads.ShadowCopyDirectories = "shadow";
            ads.ShadowCopyFiles = "shadow";
            ads.DisallowCodeDownload = true;

            perms = new PermissionSet(PermissionState.None);
            FileIOPermission fiop = new FileIOPermission(PermissionState.Unrestricted);
            perms.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            perms.AddPermission(new SecurityPermission(SecurityPermissionFlag.AllFlags));
            fiop.AddPathList(FileIOPermissionAccess.PathDiscovery, Environment.CurrentDirectory);
            fiop.AddPathList(FileIOPermissionAccess.Read, Environment.CurrentDirectory);
            fiop.AddPathList(FileIOPermissionAccess.PathDiscovery, Environment.CurrentDirectory + "Extensions\\");
            fiop.AddPathList(FileIOPermissionAccess.Read, Environment.CurrentDirectory + "Extensions\\");
            fiop.AllLocalFiles = FileIOPermissionAccess.AllAccess;
            fiop.AllFiles = FileIOPermissionAccess.AllAccess;
            perms.AddPermission(fiop);
            perms.AddPermission(new UIPermission(UIPermissionWindow.AllWindows, UIPermissionClipboard.OwnClipboard));
            perms.AddPermission(new ReflectionPermission(PermissionState.Unrestricted));

            PolicyLevel policy = PolicyLevel.CreateAppDomainLevel();
            policy.RootCodeGroup.PolicyStatement = new PolicyStatement(perms);

            // create the Domain
            result = AppDomain.CreateDomain("analyzer", null, ads);
            result.SetAppDomainPolicy(policy);
            return result;
        }
    }
}
