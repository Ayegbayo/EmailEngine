using EmailEngine.Base.Entities;
using EmailEngine.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailEngine.Repository.EmailRepository
{
    public class AuditTrailManager<TActivity> : IAuditTrailManager<TActivity> where TActivity: VatActivityLog
    {
        private readonly IGenericRepository<TActivity> _auditTrails;

        public AuditTrailManager(IGenericRepository<TActivity> auditTrails)
        {
            _auditTrails = auditTrails;
        }

        public async Task<int?> AddAuditTrail(TActivity log)
        {
            var auditTrailID = await _auditTrails.InsertAndGetIdAsync(log);
            return auditTrailID;
        }
 
        public async Task<List<TActivity>> FilterAuditTrail()
        {
            var auditTrails = await _auditTrails.GetAllListAsync();
            return auditTrails;
        }

        public async Task<List<TActivity>> GetAuditTrails()
        {
            var auditTrails = await _auditTrails.GetAllListAsync();
            return auditTrails;
        }

        public TActivity GetAuditTrail(int id)
        {
            var auditTrail = _auditTrails.Get(id);
            return auditTrail;
        }
    }
}
