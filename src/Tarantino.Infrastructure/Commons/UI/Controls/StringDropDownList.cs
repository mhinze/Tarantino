using System.Web.UI.WebControls;

namespace Tarantino.Infrastructure.Commons.UI.Controls
{
	public class StringDropDownList : DropDownList
	{
		public void BindData(params string[] values)
		{
			Items.Clear();
			foreach (string value in values)
			{
				Items.Add(value);
			}
		}

		public string GetSelectedValue()
		{
			string selectedValue = SelectedValue;
			return selectedValue;
		}

		public void SelectItem(string value)
		{
			SelectedValue = value;
		}
	}
}