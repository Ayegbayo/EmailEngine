using EmailEngine.Base.Entities;
using EmailEngine.Base.Repository.EmailRepository;
using EmailEngine.Repository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailEngine.Repository.EmailRepository
{
    public class EmailManager<TEMailLog, TEmailTemplate> : IEmailManager<TEMailLog, TEmailTemplate> where TEMailLog : VatEmailLog where TEmailTemplate : VatEmailTemplate
    {
        private readonly IRepository<TEMailLog, TEmailTemplate> _emailLogRepo;

        private readonly string _smtpPort;
        private readonly string _username;
        private readonly string _password;
        private readonly string _smtpHost;

        public static bool _mailSent;
        private readonly string _emailFrom;// = "info@nacc.org";
       


        public EmailManager(IRepository<TEMailLog, TEmailTemplate> emailLogRepo, IConfiguration configuration)
        {
            _emailLogRepo = emailLogRepo;
            _smtpPort = configuration.GetSection("EmailSettings:SMTPPort").Value;
            _username = configuration.GetSection("EmailSettings:UserNameEmail").Value;
            _password = configuration.GetSection("EmailSettings:Password").Value;
            _smtpHost = configuration.GetSection("EmailSettings:SMTPHost").Value;
            _emailFrom = configuration.GetSection("EmailSettings:EmailFrom").Value;
        }

        public async Task SaveEmailTemplate(TEmailTemplate emailTemplate)
        {
            await _emailLogRepo.InsertOrUpdateAsync(emailTemplate);
            await _emailLogRepo.SaveChangesAsync();           
        }

        public async Task LogEmail(TEMailLog emailLog)
        {
            var email =  await _emailLogRepo.InsertAsync(emailLog);
            await _emailLogRepo.SaveChangesAsync();            
        }

        public VatEmailStatus SendMail(TEMailLog emailLog)
        {
            throw new NotImplementedException();
        }

        public async Task<VatEmailStatus> SendMailAsync(TEMailLog emailLog)
        {
            try
            {
                MailMessage mMessage = new MailMessage();

                mMessage.To.Add(emailLog.Receiver);
                mMessage.Subject = emailLog.Subject;
                mMessage.From = new MailAddress($"VATEEP <{emailLog.Sender}>");
                mMessage.Body = emailLog.MailBody;
                mMessage.Priority = MailPriority.High;
                mMessage.IsBodyHtml = true;

                using (SmtpClient smtpMail = new SmtpClient())
                {
                    smtpMail.Host = _smtpHost;
                    smtpMail.Port = Convert.ToInt32(_smtpPort);
                    smtpMail.EnableSsl = true;
                    smtpMail.Credentials = new NetworkCredential(_username, _password);
                    //smtpMail.SendCompleted += SmtpMail_SendCompleted;

                    await smtpMail.SendMailAsync(mMessage);
                    return VatEmailStatus.Sent;
                }
            }
            catch (Exception)
            {
                return VatEmailStatus.Failed;
            }
            
        }

        public Task CreateEmailTempalteAsync(TEmailTemplate emailTemplate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEmailTemplate> GetEmailTemplate(VatEmailTemplateType type)
        {
            return _emailLogRepo.GetEMailTemplate(x => x.EmailTemplateType == type);
        }

        public async Task SendBatchMailAsync()
        {
            var unsentEmails = await _emailLogRepo.GetAllListAsync(x => x.Status != VatEmailStatus.Sent && x.DateToSend.Date == DateTime.Now.Date);
            foreach(var email in unsentEmails)
            {
                email.Status = await SendMailAsync(email);
                _emailLogRepo.Update(email);
                _emailLogRepo.SaveChanges();
            }
        }
    }
}
