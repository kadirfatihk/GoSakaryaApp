using GoSakaryaApp.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Data.Entities
{
    public class EventEntity : BaseEntity
    {
        public string Name { get; set; }    // etkinlik adı
        public string Location { get; set; }    // etkinlik mekanı
        public string? Artist { get; set; }
        public string? TicketPrice { get; set; }    // bilet fiyatı
        public string Description { get; set; }
        public DateTime EventDate { get; set; }     // etkinlik tarihi
        public string EventDuration { get; set; }  // etkinlik süresi(kaç saat)
        public int TicketCapacity { get; set; }


        // Relational Property
        public ICollection<CommentEntity> Comments { get; set; }    // Bir etkinliğe birden fazla yorum yapılabilir -> Bireçok
        public ICollection<EventTicketEntity> EventTickets { get; set; }      // Bir etkinliğe ait birçok bilet alınabilir ->  ÇOKAÇOK
    }

    // Configuration
    public class EventConfiguration : BaseConfiguration<EventEntity>
    {
        public override void Configure(EntityTypeBuilder<EventEntity> builder)
        {
            builder.Property(x => x.Artist).IsRequired(false);
            base.Configure(builder);
        }
    }
}
