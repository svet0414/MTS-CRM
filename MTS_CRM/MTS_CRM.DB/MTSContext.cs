namespace MTS_CRM.DB
{
    using System.Data.Entity;
    public partial class MTSContext : DbContext
    {
        //  ConfigurationManager.
        public MTSContext() : base("Data Source=kraka.ucn.dk;Initial Catalog=dmai0918_1074331;Persist Security Info=True;User ID=dmai0918_1074331;Password=Password1!")
        //public MTSContext() : base("Data Source = .\\SQLEXPRESS;Initial Catalog = mtscrm; Integrated Security = True")
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Private> Privates { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductToCustomer> ProductToCustomers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.CVR)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.phoneNo)
                .IsUnicode(false);

            /*modelBuilder.Entity<Customer>()
                .HasOptional(e => e.Company)
                .WithRequired(e => e.Customer);

            modelBuilder.Entity<Customer>()
                .HasOptional(e => e.Private)
                .WithRequired(e => e.Customer);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.ProductToCustomers)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);*/

            modelBuilder.Entity<Location>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.country)
                .IsUnicode(false);

            /*modelBuilder.Entity<Location>()
                .HasMany(e => e.Customers)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);*/

            modelBuilder.Entity<Private>()
                .Property(e => e.fname)
                .IsUnicode(false);

            modelBuilder.Entity<Private>()
                .Property(e => e.lname)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductToCustomers)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeFName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeLName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeePhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Position)
                .IsUnicode(false);
        }
    }
}
