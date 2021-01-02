using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Wsei.ExchangeThings.Web.Database;
using Wsei.ExchangeThings.Web.Entities;
using Wsei.ExchangeThings.Web.Models;

namespace Wsei.ExchangeThings.Web.Services
{
    public class ExchangeItemsRepository
    {
        private readonly ExchangesDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExchangeItemsRepository(ExchangesDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<ItemEntity> GetAll()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id");
            if (userIdClaim == null)
                return Enumerable.Empty<ItemEntity>();

            var sql = $"SELECT * FROM Items WHERE IsVisible = 1 OR UserId = {userIdClaim.Value}";
            return _dbContext.Items.FromSqlRaw(sql);
        }

        public IEnumerable<ItemEntity> GetFiltered(string query)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id");
            if (userIdClaim == null)
                return Enumerable.Empty<ItemEntity>();

            var sql = $"SELECT * FROM Items WHERE (IsVisible = 1 OR UserId = {userIdClaim.Value}) AND Name LIKE '%{query}%'";
            return _dbContext.Items.FromSqlRaw(sql);
        }

        public ItemEntity Add(ItemModel item)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id");
            if (userIdClaim == null)
                throw new Exception("Failed to add: cannot find id claim");

            var entity = new ItemEntity
            {
                UserId = int.Parse(userIdClaim.Value),
                Name = item.Name,
                Description = item.Description,
                IsVisible = item.IsVisible,
            };

            _dbContext.Items.Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }
    }
}
