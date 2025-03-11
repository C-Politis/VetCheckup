using VetCheckup.Application.Common.Exceptions;
using FluentAssertions;
using FluentValidation.Results;
using Xunit;

namespace VetCheckup.Application.UnitTests.Common.Exceptions;

public class ValidationExceptionTests
{
    [Fact]
    public void DefaultConstructorCreatesAnEmptyErrorDictionary()
    {
        // Arrange
        var exception = new ValidationException();

        // Act
        var actual = exception.Errors;

        // Assert
        actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
    }

    [Fact]
    public void SingleValidationFailureCreatesASingleElementErrorDictionary()
    {
        // Arrange
        var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Age", "must be over 18"),
            };

        // Act
        var actual = new ValidationException(failures).Errors;

        // Assert
        actual.Keys.Should().BeEquivalentTo(new string[] { "Age" });
        actual["Age"].Should().BeEquivalentTo(new string[] { "must be over 18" });
    }

    [Fact]
    public void MulitpleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
    {
        // Arrange
        var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Age", "must be 18 or older"),
                new ValidationFailure("Age", "must be 25 or younger"),
                new ValidationFailure("Password", "must contain at least 8 characters"),
                new ValidationFailure("Password", "must contain a digit"),
                new ValidationFailure("Password", "must contain upper case letter"),
                new ValidationFailure("Password", "must contain lower case letter"),
            };

        // Act
        var actual = new ValidationException(failures).Errors;

        // Assert
        actual.Keys.Should().BeEquivalentTo(new string[] { "Password", "Age" });

        actual["Age"].Should().BeEquivalentTo(new string[]
        {
                "must be 25 or younger",
                "must be 18 or older",
        });

        actual["Password"].Should().BeEquivalentTo(new string[]
        {
                "must contain lower case letter",
                "must contain upper case letter",
                "must contain at least 8 characters",
                "must contain a digit",
        });
    }

}
