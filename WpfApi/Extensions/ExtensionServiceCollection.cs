using DataLibrary.Domain;
using DataLibrary.Repositorys;
using ServiceLibrary.MenmoServices;
using ServiceLibrary.ToDoServices;

namespace WpfApi.Extensions
{
    public static class ExtensionServiceCollection
    {
        public static IServiceCollection AddExtensionScope(this IServiceCollection services)
        {
            services.AddScoped<IRepository<ToDo>, Repository<ToDo>>();
            services.AddScoped<IRepository<Menmo>, Repository<Menmo>>();
            services.AddScoped<IToDoService, ToDoService>();
            services.AddScoped<IMenmoService, MenmoService>();
            return services;
        }
    }
}
