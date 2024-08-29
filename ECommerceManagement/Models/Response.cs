namespace ECommerceManagement.Models
{
    public class JsonResponse
    {
        public string Message { get; set; }
        public Response Response { get; set; }
    }
    public class Response
    {
        public Customer Customer { get; set; }
        public Order Order { get; set; }
    }
    public class Customer
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class Order
    {
        public int OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string DeliveryAddress { get; set; }
        public List<ProductItems> productItems { get; set; }
        public string DeliveryExpected { get; set; }
    }
    public class ProductItems
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public int PriceEach { get; set; }
    }
}
