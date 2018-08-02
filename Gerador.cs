using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace SGC.GeradorScriptVersao
{
    public partial class Gerador : Form
    {
        private ScriptFinal scriptFinal;
        public Gerador()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MensagemBoasVindas();
        }

        private void MensagemBoasVindas()
        {
            string texto = "Seja bem vindo ao gerador de script para versão. \n" +
                           "Favor criar uma pasta no diretório C: chamada GeradorScriptVersao (C:\\GeradorScriptVersao\\). \n" +
                           "Após a criação, insira todos os arquivos SQL neste diretório. \n" +
                           "O script criado se chamará 'script para versão.sql' \n" +
                           "Favor certificar-se que não exista nenhum arquivo com este nome na pasta.";

            MessageBox.Show(texto, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private IList<Arquivo> LerArquivos()
        {
            var listaArquivo = new List<Arquivo>();
            DirectoryInfo diretorio = new DirectoryInfo(@"C:\GeradorScriptVersao\");
            // Busca automaticamente todos os arquivos em todos os subdiretórios
            FileInfo[] arquivos = diretorio.GetFiles("*", SearchOption.AllDirectories);

            foreach (FileInfo File in arquivos)
            {
                listaArquivo.Add(new Arquivo(File.FullName));
            }

            NaoExisteArquivo(listaArquivo);
            JaExisteScript(listaArquivo);

            return listaArquivo;
        }

       private void gerar_Click(object sender, EventArgs e)
        {
            gerar.Enabled = false;
            string texto;
            try
            {
                if (string.IsNullOrEmpty(nome.Text.ToString()))
                {
                    throw new Exception("O nome do desenvolvedor e obrigatório.");
                }
                else
                {
                    scriptFinal = new ScriptFinal()
                    {
                        NomeFuncionario = nome.Text.ToString().ToUpper(),
                        Ticket = (!string.IsNullOrEmpty(ticket.Text.ToString()) ? ticket.Text.ToString() : "XXXXX"),
                        Arquivos = LerArquivos()
                    };

                    scriptFinal.GerarScritpFinal();

                    MessageBox.Show("Arquivo de script gerado com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    gerar.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                texto = "Ocorreu um erro na aplicação: \n" + exception.Message;
                MessageBox.Show(texto, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gerar.Enabled = true;
            }
                
        }

        private void JaExisteScript(IList<Arquivo> listaArquivo)
        {
            if (listaArquivo.Any(a => a.NomeArquivo.Contains("script para versão.sql")))
            {
                throw new Exception("Já existe um script de versão na pasta C:\\GeradorScriptVersao\\.\nFavor removê-lo antes de gerar um novo script.");
            }
        }

        private void NaoExisteArquivo(IList<Arquivo> listaArquivo)
        {
            if (listaArquivo.Count <= 0)
            {
                throw new Exception("Não existem arquivos na pasta C:\\GeradorScriptVersao\\");
            }
        }
    }
}
