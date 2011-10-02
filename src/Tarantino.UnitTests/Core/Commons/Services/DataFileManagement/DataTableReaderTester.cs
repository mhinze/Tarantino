using System.Data;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Core.Commons.Model.Enumerations;
using Tarantino.Core.Commons.Services.DataFileManagement;
using DataTableReader=Tarantino.Core.Commons.Services.DataFileManagement.Impl.DataTableReader;

namespace Tarantino.UnitTests.Core.Commons.Services.DataFileManagement
{
	[TestFixture]
	public class DataTableReaderTester
	{
		[Test]
		public void Can_read_enumerated_column()
		{
			var table = new DataTable();
			table.Columns.Add("Mode Column");
			var row1 = table.NewRow();
			var row2 = table.NewRow();

			row1["Mode Column"] = "Archive";
			row2["Mode Column"] = "Live";

			table.Rows.Add(row1);
			table.Rows.Add(row2);

			IDataTableReader tableReader = new DataTableReader();

			tableReader.Open(table);

			Assert.That(tableReader.Read(), Is.EqualTo(true));
			Assert.That(tableReader.GetEnumeration<PersistenceMode>("Mode Column"), Is.EqualTo(PersistenceMode.Archive));

			Assert.That(tableReader.Read(), Is.EqualTo(true));
			Assert.That(tableReader.GetEnumeration<PersistenceMode>("Mode Column"), Is.EqualTo(PersistenceMode.Live));

			Assert.That(tableReader.Read(), Is.EqualTo(false));
		}

		[Test]
		public void Can_read_table_with_boolean_column()
		{
			var table = new DataTable();
			table.Columns.Add("Boolean Column");
			var row1 = table.NewRow();
			var row2 = table.NewRow();

			row1["Boolean Column"] = "true";
			row2["Boolean Column"] = "false";

			table.Rows.Add(row1);
			table.Rows.Add(row2);

			IDataTableReader tableReader = new DataTableReader();

			tableReader.Open(table);

			Assert.That(tableReader.Read(), Is.EqualTo(true));
			Assert.That(tableReader.GetBoolean("Boolean Column"), Is.EqualTo(true));

			Assert.That(tableReader.Read(), Is.EqualTo(true));
			Assert.That(tableReader.GetBoolean("Boolean Column"), Is.EqualTo(false));

			Assert.That(tableReader.Read(), Is.EqualTo(false));
		}

		[Test]
		public void Can_read_table_with_decimal_column()
		{
			var table = new DataTable();
			table.Columns.Add("Decimal Column");
			DataRow row1 = table.NewRow();
			DataRow row2 = table.NewRow();

			row1["Decimal Column"] = "2.1";
			row2["Decimal Column"] = "3.1";

			table.Rows.Add(row1);
			table.Rows.Add(row2);

			IDataTableReader tableReader = new DataTableReader();

			tableReader.Open(table);

			Assert.That(tableReader.Read(), Is.EqualTo(true));
			Assert.That(tableReader.GetDecimal("Decimal Column"), Is.EqualTo(2.1M));

			Assert.That(tableReader.Read(), Is.EqualTo(true));
			Assert.That(tableReader.GetDecimal("Decimal Column"), Is.EqualTo(3.1M));

			Assert.That(tableReader.Read(), Is.EqualTo(false));
		}

		[Test]
		public void Can_read_table_with_empty_string_values_at_the_end()
		{
			var table = new DataTable();
			table.Columns.Add("String Column");
			DataRow row1 = table.NewRow();
			DataRow row2 = table.NewRow();

			row1["String Column"] = "First String";
			row2["String Column"] = string.Empty;

			table.Rows.Add(row1);
			table.Rows.Add(row2);

			IDataTableReader tableReader = new DataTableReader();

			tableReader.Open(table);

			Assert.That(tableReader.Read(), Is.EqualTo(true));
			Assert.That(tableReader.Read(), Is.EqualTo(false));
		}

