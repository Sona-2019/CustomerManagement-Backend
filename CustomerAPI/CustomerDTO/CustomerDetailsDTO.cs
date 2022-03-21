using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.CustomerDTO
{
    public class CustomerDetailsDTO
    {
        public string userName { get; set; }

        public string password { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string address { get; set; }

        public string emailID { get; set; }

        public string phoneNumber { get; set; }
    }
}
