using AutoMapper;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Contracts.Persistance;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
    public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;

        public GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepository,
            IMapper mapper,IUserServices userServices)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _userServices = userServices;
        }

        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
        {

            // Check if it is logged in employee

            var leaveRequests = new List<Domain.LeaveRequest>();
            var requests = new List<LeaveRequestListDto>();

            if(request.IsLoggedUser)
            {
                var userId = _userServices.UserId;
                leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails(userId);

                var employee = await _userServices.GetEmployee(userId);
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach (var req in requests)
                {
                    req.Employee = employee;
                }
            }
            else
            {
                leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach(var req in requests)
                {
                    req.Employee = await _userServices.GetEmployee(req.RequestingEmployeeId);
                }
            }

            // Fill requests with employee information

            return requests;


        }
    }
}
