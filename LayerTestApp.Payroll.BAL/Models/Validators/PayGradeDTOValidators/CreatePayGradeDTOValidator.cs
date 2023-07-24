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
                .WithMessage("PayGradeName should have minimum 5 characters.")
                .MaximumLength(50)
                .WithMessage("PayGradeName should have maximum 50 characters.")
                .Must(PayGradeNameIsUnique)
                .WithMessage("PayGradeName already exists.");
        }

        private bool PayGradeNameIsUnique(string payGradeName)
        {
            bool payGradeNameExists = Task.Run(() => _payGradeService.NameExistsAsync(payGradeName)).Result;
            return !payGradeNameExists;
        }
    }
}
