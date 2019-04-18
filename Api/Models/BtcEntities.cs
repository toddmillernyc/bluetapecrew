using Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class BtcEntities : DbContext
    {
        public BtcEntities() { }
        public BtcEntities(DbContextOptions<BtcEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AzImages> AzImages { get; set; }
        public virtual DbSet<BannerImages> BannerImages { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartImages> CartImages { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<CategoryImages> CategoryImages { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<GuestUsers> GuestUsers { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<LayerSliders> LayerSliders { get; set; }
        public virtual DbSet<MailSettings> MailSettings { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<OpenGraphTags> OpenGraphTags { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PageOpenGraphTags> PageOpenGraphTags { get; set; }
        public virtual DbSet<Pages> Pages { get; set; }
        public virtual DbSet<PayPalPayments> PayPalPayments { get; set; }
        public virtual DbSet<ProductCategories> ProductCategories { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<SiteSetting> SiteSettings { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Style> Styles { get; set; }
        public virtual DbSet<StyleView> StyleViews { get; set; }
        public virtual DbSet<TodoItems> TodoItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
                    .HasName("PK_dbo.AspNetUserLogins");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_dbo.AspNetUserRoles");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.RoleId).HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(75);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(60);

                entity.Property(e => e.LastName).HasMaxLength(60);

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.Nickname)
                    .HasColumnName("nickname")
                    .HasMaxLength(25);

                entity.Property(e => e.PostalCode).HasMaxLength(20);

                entity.Property(e => e.State).HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AzImages>(entity =>
            {
                entity.HasIndex(e => e.ProductId)
                    .HasName("UQ_AzImages_ProductId")
                    .IsUnique();

                entity.Property(e => e.ImageData).IsRequired();

                entity.HasOne(d => d.Product)
                    .WithOne(p => p.AzImages)
                    .HasForeignKey<AzImages>(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AzImages_ProductId");
            });

            modelBuilder.Entity<BannerImages>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ_BannerImages_Name")
                    .IsUnique();

                entity.Property(e => e.ImageData).IsRequired();

                entity.Property(e => e.MimeType)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasIndex(e => new { e.CartId, e.StyleId })
                    .HasName("UQ_Cart_CartId_StyleId")
                    .IsUnique();

                entity.Property(e => e.CartId)
                    .IsRequired()
                    .HasMaxLength(68)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.StyleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_StyleId");
            });

            modelBuilder.Entity<CartImages>(entity =>
            {
                entity.Property(e => e.ImageData).IsRequired();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartImages)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartImages_ProductId");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasIndex(e => e.CategoryName)
                    .HasName("UQ_Categories_CategoryName")
                    .IsUnique();

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ImageUrl).HasMaxLength(2083);

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_Categories_ImageId");
            });

            modelBuilder.Entity<CategoryImages>(entity =>
            {
                entity.Property(e => e.MimeType).HasMaxLength(255);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryImages)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryImages_CategoryId");
            });

            modelBuilder.Entity<Colors>(entity =>
            {
                entity.HasIndex(e => e.ColorText)
                    .HasName("UQ_Colors_ColorText")
                    .IsUnique();

                entity.Property(e => e.ColorText)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<GuestUsers>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(75);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(60);

                entity.Property(e => e.LastName).HasMaxLength(60);

                entity.Property(e => e.PostalCode).HasMaxLength(20);

                entity.Property(e => e.SessionId)
                    .HasMaxLength(24)
                    .IsUnicode(false);

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

            modelBuilder.Entity<LayerSliders>(entity =>
            {
                entity.HasOne(d => d.BannerImage)
                    .WithMany(p => p.LayerSliders)
                    .HasForeignKey(d => d.BannerImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LayerSliders_BannerImageId");
            });

            modelBuilder.Entity<MailSettings>(entity =>
            {
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SmtpServer)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<OpenGraphTags>(entity =>
            {
                entity.HasIndex(e => e.Property)
                    .HasName("UQ_OpenGraphTags_Tag")
                    .IsUnique();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Property)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.Property(e => e.SubTotal).HasColumnType("smallmoney");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItems_OrderId");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.City).HasMaxLength(255);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(255);

                entity.Property(e => e.SessionId)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.Shipping).HasColumnType("smallmoney");

                entity.Property(e => e.State).HasMaxLength(255);

                entity.Property(e => e.SubTotal).HasColumnType("smallmoney");

                entity.Property(e => e.Total).HasColumnType("smallmoney");

                entity.Property(e => e.UserName).HasMaxLength(255);

                entity.Property(e => e.Zip).HasMaxLength(255);
            });

            modelBuilder.Entity<PageOpenGraphTags>(entity =>
            {
                entity.HasKey(e => new { e.PageId, e.OpenGraphTagId })
                    .HasName("PK_PageOpenGraphTags_PageId_OpenGraphTagId");

                entity.Property(e => e.Content).IsRequired();

                entity.HasOne(d => d.OpenGraphTag)
                    .WithMany(p => p.PageOpenGraphTags)
                    .HasForeignKey(d => d.OpenGraphTagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PageOpenGraphTags_OpenGraphTagId");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageOpenGraphTags)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PageOpenGraphTags_PageId");
            });

            modelBuilder.Entity<Pages>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ_Pages_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
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

            modelBuilder.Entity<ProductCategories>(entity =>
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
                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Rating)
                    .HasColumnType("decimal(18, 0)")
                    .HasDefaultValueSql("((5))");

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

                entity.Property(e => e.TwitterWidgetId).HasMaxLength(50);
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

            modelBuilder.Entity<TodoItems>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_dbo.TodoItems")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.CreatedAt)
                    .HasName("IX_CreatedAt")
                    .ForSqlServerIsClustered();

                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");

                entity.Property(e => e.Version)
                    .IsRequired()
                    .IsRowVersion();
            });
        }
    }
}
