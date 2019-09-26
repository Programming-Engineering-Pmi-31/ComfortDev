using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace TestApp
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

        public virtual DbSet<CourseTask> CourseTask { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<TestAnswer> TestAnswer { get; set; }
        public virtual DbSet<TestQuestion> TestQuestion { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserCourse> UserCourse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Configuration["ConnectionString"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseTask>(entity =>
            {
                entity.ToTable("course_task");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompPer).HasColumnName("compPer");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.TaskId).HasColumnName("taskId");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseTask)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("course_task_courseId_fkey");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.CourseTask)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("course_task_taskId_fkey");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("task");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EvalCrit)
                    .IsRequired()
                    .HasColumnName("evalCrit");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("character varying");

                entity.Property(e => e.TopicId).HasColumnName("topicId");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("task_topicId_fkey");
            });

            modelBuilder.Entity<TestAnswer>(entity =>
            {
                entity.ToTable("test_answer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasColumnName("answer");

                entity.Property(e => e.QuestionId).HasColumnName("questionId");

                entity.Property(e => e.TopicId).HasColumnName("topicId");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.TestAnswer)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("test_answer_questionId_fkey");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.TestAnswer)
                    .HasForeignKey(d => d.TopicId)
                    .HasConstraintName("test_answer_topicId_fkey");
            });

            modelBuilder.Entity<TestQuestion>(entity =>
            {
                entity.ToTable("test_question");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasColumnName("question");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.ToTable("topic");

                entity.HasIndex(e => e.Title)
                    .HasName("topic_title_key")
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

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("user_pkey");

                entity.ToTable("user");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasColumnName("hash")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<UserCourse>(entity =>
            {
                entity.ToTable("user_course");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndDate).HasColumnName("endDate");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("userName")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.UserCourse)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_course_userName_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
