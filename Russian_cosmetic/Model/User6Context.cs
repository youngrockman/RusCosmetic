using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Russian_cosmetic.Model;

namespace Russian_cosmetic.Model
{
    public partial class User6Context : DbContext
    {
        public User6Context() { }

        public User6Context(DbContextOptions<User6Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Individual> Individuals { get; set; }
        public virtual DbSet<Legalentity> Legalentities { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Orderservice> Orderservices { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Service> Services { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("host=45.67.56.214;port=5421;username=user6;database=user6;password=86fuew3H");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees", "public2");
                entity.HasKey(e => e.EmployeeId).HasName("employees_pkey");
                entity.Property(e => e.EmployeeId)
                      .HasColumnName("employee_id")
                      .ValueGeneratedNever();
                entity.Property(e => e.FullName)
                      .HasColumnName("full_name")
                      .HasMaxLength(100);
                entity.Property(e => e.LastLogin)
                      .HasColumnName("last_login")
                      .HasColumnType("timestamp without time zone");
                entity.Property(e => e.Login)
                      .HasColumnName("login")
                      .HasMaxLength(100);
                entity.Property(e => e.Password)
                      .HasColumnName("password")
                      .HasMaxLength(50);
                entity.Property(e => e.PositionId)
                      .HasColumnName("position_id");

                entity.HasOne(d => d.Position)
                      .WithMany(p => p.Employees)
                      .HasForeignKey(d => d.PositionId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("employees_position_id_fkey");

                entity.HasMany(d => d.Services)
                      .WithMany(p => p.Employees)
                      .UsingEntity<Dictionary<string, object>>(
                          "Employeeservice",
                          r => r.HasOne<Service>()
                                .WithMany()
                                .HasForeignKey("service_id")
                                .HasConstraintName("employeeservices_service_id_fkey"),
                          l => l.HasOne<Employee>()
                                .WithMany()
                                .HasForeignKey("employee_id")
                                .HasConstraintName("employeeservices_employee_id_fkey"),
                          j =>
                          {
                              j.ToTable("employeeservices", "public2");
                              j.HasKey("employee_id", "service_id")
                               .HasName("employeeservices_pkey");
                          });
            });

            modelBuilder.Entity<Individual>(entity =>
            {
                entity.ToTable("individuals", "public2");
                entity.HasKey(e => e.ClientCode).HasName("individuals_pkey");
                entity.Property(e => e.ClientCode)
                      .HasColumnName("client_code")
                      .HasMaxLength(10);
                entity.Property(e => e.FullName)
                      .HasColumnName("full_name")
                      .HasMaxLength(100);
                entity.Property(e => e.PassportDetails)
                      .HasColumnName("passport_details")
                      .HasMaxLength(100);
                entity.Property(e => e.BirthDate)
                      .HasColumnName("birth_date");
                entity.Property(e => e.Address)
                      .HasColumnName("address")
                      .HasMaxLength(200);
                entity.Property(e => e.Email)
                      .HasColumnName("email")
                      .HasMaxLength(100);
                entity.Property(e => e.Password)
                      .HasColumnName("password")
                      .HasMaxLength(50);
            });

            modelBuilder.Entity<Legalentity>(entity =>
            {
                entity.ToTable("legalentities", "public2");
                entity.HasKey(e => e.ClientCode).HasName("legalentities_pkey");
                entity.Property(e => e.ClientCode)
                      .HasColumnName("client_code")
                      .HasMaxLength(10);
                entity.Property(e => e.CompanyName)
                      .HasColumnName("company_name")
                      .HasMaxLength(100);
                entity.Property(e => e.Address)
                      .HasColumnName("address")
                      .HasMaxLength(200);
                entity.Property(e => e.Inn)
                      .HasColumnName("inn")
                      .HasMaxLength(12);
                entity.Property(e => e.AccountNumber)
                      .HasColumnName("account_number")
                      .HasMaxLength(20);
                entity.Property(e => e.Bik)
                      .HasColumnName("bik")
                      .HasMaxLength(9);
                entity.Property(e => e.DirectorName)
                      .HasColumnName("director_name")
                      .HasMaxLength(100);
                entity.Property(e => e.ContactPerson)
                      .HasColumnName("contact_person")
                      .HasMaxLength(100);
                entity.Property(e => e.ContactPhone)
                      .HasColumnName("contact_phone")
                      .HasMaxLength(50);
                entity.Property(e => e.Email)
                      .HasColumnName("email")
                      .HasMaxLength(100);
                entity.Property(e => e.Password)
                      .HasColumnName("password")
                      .HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders", "public2");
                entity.HasKey(e => e.OrderId).HasName("orders_pkey");
                entity.Property(e => e.OrderId)
                      .HasColumnName("order_id")
                      .ValueGeneratedNever();
                entity.Property(e => e.ClientCode)
                      .HasColumnName("client_code")
                      .HasMaxLength(10);
                entity.Property(e => e.CreationDate)
                      .HasColumnName("creation_date");
                entity.Property(e => e.ClosingDate)
                      .HasColumnName("closing_date")
                      .HasColumnType("timestamp without time zone");
                entity.Property(e => e.EmployeeId)
                      .HasColumnName("employee_id");
                entity.Property(e => e.ExecutionTime)
                      .HasColumnName("execution_time")
                      .HasMaxLength(50);
                entity.Property(e => e.OrderNumber)
                      .HasColumnName("order_number")
                      .HasMaxLength(50);
                entity.Property(e => e.Status)
                      .HasColumnName("status")
                      .HasMaxLength(50);

                entity.HasOne(d => d.Employee)
                      .WithMany(p => p.Orders)
                      .HasForeignKey(d => d.EmployeeId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("orders_employee_id_fkey");

                entity.HasMany(d => d.Orderservices)
                      .WithOne(o => o.Order)
                      .HasForeignKey(o => o.OrderId)
                      .HasConstraintName("orderservices_order_id_fkey");
            });

            modelBuilder.Entity<Orderservice>(entity =>
            {
                entity.ToTable("orderservices", "public2");
                entity.HasKey(e => new { e.OrderId, e.ServiceId })
                      .HasName("orderservices_pkey");
                
                entity.Property(e => e.OrderId)
                      .HasColumnName("order_id");
                entity.Property(e => e.ServiceId)
                      .HasColumnName("service_id");

                entity.HasOne(d => d.Order)
                      .WithMany(p => p.Orderservices)
                      .HasForeignKey(d => d.OrderId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("orderservices_order_id_fkey");

                entity.HasOne(d => d.Service)
                      .WithMany(p => p.Orderservices)
                      .HasForeignKey(d => d.ServiceId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("orderservices_service_id_fkey");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("positions", "public2");
                entity.HasKey(e => e.PositionId).HasName("positions_pkey");
                entity.Property(e => e.PositionId)
                      .HasColumnName("position_id")
                      .ValueGeneratedNever();
                entity.Property(e => e.PositionName)
                      .HasColumnName("position_name")
                      .HasMaxLength(100);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("services", "public2");
                entity.HasKey(e => e.ServiceId).HasName("services_pkey");
                entity.Property(e => e.ServiceId)
                      .HasColumnName("service_id")
                      .ValueGeneratedNever();
                entity.Property(e => e.ServiceCode)
                      .HasColumnName("service_code")
                      .HasMaxLength(20);
                entity.Property(e => e.ServiceName)
                      .HasColumnName("service_name")
                      .HasMaxLength(100);
                entity.Property(e => e.StandardPrice)
                      .HasColumnName("standard_price")
                      .HasPrecision(10, 2);
                entity.Property(e => e.SpecialPriceRuscosm)
                      .HasColumnName("special_price_ruscosm")
                      .HasPrecision(10, 2);
                entity.Property(e => e.DeviationValue)
                      .HasColumnName("deviation_value")
                      .HasPrecision(10, 4);
                entity.Property(e => e.DeviationUnit)
                      .HasColumnName("deviation_unit")
                      .HasMaxLength(20);
                entity.Property(e => e.ExecutionTime)
                      .HasColumnName("execution_time")
                      .HasMaxLength(50);

                entity.HasMany(d => d.Orderservices)
                      .WithOne(o => o.Service)
                      .HasForeignKey(d => d.ServiceId)
                      .HasConstraintName("orderservices_service_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
