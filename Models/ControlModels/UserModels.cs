namespace MuvekkilTakipSistemi.Models.ControlModels
{
	public class UserAndContact
	{
		public User Users { get; set; }
		public UserContact UserContact { get; set; }
		public List<UserAndContact> UserAndContacts { get; set; }//Birleştirme aşaması için sınıfın kendisi bir liste içine alındı
	}
}
