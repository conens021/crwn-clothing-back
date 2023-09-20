using Microsoft.EntityFrameworkCore;
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.DAL.Context
{
    public partial class CrwnClothingContext : DbContext
    {
        public CrwnClothingContext(DbContextOptions<CrwnClothingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<ColorCollection> ColorCollections { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderProduct> OrderProducts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductsColor> ProductsColors { get; set; } = null!;
        public virtual DbSet<ProductsSize> ProductsSizes { get; set; } = null!;
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
        public virtual DbSet<ShoppingCartProduct> ShoppingCartProducts { get; set; } = null!;
        public virtual DbSet<Size> Sizes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=NEMUS\\SQLEXPRESS;Database=crwn-clothing;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(120);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.Value).HasMaxLength(250);
            });

            modelBuilder.Entity<ColorCollection>(entity =>
            {
                entity.ToTable("ColorCollection");

                entity.Property(e => e.ContentId).HasColumnName("ContentID");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.ChargedTotal).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.PaymentIntentId).HasMaxLength(350);

                entity.Property(e => e.ShippingAddress).HasMaxLength(250);

                entity.Property(e => e.ShippingCity).HasMaxLength(250);

                entity.Property(e => e.ShippingCountry).HasMaxLength(250);

                entity.Property(e => e.ShippingZipCode).HasMaxLength(250);

                entity.Property(e => e.Subtotal).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.ToTable("OrderProduct");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderProduct_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderProduct_Products");

                entity.HasOne(d => d.Size)
                   .WithMany(p => p.OrderProducts)
                   .HasForeignKey(d => d.SizeId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_OrderProduct_Sizes");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Price).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Products_Categories");

                entity.HasOne(d => d.ColorCollection)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ColorCollectionId)
                    .HasConstraintName("FK_Products_ColorCollection");

                entity.Navigation(entity => entity.Category).AutoInclude();
                entity.Navigation(entity => entity.ProductsSizes).AutoInclude();

            });

            modelBuilder.Entity<ProductsColor>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductsSize>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductsSizes)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductsSizes_Products");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.ProductsSizes)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductsSizes_Sizes");

                entity.Navigation(entity => entity.Product).AutoInclude();
                entity.Navigation(entity => entity.Size).AutoInclude();

            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.ToTable("ShoppingCart");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCart_Users");
            });

            modelBuilder.Entity<ShoppingCartProduct>(entity =>
            {
                entity.ToTable("ShoppingCartProduct");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ShoppingCartProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCartProduct_Products");

                entity.HasOne(d => d.ShoppingCart)
                    .WithMany(p => p.ShoppingCartProducts)
                    .HasForeignKey(d => d.ShoppingCartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCartProduct_ShoppingCart");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.ShoppingCartProducts)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCartProduct_Sizes");

                entity.Navigation(entity => entity.Size).AutoInclude();
                entity.Navigation(entity => entity.Product).AutoInclude();
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("Name");

                entity.Property(e => e.Value)
                   .HasColumnName("Value");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.FirstName).HasMaxLength(250);

                entity.Property(e => e.LastLoginAt).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(250);

                entity.Property(e => e.PaymentId).HasMaxLength(250);

                entity.Property(e => e.PhoneNumber).HasMaxLength(250);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.Property(e => e.Verified).HasDefaultValueSql("((0))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
