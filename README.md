# Easy Interests
Sistema para recálculo de dívidas.

## Descrição dos requisitos com exemplo
Desenvolva um sistema que atualize o cálculo de uma dívida.

### Deverá estar configurado dinamicamente:

- A quantidade de vezes máxima que a dividas pode ser parceladas
- Tipo de juros ( composto, simples ) e seu valor em porcentagem
- Porcentagem de comissão da Negociadora sobre a divida
- O cliente poderá entrar no sistema e apenas visualizar suas dívidas com:

    - Data de vencimento
    - Quantidade parcelas
    - Valor original
    - Dias atraso
    - Valor juros
    - Valor final
    - Valor final de cada parcela com sua data de vencimento
    - Telefone de orientação para ligar e negociar com um colaborador.

### Exemplo Configuração:

- Quantidade máxima de parcelas: 3
- Juros: Simples, 0.2% ao dia
- Porcentagem da comissão da Negociadora: 10%

### Exemplo Calculo:
- Divida: R$ 3000,00
- Data de vencimento: 01/03/2019
- Parcelas: 3x de R$ 1000,00
- Data do calculo: 10/03/2019

- Dias para cálculo de juros ( dias de atraso da divida ): Data do calculo - Data de vencimento: 10 dias
- Juros Simples: = 3000,00 *(1 + 0,2% juros ao dia * 10 dias)  

- Valor total com juros: R$ 3060,00

- Valor da comissão: R$ 3060 x 10% = R$ 306,00 

- Divida finalizada:    

    - Data de vencimento: 01/03/2019
    - Dias atraso: 10
    - Valor original: R$ 3000,00
    - Valor juros: R$ 60,00
    - Valor final: R$ 3060,00
    - Valor final de cada parcela: 
        1 - R$ 1020,00   - DataVencimento: 11/03/2018
        2 - R$ 1020,00   - DataVencimento: 11/04/2018
        3 - R$ 1020,00   - DataVencimento: 11/05/2018
    - Telefone de orientação para ligar e negociar com um colaborador: (XX) XXXX-XXXX


## Regras de negócio:

- Entidades
    - Usuário (User)
        - Cliente (Customer)
        - Negociador (Negotiator)
    - Dívida (Debt)

- Ações
    - Cliente poderá  visualizar as dívidas corrigidas juntamente do número do negociador;
    - Negociador poderá cadastrar novas dívidas e definir parâmetros dinâmicos como:
        - Comissão do negociador;
        - Regra de juros;
        - Quantidade de parcelas;
        
## Como rodar o ambiente:

### Back-end
- Ter o ASP NET Core 3.1 instalado;
- Criar appsettings.json baseado na versão development;
- Executar o projetoe EasyInterests.API.csproj;

### Front-end (web)
- Ter o yarn e node (ver. 12 ou superior) instalados;
- Instalar dependências com o comando "yarn";
- Rodar projeto com o comando "yarn start";
- É importante que o back-end esteja rodando no momento que executar o front-end;
