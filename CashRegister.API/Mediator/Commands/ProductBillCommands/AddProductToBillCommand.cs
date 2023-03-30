using CashRegister.API.Dto;
using MediatR;

namespace CashRegister.API.Mediator.Commands.ProductBillCommands
{
	public class AddProductToBillCommand : IRequest<bool>
	{
		public string BillNumber { get; set; }
		public int ProductId { get; set; }
		public ProductBillDto ProductBillDto { get; set; }
		public AddProductToBillCommand(int productId, string billNumber, ProductBillDto productBillDto)
		{
			ProductId = productId;
			BillNumber = billNumber;
			ProductBillDto = productBillDto;
		}
	}
}
