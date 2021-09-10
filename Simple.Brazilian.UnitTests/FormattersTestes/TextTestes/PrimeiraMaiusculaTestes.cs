using Simple.Brazilian.Formatters;
using Xunit;

namespace Simple.Brazilian.UnitTests.FormattersTestes.TextTestes
{
    public class PrimeiraMaiusculaTestes
    {
        [Fact]
        public void Texto_1()
        {
            Assert.Equal("The book is on the table.", Text.PrimeiraMaiuscula("the book is on the table."));
        }

        [Fact]
        public void Texto_2()
        {
            Assert.Null(Text.PrimeiraMaiuscula(""));
        }

        [Fact]
        public void Texto_3()
        {
            Assert.Equal("1337Yt", Text.PrimeiraMaiuscula("1337Yt"));
        }

        [Fact]
        public void Texto_4()
        {
            Assert.Equal("123", Text.PrimeiraMaiuscula("123"));
        }

        [Fact]
        public void Texto_5()
        {
            Assert.Equal("NÃO", Text.PrimeiraMaiuscula("nÃO"));
        }

        [Fact]
        public void Texto_6()
        {
            Assert.Equal("Caneca", Text.PrimeiraMaiuscula("caneca"));
        }

        [Fact]
        public void Texto_7()
        {
            Assert.Equal("CANECA", Text.PrimeiraMaiuscula("CANECA"));
        }
        [Fact]
        public void Texto_8()
        {
            Assert.Null(Text.PrimeiraMaiuscula(" "));
        }
        [Fact]
        public void Texto_9()
        {
            Assert.Null(Text.PrimeiraMaiuscula("           "));
        }

        [Fact]
        public void Texto_10()
        {
            Assert.Equal("$%Teste", Text.PrimeiraMaiuscula("$%teste"));
        }
        [Fact]
        public void Texto_11()
        {
            Assert.Equal("     Teste     ", Text.PrimeiraMaiuscula("     teste     "));
        }
    }
}
