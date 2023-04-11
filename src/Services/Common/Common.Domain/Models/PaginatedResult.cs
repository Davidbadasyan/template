namespace Common.Domain.Models;

public class PaginatedResult<T>
{
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }
    public int TotalPages { get; private set; }
    public int TotalRecords { get; private set; }
    public IEnumerable<T> Data { get; private set; }

    public PaginatedResult(
        int pageNumber,
        int pageSize,
        int totalRecords,
        IEnumerable<T> data)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        Data = data;
        TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
    }
}