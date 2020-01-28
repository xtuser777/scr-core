var txrazaosocial = document.getElementById("txRazaoSocial");
var txnomefantasia = document.getElementById("txNomeFantasia");
var txcnpj = document.getElementById("txCnpj");
var txrua = document.getElementById("txRua");
var txnumero = document.getElementById("txNumero");
var txbairro = document.getElementById("txBairro");
var txcomplemento = document.getElementById("txComplemento");
var cbestado = document.getElementById("cbestado");
var cbcidade = document.getElementById("cbcidade");
var txcep = document.getElementById("txCep");
var txtel = document.getElementById("txTel");
var txcel = document.getElementById("txCel");
var txemail = document.getElementById("txEmail");

var btlimpar = document.getElementById("btLimpar");
var btsalvar = document.getElementById("btSalvar");
var btvoltar = document.getElementById("btVoltar");

var msRazaoSocial = document.getElementById("msRazaoSocial");
var msNomeFantasia = document.getElementById("msNomeFantasia");
var msCnpj = document.getElementById("msCnpj");
var msRua = document.getElementById("msRua");
var msNumero = document.getElementById("msNumero");
var msBairro = document.getElementById("msBairro");
var msCep = document.getElementById("msCep");
var msEstado = document.getElementById("msEstado");
var msCidade = document.getElementById("msCidade");
var msTelefone = document.getElementById("msTelefone");
var msCelular = document.getElementById("msCelular");
var msEmail = document.getElementById("msEmail");

var lista_estados = [];
var lista_cidades = [];
var erros = 0;

function limparEstados() {
    for (var i = cbestado.childElementCount - 1; i > 0; i--) {
        cbestado.children.item(i).remove();
    }
}

function carregarCidades() {
    var form = new FormData();
    form.append("estado", cbestado.value);

    $.ajax({
        type: 'POST',
        url: '/Representacao/ObterCidades',
        data: form,
        contentType: false,
        processData: false,
        async: false,
        success: function (response) {lista_cidades = response;},
        error: function (err) {
            mostraDialogo(
                "<strong>Ocorreu um problema ao se comunicar com o servidor...</strong>" +
                "<br/>Um problema no servidor impediu sua comunicação...",
                "danger",
                2000
            );
        }
    });

    limparCidades();
    if (lista_cidades !== "") {
        for (var i = 0; i < lista_cidades.length; i++) {
            var option = document.createElement("option");
            option.value = lista_cidades[i].id; 
            option.text = lista_cidades[i].nome;
            cbcidade.appendChild(option);
        }
    }
}

$(cbestado).change(function (event) {
    if (cbestado.value === "0") {
        limparCidades();
        cbcidade.disabled = true;
    } else {
        carregarCidades();
        cbcidade.disabled = false;
    }
});

function limparCidades() {
    for (var i = cbcidade.childElementCount - 1; i > 0; i--) {
        cbcidade.children.item(i).remove();
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
        error: function (err) {
            mostraDialogo(
                "<strong>Ocorreu um problema ao se comunicar com o servidor...</strong>" +
                "<br/>Um problema no servidor impediu sua comunicação...",
                "danger",
                2000
            );
        }
    });
    return res;
}

$(document).ready(function () {
    $(txcnpj).mask('00.000.000/0000-00', {reverse: false});
    $(txcep).mask('00.000-000', {reverse: false});
    $(txtel).mask('(00) 0000-0000', {reverse: false});
    $(txcel).mask('(00) 00000-0000', {reverse: false});

    lista_estados = get('/Representacao/ObterEstados');
    limparEstados();
    if (lista_estados !== "") {
        for (var i = 0; i < lista_estados.length; i++) {
            var option = document.createElement("option");
            option.value = lista_estados[i].id;
            option.text = lista_estados[i].nome;
            cbestado.appendChild(option);
        }
    }
    
    cbcidade.disabled = true;
});

function limparCampos() {
    txrazaosocial.value = "";
    txnomefantasia.value = "";
    txcnpj.value = "";
    txrua.value = "";
    txnumero.value = "";
    txbairro.value = "";
    txcomplemento.value = "";
    txcep.value = "";
    cbestado.value = "0";
    cbcidade.value = "0";
    txtel.value = "";
    txcel.value = "";
    txemail.value = "";
}

btlimpar.addEventListener("click", function (event) {
    limparCampos();
});

btvoltar.addEventListener("click", function (event) {
    limparCampos();
    window.location.href = "../../gerenciar/representacao/index";
});

