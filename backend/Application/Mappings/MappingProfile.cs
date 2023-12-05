using AutoMapper;
using Application.Features.Customer.Commands.Insert;
using Application.Features.Customer.Commands.Update;
using Application.Features.Customer.Queries.Dtos;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerToReturnDto>();


            CreateMap<Customer, InsertCommand>().ReverseMap();
            CreateMap<Customer, UpdateCommand>().ReverseMap();

        }
    }
}