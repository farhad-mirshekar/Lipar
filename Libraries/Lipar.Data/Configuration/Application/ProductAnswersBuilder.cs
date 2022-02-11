using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class ProductAnswersBuilder : IEntityTypeConfiguration<ProductAnswers>
    {
        public void Configure(EntityTypeBuilder<ProductAnswers> builder)
        {
            builder.ToTable("ProductAnswers", schema: "Application");

            builder.HasKey(pa => pa.Id);
            builder.Property(pa => pa.Id).ValueGeneratedOnAdd();

            builder.Property(pa => pa.AnswerText).HasColumnType("NVARCHAR(MAX)").IsRequired();

            builder.HasOne(pa => pa.ProductQuestion)
                .WithMany(pq => pq.ProductAnswers)
                .HasForeignKey(pa => pa.ProductQuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pa => pa.User)
                .WithMany(u => u.ProductAnswers)
                .HasForeignKey(pa => pa.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pa => pa.ViewStatus)
                .WithMany(v => v.ProductAnswers)
                .HasForeignKey(pa => pa.ViewStatusId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
