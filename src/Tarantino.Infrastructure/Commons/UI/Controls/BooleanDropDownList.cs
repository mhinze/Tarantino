using System.Web.UI.WebControls;

namespace Tarantino.Infrastructure.Commons.UI.Controls
{
	public class BooleanDropDownList : DropDownList
	{
		public void BindData()
		{
			Items.Clear();
			Items.Add(new ListItem("Yes", true.ToString()));
			Items.Add(new ListItem("No", false.ToString()));
		}

		public bool GetSelectedValue()
		{
			bool selectedValue = bool.Parse(SelectedValue);
			return selectedValue;
		}

		public void SelectItem(bool value)
		{
			SelectedValue = value.ToString();
		}
	}
}