﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MangaDownloader.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class CommonSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static CommonSettings defaultInstance = ((CommonSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new CommonSettings())));
        
        public static CommonSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool AutoCreateShortcut {
            get {
                return ((bool)(this["AutoCreateShortcut"]));
            }
            set {
                this["AutoCreateShortcut"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool AutoCreatePdf {
            get {
                return ((bool)(this["AutoCreatePdf"]));
            }
            set {
                this["AutoCreatePdf"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool AutoCreateZip {
            get {
                return ((bool)(this["AutoCreateZip"]));
            }
            set {
                this["AutoCreateZip"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool AutoUpdate {
            get {
                return ((bool)(this["AutoUpdate"]));
            }
            set {
                this["AutoUpdate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool AutoClean {
            get {
                return ((bool)(this["AutoClean"]));
            }
            set {
                this["AutoClean"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3")]
        public int TotalConcurrentWorkers {
            get {
                return ((int)(this["TotalConcurrentWorkers"]));
            }
            set {
                this["TotalConcurrentWorkers"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string RootDownloadFolderPath {
            get {
                return ((string)(this["RootDownloadFolderPath"]));
            }
            set {
                this["RootDownloadFolderPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TurnOffWhenDone {
            get {
                return ((bool)(this["TurnOffWhenDone"]));
            }
            set {
                this["TurnOffWhenDone"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowTaskbarInfoOnMinimize {
            get {
                return ((bool)(this["ShowTaskbarInfoOnMinimize"]));
            }
            set {
                this["ShowTaskbarInfoOnMinimize"] = value;
            }
        }
    }
}
