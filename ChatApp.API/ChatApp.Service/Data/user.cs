using System;
using System.Collections.Generic;

namespace ChatApp.Service.Data
{
    public partial class user
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime record_updated { get; set; }
        public int record_state { get; set; }
    }
}
