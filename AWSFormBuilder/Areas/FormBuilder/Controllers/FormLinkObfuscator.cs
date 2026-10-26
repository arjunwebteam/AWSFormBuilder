using System;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    // ✅ ADDED — turns a numeric FormId into an opaque, URL-safe token (and back) so the
    // Publish modal's Link/Embed/Share URLs don't show sequential form IDs.
    // NOTE: this is obfuscation, not real encryption — it stops casual ID-guessing/enumeration,
    // it does not replace [Authorize] or any other access control.
    public static class FormLinkObfuscator
    {
        // Any fixed 64-bit value works here — change it if you ever want old shared links to stop resolving.
        private const long ObfuscationKey = 0x5A3C9E17B2F48D61;

        public static string EncryptFormId(Int64 id)
        {
            long xored = id ^ ObfuscationKey;
            byte[] bytes = BitConverter.GetBytes(xored);
            string token = Convert.ToBase64String(bytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');
            return token;
        }

        public static Int64 DecryptFormId(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Empty token");

            string base64 = token.Replace('-', '+').Replace('_', '/');
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            byte[] bytes = Convert.FromBase64String(base64);
            long xored = BitConverter.ToInt64(bytes, 0);
            return xored ^ ObfuscationKey;
        }
    }
}
