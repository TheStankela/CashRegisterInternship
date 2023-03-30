using CashRegister.API.Dto;
using CashRegister.API.Mediator.Querries.BillQuerries;
using CashRegister.Application.Dto;
using CashRegister.Application.Services;
using CashRegister.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.API.Mediator.Handlers.BillHandlers
{
    public class GetAllBillsHandler : IRequestHandler<GetAllBillsQuerry, List<DisplayBillDto>>
    {
        private readonly IBillService _billService;
        public GetAllBillsHandler(IBillService billService)
        {
            _billService = billService;
        }
        public async Task<List<DisplayBillDto>> Handle(GetAllBillsQuerry request, CancellationToken cancellationToken)
        {
            return await _billService.GetAllBillsAsync();
        }
    }
}
