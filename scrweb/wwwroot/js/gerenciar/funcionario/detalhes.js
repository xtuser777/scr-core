var txnome = document.getElementById("txNome");
var dtNasc = document.getElementById("dtNasc");
var txrg = document.getElementById("txRg");
var txcpf = document.getElementById("txCpf");
var dtadm = document.getElementById("dtAdm");
var cbtipo = document.getElementById("cbTipo");
var txrua = document.getElementById("txRua");
var txnumero = document.getElementById("txNumero");
var txbairro = document.getElementById("txBairro");
var txcomplemento = document.getElementById("txComplemento");
var btestado = document.getElementById("btEstado");
var txidestado = document.getElementById("txIdEstado");
var btcidade = document.getElementById("btCidade");
var txidcidade = document.getElementById("txIdCidade");
var txcep = document.getElementById("txCep");
var txtel = document.getElementById("txTel");
var txcel = document.getElementById("txCel");
var txemail = document.getElementById("txEmail");
var cbnivel = document.getElementById("cbNivel");
var txlogin = document.getElementById("txLogin");
var txsenha = document.getElementById("txSenha");
var txconfsenha = document.getElementById("txConfSenha");

var btlimpar = document.getElementById("btLimpar");
var btsalvar = document.getElementById("btSalvar");
var tbestados = document.getElementById("tbEstados");
var tbestadosbody = document.getElementById("tbEstadosBody");
var btfiltrarestados = document.getElementById("btFiltrarEstado");
var txestadopesquisa = document.getElementById("txSelEstadoPesquisa");
var tbcidades = document.getElementById("tbCidades");
var tbcidadesbody = document.getElementById("tbCidadesBody");
var btfiltrarcidades = document.getElementById("btFiltrarCidade");
var txfiltrocidades = document.getElementById("txSelCidadePesquisa");
var btconfirmarestado = document.getElementById("btConfirmarEstado");
var btconfirmarcidade = document.getElementById("btConfirmarCidade");
var btvoltar = document.getElementById("btVoltar");

var msNome = document.getElementById("msNome");
var msNasc = document.getElementById("msNasc");
var msRg = document.getElementById("msRg");
var msCpf = document.getElementById("msCpf");
var msAdm = document.getElementById("msAdm");
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
var msNivel = document.getElementById("msNivel");
var msLogin = document.getElementById("msLogin");
var msSenha = document.getElementById("msSenha");
var msConfSenha = document.getElementById("msConfSenha");

var auth = document.getElementById("auth");

var idendereco = 0;
var idpessoa = 0;
var idfuncionario = 0;
var idusuario = 0;

var login_orig = "";
var estados = null;
var cidades = null;
var erros = 0;

$(document).ready(function () {
    $.getJSON("/Funcionario/ObterNiveis", function (data) {
        if (data != null && data != "") {
            for (var i = 0; i < data.length; i++) {
                var option = document.createElement("option");
                option.value = data[i].id;
                option.text = data[i].descricao;
                cbnivel.appendChild(option);
            }
        }
    });
});

function carregarTabelaEstados(dados) {
    limparTabelaEstados();
    for (var i = 0; i < dados.length; i++) {
        var row = document.createElement("tr");

        var cell0 = document.createElement("td");
        var cellText0 = document.createTextNode(dados[i].id);
        cell0.appendChild(cellText0);
        cell0.classList.add("hidden");
        row.appendChild(cell0);

        var cell1 = document.createElement("td");
        var cellText1 = document.createTextNode(dados[i].nome);
        cell1.appendChild(cellText1);
        row.appendChild(cell1);

        var cell2 = document.createElement("td");
        var cellText2 = document.createTextNode(dados[i].sigla);
        cell2.appendChild(cellText2);
        row.appendChild(cell2);

        tbestadosbody.appendChild(row);
    }

    var itensTabela = tbestadosbody.getElementsByTagName("tr");

    for (var i = 0; i < itensTabela.length; i++) {
        var item = itensTabela[i];
        item.addEventListener("click", function (event) {
            selecionarItemEstado(this);
        });
    }
}

function selecionarItemEstado(item) {
    var itens = tbestadosbody.getElementsByTagName("tr");
    for (var i = 0; i < itens.length; i++) {
        var item_ = itens[i];
        item_.classList.remove("selecionado");
    }
    item.classList.toggle("selecionado");
}

function limparTabelaEstados() {
    for (var i = tbestadosbody.childElementCount - 1; i >= 0; i--) {
        tbestadosbody.children.item(i).remove();
    }
}

btestado.addEventListener("click", function (event) {
    $.getJSON("/Funcionario/ObterEstados", function (data) {
        if (data != null && data.length > 0) {
            estados = data;
            carregarTabelaEstados(data);
        }
    });
});

