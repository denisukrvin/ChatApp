using System;
using System.Collections.Generic;

namespace ChatApp.Service.Data
{
    public partial class message
    {
        public int id { get; set; }
        public int chat_id { get; set; }
        public int user_id { get; set; }
        public string text { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime record_updated { get; set; }
        public int record_state { get; set; }

        public virtual user user_ { get; set; }
    }
}
