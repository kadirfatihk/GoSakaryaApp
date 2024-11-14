using GoSakaryaApp.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Data.Entities
{
    public class AreaEntity : BaseEntity
    {
        public string Name { get; set; }    // yer adı (sakarya müzesi... kentpark vs.)
        public string Location { get; set; }    // hangi ilçede
        public string Description { get; set; } // kısa açıklama
        public string History { get; set; } // kısa geçmişi

        // Relational Property
        public ICollection<CommentEntity> Comments { get; set; }    // Bir gezi alanına birden çok yorum yapılabilir -> Bireçok
    }

    public class AreaConfiguration : BaseConfiguration<AreaEntity>
    {
        public override void Configure(EntityTypeBuilder<AreaEntity> builder)
        {
            base.Configure(builder);
        }
    }
}
