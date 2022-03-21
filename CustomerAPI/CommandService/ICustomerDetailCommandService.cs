using CustomerAPI.CustomerDTO;
using CustomerAPI.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.CommandService
{
    public interface ICustomerDetailCommandService
    {
        Task<int> InsertCustomerDetails(CustomerDetailsDTO NewCustomer, AppSettings appSettings);
    }
}
