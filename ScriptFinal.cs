using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SGC.GeradorScriptVersao
{
    public class ScriptFinal
    {
        public string Ticket { get; set; }
        public string NomeFuncionario { get; set; }
        public IList<Arquivo> Arquivos { get; set; }
        public string TextoArquivo { get; set; }

        public ScriptFinal()
        {
            this.TextoArquivo = "";
        }

        public void EscreveScript()
        {
            this.TextoArquivo += "---------------------------------------------------------------------------------------------------------------------------------\n";
            this.TextoArquivo += $"-- INÍCIO - TK {this.Ticket} - {this.NomeFuncionario} \n";
            this.TextoArquivo += "---------------------------------------------------------------------------------------------------------------------------------\n";
            this.TextoArquivo += "\n";

            foreach (var arquivo in this.Arquivos)
            {
                this.TextoArquivo += $"{arquivo.TextoArquivo}\nGO \n";
            }

            this.TextoArquivo += "\n";
            this.TextoArquivo += "---------------------------------------------------------------------------------------------------------------------------------\n";
            this.TextoArquivo += $"-- FIM - TK {this.Ticket} - {this.NomeFuncionario} \n";
            this.TextoArquivo += "---------------------------------------------------------------------------------------------------------------------------------\n";
        }

        public void GerarScritpFinal()
        {
            EscreveScript();

            //Encoding.GetEncoding("iso-8859-1")

            //Path.Combine("C:\\GeradorScriptVersao\\", "script para versão.sql")
            //StreamWriter writer = new StreamWriter();

            using (StreamWriter writer = new StreamWriter(Path.Combine("C:\\GeradorScriptVersao\\", "script para versão.sql"), true, Encoding.GetEncoding("iso-8859-1")))
            {
                writer.WriteLine(this.TextoArquivo);
            }
        }
    }
}
