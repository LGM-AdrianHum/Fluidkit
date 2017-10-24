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
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Xml;
using FluidKit.Helpers.DragDrop;

namespace FluidKit.Samples.DragDrop.PanelExample
{
	public class DefaultDropTargetAdvisor : IDropTargetAdvisor
	{
		private static DataFormat SupportedFormat = DataFormats.GetDataFormat("FluidKit");
		private bool _applyMouseOffset;

		#region IDropTargetAdvisor Members

		public bool IsValidDataObject(IDataObject obj)
		{
			return obj.GetDataPresent(SupportedFormat.Name);
		}

		public void OnDropCompleted(IDataObject obj, Point dropPoint)
		{
			UIElement elt = ExtractElement(obj);

			(TargetUI as Panel).Children.Add(elt);
		}

		public UIElement TargetUI { get; set; }

		public bool ApplyMouseOffset
		{
			get { return _applyMouseOffset; }
		}

		public UIElement GetVisualFeedback(IDataObject obj)
		{
			Button elt = ExtractElement(obj) as Button;
			elt.Width = 50;
			elt.Height = 60;
			elt.Opacity = 0.5;
			elt.IsHitTestVisible = false;

			DoubleAnimation anim = new DoubleAnimation(0.75,
			                                           new Duration(TimeSpan.FromMilliseconds(500)));
			anim.From = 0.25;
			anim.AutoReverse = true;
			anim.RepeatBehavior = RepeatBehavior.Forever;
			elt.BeginAnimation(UIElement.OpacityProperty, anim);

			return elt;
		}

		public UIElement GetTopContainer()
		{
			return Application.Current.MainWindow.Content as UIElement;
		}

		#endregion

		private UIElement ExtractElement(IDataObject obj)
		{
			string xamlString = obj.GetData("FluidKit") as string;
			XmlReader reader = XmlReader.Create(new StringReader(xamlString));
			UIElement elt = XamlReader.Load(reader) as UIElement;

			return elt;
		}
	}
}