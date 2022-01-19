using System.Text;

namespace KairosConsoleTest
{
    public static class Extensionss
    {
        //private static readonly string[] thous = { "100", "2000000", "300000", "400000000", "500000000000", "1000000000000000" };
        //public static string ReplaceAt(this long value)
        //{

        //    int pow = 0;
        //    string powStr = "";
        //    int log = (int)Math.Log(value, 10);
        //    pow = (int)Math.Pow(10, log);
        //    powStr = thous[log];
        //    return powStr;
        //}
        public static string Fill(string value, int length, string with)
        {

            StringBuilder result = new(length);
            result.Append(value);
            result.Append(Fill(Math.Max(0, length - value.Length), with));

            return result.ToString();
        }

        public static string Fill(int length, string with)
        {
            StringBuilder sb = new(length);
            while (sb.Length < length)
            {
                sb.Append(with);
            }
            return sb.ToString();
        }
    }
}
