using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DataAccessLayer.Entities;

#nullable disable

namespace DataAccessLayer.Services
{
    public partial class CPDbContext : DbContext
    {
        public CPDbContext()
        {
        }

        public CPDbContext(DbContextOptions<CPDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<CollectionItem> CollectionItems { get; set; }
        public virtual DbSet<CollectionOptionalField> CollectionOptionalFields { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<FieldType> FieldTypes { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemComment> ItemComments { get; set; }
        public virtual DbSet<ItemLike> ItemLikes { get; set; }
        public virtual DbSet<ItemOptionalField> ItemOptionalFields { get; set; }
        public virtual DbSet<ItemTag> ItemTags { get; set; }
        public virtual DbSet<OptionalField> OptionalFields { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-HDG3Q6R\\SQLEXPRESS;Initial Catalog=CPDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Collection>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.ImageUrl).HasMaxLength(512);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Collections)
                    .HasForeignKey(d => d.CreatorId)
                    .HasConstraintName("FK_Collections_To_Users");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.Collections)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Collections_To_Themes");
            });

            modelBuilder.Entity<CollectionItem>(entity =>
            {
                entity.ToTable("CollectionItem");

                entity.HasOne(d => d.Collection)
                    .WithMany(p => p.CollectionItems)
                    .HasForeignKey(d => d.CollectionId)
                    .HasConstraintName("FK_CollectionItem_To_Collections");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.CollectionItems)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_CollectionItem_To_Items");
            });

            modelBuilder.Entity<CollectionOptionalField>(entity =>
            {
                entity.ToTable("CollectionOptionalField");

                entity.HasOne(d => d.Collection)
                    .WithMany(p => p.CollectionOptionalFields)
                    .HasForeignKey(d => d.CollectionId)
                    .HasConstraintName("FK_CollectionOptionalField_To_Collections");

                entity.HasOne(d => d.OptionalField)
                    .WithMany(p => p.CollectionOptionalFields)
                    .HasForeignKey(d => d.OptionalFieldId)
                    .HasConstraintName("FK_CollectionOptionalField_To_OptionalFields");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Value).IsRequired();
            });

            modelBuilder.Entity<FieldType>(entity =>
            {
                entity.HasIndex(e => e.Type, "UQ_FieldTypes_Type")
                    .IsUnique();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ImageUrl).HasMaxLength(512);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Items_To_Users");
            });

            modelBuilder.Entity<ItemComment>(entity =>
            {
                entity.ToTable("ItemComment");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.ItemComments)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK_ItemComment_To_Comments");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemComments)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_ItemComment_To_Items");
            });

            modelBuilder.Entity<ItemLike>(entity =>
            {
                entity.ToTable("ItemLike");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemLikes)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_ItemLike_To_Items");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ItemLikes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ItemLike_To_Users");
            });

            modelBuilder.Entity<ItemOptionalField>(entity =>
            {
                entity.ToTable("ItemOptionalField");

                entity.Property(e => e.Value).HasMaxLength(1024);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemOptionalFields)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_ItemOptionalField_To_Items");

                entity.HasOne(d => d.OptionalField)
                    .WithMany(p => p.ItemOptionalFields)
                    .HasForeignKey(d => d.OptionalFieldId)
                    .HasConstraintName("FK_ItemOptionalField_To_OptionalFields");
            });

            modelBuilder.Entity<ItemTag>(entity =>
            {
                entity.ToTable("ItemTag");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemTags)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_ItemTag_To_Items");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.ItemTags)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK_ItemTag_To_Tags");
            });

            modelBuilder.Entity<OptionalField>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.OptionalFields)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OptionalFields_To_FieldTypes");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Theme>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ_Themes_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
