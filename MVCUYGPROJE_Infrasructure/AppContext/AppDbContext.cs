using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MVCUYGPROJE_Domain.Core.BaseEntites;
using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE_Infrastructure.AppContext;

public class AppDbContext:IdentityDbContext<IdentityUser,IdentityRole,string>
{
    private readonly IHttpContextAccessor _contextAccessor;
    public const string DevConnectionString= "AppConnectionDev";
    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor contextAccessor) : base(options)
    {
        _contextAccessor= contextAccessor;
    }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        //connction string i jsondan çekeceğim için bu şekilde bir constructor oluşturuyorum.
    }

    public virtual DbSet<Admin> Admins { get; set; }
    public virtual DbSet<ProfileUser> ProfileUsers { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Publisher> Publishers { get; set; }
    public virtual DbSet<Book> Books { get; set; }


    protected override  void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(IEntityConfiguration).Assembly);
        base.OnModelCreating(builder);
    }
    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetBaseProperties();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetBaseProperties()
    {
        var entries = ChangeTracker.Entries<BaseEntity>();
        var userId = "UserBulunamadı";
        //var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "UserBulunamadı";
        foreach (var entry in entries)
        {
           SetIfAdded(entry, userId);
            SetIfModified(entry, userId);
            SetIfDeleted(entry, userId);
        }
    }

    private void SetIfDeleted(EntityEntry<BaseEntity> entry, string userId)
    {
        if(entry.State != EntityState.Deleted)
        {
            return;
        }
        if(entry.Entity is not AuditableEntity entity)
        {
            return;
        }
        entry.State = EntityState.Modified;
        entry.Entity.Status=MVCUYGPROJE_Domain.Enums.Status.Deleted;
        entity.DeletedDate = DateTime.Now;
        entity.DeletedBy = userId;
    }

    private void SetIfModified(EntityEntry<BaseEntity> entry, string userId)
    {
        if (entry.State == EntityState.Modified)
        {
            entry.Entity.Status = MVCUYGPROJE_Domain.Enums.Status.Updated;
            entry.Entity.UpdatedDate = DateTime.Now;
            entry.Entity.UpdatedBy = userId;
        }
    }

    private void SetIfAdded(EntityEntry<BaseEntity> entry, string userId)
    {
        if (entry.State == EntityState.Added)
        {
            entry.Entity.Status = MVCUYGPROJE_Domain.Enums.Status.Created;
            entry.Entity.CreatedDate = DateTime.Now;
            entry.Entity.CreatedBy = userId;
        }
    }
}
