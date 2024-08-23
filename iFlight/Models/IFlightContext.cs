using System;
using System.Collections.Generic;
using Check.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace iFlight.Models;

public partial class IFlightContext : DbContext
{
    public IFlightContext()
    {
    }

    public IFlightContext(DbContextOptions<IFlightContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AirField> AirFields { get; set; }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<ArticleSubject> ArticleSubjects { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Message> Messages { get; set; }
    public virtual DbSet<FlightInstructor> FlightInstructors { get; set; }

    public virtual DbSet<FlightType> FlightTypes { get; set; }

    public virtual DbSet<InterestedInFlightType> InterestedInFlightTypes { get; set; }

    public virtual DbSet<Pilot> Pilots { get; set; }

    public virtual DbSet<Plane> Planes { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false).Build();
    //    String connStr = config.GetConnectionString("DefaultConnectionString");
    //    optionsBuilder.UseSqlServer(connStr);
    //}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        if (!optionsBuilder.IsConfigured)
        {
            // Create a configuration builder
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Get the connection string
            string connStr = config.GetConnectionString("DefaultConnectionString");

            // Configure the DbContext to use SQL Server
            optionsBuilder.UseSqlServer(connStr);
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AirField>(entity =>
        {
            entity.HasKey(e => e.Icao).HasName("PK__AirField__B83FBAFEAB1513B4");

            entity.ToTable("AirField");

            entity.Property(e => e.Icao)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("ICAO");
            entity.Property(e => e.FieldName)
                .HasMaxLength(50)
                .HasColumnName("fieldName");
        });

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.ArticleNumber).HasName("PK__Article__3C99114315D021E8");

            entity.ToTable("Article");

            entity.Property(e => e.SubjectName).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.SubjectNameNavigation).WithMany(p => p.Articles)
                .HasForeignKey(d => d.SubjectName)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Article__Subject__5FB337D6");

            entity.HasOne(d => d.WriterLicenseNumberNavigation).WithMany(p => p.Articles)
                .HasForeignKey(d => d.WriterLicenseNumber)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Article__WriterL__5EBF139D");
        });

        modelBuilder.Entity<ArticleSubject>(entity =>
        {
            entity.HasKey(e => e.SubjectName).HasName("PK__ArticleS__E5068BFCF8318F02");

            entity.ToTable("ArticleSubject");

            entity.Property(e => e.SubjectName)
                .HasMaxLength(50)
                .HasColumnName("subjectName");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC27C748BA67");

            entity.ToTable("Employee");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PasswordKey)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TelephoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FlightInstructor>(entity =>
        {
            entity.HasKey(e => e.LicenseNumber).HasName("PK__FlightIn__E889016724029BF0");

            entity.ToTable("FlightInstructor");

            entity.HasIndex(e => e.InstructorLicenseNumber, "UQ__FlightIn__F7222AD43D968C36").IsUnique();

            entity.Property(e => e.LicenseNumber).ValueGeneratedNever();

            entity.HasOne(d => d.LicenseNumberNavigation).WithOne(p => p.FlightInstructor)
                .HasForeignKey<FlightInstructor>(d => d.LicenseNumber)
                .HasConstraintName("FK__FlightIns__Licen__6754599E");
        });

        modelBuilder.Entity<FlightType>(entity =>
        {
            entity.HasKey(e => e.FlightType1).HasName("PK__Flight_T__4B0713BEECA9B789");

            entity.ToTable("Flight_Type");

            entity.Property(e => e.FlightType1)
                .HasMaxLength(10)
                .HasColumnName("flightType");
        });

        modelBuilder.Entity<InterestedInFlightType>(entity =>
        {
            entity.HasKey(e => new { e.FlightType, e.LicenseNumber }).HasName("PK__Interest__368D8FEA9CA48264");

            entity.ToTable("InterestedInFlightType");

            entity.Property(e => e.FlightType).HasMaxLength(10);
            entity.Property(e => e.Nothing).HasColumnName("nothing");

            entity.HasOne(d => d.FlightTypeNavigation).WithMany(p => p.InterestedInFlightTypes)
                .HasForeignKey(d => d.FlightType)
                .HasConstraintName("FK__Intereste__Fligh__628FA481");

            entity.HasOne(d => d.LicenseNumberNavigation).WithMany(p => p.InterestedInFlightTypes)
                .HasForeignKey(d => d.LicenseNumber)
                .HasConstraintName("FK__Intereste__Licen__6383C8BA");
        });
        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageNumber).HasName("PK__Message__35612F9F5FB4AD5C");

            entity.ToTable("Message");

            entity.Property(e => e.MessageNumber).HasColumnName("messageNumber");
            entity.Property(e => e.MessageText)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("messageText");

            entity.HasOne(d => d.LicenseNumberNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.LicenseNumber)
                .HasConstraintName("FK__Message__License__73BA3083");
        });
        modelBuilder.Entity<Pilot>(entity =>
        {
            entity.HasKey(e => e.LicenseNumber).HasName("PK__Pilot__E889016744E27D5C");

            entity.ToTable("Pilot");

            entity.Property(e => e.LicenseNumber).ValueGeneratedNever();
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Idimage).HasColumnName("IDImage");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.LicenseType).HasMaxLength(5);
            entity.Property(e => e.MedicalExpiry).HasColumnType("date");
            entity.Property(e => e.MivhanRama).HasColumnType("date");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.PilotStatus).HasMaxLength(13);
            entity.Property(e => e.TypeRating).HasMaxLength(7);
        });

        modelBuilder.Entity<Plane>(entity =>
        {
            entity.HasKey(e => e.RegistrationCode).HasName("PK__Plane__A94D9FECF4259719");

            entity.ToTable("Plane");

            entity.Property(e => e.RegistrationCode)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.FlightNumber).HasName("PK__Slot__2EAE6F51EECB4036");

            entity.ToTable("Slot");

            entity.Property(e => e.DepartingStrip)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.EndHobbs)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("EndHOBBS");
            entity.Property(e => e.FlightDate).HasColumnType("date");
            entity.Property(e => e.IntermediateLandingStrip)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.LandingStrip)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationCode)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.StartHobbs)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("StartHOBBS");
            entity.Property(e => e.Tach)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("TACH");

            entity.HasOne(d => d.DepartingStripNavigation).WithMany(p => p.SlotDepartingStripNavigations)
                .HasForeignKey(d => d.DepartingStrip)
                .HasConstraintName("FK__Slot__DepartingS__09A971A2");

            entity.HasOne(d => d.InstructorLicenseNumberNavigation).WithMany(p => p.Slots)
                .HasForeignKey(d => d.InstructorLicenseNumber)
                .HasConstraintName("FK__Slot__Instructor__0D7A0286");

            entity.HasOne(d => d.IntermediateLandingStripNavigation).WithMany(p => p.SlotIntermediateLandingStripNavigations)
                .HasForeignKey(d => d.IntermediateLandingStrip)
                .HasConstraintName("FK__Slot__Intermedia__0B91BA14");

            entity.HasOne(d => d.LandingStripNavigation).WithMany(p => p.SlotLandingStripNavigations)
                .HasForeignKey(d => d.LandingStrip)
                .HasConstraintName("FK__Slot__LandingStr__0A9D95DB");

          //  entity.HasOne(d => d.PilotLicenseNumberNavigation).WithMany(p => p.Slots)
            //    .HasForeignKey(d => d.PilotLicenseNumber)
              //  .OnDelete(DeleteBehavior.Cascade)
                //.HasConstraintName("FK__Slot__PilotLicen__0C85DE4D");

            entity.HasOne(d => d.RegistrationCodeNavigation).WithMany(p => p.Slots)
                .HasForeignKey(d => d.RegistrationCode)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Slot__Registrati__0E6E26BF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
