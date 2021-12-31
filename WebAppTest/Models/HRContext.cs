using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebAppTest.Models
{
    public partial class HRContext : DbContext
    {
        public HRContext()
            : base("name=HRContext")
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CourseDetail> CourseDetail { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<Trainees> Trainees { get; set; }
        public virtual DbSet<Trainer> Trainer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.AccountType)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Admin)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Staff)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Trainees)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Trainer)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.AdminID)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.AdminName)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Specialty)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Categories>()
                .Property(e => e.CatID)
                .IsUnicode(false);

            modelBuilder.Entity<Categories>()
                .Property(e => e.CatName)
                .IsUnicode(false);

            modelBuilder.Entity<Categories>()
                .Property(e => e.CatDes)
                .IsUnicode(false);

            modelBuilder.Entity<Categories>()
                .HasMany(e => e.Course)
                .WithRequired(e => e.Categories)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.CourseID)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.CourseName)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.CourseDes)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.CatID)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.CourseDetail)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Topic)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseDetail>()
                .Property(e => e.CourseID)
                .IsUnicode(false);

            modelBuilder.Entity<CourseDetail>()
                .Property(e => e.TraineeID)
                .IsUnicode(false);

            modelBuilder.Entity<CourseDetail>()
                .Property(e => e.TrainerID)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.StaffID)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.StaffName)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Topic>()
                .Property(e => e.TopicID)
                .IsUnicode(false);

            modelBuilder.Entity<Topic>()
                .Property(e => e.TopicName)
                .IsUnicode(false);

            modelBuilder.Entity<Topic>()
                .Property(e => e.CourseID)
                .IsUnicode(false);

            modelBuilder.Entity<Trainees>()
                .Property(e => e.TraineeID)
                .IsUnicode(false);

            modelBuilder.Entity<Trainees>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Trainees>()
                .Property(e => e.TraineeName)
                .IsUnicode(false);

            modelBuilder.Entity<Trainees>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Trainees>()
                .Property(e => e.Edu)
                .IsUnicode(false);

            modelBuilder.Entity<Trainees>()
                .HasMany(e => e.CourseDetail)
                .WithRequired(e => e.Trainees)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Trainer>()
                .Property(e => e.TrainerID)
                .IsUnicode(false);

            modelBuilder.Entity<Trainer>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Trainer>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Trainer>()
                .Property(e => e.Specialty)
                .IsUnicode(false);

            modelBuilder.Entity<Trainer>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Trainer>()
                .HasMany(e => e.CourseDetail)
                .WithRequired(e => e.Trainer)
                .WillCascadeOnDelete(false);
        }
    }
}
