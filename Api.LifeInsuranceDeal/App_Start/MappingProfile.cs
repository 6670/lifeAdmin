using AutoMapper;
using EntityObjects.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.LifeInsuranceDeal.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<AllFieldCapture, CommonLead>();
            CreateMap<AllFieldCapture, LifeLead>();
            CreateMap<AllFieldCapture, FuneralLead>();

        }

    }
}