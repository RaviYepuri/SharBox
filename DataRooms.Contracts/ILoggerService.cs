using DataRooms.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataRooms.Contracts
{
    public interface ILoggerService
    {
        public Task<IEnumerable<ActivityLog>> GetActivityLogs(string searchString);
        public Task<ActivityLog> GetActivityLog(int id);
        public Task<int> SaveActivityLog(ActivityLog log);
        public Task SaveDataLog(DataLog log);
        public Task<IEnumerable<DataLog>> GetDataLogs(int activityLogIg, string searchString);
    }
}
