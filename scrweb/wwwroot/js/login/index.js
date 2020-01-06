var txtLogin = document.getElementById("login");
var txtSenha  = document.getElementById("senha");
var btnEntrar = document.getElementById("btnEntrar");
var form = document.getElementById("formValidar");
var msgLogin1 = document.getElementById("msgLogin");
var msgSenha1 = document.getElementById("msgSenha");
var msgAutenticacao = document.getElementById("msgAutenticacao");

function validarAcesso() {
    var msgLogin = "";
    var msgSenha = "";

    var txLogin = $(txtLogin).val();
    var txSenha = $(txtSenha).val();

    if(txLogin == null) msgLogin += "O Login precisa ser preenchido!";
    if(txLogin != null && txLogin === "") msgLogin += "O Login precisa ser preenchido!";
    if(txSenha == null) msgSenha += "A senha precisa ser preenchida!";
    if(txSenha != null && txSenha === "") msgSenha += "A senha precisa ser preenchida!";

    if(msgLogin.length > 0)
    {
        msgLogin1.classList.remove("hidden");
        msgLogin1.innerHTML = msgLogin;
    }
    else
    {
        if(!msgLogin1.classList.contains("hidden"))
        {
            msgLogin1.classList.add("hidden");
        }
    }

    if(msgSenha.length > 0)
    {
        msgSenha1.classList.remove("hidden");
        msgSenha1.innerHTML = msgSenha;
    }
    else
    {
        if(!msgSenha1.classList.contains("hidden"))
        {
            msgSenha1.classList.add("hidden");
        }
    }

    if(msgLogin.length === 0 && msgSenha.length === 0)
    {
        $.ajax({
            type: 'POST',
            url: '/login/autenticar',
            data: { login : txLogin, senha : txSenha },
            success: function (result)
            {
                if(result != null && result !== "")
                {
                    if(result.login === "first")
                    {
                        window.location.href = "../../configuracao/wizard/parametrizacao";
                    }
                    else
                    {
                        window.location.href = "../../inicio/index";
                    }
                }
                else
                {
                    msgAutenticacao.innerHTML = "Usuário ou senha inválidos...";
                    msgAutenticacao.classList.remove("hidden");
                }
            },
            error: function ()
            {
                alert("Houve um problema no processamento desta requisição...\nSe o problema persistir, entre em contato com o suporte.");
            }
        });
    }
}

btnEntrar.addEventListener("click", function (event) {
    validarAcesso();
});

txtSenha.addEventListener("keypress", function (event) {
    if (event.which === 13) {
        validarAcesso();
    }
});