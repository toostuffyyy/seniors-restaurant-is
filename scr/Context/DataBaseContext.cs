using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Seniors.Models;

namespace Seniors.Context;

public partial class DataBaseContext : DbContext
{
    public DataBaseContext()
    {
    }

    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<EmployeeRest> EmployeeRests { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<TypeDish> TypeDishes { get; set; }

    public virtual DbSet<UserOrder> UserOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-S9N70VV\\EVGENIY_LYKHOV;DataBase=UP02;User Id=Evgeniy_Lykhov;Password=1;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dish>(entity =>
        {
            entity.ToTable("Dish");

            entity.Property(e => e.Image).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(150);
            entity.Property(e => e.Price).HasMaxLength(50);

            entity.HasMany(d => d.TypeDishes).WithMany(p => p.Dishes)
                .UsingEntity<Dictionary<string, object>>(
                    "DishTypeDish",
                    r => r.HasOne<TypeDish>().WithMany()
                        .HasForeignKey("TypeDishId")
                        .HasConstraintName("FK_DishTypeDish_TypeDish"),
                    l => l.HasOne<Dish>().WithMany()
                        .HasForeignKey("DishId")
                        .HasConstraintName("FK_DishTypeDish_Dish"),
                    j =>
                    {
                        j.HasKey("DishId", "TypeDishId");
                        j.ToTable("DishTypeDish");
                    });
        });

        modelBuilder.Entity<EmployeeRest>(entity =>
        {
            entity.ToTable("EmployeeRest");

            entity.Property(e => e.Image).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);

            entity.HasOne(d => d.Post).WithMany(p => p.EmployeeRests)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_EmployeeRest_Post");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("Post");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TypeDish>(entity =>
        {
            entity.ToTable("TypeDish");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<UserOrder>(entity =>
        {
            entity.ToTable("UserOrder");

            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Emloyee).WithMany(p => p.UserOrders)
                .HasForeignKey(d => d.EmloyeeId)
                .HasConstraintName("FK_UserOrder_EmployeeRest");

            entity.HasMany(d => d.Dishes).WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderDish",
                    r => r.HasOne<Dish>().WithMany()
                        .HasForeignKey("DishId")
                        .HasConstraintName("FK_OrderDish_Dish"),
                    l => l.HasOne<UserOrder>().WithMany()
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_OrderDish_UserOrder"),
                    j =>
                    {
                        j.HasKey("OrderId", "DishId");
                        j.ToTable("OrderDish");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
