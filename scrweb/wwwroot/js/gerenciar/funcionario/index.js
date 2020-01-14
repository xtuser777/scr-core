var btFiltrar = document.getElementById('btFiltrar');
var btVoltar = document.getElementById('btVoltar');
var btDesativar = document.getElementById("btDesativar");
var btReativar = document.getElementById("btReativar");
var btDetalhes = document.getElementById('btDetalhes');
var btNovo = document.getElementById('btNovo');
var txFiltro = document.getElementById('txFiltro');
var dtFiltroAdm = document.getElementById('dtFiltroAdm');
var tbFuncionario = document.getElementById('tbFuncionarios');
var tbFuncionarioBody = document.getElementById('tbFuncionariosBody');
var cbord = document.getElementById('cbord');

var nivel_atual = "";
var funcs = [];

function ordenarLista() {
    var ord = cbord.value;

    switch (ord) {
        case "1":
            funcs.sort(function (a,b) {
                return a.id - b.id;
            });
            carregarTabela(funcs);
            break;
        case "2":
            funcs.sort(function (a,b) {
                return b.id - a.id;
            });
            carregarTabela(funcs);
            break;
        case "3":
            funcs.sort(function (a,b) {
                return a.funcionario.pessoa.nome - b.funcionario.pessoa.nome;
            });
            carregarTabela(funcs);
            break;
        case "4":
            funcs.sort(function (a,b) {
                return b.funcionario.pessoa.nome - a.funcionario.pessoa.nome;
            });
            carregarTabela(funcs);
            break;
        case "5":
            funcs.sort(function (a,b) {
                return a.login - b.login;
            });
            carregarTabela(funcs);
            break;
        case "6":
            funcs.sort(function (a,b) {
                return b.login - a.login;
            });
            carregarTabela(funcs);
            break;
        case "7":
            break;
        case "8":
            break;
        case "9":
            break;
        case "10":
            break;
        case "11":
            break;
        case "12":
            break;
        case "13":
            break;
        case "14":
            break;
        case "15":
            break;
        case "16":
            break;
        case "17":
            break;
        case "18":
            break;
    }
}

cbord.addEventListener("change", function (event) {
    ordenarLista();
});

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
        var cellText7 = document.createTextNode((lista[i].ativo === true) ? "Sim" : "Não");
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

function get(url_i) {
    let res;
    $.ajax({
        type: 'GET',
        url: url_i,
        async: false,
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {res = result;},
        error: function (err) {alert(err.d);}
    });
    return res;
}

function obterFuncionarios() {
    var data = get("/Funcionario/Obter");
    funcs = data;
    carregarTabela(data);
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

function verificarAdmin() {
    var data = get("/Funcionario/IsLastAdmin");
    return (data === true && nivel_atual === "Administrador");
}

btFiltrar.addEventListener("click", function (event) 
{
    var filtro = txFiltro.value;
    var admissao = dtFiltroAdm.value;
    
    if (filtro === "" && admissao === "") {
        obterFuncionarios();
    } else {
        if (filtro !== "" && admissao !== "") {
            $.ajax({
                type: 'POST',
                url: '/Funcionario/ObterPorChaveAdm',
                data: { chave: filtro, adm: admissao },
                success: function (response) {
                    if (response != null && response !== ""){
                        carregarTabela(response);
                    }
                },
                error: function () {
                    alert("Ocorreu um erro ao comunicar-se com o servidor...");
                }
            });
        } else {
            if (filtro !== "") {
                $.ajax({
                    type: 'POST',
                    url: '/Funcionario/ObterPorChave',
                    data: { chave: filtro },
                    success: function (response) {
                        if (response != null && response !== ""){
                            carregarTabela(response);
                        }
                    },
                    error: function () {
                        alert("Ocorreu um erro ao comunicar-se com o servidor...");
                    }
                });
            } else {
                if (admissao !== ""){
                    $.ajax({
                        type: 'POST',
                        url: '/Funcionario/ObterPorAdmissao',
                        data: { adm: admissao },
                        success: function (response) {
                            if (response != null && response !== ""){
                                carregarTabela(response);
                            }
                        },
                        error: function () {
                            alert("Ocorreu um erro ao comunicar-se com o servidor...");
                        }
                    });
                }
            }
        }
    }
});

btDesativar.addEventListener("click", function (event) {
    var selecionados = tbFuncionario.getElementsByClassName("selecionado");
    var selecionado = selecionados[0];
    if (selecionado != null && selecionado !== "") {
        selecionado = selecionado.getElementsByTagName("td");
        var id = selecionado[0].innerHTML;
        var status = selecionado[7].innerHTML;
        nivel_atual = selecionado[3].innerHTML;
        
        if (status === "Não") {
            alert("Este funcionário já está desativado!");
        } else {
            if (verificarAdmin() === true) {
                alert("Não é possível excluir o último administrador.");
            } else {
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
                                data: {id: id},
                                success: function (result) {
                                    if (result > 0) {
                                        obterFuncionarios();
                                    } else {
                                        alert(result);
                                    }
                                },
                                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                                    alert("Status: " + txtStatus);
                                    alert("Error: " + errorThrown);
                                    $("#divLoading").hide(300);
                                }
                            });
                        }
                    }
                });
            }
        }
    } else {
        alert("Selecione pelo menos um Funcionário!");
    }
});

btReativar.addEventListener("click", function (event) {
    var selecionados = tbFuncionario.getElementsByClassName("selecionado");
    var selecionado = selecionados[0];
    if (selecionado != null && selecionado !== "") {
        selecionado = selecionado.getElementsByTagName("td");
        var id = selecionado[0].innerHTML;
        var status = selecionado[7].innerHTML;
        if (status === "Sim") {
            alert("Este funcionário já está ativo!");
        } else {
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
        }
    } else {
        alert("Selecione pelo menos um Funcionário!");
    }
});

btDetalhes.addEventListener("click", function (event) 
{
    var selecionados = tbFuncionario.getElementsByClassName("selecionado");
    var selecionado = selecionados[0];
    if (selecionado != null && selecionado !== "") {
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