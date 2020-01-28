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
        form.append("desc", desc);
        form.append("eixos", eixos);
        form.append("capacidade", cap);

        $.ajax({
            type: "POST",
            url: "/TipoCaminhao/Gravar",
            data: form,
            contentType: false,
            processData: false,
            async: false,
            success: function(response) {
                if (response === "") {
                    mostraDialogo(
                        "<strong>Tipo de Caminhão gravado com sucesso!</strong>" +
                        "<br />Os dados do novo tipo foram salvos com sucesso no banco de dados.",
                        "success",
                        2000
                    );
                    limpar();
                } else {
                    mostraDialogo(
                        "<strong>Problemas ao salvar o novo tipo de caminhão...</strong>" +
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