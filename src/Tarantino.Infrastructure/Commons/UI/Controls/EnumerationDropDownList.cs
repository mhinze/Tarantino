using System.Collections.Generic;
using System.Web.UI.WebControls;
using Tarantino.Core.Commons.Model.Enumerations;

namespace Tarantino.Infrastructure.Commons.UI.Controls
{
	public class EnumerationDropDownList : DropDownList
	{
		public void SelectItem(Enumeration item)
		{
			if (item == null)
			{
				return;
			}
			SelectedValue = item.Value.ToString();
		}

		public T GetSelectedItem<T>() where T : Enumeration, new()
		{
			int value = int.Parse(SelectedValue);
			T enumeratedValue = Enumeration.FromValue<T>(value);

			return enumeratedValue;
		}

		public void BindData<T>(string notSelectedText) where T : Enumeration, new()
		{
			BindData<T>();
			Items.Insert(0, new ListItem(notSelectedText, "0"));
			Items[0].Selected = true;
		}

		public void BindData<T>(IEnumerable<T> values, string notSelectedText) where T : Enumeration, new()
		{
			BindData(values);
			Items.Insert(0, new ListItem(notSelectedText, "0"));
			Items[0].Selected = true;
		}

		public void BindData<T>() where T : Enumeration, new()
		{
			BindData(Enumeration.GetAll<T>());
		}

		public void BindData<T>(T selectedValue) where T : Enumeration, new()
		{
			BindData(Enumeration.GetAll<T>());
			SelectedValue = selectedValue.Value.ToString();
		}

		public void BindData<T>(IEnumerable<T> values) where T : Enumeration, new()
		{
			Items.Clear();
			foreach (T enumeratedItem in values)
			{
				Items.Add(new ListItem(enumeratedItem.DisplayName, enumeratedItem.Value.ToString()));
			}
		}
	}
}