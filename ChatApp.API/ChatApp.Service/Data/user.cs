using System;
using System.Collections.Generic;

namespace ChatApp.Service.Data
{
    public partial class user
    {
        public user()
        {
            chatfirst_member_ = new HashSet<chat>();
            chatsecond_member_ = new HashSet<chat>();
            message = new HashSet<message>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime record_updated { get; set; }
        public int record_state { get; set; }
        public string avatar { get; set; }
        public string description { get; set; }

        public virtual ICollection<chat> chatfirst_member_ { get; set; }
        public virtual ICollection<chat> chatsecond_member_ { get; set; }
        public virtual ICollection<message> message { get; set; }
    }
}
