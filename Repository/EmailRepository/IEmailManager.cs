using EmailEngine.Base.Entities;
using EmailEngine.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailEngine.Base.Repository.EmailRepository
{
    public interface IEmailManager<TEMailLog, TEmailTemplate> where TEMailLog : VatEmailLog where TEmailTemplate : VatEmailTemplate
    {
        Task<VatEmailStatus> SendMailAsync(TEMailLog emailLog);
        VatEmailStatus SendMail(TEMailLog emailLog);
        Task LogEmail(TEMailLog emailLog);
        Task CreateEmailTempalteAsync(TEmailTemplate emailTemplate);
        IQueryable<TEmailTemplate> GetEmailTemplate(VatEmailTemplateType type);
        Task SendBatchMailAsync();

    }
}