function verificarCnpj(cnpj) {
    $.ajax({
        type: 'POST',
        url: '/Representacao/VerificarCnpj',
        data: { cnpj: cnpj },
        async: false,
        success: function (response) {
            if (response === true) {
                erros++;
                msCnpj.innerHTML = "O CNPJ informado já existe no cadastro...";
                msCnpj.classList.remove("hidden");
            } else {
                if (msCnpj.classList.contains("hidden") === false) { msCnpj.classList.add("hidden"); }
            }
        },
        error: function () {
            mostraDialogo(
                "<strong>Ocorreu um problema ao se comunicar com o servidor...</strong>" +
                "<br/>Um problema no servidor impediu sua comunicação...",
                "danger",
                2000
            );
        }
    });
}

function validarCNPJ(cnpj) {

    cnpj = cnpj.replace(/[^\d]+/g,'');

    if(cnpj === '') return false;

    if (cnpj.length !== 14)
        return false;

    // Elimina CNPJs invalidos conhecidos
    if (cnpj === "00000000000000" ||
        cnpj === "11111111111111" ||
        cnpj === "22222222222222" ||
        cnpj === "33333333333333" ||
        cnpj === "44444444444444" ||
        cnpj === "55555555555555" ||
        cnpj === "66666666666666" ||
        cnpj === "77777777777777" ||
        cnpj === "88888888888888" ||
        cnpj === "99999999999999")
        return false;

    // Valida DVs
    tamanho = cnpj.length - 2;
    numeros = cnpj.substring(0,tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado.toString().charAt(0) !== digitos.charAt(0))
        return false;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0,tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado.toString().charAt(0) !== digitos.charAt(1))
        return false;

    return true;
}

function validacaoEmail(email) {
    usuario = email.substring(0, email.indexOf("@"));
    dominio = email.substring(email.indexOf("@")+ 1, email.length);
    if (
        (usuario.length >=1) &&
        (dominio.length >=3) &&
        (usuario.search("@")===-1) &&
        (dominio.search("@")===-1) &&
        (usuario.search(" ")===-1) &&
        (dominio.search(" ")===-1) &&
        (dominio.search(".")!==-1) &&
        (dominio.indexOf(".") >=1)&&
        (dominio.lastIndexOf(".") < dominio.length - 1)
    ) {
        return true;
    } else {
        return false;
    }
}

