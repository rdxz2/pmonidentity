using System;
using System.Collections.Generic;

namespace pmonidentity.Domains.Models
{
    public partial class m_user
    {
        public m_user()
        {
            InversecbNavigation = new HashSet<m_user>();
            InverseubNavigation = new HashSet<m_user>();
        }

        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public DateTime cd { get; set; }
        public int cb { get; set; }
        public DateTime? ud { get; set; }
        public int? ub { get; set; }
        public bool active { get; set; }

        public virtual m_user cbNavigation { get; set; }
        public virtual m_user ubNavigation { get; set; }
        public virtual m_user_detail m_user_detail { get; set; }
        public virtual ICollection<m_user> InversecbNavigation { get; set; }
        public virtual ICollection<m_user> InverseubNavigation { get; set; }
    }
}
