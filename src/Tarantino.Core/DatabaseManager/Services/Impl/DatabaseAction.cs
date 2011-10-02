using Tarantino.Core.Commons.Model.Enumerations;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	namespace Tarantino.Core.DatabaseManager.Services.Impl
	{
		public class DatabaseAction : Enumeration
		{
			public static readonly DatabaseAction Create = new DatabaseAction(1, "Create");
			public static readonly DatabaseAction Update = new DatabaseAction(2, "Update");
			public static readonly DatabaseAction Drop = new DatabaseAction(3, "Drop");

			public DatabaseAction()
			{
			}

			public DatabaseAction(int value, string displayName) : base(value, displayName)
			{
			}
		}
	}	
}