btsalvar.addEventListener("click", function (event) {
    var razaosocial = txrazaosocial.value;
    var nomefantasia = txnomefantasia.value;
    var cnpj = txcnpj.value;
    var rua = txrua.value;
    var numero = txnumero.value;
    var bairro = txbairro.value;
    var complemento = txcomplemento.value;
    var cep = txcep.value;
    let cidade = cbcidade.value;
    var telefone = txtel.value;
    var celular = txcel.value;
    var email = txemail.value;

    erros = 0;
    
    if (razaosocial.length === 0) {
        erros++;
        msRazaoSocial.innerHTML = "A Razão Social precisa ser preenchida!";
        msRazaoSocial.classList.remove("hidden");
    } else
    if (razaosocial.length < 3) {
        erros++;
        msRazaoSocial.innerHTML = "A Razão Social informada é inválida...";
        msRazaoSocial.classList.remove("hidden");
    } else {
        if (msRazaoSocial.classList.contains("hidden") === false) {
            msRazaoSocial.classList.add("hidden");
        }
    }

    if (nomefantasia.length === 0) {
        erros++;
        msNomeFantasia.innerHTML = "O Nome Fantasia precisa ser preenchido!";
        msNomeFantasia.classList.remove("hidden");
    } else {
        if (msNomeFantasia.classList.contains("hidden") === false) {
            msNomeFantasia.classList.add("hidden");
        }
    }

    if (cnpj.length === 0) {
        erros++;
        msCnpj.innerHTML = "O CNPJ precisa ser preenchido!";
        msCnpj.classList.remove("hidden");
    } else
    if (!validarCNPJ(cnpj)) {
        erros++;
        msCnpj.innerHTML = "O CNPJ informado é inválido...";
        msCnpj.classList.remove("hidden");
    } else {
        verificarCnpj(cnpj);
    }

    if (rua.length === 0) {
        erros++;
        msRua.innerHTML = "A Rua precisa ser preenchida!";
        msRua.classList.remove("hidden");
    } else {
        if (msRua.classList.contains("hidden") === false) {
            msRua.classList.add("hidden");
        }
    }

    if (numero.length === 0) {
        erros++;
        msNumero.innerHTML = "O Número precisa ser preenchido!";
        msNumero.classList.remove("hidden");
    } else {
        if (msNumero.classList.contains("hidden") === false) {
            msNumero.classList.add("hidden");
        }
    }

    if (bairro.length === 0) {
        erros++;
        msBairro.innerHTML = "O Bairro precisa ser preenchido!";
        msBairro.classList.remove("hidden");
    } else {
        if (msBairro.classList.contains("hidden") === false) {
            msBairro.classList.add("hidden");
        }
    }

    if (cep.length === 0) {
        erros++;
        msCep.innerHTML = "O CEP precisa ser preenchido!";
        msCep.classList.remove("hidden");
    } else
    if (cep.length < 10) {
        erros++;
        msCep.innerHTML = "O CEP informado é inválido...";
        msCep.classList.remove("hidden");
    } else {
        if (msCep.classList.contains("hidden") === false) {
            msCep.classList.add("hidden");
        }
    }

    if (cbestado.value === "0") {
        erros++;
        msEstado.innerHTML = "O Estado precisa ser selecionado!";
        msEstado.classList.remove("hidden");
    } else {
        if (msEstado.classList.contains("hidden") === false) {
            msEstado.classList.add("hidden");
        }
    }

    if (cbcidade.value === "0") {
        erros++;
        msCidade.innerHTML = "A Cidade precisa ser selecionada!";
        msCidade.classList.remove("hidden");
    } else {
        if (msCidade.classList.contains("hidden") === false) {
            msCidade.classList.add("hidden");
        }
    }

    if (telefone.length === 0) {
        erros++;
        msTelefone.innerHTML = "O Telefone precisa ser preenchido!";
        msTelefone.classList.remove("hidden");
    } else
    if (telefone.length < 14) {
        erros++;
        msTelefone.innerHTML = "O Telefone informado possui tamanho inválido...";
        msTelefone.classList.remove("hidden");
    } else {
        if (msTelefone.classList.contains("hidden") === false) {
            msTelefone.classList.add("hidden");
        }
    }

    if (celular.length === 0) {
        erros++;
        msCelular.innerHTML = "O Celular precisa ser preenchido!";
        msCelular.classList.remove("hidden");
    } else
    if (celular.length < 15) {
        erros++;
        msCelular.innerHTML = "O Celular informado possui tamanho inválido...";
        msCelular.classList.remove("hidden");
    } else {
        if (msCelular.classList.contains("hidden") === false) {
            msCelular.classList.add("hidden");
        }
    }

    if (email.length === 0) {
        erros++;
        msEmail.innerHTML = "O Email precisa ser preenchido!";
        msEmail.classList.remove("hidden");
    } else
    if (validacaoEmail(email) === false) {
        erros++;
        msEmail.innerHTML = "O Email informado é inválido...";
        msEmail.classList.remove("hidden");
    } else {
        if (msEmail.classList.contains("hidden") === false) {
            msEmail.classList.add("hidden");
        }
    }
    
    if (erros === 0) {
        var form = new FormData();
        form.append("razaosocial", razaosocial);
        form.append("nomefantasia", nomefantasia);
        form.append("cnpj", cnpj);
        form.append("rua", rua);
        form.append("numero", numero);
        form.append("bairro", bairro);
        form.append("complemento", complemento);
        form.append("cep", cep);
        form.append("cidade", cidade);
        form.append("telefone", telefone);
        form.append("celular", celular);
        form.append("email", email);

        $.ajax({
            type: 'POST',
            url: '/Representacao/Gravar',
            data: form,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.length > 0) {
                    mostraDialogo(
                        "<strong>Ocorreu um problema ao se comunicar com o servidor...</strong>" +
                        "<br/>"+response,
                        "danger",
                        2000
                    );
                } else {
                    mostraDialogo(
                        "<strong>Cliente cadastrado com sucesso!</strong>" +
                        "<br/>O novo cliente foi salvo com sucesso no sistema...",
                        "success",
                        2000
                    );
                    limparCampos();
                }
            },
            error: function (error) {
                mostraDialogo(
                    "<strong>Ocorreu um problema ao se comunicar com o servidor...</strong>" +
                    "<br/>Um problema no servidor impediu sua comunicação...",
                    "danger",
                    2000
                );
            }
        });
    }
});