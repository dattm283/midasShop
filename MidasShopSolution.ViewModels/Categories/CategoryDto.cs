using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MidasShopSolution.ViewModels.Categories
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}