namespace TulipShopping.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TulipShoppingModel : DbContext
    {
        public TulipShoppingModel()
            : base("name=TulipShoppingModel1")
        {
        }

        public virtual DbSet<AdminTable> AdminTable { get; set; }
        public virtual DbSet<Authority> Authority { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Notice> Notice { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminTable>()
                .Property(e => e.Kargo)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AdminTable>()
                .Property(e => e.Nakliye)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AdminTable>()
                .Property(e => e.Katki)
                .HasPrecision(18, 3);
        }
    }
}
