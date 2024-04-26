using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concrete
{
    public class AcademyContext : IdentityDbContext<AppUser, AppRole, int>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=CODER;database=DbAcademy;integrated security=true");
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Output> Outputs { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<ClassBranch> ClassBranches { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<ExamAnswers> ExamAnswers { get; set; }
        public DbSet<ExamQuestions> ExamQuestions { get; set; }
        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<Scors> Scors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //fluenAPI

            modelBuilder.Entity<ExamAnswers>()
                .HasKey(e => new { e.ExamId, e.UserId, e.QuestionId });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExamAnswers>()
               .HasOne(e => e.Option)
               .WithMany()
               .HasForeignKey(e => e.OptionId);
            base.OnModelCreating(modelBuilder);
            // Relationships
            modelBuilder.Entity<ExamAnswers>()
                .HasOne(e => e.Exam)
                .WithMany(e => e.ExamAnswers)
                .HasForeignKey(e => e.ExamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExamAnswers>()
                .HasOne(e => e.Question)
                .WithMany(q => q.ExamAnswers)
                .HasForeignKey(e => e.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExamAnswers>()
                .HasOne(e => e.Option)
                .WithMany(o => o.ExamAnswers)
                .HasForeignKey(e => e.OptionId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClassBranch>()
               .HasKey(c => new { c.ClassId, c.BranchId });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExamQuestions>()
                .HasKey(c => new { c.ExamId, c.QuestionId });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExamAnswers>()
                .HasKey(c => new { c.ExamId, c.QuestionId, c.UserId });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Level)
                .WithMany()
                .HasForeignKey(q => q.LevelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Output)
                .WithMany()
                .HasForeignKey(q => q.OutputId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Subject)
                .WithMany(s => s.Questions)
                .HasForeignKey(q => q.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Question>()
               .HasOne(q => q.Lesson)
               .WithMany()
               .HasForeignKey(q => q.LessonId)
               .OnDelete(DeleteBehavior.Restrict);

            //Lesson Fluent Api
            modelBuilder.Entity<Lesson>()
                  .HasOne(q => q.Class)
                  .WithMany()
                  .HasForeignKey(q => q.ClassId)
                  .OnDelete(DeleteBehavior.Restrict);
            //scors Fluent Api
            modelBuilder.Entity<Scors>()
              .HasOne(q => q.Exam)
              .WithMany()
              .HasForeignKey(q => q.ExamId)
              .OnDelete(DeleteBehavior.Restrict);

            //Exam Fluent Api
            modelBuilder.Entity<Exam>()
                 .HasOne(q => q.Class)
                 .WithMany()
                 .HasForeignKey(q => q.ClassId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Exam>()
                .HasOne(q => q.Subject)
                .WithMany()
                .HasForeignKey(q => q.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Exam>()
                 .HasOne(q => q.Lesson)
                 .WithMany()
                 .HasForeignKey(q => q.LessonId)
                 .OnDelete(DeleteBehavior.Restrict);

            //Solution Fluent Api

            modelBuilder.Entity<Solution>()
                .HasOne(q => q.Question)
                .WithMany()
                .HasForeignKey(q => q.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Solution>()
                .HasOne(q => q.Option)
                .WithMany()
                .HasForeignKey(q => q.OptionId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }



    }
}
