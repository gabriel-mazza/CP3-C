Farmácia Digital
Gabriel Barros Mazzariol RM-555410

Produto Escolhido
Produto: Plano de Saúde 
Justificativa: O Plano de Saúde é um produto essencial no contexto de uma farmácia digital, permitindo que clientes contratem cobertura médica diretamente pelo sistema. A regra de negócio implementada é a mensalidade variável por perfil de cliente:

Pessoa Física com 60 anos ou mais: acréscimo de 20% (faixa etária idoso, maior risco de saúde).
Pessoa Jurídica: desconto de 15% (contrato empresarial com maior volume).
Demais Pessoa Física: mensalidade base integral.


Diagrama de Classes
<img width="1121" height="841" alt="image" src="https://github.com/user-attachments/assets/2864666b-646a-4569-aef2-4c597470e386" />


Endpoints Disponíveis
POST /api/Filiais
Request:
json{
  "nome": "Farmácia Central",
  "endereco": "Rua das Flores, 100",
  "cidade": "São Paulo"
}
Response 201:
json{
  "id": 1,
  "nome": "Farmácia Central",
  "endereco": "Rua das Flores, 100",
  "cidade": "São Paulo"
}

GET /api/Filiais/{id}
Response 200:
json{
  "id": 1,
  "nome": "Farmácia Central",
  "endereco": "Rua das Flores, 100",
  "cidade": "São Paulo"
}

POST /api/Clientes/pf
Request:
json{
  "nome": "João da Silva",
  "email": "joao@email.com",
  "telefone": "11999998888",
  "cpf": "123.456.789-09",
  "dataNascimento": "1990-01-01",
  "filialId": 1
}
Response 201:
json{
  "id": 1,
  "nome": "João da Silva",
  "tipoCliente": "PF",
  "documento": "123.456.789-09",
  "filialId": 1
}

POST /api/Clientes/pj
Request:
json{
  "nome": "Empresa Saúde",
  "email": "rh@empresa.com",
  "telefone": "1133334444",
  "cnpj": "11.222.333/0001-44",
  "razaoSocial": "Empresa Saúde Ltda",
  "filialId": 1
}
Response 201:
json{
  "id": 2,
  "nome": "Empresa Saúde",
  "tipoCliente": "PJ",
  "documento": "11.222.333/0001-44",
  "filialId": 1
}

GET /api/Clientes/{id}
Response 200:
json{
  "id": 1,
  "nome": "João da Silva",
  "tipoCliente": "PF",
  "documento": "123.456.789-09",
  "filialId": 1,
  "filialNome": "Farmácia Central"
}

POST /api/Planos
Request:
json{
  "nome": "Plano Básico",
  "descricao": "Cobertura ambulatorial e hospitalar",
  "cobertura": "Básico",
  "mensalidadeBase": 200.00
}
Response 201:
json{
  "id": 1,
  "nome": "Plano Básico",
  "cobertura": "Básico",
  "mensalidadeBase": 200.00
}

POST /api/Adesoes
Request:
json{
  "clienteId": 1,
  "produtoId": 1
}
Response 201 — Cliente PF jovem (taxa base):
json{
  "id": 1,
  "nomeCliente": "João da Silva",
  "nomeProduto": "Plano Básico",
  "status": "Aprovada",
  "mensalidadeEfetiva": 200.00,
  "observacao": "Mensalidade: R$ 200,00 (taxa base)"
}
Response 201 — Cliente PF idoso (+20%):
json{
  "id": 2,
  "nomeCliente": "Maria Idosa",
  "nomeProduto": "Plano Básico",
  "status": "Aprovada",
  "mensalidadeEfetiva": 240.00,
  "observacao": "Mensalidade: R$ 240,00 (PF idoso 72 anos — 20% de acréscimo sobre base R$ 200,00)"
}
Response 201 — Cliente PJ (-15%):
json{
  "id": 3,
  "nomeCliente": "Empresa Saúde",
  "nomeProduto": "Plano Básico",
  "status": "Aprovada",
  "mensalidadeEfetiva": 170.00,
  "observacao": "Mensalidade: R$ 170,00 (PJ — 15% de desconto sobre base R$ 200,00)"
}

GET /api/Adesoes/{id}
Response 200: (mesmo formato acima)

Como Executar os Testes
bashcd FarmaciaDigital.Tests
dotnet test --verbosity normal
Resultado:
<img width="1920" height="1029" alt="image" src="https://github.com/user-attachments/assets/89b9a4e9-a84f-4d81-8ec7-5bb10c5762b1" />


Evidências de Funcionamento
<img width="1920" height="1032" alt="image" src="https://github.com/user-attachments/assets/fdd9871c-0891-46fd-84d5-774d518e2238" />


Swagger com adesão aprovada
<img width="800" height="429" alt="image" src="https://github.com/user-attachments/assets/b929ff10-20ed-4e2c-96c3-a55b7da90aa3" />


Como Executar o Projeto

Clone o repositório
Configure as credenciais Oracle no appsettings.json:

json{
  "ConnectionStrings": {
    "OracleFIAP": "Data Source=oracle.fiap.com.br:1521/ORCL;User Id=SEU_RM;Password=SUA_SENHA;"
  }
}

No console NuGet rode:

Add-Migration Inicial
Update-Database

Rode a API
Acesse o Swagger em: https://localhost:7200/swagger
