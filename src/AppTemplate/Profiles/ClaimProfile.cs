using AppTemplate.Application.Models;
using AppTemplate.Models;
using AutoMapper;

namespace AppTemplate.Profiles
{
    public class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CreateMap<CreateClaimModel, Claim>();
        }
    }
}
