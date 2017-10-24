// -------------------------------------------------------------------------------
// 
// This file is part of the FluidKit project: http://www.codeplex.com/fluidkit
// 
// Copyright (c) 2008, The FluidKit community 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
// 
// * Redistributions of source code must retain the above copyright notice, this 
// list of conditions and the following disclaimer.
// 
// * Redistributions in binary form must reproduce the above copyright notice, this 
// list of conditions and the following disclaimer in the documentation and/or 
// other materials provided with the distribution.
// 
// * Neither the name of FluidKit nor the names of its contributors may be used to 
// endorse or promote products derived from this software without specific prior 
// written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR 
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES 
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON 
// ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT 
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
// -------------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FluidKit.Controls;

namespace FluidKit.Samples.ElementFlow
{
	/// <summary>
	/// 	Interaction logic for Window1.xaml
	/// </summary>
	[ExportExample("ElementFlow")]
	public partial class ElementFlowExample
	{
		private StringCollection _dataSource;

		private LayoutBase[] _layouts = {
		                                	new Wall(),
		                                	new SlideDeck(),
		                                	new CoverFlow(),
		                                	new Carousel(),
		                                	new TimeMachine2(),
		                                	new ThreeLane(),
		                                	new VForm(),
		                                	new TimeMachine(),
		                                	new RollerCoaster(),
		                                	new Rolodex(),
		                                };

		private Random _randomizer = new Random();

		private int _viewIndex;

		public ElementFlowExample()
		{
			InitializeComponent();
			Loaded += Window1_Loaded;
		}

		private void Window1_Loaded(object sender, RoutedEventArgs e)
		{
			_elementFlow.Layout = _layouts[0];
			_currentViewText.Text = _elementFlow.Layout.GetType().Name;

			_selectedIndexSlider.Maximum = _elementFlow.Items.Count - 1;
			_elementFlow.SelectionChanged += EFSelectedIndexChanged;
			_elementFlow.SelectedIndex = 0;

			_dataSource = FindResource("DataSource") as StringCollection;
		}

		private void EFSelectedIndexChanged(object sender, SelectionChangedEventArgs e)
		{
			Debug.WriteLine((sender as Controls.ElementFlow).SelectedIndex);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.Key == Key.F12)
			{
				_viewIndex = (_viewIndex + 1)%_layouts.Length;
				_elementFlow.Layout = _layouts[_viewIndex];
				_currentViewText.Text = _elementFlow.Layout.GetType().Name;
			}
		}

		private void ChangeSelectedIndex(object sender, RoutedPropertyChangedEventArgs<double> args)
		{
			_elementFlow.SelectedIndex = (int) args.NewValue;
		}

		private void RemoveCard(object sender, RoutedEventArgs args)
		{
			if (_elementFlow.Items.Count > 0)
			{
				_dataSource.RemoveAt(_randomizer.Next(_dataSource.Count));

				// Update selectedindex slider
				_selectedIndexSlider.Maximum = _elementFlow.Items.Count - 1;
			}
		}

		private void AddCard(object sender, RoutedEventArgs args)
		{
			Button b = sender as Button;
			int index = _randomizer.Next(_dataSource.Count);
			if (b.Name == "_regular")
			{
				_dataSource.Insert(index, "Images/01.jpg");
			}
			else
			{
				_dataSource.Insert(index, string.Format("Images/{0:00}", _randomizer.Next(1, 13)) + ".jpg");
			}

			// Update selectedindex slider
			_selectedIndexSlider.Maximum = _elementFlow.Items.Count - 1;
		}
	}
}