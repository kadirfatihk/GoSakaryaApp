using GoSakaryaApp.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public UserType UserType { get; set; }

        // Relational Property
        public ICollection<CommentEntity> Comments { get; set; }    // Bir kullanıcı birden fazla yorum yapabilir -> Bireçok
        public ICollection<EventTicketEntity> UserTickets { get; set; }      // Bir kullanıcının birden çok bileti olabilir -> Çokaçok
    }

    // Configuration
    public class UserConfiguration : BaseConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(x=>x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x=>x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x=>x.PhoneNumber).IsRequired(false).HasMaxLength(11).IsFixedLength(true);
            base.Configure(builder);
        }
    }
}
