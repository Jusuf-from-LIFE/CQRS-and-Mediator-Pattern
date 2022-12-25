using AlteraMediator.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlteraMediator.Commands
{
    public record CreateBookCommand(List<Book> Books) : IRequest<List<Book>>;
}
