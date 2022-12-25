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

namespace AlteraMediator.Handlers
{
    public class GetBookListHandler : IRequestHandler<GetBookListQuery, List<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _redisCache;

        public GetBookListHandler(IUnitOfWork unitOfWork, IDistributedCache redisCache)
        {
            _unitOfWork = unitOfWork;
            _redisCache = redisCache;
        }

        public async Task<List<Book>> Handle(GetBookListQuery request, CancellationToken cancellationToken)
        {
            var books = await _redisCache.GetRecordAsync<List<Book>>("Books");

            if (books is null)
            {
                books = await _unitOfWork.Repository<Book>().GetAll().ToListAsync();
                if(!books.Any())
                {
                    return null;
                }

                await _redisCache.SetRecordAsync("Books", books, TimeSpan.FromMinutes(1));
            }

            return books;
        }
    }
}
