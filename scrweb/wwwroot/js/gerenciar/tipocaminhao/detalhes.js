var _tipo = 0;

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

$(document).ready(function (event) {
    var dados = get("/TipoCaminhao/ObterDetalhes");
    if (dados !== "") {
        _tipo = dados.id;
        $("#desc").val(dados.descricao);
        $("#eixos").val(dados.eixos);
        $("#capacidade").val(dados.capacidade);
    }
});

function gravar() {
    var desc = $("#desc").val();
    var eixos = $("#eixos").val();
    var cap = $("#capacidade").val();
    
    var erros = 0;

    if (desc === "") {
        erros++;
        $("#msdesc").html('<span class="label label-danger">A descrição do tipo precisa ser preenchida!</span>');
    } else {
        $("#msdesc").html('');
    }

    if (eixos === "" || eixos === "0") {
        erros++;
        $("#mseixos").html('<span class="label label-danger">A quantidade de eixos precisa ser preenchida!</span>');
    } else {
        $("#mseixos").html('');
    }

    if (cap === "" || cap === "0") {
        erros++;
        $("#mscap").html('<span class="label label-danger">A capacidade total em KG deve ser preenchida!</span>');
    } else {
        $("#mscap").html('');
    }
    
    if (erros === 0) {
        var form = new FormData();
        form.append("tipo", _tipo);
        form.append("desc", desc);
        form.append("eixos", eixos);
        form.append("capacidade", cap);

        $.ajax({
            type: "POST",
            url: "/TipoCaminhao/Alterar",
            data: form,
            contentType: false,
            processData: false,
            async: false,
            success: function(response) {
                if (response === "") {
                    mostraDialogo(
                        "<strong>Alterações do Tipo de Caminhão foram salvas com sucesso!</strong>" +
                        "<br />As alterações do tipo foram salvos com sucesso no banco de dados.",
                        "success",
                        2000
                    );
                } else {
                    mostraDialogo(
                        "<strong>Problemas ao alterar o tipo de caminhão...</strong>" +
                        "<br/>response",
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
    } else {
        $("#desc").val(desc);
        $("#eixos").val(eixos);
        $("#capacidade").val(cap);
    }
}

function limpar() {
    $("input[type='text']").val("");
    $("input[type='number']").val("0");
    $("#capacidade").val("0");
}