namespace MuvekkilTakipSistemi.Models.ControlModels
{
	public class UserModels
	{

       
    }

	public class UserAndContact
	{
		public User Users { get; set; }
		public UserContact UserContact { get; set; }
		public List<UserAndContact> UserAndContacts { get; set; }
	}
}
