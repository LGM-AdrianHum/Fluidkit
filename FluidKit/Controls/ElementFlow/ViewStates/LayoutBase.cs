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
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace FluidKit.Controls
{
	public abstract class LayoutBase
	{
		public ElementFlow Owner { get; internal set; }

		public void SelectElement(int selectionIndex)
		{
			for (int beforeIndex = 0; beforeIndex < selectionIndex; beforeIndex++)
			{
				var leftSB = Owner.PrepareTemplateStoryboard(beforeIndex);
				PrepareStoryboard(leftSB, GetBeforeMotion(beforeIndex - selectionIndex));
				leftSB.Begin(Owner.Viewport);
			}

			var centerSB = Owner.PrepareTemplateStoryboard(selectionIndex);
			PrepareStoryboard(centerSB, GetSelectionMotion());
			centerSB.Begin(Owner.Viewport);

			for (int afterIndex = selectionIndex + 1; afterIndex < Owner.Items.Count; afterIndex++)
			{
				var rightSB = Owner.PrepareTemplateStoryboard(afterIndex);
				PrepareStoryboard(rightSB, GetAfterMotion(afterIndex - selectionIndex));
				rightSB.Begin(Owner.Viewport);
			}

		}

		private void PrepareStoryboard(Storyboard sb, Motion motion)
		{
			// Child animations
			AxisAngleRotation3D rotation = (sb.Children[0] as Rotation3DAnimation).To as AxisAngleRotation3D;
			DoubleAnimation xAnim = sb.Children[1] as DoubleAnimation;
			DoubleAnimation yAnim = sb.Children[2] as DoubleAnimation;
			DoubleAnimation zAnim = sb.Children[3] as DoubleAnimation;

			rotation.Angle = motion.Angle;
			rotation.Axis = motion.Axis;
			xAnim.To = motion.X;
			yAnim.To = motion.Y;
			zAnim.To = motion.Z;
		}

		protected abstract Motion GetBeforeMotion(int deltaIndex);
		protected abstract Motion GetSelectionMotion();
		protected abstract Motion GetAfterMotion(int deltaIndex);
	}
}