using AutoMapper;
using Demo.DataAccessLayer.Models;
using Mvc.PresentationLayer.ViewModels;

namespace Mvc.PresentationLayer.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            
        }
    }
}
