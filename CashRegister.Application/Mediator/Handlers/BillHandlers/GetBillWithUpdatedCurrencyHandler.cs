using CashRegister.API.Mediator.Querries.BillQuerries;
using CashRegister.Application.Dto;
using CashRegister.Application.Services;
using MediatR;

namespace CashRegister.API.Mediator.Handlers.BillHandlers
{
    public class GetBillWithUpdatedCurrencyHandler : IRequestHandler<GetBillWithUpdatedCurrencyQuerry, DisplayBillDto>
    {
        private readonly IBillService _billService;

        public GetBillWithUpdatedCurrencyHandler(IBillService billService)
        {
            _billService = billService;
        }

        public async Task<DisplayBillDto> Handle(GetBillWithUpdatedCurrencyQuerry request, CancellationToken cancellationToken)
        {
            if (!_billService.BillExists(request.BillNumber))
                return null;

            var billToGet = await _billService.GetBillByBillNumberAsync(request.BillNumber);

            return await _billService.DisplayBill(billToGet, request.Currency);
        }
    }
}
