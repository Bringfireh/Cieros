namespace Cieros.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyModel : DbContext
    {
        public MyModel()
            : base("name=MyModel")
        {
        }

        public virtual DbSet<Guardian> Guardians { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MonthlySalary> MonthlySalaries { get; set; }
        public virtual DbSet<RankClass> RankClasses { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guardian>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.Guardian)
                .WillCascadeOnDelete();
        }
    }
}
