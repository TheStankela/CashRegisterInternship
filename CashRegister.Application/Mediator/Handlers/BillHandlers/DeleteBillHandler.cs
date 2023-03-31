using CashRegister.API.Mediator.Commands;
using CashRegister.API.Mediator.Commands.BillCommands;
using CashRegister.Application.Services;
using MediatR;

namespace CashRegister.API.Mediator.Handlers.BillHandlers
{
    public class DeleteBillHandler : IRequestHandler<DeleteBillCommand, bool>
    {
        private readonly IBillService _billService;

        public DeleteBillHandler(IBillService billService)
        {
            _billService = billService;
        }
        public async Task<bool> Handle(DeleteBillCommand request, CancellationToken cancellationToken)
        {
            if (!_billService.BillExists(request.BillNumber))
                return false;

            var billToDelete = await _billService.GetBillByBillNumberAsNoTracking(request.BillNumber);

            return _billService.DeleteBill(billToDelete);
        }
    }
}
