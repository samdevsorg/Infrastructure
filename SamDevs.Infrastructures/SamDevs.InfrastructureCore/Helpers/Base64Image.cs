using System;
using System.IO;
using System.Text.RegularExpressions;
using SamDevs.InfrastructureCore.Enums;
using SamDevs.InfrastructureCore.Utilities;

namespace SamDevs.InfrastructureCore.Helpers
{
    public class Base64Image
    {
        public string Data { get; }

        public string MimeType { get; }
        public long Size { get; }
        public ImageType ImageType { get; set; }
        public Base64Image(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                if (Regex.IsMatch(data, @"data:[a-z0-9]+/[a-z0-9]+;base64,\s*.+", RegexOptions.IgnoreCase))
                {
                    Data = data.Substring(data.IndexOf("base64,") + 7).Trim();
                    MimeType = data.Substring(5, data.IndexOf(";") - 5);

                    //"image/jpeg"
                    //"image/png"

                    ImageType = MimeType.Equals("image/png") ? ImageType.png : ImageType.jpg;
                }
                else
                {
                    Data = data.Trim();
                    MimeType = FileUtility.GetMimeType(".jpg");
                    ImageType = ImageType.jpg;
                }

                Size = data.Length;
            }

        }

        public static implicit operator Base64Image(string input)
        {
            var image = new Base64Image(input);
            return image;
        }

        public override string ToString()
        {
            return Data;
        }

        public string ToDataUrl()
        {
            return Data == null ? null : $"data:{MimeType};base64, {Data}";
        }

        public static Base64Image FromFile(string path)
        {
            var bytes = File.ReadAllBytes(path);
            return $"data:{FileUtility.GetMimeType(Path.GetExtension(path))};base64, {Convert.ToBase64String(bytes)}";
        }
    }
}
