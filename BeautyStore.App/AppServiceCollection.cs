using BeautyStore.BLL.Helpers;
using BeautyStore.BLL.Services;
using BeautyStore.DAL.Repository;
using BeautyStore.Helpers;
using BeautyStore.Interfaces.Helpers;
using BeautyStore.Interfaces.Repositories;
using BeautyStore.Interfaces.Services;
using BeautyStore.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BeautyStore.App
{
    public static class AppServiceCollection
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddTransient<IMapper, Mapper>();
            services.AddTransient<IMailHelper, MailHelper>();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IProductPhotoRepository, ProductPhotoRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<ICartRepository, CartRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<ICartService, CartService>();
        }
    }
}