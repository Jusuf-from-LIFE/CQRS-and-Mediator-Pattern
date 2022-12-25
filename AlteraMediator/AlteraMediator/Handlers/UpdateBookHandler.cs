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
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;

        public UpdateBookHandler(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
        }

        public async Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _memoryCache.Get<Book>("Book");

            if(book is not null && book.Id == request.Id)
            {
                book.Description = request.newDescription;
                return book;
            }
            else
            {
                book = await _unitOfWork.Repository<Book>().GetById(x => x.Id == request.Id).FirstOrDefaultAsync();
                if (book is null)
                {
                    return null;
                }

                book.Description = request.newDescription;
                _memoryCache.Set("Book", book, TimeSpan.FromSeconds(60));
            }            

            return book;
        }
    }
}
