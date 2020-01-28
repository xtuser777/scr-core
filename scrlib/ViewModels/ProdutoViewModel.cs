namespace scrlib.ViewModels
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Medida { get; set; }
        public decimal Preco { get; set; }
        public decimal PrecoOut { get; set; }
        public RepresentacaoViewModel Representacao { get; set; }
    }
}