namespace ReceiverApp.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Configuration;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0"), CompilerGenerated]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = ((Settings) SettingsBase.Synchronized(new Settings()));

        private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
        {
        }

        private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
        {
        }

        [DefaultSettingValue("ControlText"), DebuggerNonUserCode, UserScopedSetting]
        public Color Color_Default
        {
            get
            {
                return (Color) this["Color_Default"];
            }
            set
            {
                this["Color_Default"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("Red")]
        public Color Color_Error
        {
            get
            {
                return (Color) this["Color_Error"];
            }
            set
            {
                this["Color_Error"] = value;
            }
        }

        [DebuggerNonUserCode, DefaultSettingValue("LimeGreen"), UserScopedSetting]
        public Color Color_Success
        {
            get
            {
                return (Color) this["Color_Success"];
            }
            set
            {
                this["Color_Success"] = value;
            }
        }

        [DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("255, 128, 0")]
        public Color Color_TagMsg_Maintenance
        {
            get
            {
                return (Color) this["Color_TagMsg_Maintenance"];
            }
            set
            {
                this["Color_TagMsg_Maintenance"] = value;
            }
        }

        [UserScopedSetting, DefaultSettingValue("Blue"), DebuggerNonUserCode]
        public Color Color_TagMsg_Movement
        {
            get
            {
                return (Color) this["Color_TagMsg_Movement"];
            }
            set
            {
                this["Color_TagMsg_Movement"] = value;
            }
        }

        [DefaultSettingValue("0, 192, 192"), UserScopedSetting, DebuggerNonUserCode]
        public Color Color_TagMsg_Near_Activator
        {
            get
            {
                return (Color) this["Color_TagMsg_Near_Activator"];
            }
            set
            {
                this["Color_TagMsg_Near_Activator"] = value;
            }
        }

        [DefaultSettingValue("192, 0, 0"), DebuggerNonUserCode, UserScopedSetting]
        public Color Color_TagMsg_Torn_Wire
        {
            get
            {
                return (Color) this["Color_TagMsg_Torn_Wire"];
            }
            set
            {
                this["Color_TagMsg_Torn_Wire"] = value;
            }
        }

        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        [ApplicationScopedSetting, DefaultSettingValue(@"Data Source=BLING\SQLEXPRESS;Initial Catalog=PureRF;Integrated Security=True"), DebuggerNonUserCode, SpecialSetting(SpecialSetting.ConnectionString)]
        public string PureRFConnectionString
        {
            get
            {
                return (string) this["PureRFConnectionString"];
            }
        }
    }
}

