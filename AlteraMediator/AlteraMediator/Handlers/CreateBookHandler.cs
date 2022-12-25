using AlteraMediator.Commands;
using AlteraMediator.Data.UnitOfWork;
using AlteraMediator.Helpers;
using AlteraMediator.Models;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlteraMediator.Handlers
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, List<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;

        public CreateBookHandler(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
        }

        public async Task<List<Book>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            if(request.Books is not null)
            {
                _unitOfWork.Repository<Book>().CreateRange(request.Books);
                _unitOfWork.Complete();

                _memoryCache.Set("Book", request.Books.LastOrDefault(), TimeSpan.FromSeconds(60));

                return request.Books;
            }

            return null;
        }
    }
}
