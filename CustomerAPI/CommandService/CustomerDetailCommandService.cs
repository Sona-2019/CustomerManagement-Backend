using CustomerAPI.CustomerDTO;
using CustomerAPI.Database.Context;
using CustomerAPI.Database.Models;
using CustomerAPI.Helper;
using CustomerAPI.Wrapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.CommandService
{
    public class CustomerDetailCommandService : ICustomerDetailCommandService
    {
        #region :::local Variables:::
        private readonly CustomerDetailContext context;
      

        #endregion
        #region :::Constructor:::
        public CustomerDetailCommandService(CustomerDetailContext dataContext)
        {
            this.context = dataContext;
          
        }
        #endregion
        public async Task<int> InsertCustomerDetails(CustomerDetailsDTO NewCustomer, AppSettings appSettings)
        {
            int status = 0;

            try
            {
                int cutomerExist = await context.CustomerDetails.Where(C => C.EmailID == NewCustomer.emailID).CountAsync();
                if (cutomerExist > 0) return status = 1;
                else
                {
                    CustomerDetails newCustomer = new CustomerDetails();
                    newCustomer.Username = NewCustomer.userName;
                    newCustomer.Password = PasswordEncryption.Encrypt(NewCustomer.password,appSettings.EncryptionKey);
                    newCustomer.FirstName = NewCustomer.firstName;
                    newCustomer.LastName = NewCustomer.lastName;
                    newCustomer.Address = NewCustomer.address;
                    newCustomer.EmailID = NewCustomer.emailID;
                    newCustomer.PhoneNumber = NewCustomer.phoneNumber;

                    //save
                    await context.AddAsync<CustomerDetails>(newCustomer);
                    context.SaveChanges();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return await Task.FromResult(status);
        }

       
    }
}
