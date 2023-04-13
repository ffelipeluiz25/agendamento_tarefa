using AutoMapper;
namespace GestaoTarefas.Mapper;
public class MapperConfigFactory
{
    public static IMapper Create()
    {
        return CreateConfig().CreateMapper();
    }

    public static MapperConfiguration CreateConfig()
    {
        return new MapperConfiguration(cfg => MapProfiles(cfg));
    }

    public static void MapProfiles(IMapperConfigurationExpression cfg)
    {
        cfg.AddProfile<DefaultMapperProfile>();
    }
}