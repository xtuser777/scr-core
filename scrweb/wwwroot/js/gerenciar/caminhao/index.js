function preencherTabela(dados) {
    var txt = "";
    $.each(dados, function () {
        txt +=
            '<tr>\
                <td class="hidden">' + this.id + '</td>\
                <td>' + this.placa + '</td>\
                <td>' + this.marca + '</td>\
                <td>' + this.modelo + '</td>\
                <td>' + this.ano + '</td>\
                <td><a role="button" class="glyphicon glyphicon-edit" data-toggle="tooltip" data-placement="top" title="ALTERAR" href="javascript:alterar(' + this.id + ')"></a></td>\
                <td><a role="button" class="glyphicon glyphicon-trash" data-toggle="tooltip" data-placement="top" title="EXCLUIR" href="javascript:excluir(' + this.id + ')"></a></td>\
            </tr>';
    });
    $("#tbody_caminhoes").html(txt);
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

function obter() {
    var dados = get("/Caminhao/Obter");

    preencherTabela(dados);
}

$(document).ready(function (event) {
    obter();
});

function filtrar() {
    var filtro = $("#filtro").val();

    if (filtro === "") {
        obter();
    } else {
        $.ajax({
            type: "POST",
            url: "/Caminhao/ObterPorFiltro",
            data: {filtro: filtro},
            async: false,
            success: function (result) {
                preencherTabela(result);
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                mostraDialogo(
                    "<strong>Ocorreu um erro ao se comunicar com o servidor...</strong>" +
                    "<br/>" + errorThrown,
                    "danger",
                    2000
                );
            }
        });
    }
}

function ordenar() {
    var ord = $("#cbord").val();

    $.ajax({
        type: "POST",
        url: "/Caminhao/Ordenar",
        data: {coluna: coluna},
        async: false,
        success: function (result) {
            preencherTabela(result);
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            mostraDialogo(
                "<strong>Ocorreu um erro ao se comunicar com o servidor...</strong>" +
                "<br/>"+errorThrown,
                "danger",
                2000
            );
        }
    });
}

function alterar(id) {
    $.ajax({
        type: 'POST',
        url: '/Caminhao/Enviar',
        data: { id: id },
        success: function (result) {
            if (result.length > 0) alert(result);
            else {
                window.location.href = "../../gerenciar/caminhao/detalhes";
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            mostraDialogo(
                "<strong>Ocorreu um erro ao se comunicar com o servidor...</strong>" +
                "<br/>"+errorThrown,
                "danger",
                2000
            );
        }
    });
}

function excluir(id) {
    bootbox.confirm({
        message: "Confirma a exclusão deste registro?",
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
                    url: '/Caminhao/Excluir',
                    data: { id: id },
                    success: function (result) {
                        if (result === "") {
                            obter();
                        }
                        else {
                            mostraDialogo(
                                "<strong>Ocorreu um erro ao se comunicar com o servidor...</strong>" +
                                "<br/>"+result,
                                "danger",
                                2000
                            );
                        }
                    },
                    error: function (XMLHttpRequest, txtStatus, errorThrown) {
                        mostraDialogo(
                            "<strong>Ocorreu um erro ao se comunicar com o servidor...</strong>" +
                            "<br/>"+errorThrown,
                            "danger",
                            2000
                        );
                    }
                });
            }
        }
    });
}