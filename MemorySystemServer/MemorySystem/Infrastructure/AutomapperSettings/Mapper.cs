using MemorySystem.Infrastructure.AutomapperSettings;

namespace MemorySystem.Common.Infrastructure.AutomapperSettings
{
    public class Mapper
    {
        public static TDestination Map<TDestination>(object source) => AutoMapperConfig.MapperInstance.Map<TDestination>(source);
    }
}
