public class AlunoRepositorio : IAlunoRepositorio
{
    private readonly List<Aluno> _alunos = new List<Aluno>();
    private int _nextId = 1;

    public Task<IEnumerable<Aluno>> ListarTodosAsync()
    {
        return Task.FromResult<IEnumerable<Aluno>>(_alunos);
    }

    public Task<Aluno> ObterPorIdAsync(int id)
    {
        var aluno = _alunos.FirstOrDefault(a => a.Id == id);
        return Task.FromResult(aluno);
    }

    public Task AdicionarAsync(Aluno aluno)
    {
        aluno.Id = _nextId++;
        _alunos.Add(aluno);
        return Task.CompletedTask;
    }

    public Task<bool> AtualizarAsync(Aluno aluno)
    {
        var index = _alunos.FindIndex(a => a.Id == aluno.Id);
        if (index == -1)
            return Task.FromResult(false);

        _alunos[index] = aluno;
        return Task.FromResult(true);
    }

    public Task<bool> ExcluirAsync(int id)
    {
        var aluno = _alunos.FirstOrDefault(a => a.Id == id);
        if (aluno == null)
            return Task.FromResult(false);

        _alunos.Remove(aluno);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<Aluno>> ObterAlunosPorCursoAsync(int cursoId)
    {
        var alunos = _alunos.Where(a => a.CursoId == cursoId);
        return Task.FromResult(alunos);
    }
}
