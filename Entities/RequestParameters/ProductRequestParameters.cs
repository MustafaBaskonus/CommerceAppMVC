namespace Entities.RequestParameters
{
    public class ProductRequestParameters : RequestParameters
    {
        public int? CategoryId { get; set; }
        public int? MinPrice { get; set; } = 0;
        public int? MaxPrice { get; set; } = int.MaxValue;
        public bool IsValid => MaxPrice >= MinPrice;
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public ProductRequestParameters() : this(6,1)
        {

        }

        public ProductRequestParameters(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}