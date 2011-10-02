
using Tarantino.Core.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.Environment;

namespace Tarantino.Core.WebManagement.Services.Views.Impl
{
	
	public class PageView : IPageView
	{
		public const string PageTemplate = "Tarantino.Core.WebManagement.Services.Views.Resources.PageTemplate.html";
		public const string StylesheetTemplate = "Tarantino.Core.WebManagement.Services.Views.Resources.StylesheetTemplate.css";

		private readonly IResourceFileLocator _fileLocator;
		private readonly IMenuView _menuView;
		private readonly ITokenReplacer _replacer;

		public PageView(IResourceFileLocator fileLocator, IMenuView menuView, ITokenReplacer replacer)
		{
			_fileLocator = fileLocator;
			_menuView = menuView;
			_replacer = replacer;
		}

		public string BuildHtml(string bodyHtml)
		{
			string pageTemplate = _fileLocator.ReadTextFile("Tarantino.Core", PageTemplate);
			string cssHtml = _fileLocator.ReadTextFile("Tarantino.Core", StylesheetTemplate);
			string menuHtml = _menuView.BuildHtml();

			_replacer.Text = pageTemplate;

			_replacer.Replace("CSS", cssHtml);
			_replacer.Replace("MENU", menuHtml);
			_replacer.Replace("BODY", bodyHtml);

			return _replacer.Text;
		}
	}
}