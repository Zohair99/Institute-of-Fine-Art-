using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IOFA.Models;

public partial class YourDbContext : DbContext
{
    public YourDbContext()
    {
    }

    public YourDbContext(DbContextOptions<YourDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Art> Arts { get; set; }

    public virtual DbSet<Compitition> Compititions { get; set; }

    public virtual DbSet<Register> Registers { get; set; }

    public virtual DbSet<Remark> Remarks { get; set; }

    public DbSet<Award> Awards { get; set; }

    public DbSet<Teacher> Teachers { get; set; }

    public DbSet<Manager> Managers { get; set; }

    public DbSet<Admin> Admins { get; set; }

    public DbSet<Exhibition> Exhibitions { get; set; }


    public virtual DbSet<C_Art> C_Arts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=IOFA;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Art>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Art__3214EC07067B08CB");

            entity.ToTable("Art");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Path)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("path");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UploadedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UploaderName).HasMaxLength(255);
        });

        modelBuilder.Entity<Compitition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__compitit__3213E83F9979B6BF");

            entity.ToTable("compitition");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SubmitDate)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Theme)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Register>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Register__3213E83F1B5B9CD6");

            entity.ToTable("Register");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Artist)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("artist");
            entity.Property(e => e.CPassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("c_password");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Remark>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Remarks__3214EC07C68988BE");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TeacherName).HasMaxLength(100);

            entity.HasOne(d => d.Art).WithMany(p => p.Remarks)
                .HasForeignKey(d => d.ArtId)
                .HasConstraintName("FK__Remarks__ArtId__07C12930");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
