using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleDTO, Sale>();
        }
    }
}
