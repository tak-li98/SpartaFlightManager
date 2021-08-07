using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database
{
    public partial class SpartaFlightContext : DbContext
    {
        public SpartaFlightContext()
        {
        }

        public SpartaFlightContext(DbContextOptions<SpartaFlightContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Airline> Airlines { get; set; }
        public virtual DbSet<Airport> Airports { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<FlightDetail> FlightDetails { get; set; }
        public virtual DbSet<FlightPath> FlightPaths { get; set; }
        public virtual DbSet<FlightStatus> FlightStatuses { get; set; }
        public virtual DbSet<Pilot> Pilots { get; set; }
        public virtual DbSet<Plane> Planes { get; set; }
        public virtual DbSet<Region> Regions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SpartaFlight;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Airline>(entity =>
            {
                entity.Property(e => e.AirlineId).HasColumnName("airlineId");

                entity.Property(e => e.AirlineCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("airlineCode");

                entity.Property(e => e.AirlineName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("airlineName");

                entity.Property(e => e.RegionId).HasColumnName("regionId");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Airlines)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Airlines__region__36B12243");
            });

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.Property(e => e.AirportId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("airportId");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.RegionId).HasColumnName("regionId");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Airports)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK__Airports__region__35BCFE0A");
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.Property(e => e.FlightId).HasColumnName("flightId");

                entity.Property(e => e.FlightDate)
                    .HasColumnType("datetime")
                    .HasColumnName("flightDate");

                entity.Property(e => e.FlightStatusId).HasColumnName("flightStatusId");

                entity.HasOne(d => d.FlightStatus)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.FlightStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Flights__flightS__4222D4EF");
            });

            modelBuilder.Entity<FlightDetail>(entity =>
            {
                entity.HasKey(e => e.FlightDetailsId)
                    .HasName("PK__FlightDe__9D69E3FB75B16380");

                entity.Property(e => e.FlightDetailsId).HasColumnName("flightDetailsId");

                entity.Property(e => e.AirlineId).HasColumnName("airlineId");

                entity.Property(e => e.Archive).HasColumnName("archive");

                entity.Property(e => e.FlightDuration).HasColumnName("flightDuration");

                entity.Property(e => e.FlightId).HasColumnName("flightId");

                entity.Property(e => e.PassengerNumber).HasColumnName("passengerNumber");

                entity.Property(e => e.PilotId).HasColumnName("pilotId");

                entity.Property(e => e.PlaneId).HasColumnName("planeId");

                entity.HasOne(d => d.Airline)
                    .WithMany(p => p.FlightDetails)
                    .HasForeignKey(d => d.AirlineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FlightDet__airli__2F10007B");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.FlightDetails)
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FlightDet__fligh__412EB0B6");

                entity.HasOne(d => d.Pilot)
                    .WithMany(p => p.FlightDetails)
                    .HasForeignKey(d => d.PilotId)
                    .HasConstraintName("FK__FlightDet__pilot__2E1BDC42");

                entity.HasOne(d => d.Plane)
                    .WithMany(p => p.FlightDetails)
                    .HasForeignKey(d => d.PlaneId)
                    .HasConstraintName("FK__FlightDet__plane__300424B4");
            });

            modelBuilder.Entity<FlightPath>(entity =>
            {
                entity.HasKey(e => new { e.FlightId, e.AirportId })
                    .HasName("PK__FlightPa__F2843BB93B46B132");

                entity.Property(e => e.FlightId).HasColumnName("flightId");

                entity.Property(e => e.AirportId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("airportId");

                entity.Property(e => e.IsDepartElseArrival).HasColumnName("isDepartElseArrival");

                entity.HasOne(d => d.Airport)
                    .WithMany(p => p.FlightPaths)
                    .HasForeignKey(d => d.AirportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FlightPat__airpo__4AB81AF0");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.FlightPaths)
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FlightPat__fligh__49C3F6B7");
            });

            modelBuilder.Entity<FlightStatus>(entity =>
            {
                entity.Property(e => e.FlightStatusId).HasColumnName("flightStatusId");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<Pilot>(entity =>
            {
                entity.Property(e => e.PilotId).HasColumnName("pilotId");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.PhotoLink)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("photoLink");

                entity.Property(e => e.Title)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Plane>(entity =>
            {
                entity.Property(e => e.PlaneId).HasColumnName("planeId");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.PlaneModel)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("planeModel");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.RegionId).HasColumnName("regionId");

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("regionName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
