using System;
using System.Web.UI.WebControls;

namespace Tarantino.Infrastructure.Commons.UI.Controls
{
	public class NonNegativeIntegerTextBox : TextBox
	{
		public int Value
		{
			set
			{
				Text = value.ToString();
			}
			get
			{
				int value;
				int.TryParse(Text, out value);
				value = Math.Max(0, value);
				return value;
			}
		}
	}
}