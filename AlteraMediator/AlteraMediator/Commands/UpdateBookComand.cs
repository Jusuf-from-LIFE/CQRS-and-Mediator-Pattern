using AlteraMediator.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlteraMediator.Commands
{
    public record UpdateBookCommand(int Id, string newDescription) : IRequest<Book>;
}
