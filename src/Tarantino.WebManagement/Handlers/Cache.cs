namespace Tarantino.WebManagement.Handlers
{
	public class Cache : HandlerBase
	{
		public delegate void Refresh();

		public static event Refresh OnRefresh;

		protected override void OnProcessRequest()
		{
			if (_context.Request.Form.Count > 0)
			{
				foreach (string key in _context.Request.Form.Keys)
				{
					if (key == "custom")
					{
						FireOnRefresh();
					}
					else
					{
						foreach (string cacheKey in _context.Request.Form[key].Split(','))
						{
							_context.Cache.Remove(cacheKey);
						}
					}
				}
			}
			else if (_context.Request.QueryString.Count > 0)
			{
				string pattern = _context.Request.QueryString.Get("pattern");
				ClearHTTPContextCache(pattern);
				if (pattern.Length == 0 || pattern == ".*")
				{
					FireOnRefresh();
				}
			}
			
			WriteCSS();
			WriteMenu();
			
			Write(ListCacheEntries());
		}

		string ListCacheEntries()
		{
			System.Collections.ArrayList cacheKeys = new System.Collections.ArrayList();

			System.Collections.IDictionaryEnumerator enumerator = System.Web.HttpContext.Current.Cache.GetEnumerator();
			while (enumerator.MoveNext())
			{
				System.Collections.DictionaryEntry de = enumerator.Entry;
				cacheKeys.Add(de.Key);
			}
			cacheKeys.Sort();
			System.Collections.IEnumerator e = cacheKeys.GetEnumerator();
			System.Text.StringBuilder output = new System.Text.StringBuilder();
			output.Append("<table width=100%>\n");

			if (cacheKeys.Count > 0 || OnRefresh != null)
			{
				if (_authenticated)
				{
					output.AppendFormat("<tr><td><form method=post action=''>");
					output.AppendFormat("<tr><td><input type=submit value='Remove' />");
					output.AppendFormat("<input type=button value='Check all' onClick='value=check(form.cache)'>");
					output.AppendFormat("</td></tr>");

					output.Append("<SCRIPT LANGUAGE='JavaScript'><!-- \n");
					output.Append("var checkflag = 'false';function check(field) {if (checkflag == 'false') {  for (i = 0; i < field.length; i++) {  field[i].checked = true;}  checkflag = 'true';  return 'Uncheck all'; }else {  for (i = 0; i < field.length; i++) {  field[i].checked = false; }  checkflag = 'false';  return 'Check all'; }}\n");
					output.Append("//  End --></script>");
				}

				if (OnRefresh != null)
				{
					output.AppendFormat("<tr><td colspan=3>");
					if (_authenticated)
						output.AppendFormat("<input type=checkbox name='custom' value='true' />");
					output.AppendFormat(OnRefresh.GetInvocationList().Length + " Custom Cached Object(s)</td></tr>");
				}

				while (e.MoveNext())
				{
					output.AppendFormat("<tr>");
					int columns = 0;
					do
					{
						output.AppendFormat("<td width=33% nowrap>");
						if (_authenticated)
						{
							output.AppendFormat("<input type=checkbox name='cache' value='{0}' /> ", e.Current);
						}
						output.AppendFormat("{0}</td>", e.Current);
						columns++;
					} while (columns < 3 && e.MoveNext());
					output.AppendFormat("</tr>\r");
				}

				if (_authenticated)
				{
					output.AppendFormat("<tr><td><input type=submit value='Remove' />");
					output.AppendFormat("</form></td></tr>");
				}
			}
			else
			{
				output.Append("<tr><td>Cache is empty.</tr></td>\n");
			}
			output.Append("</table>\n");
			return output.ToString();
		}

		private static void FireOnRefresh()
		{
			if (OnRefresh != null)
			{
				OnRefresh();
			}
		}

		private void ClearHTTPContextCache(string pattern)
		{
			if (pattern == null || 0 == pattern.Length)
			{
				pattern = ".*";
			}

			System.Text.RegularExpressions.Regex selectionRegex = new System.Text.RegularExpressions.Regex(pattern);

			System.Collections.IDictionaryEnumerator enumerator = System.Web.HttpContext.Current.Cache.GetEnumerator();
			while (enumerator.MoveNext())
			{
				System.Collections.DictionaryEntry de = enumerator.Entry;
				string key = de.Key.ToString();
				if (selectionRegex.IsMatch(key))
				{
					System.Web.HttpContext.Current.Cache.Remove(key);
				}
			}
		}
	}
}