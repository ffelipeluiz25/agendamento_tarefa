==========================================================================================
PRE REQUISITOS - Criar 2 usuarios Joao Silva e Ana Silva
==========================================================================================
requisição 1:
url: /usuario/criar
body:
{
  "nome": "João",
  "sobrenome": "Silva"
}

requisição 2:
url: /usuario/criar
body:
{
  "nome": "Ana",
  "sobrenome": "Silva"
}


==========================================================================================
1- Criar uma tarefa associada ao user João Silva agendada para o dia 31/12/2021
==========================================================================================
url: /tarefa/agendamento
body:
{
  "usuarioId": 1,
  "dataInicio": "2021-12-31",
  "duracao": 0,
  "status": 0
}
==========================================================================================
2- Criar uma tarefa associada à Ana Silva com data atual
==========================================================================================
url: /tarefa/agendamento
body:
{
  "usuarioId": 2,
  "dataInicio": "2023-04-14",
  "duracao": 0,
  "status": 0
}
==========================================================================================
3- Atualizar o estado da tarefa da Ana Silva para Em Andamento
==========================================================================================
url: /tarefa/atualiza-status
body:
{
  "status": 1,
  "usuarioId": 2
}
==========================================================================================
4- Retornar o período de tempo em que a tarefa esteve em Andamento
==========================================================================================
url: /tarefa/recupera-periodo-tempo/{tarefaId}
body:
{
  "status": 1,
  "usuarioId": 2
}
==========================================================================================
5- Finalizar uma tarefa
==========================================================================================
url: /tarefa/atualiza-status
body:
{
  "status": 2,
  "usuarioId": 2
}
==========================================================================================
6- Finalizar uma tarefa
==========================================================================================
url: /tarefa/agendamento
body:
{
  "usuarioId": 2,
  "dataInicio": "2023-04-14",
  "duracao": 0,
  "status": 0
}
==========================================================================================
7- Anexar um arquivo à uma tarefa – salvar local
==========================================================================================
url: /arquivo/{tarefaId} anexar arquivo