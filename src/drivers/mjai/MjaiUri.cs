using System;
using NMahjong.Aux;

namespace NMahjong.Drivers.Mjai
{
    internal class MjaiUri
    {
        private readonly Uri mUri;

        private MjaiUri(Uri uri)
        {
            mUri = uri;
        }

        internal string Host
        {
            get { return mUri.Host; }
        }

        internal int Port
        {
            get { return mUri.Port; }
        }

        internal string Room
        {
            get { return mUri.AbsolutePath.Substring(1); }
        }

        internal static MjaiUri Parse(string uriString)
        {
            Uri uri = new Uri(uriString);
            CheckArg.Expect(uri.Scheme == "mjsonp", "uriString",
                            "Uri must have the mjsonp: scheme.");
            CheckArg.Expect(uri.Port >= 0, "uriString", "Uri must have a port number.");
            CheckArg.Expect(uri.Query == "" && uri.Fragment == "", "uriString",
                            "Uri must not have a query or a fragment.");
            return new MjaiUri(uri);
        }

        public override string ToString()
        {
            return mUri.ToString();
        }
    }
}
