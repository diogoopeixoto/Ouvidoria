using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace OuvidoriaAPI.Service
{
    public class EmailService
    {
        private static string _servidor;
        private static int _porta;
        private static string _usuario;
        private static string _senha;
        private static bool _ssl;
        public bool success = false;

        public static void Configure(string servidor,
            bool ssl,
            int porta,
            string usuario,
            string senha
            )
        {
            _ssl = ssl;
            _servidor = servidor;
            _porta = porta;
            _usuario = usuario;
            _senha = senha;
        }

        public bool EnviaEmail(string emailDestinatario,
            string assunto, string body, string[] arquivosAnexos = null)
        {
            if (string.IsNullOrEmpty(_servidor)) return false;
            if (string.IsNullOrEmpty(_usuario)) return false;
            if (string.IsNullOrEmpty(_servidor)) return false;
            if (_porta == 0) return false;

            var client = new System.Net.Mail.SmtpClient();

            client.Host = (_servidor);
            client.Port = _porta;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = _ssl;
            client.Credentials = new NetworkCredential(_usuario,
                 _senha);

            string nomeRem = _usuario.Substring(0,
                 _usuario.IndexOf("@"));
            string nomeDest = emailDestinatario.Substring(0,
                emailDestinatario.IndexOf("@"));

            MailAddress remetente = new MailAddress(_usuario, nomeRem);
            MailAddress destinatario = new MailAddress(emailDestinatario,
                nomeDest);

            var mail = new MailMessage(remetente, destinatario);

            mail.Subject = assunto;
            mail.Body = body;
            mail.IsBodyHtml = false;

            /*
            if (arquivosAnexos != null)
                for (int i = 0; i < arquivosAnexos.Length; i++)
                    mail.Attachments.Add(new Attachment(arquivosAnexos[i]));
            */
            try
            {
                client.Send(mail);
                client.Dispose();

                return true;
                //   MessageBox.Show("Email enviado", "Mensagem", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += $"\n{ex.InnerException.Message}";
                Erro = msg;
                return false;
            }
        }

        //

        public string Erro { get; private set; }
    }
}