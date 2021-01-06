using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyIoC
{
	public class Container
	{
	     Dictionary<Type, Type> registeredTypes = new Dictionary<Type, Type>() ;

		public void AddAssembly(Assembly assembly)
		{
			Assembly currAssembly = Assembly.GetExecutingAssembly();

			var types = currAssembly.ExportedTypes;
			foreach (var type in types)
			{

				// types that have [Import] attribute
				if (type.GetProperties().Length >=1) {
					foreach (var r in type.GetProperties())
					{
						foreach (var att in r.GetCustomAttributes(typeof(ImportAttribute), true))
						{
							AddType(r.PropertyType);
						}
					}
				}
				// [ImportConstructor] attributes
				if (type.GetCustomAttributes(typeof(ImportConstructorAttribute), true).Length>=1) {
					var constructorImport = type.GetCustomAttributes(typeof(ImportConstructorAttribute), true);
					foreach (var ci in constructorImport)
					{
						var ctor = type.GetConstructors().Where(y => y.GetParameters().Length > 0).First();
						foreach (var par in ctor.GetParameters())
						{
							AddType(par.ParameterType);
						}

					}
                }
                // export attributes
				if(type.GetCustomAttributes(typeof(ExportAttribute), true).Length >= 1)
                {
				    foreach (var exportAttribute in type.GetCustomAttributes(typeof(ExportAttribute), true))
					{
 						AddType(type);
					}

				}		 
			}

		}

		public void AddType(Type type)
		{
			if (!registeredTypes.ContainsKey(type))
				registeredTypes.Add(type, type);
		}

		public void AddType(Type type, Type baseType)
		{
			if (!registeredTypes.ContainsKey(type))
				registeredTypes.Add(type,baseType);
		}

		public object CreateInstance(Type type)
		{

			if (!registeredTypes.ContainsKey(type))
			{
				Console.WriteLine($"Type {type.Name} is not registered. Cannot instantiate.");
				return null;
			} 

			return null; //(Type)Activator.CreateInstance(type); 
		}

		public T CreateInstance<T>()
		{
			return default(T);
		}


		public void Sample()
		{
			var container = new Container();
			container.AddAssembly(Assembly.GetExecutingAssembly());

			var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
			var customerBLL2 = container.CreateInstance<CustomerBLL>();

			container.AddType(typeof(CustomerBLL));
			container.AddType(typeof(Logger));
			container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));
		}
	}
}
