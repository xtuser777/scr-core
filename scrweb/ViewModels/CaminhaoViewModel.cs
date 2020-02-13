using System;

namespace scrweb.ViewModels 
{
	public class CaminhaoViewModel
	{
		public int Id { get; set; }
		public string Placa { get; set; }
		public string Marca { get; set; }
		public string Modelo { get; set; }
		public string Ano { get; set; }
		public TipoCaminhaoViewModel Tipo { get; set; }
		public MotoristaViewModel Proprietario { get; set; }
	}
}
