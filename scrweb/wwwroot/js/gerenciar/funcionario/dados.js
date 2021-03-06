var txnome = document.getElementById("txNome");
var dtNasc = document.getElementById("dtNasc");
var txrg = document.getElementById("txRg");
var txcpf = document.getElementById("txCpf");
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
var txlogin = document.getElementById("txLogin");
var txsenha = document.getElementById("txSenha");
var txconfsenha = document.getElementById("txConfSenha");

var btsalvar = document.getElementById("btSalvar");
var btvoltar = document.getElementById("btVoltar");

var msNome = document.getElementById("msNome");
var msNasc = document.getElementById("msNasc");
var msRg = document.getElementById("msRg");
var msCpf = document.getElementById("msCpf");
var msRua = document.getElementById("msRua");
var msNumero = document.getElementById("msNumero");
var msBairro = document.getElementById("msBairro");
var msCep = document.getElementById("msCep");
var msEstado = document.getElementById("msEstado");
var msCidade = document.getElementById("msCidade");
var msTelefone = document.getElementById("msTelefone");
var msCelular = document.getElementById("msCelular");
var msEmail = document.getElementById("msEmail");
var msLogin = document.getElementById("msLogin");
var msSenha = document.getElementById("msSenha");
var msConfSenha = document.getElementById("msConfSenha");

var auth = document.getElementById("auth");

var idendereco = 0;
var idpessoa = 0;
var idfuncionario = 0;
var idusuario = 0;

var ativo = true;
var lista_estados = [];
var lista_cidades = [];
var cpf_atual = "";
var nivel_atual = "";
var tipo_atual = "";
var login_orig = "";
var erros = 0;

function limparEstados() {
    for (var i = cbestado.childElementCount - 1; i > 0; i--) {
        cbestado.children.item(i).remove();
    }
}

