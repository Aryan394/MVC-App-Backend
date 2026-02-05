namespace API.Helpers;

public class PagingParams
{
    private const int MaxPageSize = 100;
    private const int DefaultPageSize = 10;

    public int PageNumber { get; set; } = 1;

    private int _pageSize = DefaultPageSize;

    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value <= 0)
                _pageSize = DefaultPageSize;
            else if (value > MaxPageSize)
                _pageSize = MaxPageSize;
            else
                _pageSize = value;
        }
    }
}
