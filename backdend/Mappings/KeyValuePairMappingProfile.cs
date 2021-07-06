using System;
using AutoMapper;
using backend.Dtos;
using backend.Models;

namespace backend.Mappings
{
    public class KeyValuePairMappingProfile : Profile
    {
        public KeyValuePairMappingProfile()
        {
            this.CreateMap<KeyValuePair, KeyValuePairDto>().ReverseMap();
            this.CreateMap<Entities.KeyValuePair, KeyValuePair>().ReverseMap();
        }
    }
}
