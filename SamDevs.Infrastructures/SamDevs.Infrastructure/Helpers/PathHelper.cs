using System;
using System.IO;
using System.Web;

namespace SamDevs.Infrastructure.Helpers
{
    public class PathHelper
    {
        public static string GetBasePath()
        {
            return HttpContext.Current == null ? AppDomain.CurrentDomain.BaseDirectory : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
        }
    }
}
