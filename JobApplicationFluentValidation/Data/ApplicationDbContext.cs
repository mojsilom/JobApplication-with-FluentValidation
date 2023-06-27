﻿using JobApplicationFluentValidation.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationFluentValidation.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public virtual DbSet<JobApplication> Applicants { get; set; }
        public virtual DbSet<FileModel> FileModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<JobApplication>(entity =>
            {
                entity.HasKey(e => e.ApplicationID);

                entity.HasIndex(e => e.FileUploadFileModelId, "IX_Applicants_FileUploadFileModelId");

                entity.Property(e => e.ApplicationID).HasColumnName("ApplicationID");

                entity.HasOne(d => d.FileUpload).WithMany(p => p.Applicants).HasForeignKey(d => d.FileUploadFileModelId);

            });

            modelBuilder.Entity<FileModel>(entity =>
            {
                entity.ToTable("FileModel");
            });

        }
    }
}
