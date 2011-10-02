using StructureMap;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services;

namespace Tarantino.WebManagement
{
	public class CurrentContext
	{
		public static ApplicationInstance CurrentApplicationInstance
		{
			get
			{
				IApplicationInstanceContext context = ObjectFactory.GetInstance<IApplicationInstanceContext>();
				return context.GetCurrent();
			}
		}
	}
}