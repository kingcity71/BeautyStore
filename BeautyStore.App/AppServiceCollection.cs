using BeautyStore.BLL.Helpers;
using BeautyStore.BLL.Services;
using BeautyStore.DAL.Repository;
using BeautyStore.Interfaces.Helpers;
using BeautyStore.Interfaces.Repositories;
using BeautyStore.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BeautyStore.App
{
    public static class AppServiceCollection
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddTransient<IMapper, Mapper>();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IProductPhotoRepository, ProductPhotoRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IMessageService, MessageService>();
        }
    }
}