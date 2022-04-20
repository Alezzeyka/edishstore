using ASPLab.Data.Models;
using System.Collections.Generic;

namespace ASPLab.Data.Interfaces
{
    public interface IDishCategory
    {
        IEnumerable<Category> Categories { get; }
    }
}
