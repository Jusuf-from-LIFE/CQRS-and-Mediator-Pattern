using AlteraMediator.Commands;
using AlteraMediator.Data.UnitOfWork;
using AlteraMediator.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlteraMediator.Handlers
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;

        public DeleteBookHandler(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
        }

        public async Task<string> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = _memoryCache.Get<Book>("Book");

            if (book is null)
            {
                book = await _unitOfWork.Repository<Book>().GetById(x => x.Id == request.Id).FirstOrDefaultAsync();
                if (book is null)
                {
                    return $"Could not find a book with the given id: {request.Id}";
                }
            }

            _unitOfWork.Repository<Book>().Delete(book);
            _unitOfWork.Complete();

            _memoryCache.Remove("Book");

            return $"Successfully deleted book: {book.Id}";
        }
    }
}
