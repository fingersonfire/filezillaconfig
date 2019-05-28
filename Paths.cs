namespace FileZillaConfig {
    using System;
    using System.IO;

    internal static class Paths {
        private static readonly string Root          = AppConfig.Root;
        private static readonly string RootDownloads = AppConfig.Downloads;
        private static readonly string Users         = AppConfig.Users;

        public static string[] DownloadsDirs() {
            return Directory.GetDirectories(RootDownloads);
        }

        public static string Home(string username) {
            return Path.Combine(Users , username);
        }

        public static string UserDownloads(string username) {
            return Path.Combine(Home(username) , "Downloads");
        }

        public static string Uploads(string username) {
            return Path.Combine(Home(username) , "Uploads");
        }

        public static string Alias(string username , string dir) {
            var folder = Path.GetFileName(dir);
            var alias  = Path.Combine(UserDownloads(username) , dir);
            return Path.Combine(UserDownloads(username) , Path.GetFileName(dir));
        }

        public static void RootMkdir() {
            Directory.CreateDirectory(Root);
            Directory.CreateDirectory(RootDownloads);
            Directory.CreateDirectory(Users);
        }

        public static void UserMkdir(string username) {
            Directory.CreateDirectory(Home(username));
            Directory.CreateDirectory(Uploads(username));
            Directory.CreateDirectory(UserDownloads(username));
        }

        public static void UserRmdir(string username) {
            try { Directory.Delete(Home(username) , true); }
            catch(Exception) { }
        }
    }
}
