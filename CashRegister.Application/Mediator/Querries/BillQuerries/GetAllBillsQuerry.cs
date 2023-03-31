using CashRegister.Application.Dto;
using CashRegister.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.API.Mediator.Querries.BillQuerries
{
    public class GetAllBillsQuerry : IRequest<List<DisplayBillDto>>
    {
    }
}
