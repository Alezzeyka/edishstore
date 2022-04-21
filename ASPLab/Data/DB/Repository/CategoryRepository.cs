using ASPLab.Data.DB.Context;
using ASPLab.Data.Interfaces;
using ASPLab.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ASPLab.Data.DB.Repository
{
    public class CategoryRepository : IDishCategory
    {
        private readonly AppDBContent _db;
        public CategoryRepository(AppDBContent appDBContent)
        {
            _db = appDBContent;
        }
        public IEnumerable<Category> Categories => _db.Category;
    }
}
