public class CursoRepositorio : ICursoRepositorio
{
    private readonly List<Curso> _cursos = new List<Curso>();
    private int _nextId = 1;

    public Task<IEnumerable<Curso>> ListarTodosAsync()
    {
        return Task.FromResult<IEnumerable<Curso>>(_cursos);
    }

    public Task<Curso> ObterPorIdAsync(int id)
    {
        var curso = _cursos.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(curso);
    }

    public Task AdicionarAsync(Curso curso)
    {
        curso.Id = _nextId++;
        _cursos.Add(curso);
        return Task.CompletedTask;
    }

    public Task<bool> AtualizarAsync(Curso curso)
    {
        var index = _cursos.FindIndex(c => c.Id == curso.Id);
        if (index == -1)
            return Task.FromResult(false);

        _cursos[index] = curso;
        return Task.FromResult(true);
    }

    public Task<bool> ExcluirAsync(int id)
    {
        var curso = _cursos.FirstOrDefault(c => c.Id == id);
        if (curso == null)
            return Task.FromResult(false);

        _cursos.Remove(curso);
        return Task.FromResult(true);
    }
}    