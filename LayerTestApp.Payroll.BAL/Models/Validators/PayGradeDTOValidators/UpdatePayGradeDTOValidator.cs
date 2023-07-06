using FluentValidation;
using LayerTestApp.Payroll.BAL.Models.DTOs.PayGradeDTOs;
using LayerTestApp.Payroll.BAL.ServiceContracts;

namespace LayerTestApp.Payroll.BAL.Models.Validators.PayGradeValidators
{
    public class UpdatePayGradeDTOValidator : AbstractValidator<UpdatePayGradeDTO>
    {
        private readonly IPayGradeService _payGradeService;

        public UpdatePayGradeDTOValidator(IPayGradeService payGradeService)
        {
            _payGradeService = payGradeService;

            RuleFor(upg => upg.PayGradeName)
                .MinimumLength(5)
                .WithMessage("PayGrade name should have minimum 5 characters.")
                .MaximumLength(50)
                .WithMessage("PayGrade name should have maximum 50 characters.");

            RuleFor(upg => upg)
                .MustAsync(PayGradeNameIsUnique)
                .WithName("PayGradeName")
                .WithMessage("PayGrade name already exists.");
        }

        private async Task<bool> PayGradeNameIsUnique(UpdatePayGradeDTO upg, CancellationToken ct)
        {
            bool payGradeNameExists = false;

            bool payGradeNameExistsInDB = await _payGradeService.NameExistsAsync(upg.PayGradeName, ct);
            int? id;

            if (payGradeNameExistsInDB)
            {
                id = await _payGradeService.GetIdByNameAsync(upg.PayGradeName, ct);
                payGradeNameExists = (id != upg.PayGradeId);
            }

            return !payGradeNameExists;
        }

    }
}
