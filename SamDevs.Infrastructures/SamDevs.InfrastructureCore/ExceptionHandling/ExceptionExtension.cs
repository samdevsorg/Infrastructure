using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SamDevs.InfrastructureCore.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Text;
//TODO: Uncomment
namespace SamDevs.InfrastructureCore.ExceptionHandling
{
    public static class ExceptionExtension
    {
        private static StreamWriter _errorFile;
        private static FileStream _fs;
        private static DateTime _fileDate;
        private static readonly DirectoryInfo Dir;

        static ExceptionExtension()
        {
            _fileDate = DateTime.Today;
            //var appPath= new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
            Dir = Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "errors"));

        }

        private static void AddError(StringBuilder errors, StringBuilder totalErrors, string message, bool isAdminLevel = true)
        {
            if (errors.ToString().Contains(message)) return;
            errors.Clear();
            errors.AppendLine(message);
            //TODO: Check for Debug or Release

#if !DEBUG
                        if (isAdminLevel) {
                            if (errors.ToString() == "")
                                errors.AppendLine("خطایی رخ داده است. لطفاً لحظاتی دیگر مجددا تلاش نمایید");


                        } else {
                            errors.Clear();
                            errors.AppendLine(message);
                        }
#endif
            totalErrors.AppendLine(message);

        }

        public static string ToMessage(this Exception ex, ExceptionInfo info = null)
        {
            var errors = new StringBuilder();

            while (ex != null)
            {
                switch (ex.GetType().Name)
                {
                    //case nameof(DbEntityValidationException): // Inherited from: DataException
                    //    errors.AppendLine(GetMessage(ex as DbEntityValidationException));
                    //    break;
                    case nameof(DBConcurrencyException):
                        errors.AppendLine(GetMessage(ex as DBConcurrencyException));
                        break;
                    //case nameof(SqlException): // Inherited from: DbException
                    //    errors.AppendLine(GetMessage(ex as SqlException));
                    //    break;
                    case nameof(DbException): // Inherited from: ExternalException
                        errors.AppendLine(GetMessage(ex as DbException));
                        break;
                    //case nameof(UpdateException): // Inherited from: DataException
                    //    errors.AppendLine(GetMessage(ex as UpdateException));
                    //    break;
                    //case nameof(DbUpdateException): // Inherited from: DataException
                    //    errors.AppendLine(GetMessage(ex as DbUpdateException));
                    //    break;
                    case nameof(DataException):
                        errors.AppendLine(GetMessage(ex as DataException));
                        break;
                    case nameof(ArgumentNullException): // Inherited from: ArgumentException
                        errors.AppendLine(GetMessage(ex as ArgumentNullException));
                        break;
                    case nameof(AuthenticationException): // Inherited from: InvalidOperationException
                        errors.AppendLine(GetMessage(ex as AuthenticationException));
                        break;
                    case nameof(WebException): // Inherited from: InvalidOperationException
                        errors.AppendLine(GetMessage(ex as WebException));
                        break;
                    case nameof(InvalidOperationException):
                        errors.AppendLine(GetMessage(ex as InvalidOperationException));
                        break;
                    //case nameof(HttpException): // Inherited from: ExternalException
                    //    errors.AppendLine(GetMessage(ex as HttpException));
                    //    break;
                    case nameof(SocketException): // Inherited from: Win32Exception
                        errors.AppendLine(GetMessage(ex as SocketException));
                        break;
                    case nameof(Win32Exception): // Inherited from: ExternalException
                        errors.AppendLine(GetMessage(ex as Win32Exception));
                        break;
                    case nameof(ExternalException):
                        errors.AppendLine(GetMessage(ex as ExternalException));
                        break;
                    case nameof(HttpRequestException):
                        errors.AppendLine(GetMessage(ex as HttpRequestException));
                        break;
                    //case nameof(ModelValidationException):
                    //    errors.AppendLine(GetMessage(ex as ModelValidationException));
                    //    break;
                    default:
                        errors.AppendLine($"{ex.GetType().Name}: {ex.Message}");
                        break;
                }
                ex = ex.InnerException;
            }

            if (info != null)
            {
                _fs = new FileStream($"{Dir.FullName}/{_fileDate:yyyy-MM-dd}.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                _errorFile = new StreamWriter(_fs, Encoding.UTF8);
                if (DateTime.Today != _fileDate)
                {
                    _fileDate = DateTime.Today;
                    _errorFile.Close();
                    _fs.Close();
                    _fs = new FileStream($"errors/{_fileDate:yyyy-MM-dd}.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);

                    _errorFile = new StreamWriter(_fs, Encoding.UTF8);
                }
                info.Message = errors.ToString();
                _errorFile.WriteLine(JsonConvert.SerializeObject(info));
                _errorFile.Flush();
                _errorFile.Close();
                _fs.Close();
            }
            return errors.ToString();
        }

        public static string ToHtmlMessage(this Exception ex)
        {
            return "";

        }
        //MMZ :)
        private static string GetMessage(ArgumentNullException ex)
        {
            var errors = new StringBuilder();
            errors.AppendLine();
#if DEBUG
            errors.AppendLine($"[{ex.GetType().Name}]");
#endif
            if (ex.Message.Contains("Value cannot be null"))
            {
                errors.Clear();
                errors.AppendLine("مقادیر لازم به طور کامل پر نشده اند ");
            }
            else errors.AppendLine($"{ex.GetType().Name}: {ex.Message}");
            return errors.ToString();
        }

//        private static string GetMessage(ModelValidationException ex)
//        {
//            var errors = new StringBuilder();
//            errors.AppendLine();
//#if DEBUG
//            errors.AppendLine($"[{ex.GetType().Name}]");
//#endif
//            errors.AppendLine($"داده های ارسالی به ساختار داده ای نامعتبر می باشد");
//            return errors.ToString();
//        }

        private static string GetMessage(InvalidOperationException ex)
        {
            var errors = new StringBuilder();
            errors.AppendLine();
#if DEBUG
            errors.AppendLine($"[{ex.GetType().Name}]");
#endif
            if (ex.Message.Contains("The model backing"))
                errors.AppendLine("نسخه ی پایگاه داده با نسخه ی برنامه مطابقت ندارد.");
            //  Error during serialization or deserialization using the JSON JavaScriptSerializer. The length of the string exceeds the value set on the maxJsonLength property.
            else if (ex.Message.Contains("maxJsonLength"))
                errors.AppendLine("طول داده ی خروجی از حد مجاز تجاوز نموده است.");
            else
                errors.AppendLine($"{ex.GetType().Name}: {ex.Message}");
            return errors.ToString();
        }

//        private static string GetMessage(HttpException ex)
//        {
//            var errors = new StringBuilder();
//            errors.AppendLine();
//#if DEBUG
//            errors.AppendLine($"[{ex.GetType().Name}]");
//#endif

//            switch (ex.WebEventCode)
//            {
//                // Maximum request length exceeded
//                case 3004:
//                    errors.AppendLine("حجم درخواست از حداکثر طول مجاز تجاوز نموده است.");
//                    break;
//                default:
//                    errors.AppendLine($"{ex.WebEventCode}: {ex.Message}");
//                    break;
//            }

//            return errors.ToString();
//        }

        private static string GetMessage(HttpRequestException ex)
        {
            var errors = new StringBuilder();
            errors.AppendLine();
#if DEBUG
            errors.AppendLine($"[{ex.GetType().Name}]");
#endif

            //switch (ex.) {
            //    // Maximum request length exceeded
            //    case 3004:
            //        errors.AppendLine("حجم درخواست از حداکثر طول مجاز تجاوز نموده است.");
            //        break;
            //    default:
            //        errors.AppendLine($"{ex.WebEventCode}: {ex.Message}");
            //        break;
            //}
            errors.AppendLine("در زمان ارسال درخواست به سرور خطایی رخ داده است");
            return errors.ToString();
        }

        private static string GetMessage(SocketException ex)
        {
            var errors = new StringBuilder();
            errors.AppendLine();
#if DEBUG
            errors.AppendLine($"[{ex.GetType().Name}]");
#endif

            switch (ex.ErrorCode)
            {
                // Maximum request length exceeded
                case 10060:
                    errors.AppendLine("ارتباط با سرور، به دلیل عدم پاسخگویی سرور، وجود ندارد");
                    break;
                default:
                    errors.AppendLine($"{ex.ErrorCode}: {ex.Message}");
                    break;
            }

            return errors.ToString();
        }

        private static string GetMessage(WebException ex)
        {
            var errors = new StringBuilder();
            errors.AppendLine();
#if DEBUG
            errors.AppendLine($"[{ex.GetType().Name}]");
#endif

            switch (ex.Status)
            {
                // Maximum request length exceeded
                case WebExceptionStatus.TrustFailure:
                    errors.AppendLine("امکان برقراری ارتباط با سرور، از طریق ارتباط امن (https) وجود ندارد.");
                    break;
                case WebExceptionStatus.NameResolutionFailure:
                    errors.AppendLine($"آدرس ({ex.Message.Substring(ex.Message.IndexOf(":") + 1).Trim()}) یافت نشد");
                    break;
                default:
                    errors.AppendLine($"{ex.Status}: {ex.Message}");
                    break;
            }

            return errors.ToString();
        }
        private static string GetMessage(AuthenticationException ex)
        {
            var errors = new StringBuilder();
            errors.AppendLine();
#if DEBUG
            errors.AppendLine($"[{ex.GetType().Name}]");
#endif

            if (ex.Message.Contains("The remote certificate is invalid according to the validation procedure"))
                errors.AppendLine("امکان اعتبارسنجی ارتباط امن (https)، به دلیل منقضی شدن گواهینامه SSL/TLS، وجود ندارد.");
            else
                errors.AppendLine($"{ex.Message}");


            return errors.ToString();
        }

        private static string GetMessage(Win32Exception ex)
        {
            var errors = new StringBuilder();
            errors.AppendLine();
#if DEBUG
            errors.AppendLine($"[{ex.GetType().Name}]");
#endif

            switch (ex.ErrorCode)
            {
                //Maximum request length exceeded
                case 10060:
                    errors.AppendLine("ارتباط با سرور، به دلیل عدم پاسخگویی سرور، وجود ندارد");
                    break;
                default:
                    errors.AppendLine($"{ex.ErrorCode}: {ex.Message}");
                    break;
            }

            return errors.ToString();
        }

        private static string GetMessage(ExternalException ex)
        {
            var errors = new StringBuilder();
            errors.AppendLine();
#if DEBUG
            errors.AppendLine($"[{ex.GetType().Name}]");
#endif

            switch (ex.ErrorCode)
            {
                // Maximum request length exceeded
                case 10060:
                    errors.AppendLine("ارتباط با سرور، به دلیل عدم پاسخگویی سرور، وجود ندارد");
                    break;
                default:
                    errors.AppendLine($"{ex.ErrorCode}: {ex.Message}");
                    break;
            }

            return errors.ToString();
        }

//        private static string GetMessage(DbEntityValidationException ex)
//        {
//            var errors = new StringBuilder();
//            errors.AppendLine();
//#if DEBUG
//            errors.AppendLine($"[{ex.GetType().Name}]");
//#endif
//            foreach (var error in ex.EntityValidationErrors)
//            {
//                var entity = error.Entry.Entity;
//                if (entity == null) continue;
//                var entityName = GetEntityName(entity);
//                errors.AppendLine($"{entityName}:");
//                errors.AppendLine();
//                foreach (var validation in error.ValidationErrors)
//                {
//                    var propertyName = validation.PropertyName;
//                    var entityType = error.Entry.Entity.GetType();
//                    var propInfo = entityType.GetProperty(propertyName);
//                    var attributes = propInfo?.GetCustomAttributes(typeof(DisplayAttribute), false);
//                    var displayAttribute = attributes?.Length > 0 ? attributes[0] as DisplayAttribute : null;
//                    propertyName = displayAttribute?.Name ?? propertyName;
//                    if (displayAttribute?.ResourceType != null)
//                    {
//                        var resourceManager = new ResourceManager(displayAttribute.ResourceType);
//                        propertyName = resourceManager.GetString(propertyName) ?? propertyName;
//                    }
//                    errors.AppendLine($"{propertyName}: {validation.ErrorMessage}");
//                }
//            }
//            return errors.ToString();

//        }

        private static string GetMessage(DBConcurrencyException ex)
        {
            var errors = new StringBuilder();
            errors.AppendLine();
#if DEBUG
            errors.AppendLine($"[{ex.GetType().Name}]");
#endif
            return errors.ToString();
        }

        //        private static string GetMessage(DbException ex) {
        //            var errors = new StringBuilder();
        //            errors.AppendLine();
        //#if DEBUG
        //            errors.AppendLine($"[{ex.GetType().Name}]");
        //#endif
        //            string[] item;
        //            switch (ex.Number) {
        //                case -1:
        //                    errors.AppendLine(ExceptionResource.Sql_N1);
        //                    break;
        //                case 2:
        //                    errors.AppendLine(ExceptionResource.Sql_2);
        //                    break;
        //                case 53:
        //                    errors.AppendLine(ExceptionResource.Sql_53);
        //                    break;
        //                case 201:
        //                    item = ex.Message.Split('\'');
        //                    errors.AppendLine(string.Format(ExceptionResource.Sql_201, item[1], item[3]));
        //                    break;
        //                case 207:
        //                    item = ex.Message.Split('\'');
        //                    errors.AppendLine(String.Format(ExceptionResource.Sql_207, item[1]));
        //                    break;
        //                case 208:
        //                    item = ex.Message.Split('\'');
        //                    errors.AppendLine(String.Format(ExceptionResource.Sql_208, item[1]));
        //                    break;
        //                case 515:
        //                    item = ex.Message.Split('\'');
        //                    errors.AppendLine(String.Format(ExceptionResource.Sql_515, item[1], item[3]));
        //                    break;
        //                case 547:
        //                    item = ex.Message.Split(' ');
        //                    var parts = ex.Message.Split("'\"".ToCharArray());
        //                    switch (item[1].ToUpper()) {
        //                        case "INSERT":
        //                            errors.AppendLine(string.Format(ExceptionResource.Sql_547_Insert, parts[1], parts[5], parts[7]));
        //                            break;
        //                        case "UPDATE":
        //                            errors.AppendLine(ExceptionResource.Sql_547_Update);
        //                            break;
        //                        default:
        //                            errors.AppendLine(ExceptionResource.Sql_547_Delete);
        //                            break;
        //                    }
        //                    break;
        //                case 2627:
        //                    item = ex.Message.Split("()".ToCharArray());
        //                    errors.AppendLine(String.Format(ExceptionResource.Sql_2627, item[1]));
        //                    break;
        //                case 2812:
        //                    item = ex.Message.Split('\'');
        //                    errors.AppendLine(String.Format(ExceptionResource.Sql_2812, item[1]));
        //                    break;
        //                case 3702:
        //                    item = ex.Message.Split('"');
        //                    errors.AppendLine(String.Format(ExceptionResource.Sql_3702, item[1]));
        //                    break;
        //                case 4060:
        //                    // offline
        //                    // detach
        //                    // invalid name
        //                    errors.AppendLine(ExceptionResource.Sql_4060);
        //                    break;
        //                case 4121:
        //                    item = ex.Message.Split('"');
        //                    errors.AppendLine(String.Format(ExceptionResource.Sql_4121, item[1], item[3]));
        //                    break;
        //                case 8114:
        //                    item = ex.Message.Split(' ');
        //                    errors.AppendLine(String.Format(ExceptionResource.Sql_8114, item[4], item[6].Replace(".", "")));
        //                    break;
        //                case 8144:
        //                    item = ex.Message.Split(' ');
        //                    errors.AppendLine(String.Format(ExceptionResource.Sql_8144, item[3]));
        //                    break;
        //                case 8178:
        //                    item = ex.Message.Split('\'');
        //                    errors.AppendLine(String.Format("کوئری اجرا شده، شامل پارامتری به نام '{0}' می باشد که به آن ارسال نشده یا مقدار NULL ارسال شده است", item[3]));
        //                    break;
        //                case 18456:
        //                    item = ex.Message.Split('\'');
        //                    errors.AppendLine(String.Format(ExceptionResource.Sql_18456, item[1]));
        //                    break;
        //                default:
        //                    errors.AppendLine($"{ex.Number}: {ex.Message}");
        //                    break;
        //            }
        //            errors.AppendLine();
        //            return errors.ToString();
        //        }
        //        private static string GetMessage(UpdateException ex) {
        //            var errors = new StringBuilder();
        //            errors.AppendLine();
        //#if DEBUG
        //            errors.AppendLine($"[{ex.GetType().Name}]");
        //#endif
        //            // Unable to determine the principal end of the 'Psyco.Pishro.Model.AccountTypeCultureModel_Culture' relationship. Multiple added entities may have the same primary key.
        //            if (ex.Message.Contains("Multiple added entities may have the same primary key"))
        //                errors.AppendLine("چندین آیتم با کلید اصلی یکسان به پایگاه داده اضافه شده است");
        //            // An error occurred while updating the entries. See the inner exception for details.
        //            else if (ex.Message.Contains("An error occurred while updating the entries. See the inner exception for details"))
        //                errors.AppendLine("در زمان بروزرسانی پایگاه داده خطایی رخ داده است");
        //            else errors.AppendLine($"{ex.GetType().Name}: {ex.Message}");
        //            errors.AppendLine();

        //            foreach (var entry in ex.StateEntries) {
        //                var entity = entry.Entity;
        //                if (entity == null) continue;
        //                var entityName = GetEntityName(entity);
        //                errors.AppendLine($"({entityName} دچار مشکل شده است)");
        //            }
        //            errors.AppendLine();

        //            return errors.ToString();
        //        }

        //        private static string GetMessage(DbUpdateException ex) {
        //            var errors = new StringBuilder();
        //            errors.AppendLine();
        //#if DEBUG
        //            errors.AppendLine($"[{ex.GetType().Name}]");
        //#endif
        //            // Unable to determine the principal end of the 'Psyco.Pishro.Model.AccountTypeCultureModel_Culture' relationship. Multiple added entities may have the same primary key.
        //            if (ex.Message.Contains("Multiple added entities may have the same primary key"))
        //                errors.AppendLine("چندین آیتم با کلید اصلی یکسان به پایگاه داده اضافه شده است");
        //            // An error occurred while updating the entries. See the inner exception for details.
        //            else if (ex.Message.Contains("An error occurred while updating the entries. See the inner exception for details"))
        //                errors.AppendLine("در زمان بروزرسانی پایگاه داده خطایی رخ داده است");
        //            else errors.AppendLine(ex.Message);

        //            errors.AppendLine();
        //            foreach (var entry in ex.Entries) {
        //                var entity = entry.Entity;
        //                if (entity == null) continue;
        //                var entityName = GetEntityName(entity);
        //                errors.AppendLine($"({entityName} دچار مشکل شده است)");
        //                foreach (var error in entry.ValidationErrors) {
        //                    errors.AppendLine($"{error.PropertyName}: {error.ErrorMessage}");
        //                }
        //            }
        //            errors.AppendLine();

        //            return errors.ToString();
        //        }
        private static string GetMessage(DbException ex)
        {
            var errors = new StringBuilder();
            errors.AppendLine();
#if DEBUG
            errors.AppendLine($"[{ex.GetType().Name}]");
#endif
            switch (ex.ErrorCode)
            {
                default:
                    errors.AppendLine($"{ex.GetType().Name}({ex.ErrorCode}): {ex.Message}");
                    break;
            }
            errors.AppendLine();
            return errors.ToString();

        }
        private static string GetMessage(DataException ex)
        {
            // An exception occurred while initializing the database. See the InnerException for details.
            var errors = new StringBuilder();
            errors.AppendLine();
#if DEBUG
            errors.AppendLine($"[{ex.GetType().Name}]");
#endif
            if (ex.Message.Contains("An exception occurred while initializing the database"))
                errors.AppendLine("در زمان مقداردهی اولیه به پایگاه داده خطایی رخ داده است");
            else
                errors.AppendLine($"{ex.GetType().Name}: {ex.Message}");
            errors.AppendLine();
            return errors.ToString();

        }
        private static string GetEntityName(object entity)
        {
            var entityType = entity.GetType();
            var attributes = entityType.GetCustomAttributes(typeof(DisplayNameForClassAttribute), false);
            var displayAttribute = attributes.Length > 0 ? attributes[0] as DisplayNameForClassAttribute : null;
            var entityName = displayAttribute?.DisplayName ?? entityType.Name;
            return entityName;
        }

    }
}
