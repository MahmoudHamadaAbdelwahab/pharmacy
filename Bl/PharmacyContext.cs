using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Dominos.Bl;

namespace Dominos.Models; //DbContext 
public partial class PharmacyContext : IdentityDbContext <ApplicationUser>
{
    public PharmacyContext() { 
    }
    public PharmacyContext(DbContextOptions<PharmacyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerModel> TbCustomers { get; set; }
    public virtual DbSet<OrderItemModel> TbOrderItem { get; set; }
    public virtual DbSet<OrderModel> TbOrder { get; set; }
    public virtual DbSet<PaymentModel> TbPayment { get; set; }
    public virtual DbSet<PharmcistsModel> TbPharmcists { get; set; }
    public virtual DbSet<ProdcutsClassificationModel> TbProdcutsClassification { get; set; }
    public virtual DbSet<ProductsModel> TbProducts { get; set; }
    public virtual DbSet<SupplierModel> TbSupplier { get; set; }
    public virtual DbSet<SettingsModel> TbSettings { get; set; }
    public virtual DbSet<SliderModel> TbSlider { get; set; }
    public virtual DbSet<ImagesModel> TbImages { get; set; }

    public virtual DbSet<VmOrder> VmItemOrder { get; set; }
    public virtual DbSet<VwProducts> VmProductsImages { get; set; }

    //public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //optionsBuilder.UseSqlServer("Server=DESKTOP-J2S0GA5;Database=LapShop;Trusted_Connection=True;");
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // when use Identity EntityFrameworkCore 

        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<VmOrder>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("VmItemOrder"); // same name create in db 
        });
      
        modelBuilder.Entity<VwProducts>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("VmProductsImages"); // same name create in db 
            entity.Property(e => e.ProductName).HasMaxLength(100);

            entity.Property(e => e.PharmcistName).HasMaxLength(200);

            entity.Property(e => e.SupplierName).HasMaxLength(100);

            entity.Property(e => e.StockQuentity).HasMaxLength(100);

            entity.Property(e => e.ProdcutsClassificationName).HasMaxLength(100);

            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");

            entity.Property(e => e.SalesPrice).HasColumnType("decimal(8, 2)");
        });

        modelBuilder.Entity<SettingsModel>(entity =>
        {
            entity.HasKey(e => e.SettingsId);

        });
       
        modelBuilder.Entity<SliderModel>(entity =>
        {
            entity.HasKey(e => e.SliderId);
        });
       
        modelBuilder.Entity<CustomerModel>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.Property(e => e.CustomerName).HasMaxLength(100);
        });

        modelBuilder.Entity<OrderItemModel>(entity =>
        {
            entity.HasKey(a => a.OrderItemId);

            // create forign key one to one
            entity.HasOne(a => a.TbProducts) // it's object from OrderItemModel
            .WithOne(a => a.TbOrderItem) // it's list from OrderModel 
            .HasForeignKey<OrderItemModel>(c => c.ProductsId); // it's forign key inside OrderItemModel

            // create forign key meny to one
            entity.HasOne(a => a.TbOrder) // it's object from OrderItemModel
            .WithMany(a => a.TbOrderItem) // it's list from OrderModel 
            .HasForeignKey(c => c.OrderId); // it's forign key inside OrderItemModel
        });

        modelBuilder.Entity<OrderModel>(entity =>
        {
            entity.HasKey(e => e.OrderId);
            // create forign key one to one
            entity.HasOne(a => a.TbPayment) // it's object from OrderModel
            .WithOne(a => a.TbOrder) // it's list from PaymentModel 
            .HasForeignKey<OrderModel>(c => c.PaymentId); // it's forign key inside OrderModel

            // create forign key meny to one
            entity.HasOne(a => a.TbCustomer) // it's object from OrderModel
            .WithMany(a => a.TbOrder) // it's list from CustomerModel 
            .HasForeignKey(c => c.CustomerId); // it's forign key inside OrderModel
        });
              
        modelBuilder.Entity<PaymentModel>(entity =>
        {
            entity.HasKey(e => e.PaymentId);

            entity.Property(e => e.PaymentMethod).HasMaxLength(100);
        });

        modelBuilder.Entity<ProductsModel>(entity =>
        {
            entity.Property(a => a.Description).HasMaxLength(2000);

            // create forign key meny to one
            entity.HasOne(a => a.TbProdcutsClassification) // it's object from ProductsModel
            .WithMany(a => a.TbProducts) // it's list from ProdcutsClassificationModel 
            .HasForeignKey(c => c.ProdcutsClassificationId); // it's forign key inside ProductsModel
           
            entity.HasOne(a => a.TbPharmcists) // it's object from EmployeeModels , it's express the object from personalModel
            .WithMany(a => a.TbProducts) // it's list from PharmcistsModel
            .HasForeignKey(c => c.PharmcistId); // should be select from key any table 
           
            // create forign key one to one 
            entity.HasOne(a => a.TbSupplier) // it's object from ProductsModel , it's express the object from personalModel
            .WithOne(a => a.TbProducts) // it's list from SupplierlModel  
            .HasForeignKey<ProductsModel>(c => c.SupplierId); // should be select from key any table , from class ProductsModel
        });

        modelBuilder.Entity<ImagesModel>(entity =>
        {
            // create forign key meny to one
            entity.HasOne(a => a.TbProducts) // it's object from ImagesModel
            .WithMany(a => a.TbImages) // it's list from ProductsModel 
            .HasForeignKey(c => c.ProductId); // it's forign key inside ImagesModel
        });

        //modelBuilder.Entity<ApplicationUser>()
        //    .Property(u => u.FirstName)
        //    .HasColumnName("FirstName");
 
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
