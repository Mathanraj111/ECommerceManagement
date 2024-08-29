using ECommerceManagement.Interface;
using ECommerceManagement.Interface.Service;
using ECommerceManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ECommerceManagement.Controllers
{
    [ApiController]
    [Route("ECommerce")]
    public class ECommerceController : Controller
    {
        private readonly ICustomerOrder _customerOrder;
        public ECommerceController(ICustomerOrder customerOrder)
        {
            _customerOrder = customerOrder; 
        }
        [HttpPost(Name = "GetCustomerOrderDetails")]
        public IActionResult GetCustomerOrderDetails([FromBody] UserInputParam user)
        {
            JsonResponse response = new JsonResponse();
            response= _customerOrder.GetCustomerOrderDetails(user);
            return Json(response);
        }
    }
}
