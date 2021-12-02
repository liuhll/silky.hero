namespace Silky.Hero.Common.Dtos;

public abstract class PageDtoBase
{
    protected PageDtoBase()
    {
        PageIndex = 1;
        PageSize = 20;
    }

    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// 第几页
    /// </summary>
    public int PageIndex { get; set; }
}