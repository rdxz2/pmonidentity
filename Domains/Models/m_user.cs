using System;
using System.Collections.Generic;

namespace pmonidentity.Domains.Models
{
    public partial class m_user
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public DateTime cd { get; set; }
        public DateTime? md { get; set; }
        public DateTime? md_password { get; set; }
        public bool is_active { get; set; }

        public virtual m_user_detail m_user_detail { get; set; }
    }
}
