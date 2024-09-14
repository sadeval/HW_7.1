using Microsoft.EntityFrameworkCore;

namespace MailingListDB
{
    public class MailingListContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Promotion> Promotions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MailingListDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Country)
                .WithMany(c => c.Customers)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.City)
                .WithMany(c => c.Customers)
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany(c => c.Cities)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Promotion>()
                .HasOne(p => p.Section)
                .WithMany(s => s.Promotions)
                .HasForeignKey(p => p.SectionId);

            modelBuilder.Entity<Promotion>()
                .HasOne(p => p.Country)
                .WithMany(c => c.Promotions)
                .HasForeignKey(p => p.CountryId);

            base.OnModelCreating(modelBuilder);
        }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }

        public virtual Country Country { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Section> InterestedSections { get; set; }

        public Customer()
        {
            InterestedSections = new List<Section>();
        }
    }

    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Promotion> Promotions { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }

        public Country()
        {
            Cities = new List<City>();
            Promotions = new List<Promotion>();
            Customers = new List<Customer>();
        }
    }

    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }

        public City()
        {
            Customers = new List<Customer>();
        }
    }

    public class Section
    {
        public int SectionId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Promotion> Promotions { get; set; }

        public Section()
        {
            Customers = new List<Customer>();
            Promotions = new List<Promotion>();
        }
    }

    public class Promotion
    {
        public int PromotionId { get; set; }
        public int SectionId { get; set; }
        public int CountryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? ProductName { get; set; }

        public virtual Section? Section { get; set; }
        public virtual Country? Country { get; set; }
    }
}
