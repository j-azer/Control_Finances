using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace MoneyPlus.Data.Entities;


public class Payee
{
    public int Id { get; set; }

    [MaxLength(128)]
    [DisplayName("Payee")]
    [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
    public string Name { get; set; }


    [MaxLength(25)]
    public string NIF { get; set; }

    public string UserId { get; internal set; }
}