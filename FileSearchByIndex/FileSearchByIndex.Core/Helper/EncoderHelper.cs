using System.Text;

namespace FileSearchByIndex.Core.Helper
{
    public static class EncoderHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length">Only supports 16/32 md5, default is 32</param>
        /// <returns></returns>
        public static string ToMD5(this string str, int length = 32) =>
            Encoding.UTF8.GetBytes(str).ToMD5(length);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length">Only support 16/32 md5, default is 32</param>
        /// <returns></returns>
        public static string ToMD5(this byte[] bytes, int length = 32)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            var md5bt = md5.ComputeHash(bytes);
            if (length == 32)
                return BitConverter.ToString(md5bt).Replace("-", "");
            if (length == 16)
                return BitConverter.ToString(md5bt, 4, 8);
            throw new ArgumentException("Only support 16/32 md5, default is 32");
        }
    }
}
