﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TagScanner.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ID3 Media Library Files|*.id3lib|ID3 Media Library Interchange Files|*.id3libx|Al" +
            "l Files (*.*)|*.*")]
        public string LibraryFilter {
            get {
                return ((string)(this["LibraryFilter"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"Media Files (*.avi;*.mp4;*.wmv;*.m4a;*.mp3;*.wma;*.bmp;*.gif;*.jpeg;*.jpg;*.png)|*.avi;*.mp4;*.wmv;*.m4a;*.mp3;*.wma;*.bmp;*.gif;*.jpeg;*.jpg;*.png|AV Files (*.avi;*.mp4;*.wmv;*.m4a;*.mp3;*.wma)|*.avi;*.mp4;*.wmv;*.m4a;*.mp3;*.wma|Video Files (*.avi;*.mp4;*.wmv)|*.avi;*.mp4;*.wmv|Audio Files (*.m4a;*.mp3;*.wma)|*.m4a;*.mp3;*.wma|Image Files (*.bmp;*.gif;*.jpeg;*.jpg;*.png)|*.bmp;*.gif;*.jpeg;*.jpg;*.png|All Files (*.*)|*.*")]
        public string MediaFilter {
            get {
                return ((string)(this["MediaFilter"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Playlists|*.wpl|All Files (*.*)|*.*")]
        public string PlayerFilter {
            get {
                return ((string)(this["PlayerFilter"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ID3 Media Library Search Filters|*.id3filter|All Files (*.*)|*.*")]
        public string SearchFilter {
            get {
                return ((string)(this["SearchFilter"]));
            }
        }
    }
}
