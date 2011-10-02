using System;
using System.Collections.Generic;
using System.IO;

using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	
	public class FileExtensionChecker : IFileExtensionChecker
	{
		private readonly IConfigurationReader _configurationReader;
		private readonly IWebContext _context;

		public FileExtensionChecker(IConfigurationReader configurationReader, IWebContext context)
		{
			_configurationReader = configurationReader;
			_context = context;
		}

		public bool CurrentUrlCanBeRedirected()
		{
			string extension = Path.GetExtension(_context.GetCurrentUrl());
			IEnumerable<string> extensions = _configurationReader.GetStringArray("TarantinoWebManagementMaintenanceExtensions");

			List<string> extensionList = new List<string>(extensions);
			bool isExtensionIncluded = Array.IndexOf(extensionList.ToArray(), extension.ToLower()) >= 0;
			return isExtensionIncluded;
		}
	}
}