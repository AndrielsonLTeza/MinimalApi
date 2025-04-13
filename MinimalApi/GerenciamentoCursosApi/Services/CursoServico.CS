public class CursoServico : ICursoServico
{
    private readonly ICursoRepositorio _cursoRepositorio;
    private readonly IAlunoRepositorio _alunoRepositorio;

    public CursoServico(ICursoRepositorio cursoRepositorio, IAlunoRepositorio alunoRepositorio)
    {
        _cursoRepositorio = cursoRepositorio;
        _alunoRepositorio = alunoRepositorio;
    }

    public async Task<IEnumerable<Curso>> ListarTodosAsync()
    {
        var cursos = await _cursoRepositorio.ListarTodosAsync();
        
        // Carrega os alunos para cada curso
        foreach (var curso in cursos)
        {
            var alunos = await _alunoRepositorio.ObterAlunosPorCursoAsync(curso.Id);
            curso.Alunos = alunos.ToList();
        }
        
        return cursos;
    }

    public async Task<Curso> ObterPorIdAsync(int id)
    {
        var curso = await _cursoRepositorio.ObterPorIdAsync(id);
        if (curso != null)
        {
            var alunos = await _alunoRepositorio.ObterAlunosPorCursoAsync(id);
            curso.Alunos = alunos.ToList();
        }
        
        return curso;
    }

    public async Task<Curso> AdicionarAsync(Curso curso)
    {
        if (string.IsNullOrEmpty(curso.Nome))
            throw new ArgumentException("O nome do curso é obrigatório");
            
        await _cursoRepositorio.AdicionarAsync(curso);
        return curso;
    }

    public async Task<bool> AtualizarAsync(Curso curso)
    {
        if (string.IsNullOrEmpty(curso.Nome))
            throw new ArgumentException("O nome do curso é obrigatório");
            
        return await _cursoRepositorio.AtualizarAsync(curso);
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        // Verifica se há alunos matriculados
        var alunosMatriculados = await _alunoRepositorio.ObterAlunosPorCursoAsync(id);
        if (alunosMatriculados.Any())
            throw new InvalidOperationException("Não é possível excluir um curso com alunos matriculados");
            
        return await _cursoRepositorio.ExcluirAsync(id);
    }
}