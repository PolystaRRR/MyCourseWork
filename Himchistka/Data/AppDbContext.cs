using Microsoft.EntityFrameworkCore;
using Himchistka.Models.DataBase;
using System.Diagnostics;
using NuGet.DependencyResolver;

namespace Himchistka.Data;
 

    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {

        }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Service> Services { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<PhysicalPerson> PhysicalPersons { get; set; }

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
       => optionsBuilder.UseSqlServer("Server=DESKTOP-H86OC4I\\SQLEXPRESS;Database=DryCleanerDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");


        protected override void OnModelCreating(ModelBuilder modelBuilder)

    {
            modelBuilder.Entity<PhysicalPerson>(entity => 
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("PhysicalPersons");
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.Name).HasMaxLength(30);
                entity.Property(e => e.Surname).HasMaxLength(30);
                entity.Property(e => e.MiddleName).HasMaxLength(30);
                

                entity.HasOne(p => p.Client)
                    .WithOne(s => s.PhysicalPerson)
                    .HasForeignKey<Client>(s => s.PhysicalPersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Client_PhysicalPerson");

                entity.HasOne(p => p.Employee)
                    .WithOne(t => t.PhysicalPerson)
                    .HasForeignKey<Employee>(t => t.PhysicalPersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Employee_PhysicalPerson");
            });


            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.PhysicalPersonId);
                entity.ToTable("Employees");
                entity.Property(e => e.PhysicalPersonId).ValueGeneratedNever();
                entity.Property(e => e.Post).HasMaxLength(50);


                entity.HasOne(d => d.PhysicalPerson).WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.PhysicalPersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Employee_PhysicalPerson");

                
            });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.PhysicalPersonId);
            entity.ToTable("Clients");
            entity.Property(e => e.PhysicalPersonId).ValueGeneratedNever();
            entity.Property(e => e.PhoneNumber).HasMaxLength(12);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Adress).HasMaxLength(50);


            entity.HasOne(d => d.PhysicalPerson).WithOne(p => p.Client)
                .HasForeignKey<Client>(d => d.PhysicalPersonId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Client_PhysicalPerson");
        });

        modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Products");
                entity.Property(e => e.ProductName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Size).HasMaxLength(5);
                entity.Property(e => e.DeclaredValue).HasColumnType("money");
                entity.Property(e => e.FabricType).HasMaxLength(20);
                entity.Property(e => e.Color).HasMaxLength(20);
                entity.Property(e => e.Defects).HasMaxLength(50);
                entity.Property(e => e.Label).HasMaxLength(1);

                entity.HasOne(e => e.Order)
                    .WithMany(e => e.Products)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Products");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Orders");
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.AcceptanceDate).HasColumnType("datetime");
                entity.Property(e => e.ReadyDate).HasColumnType("datetime");
                entity.Property(e => e.ReceptionPoint).HasMaxLength(50);
                entity.Property(e => e.PaymentType).HasMaxLength(10);
                entity.Property(e => e.FinalPrice).HasColumnType("money");


                entity.HasOne(e => e.Client)
                   .WithMany(e => e.Orders)
                   .HasForeignKey(e => e.ClientId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Client_Orders");
            });


            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Services");
                entity.Property(e => e.Id).UseIdentityColumn();
                
                entity.Property(e => e.ServiceName).HasMaxLength(300);
                entity.Property(e => e.ServicePrice).HasColumnType("money");
                entity.Property(e => e.ServiceDescription).HasMaxLength(300);
                entity.Property(e => e.ProcessingType).HasMaxLength(50);
                entity.Property(e => e.ResourcesExpention);


                entity.HasMany(o => o.Orders)
                .WithOne(s => s.Service)
                .HasForeignKey(o => o.ServiceId)
                .HasConstraintName("FK_Orders_Service");

                entity.HasOne(e => e.Employee)
                    .WithMany(e => e.Services)
                    .HasForeignKey(e => e.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Services");
            });
        OnModelCreatingPartial(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}



