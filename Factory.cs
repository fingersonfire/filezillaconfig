namespace FileZillaConfig {
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using FileZillaConfig.Poco;

    internal static class Factory {
        public static Config Config() {
            var xconfig = XFactory.XConfig();
            var config  = Config(xconfig);
            return config;
        }

        public static Config Config(XConfig xconfig) {
            var xdoc        = xconfig.XDoc.Element("FileZillaServer");
            var config      = new Config();
            config.Settings = _Settings(xdoc.Element("Settings"));
            config.Groups   = _Groups(xdoc.Element("Groups"));
            config.Users    = _Users(xdoc.Element("Users"));
            return config;
        }

        public static User User(string name) {
            return _DefaultUser(name);
        }

        public static Permission Permission(string name , string dir) {
            var permission = new Permission();
            permission.Dir = dir;
            permission.Aliases.Add(Paths.Alias(name , dir));
            permission.FileRead   = true;
            permission.DirList    = true;
            permission.DirSubdirs = true;
            return permission;
        }

        #region XML to POCO
        private static Settings _Settings(XElement xsettings) {
            var settings = new Settings();
            var xitems   = xsettings.Elements("Item");

            foreach(var xitem in xitems) {
                var attribute = xitem.Attribute("name").Value;
                var innertext = xitem.Value;

                switch(attribute) {
                    case "Active ignore local"      : settings.ActiveIgnoreLocal       = innertext.Numeric(); break;
                    case "Admin IP Addresses"       : settings.AdminIPAddresses        = innertext;           break;
                    case "Admin IP Bindings"        : settings.AdminIPBindings         = innertext;           break;
                    case "Admin Password"           : settings.AdminPassword           = innertext;           break;
                    case "Admin port"               : settings.AdminPort               = innertext.Numeric(); break;
                    case "Allow Incoming FXP"       : settings.AllowIncomingFXP        = innertext.Numeric(); break;
                    case "Allow explicit SSL"       : settings.AllowExplicitSSL        = innertext.Numeric(); break;
                    case "Allow outgoing FXP"       : settings.AllowOutgoingFXP        = innertext.Numeric(); break;
                    case "Allow shared write"       : settings.AllowSharedWrite        = innertext.Numeric(); break;
                    case "Autoban attempts"         : settings.AutobanAttempts         = innertext.Numeric(); break;
                    case "Autoban enable"           : settings.AutobanEnable           = innertext.Numeric(); break;
                    case "Autoban time"             : settings.AutobanTime             = innertext.Numeric(); break;
                    case "Autoban type"             : settings.AutobanType             = innertext.Numeric(); break;
                    case "Buffer Size"              : settings.BufferSize              = innertext.Numeric(); break;
                    case "Custom PASV IP server"    : settings.CustomPASVIPServer      = innertext;           break;
                    case "Custom PASV IP type"      : settings.CustomPASVIPtype        = innertext.Numeric(); break;
                    case "Custom PASV IP"           : settings.CustomPASVIP            = innertext;           break;
                    case "Custom PASV max port"     : settings.CustomPASVMaxPort       = innertext.Numeric(); break;
                    case "Custom PASV min port"     : settings.CustomPASVMinPort       = innertext.Numeric(); break;
                    case "Disable IPv6"             : settings.DisableIPv6             = innertext.Numeric(); break;
                    case "Download Speedlimit Type" : settings.DownloadSpeedlimitType  = innertext.Numeric(); break;
                    case "Download Speedlimit"      : settings.DownloadSpeedlimit      = innertext.Numeric(); break;
                    case "Enable HASH"              : settings.EnableHASH              = innertext.Numeric(); break;
                    case "Enable SSL"               : settings.EnableSSL               = innertext.Numeric(); break;
                    case "Enable logging"           : settings.EnableLogging           = innertext.Numeric(); break;
                    case "Force PROT P"             : settings.ForcePROTP              = innertext.Numeric(); break;
                    case "Force explicit SSL"       : settings.ForceExplicitSSL        = innertext.Numeric(); break;
                    case "GSS Prompt for Password"  : settings.GSSPromptForPassword    = innertext.Numeric(); break;
                    case "Hide Welcome Message"     : settings.HideWelcomeMessage      = innertext.Numeric(); break;
                    case "IP Bindings"              : settings.IPBindings              = innertext;           break;
                    case "IP Filter Allowed"        : settings.IPFilterAllowed         = innertext;           break;
                    case "IP Filter Disallowed"     : settings.IPFilterDisallowed      = innertext;           break;
                    case "Implicit SSL ports"       : settings.ImplicitSSLPorts        = innertext;           break;
                    case "Initial Welcome Message"  : settings.InitialWelcomeMessage   = innertext;           break;
                    case "Logfile delete time"      : settings.LogfileDeleteTime       = innertext.Numeric(); break;
                    case "Logfile type"             : settings.LogfileType             = innertext.Numeric(); break;
                    case "Login Timeout"            : settings.LoginTimeout            = innertext.Numeric(); break;
                    case "Logsize limit"            : settings.LogsizeLimit            = innertext.Numeric(); break;
                    case "Maximum user count"       : settings.MaximumUserCount        = innertext.Numeric(); break;
                    case "Mode Z Use"               : settings.ModeZUse                = innertext.Numeric(); break;
                    case "Mode Z allow local"       : settings.ModeZAllowLocal         = innertext.Numeric(); break;
                    case "Mode Z disallowed IPs"    : settings.ModeZDisallowedIPs      = innertext;           break;
                    case "Mode Z max level"         : settings.ModeZMaxLevel           = innertext.Numeric(); break;
                    case "Mode Z min level"         : settings.ModeZMinLevel           = innertext.Numeric(); break;
                    case "Network Buffer Size"      : settings.NetworkBufferSize       = innertext.Numeric(); break;
                    case "No External IP On Local"  : settings.NoExternalIPOnLocal     = innertext.Numeric(); break;
                    case "No Strict In FXP"         : settings.NoStrictInFXP           = innertext.Numeric(); break;
                    case "No Strict Out FXP"        : settings.NoStrictOutFXP          = innertext.Numeric(); break;
                    case "No Transfer Timeout"      : settings.NoTransferTimeout       = innertext.Numeric(); break;
                    case "Number of Threads"        : settings.NumberOfThreads         = innertext.Numeric(); break;
                    case "SSL Certificate file"     : settings.SSLCertificateFile      = innertext;           break;
                    case "SSL Key Password"         : settings.SSLKeyPassword          = innertext;           break;
                    case "SSL Key file"             : settings.SSLKeyFile              = innertext;           break;
                    case "Serverports"              : settings.Serverports             = innertext;           break;
                    case "Service display name"     : settings.ServiceDisplayName      = innertext;           break;
                    case "Service name"             : settings.ServiceName             = innertext;           break;
                    case "Show Pass in Log"         : settings.ShowPassInLog           = innertext.Numeric(); break;
                    case "Timeout"                  : settings.Timeout                 = innertext.Numeric(); break;
                    case "Upload Speedlimit Type"   : settings.UploadSpeedlimitType    = innertext.Numeric(); break;
                    case "Upload Speedlimit"        : settings.UploadSpeedlimit        = innertext.Numeric(); break;
                    case "Use GSS Support"          : settings.UseGSSSupport           = innertext.Numeric(); break;
                    case "Use custom PASV ports"    : settings.UseCustomPASVPorts      = innertext.Numeric(); break;
                }
            }

            return settings;
        }
        private static int Numeric(this string str) {
            int val = 0;
            Int32.TryParse(str , out val);
            return val;
        }

        private static List<Group> _Groups(XElement xgroups) {
            var groups = new List<Group>();
            // what to do here?
            return groups;
        }

        private static List<User> _Users(XElement xusers) {
            var users = new List<User>();
            foreach(var xuser in xusers.Elements("User")) {
                var user = _User(xuser);
                if(user != null) users.Add(user);
            }
            return users;
        }
        private static User _User(XElement xuser) {
            var user         = new User();
            user.Name        = xuser.Attribute("Name").Value;
            user.Option      = _UserOption(xuser);
            user.Permissions = _Permissions(xuser.Element("Permissions"));
            return user;
        }

        private static UserOption _UserOption(XElement xuser) {
            var option   = new UserOption();
            var xoptions = xuser.Elements("Option");

            foreach(var xoption in xoptions) {
                var attribute = xoption.Attribute("Name").Value;
                var innertext = xoption.Value;
                switch(attribute) {
                    case "Pass"                   : option.Pass                  = innertext; break;
                    case "Group"                  : option.Group                 = innertext; break;
                    case "Bypass server userlimit": option.BypassServerUserlimit = innertext; break;
                    case "User Limit"             : option.UserLimit             = innertext; break;
                    case "IP Limit"               : option.IPLimit               = innertext; break;
                    case "Enabled"                : option.Enabled               = innertext; break;
                    case "Comments"               : option.Comments              = innertext; break;
                    case "ForceSsl"               : option.ForceSsl              = innertext; break;
                }
            }

            return option;
        }

        private static List<Permission> _Permissions(XElement xpermissions){
            var permissions  = new List<Permission>();
            foreach(var xpermission in xpermissions.Elements("Permission")) {
                var permission = _Permission(xpermission);
                if(permission != null) permissions.Add(permission);
            }
            return permissions;
        }
        private static Permission _Permission(XElement xpermission) {
            var permission = new Permission();
            permission.Dir = xpermission.Attribute("Dir").Value;

            var xoptions = xpermission.Elements("Option");
            foreach(var xoption in xoptions) {
                var attribute = xoption.Attribute("Name").Value;
                var innertext = xoption.Value;
                switch(attribute) {
                    case "FileRead"   : permission.FileRead   = innertext.Bool(); break;
                    case "FileWrite"  : permission.FileWrite  = innertext.Bool(); break;
                    case "FileDelete" : permission.FileDelete = innertext.Bool(); break;
                    case "FileAppend" : permission.FileAppend = innertext.Bool(); break;
                    case "DirCreate"  : permission.DirCreate  = innertext.Bool(); break;
                    case "DirDelete"  : permission.DirDelete  = innertext.Bool(); break;
                    case "DirList"    : permission.DirList    = innertext.Bool(); break;
                    case "DirSubdirs" : permission.DirSubdirs = innertext.Bool(); break;
                    case "IsHome"     : permission.IsHome     = innertext.Bool(); break;
                    case "AutoCreate" : permission.AutoCreate = innertext.Bool(); break;
                }
            }

            var xaliases = xpermission.Element("Aliases");
            if(xaliases != null) {
                foreach(var xalias in xaliases.Elements("Alias")) {
                    var alias = xalias.Value.Trim();
                    if(alias.IsNotEmpty()) permission.Aliases.Add(alias);
                }
            }

            return permission;
        }
        private static bool Bool(this string str) {
            var b = str=="1";
            return b;
        }
        #endregion

        #region Default POCO
        private static User _DefaultUser(string name) {
            var user         = new User();
            user.Name        = name;
            user.Option      = _DefaultOption();
            user.Permissions = _DefaultPermissions(name);
            return user;
        }

        private static UserOption _DefaultOption() {
            var option = new UserOption();
            option.Pass                  = "";
            option.Group                 = "";
            option.BypassServerUserlimit = "0";
            option.UserLimit             = "0";
            option.IPLimit               = "0";
            option.Enabled               = "1";
            option.Comments              = "";
            option.ForceSsl              = "0";
            return option;
        }

        private static List<Permission> _DefaultPermissions(string name) {
            var home        = new Permission();
            home.Dir        = Paths.Home(name);
            home.FileRead   = true;
            home.DirList    = true;
            home.DirSubdirs = true;
            home.IsHome     = true;

            var uploads        = new Permission();
            uploads.Dir        = Paths.Uploads(name);
            uploads.FileRead   = true;
            uploads.FileWrite  = true;
            uploads.FileDelete = true;
            uploads.FileAppend = true;
            uploads.DirCreate  = true;
            uploads.DirDelete  = true;
            uploads.DirList    = true;
            uploads.DirSubdirs = true;

            var list = new List<Permission>();
            list.Add(home);
            list.Add(uploads);

            return list;
        }
        #endregion
    }
}
