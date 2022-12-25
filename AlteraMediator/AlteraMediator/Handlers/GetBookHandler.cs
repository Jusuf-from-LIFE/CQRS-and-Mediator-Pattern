using AlteraMediator.Data.UnitOfWork;
using AlteraMediator.Helpers;
using AlteraMediator.Models;
using AlteraMediator.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace AlteraMediator.Handlers
{
    public class GetBookHandler : IRequestHandler<GetBookQuery, Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;

        public GetBookHandler(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
        }

        public async Task<Book> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = _memoryCache.Get<Book>("Book");

            if (book is not null && book.Id == request.Id)
            {
                return book;
            }
            else
            {
                book = await _unitOfWork.Repository<Book>().GetById(x => x.Id == request.Id).FirstOrDefaultAsync();
                if (book is null)
                {
                    return null;
                }

                _memoryCache.Set("Book", book, TimeSpan.FromSeconds(60));
            }
                        
            return book;
        }

        /*private async Task<Book> GetBookFromDatabase(int Id)
        {
            var book = await _unitOfWork.Repository<Book>().GetById(x => x.Id == Id).FirstOrDefaultAsync();
            if (book is null)
            {
                return null;
            }

            return book;
        }*/ 
    }
}
