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
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using FluidKit.Helpers.DragDrop;

namespace FluidKit.Samples.DragDrop.CanvasExample
{
	public class CanvasDragDropAdvisor : IDragSourceAdvisor, IDropTargetAdvisor
	{
		private bool _applyMouseOffset;
		private UIElement _sourceAndTargetElt;

		#region IDragSourceAdvisor Members

		public UIElement SourceUI
		{
			get { return _sourceAndTargetElt; }
			set { _sourceAndTargetElt = value; }
		}

		public DragDropEffects SupportedEffects
		{
			get { return DragDropEffects.Copy | DragDropEffects.Move; }
		}

		public DataObject GetDataObject(UIElement draggedElt)
		{
			string serializedElt = XamlWriter.Save(draggedElt);
			DataObject obj = new DataObject("CanvasExample", serializedElt);

			return obj;
		}

		public void FinishDrag(UIElement draggedElt, DragDropEffects finalEffects)
		{
			if ((finalEffects & DragDropEffects.Move) == DragDropEffects.Move)
			{
				(_sourceAndTargetElt as Canvas).Children.Remove(draggedElt);
			}
		}

		public bool IsDraggable(UIElement dragElt)
		{
			return (!(dragElt is Canvas));
		}

		public UIElement GetTopContainer()
		{
			return Application.Current.MainWindow.Content as UIElement;
		}

		#endregion

		#region IDropTargetAdvisor Members

		public UIElement TargetUI
		{
			get { return _sourceAndTargetElt; }
			set { _sourceAndTargetElt = value; }
		}

		public bool ApplyMouseOffset
		{
			get { return _applyMouseOffset; }
		}

		public bool IsValidDataObject(IDataObject obj)
		{
			return (obj.GetDataPresent("CanvasExample"));
		}

		public UIElement GetVisualFeedback(IDataObject obj)
		{
			UIElement elt = ExtractElement(obj);

			Type t = elt.GetType();

			Rectangle rect = new Rectangle();
			rect.Width = (double) t.GetProperty("Width").GetValue(elt, null);
			rect.Height = (double) t.GetProperty("Height").GetValue(elt, null);
			rect.Fill = new VisualBrush(elt);
			rect.Opacity = 0.5;
			rect.IsHitTestVisible = false;

			return rect;
		}

		public void OnDropCompleted(IDataObject obj, Point dropPoint)
		{
			Canvas canvas = _sourceAndTargetElt as Canvas;

			UIElement elt = ExtractElement(obj);
			canvas.Children.Add(elt);
			Canvas.SetLeft(elt, dropPoint.X);
			Canvas.SetTop(elt, dropPoint.Y);
		}

		#endregion

		private UIElement ExtractElement(IDataObject obj)
		{
			string xamlString = obj.GetData("CanvasExample") as string;
			XmlReader reader = XmlReader.Create(new StringReader(xamlString));
			UIElement elt = XamlReader.Load(reader) as UIElement;

			return elt;
		}
	}
}