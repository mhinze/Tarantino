using System;
using System.Reflection;

namespace Tarantino.WebManagement.Handlers
{
	public class Assemblies : HandlerBase
	{
		protected override void OnProcessRequest()
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			Pair[] names = new Pair[assemblies.Length];
			int count = 0;
			foreach (Assembly a in assemblies)
			{
				AssemblyName b = a.GetName();
				names[count++] = new Pair(b.Name, b.Version.ToString());
			}

			Array.Sort(names);
			System.Text.StringBuilder response = new System.Text.StringBuilder(1000);
			response.Append("<table>\r");
			foreach (Pair pair in names)
			{
				response.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>\r", pair.Name, pair.Version);
			}
			response.Append("</table>\r");
			WriteCSS();
			WriteMenu();
			Write(response.ToString());
		}

		private class Pair : IComparable
		{
			private string _name;
			private string _version;

			public Pair(string Name, string Version)
			{
				_name = Name;
				_version = Version;
			}

			public override string ToString()
			{
				return _name;
			}

			public int CompareTo(object obj)
			{
				return _name.CompareTo(((Pair)obj)._name);
			}

			public string Name
			{
				get { return _name; }
			}

			public string Version
			{
				get { return _version; }
			}
		}
	}
}