using CashRegister.API.Mediator.Commands;
using CashRegister.API.Mediator.Commands.BillCommands;
using CashRegister.Application.Services;
using CashRegister.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Mediator.Handlers.BillHandlers
{
    public class CreateBillHandler : IRequestHandler<CreateBillCommand, bool>
    {
        private readonly IBillService _billService;
        public CreateBillHandler(IBillService billService)
        {
            _billService = billService;
        }

        async Task<bool> IRequestHandler<CreateBillCommand, bool>.Handle(CreateBillCommand request, CancellationToken cancellationToken)
        {
            return _billService.AddBill(request.BillDto);
        }
    }
}
