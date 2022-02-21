namespace Silky.Saas.Application.Contracts.Edition.Dtos;

public class GetEditionFeatureOutput
{
    public long FeatureId { get; set; }

    public string Code { get; set; }

    public int FeatureValue { get; set; }
}