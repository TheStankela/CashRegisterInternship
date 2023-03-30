using CashRegister.API.Mediator.Commands;
using CashRegister.API.Mediator.Commands.BillCommands;
using CashRegister.Application.Services;
using MediatR;

namespace CashRegister.API.Mediator.Handlers.BillHandlers
{
    public class UpdateBillHandler : IRequestHandler<UpdateBillCommand, bool>
    {
        private readonly IBillService _billService;

        public UpdateBillHandler(IBillService billService)
        {
            _billService = billService;
        }
        public async Task<bool> Handle(UpdateBillCommand request, CancellationToken cancellationToken)
        {
            if (!_billService.BillExists(request.BillNumber))
                return false;

            return _billService.UpdateBill(request.BillNumber, request.AddBillDto);
        }
    }
}
