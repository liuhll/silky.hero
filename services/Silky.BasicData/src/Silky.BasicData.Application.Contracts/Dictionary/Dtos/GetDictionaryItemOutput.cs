namespace Silky.BasicData.Application.Contracts.Dictionary.Dtos;

public class GetDictionaryItemOutput : CreateDictionaryItemInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
}