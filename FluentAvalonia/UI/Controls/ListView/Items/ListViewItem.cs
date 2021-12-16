﻿using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.VisualTree;
using System;

namespace FluentAvalonia.UI.Controls
{
	public class ListViewItem : ListBoxItem
	{
		public ListViewItem()
		{

		}

		static ListViewItem()
		{
			FocusableProperty.OverrideDefaultValue<ListViewItem>(true);
		}		

		/// <summary>
		/// Flag that indicates whether the ListViewItem as generated by the ListView and is eligible for the
		/// recycle pool. If this flag is false, it was generated by user (via ChoosingItemContainer event) and
		/// user is responsible for recycling their own containers
		/// </summary>
		internal bool WasGeneratedByListView { get; set; }

		protected override void OnGotFocus(GotFocusEventArgs e)
		{
			base.OnGotFocus(e);

			var lv = this.FindAncestorOfType<ListViewBase>();
			if (lv != null)
			{
				lv.UpdateSelectionFromItemFocus(this);
			}
		}

		protected override async void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
		{
			base.OnAttachedToVisualTree(e);

			// TODO: Move this from here, make it optional
			var ani = new Animation
			{
				Duration = TimeSpan.FromSeconds(0.5),
				Children =
				{
					new KeyFrame
					{
						Cue = new Cue(0d),
						Setters =
						{
							new Setter(Visual.OpacityProperty, 0.0d),
							new Setter(ScaleTransform.ScaleXProperty, 0.85d),
							new Setter(ScaleTransform.ScaleYProperty, 0.85d)
						}
					},
					new KeyFrame
					{
						Cue = new Cue(1d),
						Setters =
						{
							new Setter(Visual.OpacityProperty, 1.0d),
							new Setter(ScaleTransform.ScaleXProperty, 1.0d),
							new Setter(ScaleTransform.ScaleYProperty, 1.0d)
						},
						KeySpline = new KeySpline(1,0,0,0)
					},
				}
			};

			await ani.RunAsync(this);
		}
	}
}
