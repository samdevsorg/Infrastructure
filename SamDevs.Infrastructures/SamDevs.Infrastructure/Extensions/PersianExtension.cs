using System.Text;
using System.Text.RegularExpressions;

namespace SamDevs.Infrastructure.Extensions {
    public static class PersianExtension {
        public static string Uniform(this string input) {
            if (string.IsNullOrEmpty(input)) return input;
            input = Regex.Replace(input, "[\U00065269\U00065270\U00065271\U00065272\U00065273\U00065274\U00065275\U00065276]", "لا");
            return Resolve(input);
        }
        private static string Resolve(string input) {
            var sb = new StringBuilder();
            foreach (var c in input)
                sb.Append(c.Uniform());
            return sb.ToString();
        }

        public static char Uniform(this char c) {
            switch ((int)c) {
                case 65152: // ﺀ
                    return (char)1569; // ء
                case 65153: // ﺁ‎‎
                case 65154: // ﺂ‎‎
                    return (char)1570; // آ
                case 1649: // ٱ
                case 1650: // ٲ
                case 1653: // ٵ
                case 65155: // ﺃ
                case 65156: // ﺄ
                    return (char)1571; // أ
                case 1654: // ٶ
                case 65157: // ﺅ
                case 65158: // ﺆ
                    return (char)1572; // ؤ
                case 1651: // ٳ
                case 65159: // ﺇ
                case 65160: // ﺈ
                    return (char)1573; // إ
                case 1656: // ٸ
                case 1747: // ۓ
                case 64432: // ﮰ
                case 64433: // ﮱ
                case 65161: // ﺉ
                case 65162: // ﺊ
                case 65163: // ﺋ
                case 65164: // ﺌ
                    return (char)1574; // ئ
                case 65165: // ﺍ‎
                case 65166: // ﺎ‎
                    return (char)1575; // ا
                case 65167: // ﺏ
                case 65168: // ﺐ‎
                case 65169: // ﺑ‎
                case 65170: // ﺒ‎
                    return (char)1576; // ب
                case 64342: // ﭖ
                case 64343: // ﭗ
                case 64344: // ﭘ
                case 64345: // ﭙ
                    return (char)1662; // پ
                case 1731: // ۃ
                case 65171: // ﺓ
                case 65172: // ‎ﺔ
                    return (char)1577; // ة
                case 1658: // ٺ
                case 64350: // ﭞ
                case 64351: // ﭟ
                case 64352: // ﭠ
                case 64353: // ﭡ
                case 65173: // ﺕ‎
                case 65174: // ﺖ‎
                case 65175: // ﺗ‎
                case 65176: // ﺘ‎
                    return (char)1578; // ت
                case 65177: // ﺙ‎
                case 65178: // ﺚ‎
                case 65179: // ﺛ‎
                case 65180: // ﺜ‎
                    return (char)1579; // ث
                case 65181: // ﺝ
                case 65182: // ﺞ
                case 65183: // ﺟ
                case 65184: // ﺠ
                    return (char)1580; // ج
                case 64378: // ﭺ
                case 64379: // ﭻ
                case 64380: // ﭼ
                case 64381: // ﭽ
                    return (char)1670; // چ
                case 65185: // ﺡ
                case 65186: // ﺢ
                case 65187: // ﺣ
                case 65188: // ﺤ
                    return (char)1581; // ح
                case 65189: // ﺥ
                case 65190: // ﺦ
                case 65191: // ﺧ
                case 65192: // ﺨ
                    return (char)1582; // خ
                case 65193: // ﺩ
                case 65194: // ﺪ
                    return (char)1583; // د
                case 65195: // ﺫ
                case 65196: // ﺬ
                    return (char)1584; // ذ
                case 65197: // ﺭ
                case 65198: // ﺮ
                    return (char)1585; // ر
                case 65199: // ﺯ
                case 65200: // ﺰ
                    return (char)1586; // ز
                case 64394: // ﮊ
                case 64395: // ﮋ
                    return (char)1688; // ژ
                case 65201: // ﺱ
                case 65202: // ﺲ
                case 65203: // ﺳ
                case 65204: // ﺴ
                    return (char)1587; // س
                case 65205: // ﺵ
                case 65206: // ﺶ
                case 65207: // ﺷ
                case 65208: // ﺸ
                    return (char)1588; // ش
                case 65209: // ﺹ
                case 65210: // ﺺ
                case 65211: // ﺻ
                case 65212: // ﺼ
                    return (char)1589; // ص
                case 65213: // ﺽ
                case 65214: // ﺾ
                case 65215: // ﺿ
                case 65216: // ﻀ
                    return (char)1590; // ض
                case 65217: // ﻁ
                case 65218: // ﻂ
                case 65219: // ﻃ
                case 65220: // ﻄ
                    return (char)1591; // ط
                case 65221: // ﻅ
                case 65222: // ﻆ
                case 65223: // ﻇ
                case 65224: // ﻈ
                    return (char)1592; // ظ
                case 65225: // ﻉ
                case 65226: // ﻊ
                case 65227: // ﻋ
                case 65228: // ﻌ
                    return (char)1593; // ع
                case 65229: // ﻍ
                case 65230: // ﻎ
                case 65231: // ﻏ
                case 65232: // ﻐ
                    return (char)1594; // غ
                case 65233: // ﻑ
                case 65234: // ﻒ
                case 65235: // ﻓ
                case 65236: // ﻔ
                    return (char)1601; // ف
                case 65237: // ﻕ
                case 65238: // ﻖ
                case 65239: // ﻗ
                case 65240: // ﻘ
                    return (char)1602; // ق
                case 1706: // ڪ
                case 1603: // ك
                case 64398: // ﮎ
                case 64399: // ﮏ
                case 64400: // ﮐ
                case 64401: // ﮑ
                case 65241: // ﻙ
                case 65242: // ﻚ
                case 65243: // ﻛ
                case 65244: // ﻜ
                    return ((char)1705); // ک
                case 64402: // ﮒ
                case 64403: // ﮓ
                case 64404: // ﮔ
                case 64405: // ﮕ
                    return (char)1711; // گ
                case 65245: // ﻝ
                case 65246: // ﻞ
                case 65247: // ﻟ
                case 65248: // ﻠ
                    return (char)1604; // ل
                case 65249: // ﻡ
                case 65250: // ﻢ
                case 65251: // ﻣ
                case 65252: // ﻤ
                    return (char)1605; // م
                case 65253: // ﻥ
                case 65254: // ﻦ
                case 65255: // ﻧ
                case 65256: // ﻨ
                    return (char)1606; // ن
                case 1749: // ە
                case 1726: // ھ
                case 1729: // ہ
                case 64422: // ﮦ
                case 64423: // ﮧ
                case 64424: // ﮨ
                case 64425: // ﮩ
                case 64426: // ﮪ
                case 64427: // ﮫ
                case 64428: // ﮬ
                case 64429: // ﮭ
                case 65257: // ﻩ
                case 65258: // ﻪ
                case 65259: // ﻫ
                case 65260: // ﻬ
                    return (char)1607; // ه
                case 65261: // ﻭ
                case 65262: // ﻮ
                    return (char)1608; // و
                case 1568:// ؠ
                case 1659: // ٻ
                case 1609: // ى
                case 1610:// ي
                case 1744: // ې
                case 1746: // ے
                case 64338: // ﭒ
                case 64339: // ﭓ
                case 64340: // ﭔ
                case 64341: // ﭕ
                case 64430: // ﮮ
                case 64431: // ﮯ
                case 64484: // ﯤ
                case 64485: // ﯥ
                case 64486: // ﯦ
                case 64487: // ﯧ
                case 64508: // ﯼ
                case 64509: // ﯽ
                case 64510: // ﯾ
                case 64511: // ﯿ
                case 65263: // ﻯ
                case 65264: // ﻰ
                case 65265: // ﻱ
                case 65266: // ﻲ
                case 65267: // ﻳ
                case 65268: // ﻴ
                    return (char)1740; // ی
                case 1730: // ۂ
                case 64420: // ﮤ
                case 64421: // ﮥ
                    return (char)1728; // ۀ
                case 8203: // Zero Width Space
                case 8205: // Zero Width No-Break Space
                case 65279: // Zero Width Joiner
                    return (char)8204; // Zero Width Non-Joiner
            }
            return c;
        }

