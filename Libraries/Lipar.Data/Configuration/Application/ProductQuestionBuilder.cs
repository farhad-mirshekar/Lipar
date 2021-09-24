using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class ProductQuestionBuilder : IEntityTypeConfiguration<ProductQuestion>
    {
        public void Configure(EntityTypeBuilder<ProductQuestion> builder)
        {
            builder.ToTable("ProductQuestions", "Application");

            builder.HasKey(pq => pq.Id);
            builder.Property(pq => pq.Id).ValueGeneratedOnAdd();

            builder.Property(pq => pq.QuestionText).HasColumnType("NVARCHAR(MAX)").IsRequired();

            builder.HasOne(pq => pq.Product)
                .WithMany(p => p.ProductQuestions)
                .HasForeignKey(pq => pq.ProductId);

            builder.HasOne(pq => pq.User)
                .WithMany(u=>u.ProductQuestions)
                .HasForeignKey(pq => pq.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pq => pq.ViewStatus)
                .WithMany(v => v.ProductQuestions)
                .HasForeignKey(pq => pq.ViewStatusId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
