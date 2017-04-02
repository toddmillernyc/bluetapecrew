using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace BlueTapeCrew.Utils
{
    public sealed class NvpCodec : NameValueCollection
    {
        private const string AMPERSAND = "&";
        private const string EQUALS = "=";
        private static readonly char[] AmpersandCharArray = AMPERSAND.ToCharArray();
        private static readonly char[] EqualsCharArray = EQUALS.ToCharArray();

        public string Encode()
        {
            var sb = new StringBuilder();
            var firstPair = true;
            foreach (var kv in AllKeys)
            {
                var name = HttpUtility.UrlEncode(kv);
                var value = HttpUtility.UrlEncode(this[kv]);
                if (!firstPair)
                {
                    sb.Append(AMPERSAND);
                }
                sb.Append(name).Append(EQUALS).Append(value);
                firstPair = false;
            }
            return sb.ToString();
        }

        public void Decode(string nvpstring)
        {
            Clear();
            foreach (var nvp in nvpstring.Split(AmpersandCharArray))
            {
                var tokens = nvp.Split(EqualsCharArray);
                if (tokens.Length < 2) continue;
                var name = HttpUtility.UrlDecode(tokens[0]);
                var value = HttpUtility.UrlDecode(tokens[1]);
                Add(name, value);
            }
        }

        public void Add(string name, string value, int index)
        {
            Add(GetArrayName(index, name), value);
        }

        public void Remove(string arrayName, int index)
        {
            Remove(GetArrayName(index, arrayName));
        }

        public string this[string name, int index]
        {
            get
            {
                return this[GetArrayName(index, name)];
            }
            set
            {
                this[GetArrayName(index, name)] = value;
            }
        }

        private static string GetArrayName(int index, string name)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "index cannot be negative : " + index);
            }
            return name + index;
        }
    }
}