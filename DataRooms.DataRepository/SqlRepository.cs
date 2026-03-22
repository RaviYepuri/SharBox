using DataRooms.Contracts;
using DataRooms.Entity;
using LoggerModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataRooms.DataRepository
{
    public class SqlRepository<T> : DBFactoryBase, ISqlRepository<T> where T : class
    {
        private readonly SqlServerContext _context;
        private readonly DbSet<T> _dbSet;
        private ILoggerManager _logger;

        public SqlRepository(IConfiguration config, SqlServerContext context, ILoggerManager logger)
            : base(config)
        {
            _logger = logger;
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public async Task<int> CreateAsync(T entity)
        {
            try
            {
                _context.Add(entity);
                return await _context.SaveChangesAsync();
                
                //Dictionary<string, object> columnNamesValues = GetDictionaryColumnNameValues(entity);
                //return await DbExecuteAsync(GetSqlQuery(columnNamesValues.Keys.ToArray()), columnNamesValues.Values.ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "--" + ex.StackTrace, ex);
                throw ex;
            }
        }

        public async Task CreateRangeAsync(IEnumerable<T> entities)
        {
            foreach(var entity in entities)
            {
                await CreateAsync(entity);
            }
        }

        public async Task<int> UpdateAsync(T entity)
        {
            try
            {
                _logger.LogDebug("Enter into Update Method..");
                //_context.Entry(entity).State = EntityState.Detached;
                //_context.Update(entity);

                _logger.LogDebug(JsonConvert.SerializeObject(entity));

                _context.Update<T>(entity);

                //_dbSet.Attach(entity);
                //_context.Entry(entity).State = EntityState.Modified;

                int status = await _context.SaveChangesAsync();
                _logger.LogDebug("Exit from Update Method..");
                return status;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message + "--" + ex.StackTrace,ex);
                return 0;
                //throw ex;
            }
        }

        public async Task<int> DeleteAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            return await _context.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            IEnumerable<T> entities = await GetAsync();
            foreach (T entity in entities.Distinct())
            {
                await DeleteAsync(entity);
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            foreach (T entity in entities.Distinct())
            {
                await DeleteAsync(entity);
            }
        }

        private Dictionary<string,object> GetDictionaryColumnNameValues(T entity)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            Type attrType = typeof(T);
            foreach(PropertyInfo property in attrType.GetProperties())
            {
                if(property.Name != "Id")
                {
                    object value = property.GetValue(entity);
                    if (value != null)
                    {
                        keyValues.Add(GetColumnName(property), value);
                    }
                }
            }
            return keyValues;
        }

        private string GetSqlQuery(string[] keys,string tableName="")
        {
            return $"INSERT INTO {GetTableName(tableName)} ({GetKeysString(keys)}) output inserted.Id VALUES ({GetValuesString(keys.Length)})";
        }

        private object GetValuesString(int count)
        {
            int[] indexes = new int[count];
            for (int i = 0; i < indexes.Length; i++)
            {
                indexes[i] = i + 1;
            }
            return $"@{string.Join(", @",indexes)}";
        }

        private object GetKeysString(string[] keys)
        {
            return string.Join(", ",keys.ToArray());
        }

        private string GetTableName(string tableName)
        {
            Type attrType = typeof(T);
            return string.IsNullOrEmpty(tableName) ? attrType.Name : tableName;
        }

        private string GetColumnName(PropertyInfo property)
        {
            return property.Name;
        }

        public void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate).AsNoTracking();
            return query;
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            if (query == null) throw new InvalidOperationException($"Cannot find set of {typeof(T)} in {_context}");
            return query;
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                if (filter != null)
                {
                    query = query.Where(filter).AsNoTracking();
                }
                if (orderBy != null)
                {
                    return await orderBy(query).AsNoTracking().ToListAsync();
                }
                IEnumerable<T> result = await query.ToListAsync();
                if (result == null)
                    result = new List<T>();
                return result;
            }
            catch(Exception ex)
            {
                return new List<T>();
            }
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                if (filter != null)
                {
                    query = query.Where(filter).AsNoTracking();
                }
                if (orderBy != null)
                {
                    return await orderBy(query).AsNoTracking().FirstAsync();
                }
                T result = await query.FirstAsync();
                if (result == null)
                    result = default(T);
                return result;
            }
            catch(Exception ex)
            {
                return default(T);
            }
            
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task CreateDataRoomTable(int id)
        {
            try
            {
                string query = string.Format(@"CREATE TABLE [dbo].[DR_{0}](
	                            [Id] [int] IDENTITY(1,1) NOT NULL,
	                            [FileName] [varchar](256) NULL,
	                            [ContentType] [varchar](256) NULL,
	                            [FileSize] [varchar](32) NULL,
	                            [Guid] [varchar](256) NULL,
	                            [FolderId] [int] NULL,
	                            [FolderName] [varchar](256) NULL,
	                            [DataRoomId] [int] NULL,
	                            [DataRoomName] [varchar](256) NULL,
                                [RelativePath] [nvarchar](1024) NULL,
	                            [IsActive] [bit] NULL,
	                            [CreatedBy] [int] NULL,
	                            [CreatorName] [varchar](256) NULL,
	                            [CreatedOn] [datetime] NULL,
	                            [ModifiedBy] [int] NULL,
	                            [ModifierName] [varchar](256) NULL,
	                            [ModifiedOn] [datetime] NULL,
	                            [DeletedBy] [int] NULL,
	                            [DeletorName] [varchar](256) NULL,
	                            [DeletedOn] [datetime] NULL)", id);
                await _context.Database.ExecuteSqlRawAsync(query);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CreateRowinDataRoomTable(T entity)
        {
            try
            {
                Dictionary<string, object> columnNamesValues = GetDictionaryColumnNameValues(entity);
                File fl = (File)(object)entity;
                string query = GetSqlQuery(columnNamesValues.Keys.ToArray(),"DR_" + fl.DataRoomId.ToString());
                return await DbExecuteAsync(query, columnNamesValues.Values.ToArray());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<IEnumerable<File>> GetAllFilesfromDataRoom(int dataroomid,int folderid,int fileid,string searchstring)
        //{
        //    try
        //    {
        //        string sql = @"SELECT Id
        //                      ,FileName
        //                      ,ContentType
        //                      ,FileSize
        //                      ,Guid
        //                      ,FolderId
        //                      ,FolderName
        //                      ,DataRoomId
        //                      ,DataRoomName
        //                      ,RelativePath
        //                      ,IsActive
        //                      ,CreatedBy
        //                      ,CreatorName
        //                      ,CreatedOn
        //                      ,ModifiedBy
        //                      ,ModifierName
        //                      ,ModifiedOn
        //                      ,DeletedBy
        //                      ,DeletorName
        //                      ,DeletedOn
        //                  FROM DR_"+ dataroomid  + " Where DataRoomId='"+ dataroomid + "'";
        //        if(fileid > 0)
        //        {
        //            sql += " AND Id= '"+ fileid + "'";
        //        }
        //        if (folderid > 0)
        //        {
        //            sql += " AND FolderId= '" + folderid + "'";
        //        }
        //        if (searchstring != "empty")
        //        {
        //            sql += " AND Lower(FolderName) like ('%" + searchstring.ToLower() + "%')";
        //        }
        //        return await GetAllFiles(sql);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        

        public async Task DeleteFileRecord(File file)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("delete from DR_"+ file.DataRoomId+" where Id = " + file.Id);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> GetDataAsync(FilterModel model)
        {
            try
            {
                return await GetData<T>(model.CurrentPage, model.RecordsPerPage, model.TableName, model.WhereCondition,model.Sort);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> DbGetDatawithQueryAsync(string sqlquery)
        {
            try
            {
                return await DbGetDatawithQueryAsync<T>(sqlquery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<T> DbGetDatawithQuery(string sqlquery)
        {
            try
            {
                return DbGetDatawithQuery<T>(sqlquery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ConfigureDatabase(DBConfigureData dBConfigureData, string scriptPath, string secretKey)
        {
            try
            {
                return CheckandCreateDatabase(dBConfigureData, scriptPath, secretKey);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HostDetails> GetHostDetailsforActivation()
        {
            try
            {
                return await GetHostDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetWaitingWithofFile(int fileid)
        {
            try
            {
                return await GetWaitingWith(fileid);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
