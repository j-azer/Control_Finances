using Microsoft.AspNetCore.Authorization;
using NuGet.ContentModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net.NetworkInformation;

namespace MoneyPlus.Data.Entities;


public class Transfer
{
    private readonly ApplicationDbContext _context;

    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
    [DataType(DataType.Date, ErrorMessage = "O campo {0} deve conter uma data válida.")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
    [DataType(DataType.Currency)]
    public double Amount { get; set; }
    
    public Typing Typing { get; set; }


    [DisplayName("Type")]
    public int TypingId { get; set; }

    public Account Account { get; set; }

    [DisplayName("Account Type")]
    public int AccountId { get; set; }

    public Subcategory Subcategory { get; set; }

    [DisplayName("Category")]
    public int SubcategoryId { get; set; }

    public Payee Payee { get; set; }

    [DisplayName("Payee")]
    public int PayeeId { get; set; }

    public Active Active { get; set; }

    [DisplayName("Name Asset - Optional")]
    public int ActiveId { get; set; }

    public string UserId { get; internal set; }


    [DisplayName("Destination Acc")]
    public int AccountIdReceived { get; set; }



    public Transfer()
    {
    }

    public Transfer(ApplicationDbContext context)
    {
        _context = context;
    }

    public Transfer(double amount, int id)
    {
        Amount = amount;
        Id = id;
    }

}