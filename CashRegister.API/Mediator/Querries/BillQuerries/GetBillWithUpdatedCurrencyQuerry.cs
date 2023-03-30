using CashRegister.Application.Dto;
using MediatR;

namespace CashRegister.API.Mediator.Querries.BillQuerries
{
    public class GetBillWithUpdatedCurrencyQuerry : IRequest<DisplayBillDto>
    {
        public string BillNumber { get; set; }
        public string Currency { get; set; }
        public GetBillWithUpdatedCurrencyQuerry(string billNumber, string currency)
        {
            BillNumber = billNumber;
            Currency = currency;
        }
    }
}
