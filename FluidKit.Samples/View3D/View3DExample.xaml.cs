using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FluidKit.Controls.View3D;

namespace FluidKit.Samples.View3D
{
	/// <summary>
	/// 	Interaction logic for Window1.xaml
	/// </summary>
	[ExportExample("View3D")]
	public partial class View3DExample : UserControl
	{
		public View3DExample()
		{
			InitializeComponent();
		}

		private void AddModel(object sender, RoutedEventArgs e)
		{
			Button b = sender as Button;
			MapItemVisual3D model = null;
			switch (b.Name)
			{
				case "_cube":
					model = new Cube();
					//model.FaceBrush = new SolidColorBrush(Colors.LightCoral);
					break;
				case "_cylinder":
					model = new Cylinder();
					//model.FaceBrush = new SolidColorBrush(Colors.Ivory);
					break;
				case "_sphere":
					model = new Sphere();
					//model.FaceBrush = new LinearGradientBrush(Colors.LightBlue, Colors.Orchid, 90);
					break;
				case "_torus":
					model = new Torus();
					//model.FaceBrush = new SolidColorBrush(Colors.Khaki);
					break;
				case "_cone":
					model = new Cone();
					//model.FaceBrush = new LinearGradientBrush(Colors.LightBlue, Colors.Orchid, 90);
					break;
			}
			model.FaceBrush = new LinearGradientBrush(Colors.LightBlue, Colors.Orchid, 90);
			model.FaceBrush.Opacity = 0.5;
			model.EdgePen = new Pen(Brushes.Black, 1);

			_view3D.Children.Add(model);
		}
	}
}