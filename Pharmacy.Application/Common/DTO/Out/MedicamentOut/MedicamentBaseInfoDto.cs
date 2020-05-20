﻿using AutoMapper;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.Out.MedicamentOut
{
    public class MedicamentBaseInfoDto: IMapFrom<Medicament>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public int Offtake { get; set; }

        public List<ImageOutDto> Images { get; set; }

        public MedicamentBaseInfoDto()
        {
            Images = new List<ImageOutDto>();
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Medicament, MedicamentOutDto>()
                .ForMember(m => m.Images, mf => mf.MapFrom(p => p.Images));
        }
    }
}