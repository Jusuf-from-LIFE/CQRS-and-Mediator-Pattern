using AlteraMediator.Data.UnitOfWork;
using AlteraMediator.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlteraMediator.Queries
{
    public record GetBookListQuery() : IRequest<List<Book>>;
}
