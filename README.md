# Ouvidoria
Projeto UGB

*** VISÃO GERAL

O sistema como um todo é composto por 3 projetos

- API ouvidoria............:  concentra as funcionalidades do sistema de forma geral em forma de api da web
- API Ouvidoria (testes)...:  projeto de testes do MSTest para a API de ouvidoria
- OuvidoriaApp.............:  aplicacao MVC que serve os clientes e se comunica com a API
				 
-------------------------

*** PARA RODAR

Recomenda se rodar este comando: "dotnet dev-certs https --trust", no Windows PowerShell como Administrador.

Em ambos as soluções/projetos, configure corretamente os arquivos "appsettings.json"
Caso queira executar os testes, abra a janela de Testes do Visual Studio e clique em "Executar Todos"

Inicie a depuração do projeto da API
Inicie a depuraçao do projeto Ouvidoria (MVC)

a aplicação web deverá aparecer no navegador em instantes

--------------------------

[!!!] NÃO TESTADO!
O envio de email em servidores Google e Outlook aparentemente não
podem mais ser feito via client convencional por restrições de segurança
dos serviços;

Neste caso, é indicado a utilização de um servidor de email privado/corporativo 
As configurações de email estão disponíveis no "appsettings.json" tanto da API quanto do projeto de Testes

