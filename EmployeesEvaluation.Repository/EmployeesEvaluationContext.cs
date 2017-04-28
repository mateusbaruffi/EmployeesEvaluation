/*
 ** Entity Framework DbContext class 
 */
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EmployeesEvaluation.Core.Models;
using System.Linq;
using System;

namespace EmployeesEvaluation.Repository
{
    public class EmployeesEvaluationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Evaluation> Evaluation { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<EvaluationQuestion> EvaluationQuestion { get; set; }
        public DbSet<QuestionType> QuestionType { get; set; }
        public DbSet<Season> Season { get; set; }

        public EmployeesEvaluationContext (DbContextOptions<EmployeesEvaluationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Set ON DELETE no Action
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Using fluent API instead data annotations to preserve the domain model
            modelBuilder.Entity<Department>()
                .ToTable("Departments");

            modelBuilder.Entity<Department>()
                .Property(d => d.CreatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Department>()
                .Property(d => d.UpdatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Evaluation>()
                .ToTable("Evaluations").Ignore(e => e.Questions);

            modelBuilder.Entity<Question>()
                .ToTable("Questions");

            modelBuilder.Entity<Question>()
                .Property(q => q.QuestionTypeId)
                .IsRequired();

            modelBuilder.Entity<Question>()
                .Property(d => d.CreatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Question>()
                .Property(d => d.UpdatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.QuestionType)
                .WithMany(c => c.Questions);

            modelBuilder.Entity<QuestionType>()
                .ToTable("QuestionTypes");

            modelBuilder.Entity<Season>()
                .ToTable("Seasons");

            modelBuilder.Entity<EvaluationQuestion>()
              .ToTable("EvaluationQuestion");

            modelBuilder.Entity<EvaluationQuestion>()
                .HasOne(eq => eq.Evaluation)
                .WithMany(e => e.EvaluationQuestions)
                .HasForeignKey(eq => eq.EvaluationId);

            modelBuilder.Entity<EvaluationQuestion>()
                .HasOne(eq => eq.Question)
                .WithMany(q => q.EvaluationQuestions)
                .HasForeignKey(eq => eq.QuestionId); 
            
        }        
        

    }
}