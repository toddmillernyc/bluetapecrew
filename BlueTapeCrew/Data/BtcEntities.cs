
using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlueTapeCrew.Data
{
    public partial class BtcEntities : IdentityDbContext<ApplicationUser>
    {
        public BtcEntities() { }
        public BtcEntities(DbContextOptions<BtcEntities> options) : base(options) { }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartImage> CartImages { get; set; }
        public virtual DbSet<CartView> CartViews { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<GuestUser> GuestUsers { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<PayPalPayments> PayPalPayments { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<SiteSetting> SiteSettings { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<StyleView> StyleViews { get; set; }
        public virtual DbSet<Style> Styles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasIndex(e => new { e.CartId, e.StyleId })
                    .HasName("UQ_Cart_CartId_StyleId")
                    .IsUnique();

                entity.Property(e => e.CartId)
                    .IsRequired()
                    .HasMaxLength(68)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.StyleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_StyleId");
            });

            modelBuilder.Entity<CartImage>(entity =>
            {
                entity.Property(e => e.ImageData).IsRequired();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartImages)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartImages_ProductId");
            });

            modelBuilder.Entity<CartView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CartView");

                entity.Property(e => e.CartId)
                    .IsRequired()
                    .HasMaxLength(68)
                    .IsUnicode(false);

                entity.Property(e => e.ColorText)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.LinkName).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StyleText)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.SubTotal).HasColumnType("smallmoney");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.CategoryName)
                    .HasName("UQ_Categories_CategoryName")
                    .IsUnique();

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_Categories_ImageId");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasIndex(e => e.ColorText)
                    .HasName("UQ_Colors_ColorText")
                    .IsUnique();

                entity.Property(e => e.ColorText)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<GuestUser>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(75);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(60);

                entity.Property(e => e.LastName).HasMaxLength(60);

                entity.Property(e => e.PostalCode).HasMaxLength(20);

                entity.Property(e => e.SessionId)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.State).HasMaxLength(50);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ_Imagess_Name")
                    .IsUnique();

                entity.Property(e => e.ImageData).IsRequired();

                entity.Property(e => e.MimeType)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.Property(e => e.SubTotal).HasColumnType("smallmoney");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItems_OrderId");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.City).HasMaxLength(255);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(255);

                entity.Property(e => e.SessionId)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Shipping).HasColumnType("smallmoney");

                entity.Property(e => e.State).HasMaxLength(255);

                entity.Property(e => e.SubTotal).HasColumnType("smallmoney");

                entity.Property(e => e.Total).HasColumnType("smallmoney");

                entity.Property(e => e.UserName).HasMaxLength(255);

                entity.Property(e => e.Zip).HasMaxLength(255);
            });

            modelBuilder.Entity<PayPalPayments>(entity =>
            {
                entity.Property(e => e.Amt)
                    .HasColumnName("amt")
                    .HasMaxLength(255);

                entity.Property(e => e.Cc)
                    .HasColumnName("cc")
                    .HasMaxLength(255);

                entity.Property(e => e.Tx)
                    .HasColumnName("tx")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.ProductId })
                    .HasName("PK_ProductCategories_CategoryId_ProductId");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCategories_CategoryId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCategories_ProductId");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => new { e.ImageId, e.ProductId })
                    .HasName("PK_ProductImages_ImageId_ProductId");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductImages_ImageId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductImages_ProductId");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.LinkName).HasMaxLength(255);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_Products_ImageId");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Rating).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ReviewText).IsRequired();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reviews_ProductId");
            });

            modelBuilder.Entity<SiteSetting>(entity =>
            {
                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ContactEmailAddress).HasMaxLength(255);

                entity.Property(e => e.ContactPhoneNumber).HasMaxLength(255);

                entity.Property(e => e.CopyrightLinktext).HasMaxLength(255);

                entity.Property(e => e.CopyrightText).HasMaxLength(255);

                entity.Property(e => e.CopyrightUrl).HasMaxLength(255);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.FaceBookUrl).HasMaxLength(255);

                entity.Property(e => e.FacebookAppId).HasMaxLength(255);

                entity.Property(e => e.FacebookClientId).HasMaxLength(255);

                entity.Property(e => e.FacebookClientSecret).HasMaxLength(255);

                entity.Property(e => e.FlatShippingRate).HasColumnType("money");

                entity.Property(e => e.FreeShippingThreshold).HasColumnType("money");

                entity.Property(e => e.GoogleClientId).HasMaxLength(255);

                entity.Property(e => e.GoogleClientSecret).HasMaxLength(255);

                entity.Property(e => e.InstagramClientId).HasMaxLength(255);

                entity.Property(e => e.InstagramClientSecret).HasMaxLength(255);

                entity.Property(e => e.Keywords).IsRequired();

                entity.Property(e => e.LinkedInUrl).HasMaxLength(255);

                entity.Property(e => e.MailChimpApiKey).HasMaxLength(255);

                entity.Property(e => e.MailChimpListId).HasMaxLength(50);

                entity.Property(e => e.MicrosoftClientId).HasMaxLength(255);

                entity.Property(e => e.MicrosoftClientSecret).HasMaxLength(255);

                entity.Property(e => e.PaypalApiUsername).HasMaxLength(255);

                entity.Property(e => e.PaypalClientId).HasMaxLength(255);

                entity.Property(e => e.PaypalClientSecret).HasMaxLength(255);

                entity.Property(e => e.PaypalEndpointUrl).HasMaxLength(255);

                entity.Property(e => e.PaypalSandBoxClientId).HasMaxLength(255);

                entity.Property(e => e.PaypalSandBoxSecret).HasMaxLength(255);

                entity.Property(e => e.PaypalSandboxAccount).HasMaxLength(255);

                entity.Property(e => e.SiteLogoUrl).HasMaxLength(255);

                entity.Property(e => e.SiteTitle)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SiteUrl).HasMaxLength(255);

                entity.Property(e => e.SmtpHost).HasMaxLength(255);

                entity.Property(e => e.SmtpPassword).HasMaxLength(255);

                entity.Property(e => e.SmtpUsername).HasMaxLength(255);

                entity.Property(e => e.TwitterClientId).HasMaxLength(255);

                entity.Property(e => e.TwitterClientSecret).HasMaxLength(255);

                entity.Property(e => e.TwitterUrl).HasMaxLength(255);
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.HasIndex(e => e.SizeOrder)
                    .HasName("UQ_Sizes_SizeOrder")
                    .IsUnique();

                entity.HasIndex(e => e.SizeText)
                    .HasName("UQ_Sizes_SizeText")
                    .IsUnique();

                entity.Property(e => e.SizeText)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<StyleView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("StyleView");

                entity.Property(e => e.ColorText)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.Property(e => e.SizeText)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.StyleText)
                    .IsRequired()
                    .HasMaxLength(48);
            });

            modelBuilder.Entity<Style>(entity =>
            {
                entity.HasIndex(e => new { e.ColorId, e.ProductId, e.SizeId })
                    .HasName("UQ_Styles_ColorId_ProductId_SizeId")
                    .IsUnique();

                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Styles)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Styles_ColorId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Styles)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Styles_ProductId");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Styles)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Styles_SizeId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
