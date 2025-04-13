public interface ICursoRepositorio
{
    Task<IEnumerable<Curso>> ListarTodosAsync();
    Task<Curso> ObterPorIdAsync(int id);
    Task AdicionarAsync(Curso curso);
    Task<bool> AtualizarAsync(Curso curso);
    Task<bool> ExcluirAsync(int id);
}
