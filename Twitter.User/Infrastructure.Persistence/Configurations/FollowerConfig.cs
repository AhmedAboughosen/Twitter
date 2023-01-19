using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class FollowerConfig : IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> builder)
        {
            
            // builder.HasOne(pi => pi.Followers)
            //     .WithMany()
            //     .HasForeignKey(pi => pi.FollowerId)
            //     .OnDelete(DeleteBehavior.Cascade);
            //
            // builder.HasOne(pi => pi.Followee)
            //     .WithMany()
            //     .HasForeignKey(pi => pi.FolloweeId)
            //     .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}