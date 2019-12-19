var btFiltrar = document.getElementById('btFiltrar');
var btVoltar = document.getElementById('btVoltar');
var btDesativar = document.getElementById("btDesativar");
var btReativar = document.getElementById("btReativar");
var btDetalhes = document.getElementById('btDetalhes');
var btNovo = document.getElementById('btNovo');
var txFiltro = document.getElementById('txFiltro');
var dtFiltroAdmissao = document.getElementById('dtFiltroAdmissao');
var tbFuncionario = document.getElementById('tbFuncionarios');
var tbFuncionarioBody = document.getElementById('tbFuncionariosBody');

var nivel_atual = "";
var funcs = null;

function carregarTabela(lista) 
{
    limparTabelaFuncionarios();
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
        var cellText6 = document.createTextNode((lista[i].funcionario.tipo === 1) ? "Interno" : "Vendedor");
        cell6.appendChild(cellText6);
        row.appendChild(cell6);

        var cell7 = document.createElement("td");
        var cellText7 = document.createTextNode((lista[i].funcionario.demissao === null) ? "Sim" : "Não");
        cell7.appendChild(cellText7);
        row.appendChild(cell7);

        var cell8 = document.createElement("td");
        var cellText8 = document.createTextNode(lista[i].funcionario.pessoa.email);
        cell8.appendChild(cellText8);
        row.appendChild(cell8);

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

function limparTabelaFuncionarios() {
    for (var i = tbFuncionarioBody.childElementCount - 1; i >= 0; i--) {
        tbFuncionarioBody.children.item(i).remove();
    }
}

function obterFuncionarios() 
{
    $.getJSON("/Funcionario/Obter", function (data) 
    {
        funcs = data;
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

/**
 * @return {string}
 */
function Get(whateverUrl){
    var Httpreq = new XMLHttpRequest(); // a new request
    Httpreq.open("GET",whateverUrl,false);
    Httpreq.send(null);
    return Httpreq.responseText;
}

function verificarAdmin() {
    var data = JSON.parse(Get("/Funcionario/IsLastAdmin"));
    return (data === true && nivel_atual === "Administrador");
}

btFiltrar.addEventListener("click", function (event) 
{
    var filtro = txFiltro.value;
    var filtrado = [];
    for (var i = 0; i < funcs.length; i++) {
        var login = funcs[i].login;
        var nome = funcs[i].funcionario.pessoa.nome;
        if (nome.includes(filtro) || login.includes(filtro)) {
            filtrado.push(funcs[i]);
        }
    }
    carregarTabela(filtrado);
});

btDesativar.addEventListener("click", function (event) {
    var selecionados = tbFuncionario.getElementsByClassName("selecionado");
    var selecionado = selecionados[0];
    if (selecionado != null && selecionado !== "") {
        selecionado = selecionado.getElementsByTagName("td");
        var id = selecionado[0].innerHTML;
        nivel_atual = selecionado[3].innerHTML;
        if (verificarAdmin() === false) {
            bootbox.confirm({
                message: "Confirma o desligamento deste funcionário?",
                buttons: {
                    confirm: {
                        label: 'Sim',
                        className: 'btn-success'
                    },
                    cancel: {
                        label: 'Não',
                        className: 'btn-danger'
                    }
                },
                callback: function (result) {
                    if (result) {
                        $.ajax({
                            type: 'POST',
                            url: '/Funcionario/Desativar',
                            data: { id: id },
                            success: function (result) {
                                if (result > 0) {
                                    obterFuncionarios();
                                }
                                else {
                                    alert(result);
                                }
                            },
                            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                                $("#divLoading").hide(300);
                            }
                        });
                    }
                }
            });
        }
    } else {
        alert("Selecione pelo menos um Funcionário!");
    }
});

btReativar.addEventListener("click", function (event) {
    var selecionados = tbFuncionario.getElementsByClassName("selecionado");
    var selecionado = selecionados[0];
    if (selecionado != null && selecionado != "") {
        selecionado = selecionado.getElementsByTagName("td");
        var id = selecionado[0].innerHTML;
        bootbox.confirm({
            message: "Confirma a Reativação deste funcionário?",
            buttons: {
                confirm: {
                    label: 'Sim',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'Não',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        type: 'POST',
                        url: '/Funcionario/Reativar',
                        data: { id: id },
                        success: function (result) {
                            if (result > 0) {
                                obterFuncionarios();
                            }
                            else {
                                alert(result);
                            }
                        },
                        error: function (XMLHttpRequest, txtStatus, errorThrown) {
                            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                            $("#divLoading").hide(300);
                        }
                    });
                }
            }
        });
    } else {
        alert("Selecione pelo menos um Funcionário!");
    }
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