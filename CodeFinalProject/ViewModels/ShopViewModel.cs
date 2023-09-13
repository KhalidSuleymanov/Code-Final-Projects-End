using CodeFinalProject.Models;
using Microsoft.Net.Http.Headers;

namespace CodeFinalProject.ViewModels
{
    public class ShopViewModel
    {
        public List<Room> Rooms { get; set; }

        public List<Room> Names { get; set; }

        public List<string> SelectedName { get; set; }

        public int? SelectedElderCount { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }

        public int? SelectedChilCount { get; set; }

        public decimal SelectedMinPrice { get; set; }

        public decimal SelectedMaxPrice { get; set; }

    }
}



