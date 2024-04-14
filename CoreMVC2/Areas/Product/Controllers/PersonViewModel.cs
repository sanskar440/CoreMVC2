using DataAccess.Models;

namespace CoreMVC2.Areas.Product.Controllers
{
    internal class PersonViewModel
    {
        public List<Persons> Persons { get; set; }
        public string Search { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public string OrderBy { get; set; }
    }
}