using System.ComponentModel.DataAnnotations;

namespace Silky.Organization.Application.Contracts.Organization.Dtos;

public class CheckOrganizationInput
{
    public long? ParentId { get; set; }

    [Required(ErrorMessage = "名称不允许为空")]
    public string Name { get; set; }
}