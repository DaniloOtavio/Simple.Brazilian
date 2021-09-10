using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Brazilian.Formatters
{
    /// <summary>
    /// Formatadores textuais
    /// </summary>
    public static class Text
    {
        /// <summary>
        /// Torna o texto compatível com ASCII sem acentuação. 
        /// Letras acentuadas serão convertidas e outros caracteres serão descartados
        /// </summary>
        /// <param name="texto">Texto a ser processado</param>
        /// <param name="opcoes">Opções de remoção</param>
        /// <returns>Retorno sem acentos</returns>
        public static string RemoveAcentos(this string texto, TextOptions opcoes = null)
        {
            return removeAcentos(texto, opcoes ?? TextOptions.Padrao);
        }

        private static string removeAcentos(string textoEntrada, TextOptions opcoes)
        {
            if (string.IsNullOrEmpty(textoEntrada)) return textoEntrada;

            string busca = "áéíóúýàèìòùâêîôûãõäëïöüÿåçñ ÁÉÍÓÚÝÀÈÌÒÙÂÊÎÔÛÃÕÄËÏÖÜŸÅÇÑ ªº°¹²³";
            string troca = "aeiouyaeiouaeiouaoaeiouyacn AEIOUYAEIOUAEIOUAOAEIOUYACN aoo123";

            char[] texto = textoEntrada.ToCharArray();

            for (int i = 0; i < texto.Length; i++)
            {
                // Textos em ASCII baixo, mantém
                // 20..2F => !"#$%&'()*+´-./
                // 30..39 => Números
                // 3A..40 => :;<=>?@
                // 41..5A => Letras MAIÚSCULAS
                // 5B..60 => [\]^_`
                // 61..7A => Letras minúsculas
                // 7B..7E => {|}
                if (texto[i] >= 0x20 && texto[i] <= 0x7E)
                {
                    continue;
                }

                // Pula linha
                if (texto[i] == '\r' || texto[i] == '\n')
                {
                    continue;
                }

                // Busca na lista de subsituição
                int idx = busca.IndexOf(texto[i]);
                // Tem na lista
                if (idx >= 0)
                {
                    texto[i] = troca[idx];
                    continue;
                }

                texto[i] = opcoes.CaracterPadraoSubstituicao;
            }

            return new string(texto);
        }

        /// <summary>
        /// Aplica uma máscara no texto
        /// </summary>
        /// <param name="texto">Texto base para aplicar a máscara</param>
        /// <param name="mascara">Máscara a ser usada. O caracter underline '_' será trocado pelo texto, demais caracteres serão mantidos</param>
        /// <returns>Texto com a máscara aplicada</returns>
        public static string ApplyMask(string texto, string mascara)
        {
            if (texto is null) throw new ArgumentNullException(nameof(texto));
            if (texto.Length == 0) return texto;

            if (mascara is null) throw new ArgumentNullException(nameof(mascara));
            if (mascara.Length == 0) throw new ArgumentException($"O parâmetro {nameof(mascara)} não pode ser vazio");

            var result = mascara.ToCharArray();
            int idxTexto = 0;
            for (int i = 0; i < mascara.Length && idxTexto < texto.Length; i++)
            {
                if (mascara[i] != '_') continue;

                result[i] = texto[idxTexto];
                idxTexto++;

                if (idxTexto == texto.Length) return new string(result, 0, i + 1);

            }

            return new string(result);
        }

        /// <summary>
        /// Remove todos os caracteres, exceto letras e números
        /// </summary>
        /// <param name="texto">Texto a ser limpo</param>
        /// <returns>Texto desformatado</returns>
        public static string RemoveMask(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return texto;

            StringBuilder sb = new StringBuilder(texto.Length);

            for (int i = 0; i < texto.Length; i++)
            {
                if (char.IsNumber(texto[i])) sb.Append(texto[i]);
                else if (char.IsLetter(texto[i])) sb.Append(texto[i]);
            }

            return sb.ToString();
        }
        /// <summary>
        /// Função para capitalizar a primeira letra de uma string. O restante da string não sofre alterações.
        /// </summary>
        /// <param name="texto">Texto a ser formatado</param>
        /// <returns>Retorna o próprio texto com a primeira letra maiúscula.</returns>
        public static string PrimeiraMaiuscula(string texto)
        {
            if (isNullOrWhiteSpaceOrEmpty(texto)) return null;

            string primeiraLetra = "";
            int posPrimeiraLetra = 0;
            for (int i = 0; i < texto.Length; i++)
            {
                if (!char.IsLetter(texto[i])) continue;

                primeiraLetra = char.ToUpper(texto[i]).ToString();
                posPrimeiraLetra = i;
                break;
            }
            if (primeiraLetra == "") return texto;

            texto = texto.Remove(posPrimeiraLetra, 1).Insert(posPrimeiraLetra, primeiraLetra);
            return texto;
        }

        private static bool isNullOrWhiteSpaceOrEmpty(string texto)
        {
            if (texto.Length == 0 || texto == null || texto == "") return true;

            bool allCharsWhiteSpace = false;
            for (int i = 0; i < texto.Length; i++)
            {
                if (!char.IsWhiteSpace(texto[i]))
                {
                    allCharsWhiteSpace = false;
                    break;
                }
                allCharsWhiteSpace = true;
            }

            return allCharsWhiteSpace;
        }
    }
}
