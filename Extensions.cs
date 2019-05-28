namespace FileZillaConfig {
    using System;
    using System.IO;
    using FileZillaConfig.Poco;

    internal static class Extensions {
        public static bool IsEmpty(this string str) {
            return String.IsNullOrWhiteSpace(str);
        }
        public static bool IsNotEmpty(this string str) {
            return ! str.IsEmpty();
        }

        public static void Save(this Config config) {
            XFactory.XConfig(config).Save();
        }

        public static void Save(this XConfig xconfig) {
            xconfig.XDoc.Save(xconfig.Filename);
        }
    }
}
