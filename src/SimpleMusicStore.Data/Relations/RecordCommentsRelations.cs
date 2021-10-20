using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleMusicStore.Entities;

namespace SimpleMusicStore.Data.Relations
{
    class RecordCommentsRelations : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(c => c.Record)
                .WithMany(r => r.Comments)
                .HasForeignKey(c => c.RecordId);
        }
    }
}
