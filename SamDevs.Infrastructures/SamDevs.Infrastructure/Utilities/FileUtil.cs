﻿namespace SamDevs.Infrastructure.Utilities {
    public class FileUtil {
        public static string GetMimeType(string extension) {
            extension = extension.TrimStart('.').ToLower();
            switch (extension) {
                case "jpg":
                case "jpeg":
                case "jpe":
                    return "image/jpeg";

                case "png":
                    return "image/png";
                case "gif":
                    return "image/gif";
                default:
                    return "";
            }
        }
    }
}
