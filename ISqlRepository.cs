using DataRooms.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataRooms.Contracts
{
    public interface ISqlRepository<T>
    {
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<int> CreateAsync(T entity);
        Task CreateRangeAsync(IEnumerable<T> entities);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
        Task DeleteAllAsync();
        Task DeleteRangeAsync(IEnumerable<T> entities);
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Edit(T entity);
        void Remove(T entity);
        Task CreateDataRoomTable(int id);
        Task<int> CreateRowinDataRoomTable(T entity);
        //Task<IEnumerable<DataRooms.Entity.File>> GetAllFilesfromDataRoom(int dataroomid,int folderid, int fileid, string searchstring);
        Task DeleteFileRecord(DataRooms.Entity.File file);
        Task<IEnumerable<T>> GetDataAsync(DataRooms.Entity.FilterModel model);
        Task<IEnumerable<T>> DbGetDatawithQueryAsync(string sqlquery);
        IEnumerable<T> DbGetDatawithQuery(string sqlquery);

        bool ConfigureDatabase(DBConfigureData dBConfigureData, string scriptPath, string secretKey);
        Task<HostDetails> GetHostDetailsforActivation();
        Task<string> GetWaitingWithofFile(int fileid);
    }
}
