using System;

namespace Tarantino.Deployer.Core.Services.Impl
{
	public class VersionNumberParser : IVersionNumberParser
	{
		private const string _searchString = "Working Version Number: ";

		public int Parse(string output)
		{
			int position = output.IndexOf(_searchString) + _searchString.Length;

			if (position < _searchString.Length)
			{
				throw new ApplicationException("The term 'Working Version Number:' was not found in the build output.  Could not determine the version number or record deployment occurence!");
			}

			string versionNumberString = string.Empty;
			while (getChar(output, position) != " " && getChar(output, position) != "\n")
			{
				versionNumberString += output.Substring(position, 1);
				position++;
			}

			int versionNumber = int.Parse(versionNumberString);

			return versionNumber;
		}

		private string getChar(string output, int position)
		{
			return output.Substring(position, 1);
		}
	}
}