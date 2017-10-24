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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FluidKit.Helpers.DragDrop;

namespace FluidKit.Samples.DragDrop.CrossApp
{
	public class CrossAppAdvisor : IDragSourceAdvisor, IDropTargetAdvisor
	{
		private bool _applyMouseOffset;

		#region IDragSourceAdvisor Members

		public UIElement SourceUI { get; set; }

		public DragDropEffects SupportedEffects
		{
			get { return DragDropEffects.Copy | DragDropEffects.Move; }
		}

		public DataObject GetDataObject(UIElement draggedElt)
		{
			DataObject obj = new DataObject();
			obj.SetData(DataFormats.Text, "Text from WPF - CrossApp example");

			return obj;
		}

		public void FinishDrag(UIElement draggedElt, DragDropEffects finalEffects)
		{
		}

		public bool IsDraggable(UIElement dragElt)
		{
			return (dragElt is Button);
		}

		public UIElement GetTopContainer()
		{
			return Application.Current.MainWindow.Content as UIElement;
		}

		#endregion

		/*
_______________________________________________________________________________
		Drop Target
_______________________________________________________________________________
*/

		#region IDropTargetAdvisor Members

		public UIElement TargetUI { get; set; }

		public bool ApplyMouseOffset
		{
			get { return _applyMouseOffset; }
		}

		public bool IsValidDataObject(IDataObject obj)
		{
			return obj.GetDataPresent(DataFormats.Text);
		}

		public void OnDropCompleted(IDataObject obj, Point dropPoint)
		{
			Button b = (TargetUI as Panel).FindName("dropButton") as Button;
			b.Content = obj.GetData(DataFormats.Text) as string;
		}

		public UIElement GetVisualFeedback(IDataObject obj)
		{
			TextBlock tb = new TextBlock();
			tb.Text = obj.GetData(DataFormats.Text) as string;
			tb.Background = new SolidColorBrush(Colors.Gray);
			tb.Foreground = new SolidColorBrush(Colors.White);

			tb.Height = 25;
			tb.Opacity = 0.5;

			return tb;
		}

		#endregion
	}
}