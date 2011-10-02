namespace Tarantino.WebManagement.Handlers
{
	/// <summary>
	/// Summary description for DisableSSL.
	/// </summary>
	public class DisableSSL : HandlerBase
	{
		protected override void OnProcessRequest()
		{
			WriteCSS();
			WriteMenu();

			if (_context.Request["bypass"] != null)
			{
				if (_context.Request["bypass"].ToLower().Equals("true"))
				{
					if (_context.Response.Cookies["bypassssl"] != null)
					{
						_context.Response.Cookies.Remove("bypassssl");
					}
					_context.Response.Cookies.Add(new System.Web.HttpCookie("bypassssl"));
					_context.Response.Cookies["bypassssl"].Value = "true";
				}
				else
				{
					_context.Response.Cookies["bypassssl"].Value = "false";
				}
			}
			if (_context.Response.Cookies["bypassssl"] != null && _context.Response.Cookies["bypassssl"].Value != null && _context.Response.Cookies["bypassssl"].Value.ToLower() == "true")
			{
				Write("SSL Disabled</br>");
			}
			else
			{
				Write("SSL Enabled</br>");
			}
			Write("<a href='?bypass=true'>Bypass</a></br>");
			Write("<a href='?bypass=false'>Enable</a>");
		}
	}
}
