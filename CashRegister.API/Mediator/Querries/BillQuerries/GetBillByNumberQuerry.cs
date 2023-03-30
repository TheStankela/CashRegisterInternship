using CashRegister.Application.Dto;
using MediatR;

namespace CashRegister.API.Mediator.Querries.BillQuerries
{
    public class GetBillByNumberQuerry : IRequest<DisplayBillDto>
    {
        public string BillNumber { get; set; }
        public GetBillByNumberQuerry(string billNumber)
        {
            BillNumber = billNumber;
        }
    }
}
