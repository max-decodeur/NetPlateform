namespace WcfDecryptorService.Models.Persistence
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext() : base("name=SqlServerDbContext") { }

        public virtual DbSet<log> logs { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<user>()
                .Property(e => e.login)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.usertoken)
                .IsUnicode(false);
        }
    }
}
