public interface ICursoServico
{
    Task<IEnumerable<Curso>> ListarTodosAsync();
    Task<Curso> ObterPorIdAsync(int id);
    Task<Curso> AdicionarAsync(Curso curso);
    Task<bool> AtualizarAsync(Curso curso);
    Task<bool> ExcluirAsync(int id);
}
