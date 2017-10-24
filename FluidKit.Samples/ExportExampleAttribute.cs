using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace FluidKit.Samples
{
	public interface IExampleMetadata
	{
		string Title { get; }
		string Category { get; }
	}

	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class ExportExampleAttribute : ExportAttribute, IExampleMetadata
	{
		public ExportExampleAttribute(string title) : base(typeof(UserControl))
		{
			Title = title;
		}

		#region IExampleMetadata Members

		public string Title { get; set; }
		public string Category { get; set; }

		#endregion
	}
}