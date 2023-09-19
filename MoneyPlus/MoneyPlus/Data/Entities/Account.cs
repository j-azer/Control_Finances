using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Security.Cryptography.Xml;
using MoneyPlus.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using Humanizer;
using System.Diagnostics;

namespace MoneyPlus.Data.Entities;


public class Account
{
    private readonly ApplicationDbContext _context;

    public int Id { get; set; }


    [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
    [DisplayName("Account Type")]
    public string Type { get; set; }


    [MaxLength(128)]
    [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
    [DisplayName("Current Balance")]
    [DataType(DataType.Currency)]
    public double Balance { get; set; }


    public IdentityUser User { get; set; }

    public string UserId { get; set; }


    public Account()
    {
    }



    public Account(ApplicationDbContext context)
    {
        _context = context;
    }

    public Account(double balance, int id)
    {
        Balance = balance;
        Id = id;
    }



    public bool DepositMoney(double valor, int id)
    {

        var account = (from Account in _context.Accounts
                       where Account.Id == id
                       select Account).FirstOrDefault();

        if (account.Id != null)
        {
            account.Balance += valor;

            _context.SaveChanges();

            return true;
        }
        return false;

    }

    public bool TakeMoney(double valor, int id)
    {
        var account = (from Account in _context.Accounts
                       where Account.Id == id
                       select Account).FirstOrDefault();

        if (account.Id != null && account.Balance - valor >= 0)
        {
            account.Balance -= valor;

            _context.SaveChanges();

            return true;
        }
        return false;

    }


    public void TransferMoney(double valor, int takeId, int depositId)
    {
        var result = TakeMoney(valor, takeId);

        if (result == true)
        {
            DepositMoney(valor, depositId);
        }

    }
}
