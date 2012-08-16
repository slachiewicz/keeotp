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

        public byte[] Key { get; set; }
        public OtpType Type { get; set; }

        public int Step { get; set; }
        public int Size { get; set; }

        public int Counter { get; set; }


        public static OtpAuthData FromString(string data)
        {
            var parameters = HttpUtility.ParseQueryString(data);

            if (parameters[keyParameter] == null)
                throw new ArgumentException("Must have a key in the data");

            var otpData = new OtpAuthData();

            otpData.Key = Base32.Decode(parameters[keyParameter]);
            if (parameters[typeParameter] != null)
                otpData.Type = (OtpType)Enum.Parse(typeof(OtpType), parameters[typeParameter]);

            if (otpData.Type == OtpType.Totp)
                otpData.Step = GetIntOrDefault(parameters, stepParameter, 30);
            else if (otpData.Type == OtpType.Hotp)
                otpData.Counter = GetIntOrDefault(parameters, counterParameter, 0);

            otpData.Size = GetIntOrDefault(parameters, sizeParameter, 6);

            return otpData;
        }

        private static int GetIntOrDefault(NameValueCollection parameters, string parameterKey, int defaultValue)
        {
            if (parameters[stepParameter] != null)
            {
                int step;
                if (int.TryParse(parameters[stepParameter], out step))
                    return step;
                else
                    return defaultValue;
            }
            else
                return defaultValue;
        }

        public string EncodedString
        {
            get
            {
                NameValueCollection collection = new NameValueCollection();
                collection.Add(keyParameter, Base32.Encode(this.Key));

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

                string data = string.Empty;
                foreach (var key in collection.AllKeys)
                {
                    data += string.Format("{0}={1}&", key, HttpUtility.UrlEncode(collection[key]));
                }

                return data.TrimEnd('&');
            }
        }
    }
}
