﻿using GestaoTarefas.Enumeradores;
namespace GestaoTarefas.Data.DTOs;
public class AgendamentoDTO
{
    public int UsuarioId { get; set; }
    public DateTime DataInicio { get; set; }
    public int Duracao { get; set; }
    public int Status { get; set; }

}