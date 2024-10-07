using AutoMapper;
using CleanArchitecture.Application.Contracts.Logging;
using CleanArchitecture.Application.Contracts.Persistance;
using CleanArchitecture.Application.Exceptions;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public class GetLeaveTypeDetailsQueryHandler : IRequestHandler<GetLeaveTypesDetailsQuery,LeaveTypeDetailsDto>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public GetLeaveTypeDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypesDetailsQuery request, CancellationToken cancellationToken)
        {
            var leaveTypes =  await _leaveTypeRepository.GetByIdAsync(request.Id);

            if (leaveTypes == null)
            {
                throw new NotFoundException(nameof(LeaveType), request.Id);
            }

            var data = _mapper.Map<LeaveTypeDetailsDto>(leaveTypes);

            return data;
        }
    }
}
