using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LearnASPWithAkichan.Models
{
    public partial class registrion_courseContext : DbContext
    {
        public registrion_courseContext()
        {
        }

        public registrion_courseContext(DbContextOptions<registrion_courseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Regist> Regists { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<TableCore> TableCores { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=BUI_TUAN;Initial Catalog=registrion_course;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.HasIndex(e => e.Username, "UQ__account__F3DBC5726B83E517")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("student_id");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__account__student__2E1BDC42");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.HasIndex(e => e.Name, "UQ__departme__72E12F1B8D455414")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "UQ__departme__B43B145F6FAD2B34")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
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

            modelBuilder.Entity<Regist>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.SubjectId, e.TableCoreId })
                    .HasName("PK__regist__D34F80D56426B578");

                entity.ToTable("regist");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("student_id");

                entity.Property(e => e.SubjectId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("subject_id");

                entity.Property(e => e.TableCoreId).HasColumnName("table_core_id");

                entity.Property(e => e.CloseTime)
                    .HasColumnType("datetime")
                    .HasColumnName("close_time");

                entity.Property(e => e.OpenTime)
                    .HasColumnType("datetime")
                    .HasColumnName("open_time");

                entity.Property(e => e.RegistTime)
                    .HasColumnType("datetime")
                    .HasColumnName("regist_time");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Regists)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__regist__student___37A5467C");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Regists)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__regist__subject___38996AB5");

                entity.HasOne(d => d.TableCore)
                    .WithMany(p => p.Regists)
                    .HasForeignKey(d => d.TableCoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__regist__table_co__398D8EEE");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.HasIndex(e => e.Phone, "UQ__student__B43B145F5842B2A6")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(1000)
                    .HasColumnName("address");

                entity.Property(e => e.Birth)
                    .HasColumnType("date")
                    .HasColumnName("BIRTH");

                entity.Property(e => e.DepartmentId)
                    .HasMaxLength(100)
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
                    .HasConstraintName("FK__student__departm__2A4B4B5E");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subject");

                entity.HasIndex(e => e.Name, "UQ__subject__72E12F1B1014DC20")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Credits).HasColumnName("credits");

                entity.Property(e => e.DepartmentId)
                    .HasMaxLength(100)
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
                    .HasConstraintName("FK__subject__departm__32E0915F");

                entity.HasOne(d => d.SubjectNavigation)
                    .WithMany(p => p.InverseSubjectNavigation)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__subject__subject__31EC6D26");
            });

            modelBuilder.Entity<TableCore>(entity =>
            {
                entity.ToTable("table_core");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AverageCore).HasColumnName("average_core");

                entity.Property(e => e.EndCore).HasColumnName("end_core");

                entity.Property(e => e.MidCore).HasColumnName("mid_core");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
