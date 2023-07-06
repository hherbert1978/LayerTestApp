using FluentValidation.TestHelper;
using LayerTestApp.Payroll.BAL.Models.DTOs.PayGradeDTOs;
using LayerTestApp.Payroll.BAL.Models.Validators.PayGradeValidators;
using Microsoft.Extensions.Logging;

namespace LayerTestApp.Payroll.BAL.Tests
{
    [TestFixture, Order(1)]
    public class ValidatorTests : BaseTestClass
    {
        private CreatePayGradeDTOValidator _createPayGradeDTOValidator;
        private UpdatePayGradeDTOValidator _updatePayGradeDTOValidator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _logger.LogInformation("------------------------------------------------------------------------------------------");
            _logger.LogInformation("Starting ValidatorTests.");
            _logger.LogInformation("------------------------------------------------------------------------------------------\r\n");

            _createPayGradeDTOValidator = new CreatePayGradeDTOValidator(_payGradeService);
            _updatePayGradeDTOValidator = new UpdatePayGradeDTOValidator(_payGradeService);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _logger.LogInformation("------------------------------------------------------------------------------------------");
            _logger.LogInformation("Finishing ValidatorTests.");
            _logger.LogInformation("------------------------------------------------------------------------------------------\r\n");
        }

        #region "CreatePayGradeDTOValidator"

        [Test, Order(1)]
        public async Task CreatePayGradeDTOValidatorUniqueNameFailureTest()
        {
            _logger.LogInformation("Starting CreatePayGradeDTOValidatorUniqueNameFailureTest.");

            var createPayGradeDTO = new CreatePayGradeDTO("Meister");

            try
            {
                var result = await _createPayGradeDTOValidator.TestValidateAsync(createPayGradeDTO);

                Assert.Multiple(() =>
                {
                    Assert.That(result.IsValid, Is.EqualTo(false));
                    Assert.That(result.Errors, Has.Count.EqualTo(1));
                    Assert.That(result.Errors.FirstOrDefault()?.PropertyName, Is.EqualTo("PayGradeName"));
                    Assert.That(result.Errors.FirstOrDefault()?.ErrorMessage, Is.EqualTo("PayGrade name already exists."));
                });
                _logger.LogInformation("Validator Test - CreatePayGradeDTOValidatorUniqueNameFailureTest - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Validator Test - CreatePayGradeDTOValidatorUniqueNameFailureTest - Test finished with error. \r\n");
            }
        }

        [Test, Order(2)]
        public async Task CreatePayGradeDTOValidatorUniqueNameSuccessTest()
        {
            _logger.LogInformation("Starting CreatePayGradeDTOValidatorUniqueNameSuccessTest.");

            var createPayGradeDTO = new CreatePayGradeDTO("Azubi");

            try
            {
                var result = await _createPayGradeDTOValidator.TestValidateAsync(createPayGradeDTO);

                Assert.Multiple(() =>
                {
                    Assert.That(result.IsValid, Is.EqualTo(true));
                    Assert.That(result.Errors, Has.Count.EqualTo(0));
                });
                _logger.LogInformation("Validator Test - CreatePayGradeDTOValidatorUniqueNameSuccessTest - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Validator Test - CreatePayGradeDTOValidatorUniqueNameSuccessTest - Test finished with error. \r\n");
            }
        }

        [Test, Order(3)]
        public async Task CreatePayGradeDTOValidatorMinLengthFailureTest()
        {
            _logger.LogInformation("Starting CreatePayGradeDTOValidatorMinLengthFailureTest.");

            var createPayGradeDTO = new CreatePayGradeDTO("H");

            try
            {
                var result = await _createPayGradeDTOValidator.TestValidateAsync(createPayGradeDTO);

                Assert.Multiple(() =>
                {
                    Assert.That(result.IsValid, Is.EqualTo(false));
                    Assert.That(result.Errors, Has.Count.EqualTo(1));
                    Assert.That(result.Errors.FirstOrDefault()?.PropertyName, Is.EqualTo("PayGradeName"));
                    Assert.That(result.Errors.FirstOrDefault()?.ErrorMessage, Is.EqualTo("PayGrade name should have minimum 5 characters."));
                });
                _logger.LogInformation("Validator Test - CreatePayGradeDTOValidatorUniqueNameFailureTest - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Validator Test - CreatePayGradeDTOValidatorUniqueNameFailureTest - Test finished with error. \r\n");
            }
        }

        [Test, Order(4)]
        public async Task CreatePayGradeDTOValidatorMaxLengthFailureTest()
        {
            _logger.LogInformation("Starting CreatePayGradeDTOValidatorMaxLengthFailureTest.");

            var createPayGradeDTO = new CreatePayGradeDTO("MeisterGeselleLehrlingFeldarbeiterHilfsarbeiterAushilfe");

            try
            {
                var result = await _createPayGradeDTOValidator.TestValidateAsync(createPayGradeDTO);

                Assert.Multiple(() =>
                {
                    Assert.That(result.IsValid, Is.EqualTo(false));
                    Assert.That(result.Errors, Has.Count.EqualTo(1));
                    Assert.That(result.Errors.FirstOrDefault()?.PropertyName, Is.EqualTo("PayGradeName"));
                    Assert.That(result.Errors.FirstOrDefault()?.ErrorMessage, Is.EqualTo("PayGrade name should have maximum 50 characters."));
                });
                _logger.LogInformation("Validator Test - CreatePayGradeDTOValidatorUniqueNameFailureTest - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Validator Test - CreatePayGradeDTOValidatorUniqueNameFailureTest - Test finished with error. \r\n");
            }
        }

