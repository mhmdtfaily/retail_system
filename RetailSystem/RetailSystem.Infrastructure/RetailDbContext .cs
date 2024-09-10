using Microsoft.EntityFrameworkCore;
using RetailSystem.Core;
using RetailSystem.Core.Entities;

namespace RetailSystem.Infrastructure;

public class RetailDbContext : DbContext
{
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SupplierStatus> SupplierStatuses { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public DbSet<PurchaseOrderStatus> PurchaseOrderStatuses { get; set; }
    public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<ReceiptItem> ReceiptItems { get; set; }
    public DbSet<ItemLedger> ItemLedgers { get; set; }

    public RetailDbContext(DbContextOptions<RetailDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Seed SupplierStatus table
        modelBuilder.Entity<SupplierStatus>().HasData(
            new SupplierStatus { Id = Guid.NewGuid(), MachineName = "active", DisplayName = "Active" },
            new SupplierStatus { Id = Guid.NewGuid(), MachineName = "in_active", DisplayName = "Inactive" }
        );

        // Seed PurchaseOrderStatus table
        modelBuilder.Entity<PurchaseOrderStatus>().HasData(
            new PurchaseOrderStatus { Id = Guid.NewGuid(), MachineName = "pending", DisplayName = "Pending" },
            new PurchaseOrderStatus { Id = Guid.NewGuid(), MachineName = "completed", DisplayName = "Completed" },
            new PurchaseOrderStatus { Id = Guid.NewGuid(), MachineName = "canceled", DisplayName = "Canceled" }
        );


        base.OnModelCreating(modelBuilder);

    }
}
