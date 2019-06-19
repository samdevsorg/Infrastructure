using SamDevs.Infrastructure.Utilities;

namespace SamDevs.Infrastructure.Helpers
{
    public class Base64Image
    {
        public string Data { get; set; }
        public static implicit operator Base64Image(string input)
        {
            return new Base64Image
            {
                Data = input.Substring(input.IndexOf(" ") + 1)
            };
        }

        public string ToDataUrl(string extension = ".jpg")
        {
            return $"data:{FileUtil.GetMimeType(extension)};base64, {Data}";
        }
    }
}
