using Newtonsoft.Json.Linq;
using scrweb.DAO;

namespace scrweb.Models
{
    public class Contato
    {
        private int _id;
        private string _telefone;
        private string _celular;
        private string _email;
        private Endereco _endereco;

        public int Id { get => _id; set => _id = value; }
        public string Telefone { get => _telefone; set => _telefone = value; }
        public string Celular { get => _celular; set => _celular = value; }
        public string Email { get => _email; set => _email = value; }
        public Endereco Endereco { get => _endereco; set => _endereco = value; }

        public Contato GetById(int id)
        {
            return id > 0 ? new ContatoDAO().GetById(id) : null;
        }

        public int Gravar()
        {
            if (_id != 0 || string.IsNullOrEmpty(_telefone) || string.IsNullOrEmpty(_celular) || 
                string.IsNullOrEmpty(_email) || _endereco == null
            ) return -5;
            
            return new ContatoDAO().Gravar(this);
        }

        public int Alterar()
        {
            if (_id <= 0 || string.IsNullOrEmpty(_telefone) || string.IsNullOrEmpty(_celular) || 
                string.IsNullOrEmpty(_email) || _endereco == null
            ) return -5;
            
            return new ContatoDAO().Alterar(this);
        }

        public int Excluir(int id)
        {
            return id > 0 ? new ContatoDAO().Excluir(id) : -5;
        }

        public JObject ToJObject()
        {
            JObject json = new JObject();
            json.Add("id", _id);
            json.Add("telefone", _telefone);
            json.Add("celular", _celular);
            json.Add("email", _email);
            json.Add("endereco", _endereco.ToJObject());

            return json;
        }
    }
}