using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Olahrago.ApiLayer.Model
{
    public partial class OlahragoContext : DbContext
    {
        public OlahragoContext()
        {
        }

        public OlahragoContext(DbContextOptions<OlahragoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<ApplicationMessage> ApplicationMessage { get; set; }
        public virtual DbSet<Balance> Balance { get; set; }
        public virtual DbSet<Court> Court { get; set; }
        public virtual DbSet<Facilities> Facilities { get; set; }
        public virtual DbSet<Owner> Owner { get; set; }
        public virtual DbSet<Playground> Playground { get; set; }
        public virtual DbSet<PlaygroundFacilities> PlaygroundFacilities { get; set; }
        public virtual DbSet<PlaygroundImage> PlaygroundImage { get; set; }
        public virtual DbSet<Tokens> Tokens { get; set; }
        public virtual DbSet<TransactionBooking> TransactionBooking { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseNpgsqlIdentityAlwaysColumn();

                entity.Property(e => e.AccountType).HasColumnName("account_type");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedTime).HasColumnName("created_time");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(32);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<ApplicationMessage>(entity =>
            {
                entity.ToTable("application_message");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseNpgsqlIdentityAlwaysColumn();

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasMaxLength(100);

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Balance>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.ToTable("balance");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Balance1).HasColumnName("balance");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedTime).HasColumnName("updated_time");
            });

            modelBuilder.Entity<Court>(entity =>
            {
                entity.ToTable("court");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseNpgsqlIdentityAlwaysColumn();

                entity.Property(e => e.CourtType).HasColumnName("court_type");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnName("created_date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.PlaygroundId).HasColumnName("playground_id");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.Property(e => e.SizeType).HasColumnName("size_type");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            });

            modelBuilder.Entity<Facilities>(entity =>
            {
                entity.ToTable("facilities");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.ToTable("owner");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(150);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedTime).HasColumnName("created_time");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd()
                    .UseNpgsqlIdentityAlwaysColumn();

                entity.Property(e => e.IdentityNumber).HasColumnName("identity_number");

                entity.Property(e => e.IdentityType).HasColumnName("identity_type");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(15);

                entity.HasOne(d => d.Account)
                    .WithOne(p => p.Owner)
                    .HasForeignKey<Owner>(d => d.AccountId)
                    .HasConstraintName("owner_account_fk");
            });

            modelBuilder.Entity<Playground>(entity =>
            {
                entity.ToTable("playground");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseNpgsqlIdentityAlwaysColumn();

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(100);

                entity.Property(e => e.Contact)
                    .IsRequired()
                    .HasColumnName("contact")
                    .HasMaxLength(15);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedTime).HasColumnName("created_time");

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasColumnName("district")
                    .HasMaxLength(40);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasColumnName("province")
                    .HasMaxLength(40);

                entity.Property(e => e.Regency)
                    .IsRequired()
                    .HasColumnName("regency")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<PlaygroundFacilities>(entity =>
            {
                entity.ToTable("playground_facilities");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.FacilityId).HasColumnName("facility_id");

                entity.Property(e => e.PlaygroundId).HasColumnName("playground_id");
            });

            modelBuilder.Entity<PlaygroundImage>(entity =>
            {
                entity.ToTable("playground_image");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedTime).HasColumnName("created_time");

                entity.Property(e => e.FileName)
                    .HasColumnName("file_name")
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Path)
                    .HasColumnName("path")
                    .HasMaxLength(255);

                entity.Property(e => e.PlaygroundId).HasColumnName("playground_id");
            });

            modelBuilder.Entity<Tokens>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.ToTable("tokens");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ExpiredDate).HasColumnName("expired_date");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TransactionBooking>(entity =>
            {
                entity.ToTable("transaction_booking");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.BookingDate)
                    .HasColumnName("booking_date")
                    .HasColumnType("date");

                entity.Property(e => e.BookingNumber)
                    .IsRequired()
                    .HasColumnName("booking_number")
                    .HasMaxLength(30);

                entity.Property(e => e.BookingStatus).HasColumnName("booking_status");

                entity.Property(e => e.BookingType).HasColumnName("booking_type");

                entity.Property(e => e.CourtId).HasColumnName("court_id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedTime).HasColumnName("created_time");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("time without time zone");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.ToTable("user");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedTime).HasColumnName("created_time");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd()
                    .UseNpgsqlIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(15);

                entity.HasOne(d => d.Account)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.AccountId)
                    .HasConstraintName("user_account_fk");
            });
        }
    }
}
