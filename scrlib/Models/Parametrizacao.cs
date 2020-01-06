using scrlib.DAO;

namespace scrlib.Models
{
    internal class Parametrizacao
    {
        private int _id;
        private string _razaoSocial;
        private string _nomeFantasia;
        private string _cnpj;
        private string _rua;
        private string _numero;
        private string _bairro;
        private string _complemento;
        private string _cep;
        private int _cidade;
        private string _telefone;
        private string _celular;
        private string _email;
        private string _logotipo;

        internal int Id
        {
            get => _id;
            set => _id = value;
        }

        internal string RazaoSocial
        {
            get => _razaoSocial;
            set => _razaoSocial = value;
        }

        internal string NomeFantasia
        {
            get => _nomeFantasia;
            set => _nomeFantasia = value;
        }

        internal string Cnpj
        {
            get => _cnpj;
            set => _cnpj = value;
        }

        internal string Rua
        {
            get => _rua;
            set => _rua = value;
        }

        internal string Numero
        {
            get => _numero;
            set => _numero = value;
        }

        internal string Bairro
        {
            get => _bairro;
            set => _bairro = value;
        }

        internal string Complemento
        {
            get => _complemento;
            set => _complemento = value;
        }

        internal string Cep
        {
            get => _cep;
            set => _cep = value;
        }

        internal int Cidade
        {
            get => _cidade;
            set => _cidade = value;
        }

        internal string Telefone
        {
            get => _telefone;
            set => _telefone = value;
        }

        internal string Celular
        {
            get => _celular;
            set => _celular = value;
        }

        internal string Email
        {
            get => _email;
            set => _email = value;
        }

        internal string Logotipo
        {
            get => _logotipo;
            set => _logotipo = value;
        }

        internal Parametrizacao Get()
        {
            return new ParametrizacaoDAO().Get();
        }

        internal int Gravar()
        {
            if (_id == 0)
            {
                return new ParametrizacaoDAO().Gravar(this);
            }

            return -5;
        }

        internal int Alterar()
        {
            if (_id > 0)
            {
                return new ParametrizacaoDAO().Alterar(this);
            }

            return -5;
        }
    }
}