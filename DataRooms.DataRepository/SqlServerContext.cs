using DataRooms.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataRooms.DataRepository
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options)
            : base(options)
        {            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.EnableSensitiveDataLogging();
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(k=> new { k.Id });
            modelBuilder.Entity<RoleMaster>().HasKey(k => new { k.Id });
            modelBuilder.Entity<UserRoleMapping>().HasKey(k => new { k.Id });
            modelBuilder.Entity<DataRoom>().HasKey(k => new { k.Id });
            modelBuilder.Entity<PermissionMaster>().HasKey(k => new { k.Id });
            modelBuilder.Entity<DataRoomPermission>().HasKey(k => new { k.Id });
            modelBuilder.Entity<ActivityLog>().HasKey(k => new { k.Id });
            modelBuilder.Entity<Folder>().HasKey(k => new { k.Id });
            modelBuilder.Entity<File>().HasKey(k => new { k.Id });
            modelBuilder.Entity<FileVersion>().HasKey(k => new { k.Id });
            modelBuilder.Entity<DataLog>().HasKey(k => new { k.Id });
            modelBuilder.Entity<FolderPermission>().HasKey(k => new { k.Id });
            modelBuilder.Entity<FilePermission>().HasKey(k => new { k.Id });
            modelBuilder.Entity<LicenseInfo>().HasKey(k => new { k.Id });
            modelBuilder.Entity<ADInfo>().HasKey(k => new { k.Id });
            modelBuilder.Entity<Company>().HasKey(k => new { k.Id });
            modelBuilder.Entity<WorkFlowMaster>().HasKey(k => new { k.Id });
            modelBuilder.Entity<DataRoomWorkFlowUser>().HasKey(k => new { k.Id });
            modelBuilder.Entity<ToDoTask>().HasKey(k => new { k.Id });
            modelBuilder.Entity<AuditLog>().HasKey(k => new { k.Id });
            modelBuilder.Entity<Setting>().HasKey(k => new { k.Id });
            modelBuilder.Entity<EmailConfiguration>().HasKey(k => new { k.Id });
            modelBuilder.Entity<ItemTrackerMetaData>().HasKey(k => new { k.Id });
            modelBuilder.Entity<ItemTrackerControl>().HasKey(k => new { k.Id });
            modelBuilder.Entity<ItemTrackerData>().HasKey(k => new { k.Id });
            modelBuilder.Entity<ItemTrackerPermission>().HasKey(k => new { k.Id });
            modelBuilder.Entity<ItemTrackerHistory>().HasKey(k => new { k.Id });
        }
    }
}
