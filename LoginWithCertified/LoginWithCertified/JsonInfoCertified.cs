using System;
using System.Collections.Generic;
using System.Text;

namespace LoginWithCertified
{
    public class JsonInfoCertified
    {

        public string pattern { get; set; }
        public Filter filter { get; set; }
        public JsonInfoCertified()
        {
            filter = new Filter();
            filter.SUBJECT = new SUBJECT();
            filter.ISSUER = new ISSUER();
        }
        public class Filter
        {
            public ISSUER ISSUER { get; set; }
            public SUBJECT SUBJECT { get; set; }
        }

        public class ISSUER
        {
            public string CN { get; set; }
            public string C { get; set; }
            public string O { get; set; }
        }

        public class SUBJECT
        {
            public string CN { get; set; }
            public string O { get; set; }
            public string OU { get; set; }
        }
    }
}
