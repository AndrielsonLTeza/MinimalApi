public class AlunoServico : IAlunoServico
{
    private readonly IAlunoRepositorio _alunoRepositorio;
    private readonly ICursoRepositorio _cursoRepositorio;

    public AlunoServico(IAlunoRepositorio alunoRepositorio, ICursoRepositorio cursoRepositorio)
    {
        _alunoRepositorio = alunoRepositorio;
        _cursoRepositorio = cursoRepositorio;
    }

    public async Task<IEnumerable<Aluno>> ListarTodosAsync()
    {
        return await _alunoRepositorio.ListarTodosAsync();
    }

    public async Task<Aluno> ObterPorIdAsync(int id)
    {
        return await _alunoRepositorio.ObterPorIdAsync(id);
    }

    public async Task<Aluno> AdicionarAsync(Aluno aluno)
    {
        // Validações básicas
        if (string.IsNullOrEmpty(aluno.Nome))
            throw new ArgumentException("O nome do aluno é obrigatório");
            
        if (string.IsNullOrEmpty(aluno.Email))
            throw new ArgumentException("O email do aluno é obrigatório");
            
        // Verifica se o curso existe
        var curso = await _cursoRepositorio.ObterPorIdAsync(aluno.CursoId);
        if (curso == null)
            throw new ArgumentException("O curso informado não existe");
            
        await _alunoRepositorio.AdicionarAsync(aluno);
        return aluno;
    }

    public async Task<bool> AtualizarAsync(Aluno aluno)
    {
        // Validações básicas
        if (string.IsNullOrEmpty(aluno.Nome))
            throw new ArgumentException("O nome do aluno é obrigatório");
            
        if (string.IsNullOrEmpty(aluno.Email))
            throw new ArgumentException("O email do aluno é obrigatório");
            
        // Verifica se o curso existe
        var curso = await _cursoRepositorio.ObterPorIdAsync(aluno.CursoId);
        if (curso == null)
            throw new ArgumentException("O curso informado não existe");
            
        return await _alunoRepositorio.AtualizarAsync(aluno);
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        return await _alunoRepositorio.ExcluirAsync(id);
    }

    public async Task<IEnumerable<Aluno>> ObterAlunosPorCursoAsync(int cursoId)
    {
        return await _alunoRepositorio.ObterAlunosPorCursoAsync(cursoId);
    }
}