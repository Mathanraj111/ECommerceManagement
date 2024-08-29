using ECommerceManagement.Models;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace ECommerceManagement.Interface.Service
{
    public class CustomerOrder : ICustomerOrder
    {
        private readonly IConfiguration _configuration;
        public CustomerOrder(IConfiguration configuration)
        {
            _configuration = configuration;  
        }
        public JsonResponse GetCustomerOrderDetails(UserInputParam user)
        {
            JsonResponse res = new JsonResponse();
            Response response = new Response();
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:DatabaseConnection"]))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetCustomerOrderDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = user.User;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.VarChar).Value = user.CustomerId;
                        DataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(ds);
                    }
                }
                if (ds != null && ds.Tables.Count > 0)
                {
                    Customer customer = new Customer();
                    customer.FirstName = Convert.ToString(ds.Tables[0].Rows[0]["FIRSTNAME"]);
                    customer.LastName = Convert.ToString(ds.Tables[0].Rows[0]["LASTNAME"]);
                    response.Customer = customer;
                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        Order order = new Order();
                        order.OrderNumber = Convert.ToInt32(ds.Tables[1].Rows[0]["ORDERID"]);
                        order.OrderDate = Convert.ToString(ds.Tables[1].Rows[0]["ORDERDATE"]);
                        order.DeliveryAddress = Convert.ToString(ds.Tables[1].Rows[0]["OrderAddress"]);
                        order.DeliveryExpected = Convert.ToString(ds.Tables[1].Rows[0]["DELIVERYEXPECTED"]);
                        List<ProductItems> productList = new List<ProductItems>();
                        if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                            {
                                ProductItems productItem = new ProductItems();
                                productItem.Product = Convert.ToString(ds.Tables[2].Rows[i]["PRODUCTNAME"]);
                                productItem.Quantity = Convert.ToInt32(ds.Tables[2].Rows[i]["ProductCount"]);
                                productItem.PriceEach = Convert.ToInt32(ds.Tables[2].Rows[i]["PRICE"]);
                                productList.Add(productItem);
                            }
                            order.productItems = productList;
                        }
                        response.Order = order;
                    }
                    res.Message = "User  data available";
                    res.Response= response;
                }
                else
                {
                    res.Message = "User  data not available";
                }
            }
            catch (Exception ex)
            {
                res.Message ="Some error occured while processing your request";
            }
            return res;
        }
    }
}
