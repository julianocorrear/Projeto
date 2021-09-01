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

        public void MenuPrincipal()
        {
            var Metodos1 = new Metodos();
            string docname;
            string TextoCarregado;
            string busca;
            string buscarefinada;
            string log = "a";
            Console.WriteLine(@"A pasta padrão utilizada no momento é: C:\Temp\");
            Console.WriteLine("Digite o nome do PDF que será aberto:");
            docname = Console.ReadLine();
            TextoCarregado = Metodos1.CarregaPDF(docname);
            busca = Metodos1.DefineStringDePesquisa();
            buscarefinada = Metodos1.Pesquisa(TextoCarregado,busca);
            log = Metodos1.PreparaLog(log,docname,busca);
            Metodos1.SalvaEmArquivo(log);
        }
        #endregion

    }
}