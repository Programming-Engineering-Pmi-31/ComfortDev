using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace TestConsoleApp
{
    public partial class ComfortDevTestContext : DbContext
    {
        private static IConfigurationRoot Configuration;
        public ComfortDevTestContext()
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.IsNullOrWhiteSpace(env))
            {
                env = "Development";
            }

            var builder = new ConfigurationBuilder();

            if (env == "Development")
            {
                builder.AddUserSecrets<Program>();
            }

            Configuration = builder.Build();
        }

        public ComfortDevTestContext(DbContextOptions<ComfortDevTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CourseTasks> CourseTasks { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<TestAnswers> TestAnswers { get; set; }
        public virtual DbSet<TestQuestions> TestQuestions { get; set; }
        public virtual DbSet<Topics> Topics { get; set; }
        public virtual DbSet<UserCourses> UserCourses { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Configuration["ConnectionString"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseTasks>(entity =>
            {
                entity.ToTable("course_tasks");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompPer).HasColumnName("compPer");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.TaskId).HasColumnName("taskId");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseTasks)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("course_tasks_courseId_fkey");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.CourseTasks)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("course_tasks_taskId_fkey");
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.ToTable("tasks");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EvalCrit)
                    .IsRequired()
                    .HasColumnName("evalCrit");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("character varying");

                entity.Property(e => e.TopicId).HasColumnName("topicId");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tasks_topicId_fkey");
            });

            modelBuilder.Entity<TestAnswers>(entity =>
            {
                entity.ToTable("test_answers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasColumnName("answer");

                entity.Property(e => e.QuestionId).HasColumnName("questionId");

                entity.Property(e => e.TopicId).HasColumnName("topicId");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.TestAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("test_answers_questionId_fkey");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.TestAnswers)
                    .HasForeignKey(d => d.TopicId)
                    .HasConstraintName("test_answers_topicId_fkey");
            });

            modelBuilder.Entity<TestQuestions>(entity =>
            {
                entity.ToTable("test_questions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasColumnName("question");
            });

            modelBuilder.Entity<Topics>(entity =>
            {
                entity.ToTable("topics");

                entity.HasIndex(e => e.Title)
                    .HasName("topics_title_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ImageSource)
                    .HasColumnName("imageSource")
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<UserCourses>(entity =>
            {
                entity.ToTable("user_courses");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndDate).HasColumnName("endDate");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("userName")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.UserCourses)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_courses_userName_fkey");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("users_pkey");

                entity.ToTable("users");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasColumnName("hash")
                    .HasColumnType("character varying");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
