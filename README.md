# API Consultório com Módulo de Envio de E-mails.
[![](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)]()
[![](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white)]()
[![](https://img.shields.io/badge/Microsoft_SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)]()  
[![](https://img.shields.io/badge/GitKraken-179287?style=for-the-badge&logo=GitKraken&logoColor=white)]() 
 
 # Colaboradores   
 [![](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)]() 
  
- Everton M. L. Gonçalves - [EvertonMLGoncalves](https://github.com/EvertonMLGoncalves)
- Daniel Alves - [DanieAlves](https://github.com/DanieAlves)
 
 ## Sobre o Projeto 📖
 #### Esse projeto refere à segunda parte da atividade em duplas de construção de API's REST com o ASP .NET solicita pelo nosso instrutor do programa +Devs2Blu. Nesta atividade, foi construída uma API para o controle de consultas, médicos e pacientes. As entidade Médico e Paciente possuem uma relação N - N. 
   
  ## Pré-requisitos ✏️ 
### Para utilizar nosso projeto, você deverá ter os seguintes programas instalados em seu computador: 
 1. ### Visual Studio (de preferência) ou Visual Studio Code;  
2.  ### SQL Server 
 
## Instalação - ConsultorioAPI 💿 
### Para instalar, siga os seguintes passos: 
1. ## Abra um repositório local (utilizando o git bash ou alguma ferramenta com interface gráfica): 
```bash 
git init
``` 
2. ## Execute o comando git clone para clonar o repositório do GitHub: 
```bash 
git clone https://github.com/EvertonMLGoncalves/APIConsultorio
```
3. ## Após clonar o repositório, abra o projeto "AliensAPI" e localize o arquivo chamado appsettings.json. Após abrí-lo, mude o primeiro valor de "server" da string "DefaultConnect" colocando o seu banco de dados: 
```json
"ConnectionStrings": {
    "DefaultConnection": "server=SEU-SERVIDOR-SQL-SERVER;database=APIConsultorio;trusted_connection=true;TrustServerCertificate=True"
  },
``` 
#### Dica: o nome do servidor pode ser obtido abrindo o SQL Server Management Studio (SSMS) no seu computador. Caso não possua, é recomendável a sua instalação 
4. ## Utilize (Ctrl + Q) e busque pelo "Console do gerenciador de pacotes". Após ter feito isso, execute dois comandos: 
```console
add-migration (qualquer_nome)
``` 
```console
update-database
``` 
#### Após ter feito isso, sua solução deve estar pronta para rodar. Fique atento aos próximos passos para a configuração do módulo de envio de e-mails.
  
## Instalação - Módulo de Envio de E-mails 📫
### Para instalar, siga os seguintes passos: 
1. ## Localize o arquivo appsettings.json e altere os seguintes dados:
```json 
"EmailHost": "seu_serviço_de_mensageria"
```
```json 
"EmailUsername": "seu_usuário@servico.com"
```
```json
"EmailPassword": "sua_senha"
``` 
#### Após ter feito isso, seu módulo de envio de e-mail deve estar pronto para o uso. Lembrando que seu endereço é: 
```
https://localhost:7043/api/Email
```   

  ## API Consultório ☁️
  #### É a parte principal do projeto, contendo todos os métodos de rota para realizar as requisições HTTP. 
  ### Componentes 
  #### Seus principais componentes são: 
- ### Program.cs 💻 -> É onde toda a API é executada, junto com os escopos, a conexão com o banco de dados e o swagger 
- ### Models 👨‍🦲 -> É onde estão contidos os três modelos principais do nosso projeto: Medico, Paciente e Consultorio 
- ### Data 💾 -> É onde está localizada a classe DataContext, responsável por definir as relações entre as entidades no banco de dados 
- ### Services 🛠️ -> Contém as clases e interfaces services, que são responsáveis por toda a lógica de acesso e envio de dados ao banco de dados. Possuem uma injeção de depedência de DataContext para o acesso ao banco. 
- ### DTOs 🛑 -> Contém todas as DTOs que são usadas pelas classes services para o envio e retorno de dados. 
- ### Controllers ⌨️ -> Contém os métodos de rota. Eles acessam as classes services através da injeção de dependência e apenas são responsáveis pela validação de nulidade e execução dos métodos HTTP.
- ### Migrations 💼 -> São as migrações responsáveis pelo mapeamento das relações entre as entidades. 
- ### EmailModule 📩 -> Contém a classe STMP, que é responsável pela conexão com o módulo de e-mails através de uma requisição post.  
 
## Módulo de E-mails 📫 
#### Esta parte do projeto é responsável pelo envio de e-mails para qualquer destinatário. 
### Componentes 
#### Seus principais componentes são:  
- ### Program.cs 💻 -> É responsável pela execução da API, swagger e pelos escopos dos serviços de e-mail 
- ### Service 🛠️ -> É responsável pelo envio de emails.
- ### DTOs 🛑 -> Contém a classe EmailDTO, que define o formato de envio de e-mails.
- ### Controller ⌨️ -> Contém o método de rota post para o envio de e-mail. É responsável apenas receber a requisição e acionar o método de envio da classe service, pois possui uma injeção de dependências da mesma

## Agradecimentos 👍 
#### Agradecemos a todos os responsáveis pelo programa +Devs2Blu, em especial à Blusoft e a Proway.  
 
## Lincença 🔓
#### Licensa MIT

 
