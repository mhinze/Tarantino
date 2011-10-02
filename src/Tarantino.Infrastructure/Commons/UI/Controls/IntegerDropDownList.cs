using System.Web.UI.WebControls;

namespace Tarantino.Infrastructure.Commons.UI.Controls
{
	public class IntegerDropDownList : DropDownList
	{
		public void BindData(params int[] values)
		{
			Items.Clear();
			foreach (int value in values)
			{
				Items.Add(value.ToString());
			}
		}

		public int GetSelectedValue()
		{
			int selectedValue = int.Parse(SelectedValue);
			return selectedValue;
		}

		public void SelectItem(int value)
		{
			SelectedValue = value.ToString();
		}
	}
}