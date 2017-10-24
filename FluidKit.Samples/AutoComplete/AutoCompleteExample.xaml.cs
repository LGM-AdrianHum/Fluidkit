using System.Windows;
using System.Windows.Controls;

namespace FluidKit.Samples.AutoComplete
{
	[ExportExample("AutoCompleteBox")]
	public partial class AutoCompleteExample : UserControl
	{
		public AutoCompleteExample()
		{
			InitializeComponent();

			Loaded += (s, args) =>
			          	{
			          		var source = FindResource("DataSource") as StringCollection;
			          		for (int i = 0; i < 30; i++)
			          		{
			          			source.Add("Item - " + i);
			          		}
			          	};
		}
	}
}