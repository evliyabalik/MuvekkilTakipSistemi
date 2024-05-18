namespace MuvekkilTakipSistemi.Models.ControlModels
{
	public class UsersModel
	{
		public List<Mahkemeler> Mahkemelers { get; set; }
		public List<OdemeSekli> OdemeSeklis { get; set; }
		public List<Islem_Turu> Islem_Turus { get; set; }
		public List<ClientInfo> ClientInfos { get; set; }
		public List<ClientGroupName>? clientGroupNames { get; set; }
	}
}
