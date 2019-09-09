using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Data.Relations
{
    class RecordCommentsRelations : IEntityTypeConfiguration<RecordComment>
    {
        public void Configure(EntityTypeBuilder<RecordComment> builder)
        {
            builder.HasOne(rc => rc.Record)
                .WithMany(c => c.RecordComments)
                .HasForeignKey(rc => rc.RecordId);

            builder.HasOne(rc => rc.Comment)
                .WithMany(r => r.RecordComments)
                .HasForeignKey(rc => rc.CommentId);
        }
    }
}
