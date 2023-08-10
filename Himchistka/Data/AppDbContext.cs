using Microsoft.EntityFrameworkCore;
using Himchistka.Models.DataBase;
using System.Diagnostics;

namespace Himchistka.Data;
 

    public class AppDbContext : DbContext
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
       => optionsBuilder.UseSqlServer("Server=DESKTOP-3ETDS8H\\SQLEXPRESS;Database=DryCleanerDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.PhysicalPersonId);

                entity.ToTable("Client");

                entity.Property(e => e.PhysicalPersonId).ValueGeneratedNever();
                entity.Property(e => e.PhoneNumber).HasMaxLength(11);
                entity.Property(e => e.Email).HasMaxLength(20);
                entity.Property(e => e.Adress).HasMaxLength(50);

                entity.HasOne(d => d.PhysicalPerson).WithOne(p => p.Client)
                    .HasForeignKey<Client>(d => d.PhysicalPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Client_PhysicalPerson");
            });




            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.PhysicalPersonId);

                entity.ToTable("Employee");

                entity.Property(e => e.PhysicalPersonId).ValueGeneratedNever();

                entity.Property(e => e.Post).HasMaxLength(20);


                entity.HasOne(d => d.PhysicalPerson).WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.PhysicalPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_PhysicalPerson");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProductName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.DeclaredValue).HasColumnType("money");
                entity.Property(e => e.FabricType).HasMaxLength(20);

                entity.Property(e => e.Color).HasMaxLength(20);

                entity.Property(e => e.Defects).HasMaxLength(20);
                entity.Property(e => e.Label).HasMaxLength(1);

                entity.HasOne(e => e.Order)
                    .WithMany(e => e.Products)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Order");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.AcceptanceDate).HasColumnType("datetime");
                entity.Property(e => e.ReadyDate).HasColumnType("datetime");
                entity.Property(e => e.ReceptionPoint).HasMaxLength(50);
                entity.Property(e => e.FinalPrice).HasColumnType("money");


                entity.HasOne(d => d.Service).WithOne(p => p.Order)
                    .HasForeignKey<Order>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Service");

                entity.HasOne(e => e.Client)
                   .WithMany(e => e.Orders)
                   .HasForeignKey(e => e.ClientId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("FK_Client_Orders");
            });


            modelBuilder.Entity<Service>(entity =>
            {


                entity.HasKey(e => e.Id);
                entity.Property(e => e.ServiceName).HasMaxLength(50);
                entity.Property(e => e.ProcessingType).HasMaxLength(10);
                entity.Property(e => e.ServiceDescription).HasMaxLength(50);
                entity.Property(e => e.ResourcesExpention).HasMaxLength(50);




                entity.HasOne(e => e.Employee)
                    .WithMany(e => e.Services)
                    .HasForeignKey(e => e.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Employee_Services");
            });
        //OnModelCreatingPartial(modelBuilder);
        }
      // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }



