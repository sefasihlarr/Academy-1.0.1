using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations
{
    public class ExamAnswerConfiguration : IEntityTypeConfiguration<ExamAnswers>
    {
        public void Configure(EntityTypeBuilder<ExamAnswers> builder)
        {
            builder.ToTable("ExamAnswers");

            // Primary key
            builder.HasKey(e => new { e.ExamId, e.UserId, e.QuestionId, e.OptionId });

            // Relationships
            builder.HasOne(e => e.Exam)
                .WithMany(e => e.ExamAnswers)
                .HasForeignKey(e => e.ExamId)
                .OnDelete(DeleteBehavior.Restrict);



            builder.HasOne(e => e.Question)
                .WithMany(q => q.ExamAnswers)
                .HasForeignKey(e => e.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Option)
                .WithMany(o => o.ExamAnswers)
                .HasForeignKey(e => e.OptionId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
