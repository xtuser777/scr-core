var btFiltrar = document.getElementById('btFiltrar');
var btVoltar = document.getElementById('btVoltar');
var btDetalhes = document.getElementById('btDetalhes');
var btNovo = document.getElementById('btNovo');
var txFiltro = document.getElementById('txFiltro');
var dtFiltroAdmissao = document.getElementById('dtFiltroAdmissao');
var tbFuncionario = document.getElementById('tbFuncionarios');
var tbFuncionarioBody = document.getElementById('tbFuncionariosBody');

function carregarTabela(lista) 
{
    for (var i = 0; i < lista.length; i++) 
    {
        var row = document.createElement("tr");

        var cell0 = document.createElement("td");
        var cellText0 = document.createTextNode(lista[i].id);
        cell0.appendChild(cellText0);
        cell0.classList.add("hidden");
        row.appendChild(cell0);

        var cell1 = document.createElement("td");
        var cellText1 = document.createTextNode(lista[i].funcionario.pessoa.nome);
        cell1.appendChild(cellText1);
        row.appendChild(cell1);

        var cell2 = document.createElement("td");
        var cellText2 = document.createTextNode(lista[i].login);
        cell2.appendChild(cellText2);
        row.appendChild(cell2);

        var cell3 = document.createElement("td");
        var cellText3 = document.createTextNode(lista[i].nivel.descricao);
        cell3.appendChild(cellText3);
        row.appendChild(cell3);

        var cell4 = document.createElement("td");
        var cellText4 = document.createTextNode(lista[i].funcionario.pessoa.cpf);
        cell4.appendChild(cellText4);
        row.appendChild(cell4);

        var cell5 = document.createElement("td");
        var cellText5 = document.createTextNode(FormatarData(lista[i].funcionario.admissao));
        cell5.appendChild(cellText5);
        row.appendChild(cell5);

        var cell6 = document.createElement("td");
        var cellText6 = document.createTextNode((lista[i].funcionario.tipo == 1) ? "Interno" : "Vendedor");
        cell6.appendChild(cellText6);
        row.appendChild(cell6);

        var cell7 = document.createElement("td");
        var cellText7 = document.createTextNode(lista[i].funcionario.pessoa.email);
        cell7.appendChild(cellText7);
        row.appendChild(cell7);

        tbFuncionarioBody.appendChild(row);
    }

    var itensTabela = tbFuncionarioBody.getElementsByTagName("tr");

    for (var i = 0; i < itensTabela.length; i++) 
    {
        var item = itensTabela[i];
        item.addEventListener("click", function (event) 
        {
            selecionarItem(this);
        });
    }
}

function obterFuncionarios() 
{
    $.getJSON("/Funcionario/Obter", function (data) 
    {
        carregarTabela(data);
    });
}

document.addEventListener("ready", obterFuncionarios());

function selecionarItem(item) 
{
    var itens = tbFuncionarioBody.getElementsByTagName("tr");
    for(var i = 0; i < itens.length; i++)
    {
        var item_ = itens[i];
        item_.classList.remove("selecionado");
    }
    item.classList.toggle("selecionado");
}

btFiltrar.addEventListener("click", function (event) 
{
   
});

btDetalhes.addEventListener("click", function (event) 
{
    var selecionados = tbFuncionario.getElementsByClassName("selecionado");
    var selecionado = selecionados[0];
    if (selecionado != null && selecionado != "") {
        selecionado = selecionado.getElementsByTagName("td");
        var id = selecionado[0].innerHTML;
        window.location.href = "../../Funcionario/Detalhes/"+id;
    } else {
        alert("Selecione pelo menos um Funcionário!");
    }
});

btNovo.addEventListener("click", function (event) 
{
    window.location.href = "../../gerenciar/funcionario/novo";
});