btfiltrarestados.addEventListener("click", function (event) {
    var filtro = txestadopesquisa.value;
    var filtrado = [];
    for (var i = 0; i < estados.length; i++) {
        var nome = estados[i].nome;
        var sigla = estados[i].sigla;
        if (nome.includes(filtro) || sigla.includes(filtro)) {
            filtrado.push(estados[i]);
        }
    }
    limparTabelaEstados();
    carregarTabelaEstados(filtrado);
});

btconfirmarestado.addEventListener("click", function (event) {
    var selecionados = tbestados.getElementsByClassName("selecionado");
    var selecionado = selecionados[0];
    if (selecionado != null && selecionado != "") {
        selecionado = selecionado.getElementsByTagName("td");
        txidestado.value = selecionado[0].innerHTML;
        btestado.innerHTML = selecionado[1].innerText;
        $.fancybox.close();
    } else {
        alert("selecione pelo menos um estado!");
    }
});

function carregarTabelaCidades(dados) {
    limparTabelaCidades();
    for (var i = 0; i < dados.length; i++) {
        var row = document.createElement("tr");

        var cell0 = document.createElement("td");
        var cellText0 = document.createTextNode(dados[i].id);
        cell0.appendChild(cellText0);
        cell0.classList.add("hidden");
        row.appendChild(cell0);

        var cell1 = document.createElement("td");
        var cellText1 = document.createTextNode(dados[i].nome);
        cell1.appendChild(cellText1);
        row.appendChild(cell1);

        tbcidadesbody.appendChild(row);
    }

    var itensTabela = tbcidadesbody.getElementsByTagName("tr");

    for (var i = 0; i < itensTabela.length; i++) {
        var item = itensTabela[i];
        item.addEventListener("click", function (event) {
            selecionarItemCidade(this);
        });
    }
}

function selecionarItemCidade(item) {
    var itens = tbcidadesbody.getElementsByTagName("tr");
    for (var i = 0; i < itens.length; i++) {
        var item_ = itens[i];
        item_.classList.remove("selecionado");
    }
    item.classList.toggle("selecionado");
}

function limparTabelaCidades() {
    for (var i = tbcidadesbody.childElementCount - 1; i >= 0; i--) {
        tbcidadesbody.children.item(i).remove();
    }
}

btcidade.addEventListener("click", function (event) {
    if (txidestado.value != "0") {
        $.ajax({
            type: 'POST',
            url: '/Funcionario/ObterCidadesPorEstado',
            data: { estado: txidestado.value },
            success: function (result) {
                if (result != null && result.length > 0) {
                    cidades = result;
                    carregarTabelaCidades(result);
                }
            },
            error: function () {
                alert("Houve um erro na comunicação com o servidor...");
            }
        });
    } else {
        alert("Selecione um estado primeiro!");
        $.fancybox.close();
    }
});

btfiltrarcidades.addEventListener("click", function (event) {
    var filtro = txfiltrocidades.value;
    var filtrado = [];
    for (var i = 0; i < cidades.length; i++) {
        var nome = cidades[i].nome;
        if (nome.includes(filtro)) {
            filtrado.push(cidades[i]);
        }
    }
    limparTabelaCidades();
    carregarTabelaCidades(filtrado);
});

btconfirmarcidade.addEventListener("click", function (event) {
    var selecionados = tbcidades.getElementsByClassName("selecionado");
    var selecionado = selecionados[0];
    if (selecionado != null && selecionado != "") {
        selecionado = selecionado.getElementsByTagName("td");
        txidcidade.value = selecionado[0].innerHTML;
        btcidade.innerHTML = selecionado[1].innerText;
        $.fancybox.close();
    } else {
        alert("Selecione pelo menos uma Cidade!");
    }
});

function limparCampos() {
    txnome.value = "";
    dtNasc.value = "";
    txrg.value = "";
    txcpf.value = "";
    cbtipo.value = "0";
    dtadm.value = "";
    txrua.value = "";
    txnumero.value = "";
    txbairro.value = "";
    txcomplemento.value = "";
    txcep.value = "";
    btestado.innerHTML = "SELECIONAR";
    txidestado.value = "0";
    btcidade.innerHTML = "SELECIONAR";
    txidcidade.value = "0";
    txtel.value = "";
    txcel.value = "";
    txemail.value = "";
    cbnivel.value = "0";
    txlogin.value = "";
    txsenha.value = "";
    txconfsenha.value = "";
}

btlimpar.addEventListener("click", function (event) {
    limparCampos();
});

