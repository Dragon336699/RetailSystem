using FluentValidation.TestHelper;
using RetailSystem.Application.Validation.Users;
using RetailSystem.SharedLibrary.Dtos.Users;

public class RegisterCustomerCommandValidatorTests
{
    private RegisterCustomerCommandValidator CreateValidator()
    {
        return new RegisterCustomerCommandValidator();
    }

    private RegisterCustomerCommand CreateValidModel()
    {
        return new RegisterCustomerCommand
        {
            FullName = "Nguyen Van A",
            UserName = "user_01",
            Password = "Password1"
        };
    }

    // HAPPY CASE
    [Fact]
    public void Should_Not_Have_Error_When_Input_Is_Valid()
    {
        var validator = CreateValidator();
        var model = CreateValidModel();

        var result = validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    // FULLNAME
    [Fact]
    public void Should_Have_Error_When_FullName_Is_Empty()
    {
        var validator = CreateValidator();
        var model = CreateValidModel() with { FullName = "" };

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.FullName);
    }

    [Fact]
    public void Should_Have_Error_When_FullName_Too_Long()
    {
        var validator = CreateValidator();
        var model = CreateValidModel() with { FullName = new string('A', 101) };

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.FullName);
    }

    // USERNAME
    [Fact]
    public void Should_Have_Error_When_UserName_Is_Empty()
    {
        var validator = CreateValidator();
        var model = CreateValidModel() with { UserName = "" };

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.UserName);
    }

    [Fact]
    public void Should_Have_Error_When_UserName_Too_Short()
    {
        var validator = CreateValidator();
        var model = CreateValidModel() with { UserName = "ab" };

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.UserName);
    }

    [Fact]
    public void Should_Have_Error_When_UserName_Invalid_Characters()
    {
        var validator = CreateValidator();
        var model = CreateValidModel() with { UserName = "user@name!" };

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.UserName);
    }

    // PASSWORD

    [Fact]
    public void Should_Have_Error_When_Password_Is_Empty()
    {
        var validator = CreateValidator();
        var model = CreateValidModel() with { Password = "" };

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Too_Short()
    {
        var validator = CreateValidator();
        var model = CreateValidModel() with { Password = "Aa1" };

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Missing_Uppercase()
    {
        var validator = CreateValidator();
        var model = CreateValidModel() with { Password = "password1" };

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Missing_Lowercase()
    {
        var validator = CreateValidator();
        var model = CreateValidModel() with { Password = "PASSWORD1" };

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Missing_Number()
    {
        var validator = CreateValidator();
        var model = CreateValidModel() with { Password = "Password" };

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Password);
    }
}