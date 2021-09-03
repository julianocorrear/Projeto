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
            var Metodos1 = new Metodos();
                              
            
            string log = "a";          

            Console.WriteLine(@"A pasta padrão utilizada no momento é: C:\Temp\");
            Console.WriteLine("Digite o nome do PDF que será aberto:");
            Objetos.varDocName = Console.ReadLine();
            Objetos.varTextoCarregado = (Metodos1.CarregaPDF(Objetos.varDocName)).ToLower();
            Metodos1.DefineStringDePesquisa();            
            Metodos1.Pesquisa(Objetos.varTextoCarregado,Objetos.varBusca);
            Objetos.varLog = Metodos1.PreparaLog(log,Objetos.varDocName,Objetos.varBusca,Objetos.varOcorrencias);
            Metodos1.SalvaEmArquivo(Objetos.varLog);
        }
        #endregion

    }
}