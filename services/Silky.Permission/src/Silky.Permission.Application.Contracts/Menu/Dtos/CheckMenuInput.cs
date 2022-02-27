using System.ComponentModel.DataAnnotations;

namespace Silky.Permission.Application.Contracts.Menu.Dtos;

public class CheckMenuInput
{
    public long? Id { get; set; }

    public long? ParentId { get; set; }

    [Required(ErrorMessage = "菜单名称不允许为空")]
    public string Name { get; set; }
}