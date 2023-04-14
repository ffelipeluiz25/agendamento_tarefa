using AutoMapper;
using GestaoTarefas.Data.DTOs;
using GestaoTarefas.Data.Models;
namespace GestaoTarefas.Mapper;
public class DefaultMapperProfile : Profile
{
    public DefaultMapperProfile()
    {
        CreateMap<UsuarioDTO, Usuarios>();
        CreateMap<Usuarios, UsuarioDTO>();

        CreateMap<UsuarioAllDTO, Usuarios>();
        CreateMap<Usuarios, UsuarioAllDTO>();

        CreateMap<AgendamentoDTO, AgendamentoTarefas>();
        CreateMap<AgendamentoTarefas, AgendamentoDTO>();

    }
}