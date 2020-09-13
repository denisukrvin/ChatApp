using System;
using System.Collections.Generic;

namespace ChatApp.Service.Data
{
    public partial class chat
    {
        public int id { get; set; }
        public int first_member_id { get; set; }
        public int second_member_id { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime record_updated { get; set; }
        public int record_state { get; set; }

        public virtual user first_member_ { get; set; }
        public virtual user second_member_ { get; set; }
    }
}
