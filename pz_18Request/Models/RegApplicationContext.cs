using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace pz_18Request.Models;

public partial class RegApplicationContext : DbContext
{
    public RegApplicationContext()
    {
    }

    public RegApplicationContext(DbContextOptions<RegApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<DeviceModel> DeviceModels { get; set; }

    public virtual DbSet<DeviceType> DeviceTypes { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestStatus> RequestStatuses { get; set; }

    public virtual DbSet<Technician> Technicians { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-FU9UH67\\MSSQLSERVER32; Database= RegApplication; TrustServerCertificate = true; Trusted_Connection=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__E67E1A04CE0E6E95");

            entity.Property(e => e.ClientId)
                .ValueGeneratedNever()
                .HasColumnName("ClientID");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFAA435BE2E1");

            entity.Property(e => e.CommentId)
                .ValueGeneratedNever()
                .HasColumnName("CommentID");
            entity.Property(e => e.CommentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CommentText).HasMaxLength(500);
            entity.Property(e => e.RequestId).HasColumnName("RequestID");

            entity.HasOne(d => d.Request).WithMany(p => p.Comments)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__Reques__5CD6CB2B");
        });

        modelBuilder.Entity<DeviceModel>(entity =>
        {
            entity.HasKey(e => e.DeviceModelId).HasName("PK__DeviceMo__B81D528A74288EE0");

            entity.Property(e => e.DeviceModelId)
                .ValueGeneratedNever()
                .HasColumnName("DeviceModelID");
            entity.Property(e => e.DeviceModelName).HasMaxLength(100);
            entity.Property(e => e.DeviceTypeId).HasColumnName("DeviceTypeID");

            entity.HasOne(d => d.DeviceType).WithMany(p => p.DeviceModels)
                .HasForeignKey(d => d.DeviceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DeviceMod__Devic__534D60F1");
        });

        modelBuilder.Entity<DeviceType>(entity =>
        {
            entity.HasKey(e => e.DeviceTypeId).HasName("PK__DeviceTy__07A6C7166D72D1EE");

            entity.HasIndex(e => e.DeviceTypeName, "UQ__DeviceTy__7C83B418F24D4F95").IsUnique();

            entity.Property(e => e.DeviceTypeId)
                .ValueGeneratedNever()
                .HasColumnName("DeviceTypeID");
            entity.Property(e => e.DeviceTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Requests__33A8519AE1AF6371");

            entity.Property(e => e.RequestId)
                .ValueGeneratedNever()
                .HasColumnName("RequestID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.DeviceModelId).HasColumnName("DeviceModelID");
            entity.Property(e => e.ProblemDescription).HasMaxLength(255);
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.TechnicianId).HasColumnName("TechnicianID");

            entity.HasOne(d => d.Client).WithMany(p => p.Requests)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__Client__5629CD9C");

            entity.HasOne(d => d.DeviceModel).WithMany(p => p.Requests)
                .HasForeignKey(d => d.DeviceModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__Device__5812160E");

            entity.HasOne(d => d.Status).WithMany(p => p.Requests)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__Status__59063A47");

            entity.HasOne(d => d.Technician).WithMany(p => p.Requests)
                .HasForeignKey(d => d.TechnicianId)
                .HasConstraintName("FK__Requests__Techni__571DF1D5");
        });

        modelBuilder.Entity<RequestStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__RequestS__C8EE20430CC1F8D3");

            entity.ToTable("RequestStatus");

            entity.HasIndex(e => e.StatusName, "UQ__RequestS__05E7698ACCEDD641").IsUnique();

            entity.Property(e => e.StatusId)
                .ValueGeneratedNever()
                .HasColumnName("StatusID");
            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<Technician>(entity =>
        {
            entity.HasKey(e => e.TechnicianId).HasName("PK__Technici__301F82C1A75D25F4");

            entity.Property(e => e.TechnicianId)
                .ValueGeneratedNever()
                .HasColumnName("TechnicianID");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Specialization).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
