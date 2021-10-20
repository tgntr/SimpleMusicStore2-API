using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleMusicStore.Entities;

namespace SimpleMusicStore.Data.Relations
{
    class ItemRelations : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder
                .HasKey(i => new { i.RecordId, i.OrderId });

            builder
                .HasOne(i => i.Record)
                .WithMany(r => r.Orders)
                .HasForeignKey(i => i.RecordId);

            builder
                .HasOne(i => i.Order)
                .WithMany(u => u.Items)
                .HasForeignKey(i => i.OrderId);
        }
    }
}
