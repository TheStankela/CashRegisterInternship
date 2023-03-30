using CashRegister.API.Dto;
using MediatR;

namespace CashRegister.API.Mediator.Commands.BillCommands
{
    public class UpdateBillCommand : IRequest<bool>
    {
        public AddBillDto AddBillDto { get; set; }
        public string BillNumber { get; set; }
        public UpdateBillCommand(AddBillDto billDto, string billNumber)
        {
            AddBillDto = billDto;
            BillNumber = billNumber;
        }
    }
}
