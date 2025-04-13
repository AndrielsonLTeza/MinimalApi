Atividade da Aula de TDS UTFPR 1/2025

Sistema de Gerenciamento de Cursos e
Alunos em .NET
Este documento apresenta dois enunciados para a criação de um sistema de gerenciamento de cursos e alunos
utilizando .NET com C#, abordando duas implementações diferentes: Minimal API e Controller padrão.
Comparação entre Minimal API e Controller
Padrão
Enunciado 1 4 Minimal API
Você deve criar uma aplicação .NET com C Sharp, utilizando o modelo de Minimal API, que represente um
sistema de gerenciamento de cursos e alunos.
Requisitos
Modelos
Crie as classes Aluno e Curso.
Um Curso pode ter múltiplos Alunos matriculados.
Um Aluno pertence a apenas um Curso.
Camadas
Camada de modelo com as entidades e suas propriedades.
Camada de repositório com operações simuladas (pode ser in-memory).
Camada de serviço, contendo as regras de negócio e a lógica de manipulação dos dados.
Operações Implemente as operações CRUD para as entidades:
Curso: Criar, Listar, Buscar por ID, Atualizar, Excluir.
Aluno: Criar, Listar, Buscar por ID, Atualizar, Excluir.
Criar uma rota que associe alunos a um curso (ex: POST /cursos/{id}/matricular).
Tecnologias
Use .NET 7 ou superior.
Utilize a abordagem de Minimal APIs no Program.cs.
Extras (opcional)
Se quiser, utilize um dicionário ou lista in-memory como base de dados simulada.
Pode adicionar validações básicas de entrada.