        public static bool IsRtlLetter(this char c) {
            
            var letters = "ﺀءﺁ‎‎ﺂ‎‎آٱٲٵﺃﺄأٶﺅﺆؤٳﺇﺈإٸۓﮰﮱﺉﺊﺋﺌئﺍ‎ﺎ‎اﺏﺐ‎ﺑ‎ﺒ‎بﭖﭗﭘﭙپۃﺓ‎ﺔةٺﭞﭟﭠﭡﺕ‎ﺖ‎ﺗ‎ﺘ‎تﺙ‎ﺚ‎ﺛ‎ﺜ‎ثﺝﺞﺟﺠجﭺﭻﭼﭽچﺡﺢﺣﺤحﺥﺦﺧﺨخﺩﺪدﺫﺬذﺭﺮرﺯﺰزﮊﮋژﺱﺲﺳﺴسﺵﺶﺷﺸشﺹﺺﺻﺼصﺽﺾﺿﻀضﻁﻂﻃﻄطﻅﻆﻇﻈظﻉﻊﻋﻌعﻍﻎﻏﻐغﻑﻒﻓﻔفﻕﻖﻗﻘقڪكﮎﮏﮐﮑﻙﻚﻛﻜکﮒﮓﮔﮕگﻝﻞﻟﻠلﻡﻢﻣﻤمﻥﻦﻧﻨنەھہﮦﮧﮨﮩﮪﮫﮬﮭﻩﻪﻫﻬهﻭﻮوؠٻىيېےﭒﭓﭔﭕﮮﮯﯤﯥﯦﯧﯼﯽﯾﯿﻯﻰﻱﻲﻳﻴیۂﮤﮥۀ";
            return letters.Contains(c.ToString());

        }
        public static string ToEnglishNumber(this string input)
        {
            var output = new StringBuilder();
            foreach (var ch in input)
            {
                if (ch >= 1632 && ch <= 1641)
                    output.Append((char)(ch - 1584));
                else if (ch >= 1776 && ch <= 1785)
                    output.Append((char)(ch - 1728));
                else
                    output.Append(ch);
            }

            return output.ToString();
        }
    }
}
