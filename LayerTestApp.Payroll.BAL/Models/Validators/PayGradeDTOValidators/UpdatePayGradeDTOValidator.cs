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

            //RuleFor(upg => upg.PayGradeId)
            //    .Must(PayGradeIdExists)
            //    .WithMessage("PayGrade Id not exists.");

            RuleFor(upg => upg.PayGradeName)
                .MinimumLength(5)
                .WithMessage("PayGrade name should have minimum 5 characters.")
                .MaximumLength(50)
                .WithMessage("PayGrade name should have maximum 50 characters.");

            RuleFor(upg => upg)
                .Must(PayGradeNameIsUnique)
                .WithName("PayGradeName")
                .WithMessage("PayGrade name already exists.");
        }

        private bool PayGradeIdExists(int id)
        {
            bool payGradeExists = Task.Run(() => _payGradeService.IdExistsAsync(id)).Result;
            return payGradeExists;
        }

        private bool PayGradeNameIsUnique(UpdatePayGradeDTO upg)
        {

            if (!PayGradeIdExists(upg.PayGradeId)) { return true; }

            bool payGradeNameExists = false;

            bool payGradeNameExistsInDB = Task.Run(() => _payGradeService.NameExistsAsync(upg.PayGradeName)).Result;
            int? id;

            if (payGradeNameExistsInDB)
            {
                id = Task.Run(() => _payGradeService.GetIdByNameAsync(upg.PayGradeName)).Result;
                payGradeNameExists = (id != upg.PayGradeId);
            }

            return !payGradeNameExists;
        }       
    }
}
