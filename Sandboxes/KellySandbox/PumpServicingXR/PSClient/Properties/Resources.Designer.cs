﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PSClient.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PSClient.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The DataBase directory is invalid..
        /// </summary>
        internal static string ApplicationException_DBDirectoryInvalid {
            get {
                return ResourceManager.GetString("ApplicationException_DBDirectoryInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is no DataBase..
        /// </summary>
        internal static string ApplicationException_NoDB {
            get {
                return ResourceManager.GetString("ApplicationException_NoDB", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error has occurred..
        /// </summary>
        internal static string Error {
            get {
                return ResourceManager.GetString("Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The DataBase is incompatible..
        /// </summary>
        internal static string IncompatibleDatabaseError {
            get {
                return ResourceManager.GetString("IncompatibleDatabaseError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Sync Manager is checking for local modifications..
        /// </summary>
        internal static string SyncManager_CheckingForLocalModification {
            get {
                return ResourceManager.GetString("SyncManager_CheckingForLocalModification", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Sync Manager is checking for updates..
        /// </summary>
        internal static string SyncManager_CheckingForUpdates {
            get {
                return ResourceManager.GetString("SyncManager_CheckingForUpdates", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Sync Manager is resolving deleted data..
        /// </summary>
        internal static string SyncManager_ResolvingDeletedData {
            get {
                return ResourceManager.GetString("SyncManager_ResolvingDeletedData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Sync Manager is updating the configuration..
        /// </summary>
        internal static string SyncManager_UpdatingConfiguration {
            get {
                return ResourceManager.GetString("SyncManager_UpdatingConfiguration", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Sync Manager upload is complete..
        /// </summary>
        internal static string SyncManager_UploadComplete {
            get {
                return ResourceManager.GetString("SyncManager_UploadComplete", resourceCulture);
            }
        }
    }
}