btvoltar.addEventListener("click", function (event) {
    limparCampos();
    window.location.href = "../../gerenciar/funcionario/index";
});

cbtipo.addEventListener("change", function (event) {
    if (cbtipo.value == "2") {
        if (!auth.classList.contains("hidden"))
            auth.classList.add("hidden");
    } else {
        if (auth.classList.contains("hidden"))
            auth.classList.remove("hidden");
    }
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
                if (msLogin.classList.contains("hidden") == false) { msLogin.classList.add("hidden"); }
            }
        },
        error: function () {
            alert("Ocorreu um problema na comunicação com o servidor...");
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

btsalvar.addEventListener("click", function (event) {
    var nome = txnome.value;
    var nasc = dtNasc.value;
    var rg = txrg.value;
    var cpf = txcpf.value;
    var adm = dtadm.value;
    var tipo = cbtipo.value;
    var rua = txrua.value;
    var numero = txnumero.value;
    var bairro = txbairro.value;
    var complemento = txcomplemento.value;
    var cep = txcep.value;
    var telefone = txtel.value;
    var celular = txcel.value;
    var email = txemail.value;
    var nivel = cbnivel.value;
    var login = txlogin.value;
    var senha = txsenha.value;
    var confsenha = txconfsenha.value;

    var dataNasc = new Date(nasc);
    var dataAdm = new Date(adm);
    erros = 0;

    if (nome.length == 0) {
        erros++;
        msNome.innerHTML = "O Nome precisa ser preenchido!";
        msNome.classList.remove("hidden");
    } else
        if (nome.length < 3) {
            erros++;
            msNome.innerHTML = "O Nome informado é inválido...";
            msNome.classList.remove("hidden");
        } else {
            if (msNome.classList.contains("hidden") == false) {
                msNome.classList.add("hidden");
            }
        }

    if (nasc.length == 0) {
        erros++;
        msNasc.innerHTML = "A data de admissão precisa ser preenchida!";
        msNasc.classList.remove("hidden");
    } else
        if (dataNasc >= Date.now()) {
            erros++;
            msNasc.innerHTML = "A data de admissão informada é inválida...";
            msNasc.classList.remove("hidden");
        } else {
            if (msNasc.classList.contains("hidden") == false) {
                msNasc.classList.add("hidden");
            }
        }

    if (rg.length == 0) {
        erros++;
        msRg.innerHTML = "O RG precisa ser preenchido!";
        msRg.classList.remove("hidden");
    } else {
        if (msRg.classList.contains("hidden") == false) {
            msRg.classList.add("hidden");
        }
    }

    if (cpf.length == 0) {
        erros++;
        msCpf.innerHTML = "O CPF precisa ser preenchido!";
        msCpf.classList.remove("hidden");
    } else
        if (!validarCpf(cpf)) {
            erros++;
            msCpf.innerHTML = "O CPF informado é inválido...";
            msCpf.classList.remove("hidden");
        } else {
            if (msCpf.classList.contains("hidden") == false) {
                msCpf.classList.add("hidden");
            }
        }

    if (adm.length == 0) {
        erros++;
        msAdm.innerHTML = "A data de admissão precisa ser preenchida!";
        msAdm.classList.remove("hidden");
    } else
        if (dataAdm > Date.now()) {
            erros++;
            msAdm.innerHTML = "A data de admissão informada é inválida...";
            msAdm.classList.remove("hidden");
        } else {
            if (msAdm.classList.contains("hidden") == false) {
                msAdm.classList.add("hidden");
            }
        }

    if (tipo == "0") {
        erros++;
        msTipo.innerHTML = "O Tipo de do funcionário precisa der preenchido!";
        msTipo.classList.remove("hidden");
    } else {
        if (msTipo.classList.contains("hidden") == false) {
            msTipo.classList.add("hidden");
        }
    }

    if (rua.length == 0) {
        erros++;
        msRua.innerHTML = "A Rua precisa ser preenchida!";
        msRua.classList.remove("hidden");
    } else {
        if (msRua.classList.contains("hidden") == false) {
            msRua.classList.add("hidden");
        }
    }

    if (numero.length == 0) {
        erros++;
        msNumero.innerHTML = "O Número precisa ser preenchido!";
        msNumero.classList.remove("hidden");
    } else {
        if (msNumero.classList.contains("hidden") == false) {
            msNumero.classList.add("hidden");
        }
    }

    if (bairro.length == 0) {
        erros++;
        msBairro.innerHTML = "O Bairro precisa ser preenchido!";
        msBairro.classList.remove("hidden");
    } else {
        if (msBairro.classList.contains("hidden") == false) {
            msBairro.classList.add("hidden");
        }
    }

    if (cep.length == 0) {
        erros++;
        msCep.innerHTML = "O CEP precisa ser preenchido!";
        msCep.classList.remove("hidden");
    } else
        if (cep.length < 10) {
            erros++;
            msCep.innerHTML = "O CEP informado é inválido...";
            msCep.classList.remove("hidden");
        } else {
            if (msCep.classList.contains("hidden") == false) {
                msCep.classList.add("hidden");
            }
        }

    if (txidestado.value == "0") {
        erros++;
        msEstado.innerHTML = "O Estado precisa ser selecionado!";
        msEstado.classList.remove("hidden");
    } else {
        if (msEstado.classList.contains("hidden") == false) {
            msEstado.classList.add("hidden");
        }
    }

    if (txidcidade.value == "0") {
        erros++;
        msCidade.innerHTML = "A Cidade precisa ser selecionada!";
        msCidade.classList.remove("hidden");
    } else {
        if (msCidade.classList.contains("hidden") == false) {
            msCidade.classList.add("hidden");
        }
    }

    if (telefone.length == 0) {
        erros++;
        msTelefone.innerHTML = "O Telefone precisa ser preenchido!";
        msTelefone.classList.remove("hidden");
    } else
        if (telefone.length < 14) {
            erros++;
            msTelefone.innerHTML = "O Telefone informado possui tamanho inválido...";
            msTelefone.classList.remove("hidden");
        } else {
            if (msTelefone.classList.contains("hidden") == false) {
                msTelefone.classList.add("hidden");
            }
        }

    if (celular.length == 0) {
        erros++;
        msCelular.innerHTML = "O Celular precisa ser preenchido!";
        msCelular.classList.remove("hidden");
    } else
        if (celular.length < 15) {
            erros++;
            msCelular.innerHTML = "O Celular informado possui tamanho inválido...";
            msCelular.classList.remove("hidden");
        } else {
            if (msCelular.classList.contains("hidden") == false) {
                msCelular.classList.add("hidden");
            }
        }

    if (email.length == 0) {
        erros++;
        msEmail.innerHTML = "O Email precisa ser preenchido!";
        msEmail.classList.remove("hidden");
    } else
        if (email.includes("@") == false) {
            erros++;
            msEmail.innerHTML = "O Email informado é inválido...";
            msEmail.classList.remove("hidden");
        } else {
            if (msEmail.classList.contains("hidden") == false) {
                msEmail.classList.add("hidden");
            }
        }

    if (tipo != "2") {
        if (nivel == "0") {
            erros++;
            msNivel.innerHTML = "O Nível de acesso precisa ser selecionado!";
            msNivel.classList.remove("hidden");
        } else {
            if (msNivel.classList.contains("hidden") == false) {
                msNivel.classList.add("hidden");
            }
        }

        if (login.length == 0) {
            erros++;
            msLogin.innerHTML = "O Login precisa ser preenchido!";
            msLogin.classList.remove("hidden");
        } else {
            verificarLogin(login);
        }

        if (senha.length == 0) {
            erros++;
            msSenha.innerHTML = "A Senha precisa ser preenchida!";
            msSenha.classList.remove("hidden");
        } else
            if (senha.length < 6) {
                erros++;
                msSenha.innerHTML = "A Senha informada possui tamanho inválido...";
                msSenha.classList.remove("hidden");
            } else {
                if (msSenha.classList.contains("hidden") == false) {
                    msSenha.classList.add("hidden");
                }
            }

        if (confsenha.length == 0) {
            erros++;
            msConfSenha.innerHTML = "A Senha de confirmação precisa ser preenchida!";
            msConfSenha.classList.remove("hidden");
        } else
            if (confsenha.length < 6) {
                erros++;
                msConfSenha.innerHTML = "A Senha de confirmação possui tamanho inválido...";
                msConfSenha.classList.remove("hidden");
            } else
                if (confsenha != senha) {
                    erros++;
                    msConfSenha.innerHTML = "As senhas não conferem!";
                    msConfSenha.classList.remove("hidden");
                } else {
                    if (msConfSenha.classList.contains("hidden") == false) {
                        msConfSenha.classList.add("hidden");
                    }
                }
    }

    if (erros == 0) {
        var form = new FormData();
        form.append("idendereco", idendereco);
        form.append("idpessoa", idpessoa);
        form.append("idfuncionario", idfuncionario);
        form.append("idusuario", idusuario);
        form.append("nome", nome);
        form.append("nasc", nasc);
        form.append("rg", rg);
        form.append("cpf", cpf);
        form.append("adm", adm);
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
                    alert("Alteração salva com sucesso!");
                }
            },
            error: function (error) {
                alert(error);
            }
        });
    }
});