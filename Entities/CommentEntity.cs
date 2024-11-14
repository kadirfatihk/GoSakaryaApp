using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Data.Entities
{
    public class CommentEntity : BaseEntity
    {
        public string Text { get; set; }
        public int? AreaId { get; set; }
        public int? EventId { get; set; }
        public DateTime CreatedDate { get; set; }

        // Relational Property
        public AreaEntity? Area { get; set; } 
        public EventEntity? Event { get; set; }
    }

    // Configuration
    public class CommentConfiguration : BaseConfiguration<CommentEntity>
    {
        public override void Configure(EntityTypeBuilder<CommentEntity> builder)
        {
            builder.Property(x=>x.AreaId).IsRequired(false);
            builder.Property(x=>x.EventId).IsRequired(false);
            base.Configure(builder);
        }
    }
}
