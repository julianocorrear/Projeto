using System;
using System.IO;

namespace Classes
{
    public class Arquivo
    {
        public int NumeroRepeticoes {get;}
        public string PalavraBuscada {get;}
        public string NomeArquivo {get;}

        public Arquivo(string pb, string path, int numeroRepeticoes)
        {            
            this.PalavraBuscada = pb;
            string[] aux = path.Split('\\');            
            this.NomeArquivo = aux[aux.Length-1];
            this.NumeroRepeticoes = numeroRepeticoes;           
        }   
        public Arquivo(string pb)
        {            
            this.PalavraBuscada = pb;                        
        }
    }
}