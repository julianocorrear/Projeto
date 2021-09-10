using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Data;
using System.Data.SQLite;


namespace Classes
{
    public unsafe class Metodos
    {
        public string arquivoDeLog = @"C:\Temp\LogDePesquisa.txt";
        private static SQLiteConnection sqliteConnection;          
                    
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
            public void DefineStringDePesquisa()
            // define a string que será usada
            {
                Console.WriteLine("Qual expressão deseja pesquisar? (Utilize AND ou OR como operadores lógicos)");
                Objetos.varBusca = (Console.ReadLine()).ToLower();                               
            }           
            public void Pesquisa(string vartexto,string varBusca)
            // executa a string de pesquisa no texto do pdf
            {
                int isAndOK = 0;
                Objetos.varOcorrencias = 0;                 
                var StringDeBusca = new List<string>();
                System.Text.StringBuilder resultado = new System.Text.StringBuilder();
                String[] varBuscaAux = varBusca.Split(' ');
                int validacao = 0; 
                int i = 0;               

                //debug - Console.WriteLine("Texto: "+vartexto);            
                foreach(string palavra in varBuscaAux)
                {
                    switch (palavra)
                    {
                        case "or":
                        validacao = 1;
                        break;

                        case "and":
                        validacao = 2;
                        break;                                       
                    }
                    
                    switch (validacao)                    
                    {
                        //caso a string possua 'OR'
                        case 1:                        
                        varBuscaAux = varBusca.Split(' ');
                        foreach (string palavra2 in varBuscaAux)
                        {
                            if (!palavra2.Contains("or"))
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
                                if (palavra3.Equals(StringDeBusca[i]))                                                                                                                            
                                {   
                                    resultado.Append($"{palavra}{"\n"}");                            
                                    Objetos.varOcorrencias++;                              
                                }   
                            }
                            Objetos.varTexto = resultado.ToString();                                  
                        }                                                                      
                        break;

                        case 2:
                        var isAndOKList = new List<int>();                        
                        varBuscaAux = varBusca.Split(' ');
                        foreach (string palavra2 in varBuscaAux)
                        {
                            if (!palavra2.Contains("and"))
                            //Console.WriteLine("Palavra2: "+palavra2);
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
                                if (palavra3.Equals(StringDeBusca[i]))                                                                                                                                                            
                                {   
                                    //Console.WriteLine("String de Busca: "+StringDeBusca[i]);                                    
                                    isAndOKList.Add(1);
                                    resultado.Append($"{palavra}{"\n"}");                                                                                        
                                }
                                else
                                {
                                    isAndOKList.Add(0);
                                }                                                            
                            }
                            i=0;
                            for (i = 0; i < isAndOKList.Count; i++)
                            {
                                if (isAndOKList[i] == 1)
                                {
                                    isAndOK = 1;
                                }
                                else
                                {
                                    isAndOK = 0;
                                }                                   
                            }                                                                     
                        }
                        if (isAndOK == 1)                            
                        {
                            for (i = 0; i < isAndOKList.Count;i++)
                            {
                                if (isAndOKList[i] == 1)
                                {
                                    Objetos.varOcorrencias++;
                                }   
                            }
                            Objetos.varTexto = resultado.ToString();
                        }
                        else
                        {
                            Console.WriteLine("\nPesquisa AND não retornou nenhum resultado!\n");
                        }                                 
                        break;

                        case 0:

                            //pesquisa sem operador
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
             public void ConexaoDB(string path)
             {               
                 sqliteConnection = new SQLiteConnection(path);
                 sqliteConnection.Open();
             }
             public void CriarDatabase(string nome)
             {
                 Console.WriteLine(nome+"\n");
                 Console.ReadKey();
                 SQLiteConnection.CreateFile(nome);
                 Console.WriteLine("DB Criado \n");
                 Console.ReadKey();
             }
             public void CriarTabelaNoDB()
             {
                 using (var cmd = sqliteConnection.CreateCommand())
                 {
                 cmd.CommandText = "CREATE TABLE IF NOT EXISTS DadosDaPesquisa(NomeDoArquivo VarChar(30), PalavraBuscada VarChar(30), NumeroRepeticoes int)";
                 cmd.ExecuteNonQuery();
                 }
             }
             public void GravarNoDB(Arquivo arquivo)
             {
                 using (var cmd = sqliteConnection.CreateCommand())
                 {
                      cmd.CommandText = "INSERT INTO DadosDaPesquisa(NomeDoArquivo, PalavraBuscada, NumeroRepeticoes ) values (@NomeArquivo, @PalavraBuscada, @NumeroRepeticoes)";
                      cmd.Parameters.AddWithValue("@NomeArquivo",arquivo.NomeArquivo);
                      cmd.Parameters.AddWithValue("@PalavraBuscada", arquivo.PalavraBuscada);
                      cmd.Parameters.AddWithValue("@NumeroRepeticoes", arquivo.NumeroRepeticoes);
                      cmd.ExecuteNonQuery();
                 }
             }
             public void LeituraDoBD()
             {
                 using (var cmd = new System.Data.SQLite.SQLiteCommand(sqliteConnection))
                 {
                     List<Arquivo> listaArquivos = new List<Arquivo>();
                     Arquivo arquivo;
                     cmd.CommandText = "SELECT * FROM DadosDaPesquisa";

                     Console.WriteLine("Dados do Banco:");
                     using (var reader = cmd.ExecuteReader())
                     {
                          while (reader.Read())
                          {
                               arquivo = new Arquivo(reader["PalavraBuscada"].ToString(), reader["NomeArquivo"].ToString(), Convert.ToInt32(reader["NumeroRepeticoes"].ToString()));
                               listaArquivos.Add(arquivo);
                          }
                          Console.WriteLine("Informações dos Arquivos na Banco:\n");
                          Console.WriteLine("########################################");
                          foreach(Arquivo arq in listaArquivos)
                          {                        
                                Console.WriteLine($"Palavra Buscada: {arq.PalavraBuscada}");
                                Console.WriteLine($"Nome do Arquivo: {arq.NomeArquivo}");
                                Console.WriteLine($"Numero de linhas em que a palavra aparece: {arq.NumeroRepeticoes}");
                                Console.WriteLine("########################################");
                          }
                     }
                 }
             }

    }
}