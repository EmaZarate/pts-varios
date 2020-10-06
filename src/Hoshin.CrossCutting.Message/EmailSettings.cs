using System;

namespace Hoshin.CrossCutting.Message
{
    public class EmailSettings
    {
        public String PrimaryDomain { get; set; }
        public int PrimaryPort { get; set; }
        public String SecondayDomain { get; set; }
        public int SecondaryPort { get; set; }
        public String UsernameEmail { get; set; }
        public String UsernamePassword { get; set; }
        public String UsernameName { get; set; }
        public String FromEmail { get; set; }
        public String Url { get; set; }
        public string SendGridKey { get; set; }
        public bool UseSendGrid { get; set; }
    }
}
