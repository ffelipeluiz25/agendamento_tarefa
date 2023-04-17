namespace GestaoTarefas.Data.DTOs;
public class TarefaArquivoDTO
{
    public TarefaArquivoDTO(){ }
    public TarefaArquivoDTO(int tarefaId, IFormFile arquivo)
    {
        TarefaId = tarefaId;
        Arquivo = arquivo;
        var splitName = arquivo.FileName.Split("$_$");
        if (splitName.Length > 0)
        {
            NomeArquivo = splitName[0];
            Extensao = Path.GetExtension(splitName[0]);
        }
    }
    public int Id { get; set; }
    public int TarefaId { get; set; }
    public IFormFile Arquivo { get; set; }
    public string NomeArquivo { get; set; }
    public string Extensao { get; set; }
    public DateTime DataCriacao { get; set; }
}