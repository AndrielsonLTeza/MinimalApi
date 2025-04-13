public class Curso
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int CargaHoraria { get; set; }
    public List<Aluno> Alunos { get; set; } = new List<Aluno>();
}