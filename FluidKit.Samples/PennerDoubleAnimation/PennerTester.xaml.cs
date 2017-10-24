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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using FluidKit.Helpers.Animation;

namespace FluidKit.Samples.PennerDoubleAnimation
{
	/// <summary>
	/// 	Interaction logic for PennerTester.xaml
	/// </summary>
	[ExportExample("Penner Animations")]
	public partial class PennerTester : UserControl
	{
		private Equations _equation = Equations.Linear;

		public PennerTester()
		{
			InitializeComponent();
		}

		private void PennerAnimate(object sender, RoutedEventArgs args)
		{
			Storyboard board = PrepareStoryboard();

			//PennerDoubleAnimation anim1 = new PennerDoubleAnimation(_equation, 25, 100, new Duration(TimeSpan.FromSeconds(1)));
			//Storyboard.SetTargetProperty(anim1, new PropertyPath("Width"));

			//PennerDoubleAnimation anim2 = new PennerDoubleAnimation(_equation, 25, 100, new Duration(TimeSpan.FromSeconds(1)));
			//Storyboard.SetTargetProperty(anim2, new PropertyPath("Height"));

			//board.Children.Add(anim1);
			//board.Children.Add(anim2);

			board.Begin(sender as Button);
		}

		private Storyboard PrepareStoryboard()
		{
			Storyboard board = (FindResource("Animation") as Storyboard).Clone();
			(board.Children[0] as Helpers.Animation.PennerDoubleAnimation).Equation = _equation;
			(board.Children[1] as Helpers.Animation.PennerDoubleAnimation).Equation = _equation;

			return board;
		}

		private void SelectEquation(object sender, RoutedEventArgs e)
		{
			RadioButton b = sender as RadioButton;
			_equation = (Equations) Enum.Parse(typeof (Equations), b.Content.ToString());
		}
	}
}