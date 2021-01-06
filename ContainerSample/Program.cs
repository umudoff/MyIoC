using MyIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSample
{
    class Program
    {
        static void Main(string[] args)
        {
            MyIoC.Container container =new MyIoC.Container();

            container.AddType(typeof(MyIoC.CustomerBLL));
            container.AddType(typeof(MyIoC.Logger));
            container.AddType(typeof(MyIoC.CustomerDAL), typeof(MyIoC.ICustomerDAL));
            object cust =  container.CreateInstance(typeof(MyIoC.CustomerBLL));
            //container.AddAssembly(Assembly.LoadFrom(@"C:\Users\Amrah\source\repos\TaskMyIoC\Task_MyIoC\ContainerSample\bin\Debug\MyIoC.dll"));
            //object cust = container.CreateInstance(typeof(MyIoC.CustomerBLL));
            //var customerBLL = container.CreateInstance<CustomerBLL>();

            Console.ReadLine();
        }
    }

   
     

}
