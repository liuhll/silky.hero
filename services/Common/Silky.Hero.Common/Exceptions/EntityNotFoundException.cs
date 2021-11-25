using Silky.Core.Exceptions;

namespace Silky.Hero.Common.Exceptions;

public class EntityNotFoundException : SilkyException
{
    public object Id { get; set; }

    public Type EntityType { get; set; }


    public EntityNotFoundException()
    {
    }

    public EntityNotFoundException(Type entityType)
        : this(entityType, null, null)
    {
    }

    public EntityNotFoundException(Type entityType, object id)
        : this(entityType, id, null)
    {
    }


    public EntityNotFoundException(Type entityType, object id, Exception innerException)
        : base(
            id == null
                ? $"没有找到给定id的记录. 实体类型: {entityType.FullName}"
                : $"没有找到给定id的记录. 实体类型: {entityType.FullName}, id: {id}",
            innerException, StatusCode.BusinessError)
    {
        EntityType = entityType;
        Id = id;
    }
}