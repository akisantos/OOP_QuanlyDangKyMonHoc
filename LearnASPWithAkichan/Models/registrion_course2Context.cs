using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LearnASPWithAkichan.Models
{
    public partial class registrion_course2Context : DbContext
    {
        public registrion_course2Context()
        {
        }

        public registrion_course2Context(DbContextOptions<registrion_course2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<LopHocPhan> LopHocPhans { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=BUI_TUAN;Initial Catalog=registrion_course2;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.HasIndex(e => e.Username, "UQ__account__F3DBC5720905E489")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("student_id");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__account__student__4BAC3F29");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.HasIndex(e => e.Name, "UQ__departme__72E12F1BF2E771D3")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "UQ__departme__B43B145F76F33673")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .HasColumnName("address");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<LopHocPhan>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.SubjectId })
                    .HasName("PK__LopHocPh__3F3349FC39D6893B");

                entity.ToTable("LopHocPhan");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("student_id");

                entity.Property(e => e.SubjectId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("subject_id");

                entity.Property(e => e.AverageCore).HasColumnName("average_core");

                entity.Property(e => e.EndCore).HasColumnName("end_core");

                entity.Property(e => e.MidCore).HasColumnName("mid_core");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasColumnName("time");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.LopHocPhans)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LopHocPha__stude__3E52440B");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.LopHocPhans)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LopHocPha__subje__3F466844");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.HasIndex(e => e.Phone, "UQ__student__B43B145FA6D43096")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(1000)
                    .HasColumnName("address");

                entity.Property(e => e.Birth)
                    .HasColumnType("date")
                    .HasColumnName("BIRTH");

                entity.Property(e => e.DepartmentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("department_id");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(100)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.Gender)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("gender")
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__student__departm__2F10007B");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subject");

                entity.HasIndex(e => e.Name, "UQ__subject__72E12F1BF55B38D1")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Credits).HasColumnName("credits");

                entity.Property(e => e.DepartmentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("department_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.SubjectId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("subject_id");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__subject__departm__3B75D760");

                entity.HasOne(d => d.SubjectNavigation)
                    .WithMany(p => p.InverseSubjectNavigation)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__subject__subject__3A81B327");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
