using System;

namespace Tarantino.Core.Commons.Services.Environment.Impl
{
	public class MachineConsole : IMachineConsole
	{
		public void WriteLine(string text)
		{
			Console.WriteLine(text);
		}
	}
}