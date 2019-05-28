namespace FileZillaConfig {
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml.Linq;
    using FileZillaConfig.Poco;

    internal static class XFactory {
        #region Template
        private const string TEMPLATE = @"
<FileZillaServer>
    <Settings>
        <Item name='Active ignore local'      type='numeric' >1</Item>
        <Item name='Admin IP Addresses'       type='string' />
        <Item name='Admin IP Bindings'        type='string' />
        <Item name='Admin Password'           type='string' />
        <Item name='Admin port'               type='numeric' >14147</Item>
        <Item name='Allow Incoming FXP'       type='numeric' >0</Item>
        <Item name='Allow explicit SSL'       type='numeric' >1</Item>
        <Item name='Allow outgoing FXP'       type='numeric' >0</Item>
        <Item name='Allow shared write'       type='numeric' >0</Item>
        <Item name='Autoban attempts'         type='numeric' >10</Item>
        <Item name='Autoban enable'           type='numeric' >0</Item>
        <Item name='Autoban time'             type='numeric' >1</Item>
        <Item name='Autoban type'             type='numeric' >0</Item>
        <Item name='Buffer Size'              type='numeric' >32768</Item>
        <Item name='Custom PASV IP server'    type='string'  >http://ip.filezilla-project.org/ip.php</Item>
        <Item name='Custom PASV IP type'      type='numeric' >0</Item>
        <Item name='Custom PASV IP'           type='string' />
        <Item name='Custom PASV max port'     type='numeric' >0</Item>
        <Item name='Custom PASV min port'     type='numeric' >0</Item>
        <Item name='Disable IPv6'             type='numeric' >0</Item>
        <Item name='Download Speedlimit Type' type='numeric' >0</Item>
        <Item name='Download Speedlimit'      type='numeric' >10</Item>
        <Item name='Enable HASH'              type='numeric' >0</Item>
        <Item name='Enable SSL'               type='numeric' >0</Item>
        <Item name='Enable logging'           type='numeric' >1</Item>
        <Item name='Force PROT P'             type='numeric' >0</Item>
        <Item name='Force explicit SSL'       type='numeric' >0</Item>
        <Item name='GSS Prompt for Password'  type='numeric' >0</Item>
        <Item name='Hide Welcome Message'     type='numeric' >0</Item>
        <Item name='IP Bindings'              type='string'  >*</Item>
        <Item name='IP Filter Allowed'        type='string' />
        <Item name='IP Filter Disallowed'     type='string' />
        <Item name='Implicit SSL ports'       type='string'  >990</Item>
        <Item name='Initial Welcome Message'  type='string' />
        <Item name='Logfile delete time'      type='numeric' >14</Item>
        <Item name='Logfile type'             type='numeric' >1</Item>
        <Item name='Login Timeout'            type='numeric' >60</Item>
        <Item name='Logsize limit'            type='numeric' >0</Item>
        <Item name='Maximum user count'       type='numeric' >0</Item>
        <Item name='Mode Z Use'               type='numeric' >0</Item>
        <Item name='Mode Z allow local'       type='numeric' >0</Item>
        <Item name='Mode Z disallowed IPs'    type='string' />
        <Item name='Mode Z max level'         type='numeric' >9</Item>
        <Item name='Mode Z min level'         type='numeric' >1</Item>
        <Item name='Network Buffer Size'      type='numeric' >65536</Item>
        <Item name='No External IP On Local'  type='numeric' >1</Item>
        <Item name='No Strict In FXP'         type='numeric' >0</Item>
        <Item name='No Strict Out FXP'        type='numeric' >0</Item>
        <Item name='No Transfer Timeout'      type='numeric' >600</Item>
        <Item name='Number of Threads'        type='numeric' >2</Item>
        <Item name='SSL Certificate file'     type='string' />
        <Item name='SSL Key Password'         type='string' />
        <Item name='SSL Key file'             type='string' />
        <Item name='Serverports'              type='string'  >21</Item>
        <Item name='Service display name'     type='string' />
        <Item name='Service name'             type='string' />
        <Item name='Show Pass in Log'         type='numeric' >0</Item>
        <Item name='Timeout'                  type='numeric' >120</Item>
        <Item name='Upload Speedlimit Type'   type='numeric' >0</Item>
        <Item name='Upload Speedlimit'        type='numeric' >10</Item>
        <Item name='Use GSS Support'          type='numeric' >0</Item>
        <Item name='Use custom PASV ports'    type='numeric' >0</Item>
        <SpeedLimits>
            <Download />
            <Upload />
        </SpeedLimits>
    </Settings>
    <Groups />
    <Users />
</FileZillaServer>
";
        #endregion

        public static readonly string Template = TEMPLATE;

        public static XConfig XConfig() {
            var xconfig  = new XConfig();
            var filename = xconfig.Filename;
            xconfig.XDoc = File.Exists(filename) ? XDocument.Load(filename) : XDocument.Parse(Template);
            return xconfig;
        }

        public static XConfig XConfig(Config config) {
            return new XConfig() { XDoc = XDoc(config) };
        }

        public static string MD5Hash(string input) {
            var md5  = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb   = new StringBuilder();
            foreach(var b in hash) {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        #region Private
        private static XDocument XDoc(Config config) {
            var xconfig = new XElement("FileZillaServer");
            xconfig.Add(XSettings(config.Settings));
            xconfig.Add(XGroups(config.Groups));
            xconfig.Add(XUsers(config.Users));

            return new XDocument(xconfig);
        }

        private static XElement XSettings(Settings settings) {
            var xsettings = new XElement("Settings");

            xsettings.Add(XItem("Active ignore local"      , "numeric" , settings.ActiveIgnoreLocal      ));
            xsettings.Add(XItem("Admin IP Addresses"       , "string"  , settings.AdminIPAddresses       ));
            xsettings.Add(XItem("Admin IP Bindings"        , "string"  , settings.AdminIPBindings        ));
            xsettings.Add(XItem("Admin Password"           , "string"  , settings.AdminPassword          ));
            xsettings.Add(XItem("Admin port"               , "numeric" , settings.AdminPort              ));
            xsettings.Add(XItem("Allow Incoming FXP"       , "numeric" , settings.AllowIncomingFXP       ));
            xsettings.Add(XItem("Allow explicit SSL"       , "numeric" , settings.AllowExplicitSSL       ));
            xsettings.Add(XItem("Allow outgoing FXP"       , "numeric" , settings.AllowOutgoingFXP       ));
            xsettings.Add(XItem("Allow shared write"       , "numeric" , settings.AllowSharedWrite       ));
            xsettings.Add(XItem("Autoban attempts"         , "numeric" , settings.AutobanAttempts        ));
            xsettings.Add(XItem("Autoban enable"           , "numeric" , settings.AutobanEnable          ));
            xsettings.Add(XItem("Autoban time"             , "numeric" , settings.AutobanTime            ));
            xsettings.Add(XItem("Autoban type"             , "numeric" , settings.AutobanType            ));
            xsettings.Add(XItem("Buffer Size"              , "numeric" , settings.BufferSize             ));
            xsettings.Add(XItem("Custom PASV IP server"    , "string"  , settings.CustomPASVIPServer     ));
            xsettings.Add(XItem("Custom PASV IP type"      , "numeric" , settings.CustomPASVIPtype       ));
            xsettings.Add(XItem("Custom PASV IP"           , "string"  , settings.CustomPASVIP           ));
            xsettings.Add(XItem("Custom PASV max port"     , "numeric" , settings.CustomPASVMaxPort      ));
            xsettings.Add(XItem("Custom PASV min port"     , "numeric" , settings.CustomPASVMinPort      ));
            xsettings.Add(XItem("Disable IPv6"             , "numeric" , settings.DisableIPv6            ));
            xsettings.Add(XItem("Download Speedlimit Type" , "numeric" , settings.DownloadSpeedlimitType ));
            xsettings.Add(XItem("Download Speedlimit"      , "numeric" , settings.DownloadSpeedlimit     ));
            xsettings.Add(XItem("Enable HASH"              , "numeric" , settings.EnableHASH             ));
            xsettings.Add(XItem("Enable SSL"               , "numeric" , settings.EnableSSL              ));
            xsettings.Add(XItem("Enable logging"           , "numeric" , settings.EnableLogging          ));
            xsettings.Add(XItem("Force PROT P"             , "numeric" , settings.ForcePROTP             ));
            xsettings.Add(XItem("Force explicit SSL"       , "numeric" , settings.ForceExplicitSSL       ));
            xsettings.Add(XItem("GSS Prompt for Password"  , "numeric" , settings.GSSPromptForPassword   ));
            xsettings.Add(XItem("Hide Welcome Message"     , "numeric" , settings.HideWelcomeMessage     ));
            xsettings.Add(XItem("IP Bindings"              , "string"  , settings.IPBindings             ));
            xsettings.Add(XItem("IP Filter Allowed"        , "string"  , settings.IPFilterAllowed        ));
            xsettings.Add(XItem("IP Filter Disallowed"     , "string"  , settings.IPFilterDisallowed     ));
            xsettings.Add(XItem("Implicit SSL ports"       , "string"  , settings.ImplicitSSLPorts       ));
            xsettings.Add(XItem("Initial Welcome Message"  , "string"  , settings.InitialWelcomeMessage  ));
            xsettings.Add(XItem("Logfile delete time"      , "numeric" , settings.LogfileDeleteTime      ));
            xsettings.Add(XItem("Logfile type"             , "numeric" , settings.LogfileType            ));
            xsettings.Add(XItem("Login Timeout"            , "numeric" , settings.LoginTimeout           ));
            xsettings.Add(XItem("Logsize limit"            , "numeric" , settings.LogsizeLimit           ));
            xsettings.Add(XItem("Maximum user count"       , "numeric" , settings.MaximumUserCount       ));
            xsettings.Add(XItem("Mode Z Use"               , "numeric" , settings.ModeZUse               ));
            xsettings.Add(XItem("Mode Z allow local"       , "numeric" , settings.ModeZAllowLocal        ));
            xsettings.Add(XItem("Mode Z disallowed IPs"    , "string"  , settings.ModeZDisallowedIPs     ));
            xsettings.Add(XItem("Mode Z max level"         , "numeric" , settings.ModeZMaxLevel          ));
            xsettings.Add(XItem("Mode Z min level"         , "numeric" , settings.ModeZMinLevel          ));
            xsettings.Add(XItem("Network Buffer Size"      , "numeric" , settings.NetworkBufferSize      ));
            xsettings.Add(XItem("No External IP On Local"  , "numeric" , settings.NoExternalIPOnLocal    ));
            xsettings.Add(XItem("No Strict In FXP"         , "numeric" , settings.NoStrictInFXP          ));
            xsettings.Add(XItem("No Strict Out FXP"        , "numeric" , settings.NoStrictOutFXP         ));
            xsettings.Add(XItem("No Transfer Timeout"      , "numeric" , settings.NoTransferTimeout      ));
            xsettings.Add(XItem("Number of Threads"        , "numeric" , settings.NumberOfThreads        ));
            xsettings.Add(XItem("SSL Certificate file"     , "string"  , settings.SSLCertificateFile     ));
            xsettings.Add(XItem("SSL Key Password"         , "string"  , settings.SSLKeyPassword         ));
            xsettings.Add(XItem("SSL Key file"             , "string"  , settings.SSLKeyFile             ));
            xsettings.Add(XItem("Serverports"              , "string"  , settings.Serverports            ));
            xsettings.Add(XItem("Service display name"     , "string"  , settings.ServiceDisplayName     ));
            xsettings.Add(XItem("Service name"             , "string"  , settings.ServiceName            ));
            xsettings.Add(XItem("Show Pass in Log"         , "numeric" , settings.ShowPassInLog          ));
            xsettings.Add(XItem("Timeout"                  , "numeric" , settings.Timeout                ));
            xsettings.Add(XItem("Upload Speedlimit Type"   , "numeric" , settings.UploadSpeedlimitType   ));
            xsettings.Add(XItem("Upload Speedlimit"        , "numeric" , settings.UploadSpeedlimit       ));
            xsettings.Add(XItem("Use GSS Support"          , "numeric" , settings.UseGSSSupport          ));
            xsettings.Add(XItem("Use custom PASV ports"    , "numeric" , settings.UseCustomPASVPorts     ));

            var SpeedLimits = new XElement("SpeedLimits" , new XElement("Disallowed") , new XElement("Allowed") );
            xsettings.Add(SpeedLimits);

            return xsettings;
        }
        private static XElement XItem(string name , string type , object content) {
            return new XElement("Item" , new XAttribute("name" , name) , new XAttribute("type" , type) , content);
        }

        private static XElement XGroups(IEnumerable<Group> groups) {
            return new XElement("Groups");
        }

        private static XElement XUsers(IEnumerable<User> users) {
            var xusers = new XElement("Users");
            foreach(var user in users) {
                var xuser = XUser(user);
                xusers.Add(xuser);
            }
            return xusers;
        }

        private static XElement XUser(User user) {
            var xuser = new XElement( "User" , new XAttribute("Name" , user.Name));

            // Options
            xuser.Add(XOption("Name" , "Pass"                    , user.Option.Pass                 ));
            xuser.Add(XOption("Name" , "Group"                   , user.Option.Group                ));
            xuser.Add(XOption("Name" , "User Limit"              , user.Option.UserLimit            ));
            xuser.Add(XOption("Name" , "IP Limit"                , user.Option.IPLimit              ));
            xuser.Add(XOption("Name" , "Enabled"                 , user.Option.Enabled              ));
            xuser.Add(XOption("Name" , "Comments"                , user.Option.Comments             ));
            xuser.Add(XOption("Name" , "ForceSsl"                , user.Option.ForceSsl             ));
            xuser.Add(XOption("Name" , "Bypass server userlimit" , user.Option.BypassServerUserlimit));

            // IpFilter - not used at the moment
            xuser.Add(new XElement("IpFilter" , new XElement("Disallowed") , new XElement("Allowed")));

            // Permissions
            xuser.Add(XPermissions(user.Permissions));

            // SpeedLimits- not used at the moment
            xuser.Add(XUserSpeedLimits());

            return xuser;
        }

        private static XElement XPermissions(IEnumerable<Permission> permissions) {
            var xpermissions = new XElement("Permissions");
            foreach(var permission in permissions) {
                xpermissions.Add(XPermission(permission));
            }
            return xpermissions;
        }

        private static XElement XPermission(Permission permission) {
            var xpermission = new XElement("Permission" , new XAttribute("Dir" , permission.Dir));

            // Aliases
            if(permission.Aliases.Count > 0) {
                xpermission.Add(XAliases(permission.Aliases));
            }

            // Options
            xpermission.Add(XOption("Name" , "FileRead"   , permission.FileRead  .Binary()));
            xpermission.Add(XOption("Name" , "FileWrite"  , permission.FileWrite .Binary()));
            xpermission.Add(XOption("Name" , "FileDelete" , permission.FileDelete.Binary()));
            xpermission.Add(XOption("Name" , "FileAppend" , permission.FileAppend.Binary()));
            xpermission.Add(XOption("Name" , "DirCreate"  , permission.DirCreate .Binary()));
            xpermission.Add(XOption("Name" , "DirDelete"  , permission.DirDelete .Binary()));
            xpermission.Add(XOption("Name" , "DirList"    , permission.DirList   .Binary()));
            xpermission.Add(XOption("Name" , "DirSubdirs" , permission.DirSubdirs.Binary()));
            xpermission.Add(XOption("Name" , "IsHome"     , permission.IsHome    .Binary()));
            xpermission.Add(XOption("Name" , "AutoCreate" , permission.AutoCreate.Binary()));

            return xpermission;
        }
        private static string Binary(this bool b) {
            var binary = b ? "1" : "0";
            return binary;
        }

        private static XElement XAliases(IEnumerable<string> aliases) {
            var xaliases = new XElement("Aliases");
            foreach(var alias in aliases) {
                var xalias = new XElement("Alias" , alias);
                xaliases.Add(xalias);
            }
            return xaliases;
        }

        private static XElement XUserSpeedLimits() {
            var ret = new XElement
                ("SpeedLimits"
                , XAttribute("DlType"              , "0" )
                , XAttribute("DlLimit"             , "10")
                , XAttribute("ServerDlLimitBypass" , "0" )
                , XAttribute("UlType"              , "0" )
                , XAttribute("UlLimit"             , "10")
                , XAttribute("ServerUlLimitBypass" , "0" )
                , new XElement("Disallowed")
                , new XElement("Allowed")
                );
            return ret;
        }

        private static XElement XOption(string attrTag , string attrVal , string content) {
            return new XElement("Option" , new XAttribute(attrTag , attrVal) , content);
        }

        private static XAttribute XAttribute(string tag , string val) {
            return new XAttribute(tag , val);
        }
        #endregion
    }
}
