using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LearnASPWithAkichan.Models
{
    public partial class regist_courseContext : DbContext
    {
        public regist_courseContext()
        {
        }

        public regist_courseContext(DbContextOptions<regist_courseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<ClassSession> ClassSessions { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<PrerequisiteSubject> PrerequisiteSubjects { get; set; } = null!;
        public virtual DbSet<RegistClass> RegistClasses { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-D99T0PS\\THAIVY;Initial Catalog=regist_course;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.HasIndex(e => e.UserName, "UQ__account__7C9273C44147142B")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.PassWord)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("passWord");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("user_name");
            });

            modelBuilder.Entity<ClassSession>(entity =>
            {
                entity.ToTable("class_session");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.BeginDate)
                    .HasColumnType("date")
                    .HasColumnName("begin_date");

                entity.Property(e => e.CommonClass).HasColumnName("common_class");

                entity.Property(e => e.DepartmentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("department_id")
                    .IsFixedLength();

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.PointClass).HasColumnName("point_class");

                entity.Property(e => e.PointEnd).HasColumnName("point_end");

                entity.Property(e => e.PointMid).HasColumnName("point_mid");

                entity.Property(e => e.SubjectId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("subject_id")
                    .IsFixedLength();

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.ClassSessions)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__class_ses__depar__31EC6D26");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.ClassSessions)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__class_ses__subje__32E0915F");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.HasIndex(e => e.Name, "UQ__departme__72E12F1B1CDB9A6A")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "UQ__departme__B43B145FFE502167")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Address)
                    .HasMaxLength(1000)
                    .HasColumnName("address");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<PrerequisiteSubject>(entity =>
            {
                entity.ToTable("prerequisite_subject");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PrerequisiteSubjectId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("prerequisite_subject_id")
                    .IsFixedLength();

                entity.Property(e => e.SubjectId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("subject_id")
                    .IsFixedLength();

                entity.HasOne(d => d.PrerequisiteSubjectNavigation)
                    .WithMany(p => p.PrerequisiteSubjectPrerequisiteSubjectNavigations)
                    .HasForeignKey(d => d.PrerequisiteSubjectId)
                    .HasConstraintName("FK__prerequis__prere__74AE54BC");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.PrerequisiteSubjectSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__prerequis__subje__73BA3083");
            });

            modelBuilder.Entity<RegistClass>(entity =>
            {
                entity.ToTable("regist_class");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClassSessionId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("class_session_id")
                    .IsFixedLength();

                entity.Property(e => e.Credits).HasColumnName("credits");

                entity.Property(e => e.RegistDate)
                    .HasColumnType("datetime")
                    .HasColumnName("regist_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("student_id")
                    .IsFixedLength();

                entity.HasOne(d => d.ClassSession)
                    .WithMany(p => p.RegistClasses)
                    .HasForeignKey(d => d.ClassSessionId)
                    .HasConstraintName("FK__regist_cl__class__619B8048");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.RegistClasses)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__regist_cl__stude__60A75C0F");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasColumnName("address");

                entity.Property(e => e.Birth)
                    .HasColumnType("date")
                    .HasColumnName("birth");

                entity.Property(e => e.DepartmentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("department_id")
                    .IsFixedLength();

                entity.Property(e => e.HomeTown)
                    .HasMaxLength(100)
                    .HasColumnName("home_town");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__student__account__36B12243");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__student__departm__35BCFE0A");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subject");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Credits).HasColumnName("credits");

                entity.Property(e => e.DepartmentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("department_id")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__subject__departm__2B3F6F97");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
