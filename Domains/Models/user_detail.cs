using System;
using System.Collections.Generic;

namespace pmonidentity.Domains.Models
{
    public partial class user_detail
    {
        public int id_user { get; set; }
        public string name { get; set; }
        public string name_shorthand { get; set; }
        public string nik { get; set; }
        public string email { get; set; }
        public string ext { get; set; }
        public string image { get; set; }

        public virtual user id_userNavigation { get; set; }
    }
}
