namespace FileZillaConfig {
    using System.IO;
    using CM = System.Configuration.ConfigurationManager;

    internal static class AppConfig {
        public static readonly string Root      = CM.AppSettings["Root"]      ?? Path.GetFullPath(@"C:\FileZilla");
        public static readonly string Downloads = CM.AppSettings["Downloads"] ?? Path.Combine(Root , "Downloads");
        public static readonly string Users     = CM.AppSettings["Users"]     ?? Path.Combine(Root , "Users");
    }
}
