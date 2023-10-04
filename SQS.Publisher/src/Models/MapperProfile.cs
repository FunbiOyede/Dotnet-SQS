using System;
using AutoMapper;
using SQS.Publisher.Messaging;

namespace SQS.Publisher.Models
{


	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<OrderData, Orders>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.purchasedDate, opt => opt.MapFrom(src => src.purchasedDate));
        }
	}
}

