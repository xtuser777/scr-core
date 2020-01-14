var cbtipo = document.getElementById("cbtipo");
var txnome = document.getElementById("txNome");
var dtNasc = document.getElementById("dtNasc");
var txrg = document.getElementById("txRg");
var txcpf = document.getElementById("txCpf");  
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

var btlimpar = document.getElementById("btLimpar");
var btsalvar = document.getElementById("btSalvar");
var btvoltar = document.getElementById("btVoltar");

var msNome = document.getElementById("msNome");
var msNasc = document.getElementById("msNasc");
var msRg = document.getElementById("msRg");
var msCpf = document.getElementById("msCpf");
var msRazaoSocial = document.getElementById("msRazaoSocial");
var msNomeFantasia = document.getElementById("msNomeFantasia");
var msCnpj = document.getElementById("msCnpj");
var msTipo = document.getElementById("msTipo");
var msRua = document.getElementById("msRua");
var msNumero = document.getElementById("msNumero");
var msBairro = document.getElementById("msBairro");
var msCep = document.getElementById("msCep");
var msEstado = document.getElementById("msEstado");
var msCidade = document.getElementById("msCidade");
var msTelefone = document.getElementById("msTelefone");
var msCelular = document.getElementById("msCelular");
var msEmail = document.getElementById("msEmail");

var fisica = document.getElementById("fisica");
var juridica = document.getElementById("juridica");

var lista_estados = [];
var lista_cidades = [];
var erros = 0;

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
        url: '/Cliente/ObterCidades',
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
    $(txcpf).mask('000.000.000-00', {reverse: false});
    $(txcnpj).mask('00.000.000/0000-00', {reverse: false});
    $(txcep).mask('00.000-000', {reverse: false});
    $(txtel).mask('(00) 0000-0000', {reverse: false});
    $(txcel).mask('(00) 00000-0000', {reverse: false});

    lista_estados = get('/Cliente/ObterEstados');
    limparEstados();
    if (lista_estados !== "") {
        for (var i = 0; i < lista_estados.length; i++) {
            var option = document.createElement("option");
            option.value = "("+lista_estados[i].id+") "+lista_estados[i].nome;
            estados.appendChild(option);
        }
    }

    if (cbtipo.value === "1") {
        if (!juridica.classList.contains("hidden"))
            juridica.classList.add("hidden");
        if (fisica.classList.contains("hidden"))
            fisica.classList.remove("hidden");
    } else {
        if (juridica.classList.contains("hidden"))
            juridica.classList.remove("hidden");
        if (!fisica.classList.contains("hidden"))
            fisica.classList.add("hidden");
    }
});

function limparCampos() {
    txnome.value = "";
    dtNasc.value = "";
    txrg.value = "";
    txcpf.value = "";
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
}

btlimpar.addEventListener("click", function (event) {
    limparCampos();
});

btvoltar.addEventListener("click", function (event) {
    limparCampos();
    window.location.href = "../../gerenciar/cliente/index";
});

cbtipo.addEventListener("change", function (event) {
    if (cbtipo.value === "1") {
        if (!juridica.classList.contains("hidden"))
            juridica.classList.add("hidden");
        if (fisica.classList.contains("hidden"))
            fisica.classList.remove("hidden");
    } else {
        if (juridica.classList.contains("hidden"))
            juridica.classList.remove("hidden");
        if (!fisica.classList.contains("hidden"))
            fisica.classList.add("hidden");
    }
});

