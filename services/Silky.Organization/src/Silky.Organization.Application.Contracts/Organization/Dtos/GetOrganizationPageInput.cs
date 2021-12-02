using Silky.Hero.Common.Dtos;
using Silky.Hero.Common.Enums;

namespace Silky.Organization.Application.Contracts.Organization.Dtos;

public class GetOrganizationPageInput : PageDtoBase
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public Status? Status { get; set; }
}