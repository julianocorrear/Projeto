using System;
using System.IO;

namespace Classes
{
    public class Interface
    {
        
        #region Menus
        public void MenuInicial()
        {
        Console.WriteLine("\n===========================================\nProjeto de Mineração de dados - Atividade 1\n===========================================\n");
        Console.WriteLine("Pressione Qualquer Tecla para Continuar...");
        Console.ReadKey();
        Console.Clear();
        }

        public unsafe void MenuPrincipal()
        {
            Metodos Metodos1 = new Metodos();
            string log = "a";
            int debug = 0;
            if(debug == 0)          
            {
                //Console.WriteLine(@"A pasta padrão utilizada no momento é: C:\Temp\");
                //Console.WriteLine("Digite o nome do PDF que será aberto:");
                //Objetos.varDocName = Console.ReadLine();
                //Objetos.varTextoCarregado = (Metodos1.CarregaPDF(Objetos.varDocName)).ToLower();
                //Metodos1.DefineStringDePesquisa();            
                //Metodos1.Pesquisa(Objetos.varTextoCarregado,Objetos.varBusca);
                //Objetos.varLog = Metodos1.PreparaLog(log,Objetos.varDocName,Objetos.varBusca,Objetos.varOcorrencias);
                //Metodos1.SalvaEmArquivo(Objetos.varLog);
                Metodos1.CapturaPastaDataset();
                Metodos1.CapturaPalavrasRanking();
                
            }
            else
            {
                Arquivo arquivo = new Arquivo("Teste");
                string db="Data Source= C:\\Temp\\dados2.db";
                Console.WriteLine("Debugando app....");
                Console.WriteLine("Criando arquivo....");                
                Metodos1.CriarDatabase("C:\\Temp\\dados2.db");
                Metodos1.ConexaoDB(db);
                Console.WriteLine("Criando tabela....");
                Metodos1.CriarTabelaNoDB();
                Console.WriteLine("Gravando Texto de exemplo....");
                Metodos1.GravarNoDB(arquivo);
                Metodos1.LeituraDoBD();
            }
        }
        #endregion

        
    }
}