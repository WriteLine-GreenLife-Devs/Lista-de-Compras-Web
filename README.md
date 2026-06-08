# 🛒 Lista de Compras

## 📖 Sobre o Projeto

O **Lista de Compras** é uma aplicação web desenvolvida em **ASP.NET Core MVC** com o objetivo de auxiliar usuários no gerenciamento de listas de compras.

O sistema permite cadastrar categorias, produtos, listas de compras e itens pertencentes a cada lista, possibilitando o acompanhamento da quantidade de produtos e do valor estimado das compras.

O projeto foi desenvolvido utilizando uma arquitetura em camadas, promovendo organização, reutilização de código e facilidade de manutenção.

---

## ✨ Funcionalidades

### 📂 Categorias

* Cadastrar categorias.
* Editar categorias existentes.
* Excluir categorias.
* Visualizar categorias cadastradas.

### 🛍️ Produtos

* Cadastrar produtos.
* Associar produtos a categorias.
* Editar produtos.
* Excluir produtos.
* Visualizar produtos cadastrados.

### 📋 Listas de Compras

* Criar listas de compras.
* Definir status da lista.
* Editar listas.
* Excluir listas.
* Visualizar quantidade total de itens.
* Visualizar gasto estimado da lista.

### 🧾 Itens da Lista

* Adicionar produtos em listas de compras.
* Definir quantidade.
* Definir preço unitário.
* Calcular automaticamente o valor total.
* Atualizar automaticamente os totais da lista.
* Impedir produtos duplicados dentro da mesma lista.

---

## 🏗️ Arquitetura Utilizada

O projeto segue o padrão de arquitetura em camadas:

```text
ListaDeCompras.WebApplication
│
├── Compartilhado
│
├── ModuloCategoria
│
├── ModuloProduto
│
├── ModuloListasDeCompras
│
└── ModuloItensDaLista
```

Cada módulo possui:

```text
ModuloX
│
├── Apresentacao
│   ├── Controllers
│   ├── ViewModels
│   └── Views
│
├── Aplicacao
│   ├── DTOs
│   └── Serviços
│
├── Dominio
│   ├── Entidades
│   ├── Interfaces
│   └── Regras de Negócio
│
└── Infraestrutura
    └── Repositórios
```

---

## 🔧 Tecnologias Utilizadas

* ASP.NET Core MVC
* C#
* Razor Pages
* Bootstrap 5
* AutoMapper
* FluentResults
* Injeção de Dependência
* Programação Orientada a Objetos (POO)
* Persistência em Arquivos JSON

---

## 📋 Regras de Negócio

### Categorias

* Não permitir categorias inválidas.

### Produtos

* Todo produto deve pertencer a uma categoria.

### Listas de Compras

* Possuem data de criação.
* Possuem status.
* Mantêm o total de itens.
* Mantêm o gasto estimado.

### Itens da Lista

* Devem estar vinculados a uma lista.
* Devem estar vinculados a um produto.
* Quantidade deve ser maior que zero.
* Preço unitário deve ser maior que zero.
* O valor total é calculado automaticamente:

Valor Total = Quantidade × Preço Unitário

* Não é permitido cadastrar o mesmo produto duas vezes na mesma lista.

---

## 🔄 Fluxo de Funcionamento

1. O usuário cadastra categorias.
2. O usuário cadastra produtos vinculados às categorias.
3. O usuário cria uma lista de compras.
4. O usuário adiciona produtos à lista.
5. O sistema calcula automaticamente:

   * Quantidade total de itens.
   * Valor estimado da compra.
6. O usuário acompanha o status da lista.

---

## 📂 Estrutura dos Módulos

### 📁 Categoria

Responsável pelo gerenciamento das categorias dos produtos.

### 📁 Produto

Responsável pelo cadastro e manutenção dos produtos.

### 📁 Lista de Compras

Responsável pela criação e gerenciamento das listas.

### 📁 Itens da Lista

Responsável pela associação entre produtos e listas, controlando quantidade, preço e valor total.

---

## 🚀 Como Executar o Projeto

### Pré-requisitos

* .NET 8 SDK (ou versão utilizada no projeto)
* Visual Studio 2022

### Passos

```bash
git clone https://github.com/seu-usuario/lista-de-compras.git
```

Abra a solução no Visual Studio:

```bash
ListaDeCompras.WebApplication.sln
```

Execute o projeto:

```bash
Ctrl + F5
```

ou

```bash
F5
```

---

## 📸 Telas do Sistema

* Página Inicial
* Controle de Categorias
* Controle de Produtos
* Controle de Listas de Compras
* Controle de Itens da Lista

---

## 🎯 Objetivos do Projeto

* Aplicar conceitos de arquitetura em camadas.
* Aplicar princípios de orientação a objetos.
* Utilizar ASP.NET Core MVC.
* Implementar CRUD completo.
* Trabalhar com injeção de dependência.
* Utilizar AutoMapper e FluentResults.
* Implementar persistência de dados.

---

## 👨‍💻 Autor

Projeto desenvolvido por Gustavo Tessaro e Alec Luí para fins acadêmicos.
