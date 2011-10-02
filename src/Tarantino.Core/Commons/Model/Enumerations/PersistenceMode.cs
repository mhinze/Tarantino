namespace Tarantino.Core.Commons.Model.Enumerations
{
	public class PersistenceMode : Enumeration
	{
		public static readonly PersistenceMode Live = new PersistenceMode(1, "Live");
		public static readonly PersistenceMode Archive = new PersistenceMode(2, "Archive");

		public PersistenceMode()
		{
		}

		public PersistenceMode(int value, string displayName) : base(value, displayName)
		{
		}
	}
}