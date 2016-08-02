using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using OtpSharp;

namespace KeeOtp
{
    /// <summary>
    /// Class that serializes and deserializes data into the Strings for the entry
    /// </summary>
    public class OtpAuthData
    {
        public const string StringDictionaryKey = "otp";

        const string keyParameter = "key";
        const string typeParameter = "type";
        const string stepParameter = "step";
        const string sizeParameter = "size";
        const string counterParameter = "counter";
        const string otpHashModeParameter = "otpHashMode";

        public Key Key { get; set; }
        public OtpType Type { get; set; }

        public OtpHashMode OtpHashMode { get; set; }

        public int Step { get; set; }
        public int Size { get; set; }

        public int Counter { get; set; }

        public OtpAuthData()
        {
            this.OtpHashMode = OtpHashMode.Sha1;
        }

        public static OtpAuthData FromString(string data)
        {
            NameValueCollection parameters = ParseQueryString(data);

            if (parameters[keyParameter] == null)
                throw new ArgumentException("Must have a key in the data");

            var otpData = new OtpAuthData();

            otpData.Key = ProtectedKey.CreateProtectedKeyAndDestroyPlaintextKey(Base32.Decode(parameters[keyParameter]));

            if (parameters[typeParameter] != null)
                otpData.Type = (OtpType)Enum.Parse(typeof(OtpType), parameters[typeParameter]);

            if (parameters[otpHashModeParameter] != null)
                otpData.OtpHashMode = (OtpHashMode)Enum.Parse(typeof(OtpHashMode), parameters[otpHashModeParameter]);

            if (otpData.Type == OtpType.Totp)
                otpData.Step = GetIntOrDefault(parameters, stepParameter, 30);
            else if (otpData.Type == OtpType.Hotp)
                otpData.Counter = GetIntOrDefault(parameters, counterParameter, 0);

            otpData.Size = GetIntOrDefault(parameters, sizeParameter, 6);

            return otpData;
        }

        /// <remarks>
        /// Hacky query string parsing.  This was done due to reports
        /// of people with just a 3.5 or 4.0 client profile getting errors
        /// as the System.Web assembly where .net's implementation of
        /// Url encoding and query string parsing is located.
        /// 
        /// This should be fine since the only thing stored in the string
        /// that needs to be encoded or decoded is the '=' sign.
        /// </remarks>
        private static NameValueCollection ParseQueryString(string data)
        {
            var collection = new NameValueCollection();

            var parameters = data.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var parameter in parameters)
            {
                if (parameter.Contains("="))
                {
                    var pieces = parameter.Split('=');
                    if (pieces.Length != 2)
                        continue;

                    collection.Add(pieces[0], pieces[1].Replace("%3d", "="));
                }
            }

            return collection;
        }

        private static int GetIntOrDefault(NameValueCollection parameters, string parameterKey, int defaultValue)
        {
            if (parameters[parameterKey] != null)
            {
                int step;
                if (int.TryParse(parameters[parameterKey], out step))
                    return step;
                else
                    return defaultValue;
            }
            else
                return defaultValue;
        }

        /// <remarks>
        /// Hacky query string parsing.  This was done due to reports
        /// of people with just a 3.5 or 4.0 client profile getting errors
        /// as the System.Web assembly where .net's implementation of
        /// Url encoding and query string parsing is locate.
        /// 
        /// This should be fine since the only thing stored in the string
        /// that needs to be encoded or decoded is the '=' sign.
        /// </remarks>
        public string EncodedString
        {
            get
            {
                NameValueCollection collection = new NameValueCollection();
                string base32Key = null;
                this.Key.UsePlainKey(key =>
                {
                    base32Key = Base32.Encode(key).Replace("=", "%3d");
                });
                collection.Add(keyParameter, base32Key);

                if (this.Type != KeeOtp.OtpType.Totp)
                    collection.Add(typeParameter, this.Type.ToString());

                if (this.Type == KeeOtp.OtpType.Hotp)
                    collection.Add(counterParameter, this.Counter.ToString());

                else if (this.Type == KeeOtp.OtpType.Totp)
                {
                    if (this.Step != 30)
                        collection.Add(stepParameter, this.Step.ToString());
                }

                if (this.Size != 6)
                    collection.Add(sizeParameter, this.Size.ToString());

                if (this.OtpHashMode != OtpHashMode.Sha1)
                    collection.Add(otpHashModeParameter, this.OtpHashMode.ToString());

                    string data = string.Empty;
                foreach (var key in collection.AllKeys)
                {
                    data += string.Format("{0}={1}&", key, collection[key]);
                }

                return data.TrimEnd('&');
            }
        }
    }
}