        #endregion

        #region "UpdatePayGradeDTOValidator"

        [Test, Order(5)]
        public async Task UpdatePayGradeDTOValidatorUniqueNameFailureTest()
        {
            _logger.LogInformation("Starting UpdatePayGradeDTOValidatorUniqueNameFailureTest.");

            var updatePayGradeDTO = new UpdatePayGradeDTO(3, "Meister", true);

            try
            {
                var result = await _updatePayGradeDTOValidator.TestValidateAsync(updatePayGradeDTO);

                Assert.Multiple(() =>
                {
                    Assert.That(result.IsValid, Is.EqualTo(false));
                    Assert.That(result.Errors, Has.Count.EqualTo(1));
                    Assert.That(result.Errors.FirstOrDefault()?.PropertyName, Is.EqualTo("PayGradeName"));
                    Assert.That(result.Errors.FirstOrDefault()?.ErrorMessage, Is.EqualTo("PayGrade name already exists."));
                });
                _logger.LogInformation("Validator Test - UpdatePayGradeDTOValidatorUniqueNameFailureTest - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Validator Test - CreatePayGradeDTOValidatorUniqueNameFailureTest - Test finished with error. \r\n");
            }
        }

        [Test, Order(6)]
        public async Task UpdatePayGradeDTOValidatorUniqueNameWithoutChangingNameSuccessTest()
        {
            _logger.LogInformation("Starting UpdatePayGradeDTOValidatorUniqueNameWithoutChangingNameSuccessTest.");

            var updatePayGradeDTO = new UpdatePayGradeDTO(5, "Hilfsarbeiter", true);

            try
            {
                var result = await _updatePayGradeDTOValidator.TestValidateAsync(updatePayGradeDTO);

                Assert.Multiple(() =>
                {
                    Assert.That(result.IsValid, Is.EqualTo(true));
                    Assert.That(result.Errors, Has.Count.EqualTo(0));
                });
                _logger.LogInformation("Validator Test - UpdatePayGradeDTOValidatorUniqueNameWithoutChangingNameSuccessTest - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Validator Test - UpdatePayGradeDTOValidatorUniqueNameWithoutChangingNameSuccessTest - Test finished with error. \r\n");
            }
        }

        [Test, Order(7)]
        public async Task UpdatePayGradeDTOValidatorUniqueNameWithChangingNameSuccessTest()
        {
            _logger.LogInformation("Starting UpdatePayGradeDTOValidatorUniqueNameWithoutChangingNameSuccessTest.");

            var updatePayGradeDTO = new UpdatePayGradeDTO(5, "Aushilfsarbeiter", false);

            try
            {
                var result = await _updatePayGradeDTOValidator.TestValidateAsync(updatePayGradeDTO);

                Assert.Multiple(() =>
                {
                    Assert.That(result.IsValid, Is.EqualTo(true));
                    Assert.That(result.Errors, Has.Count.EqualTo(0));
                });
                _logger.LogInformation("Validator Test - UpdatePayGradeDTOValidatorUniqueNameWithChangingNameSuccessTest - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Validator Test - UpdatePayGradeDTOValidatorUniqueNameWithChangingNameSuccessTest - Test finished with error. \r\n");
            }
        }

        [Test, Order(8)]
        public async Task UpdatePayGradeDTOValidatorMinLengthFailureTest()
        {
            _logger.LogInformation("Starting UpdatePayGradeDTOValidatorMinLengthFailureTest.");

            var updatePayGradeDTO = new UpdatePayGradeDTO(1, "H", false);

            try
            {
                var result = await _updatePayGradeDTOValidator.TestValidateAsync(updatePayGradeDTO);

                Assert.Multiple(() =>
                {
                    Assert.That(result.IsValid, Is.EqualTo(false));
                    Assert.That(result.Errors, Has.Count.EqualTo(1));
                    Assert.That(result.Errors.FirstOrDefault()?.PropertyName, Is.EqualTo("PayGradeName"));
                    Assert.That(result.Errors.FirstOrDefault()?.ErrorMessage, Is.EqualTo("PayGrade name should have minimum 5 characters."));
                });
                _logger.LogInformation("Validator Test - UpdatePayGradeDTOValidatorMinLengthFailureTest - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Validator Test - UpdatePayGradeDTOValidatorMinLengthFailureTest - Test finished with error. \r\n");
            }
        }

        [Test, Order(9)]
        public async Task UpdatePayGradeDTOValidatorMaxLengthFailureTest()
        {
            _logger.LogInformation("Starting CreatePayGradeDTOValidatorMaxLengthFailureTest.");

            var updatePayGradeDTO = new UpdatePayGradeDTO(4, "MeisterGeselleLehrlingFeldarbeiterHilfsarbeiterAushilfe", true);

            try
            {
                var result = await _updatePayGradeDTOValidator.TestValidateAsync(updatePayGradeDTO);

                Assert.Multiple(() =>
                {
                    Assert.That(result.IsValid, Is.EqualTo(false));
                    Assert.That(result.Errors, Has.Count.EqualTo(1));
                    Assert.That(result.Errors.FirstOrDefault()?.PropertyName, Is.EqualTo("PayGradeName"));
                    Assert.That(result.Errors.FirstOrDefault()?.ErrorMessage, Is.EqualTo("PayGrade name should have maximum 50 characters."));
                });
                _logger.LogInformation("Validator Test - UpdatePayGradeDTOValidatorMaxLengthFailureTest - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Validator Test - UpdatePayGradeDTOValidatorMaxLengthFailureTest - Test finished with error. \r\n");
            }
        }

        #endregion

    }
}
