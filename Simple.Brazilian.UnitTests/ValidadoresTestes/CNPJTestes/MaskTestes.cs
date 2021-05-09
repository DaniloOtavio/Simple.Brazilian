﻿using Simple.Brazilian.Validadores;
using Xunit;

namespace Simple.Brazilian.UnitTests.ValidadoresTestes.CNPJTestes
{
    public class MaskTestes
    {
        [Theory]
        [InlineData("12345678000190", "12.345.678/0001-90")]
        public void CNPJ_Unmask(string entrada, string saida)
        {
            Assert.Equal(saida, CNPJ.Mask(entrada));
        }
    }
}
