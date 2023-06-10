using FluentValidation;
using LayerTestApp.Payroll.BAL.Models.DTOs.PayGradeDTOs;
using LayerTestApp.Payroll.BAL.ServiceContracts;

namespace LayerTestApp.Payroll.BAL.Models.Validators.PayGradeValidators
{
    public class CreatePayGradeDTOValidator : AbstractValidator<CreatePayGradeDTO>
    {
        private readonly IPayGradeService _payGradeService;

        public CreatePayGradeDTOValidator(IPayGradeService payGradeService)
        {
            _payGradeService = payGradeService;

            RuleFor(cpg => cpg.PayGradeName)
                .MinimumLength(5)
                .WithMessage("PayGrade name should have minimum 5 characters.")
                .MaximumLength(50)
                .WithMessage("PayGrade name should have maximum 50 characters.")
                .MustAsync(PayGradeNameIsUnique)
                .WithMessage("PayGrade name already exists.");
        }

        private async Task<bool> PayGradeNameIsUnique(string payGradeName, CancellationToken ct)
        {
            bool payGradeNameExists = await _payGradeService.NameExistsAsync(payGradeName, ct);
            return !payGradeNameExists;
        }
    }
}
