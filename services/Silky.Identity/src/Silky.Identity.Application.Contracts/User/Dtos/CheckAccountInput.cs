using System.ComponentModel.DataAnnotations;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Application.Contracts.User.Dtos;

public class CheckAccountInput
{
    [Required(ErrorMessage = "账号不允许为空")]
    public string Account { get; set; }

    public AccountType AccountType { get; set; }
}