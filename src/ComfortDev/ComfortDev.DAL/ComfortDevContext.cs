using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ComfortDev.DAL.Entities;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ComfortDev.DAL
{
    public partial class ComfortDevContext : DbContext
    {
        private readonly string connectionString;
        public ComfortDevContext()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("secret.json");
            var config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public ComfortDevContext(DbContextOptions<ComfortDevContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CourseTask> CourseTasks { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TestAnswer> TestAnswers { get; set; }
        public virtual DbSet<TestQuestion> TestQuestions { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<UserCourse> UserCourses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseTask>(entity =>
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

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("tasks");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EvalCrit)
                    .IsRequired()
                    .HasColumnName("evalCrit");

                entity.Property(e => e.TaskText)
                    .IsRequired()
                    .HasColumnName("taskText");

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

            modelBuilder.Entity<TestAnswer>(entity =>
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

            modelBuilder.Entity<TestQuestion>(entity =>
            {
                entity.ToTable("test_questions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasColumnName("question");
            });

            modelBuilder.Entity<Topic>(entity =>
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

            modelBuilder.Entity<UserCourse>(entity =>
            {
                entity.ToTable("user_courses");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndDate).HasColumnName("endDate");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCourses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_courses_userId_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasColumnName("hash")
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
