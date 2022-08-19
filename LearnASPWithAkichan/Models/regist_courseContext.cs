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
        public virtual DbSet<RegistClass> RegistClasses { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=BUI_TUAN;Initial Catalog=regist_course;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.HasIndex(e => e.UserName, "UQ__account__7C9273C407D22EDA")
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

                entity.HasIndex(e => e.Name, "UQ__departme__72E12F1BB1B2FDEF")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "UQ__departme__B43B145FE3EA6A38")
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

            modelBuilder.Entity<RegistClass>(entity =>
            {
                entity.HasKey(e => new { e.ClassSessionId, e.StudentId })
                    .HasName("PK__regist_c__249FCF5C89324CF5");

                entity.ToTable("regist_class");

                entity.Property(e => e.ClassSessionId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("class_session_id")
                    .IsFixedLength();

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("student_id")
                    .IsFixedLength();

                entity.Property(e => e.Credits).HasColumnName("credits");

                entity.Property(e => e.RegistDate)
                    .HasColumnType("datetime")
                    .HasColumnName("regist_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.ClassSession)
                    .WithMany(p => p.RegistClasses)
                    .HasForeignKey(d => d.ClassSessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__regist_cl__class__3A81B327");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.RegistClasses)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__regist_cl__stude__398D8EEE");
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

                entity.HasMany(d => d.PrerequisiteSubjects)
                    .WithMany(p => p.Subjects)
                    .UsingEntity<Dictionary<string, object>>(
                        "PrerequisiteSubject",
                        l => l.HasOne<Subject>().WithMany().HasForeignKey("PrerequisiteSubjectId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__prerequis__prere__2F10007B"),
                        r => r.HasOne<Subject>().WithMany().HasForeignKey("SubjectId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__prerequis__subje__2E1BDC42"),
                        j =>
                        {
                            j.HasKey("SubjectId", "PrerequisiteSubjectId").HasName("PK__prerequi__5E9C16D777E628E1");

                            j.ToTable("prerequisite_subject");

                            j.IndexerProperty<string>("SubjectId").HasMaxLength(10).IsUnicode(false).HasColumnName("subject_id").IsFixedLength();

                            j.IndexerProperty<string>("PrerequisiteSubjectId").HasMaxLength(10).IsUnicode(false).HasColumnName("prerequisite_subject_id").IsFixedLength();
                        });

                entity.HasMany(d => d.Subjects)
                    .WithMany(p => p.PrerequisiteSubjects)
                    .UsingEntity<Dictionary<string, object>>(
                        "PrerequisiteSubject",
                        l => l.HasOne<Subject>().WithMany().HasForeignKey("SubjectId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__prerequis__subje__2E1BDC42"),
                        r => r.HasOne<Subject>().WithMany().HasForeignKey("PrerequisiteSubjectId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__prerequis__prere__2F10007B"),
                        j =>
                        {
                            j.HasKey("SubjectId", "PrerequisiteSubjectId").HasName("PK__prerequi__5E9C16D777E628E1");

                            j.ToTable("prerequisite_subject");

                            j.IndexerProperty<string>("SubjectId").HasMaxLength(10).IsUnicode(false).HasColumnName("subject_id").IsFixedLength();

                            j.IndexerProperty<string>("PrerequisiteSubjectId").HasMaxLength(10).IsUnicode(false).HasColumnName("prerequisite_subject_id").IsFixedLength();
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
