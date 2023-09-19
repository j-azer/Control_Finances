using Microsoft.AspNetCore.Authorization;
using MoneyPlus.Enums;
using System.ComponentModel.DataAnnotations;

namespace MoneyPlus.Data.Entities;


public class Typing
{
    public int Id { get; set; }


    [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
    public string Type { get; set; }
}