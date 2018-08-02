using System.IO;
using System.Text;

namespace SGC.GeradorScriptVersao
{
    public class Arquivo
    {
        public string NomeArquivo { get; set; }
        public string TextoArquivo { get; set; }

        public Arquivo(string nomeArquivo)
        {
            this.NomeArquivo = nomeArquivo;

            StreamReader reader = new StreamReader(Path.Combine("C:\\GeradorScriptVersao\\", nomeArquivo), Encoding.GetEncoding("iso-8859-1"));
            string texto = "";

            while (!reader.EndOfStream)
            {
                texto += $"{reader.ReadLine()} \n";
            }
            reader.Close();

            this.TextoArquivo = texto;
        }
    }
}
