using ASPLab.Data.Models;
using System;
using System.Collections.Generic;

namespace ASPLab.Data.Interfaces
{
    public interface IAllDish
    {
        IEnumerable<Dish> Dishes { get; }
        Dish GetDish(Guid dishID);
    }
}
