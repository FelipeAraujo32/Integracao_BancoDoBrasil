using BancoDoBrasil.Exceptions;
using BancoDoBrasil.Validation.Structure;
using BancoDoBrasil.Tests.Builders;
using FluentAssertions;
using Xunit;

namespace BancoDoBrasil.Tests.Validation.Structure;

public class RegistrarBoletoStructureValidatorTests
{
    [Fact]
    public void Validate_PagadorNulo_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComPagadorNulo()
            .Build();

        Action act = () => RegistrarBoletoStructureValidator.Validate(request);

        act.Should().Throw<BancoDoBrasilValidationException>()
            .WithMessage("*pagador*");
    }
}
