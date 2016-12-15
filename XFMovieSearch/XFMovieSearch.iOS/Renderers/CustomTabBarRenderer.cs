﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFMovieSearch.iOS.Renderers;
using UIKit;
using System.Text.RegularExpressions;

/**
 * Credit goes to Gerald Versluis, for his sample of 
 * creating a Custom Renderer for tab bar icons in iOS:
 * https://blog.verslu.is/xamarin/xamarin-forms-xamarin/spicing-up-your-xamarin-formsios-tabbar/
 */

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabBarRenderer))]
namespace XFMovieSearch.iOS.Renderers
{
	public class CustomTabBarRenderer : TabbedRenderer
	{
		private bool _initialized;

		public override void ViewWillAppear(bool animated)
		{
			if (!_initialized)
			{
				if (TabBar?.Items == null)
					return;

				var tabs = Element as TabbedPage;

				if (tabs != null)
				{
					for (int i = 0; i < TabBar.Items.Length; i++)
					{
						UpdateItem(TabBar.Items[i], tabs.Children[i].Icon, tabs.Children[i].StyleId);
					}
				}

				_initialized = true;
			}

			base.ViewWillAppear(animated);
		}

		private void UpdateItem(UITabBarItem item, string icon, string badgeValue)
		{
			if (item == null)
				return;

			try
			{
				if (icon.EndsWith(".png"))
					icon = icon.Replace(".png", "_selected.png");
				else
					icon += "_selected";

				item.SelectedImage = UIImage.FromBundle(icon);
				item.SelectedImage.AccessibilityIdentifier = icon;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Unable to set selected icon: " + ex);
			}
		}
	}
}
