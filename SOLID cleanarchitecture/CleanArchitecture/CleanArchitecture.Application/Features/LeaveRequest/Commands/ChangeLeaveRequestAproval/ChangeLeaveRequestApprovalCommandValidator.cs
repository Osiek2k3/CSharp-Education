using FluentValidation;

namespace CleanArchitecture.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestAproval
{
    public class ChangeLeaveRequestApprovalCommandValidator : AbstractValidator<ChangeLeaveRequestApprovalCommand>
    {
        public ChangeLeaveRequestApprovalCommandValidator()
        {
            RuleFor(p => p.Approved)
                .NotNull()
                .WithMessage("Approval status cannot be null");
        }
    }
}
