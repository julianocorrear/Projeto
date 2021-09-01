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
                 }
                 else
                 //Append se já existe
                 {                  
                     var log = CarregaArquivoDeLog();                                          
                     var texto = new System.Text.StringBuilder();
                     string[] linhas =  varLog.Split('\n');
                     using (StreamWriter file = new StreamWriter(arquivoDeLog, append:true))
                     foreach (string linha in linhas)
                     {
                         texto.Append($"{linha}{"\n"}");
                         file.WriteLine(linha);
                     }                     
                 }
             }

             public String CarregaArquivoDeLog()
             {
                 string log = "a";
                 if (System.IO.File.Exists(arquivoDeLog))
                 {
                     log = File.ReadAllText(arquivoDeLog);                                     
                 }
                 return log;
             }

             public int CalculaQuantasPesquisas(string varLog)
             {
                 int contador = 0;
                 string[] linhas = varLog.Split('\n');
                 foreach (string linha in linhas)
                 {
                     if (linha.Contains("Numero da consulta:"))
                     {
                         contador++;
                     }
                 }
                 return contador;
             }                        
             public string PreparaLog(String varLog, String varDocName, String varBuscaRefinada)
             {
                 var texto = new System.Text.StringBuilder();
                 string log = CarregaArquivoDeLog();
                 int pesquisas = CalculaQuantasPesquisas(log);
                 texto.Append("****************************\n");
                 texto.Append("Numero da consulta: "+pesquisas+"\n");
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