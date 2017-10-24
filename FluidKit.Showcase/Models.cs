using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using FluidKit.Samples;

namespace FluidKit.Showcase
{
	public class FluidKitExample
	{
		public string Title { get; set; }

		public UserControl Control { get; set; }
	}

	public class FluidKitShowcase
	{
		public List<FluidKitExample> Examples { get; private set; }

		public void Prepare(List<Lazy<UserControl, IExampleMetadata>> examples)
		{
			Examples = (from x in examples
						orderby x.Metadata.Title
						select new FluidKitExample
						{
							Title = x.Metadata.Title,
							Control = x.Value
						}).ToList();
		}
	}

}