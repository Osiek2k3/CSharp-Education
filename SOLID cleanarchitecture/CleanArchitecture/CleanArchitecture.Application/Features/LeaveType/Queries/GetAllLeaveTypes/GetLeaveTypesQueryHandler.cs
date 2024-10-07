using AutoMapper;
using CleanArchitecture.Application.Contracts.Logging;
using CleanArchitecture.Application.Contracts.Persistance;
using CleanArchitecture.Application.DTOs;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;

        public GetLeaveTypesQueryHandler(IMapper mapper,ILeaveTypeRepository leaveTypeRepository,
            IAppLogger<GetLeaveTypesQueryHandler> logger) 
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
        }
        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
        {
            var leaveType =await _leaveTypeRepository.GetAsync();

            var data = _mapper.Map<List<LeaveTypeDto>>(leaveType);

            _logger.LogInformation("Leave types were retrieved successfully");
            return data;
        }
    }
}
