using System;
using System.IO;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;


namespace Classes
{
    public class Metodos
    {
        public string arquivoDeLog = @"C:\Temp\LogDePesquisa.txt";
        
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
           public string DefineStringDePesquisa()
           {
                Console.WriteLine("Qual expressão deseja pesquisar? (Utilize AND ou OR como operadores lógicos)");
                string varBusca = Console.ReadLine();                
                return varBusca;
           }
           
           public string Pesquisa(string vartexto,string varBusca)
            {
                System.Text.StringBuilder resultado = new System.Text.StringBuilder();                                
                string[] linhas = vartexto.Split('\n');
                    foreach(string linha in linhas)
                    {                        
                        if(linha.Contains(varBusca))
                        {                            
                            resultado.Append($"{linha}{"\n"}");
                        }   
                    }
                    vartexto = resultado.ToString();
                    return vartexto;                                 
            }

             #region SalvaPesquisaEmArquivo
             public void SalvaEmArquivo(string varLog)
             {
                 if (!System.IO.File.Exists(arquivoDeLog))
                 //Cria novo se não existe
                 {
                     var texto = new System.Text.StringBuilder();
                     string[] linhas =  varLog.Split('\n');
                     using (StreamWriter file = new StreamWriter(arquivoDeLog))
                     foreach (string linha in linhas)
                     {
                         texto.Append($"{linha}{"\n"}");
                         file.WriteLine(linha);
                     }

                     //using (StreamWriter file = new StreamWriter(arquivoDeLog))
                     //for (int i = 0; i < 100; i++)
                     //file.Write($"{i} ");
                 }
                 else
                 //Append se já existe
                 {
                     
                 }

             }

             public string PreparaLog(String varLog, String varDocName, String varBuscaRefinada)
             {
                 var texto = new System.Text.StringBuilder();
                 texto.Append("****************************\n");
                 texto.Append("Numero da consulta: \n");
                 texto.Append("Nome do documento: "+varDocName+"\n");
                 texto.Append("String de busca: "+varBuscaRefinada+"\n");
                 texto.Append("Ocorrências: \n");
                 texto.Append("****************************\n");

                 varLog = texto.ToString();
                 return varLog;
             }
             #endregion

           


    }
}