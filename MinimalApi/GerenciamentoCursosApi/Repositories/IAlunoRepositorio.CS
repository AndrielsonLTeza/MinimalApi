public interface IAlunoRepositorio
{
    Task<IEnumerable<Aluno>> ListarTodosAsync();
    Task<Aluno> ObterPorIdAsync(int id);
    Task AdicionarAsync(Aluno aluno);
    Task<bool> AtualizarAsync(Aluno aluno);
    Task<bool> ExcluirAsync(int id);
    Task<IEnumerable<Aluno>> ObterAlunosPorCursoAsync(int cursoId);
}