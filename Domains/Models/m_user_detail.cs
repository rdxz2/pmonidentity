using System;
using System.Collections.Generic;

namespace pmonidentity.Domains.Models
{
    public partial class m_user_detail
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nik { get; set; }
        public string email { get; set; }
        public string ext { get; set; }

        public virtual m_user idNavigation { get; set; }
    }
}
