using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Data.Entities
{
    public class EventTicketEntity : BaseEntity
    {
        // ARA TABLO
        public int UserId { get; set; } // bilet alan kullanıcı - foreign key
        public int EventId { get; set; }    // bilet alınan etkinlik - foreign key
        public DateTime PurchaseDate { get; set; }   // biletin satın alındığı tarih.

        // Relational Property
        public UserEntity User { get; set; }    // Bir biletin bir sahibi olabilir.
        public EventEntity Event { get; set; }  // Bir bilet bir etkinliğe ait olabilir.
    }

    // Configuration
    public class TicketConfiguration : BaseConfiguration<EventTicketEntity>
    {
        public override void Configure(EntityTypeBuilder<EventTicketEntity> builder)
        {
            //builder.Ignore(x=>x.Id);
            //builder.HasKey("UserId", "EventId");
            base.Configure(builder);
        }
    }
}
