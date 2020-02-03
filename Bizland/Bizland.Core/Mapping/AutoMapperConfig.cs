using AutoMapper;

using Prism.Ioc;

namespace Bizland.Core
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings(IContainerRegistry containerRegistry)
        {
            // Đăng ký config automapper
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<DomainToViewModelMappingProfile>();
                c.AddProfile<ViewModelToDomainMappingProfile>();
            });
            containerRegistry.RegisterInstance(config.CreateMapper());
        }
    }
}