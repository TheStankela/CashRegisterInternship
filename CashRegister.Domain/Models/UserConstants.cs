using CashRegister.Domain.Models;

public class UserConstants
{
	public static List<User> Users = new()
			{
					new User(){ Username="admin",Password="admin",Role="Admin"},
					new User(){ Username="user",Password="user",Role="User"}
			};
}
