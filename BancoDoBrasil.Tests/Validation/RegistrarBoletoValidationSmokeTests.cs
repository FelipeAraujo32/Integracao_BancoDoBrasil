using BancoDoBrasil.Validation.Structure;
using BancoDoBrasil.Validation.Business;
using BancoDoBrasil.Tests.Common;
using Xunit;

namespace BancoDoBrasil.Tests.Validation;

public class RegistrarBoletoValidationSmokeTests
{
    [Fact]
    public void Request_Valido_Nao_Deve_Lancar_Excecao()
    {
        var request = RequestFactory.CriarValido();

        RegistrarBoletoStructureValidator.Validate(request);
        RegistrarBoletoBusinessValidator.Validate(request);
    }
}
