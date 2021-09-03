using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;


namespace Classes
{
    public unsafe class Metodos
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
           public void DefineStringDePesquisa()
           // define a string que será usada
           {
                Console.WriteLine("Qual expressão deseja pesquisar? (Utilize AND ou OR como operadores lógicos)");
                Objetos.varBusca = (Console.ReadLine()).ToLower();                               
           }
           
           public void Pesquisa(string vartexto,string varBusca)
           // executa a string de pesquisa no texto do pdf
            {
                Objetos.varOcorrencias = 0;                 
                var StringDeBusca = new List<string>();
                System.Text.StringBuilder resultado = new System.Text.StringBuilder();
                String[] varBuscaAux = varBusca.Split(' ');
                int validacao = 0; 
                int i = 0;               

                Console.WriteLine("Texto: "+vartexto);            
                foreach(string palavra in varBuscaAux)
                {
                    if (palavra.Equals("and"))
                    {
                        validacao = 1;                        
                    }

                    switch (validacao)                    
                    {
                        //caso a string possua AND
                        case 1:
                        //Console.WriteLine("Case 1: "+i);
                        varBuscaAux = varBusca.Split(' ');
                        foreach (string palavra2 in varBuscaAux)
                        {
                            if (!palavra2.Contains("and"))
                            Console.WriteLine("Palavra2: "+palavra2);
                            {   
                                StringDeBusca.Add(palavra2);                                
                            }                                                       
                        }
                        i=0;
                        varBuscaAux = vartexto.Split(' ');
                        for (i = 0; i < StringDeBusca.Count; i++)
                        {
                            foreach (string palavra3 in varBuscaAux)
                            {
                                //debug - Console.WriteLine("Palavra3: "+palavra3+"\n");
                                if (palavra3.Equals(StringDeBusca[i]))                                                                                                                            
                                {    
                                    //debug - Console.WriteLine("String de Busca: "+StringDeBusca[i]+"\n");                                                                                            
                                    //debug - Console.WriteLine("Palavra3: "+palavra3+"\n");
                                    Console.WriteLine("VarOcorrencias: "+Objetos.varOcorrencias+"\n");

                                    resultado.Append($"{palavra}{"\n"}");                            
                                    Objetos.varOcorrencias++;                              
                                }   
                            }
                            Objetos.varTexto = resultado.ToString();                                  
                        }                                              
                        
                        break;

                        case 0:
                            System.Text.StringBuilder resultado2 = new System.Text.StringBuilder();                                
                            string[] linhas2 = vartexto.Split('\n');
                            foreach(string linha in linhas2)
                            {                        
                                if(linha.Contains(varBusca))
                                {                            
                                    resultado2.Append($"{linha}{"\n"}");                            
                                    Objetos.varOcorrencias++;                              
                                }   
                            }
                            Objetos.varTexto = resultado2.ToString();
                        break;                        
                    
                    }
                
                }                                
                                   
            }

             #region SalvaPesquisaEmArquivo
             public void SalvaEmArquivo(string varLog)
             //salva o log preparado em arquivo
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
             //carrega o arquivo de log
             {
                 string log = "a";
                 if (System.IO.File.Exists(arquivoDeLog))
                 {
                     log = File.ReadAllText(arquivoDeLog);                                     
                 }
                 return log;
             }

             public int CalculaQuantasPesquisas(string varLog)
             // Calcula quantas pesquisas existem no arquivo de log
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
             public string PreparaLog(String varLog, String varDocName, String varBuscaRefinada, int varContadorDeOcorrências)
             {
                 var texto = new System.Text.StringBuilder();
                 string log = CarregaArquivoDeLog();
                 int pesquisas = CalculaQuantasPesquisas(log);
                 texto.Append("****************************\n");
                 texto.Append("Numero da consulta: "+pesquisas+"\n");
                 texto.Append("Nome do documento: "+varDocName+"\n");
                 texto.Append("String de busca: "+varBuscaRefinada+"\n");
                 texto.Append("Ocorrências: "+varContadorDeOcorrências+"\n");
                 texto.Append("****************************\n");

                 varLog = texto.ToString();
                 return varLog;
             }
             #endregion

           


    }
}