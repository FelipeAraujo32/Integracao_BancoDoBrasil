using System;
using BancoDoBrasil.Formatting;
using FluentAssertions;
using Xunit;

namespace BancoDoBrasil.Tests.Formatting;

public class BbDateFormatterTests
{
    [Fact]
    public void Format_DeveFormatarDataNoPadraoBB()
    {
        // Given
        var data = new DateTime(2026, 1, 15);
    
        // When
        var resultado = BbDateFormatter.Format(data);

        // Then
        resultado.Should().Be("15.01.2026");
    }

    [Fact]
    public void Format_NullableComValor_DeveFormatar()
    {
        // Given
        DateTime? data = new DateTime(2026, 12, 31);
    
        // When
        var resultado = BbDateFormatter.Format(data);
    
        // Then
        resultado.Should().Be("31.12.2026");
    }

    [Fact]
    public void Format_NullableNulo_DeveRetornarNull()
    {
        DateTime? data = null;

        var resultado = BbDateFormatter.Format(data);

        resultado.Should().BeNull();
    }

}