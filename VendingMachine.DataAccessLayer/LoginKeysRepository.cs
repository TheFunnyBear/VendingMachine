using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine.Repository
{
    public sealed class LoginKeysRepository : ILoginKeysRepository
    {
        private VendingMachineContext _context = new VendingMachineContext();

        public async Task<AdminKey> GetAdminKey()
        {
            var loginKeys = await _context.LoginKeys.ToListAsync();
            if (loginKeys.Count > 0)
            {
                var lastAdminKey = loginKeys.Last();
                return lastAdminKey;
            }
            else
            {
                var defaultAdminKey = new AdminConstants().GetDefaultAdminKey();
                return new AdminKey() { Key = defaultAdminKey };
            }
        }

        public async Task<bool> IsAdminKeyValid(string adminKey)
        {
            var loginKeys = await _context.LoginKeys.ToListAsync();
            if (loginKeys.Count() > 0)
            {
                var lastAdminKey = loginKeys.Last();
                if (lastAdminKey.Key.Equals(adminKey, StringComparison.OrdinalIgnoreCase))
                {
                    _context.AdminEvents.Add(new AdminEvent() { EventTime = DateTime.Now, EventType = AdminEventType.Login });
                    int x = await _context.SaveChangesAsync();
                    return x == 0 ? false : true;
                }

            }
            else
            {
                var defaultAdminKey = new AdminConstants().GetDefaultAdminKey();
                if (defaultAdminKey.Equals(adminKey, StringComparison.OrdinalIgnoreCase))
                {
                    _context.AdminEvents.Add(new AdminEvent() { EventTime = DateTime.Now, EventType = AdminEventType.Login });
                    int x = await _context.SaveChangesAsync();
                    return x == 0 ? false : true;
                }
            }

            return false;
        }

        public async Task<bool> IsLoginExpired()
        {
            if (await _context.AdminEvents.CountAsync() > 0)
            {
                var adminEvents = await _context.AdminEvents.ToListAsync();
                var lasLoginTime = adminEvents.Where(adminEvent => adminEvent.EventType == AdminEventType.Login).Last();
                if (lasLoginTime != null)
                {
                    return (DateTime.Now - lasLoginTime.EventTime).TotalSeconds > new AdminConstants().GetSessionTime().TotalSeconds;
                }
            }
            return true;
        }

        public async Task<bool> SetAdminKey(string adminKey)
        {
            _context.LoginKeys.Add(new AdminKey() { Key = adminKey });
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }
    }

}
