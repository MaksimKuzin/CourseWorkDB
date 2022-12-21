using System;
using System.Collections.Generic;
using CourseWorkDB.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkDB;

public partial class ChurchParishCourseWorkContext : DbContext
{
    public ChurchParishCourseWorkContext()
    {
    }

    public ChurchParishCourseWorkContext(DbContextOptions<ChurchParishCourseWorkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<DivineService> DivineServices { get; set; }

    public virtual DbSet<Donation> Donations { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Parishioner> Parishioners { get; set; }

    public virtual DbSet<Priest> Priests { get; set; }

    public virtual DbSet<SacredEvent> SacredEvents { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-5V89RQO\\SQLEXPRESS;Database=ChurchParishCourseWork;Trusted_Connection=True;Encrypt=False;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.EventId);

            entity.ToTable("Activity");

            entity.Property(e => e.EventId).ValueGeneratedNever();

            entity.HasOne(d => d.Event).WithOne(p => p.Activity)
                .HasForeignKey<Activity>(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Activity_Event");
        });

        modelBuilder.Entity<DivineService>(entity =>
        {
            entity.HasKey(e => e.EventId);

            entity.ToTable("DivineService");

            entity.Property(e => e.EventId).ValueGeneratedNever();
            entity.Property(e => e.Justification).HasMaxLength(30);
            entity.Property(e => e.Prayer).HasMaxLength(30);

            entity.HasOne(d => d.Event).WithOne(p => p.DivineService)
                .HasForeignKey<DivineService>(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DivineService_Event");
        });

        modelBuilder.Entity<Donation>(entity =>
        {
            entity.ToTable("Donation");

            entity.Property(e => e.Purpose).HasMaxLength(50);
            entity.Property(e => e.Sum).HasColumnType("money");

            entity.HasOne(d => d.Parishioner).WithMany(p => p.Donations)
                .HasForeignKey(d => d.ParishionerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Donation_Parishioner");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Event");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(30);

            entity.HasOne(d => d.Priest).WithMany(p => p.Events)
                .HasForeignKey(d => d.PriestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Event_Priest");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.ToTable("Inventory");

            entity.Property(e => e.DateOfPurchase).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Event).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventory_Event");
        });

        modelBuilder.Entity<Parishioner>(entity =>
        {
            entity.ToTable("Parishioner");

            entity.Property(e => e.Address).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Patronymic).HasMaxLength(30);
            entity.Property(e => e.PhoneNumber).HasMaxLength(12);
            entity.Property(e => e.Surname).HasMaxLength(30);

            entity.HasOne(d => d.Priest).WithMany(p => p.Parishioners)
                .HasForeignKey(d => d.PriestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Parishioner_Priest");

            entity.HasMany(d => d.Events).WithMany(p => p.Parishioners)
                .UsingEntity<Dictionary<string, object>>(
                    "ParishionerEvent",
                    r => r.HasOne<Event>().WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ParishionerEvent_Event"),
                    l => l.HasOne<Parishioner>().WithMany()
                        .HasForeignKey("ParishionerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ParishionerEvent_Parishioner"),
                    j =>
                    {
                        j.HasKey("ParishionerId", "EventId");
                        j.ToTable("ParishionerEvent");
                    });
        });

        modelBuilder.Entity<Priest>(entity =>
        {
            entity.ToTable("Priest");

            entity.Property(e => e.InitialDate).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Title).HasMaxLength(30);
        });

        modelBuilder.Entity<SacredEvent>(entity =>
        {
            entity.HasKey(e => e.EventId);

            entity.ToTable("SacredEvent");

            entity.Property(e => e.EventId).ValueGeneratedNever();
            entity.Property(e => e.FinishDate).HasColumnType("datetime");
            entity.Property(e => e.Place).HasMaxLength(30);
            entity.Property(e => e.Route).HasMaxLength(200);
            entity.Property(e => e.SourceOfFunding).HasMaxLength(30);
            entity.Property(e => e.Transport).HasMaxLength(30);

            entity.HasOne(d => d.Event).WithOne(p => p.SacredEvent)
                .HasForeignKey<SacredEvent>(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SacredEvent_Event");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
