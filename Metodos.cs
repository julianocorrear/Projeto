using System;
using System.IO;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace Classes
{
    public class Metodos
    {
            #region CarregaPDF
            public string CarregaPDF(string vardocname)
            {
                string TextoAux;
                using (PdfReader reader = new PdfReader (@"C:\Temp\"+vardocname))
                {
                    System.Text.StringBuilder textoDoPdf = new System.Text.StringBuilder();
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        string aux = PdfTextExtractor.GetTextFromPage(reader, i);
                        string[] linhasDoPdf = aux.Split('\n');
                        foreach(string linha in linhasDoPdf)
                        {
                            textoDoPdf.Append($"{linha}{"\n"}");
                        }
                        
                    }
                
                    TextoAux = textoDoPdf.ToString();
                }
                return TextoAux;

            }
           #endregion
           public string DefineQuantPalavras()
           {
                Console.WriteLine("Quantas Palavras Deseja pesquisar? (1 ou 2)");
                string varopcao = Console.ReadLine();
                return varopcao;
           }

            public string Pesquisa(string vartexto)
            {
                System.Text.StringBuilder resultado = new System.Text.StringBuilder();
                Console.WriteLine("Qual expressão deseja pesquisar? (Utilize AND ou OR como operadores lógicos)");
                string busca = Console.ReadLine();
                string[] linhas = vartexto.Split('\n');
                    foreach(string linha in linhas)
                    {
                        
                        if(linha.Contains(busca))
                        {                            
                            resultado.Append($"{linha}{"\n"}");
                        }   

                    }
                    vartexto = resultado.ToString();
                    return vartexto;                 

                
            }
           
    }
}