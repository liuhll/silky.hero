using Silky.EntityFrameworkCore.Extras.Entities;

namespace Silky.BasicData.Domain.Dictionary;

public class DictionaryItem : AuditedEntity
{
    public long DictionaryId { get; set; }
    
    public string Value { get; set; }

    public string Code { get; set; }

    public int Sort { get; set; }

    public string Remark { get; set; }

    public virtual DictionaryType DictionaryType { get; set; }

}