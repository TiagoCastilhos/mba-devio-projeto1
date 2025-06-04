# Feedback - Avaliação Geral

## Front End

### Navegação
  * Pontos positivos:
    - Projeto MVC implementado com rotas e views para autenticação, produtos e categorias.
    - Funcionalidade de CRUD acessível e estruturada de forma clara.

  * Pontos negativos:
    - Nenhum ponto negativo encontrado na navegação.

### Design
  - O design da interface administrativa segue um padrão funcional e adequado para a proposta.

### Funcionalidade
  * Pontos positivos:
    - Registro do vendedor no momento da criação do usuário conforme especificação.
    - CRUD completo para produtos e categorias disponível tanto em API quanto em MVC.
    - Mecanismos de autenticação e autorização implementados com ASP.NET Identity.
    - Configuração de SQLite, migrations e seed de dados corretamente aplicada.

  * Pontos negativos:
    - Nenhum ponto funcional negativo.

## Back End

### Arquitetura
  * Pontos positivos:
    - Arquitetura em camadas bem organizada com separação entre API, MVC, Core e Data.
    - Camada `Core` concentra serviços, modelos e interfaces de forma coesa e clara.

  * Pontos negativos:
    - Embora bem estruturada, a implementação apresenta um nível de complexidade maior do que o necessário para a proposta do projeto, conforme o escopo. O excesso de abstrações pode ser considerada excessiva e negativa perante um desafio simples. Complexidade depende de necessidade, pois se torna um problema grave.

### Funcionalidade
  * Pontos positivos:
    - Registro e autenticação integrados.
    - Implementação consistente de regras de domínio.
    - Validações e controle de acesso aplicados corretamente.

  * Pontos negativos:
    - Nenhum.

### Modelagem
  * Pontos positivos:
    - Entidades bem modeladas e nomeadas.
    - Associações entre produto, categoria e vendedor condizem com o domínio.

  * Pontos negativos:
    - Nenhum.

## Projeto

### Organização
  * Pontos positivos:
    - Projeto bem organizado, com `src` na raiz, arquivos `.sln`, `README.md`, `FEEDBACK.md` e pastas bem nomeadas.
    - Swagger presente para testes da API.

  * Pontos negativos:
    - A linguagem usada (nomes de classes, arquivos, pastas) está predominantemente em inglês, desconsiderando a diretriz de usar a linguagem de negócio (português).

### Documentação
  * Pontos positivos:
    - README.md e FEEDBACK.md presentes e informativos.
    - Documentação via Swagger configurada.

### Instalação
  * Pontos positivos:
    - SQLite configurado como provider de desenvolvimento.
    - Migrations automáticas e seed de dados ativados no startup.

  * Pontos negativos:
    - Nenhum.

---

# 📊 Matriz de Avaliação de Projetos

| **Critério**                   | **Peso** | **Nota** | **Resultado Ponderado**                  |
|-------------------------------|----------|----------|------------------------------------------|
| **Funcionalidade**            | 30%      | 10       | 3,0                                      |
| **Qualidade do Código**       | 20%      | 10       | 2,0                                      |
| **Eficiência e Desempenho**   | 20%      | 7        | 1,4                                      |
| **Inovação e Diferenciais**   | 10%      | 10       | 1,0                                      |
| **Documentação e Organização**| 10%      | 10       | 1,0                                      |
| **Resolução de Feedbacks**    | 10%      | 10       | 1,0                                      |
| **Total**                     | 100%     | -        | **9,4**                                  |

## 🎯 **Nota Final: 9,4 / 10**
