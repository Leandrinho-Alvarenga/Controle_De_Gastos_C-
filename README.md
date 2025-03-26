# Controle de Gastos - README

## 📌 Sobre o Projeto
Este é um sistema simples de **controle de gastos** desenvolvido em C#. Ele permite adicionar, listar e remover transações financeiras, associando informações como **nome, idade e descrição**. Os dados são salvos em um arquivo JSON para persistência.

## 🚀 Funcionalidades
- Adicionar uma nova transação (entrada ou despesa).
- Listar todas as transações registradas.
- Remover uma transação pelo ID.
- Exibir um resumo do saldo total.

## 🛠️ Como Utilizar

### 🔹 Pré-requisitos
- Ter o **.NET SDK** instalado.
- Um editor como **Visual Studio Code** ou **Visual Studio**.

### 🔹 Rodando o Projeto
1. Clone este repositório ou baixe os arquivos.
2. Abra um terminal na pasta do projeto e execute:
   ```sh
   dotnet run
   ```

### 🔹 Navegação no Menu
Ao executar o programa, você verá um menu com as seguintes opções:

1️⃣ **Adicionar Transação**: Insira os dados da transação.
2️⃣ **Listar Transações**: Exibe todas as transações cadastradas.
3️⃣ **Remover Transação**: Informe o ID para excluir uma transação.
4️⃣ **Sair**: Encerra o programa, salvando as alterações.

## 📂 Estrutura do Projeto
```
📁 Projeto
│-- Program.cs  # Código principal do sistema
│-- README.md  # Este arquivo
```

## ⚡ Exemplo de Uso
```
=== Controle de Gastos ===
Entrada: R$500.00
Saída: R$200.00
Total: R$300.00
==========================

1 - Adicionar Transação
2 - Listar Transações
3 - Remover Transação
4 - Sair
Escolha uma opção: 
```