function validarCpf(cpf) {
    cpf = cpf.replace(/[^\d]+/g, '');
    if (cpf === '') {
        return false;
    }
    // Elimina CPFs invalidos conhecidos	
    if (cpf.length != 11 || cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" || cpf == "33333333333" || cpf == "44444444444" || cpf == "55555555555" || cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888" || cpf == "99999999999") {
        return false;
    }
    // Valida 1o digito	
    add = 0;
    for (i = 0; i < 9; i++) {
        add += parseInt(cpf.charAt(i)) * (10 - i);
    }
    rev = 11 - (add % 11);
    if (rev == 10 || rev == 11) {
        rev = 0;
    }
    if (rev != parseInt(cpf.charAt(9))) {
        return false;
    }
    // Valida 2o digito	
    add = 0;
    for (i = 0; i < 10; i++) {
        add += parseInt(cpf.charAt(i)) * (11 - i);
    }
    rev = 11 - (add % 11);
    if (rev == 10 || rev == 11) {
        rev = 0;
    }
    if (rev != parseInt(cpf.charAt(10))) {
        return false;
    }
    return true;
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
    if (cbtipo.value === "1") {
        var nome = txnome.value;
        var nasc = dtNasc.value;
        var rg = txrg.value;
        var cpf = txcpf.value;
    } else {
        var razaosocial = txrazaosocial.value;
        var nomefantasia = txnomefantasia.value;
        var cnpj = txcnpj.value;  
    }
    var tipo = cbtipo.value;
    var rua = txrua.value;
    var numero = txnumero.value;
    var bairro = txbairro.value;
    var complemento = txcomplemento.value;
    var cep = txcep.value;
    var telefone = txtel.value;
    var celular = txcel.value;
    var email = txemail.value;

    var dataNasc = new Date(nasc);
    erros = 0;

    if (cbtipo.value === "1") {
        if (nome.length === 0) {
            erros++;
            msNome.innerHTML = "O Nome precisa ser preenchido!";
            msNome.classList.remove("hidden");
        } else
        if (nome.length < 3) {
            erros++;
            msNome.innerHTML = "O Nome informado é inválido...";
            msNome.classList.remove("hidden");
        } else {
            if (msNome.classList.contains("hidden") === false) {
                msNome.classList.add("hidden");
            }
        }

        if (nasc.length === 0) {
            erros++;
            msNasc.innerHTML = "A data de admissão precisa ser preenchida!";
            msNasc.classList.remove("hidden");
        } else
        if (dataNasc >= Date.now()) {
            erros++;
            msNasc.innerHTML = "A data de admissão informada é inválida...";
            msNasc.classList.remove("hidden");
        } else {
            if (msNasc.classList.contains("hidden") === false) {
                msNasc.classList.add("hidden");
            }
        }

        if (rg.length === 0) {
            erros++;
            msRg.innerHTML = "O RG precisa ser preenchido!";
            msRg.classList.remove("hidden");
        } else {
            if (msRg.classList.contains("hidden") === false) {
                msRg.classList.add("hidden");
            }
        }

        if (cpf.length === 0) {
            erros++;
            msCpf.innerHTML = "O CPF precisa ser preenchido!";
            msCpf.classList.remove("hidden");
        } else
        if (!validarCpf(cpf)) {
            erros++;
            msCpf.innerHTML = "O CPF informado é inválido...";
            msCpf.classList.remove("hidden");
        } else {
            if (msCpf.classList.contains("hidden") === false) {
                msCpf.classList.add("hidden");
            }
        }   
    } else {
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
    }

    if (tipo === "0") {
        erros++;
        msTipo.innerHTML = "O Tipo de do funcionário precisa der preenchido!";
        msTipo.classList.remove("hidden");
    } else {
        if (msTipo.classList.contains("hidden") === false) {
            msTipo.classList.add("hidden");
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
        if (cbtipo.value === "1") {
            form.append("nome", nome);
            form.append("nasc", nasc);
            form.append("rg", rg);
            form.append("cpf", cpf);    
        } else {
            form.append("razaosocial", razaosocial);
            form.append("nomefantasia", nomefantasia);
            form.append("cnpj", cnpj);   
        }
        form.append("tipo", tipo);
        form.append("rua", rua);
        form.append("numero", numero);
        form.append("bairro", bairro);
        form.append("complemento", complemento);
        form.append("cep", cep);
        form.append("cidade", txidcidade.value);
        form.append("telefone", telefone);
        form.append("celular", celular);
        form.append("email", email);

        $.ajax({
            type: 'POST',
            url: '/Cliente/Gravar',
            data: form,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.length > 0) {
                    alert(response);
                } else {
                    limparCampos();
                }
            },
            error: function (error) {
                alert('Ocorreu um problema ao comunicar-se com o servidor...');
            }
        });
    }
});