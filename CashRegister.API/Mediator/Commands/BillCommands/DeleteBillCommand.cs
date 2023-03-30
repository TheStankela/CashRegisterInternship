using MediatR;

namespace CashRegister.API.Mediator.Commands.BillCommands
{
    public class DeleteBillCommand : IRequest<bool>
    {
        public string BillNumber { get; set; }
        public DeleteBillCommand(string billNumber)
        {
            BillNumber = billNumber;
        }
    }
}
