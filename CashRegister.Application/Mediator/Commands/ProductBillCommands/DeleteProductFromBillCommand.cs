using MediatR;

namespace CashRegister.API.Mediator.Commands.ProductBillCommands
{
	public class DeleteProductFromBillCommand : IRequest<bool>
	{
		public string BillNumber { get; set; }
		public int ProductId { get; set; }
		public DeleteProductFromBillCommand(string billNumber, int productId)
		{
			BillNumber = billNumber;
			ProductId = productId;
		}

		
	}
}
