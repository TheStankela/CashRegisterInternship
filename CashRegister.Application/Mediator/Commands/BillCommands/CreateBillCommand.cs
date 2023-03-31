using CashRegister.API.Dto;
using MediatR;

namespace CashRegister.API.Mediator.Commands.BillCommands
{
    public class CreateBillCommand : IRequest<bool>
    {
        public AddBillDto BillDto { get; set; }
        public CreateBillCommand(AddBillDto addBillDto)
        {
            BillDto = addBillDto;
        }
    }
}
