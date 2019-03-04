using System;
using System.Collections.Generic;
using System.Text;

namespace EmailEngine.Base.Entities
{
    public abstract class VatActivityLog:IGenericEntity
    {        
        public int UserId { get; set; }
        public string UserName { get; set; }
        public AuditAction ActionTaken { get; set; }
        public string Description { get; set; }
        public string Entity { get; set; }

        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public bool IsTransient()
        {
            return EqualityComparer<int>.Default.Equals(Id, default(int));
        }
    }
}
