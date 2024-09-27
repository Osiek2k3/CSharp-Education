using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Commands;
using CleanArch.Domain.Core.Bus;
using CleanArch.Domain.Interfaces;

namespace CleanArch.Application.Services
{
    public class CourseService : ICourseServices
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMediatorHandler _bus;
        private readonly IMapper _automapper;

        public CourseService(ICourseRepository courseRepository, IMediatorHandler bus, IMapper automapper)
        {
            _courseRepository = courseRepository;
            _bus = bus;
            _automapper = automapper;
        }

        public IEnumerable<CourseViewModel> GetCourses()
        {
            return _courseRepository.GetCourses().ProjectTo<CourseViewModel>(_automapper.ConfigurationProvider);
        }

        public void Create(CourseViewModel courseViewModel)
        {
            /*var createCourseCommand = new CreateCourseCommand(
                    courseViewModel.Name,
                    courseViewModel.Description,
                    courseViewModel.Imageurl
                );*/

            _bus.SendCommand(_automapper.Map<CreateCourseCommand>(courseViewModel));
        }
    }
}
