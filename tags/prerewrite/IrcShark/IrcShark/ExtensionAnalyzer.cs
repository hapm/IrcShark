using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Reflection;
using System.IO;
using IrcShark.Extensions;

namespace IrcShark
{
    /// <summary>
    /// Analyze a .NET dll to find exntesions in it.
    /// </summary>
    class ExtensionAnalyzer
    {
        private List<ExtensionInfo> ExtensionsValue;

        public ExtensionAnalyzer(FileInfo fileToAnalyze)
        {
            String AsmName;
            String TypeName;
            AppDomain AnalyzerDomain;
            ExtensionInfoBuilder extBuilder;
            ExtensionsValue = new List<ExtensionInfo>();
            AnalyzerDomain = CreateAnalyzerDomain();
            AsmName = GetType().Assembly.FullName;
            TypeName = typeof(ExtensionInfoBuilder).FullName;
            extBuilder = (ExtensionInfoBuilder)AnalyzerDomain.CreateInstanceAndUnwrap(AsmName, TypeName, false, BindingFlags.CreateInstance, null, new Object[] { fileToAnalyze.FullName }, null, null, null);
            foreach (ExtensionInfo p in extBuilder.Extensions)
                ExtensionsValue.Add(new ExtensionInfo(p));
            AppDomain.Unload(AnalyzerDomain);
        }

        private AppDomain CreateAnalyzerDomain()
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
            //fiop.AllLocalFiles = FileIOPermissionAccess.AllAccess
            //fiop.AllFiles = FileIOPermissionAccess.AllAccess
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

        /// <summary>
        /// A list of extensions found in the given .NET dll.
        /// </summary>
        public ExtensionInfo[] Extensions
        {
            get { return ExtensionsValue.ToArray(); }
        }
    }
}
