using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using InvAPI.Models;

namespace InvAPI.Models;

public partial class InventarisationDbContext : IdentityDbContext<IdentityUser>
{
    public InventarisationDbContext()
    {
    }

    public InventarisationDbContext(DbContextOptions<InventarisationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActiveTask> ActiveTasks { get; set; }

    //public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    //public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    //public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    //public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    //public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Employer> Employers { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Movement> Movements { get; set; }

    public virtual DbSet<Nomenclature> Nomenclatures { get; set; }

    public virtual DbSet<PhoneSession> PhoneSessions { get; set; }

    public virtual DbSet<Placement> Placements { get; set; }

    public virtual DbSet<Register> Registers { get; set; }

    public virtual DbSet<RevisionItem> RevisionItems { get; set; }

    public virtual DbSet<UnitsMeasurement> UnitsMeasurements { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Workplace> Workplaces { get; set; }

    public virtual DbSet<WriteOff> WriteOffs { get; set; }
    public DbSet<QueueLists> QueueLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=SV-SQL-02\\SVSQL02;Database=InventarisationDB;Persist Security Info=False;user=api-user;Password=QFgQJkWi4t;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActiveTask>(entity =>
        {
            entity.HasKey(e => e.IdTask);

            entity.Property(e => e.IdTask).HasColumnName("id_task");
            entity.Property(e => e.InventoryId).HasColumnName("inventory_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");

            entity.HasOne(d => d.Inventory).WithMany(p => p.ActiveTasks)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ActiveTasks_Inventory");
        });

        //modelBuilder.Entity<AspNetRole>(entity =>
        //{
        //    entity.Property(e => e.Name).HasMaxLength(256);
        //    entity.Property(e => e.NormalizedName).HasMaxLength(256);
        //});

        //modelBuilder.Entity<AspNetRoleClaim>(entity =>
        //{
        //    entity.Property(e => e.RoleId).HasMaxLength(450);

        //    entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        //});

        //modelBuilder.Entity<AspNetUser>(entity =>
        //{
        //    entity.Property(e => e.Email).HasMaxLength(256);
        //    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
        //    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
        //    entity.Property(e => e.UserName).HasMaxLength(256);

        //    entity.HasMany(d => d.Roles).WithMany(p => p.Users)
        //        .UsingEntity<Dictionary<string, object>>(
        //            "AspNetUserRole",
        //            r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
        //            l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
        //            j =>
        //            {
        //                j.HasKey("UserId", "RoleId");
        //                j.ToTable("AspNetUserRoles");
        //            });
        //});

        //modelBuilder.Entity<AspNetUserClaim>(entity =>
        //{
        //    entity.Property(e => e.UserId).HasMaxLength(450);

        //    entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        //});

        //modelBuilder.Entity<AspNetUserLogin>(entity =>
        //{
        //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

        //    entity.Property(e => e.UserId).HasMaxLength(450);

        //    entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        //});

        //modelBuilder.Entity<AspNetUserToken>(entity =>
        //{
        //    entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

        //    entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        //});

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.IdCompany);

            entity.Property(e => e.IdCompany).HasColumnName("id_company");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("company_name");
        });

        modelBuilder.Entity<Employer>(entity =>
        {
            entity.HasKey(e => e.IdEmpolyer);

            entity.ToTable("Employer");

            entity.Property(e => e.IdEmpolyer).HasColumnName("id_empolyer");
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("full_name");

        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inventor__3213E83F28C2101C");

            entity.ToTable("Inventory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.InvNum)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasComputedColumnSql("([dbo].[inv_num]([id]))", stored: true)
                .IsFixedLength()
                .HasColumnName("inv_num");
            entity.Property(e => e.NomenclatureId).HasColumnName("nomenclature_id");
            entity.Property(e => e.MoveId).HasColumnName("move_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Comment)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.PaymentNum).HasColumnName("payment_num");

            entity.Property(e => e.Invoice)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("invoice");
            entity.Property(e => e.WorkplaceId).HasColumnName("workplace_id");

            entity.Property(e => e.DateInv)
                .HasColumnType("datetime")
                .HasColumnName("dateInvCreate");

            //entity.HasOne(d => d.Company).WithMany(p => p.Inventories)
            //    .HasForeignKey(d => d.CompanyId)
            //    .OnDelete(DeleteBehavior.Cascade)
            //    .HasConstraintName("FK_Inventory_Companies");

            //entity.HasOne(d => d.Nomenclature).WithMany(p => p.Inventories)
            //    .HasForeignKey(d => d.NomenclatureId)
            //    .OnDelete(DeleteBehavior.Cascade)
            //    .HasConstraintName("FK_Inventory_Nomenclature");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("Login");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Movement>(entity =>
        {
            entity.HasKey(e => e.IdMovement);

            entity.Property(e => e.IdMovement).HasColumnName("id_movement");
            entity.Property(e => e.DateMove)
                .HasColumnType("date")
                .HasColumnName("date_move");
            entity.Property(e => e.IdInventory).HasColumnName("id_inventory");
            entity.Property(e => e.PlacementId).HasColumnName("placement_id");
            entity.Property(e => e.Planner)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("planner");

            entity.HasOne(d => d.IdInventoryNavigation).WithMany(p => p.Movements)
                .HasForeignKey(d => d.IdInventory)
                .HasConstraintName("FK_Movements_Inventory");

            entity.HasOne(d => d.Placement).WithMany(p => p.Movements)
                .HasForeignKey(d => d.PlacementId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Movements_Placements1");
        });

        modelBuilder.Entity<Nomenclature>(entity =>
        {
            entity.HasKey(e => e.IdNomenclature);

            entity.ToTable("Nomenclature");

            entity.Property(e => e.IdNomenclature).HasColumnName("id_nomenclature");
            entity.Property(e => e.CountDevice).HasColumnName("count_device");
            entity.Property(e => e.DateChange)
                .HasColumnType("date")
                .HasColumnName("date_change");
            entity.Property(e => e.DateCreation)
                .HasColumnType("date")
                .HasColumnName("date_creation");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("manufacturer");
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("model");
            entity.Property(e => e.NameDevice)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name_device");
        });

        modelBuilder.Entity<PhoneSession>(entity =>
        {
            entity.HasKey(e => e.IdSession);

            entity.Property(e => e.IdSession)
                .ValueGeneratedNever()
                .HasColumnName("id_session");
            entity.Property(e => e.DateCreationSession)
                .HasColumnType("date")
                .HasColumnName("date_creation_session");
            entity.Property(e => e.DateExpireSession)
                .HasColumnType("date")
                .HasColumnName("date_expire_session");
            entity.Property(e => e.NumSession)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("num_session");
            entity.Property(e => e.UsernameSession)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username_session");
        });

        modelBuilder.Entity<Placement>(entity =>
        {
            entity.HasKey(e => e.IdPlacement);

            entity.Property(e => e.IdPlacement).HasColumnName("id_placement");
            entity.Property(e => e.NamePlacement)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name_placement");
        });

        modelBuilder.Entity<Register>(entity =>
        {
            entity.HasKey(e => e.Username);

            entity.ToTable("Register");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RevisionItem>(entity =>
        {
            entity.HasKey(e => e.IdQueue);

            entity.Property(e => e.IdQueue).HasColumnName("id_queue");
            entity.Property(e => e.DateScan)
                .HasColumnType("datetime")
                .HasColumnName("date_scan");
            entity.Property(e => e.InventoryId).HasColumnName("inventory_id");
            entity.Property(e => e.IsDone).HasColumnName("is_done");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");

            entity.HasOne(d => d.Inventory).WithMany(p => p.RevisionItems)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_RevisionItems_Inventory");
        });

        modelBuilder.Entity<UnitsMeasurement>(entity =>
        {
            entity.HasKey(e => e.IdMeasure);

            entity.ToTable("UnitsMeasurement");

            entity.Property(e => e.IdMeasure).HasColumnName("id_measure");
            entity.Property(e => e.NameUnit)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("name_unit");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.FirstName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Login)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("patronymic");
        });

        modelBuilder.Entity<Workplace>(entity =>
        {
            entity.HasKey(e => e.IdWorkplace);

            entity.ToTable("Workplace");

            entity.Property(e => e.IdWorkplace).HasColumnName("id_workplace");
            entity.Property(e => e.EmployerId).HasColumnName("employer_id");
            entity.Property(e => e.IdInventory).HasColumnName("id_inventory");
 
            entity.Property(e => e.NameWorkplace)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("name_workplace");
            entity.Property(e => e.PlacementIdWp).HasColumnName("placement_id_wp");

       

            //entity.HasOne(d => d.Employer).WithMany(p => p.Workplaces)
            //    .HasForeignKey(d => d.EmployerId)
            //    .OnDelete(DeleteBehavior.Cascade)
            //    .HasConstraintName("FK_Workplace_Employer");

            //entity.HasOne(d => d.IdInventoryNavigation).WithMany(p => p.Workplaces)
            //    .HasForeignKey(d => d.IdInventory)
            //    .OnDelete(DeleteBehavior.Cascade)
            //    .HasConstraintName("FK_Workplace_Inventory");

            //entity.HasOne(d => d.PlacementIdWpNavigation).WithMany(p => p.Workplaces)
            //    .HasForeignKey(d => d.PlacementIdWp)
            //    .HasConstraintName("FK_Workplace_Placements");
        });

        modelBuilder.Entity<WriteOff>(entity =>
        {
            entity.HasKey(e => e.IdWriteoff);

            entity.ToTable("WriteOff");

            entity.Property(e => e.IdWriteoff).HasColumnName("id_writeoff");
            entity.Property(e => e.CountWriteoff).HasColumnName("count_writeoff");
            entity.Property(e => e.IdInventory).HasColumnName("id_inventory");
            entity.Property(e => e.Reason)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("reason");

            entity.HasOne(d => d.IdInventoryNavigation).WithMany(p => p.WriteOffs)
                .HasForeignKey(d => d.IdInventory)
                .HasConstraintName("FK_WriteOff_Inventory");
        });

        modelBuilder.Entity<QueueLists>(entity =>
        {
            entity.HasKey(e => e.id_list);

            entity.ToTable("QueueLists");

            entity.Property(e => e.id_list).HasColumnName("id_list");
            entity.Property(e => e.id_parent).HasColumnName("id_parent");
            entity.Property(e => e.is_active).HasColumnName("is_active");
     
            
        });

        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    
}
