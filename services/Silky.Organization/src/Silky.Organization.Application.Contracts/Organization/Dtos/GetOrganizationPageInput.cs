using Silky.Hero.Common.Enums;

namespace Silky.Organization.Application.Contracts.Organization.Dtos;

public class GetOrganizationPageInput
{
    public int PageSize { get; set; } = 10;

    public int PageIndex { get; set; } = 1;

    public long? Id { get; set; }

    public string Name { get; set; }

    public Status? Status { get; set; }
}