using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Data.Entities;
using System.Drawing;
using System.IO.Compression;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace MoneyPlus.Services.EmailService;

public class EmailService : IEmailService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;

    public string Username { get; private set; }
    public string Password { get; private set; }

    public EmailService(ApplicationDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }



    public void SendEmail(List<string> ids)
    {

        foreach (string id in ids)
        {
            if(CurrentEquityValue(id).Count > 0)
            {
                var message = PrepareteMessage(id);
                SendEmailBySmtp(message);
            }
            
        }

    }


    private MailMessage PrepareteMessage(string id)
    {
        var email = CurrentEquityValue(id).FirstOrDefault().Email;

        var mail = new MailMessage();
        mail.From = new MailAddress(_config.GetValue<string>("SMTP:UserName"));

        mail.To.Add(email);

        mail.Subject = "Resumo Patrimonial";
        mail.Body = GetBody(id);
        mail.IsBodyHtml = true;

        return mail;
    }

    private bool ValidateEmail(string email)
    {
        Regex expression = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

        if (expression.IsMatch(email))
        {
            return true;
        }
        return false;
    }

    private void SendEmailBySmtp(MailMessage message)
    {
        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Host = _config.GetValue<string>("SMTP:Host");
        smtpClient.Port = _config.GetValue<int>("SMTP:Port");
        smtpClient.EnableSsl = true;
        smtpClient.Timeout = 10000;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(_config.GetValue<string>("SMTP:UserName"), _config.GetValue<string>("SMTP:Password"));
        smtpClient.Send(message);
        smtpClient.Dispose();
    }


    public List<AccountClient> CurrentEquityValue(string id)
    {
        var account = _context.Accounts.Include(a => a.User)
            .Where(a => a.UserId == id).Select(x => new AccountClient
            {
                Email = x.User.Email,
                Value = x.Balance.ToString(),
                DescriptionAccount = x.Description
            }).ToList();

        return account;

    }

    public string GetBody(string id)
    {
        var values = CurrentEquityValue(id);

        var resultvalues = "";

        foreach (var value in values)
        {
            resultvalues += $"<b>{value.DescriptionAccount} => $ {value.Value}</b><br/>\n";
        }

        var bodyEmail = $"Olá, bom dia !!!<br/>\r\nA Money Plus deseja à você um dia extraordinário." +
            $"<br/>\r\nSegue o seu Resumo Patrimonial de hoje.<br/><br/>\r\n {resultvalues}<br/>\r\nObrigado por utilizar os serviços da Money Plus Finance !!!" +
            $"<br/>\r\n\r\nAtt,<br/>\r\n\r\nEquipe de Reports Money Plus Finance<br/>\r\nMoney Plus Lda";

        return bodyEmail;
    }

    public class AccountClient
    {
        public string Email { get; set; }
        public string Value { get; set; }
        public string DescriptionAccount { get; set; }

    }



}
