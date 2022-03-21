using CustomerAPI.CommandService;
using CustomerAPI.CustomerDTO;
using CustomerAPI.Wrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CustomerAPI.Controllers
{
    public class CustomerController : ControllerBase
    {
        #region :::variables:::
        ///<summary>
        ///private interface variables
        ///</summary>
        ///
        // private readonly ICustomerDetailQueryService queryService;
        private readonly ICustomerDetailCommandService commandService;
        private readonly ILogger<CustomerController> logger;

        //local variable pointing the appsettings in configuration.
        private readonly AppSettings appSettings;

        #endregion
        #region :::Constructor:::
        ///<summary>
        ///Constructor of CustomerDetails Controller 
        ///</summary>
        ///<param name="commands"></param>
        public CustomerController(ICustomerDetailCommandService commands, ILogger<CustomerController> loggerparam, IOptions<AppSettings> appSettingOptions)

        {
            //this.queryService = queries ?? throw new ArgumentNullException(nameof(queries));
            this.commandService = commands ?? throw new ArgumentNullException(nameof(commands));
            this.logger = loggerparam;
            this.appSettings = appSettingOptions.Value;

        }


        #endregion
        #region :::API:::

        /// <summary>
        /// API to insert New Customer Details
        /// </summary>
        /// <param name="customerDetails"></param>
        /// <returns></returns>
        [Route("api/Customer/RegisterCustomer")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(CustomerDetailsDTO), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<IActionResult> InsertCustomerDetails([FromBody] CustomerDetailsDTO customerDetails)
        {
            ResultToReturn<List<CustomerDetailsDTO>> customerData = new ResultToReturn<List<CustomerDetailsDTO>>();
            
            try
            {
                if (customerDetails == null)
                {

                    throw new Exception("CustomerDetails cannot be null");

                }
                int status = await commandService.InsertCustomerDetails(customerDetails,appSettings);
                if (status == 1)
                {
                    customerData.Status = ActionStatus.Failed;
                    customerData.Message = "The Customer already Exists";
                   
                }
                if (status == 0)
                {
                    customerData.Status = ActionStatus.Success;
                    customerData.Message = "New Customer is created Successfully";
                   
                }
            }
            catch (Exception exp)
            {
                customerData.Status = ActionStatus.Error;
                this.logger.LogError(exp, "Exception Caught");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Inserting Customer Details");
            }
            return Ok(customerData);
        }


    }

    #endregion

}

