using AlteraMediator.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlteraMediator.Queries
{
    public record GetBookQuery(int Id) : IRequest<Book>;
}
