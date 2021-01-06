using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIoC
{
	[ImportConstructor]
	public class CustomerBLL
	{
		public ICustomerDAL CustomerDAL { get; private set; }
		public Logger logger { get; private set; }
		public CustomerBLL(ICustomerDAL dal, Logger logger)
		{
			this.CustomerDAL = dal;
			this.logger = logger;
		}
	}

	public class CustomerBLL2
	{
		[Import]
		public ICustomerDAL CustomerDAL { get; set; }
		[Import]
		public Logger logger { get; set; }
	}
}
