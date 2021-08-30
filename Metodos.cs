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

            public void CarregaPDF(string vardocname)
            {
                using (PdfReader reader = new PdfReader (@"C:\Temp\"+vardocname))
                {
                    //var textoDoPdf = new System.Text.StringBuilder();
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        string aux = PdfTextExtractor.GetTextFromPage(reader, i);
                        string[] linhasDoPdf = aux.Split('\n');
                        foreach(string linha in linhasDoPdf)
                        {

                        }
                        
                    }
                    
                }

            }
    
            #endregion
            #region Pesquisa1Palavra
            
            #endregion
    }




}