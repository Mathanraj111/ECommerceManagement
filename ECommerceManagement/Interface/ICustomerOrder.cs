using ECommerceManagement.Models;

namespace ECommerceManagement.Interface
{
    public interface ICustomerOrder
    {
        public JsonResponse GetCustomerOrderDetails(UserInputParam user);
    }
}
