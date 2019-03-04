using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EmailEngine.Base.Entities
{
    public enum AuditAction
    {
        [Description("Create")]
        Create = 1,
        [Description("Update")]
        Update,
        [Description("Delete")]
        Delete,
        [Description("Read")]
        Read        
    }
}
