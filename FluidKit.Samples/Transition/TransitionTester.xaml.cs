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
using System.Windows.Input;
using FluidKit.Controls;

namespace FluidKit.Samples.Transition
{
	/// <summary>
	/// 	Interaction logic for TransitionTester.xaml
	/// </summary>
	[ExportExample("Transitions")]
	public partial class TransitionTester : UserControl
	{
		private string _backItem = "_image1";
		private string _frontItem = "_image2";

		public TransitionTester()
		{
			InitializeComponent();
			Loaded += TransitionTester_Loaded;
		}

		private void TransitionTester_Loaded(object sender, RoutedEventArgs e)
		{
			_transContainer.TransitionCompleted += _transContainer_TransitionCompleted;
		}

		private void _transContainer_TransitionCompleted(object sender, EventArgs e)
		{
			SwapFrontAndBack();
		}

		private void SwitchImage(object sender, MouseButtonEventArgs args)
		{
			if (Keyboard.Modifiers == ModifierKeys.Control)
			{
				_transContainer.ApplyTransition("_image2", "_image1");
			}
		}

		private void PlayGenie()
		{
			GenieTransition transition = Resources["GenieTransition"] as GenieTransition;
			transition.EffectType = (_intoLamp.IsChecked.Value)
			                        	? GenieEffectType.IntoLamp
			                        	: GenieEffectType.OutOfLamp;

			_transContainer.Transition = transition;
			_transContainer.ApplyTransition(_frontItem, _backItem);
		}

		private void PlayCube()
		{
			CubeTransition transition = Resources["CubeTransition"] as CubeTransition;
			if (_l2r.IsChecked.Value)
			{
				transition.Rotation = Direction.LeftToRight;
			}
			if (_r2l.IsChecked.Value)
			{
				transition.Rotation = Direction.RightToLeft;
			}
			if (_t2b.IsChecked.Value)
			{
				transition.Rotation = Direction.TopToBottom;
			}
			if (_b2t.IsChecked.Value)
			{
				transition.Rotation = Direction.BottomToTop;
			}

			_transContainer.Transition = transition;
			_transContainer.ApplyTransition(_frontItem, _backItem);
		}

		private void PlayTransition(object sender, RoutedEventArgs args)
		{
			Button b = sender as Button;
			switch (b.Name)
			{
				case "_playGenie":
					PlayGenie();
					break;
				case "_playCube":
					PlayCube();
					break;
				case "_playSlide":
					PlaySlide();
					break;
				case "_playFlip":
					PlayFlip();
					break;
			}
		}

		private void PlayFlip()
		{
			FlipTransition transition = Resources["FlipTransition"] as FlipTransition;
			if (_flipL2R.IsChecked.Value)
			{
				transition.Rotation = Direction.LeftToRight;
			}
			else if (_flipR2L.IsChecked.Value)
			{
				transition.Rotation = Direction.RightToLeft;
			}

			_transContainer.Transition = transition;
			_transContainer.ApplyTransition(_frontItem, _backItem);
		}

		private void PlaySlide()
		{
			SlideTransition transition = Resources["SlideTransition"] as SlideTransition;
			transition.Direction = _slideL2R.IsChecked.Value ? Direction.LeftToRight : Direction.RightToLeft;

			_transContainer.Transition = transition;
			_transContainer.ApplyTransition(_frontItem, _backItem);
		}

		private void SwapFrontAndBack()
		{
			string temp = _frontItem;
			_frontItem = _backItem;
			_backItem = temp;
		}
	}
}