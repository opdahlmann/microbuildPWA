using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.PWA.Models
{
    public class PushNotificationOptions
    {
        //this is used with lib.net.http.webpush library.
        //public string PublicKey { get; set; }
        //public string PrivateKey { get; set; }

        public string Subject { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}
