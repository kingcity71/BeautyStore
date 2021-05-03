using BeautyStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BeautyStore.DAL
{
    public class Context : DbContext
    {
        const string SQL_CONNECTION_CONFIG_NAME = "BeautyStore";
        readonly string _connectionString;
        public Context(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(SQL_CONNECTION_CONFIG_NAME);
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
