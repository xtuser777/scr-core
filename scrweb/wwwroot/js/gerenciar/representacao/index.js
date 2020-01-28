var btFiltrar = document.getElementById('btFiltrar');
var btVoltar = document.getElementById('btVoltar');
var btAddUnid = document.getElementById('btAddUnid');
var btExcluir = document.getElementById("btExcluir");
var btDetalhes = document.getElementById('btDetalhes');
var btNovo = document.getElementById('btNovo');
var txFiltro = document.getElementById('txFiltro');
var dtFiltroCad = document.getElementById('dtFiltroCad');
var tbRepresentacoes = document.getElementById('tbRepresentacoes');
var tbRepresentacoesBody = document.getElementById('tbRepresentacoesBody');
var cbord = document.getElementById('cbord');

cbord.addEventListener("change", function (event) {
    var ord = cbord.value;
    
    $.ajax({
        type: 'POST',
        url: '/Representacao/Ordenar',
        async: false,
        data: { col : ord },
        success: function (response) { carregarTabela(response); },
        error: function () { alert("Ocorreu um problema ao comunicar-se com o servidor..."); }
    });
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
        var cellText1 = document.createTextNode(lista[i].pessoa.nomeFantasia);
        cell1.appendChild(cellText1);
        row.appendChild(cell1);

        var cell2 = document.createElement("td");
        var cellText2 = document.createTextNode(lista[i].pessoa.cnpj);
        cell2.appendChild(cellText2);
        row.appendChild(cell2);

        var cell3 = document.createElement("td");
        var cellText3 = document.createTextNode(FormatarData(lista[i].cadastro));
        cell3.appendChild(cellText3);
        row.appendChild(cell3);

        var cell4 = document.createElement("td");
        var cellText4 = document.createTextNode(lista[i].unidade);
        cell4.appendChild(cellText4);
        row.appendChild(cell4);

        var cell5 = document.createElement("td");
        var cellText5 = document.createTextNode(lista[i].pessoa.email);
        cell5.appendChild(cellText5);
        row.appendChild(cell5);

        tbRepresentacoesBody.appendChild(row);
    }

    var itensTabela = tbRepresentacoesBody.getElementsByTagName("tr");

    for (var i = 0; i < itensTabela.length; i++) {
        var item = itensTabela[i];
        item.addEventListener("click", function (event) {
            selecionarItem(this);
        });
    }
}

function limparTabela() {
    for (var i = tbRepresentacoesBody.childElementCount - 1; i >= 0; i--) {
        tbRepresentacoesBody.children.item(i).remove();
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

function obterRepresentacoes() {
    var data = get("/Representacao/Obter");
    carregarTabela(data);
}

document.addEventListener("ready", obterRepresentacoes());

function selecionarItem(item) {
    var itens = tbRepresentacoesBody.getElementsByTagName("tr");
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
        obterRepresentacoes();
    } else {
        if (filtro !== "" && cadastro !== "") {
            $.ajax({
                type: 'POST',
                url: '/Representacao/ObterPorChaveCad',
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
                    url: '/Representacao/ObterPorChave',
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
                        url: '/Representacao/ObterPorCadastro',
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

btAddUnid.addEventListener("click", function (event) {
    var selecionados = tbRepresentacoes.getElementsByClassName("selecionado");
    var selecionado = selecionados[0];
    if (selecionado != null && selecionado !== "") {
        selecionado = selecionado.getElementsByTagName("td");
        var id = selecionado[0].innerHTML;

        $.ajax({
            type: 'POST',
            url: '/Representacao/Enviar',
            data: {id: id},
            async: false,
            success: function (response) {
                if (response.length > 0) {
                    alert(response);
                } else {
                    window.location.href = "../../gerenciar/representacao/addunidade";
                }
            },
            erros: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus);
                alert("Error: " + errorThrown);
            }
        })
    } else {
        alert("Selecione pelo menos uma Representação!");
    }
});

btExcluir.addEventListener("click", function (event) {
    var selecionados = tbRepresentacoes.getElementsByClassName("selecionado");
    var selecionado = selecionados[0];
    if (selecionado != null && selecionado !== "") {
        selecionado = selecionado.getElementsByTagName("td");
        var id = selecionado[0].innerHTML;
        
        bootbox.confirm({
            message: "Confirma a exclusão desta representação?",
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
                        url: '/Representacao/Excluir',
                        data: {id: id},
                        success: function (result) {
                            if (result > 0) {
                                obterClientes();
                            } else {
                                alert("Ocorreu um problema ao excluir este cliente...");
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
        alert("Selecione pelo menos uma Representação!");
    }
});

btDetalhes.addEventListener("click", function (event) {
    var selecionados = tbRepresentacoes.getElementsByClassName("selecionado");
    var selecionado = selecionados[0];
    if (selecionado != null && selecionado !== "") {
        selecionado = selecionado.getElementsByTagName("td");
        var id = selecionado[0].innerHTML;
        
        $.ajax({
            type: 'POST',
            url: '/Representacao/Enviar',
            data: {id: id},
            async: false,
            success: function (response) {
                if (response.length > 0) {
                    mostraDialogo(
                        "Ocorreu um problema ao enviar as informações da representação...",
                        "danger",
                        2000
                    );
                } else {
                    window.location.href = "../../gerenciar/representacao/detalhes";
                }
            }
        })
    } else {
        alert("Selecione pelo menos uma Representação!");
    }
});

btNovo.addEventListener("click", function (event) {
    window.location.href = "../../gerenciar/representacao/novo";
});