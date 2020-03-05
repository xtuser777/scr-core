using Newtonsoft.Json.Linq;
using scrweb.DAO;

namespace scrweb.Models
{
    public class Parametrizacao
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
        private Cidade _cidade;
        private string _telefone;
        private string _celular;
        private string _email;
        private string _logotipo;

        public int Id { get => _id; set => _id = value; }

        public string RazaoSocial { get => _razaoSocial; set => _razaoSocial = value; }

        public string NomeFantasia { get => _nomeFantasia; set => _nomeFantasia = value; }

        public string Cnpj { get => _cnpj; set => _cnpj = value; }

        public string Rua { get => _rua; set => _rua = value; }

        public string Numero { get => _numero; set => _numero = value; }

        public string Bairro { get => _bairro; set => _bairro = value; }

        public string Complemento { get => _complemento; set => _complemento = value; }

        public string Cep { get => _cep; set => _cep = value; }

        public Cidade Cidade { get => _cidade; set => _cidade = value; }

        public string Telefone { get => _telefone; set => _telefone = value; }

        public string Celular { get => _celular; set => _celular = value; }

        public string Email { get => _email; set => _email = value; }

        public string Logotipo { get => _logotipo; set => _logotipo = value; }

        public Parametrizacao Get()
        {
            return new ParametrizacaoDAO().Get();
        }

        public int Gravar()
        {
            if (string.IsNullOrEmpty(_razaoSocial) || string.IsNullOrEmpty(_nomeFantasia) ||
                string.IsNullOrEmpty(_cnpj) || string.IsNullOrEmpty(_rua) || string.IsNullOrEmpty(_numero) ||
                string.IsNullOrEmpty(_bairro) || string.IsNullOrEmpty(_cep) || string.IsNullOrEmpty(_telefone) ||
                string.IsNullOrEmpty(_celular) || string.IsNullOrEmpty(_email) || _cidade == null) return -5;
            
            return new ParametrizacaoDAO().Gravar(this);
        }

        public int Alterar()
        {
            if (_id <= 0 || string.IsNullOrEmpty(_razaoSocial) || string.IsNullOrEmpty(_nomeFantasia) ||
                string.IsNullOrEmpty(_cnpj) || string.IsNullOrEmpty(_rua) || string.IsNullOrEmpty(_numero) ||
                string.IsNullOrEmpty(_bairro) || string.IsNullOrEmpty(_cep) || string.IsNullOrEmpty(_telefone) ||
                string.IsNullOrEmpty(_celular) || string.IsNullOrEmpty(_email) || _cidade == null) return -5;

            return new ParametrizacaoDAO().Alterar(this);
        }

        public JObject ToJObject()
        {
            JObject json = new JObject();
            json.Add("id", _id);
            json.Add("razaoSocial", _razaoSocial);
            json.Add("nomeFantasia", _nomeFantasia);
            json.Add("cnpj", _cnpj);
            json.Add("rua", _rua);
            json.Add("numero", _numero);
            json.Add("bairro", _bairro);
            json.Add("complemento", _complemento);
            json.Add("cep", _cep);
            json.Add("telefone", _telefone);
            json.Add("celular", _celular);
            json.Add("email", _email);
            json.Add("logotipo", _logotipo);
            json.Add("cidade", _cidade.ToJObject());

            return json;
        }
    }
}