function carregarCidades() {
    $.ajax({
        type: 'POST',
        url: '/Cidade/ObterPorEstado',
        data: { estado: cbestado.value },
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
    $(txcpf).mask('000.000.000-00', {reverse: false});
    $(txcep).mask('00.000-000', {reverse: false});
    $(txtel).mask('(00) 0000-0000', {reverse: false});
    $(txcel).mask('(00) 00000-0000', {reverse: false});

    lista_estados = get('/Estado/Obter');
    limparEstados();
    if (lista_estados !== "") {
        for (var i = 0; i < lista_estados.length; i++) {
            var option = document.createElement("option");
            option.value = lista_estados[i].id;
            option.text = lista_estados[i].nome;
            cbestado.appendChild(option);
        }
    }

    let response = get('/Funcionario/ObterFuncData');

    if (response !== "") {
        idendereco = response.funcionario.pessoa.contato.endereco.id;
        idcontato = response.funcionario.pessoa.contato.id;
        idpessoa = response.funcionario.pessoa.id;
        idfuncionario = response.funcionario.id;
        idusuario = response.id;
        ativo = response.ativo;
        cpf_atual = response.funcionario.pessoa.cpf;

        txnome.value = response.funcionario.pessoa.nome;
        dtNasc.value = FormatarDataIso(response.funcionario.pessoa.nascimento);
        txrg.value = response.funcionario.pessoa.rg;
        txcpf.value = response.funcionario.pessoa.cpf;
        tipo_atual = response.funcionario.tipo;
        txrua.value = response.funcionario.pessoa.contato.endereco.rua;
        txnumero.value = response.funcionario.pessoa.contato.endereco.numero;
        txbairro.value = response.funcionario.pessoa.contato.endereco.bairro;
        txcomplemento.value = response.funcionario.pessoa.contato.endereco.complemento;
        txcep.value = response.funcionario.pessoa.contato.endereco.cep;
        cbestado.value = response.funcionario.pessoa.contato.endereco.cidade.estado.id;
        carregarCidades();
        cbcidade.value = response.funcionario.pessoa.contato.endereco.cidade.id;
        txtel.value = response.funcionario.pessoa.contato.telefone;
        txcel.value = response.funcionario.pessoa.contato.celular;
        txemail.value = response.funcionario.pessoa.contato.email;
        nivel_atual = response.nivel.id;
        txlogin.value = response.login;
        login_orig = response.login;
        txsenha.value = response.senha;
        txconfsenha.value = response.senha;

        if (tipo_atual.value === "2") {
            if (!auth.classList.contains("hidden"))
                auth.classList.add("hidden");
        } else {
            if (auth.classList.contains("hidden"))
                auth.classList.remove("hidden");
        }
    }
});

function limparCampos() {
    txnome.value = "";
    dtNasc.value = "";
    txrg.value = "";
    txcpf.value = "";
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
    txlogin.value = "";
    txsenha.value = "";
    txconfsenha.value = "";
}

btvoltar.addEventListener("click", function (event) {
    limparCampos();
    window.location.href = "../../inicio/index";
});

function verificarLogin(login) {
    $.ajax({
        type: 'POST',
        url: '/Funcionario/VerificaLogin',
        data: { login: login },
        async: false,
        success: function (response) {
            if (response === "true" && login !== login_orig) {
                erros++;
                msLogin.innerHTML = "O Login informado já existe...";
                msLogin.classList.remove("hidden");
            } else {
                if (msLogin.classList.contains("hidden") === false) { msLogin.classList.add("hidden"); }
            }
        },
        error: function () {
            alert("Ocorreu um problema na comunicação com o servidor...");
        }
    });
}

function verificarAdmin() {
    var data = get("/Funcionario/IsLastAdmin");
    return (data === true && nivel_atual === 1);
}

function verificarCpf(cpf) {
    $.ajax({
        type: 'POST',
        url: '/Funcionario/VerificarCpf',
        data: { cpf: cpf },
        async: false,
        success: function (response) {
            if (response === true && cpf !== cpf_atual) {
                erros++;
                msCpf.innerHTML = "O CPF informado já existe no cadastro...";
                msCpf.classList.remove("hidden");
            } else {
                if (msCpf.classList.contains("hidden") === false) { msCpf.classList.add("hidden"); }
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

function validarCpf(cpf) {
    cpf = cpf.replace(/[^\d]+/g, '');
    if (cpf == '') {
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
    var nome = txnome.value;
    var nasc = dtNasc.value;
    var rg = txrg.value;
    var cpf = txcpf.value;
    var tipo = tipo_atual;
    var rua = txrua.value;
    var numero = txnumero.value;
    var bairro = txbairro.value;
    var complemento = txcomplemento.value;
    var cep = txcep.value;
    let cidade = cbcidade.value;
    var telefone = txtel.value;
    var celular = txcel.value;
    var email = txemail.value;
    var nivel = nivel_atual;
    var login = txlogin.value;
    var senha = txsenha.value;
    var confsenha = txconfsenha.value;

    var dataNasc = new Date(nasc);
    erros = 0;

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
        verificarCpf(cpf);
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

    if (tipo !== "2") {
        if (login.length === 0) {
            erros++;
            msLogin.innerHTML = "O Login precisa ser preenchido!";
            msLogin.classList.remove("hidden");
        } else {
            verificarLogin(login);
        }

        if (senha.length === 0) {
            erros++;
            msSenha.innerHTML = "A Senha precisa ser preenchida!";
            msSenha.classList.remove("hidden");
        } else
        if (senha.length < 6) {
            erros++;
            msSenha.innerHTML = "A Senha informada possui tamanho inválido...";
            msSenha.classList.remove("hidden");
        } else {
            if (msSenha.classList.contains("hidden") === false) {
                msSenha.classList.add("hidden");
            }
        }

        if (confsenha.length === 0) {
            erros++;
            msConfSenha.innerHTML = "A Senha de confirmação precisa ser preenchida!";
            msConfSenha.classList.remove("hidden");
        } else
            if (confsenha.length < 6) {
                erros++;
                msConfSenha.innerHTML = "A Senha de confirmação possui tamanho inválido...";
                msConfSenha.classList.remove("hidden");
            } else
                if (confsenha !== senha) {
                    erros++;
                    msConfSenha.innerHTML = "As senhas não conferem!";
                    msConfSenha.classList.remove("hidden");
                } else {
                    if (msConfSenha.classList.contains("hidden") === false) {
                        msConfSenha.classList.add("hidden");
                    }
                }
    }

    if (erros === 0) {
        var form = new FormData();
        form.append("idendereco", idendereco);
        form.append("idcontato", idcontato);
        form.append("idpessoa", idpessoa);
        form.append("idfuncionario", idfuncionario);
        form.append("idusuario", idusuario);
        form.append("ativo", ativo);
        form.append("nome", nome);
        form.append("nasc", nasc);
        form.append("rg", rg);
        form.append("cpf", cpf);
        form.append("tipo", tipo);
        form.append("rua", rua);
        form.append("numero", numero);
        form.append("bairro", bairro);
        form.append("complemento", complemento);
        form.append("cep", cep);
        form.append("cidade", cidade);
        form.append("telefone", telefone);
        form.append("celular", celular);
        form.append("email", email);
        form.append("nivel", nivel);
        form.append("login", login);
        form.append("senha", senha);

        $.ajax({
            type: 'POST',
            url: '/Funcionario/Alterar',
            data: form,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.length > 0) {
                    alert(response);
                } else {
                    cpf_atual = cpf;
                    login_orig = login;
                    mostraDialogo(
                        "<strong>Alteração salva com sucesso!</strong>" +
                        "<br/>As alterações nos campos foram salvas com sucesso...",
                        "success",
                        2000
                    );
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

