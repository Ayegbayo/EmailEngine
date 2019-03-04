using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailEngine.Base.Entities
{
    public abstract class EmailContext<TEMailLog, TEmailTemplate>:DbContext where TEMailLog : VatEmailLog where TEmailTemplate :VatEmailTemplate
    {
        public DbSet<TEMailLog> EMailLogs { get; set; }
        public DbSet<TEmailTemplate> EmailTemplates { get; set; }
    }
}
