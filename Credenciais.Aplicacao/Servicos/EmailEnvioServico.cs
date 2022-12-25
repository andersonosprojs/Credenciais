using Credenciais.Aplicacao.DTOs;
using Credenciais.Aplicacao.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace Credenciais.Aplicacao.Servicos
{
    public class EmailEnvioServico : IEmailEnvioServico
    {
        private readonly IUsuarioServico _usuarioServico;

        public EmailEnvioServico(IOptions<EmailSettings> emailSettings, IUsuarioServico usuarioServico)
        {
            _emailSettings = emailSettings.Value;
            _usuarioServico = usuarioServico;
        }            

        public EmailSettings _emailSettings { get; }

        public RetornoEnviarEmail EnviarEmailAsync(string email, string assunto, string mensagem)
        {
            try
            {
                var usuarioDTO = _usuarioServico.SelecionarPorLoginAsync(email).Result;
                if (usuarioDTO == null)
                    return new RetornoEnviarEmail() { Mensagem = "E-mail não localizado" };

                Execute(email, assunto, mensagem).Wait();
                return new RetornoEnviarEmail() { Mensagem = "E-mail enviado! Siga as instruções deste e-mail" };
            }
            catch (Exception ex)
            {
                return new RetornoEnviarEmail() { Mensagem = "Falha ao enviar e-mail" }; 
            }
        }

        public async Task Execute(string email, string subject, string message)
        {
            try
            {
                string? toEmail = string.IsNullOrEmpty(email) ? _emailSettings.ToEmail : email;

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "Credenciais")
                };

                mail.To.Add(new MailAddress(toEmail));
                //mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                mail.Subject = "Credenciais - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                //mail.Priority = MailPriority.High;

                //outras opções
                //mail.Attachments.Add(new Attachment(arquivo));
                //

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
