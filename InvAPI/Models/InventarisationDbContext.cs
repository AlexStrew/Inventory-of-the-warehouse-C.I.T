using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InvAPI.Models;

public partial class InventarisationDbContext : DbContext
{
    public InventarisationDbContext()
    {
    }

    public InventarisationDbContext(DbContextOptions<InventarisationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Employer> Employers { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Movement> Movements { get; set; }

    public virtual DbSet<Nomenclature> Nomenclatures { get; set; }

    public virtual DbSet<Placement> Placements { get; set; }

    public virtual DbSet<UnitsMeasurement> UnitsMeasurements { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Workplace> Workplaces { get; set; }

    public virtual DbSet<WriteOff> WriteOffs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=InventarisationDB;Data Source=SV-SQL-02\\SVSQL02;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
            entity.Property(e => e.FirstName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("patronymic");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inventor__3213E83F28C2101C");

            entity.ToTable("Inventory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.InvNum)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasComputedColumnSql("([dbo].[inv_num]([id]))", false)
                .IsFixedLength()
                .HasColumnName("inv_num");
            entity.Property(e => e.Invoice)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("invoice");
            entity.Property(e => e.MoveId).HasColumnName("move_id");
            entity.Property(e => e.NomenclatureId).HasColumnName("nomenclature_id");
            entity.Property(e => e.PaymentNum).HasColumnName("payment_num");
            entity.Property(e => e.WorkplaceId).HasColumnName("workplace_id");

            entity.HasOne(d => d.Company).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Inventory_Companies");

            entity.HasOne(d => d.Nomenclature).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.NomenclatureId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Inventory_Nomenclature");
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
            entity.Property(e => e.NameDevice)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name_device");
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
            entity.Property(e => e.DeviceId).HasColumnName("device_id");
            entity.Property(e => e.EmployerId).HasColumnName("employer_id");
            entity.Property(e => e.IdInventory).HasColumnName("id_inventory");
            entity.Property(e => e.Mol)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("mol");
            entity.Property(e => e.NameWorkplace)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("name_workplace");
            entity.Property(e => e.PlacementIdWp).HasColumnName("placement_id_wp");

            entity.HasOne(d => d.Device).WithMany(p => p.Workplaces)
                .HasForeignKey(d => d.DeviceId)
                .HasConstraintName("FK_Workplace_Nomenclature");

            entity.HasOne(d => d.Employer).WithMany(p => p.Workplaces)
                .HasForeignKey(d => d.EmployerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Workplace_Employer");

            entity.HasOne(d => d.IdInventoryNavigation).WithMany(p => p.Workplaces)
                .HasForeignKey(d => d.IdInventory)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Workplace_Inventory");

            entity.HasOne(d => d.PlacementIdWpNavigation).WithMany(p => p.Workplaces)
                .HasForeignKey(d => d.PlacementIdWp)
                .HasConstraintName("FK_Workplace_Placements");
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
