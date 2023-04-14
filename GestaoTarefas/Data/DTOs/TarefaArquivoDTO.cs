namespace GestaoTarefas.Data.DTOs;
public class TarefaArquivoDTO
{
    public TarefaArquivoDTO(int tarefaId, IFormFile arquivo)
    {
        TarefaId = tarefaId;
        Arquivo = arquivo;
        NomeArquivo = arquivo.FileName;
        Extensao = Path.GetExtension(arquivo.FileName);
    }
    public int TarefaId { get; set; }
    public IFormFile Arquivo { get; set; }
    public string NomeArquivo { get; set; }
    public string Extensao { get; set; }
}