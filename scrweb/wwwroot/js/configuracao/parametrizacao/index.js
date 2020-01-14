var txrazaosocial = document.getElementById("txRazaoSocial");
var txnomefantasia = document.getElementById("txNomeFantasia");
var txcnpj = document.getElementById("txCnpj");
var txrua = document.getElementById("txRua");
var txnumero = document.getElementById("txNumero");
var txbairro = document.getElementById("txBairro");
var txcomplemento = document.getElementById("txComplemento");
var cbestado = document.getElementById("cbestado");
var estados = document.getElementById("estados");
var txidestado = document.getElementById("txIdEstado");
var cbcidade = document.getElementById("cbcidade");
var cidades = document.getElementById("cidades");
var txidcidade = document.getElementById("txIdCidade");
var txcep = document.getElementById("txCep");
var txtel = document.getElementById("txTel");
var txcel = document.getElementById("txCel");
var txemail = document.getElementById("txEmail");
var logotipo = document.getElementById("logotipo");

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

var erros = 0;
var lista_estados = [];
var lista_cidades = [];
var novo = true;

function limparEstados() {
    for (var i = estados.childElementCount - 1; i >= 0; i--) {
        estados.children.item(i).remove();
    }
}

$(cbestado).focusin(function () {
    $(cbestado).select();
});

$(cbestado).change(function (event) {
    cbcidade.value = "";
    txidcidade.value = "0";

    var est = cbestado.value;
    txidestado.value = est.slice(1,est.indexOf(')'));

    var form = new FormData();
    form.append("estado", txidestado.value);

    $.ajax({
        type: 'POST',
        url: '/Funcionario/ObterCidades',
        data: form,
        contentType: false,
        processData: false,
        async: false,
        success: function (response) {lista_cidades = response;},
        error: function (err) {alert("Ocorreu um problema ao se comunicar com o servidor...");}
    });

    limparCidades();
    if (lista_cidades !== "") {
        for (var i = 0; i < lista_cidades.length; i++) {
            var option = document.createElement("option");
            option.value = "("+lista_cidades[i].id+") "+lista_cidades[i].nome;
            cidades.appendChild(option);
        }
    }
});

function limparCidades() {
    for (var i = cidades.childElementCount - 1; i >= 0; i--) {
        cidades.children.item(i).remove();
    }
}

$(cbcidade).focusin(function () {
    $(cbcidade).select();
});

$(cbcidade).focusout(function () {
    var cidade = cbcidade.value;
    txidcidade.value = cidade.slice(1,cidade.indexOf(')'));
});

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

$(document).ready(function () {
    $(txcnpj).mask('00.000.000/0000-00', {reverse: false});
    $(txcep).mask('00.000-000', {reverse: false});
    $(txtel).mask('(00) 0000-0000', {reverse: false});
    $(txcel).mask('(00) 00000-0000', {reverse: false});

    lista_estados = get('/Funcionario/ObterEstados');
    limparEstados();
    if (lista_estados !== "") {
        for (var i = 0; i < lista_estados.length; i++) {
            var option = document.createElement("option");
            option.value = "("+lista_estados[i].id+") "+lista_estados[i].nome;
            estados.appendChild(option);
        }
    }
    
    var response = get("/Parametrizacao/Obter");
    if (response != null && response !== "") {
        txrazaosocial.value = response.razaoSocial;
        txnomefantasia.value = response.nomeFantasia;
        txcnpj.value = response.cnpj;
        txrua.value = response.rua;
        txnumero.value = response.numero;
        txbairro.value = response.bairro;
        txcomplemento.value = response.complemento;
        txcep.value = response.cep;
        cbestado.value = "("+response.cidade.estado.id+") "+response.cidade.estado.nome;
        txidestado.value = response.cidade.estado.id;
        cbcidade.value = "("+response.cidade.id+") "+response.cidade.nome;
        txidcidade.value = response.cidade.id;
        txtel.value = response.telefone;
        txcel.value = response.celular;
        txemail.value = response.email;
        
        novo = false;
    }

    cbcidade.disabled = (txidestado.value === "0");
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
    cbestado.value = "";
    txidestado.value = "0";
    cbcidade.value = "";
    txidcidade.value = "0";
    txtel.value = "";
    txcel.value = "";
    txemail.value = "";
    logotipo.value = "";
}

btvoltar.addEventListener("click", function (event) {
    limparCampos();
    window.location.href = "../../inicio/index";
});

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
        if (msCnpj.classList.contains("hidden") === false) {
            msCnpj.classList.add("hidden");
        }
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

    if (txidestado.value === "0") {
        erros++;
        msEstado.innerHTML = "O Estado precisa ser selecionado!";
        msEstado.classList.remove("hidden");
    } else {
        if (msEstado.classList.contains("hidden") === false) {
            msEstado.classList.add("hidden");
        }
    }

    if (txidcidade.value === "0") {
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
        form.append("cidade", txidcidade.value);
        form.append("telefone", telefone);
        form.append("celular", celular);
        form.append("email", email);
        
        if (logotipo.files.length > 0 && logotipo.files[0].size > 0) {
            form.append("logotipo", logotipo.files[0]);
        }

        if (novo === true) {
            $.ajax({
                type: 'POST',
                url: '/Parametrizacao/Gravar',
                data: form,
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response.length > 0) {
                        alert(response);
                    } else {
                        alert("Parametrização salva com sucesso!");
                    }
                },
                error: function (error) {
                    alert("Ocorreu um problema ao comunicar-se com o servidor");
                }
            });
        } else {
            $.ajax({
                type: 'POST',
                url: '/Parametrizacao/Alterar',
                data: form,
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response.length > 0) {
                        alert(response);
                    } else {
                        alert("Alteração salva com sucesso!");
                    }
                },
                error: function (error) {
                    alert(error);
                }
            });   
        }
    }
});