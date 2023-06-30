using AppTemplate.Application.Models;
using AppTemplate.Models;
using AutoMapper;

namespace AppTemplate.Profiles
{
    public class UsernameAndPasswordProfile : Profile
    {
        public UsernameAndPasswordProfile()
        {
            CreateMap<UsernameAndPassword, UsernameAndPassworModel>().ReverseMap();
        }
    }
}
