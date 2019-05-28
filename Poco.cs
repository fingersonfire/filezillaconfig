namespace FileZillaConfig.Poco {
    using System.Collections.Generic;
    using System.Xml.Linq;

    internal class BaseConfig {
        public string Filename { get; set; }

        public BaseConfig() {
            Filename = System.IO.Path.Combine(AppConfig.Root , "FileZilla Server.xml");
        }
    }

    internal class XConfig : BaseConfig {
        public XDocument XDoc { get; set; }

        public XConfig() : base() { }
    }

    internal class Config : BaseConfig {
        public Settings     Settings { get; set; }
        public List<Group>  Groups   { get; set; }
        public List<User>   Users    { get; set; }

        public Config() : base() {
            Groups   = new List<Group>();
            Users    = new List<User>();
        }
    }

    internal class Settings {
        public int      ActiveIgnoreLocal      {get; set;}
        public string   AdminIPAddresses       {get; set;}
        public string   AdminIPBindings        {get; set;}
        public string   AdminPassword          {get; set;}
        public int      AdminPort              {get; set;}
        public int      AllowExplicitSSL       {get; set;}
        public int      AllowIncomingFXP       {get; set;}
        public int      AllowOutgoingFXP       {get; set;}
        public int      AllowSharedWrite       {get; set;}
        public int      AutobanAttempts        {get; set;}
        public int      AutobanEnable          {get; set;}
        public int      AutobanTime            {get; set;}
        public int      AutobanType            {get; set;}
        public int      BufferSize             {get; set;}
        public string   CustomPASVIP           {get; set;}
        public string   CustomPASVIPServer     {get; set;}
        public int      CustomPASVIPtype       {get; set;}
        public int      CustomPASVMaxPort      {get; set;}
        public int      CustomPASVMinPort      {get; set;}
        public int      DisableIPv6            {get; set;}
        public int      DownloadSpeedlimit     {get; set;}
        public int      DownloadSpeedlimitType {get; set;}
        public int      EnableHASH             {get; set;}
        public int      EnableLogging          {get; set;}
        public int      EnableSSL              {get; set;}
        public int      ForceExplicitSSL       {get; set;}
        public int      ForcePROTP             {get; set;}
        public int      GSSPromptForPassword   {get; set;}
        public int      HideWelcomeMessage     {get; set;}
        public string   IPBindings             {get; set;}
        public string   IPFilterAllowed        {get; set;}
        public string   IPFilterDisallowed     {get; set;}
        public string   ImplicitSSLPorts       {get; set;}
        public string   InitialWelcomeMessage  {get; set;}
        public int      LogfileDeleteTime      {get; set;}
        public int      LogfileType            {get; set;}
        public int      LoginTimeout           {get; set;}
        public int      LogsizeLimit           {get; set;}
        public int      MaximumUserCount       {get; set;}
        public int      ModeZAllowLocal        {get; set;}
        public string   ModeZDisallowedIPs     {get; set;}
        public int      ModeZMaxLevel          {get; set;}
        public int      ModeZMinLevel          {get; set;}
        public int      ModeZUse               {get; set;}
        public int      NetworkBufferSize      {get; set;}
        public int      NoExternalIPOnLocal    {get; set;}
        public int      NoStrictInFXP          {get; set;}
        public int      NoStrictOutFXP         {get; set;}
        public int      NoTransferTimeout      {get; set;}
        public int      NumberOfThreads        {get; set;}
        public string   SSLCertificateFile     {get; set;}
        public string   SSLKeyFile             {get; set;}
        public string   SSLKeyPassword         {get; set;}
        public string   Serverports            {get; set;}
        public string   ServiceDisplayName     {get; set;}
        public string   ServiceName            {get; set;}
        public int      ShowPassInLog          {get; set;}
        public int      Timeout                {get; set;}
        public int      UploadSpeedlimit       {get; set;}
        public int      UploadSpeedlimitType   {get; set;}
        public int      UseCustomPASVPorts     {get; set;}
        public int      UseGSSSupport          {get; set;}

        //<SpeedLimits>
        //  <Download />
        //  <Upload />
        //</SpeedLimits>
    }

    internal class Group {
        // what goes in here?
    }

    internal class User {
        public string           Name        { get; set; }
        public UserOption       Option      { get; set; }

        //<IpFilter>
        //  <Disallowed />
        //  <Allowed />
        //</IpFilter>

        public List<Permission> Permissions { get; set; }

        //<SpeedLimits DlType="0" DlLimit="10" ServerDlLimitBypass="0" UlType="0" UlLimit="10" ServerUlLimitBypass="0">
        //  <Download />
        //  <Upload />
        //</SpeedLimits>

        public User() {
            Permissions = new List<Permission>();
        }

        public override string ToString() {
            return Name;
        }
    }

    internal class UserOption {
        public string Pass                  { get; set; } // MD5 hash - generate from User.Password
        public string Group                 { get; set; }
        public string BypassServerUserlimit { get; set; }
        public string UserLimit             { get; set; }
        public string IPLimit               { get; set; }
        public string Enabled               { get; set; }
        public string Comments              { get; set; }
        public string ForceSsl              { get; set; }
    }

    internal class Permission {
        public List<string> Aliases { get; set; }
        public string Dir           { get; set; }
        public bool FileRead        { get; set; }
        public bool FileWrite       { get; set; }
        public bool FileDelete      { get; set; }
        public bool FileAppend      { get; set; }
        public bool DirCreate       { get; set; }
        public bool DirDelete       { get; set; }
        public bool DirList         { get; set; }
        public bool DirSubdirs      { get; set; }
        public bool IsHome          { get; set; }
        public bool AutoCreate      { get; set; }

        public Permission() {
            Aliases = new List<string>();
        }

        public override string ToString() {
            return Dir;
        }
    }
}
