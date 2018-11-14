using System;

namespace VendingMachine.Model
{
    public sealed class AdminConstants 
    {
        public string GetDefaultAdminKey()
        {
            return "3D130DCC-E060-418E-B8D4-F66E7BE596BE";
        }

        public TimeSpan GetSessionTime()
        {
            return TimeSpan.FromMinutes(15);
        }
    }
}
