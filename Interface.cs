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
            Console.WriteLine(@"A pasta padrão utilizada no momento é: C:\Temp\");
            Console.WriteLine("Digite o nome do PDF que será aberto:");
            docname = Console.ReadLine();
            Metodos1.CarregaPDF(docname);
        }
        #endregion

    }
}