using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Models;

namespace CleanArch.Application.AutoMapper
{
    public class DomainProfilesToViewModel : Profile
    {
        public DomainProfilesToViewModel() {
            CreateMap<Course, CourseViewModel>();
        }
    }
}
