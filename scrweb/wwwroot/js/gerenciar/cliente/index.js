var btFiltrar = document.getElementById('btFiltrar');
var btVoltar = document.getElementById('btVoltar');
var btExcluir = document.getElementById("btExcluir");
var btDetalhes = document.getElementById('btDetalhes');
var btNovo = document.getElementById('btNovo');
var txFiltro = document.getElementById('txFiltro');
var dtFiltroCad = document.getElementById('dtFiltroCad');
var tbClientes = document.getElementById('tbClientes');
var tbClientesBody = document.getElementById('tbClientesBody');
var cbord = document.getElementById('cbord');

var clientes = [];

cbord.addEventListener("change", function (event) {
    var ord = cbord.value;
    
    switch (ord) {
        case "1":
            clientes.sort(function (a,b) {
                return a.id - b.id;
            });
            carregarTabela(clientes);
            break;
        case "2":
            clientes.sort(function (a,b) {
                return b.id - a.id;
            });
            carregarTabela(clientes);
            break;
        case "4":
            break;
        case "5":
            break;
        case "6":
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
    }
});

function carregarTabela(lista) {
    limparTabela();
    for (var i = 0; i < lista.length; i++) {
        var row = document.createElement("tr");

        var cell0 = document.createElement("td");
        var cellText0 = document.createTextNode(lista[i].id);
        cell0.appendChild(cellText0);
        cell0.classList.add("hidden");
        row.appendChild(cell0);

        var cell1 = document.createElement("td");
        var cellText1 = document.createTextNode((lista[i].tipo === 1) ? lista[i].pessoa.nome : lista[i].pessoa.nomeFantasia );
        cell1.appendChild(cellText1);
        row.appendChild(cell1);

        var cell2 = document.createElement("td");
        var cellText2 = document.createTextNode((lista[i].tipo === 1) ? lista[i].pessoa.cpf : lista[i].pessoa.cnpj );
        cell2.appendChild(cellText2);
        row.appendChild(cell2);

        var cell3 = document.createElement("td");
        var cellText3 = document.createTextNode(FormatarData(lista[i].cadastro));
        cell3.appendChild(cellText3);
        row.appendChild(cell3);

        var cell4 = document.createElement("td");
        var cellText4 = document.createTextNode((lista[i].tipo === 1) ? 'Física' : 'Jurídica');
        cell4.appendChild(cellText4);
        row.appendChild(cell4);

        var cell5 = document.createElement("td");
        var cellText5 = document.createTextNode(lista[i].pessoa.email);
        cell5.appendChild(cellText5);
        row.appendChild(cell5);

        tbClientesBody.appendChild(row);
    }

    var itensTabela = tbClientesBody.getElementsByTagName("tr");

    for (var i = 0; i < itensTabela.length; i++) {
        var item = itensTabela[i];
        item.addEventListener("click", function (event) {
            selecionarItem(this);
        });
    }
}

function limparTabela() {
    for (var i = tbClientesBody.childElementCount - 1; i >= 0; i--) {
        tbClientesBody.children.item(i).remove();
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

function obterClientes() {
    var data = get("/Cliente/Obter");
    clientes = data;
    carregarTabela(data);
}

document.addEventListener("ready", obterClientes());

function selecionarItem(item) {
    var itens = tbClientesBody.getElementsByTagName("tr");
    for(var i = 0; i < itens.length; i++)
    {
        var item_ = itens[i];
        item_.classList.remove("selecionado");
    }
    item.classList.toggle("selecionado");
}

btFiltrar.addEventListener("click", function (event) {
    var filtro = txFiltro.value;
    var cadastro = dtFiltroCad.value;

    if (filtro === "" && cadastro === "") {
        obterClientes();
    } else {
        if (filtro !== "" && cadastro !== "") {
            $.ajax({
                type: 'POST',
                url: '/Cliente/ObterPorChaveCad',
                data: { chave: filtro, cad: cadastro },
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
                    url: '/Cliente/ObterPorChave',
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
                if (cadastro !== ""){
                    $.ajax({
                        type: 'POST',
                        url: '/Cliente/ObterPorCadastro',
                        data: { cad: cadastro },
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

btVoltar.addEventListener("click", function (event) {
    window.location.href = '../../inicio/index';
});

btExcluir.addEventListener("click", function (event) {
    var selecionados = tbClientes.getElementsByClassName("selecionado");
    var selecionado = selecionados[0];
    if (selecionado != null && selecionado !== "") {
        selecionado = selecionado.getElementsByTagName("td");
        var id = selecionado[0].innerHTML;
        
        bootbox.confirm({
            message: "Confirma a exclusão deste cliente?",
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
                        url: '/Cliente/Excluir',
                        data: {id: id},
                        success: function (result) {
                            if (result > 0) {
                                obterClientes();
                            } else {
                                alert(result);
                            }
                        },
                        error: function (XMLHttpRequest, txtStatus, errorThrown) {
                            alert("Status: " + txtStatus);
                            alert("Error: " + errorThrown);
                        }
                    });
                }
            }
        });
    } else {
        alert("Selecione pelo menos um Cliente!");
    }
});

btDetalhes.addEventListener("click", function (event) {
    var selecionados = tbClientes.getElementsByClassName("selecionado");
    var selecionado = selecionados[0];
    if (selecionado != null && selecionado !== "") {
        selecionado = selecionado.getElementsByTagName("td");
        var id = selecionado[0].innerHTML;
        window.location.href = "../../Cliente/Detalhes/"+id;
    } else {
        alert("Selecione pelo menos um Cliente!");
    }
});

btNovo.addEventListener("click", function (event) {
    window.location.href = "../../gerenciar/cliente/novo";
});