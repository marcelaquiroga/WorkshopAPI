using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopApp.Models;

namespace WorkshopApp.DTOs
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Sale, SaleDTO>().ReverseMap();
            CreateMap<SaleDetail, SaleDetailDTO>().ReverseMap();
        }
    }
}