		[Test]
		public void Can_read_table_with_integer_column()
		{
			var table = new DataTable();
			table.Columns.Add("Integer Column");
			DataRow row1 = table.NewRow();
			DataRow row2 = table.NewRow();

			row1["Integer Column"] = "5";
			row2["Integer Column"] = "7";

			table.Rows.Add(row1);
			table.Rows.Add(row2);

			IDataTableReader tableReader = new DataTableReader();

			tableReader.Open(table);

			Assert.That(tableReader.Read(), Is.EqualTo(true));
			Assert.That(tableReader.GetInteger("Integer Column"), Is.EqualTo(5));

			Assert.That(tableReader.Read(), Is.EqualTo(true));
			Assert.That(tableReader.GetInteger("Integer Column"), Is.EqualTo(7));

			Assert.That(tableReader.Read(), Is.EqualTo(false));
		}

		[Test]
		public void Can_read_table_with_null_values_at_the_end()
		{
			var table = new DataTable();
			table.Columns.Add("String Column");
			DataRow row1 = table.NewRow();
			DataRow row2 = table.NewRow();

			row1["String Column"] = "First String";
			row2["String Column"] = null;

			table.Rows.Add(row1);
			table.Rows.Add(row2);

			IDataTableReader tableReader = new DataTableReader();

			tableReader.Open(table);

			Assert.That(tableReader.Read(), Is.EqualTo(true));
			Assert.That(tableReader.Read(), Is.EqualTo(false));
		}

		[Test]
		public void Can_read_table_with_partially_empty_row()
		{
			var table = new DataTable();
			table.Columns.Add("String Column");
			table.Columns.Add("Integer Column");
			DataRow row1 = table.NewRow();
			DataRow row2 = table.NewRow();

			row1["String Column"] = "First String";
			row1["Integer Column"] = 7;

			row2["String Column"] = string.Empty;
			row2["Integer Column"] = 12;

			table.Rows.Add(row1);
			table.Rows.Add(row2);

			IDataTableReader tableReader = new DataTableReader();

			tableReader.Open(table);

			Assert.That(tableReader.Read(), Is.EqualTo(true));
			Assert.That(tableReader.Read(), Is.EqualTo(true));
			Assert.That(tableReader.Read(), Is.EqualTo(false));
		}

		[Test]
		public void Can_read_table_with_partially_null_row()
		{
			var table = new DataTable();
			table.Columns.Add("String Column");
			table.Columns.Add("Integer Column");
			DataRow row1 = table.NewRow();
			DataRow row2 = table.NewRow();

			row1["String Column"] = "First String";
			row1["Integer Column"] = 7;

			row2["String Column"] = null;
			row2["Integer Column"] = 12;

			table.Rows.Add(row1);
			table.Rows.Add(row2);

			IDataTableReader tableReader = new DataTableReader();

			tableReader.Open(table);

			Assert.That(tableReader.Read(), Is.EqualTo(true));
			Assert.That(tableReader.Read(), Is.EqualTo(true));
			Assert.That(tableReader.Read(), Is.EqualTo(false));
		}

		[Test]
		public void Can_read_table_with_string_column()
		{
			var table = new DataTable();
			table.Columns.Add("String Column");
			DataRow row1 = table.NewRow();
			DataRow row2 = table.NewRow();

			row1["String Column"] = "First String";
			row2["String Column"] = "Second String";

			table.Rows.Add(row1);
			table.Rows.Add(row2);

			IDataTableReader tableReader = new DataTableReader();

			tableReader.Open(table);

			Assert.That(tableReader.Read(), Is.EqualTo(true));
			Assert.That(tableReader.GetString("String Column"), Is.EqualTo("First String"));

			Assert.That(tableReader.Read(), Is.EqualTo(true));
			Assert.That(tableReader.GetString("String Column"), Is.EqualTo("Second String"));

			Assert.That(tableReader.Read(), Is.EqualTo(false));
		}

		[Test]
		public void Returns_null_for_an_empty_string_value()
		{
			var table = new DataTable();
			table.Columns.Add("String Column");
			table.Columns.Add("Decimal Column");
			DataRow row1 = table.NewRow();

			row1["String Column"] = string.Empty;
			row1["Decimal Column"] = 5.5M;

			table.Rows.Add(row1);

			IDataTableReader tableReader = new DataTableReader();

			tableReader.Open(table);

			Assert.That(tableReader.Read(), Is.EqualTo(true));
			Assert.That(tableReader.GetString("String Column"), Is.Null);
			Assert.That(tableReader.GetDecimal("Decimal Column"), Is.EqualTo(5.5M));

			Assert.That(tableReader.Read(), Is.EqualTo(false));
		}
	}
}