using CashRegister.API.Mediator.Querries.BillQuerries;
using CashRegister.Application.Dto;
using CashRegister.Application.Services;
using MediatR;

namespace CashRegister.API.Mediator.Handlers.BillHandlers
{
    public class GetBillByNumberHandler : IRequestHandler<GetBillByNumberQuerry, DisplayBillDto>
    {
        private readonly IBillService _billService;
        public GetBillByNumberHandler(IBillService billService)
        {
            _billService = billService;
        }
        public async Task<DisplayBillDto> Handle(GetBillByNumberQuerry request, CancellationToken cancellationToken)
        {
            return await _billService.GetBillByBillNumberAsync(request.BillNumber);
        }
    }
}
