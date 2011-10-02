using System.Web.UI.WebControls;

namespace Tarantino.Infrastructure.Commons.UI.Controls
{
	public class DecimalDropDownList : DropDownList
	{
		public void BindData(params decimal[] values)
		{
			Items.Clear();
			foreach (decimal value in values)
			{
				Items.Add(value.ToString());
			}
		}

		public decimal GetSelectedValue()
		{
			decimal selectedValue = decimal.Parse(SelectedValue);
			return selectedValue;
		}

		public void SelectItem(decimal value)
		{
			foreach (ListItem item in Items)
			{
				if (decimal.Parse(item.Value) == value)
				{
					item.Selected = true;
					break;
				}
			}
		}
	}
}