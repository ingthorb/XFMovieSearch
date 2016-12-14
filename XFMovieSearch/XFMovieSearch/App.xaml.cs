using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XFMovieSearch
{
    public partial class App : Application
    {
        public TabbedPage _tabbedPage;

        public App()
        {
            InitializeComponent();
            var mainPage = new MainPage();
            var mainPageNavigation = new NavigationPage(mainPage);
            mainPageNavigation.Title = "Movie Search";

            var topPage = new TopList();
            var topPageNavigation = new NavigationPage(topPage);
            topPageNavigation.Title = "Top List";

            var popPage = new PopularPageXF();
            var popPageNavigation = new NavigationPage(popPage);
            popPageNavigation.Title = "Popular";

            var tabbedPage = new TabbedPage();
            tabbedPage.Children.Add(mainPageNavigation);
            tabbedPage.Children.Add(topPageNavigation);
            tabbedPage.Children.Add(popPageNavigation);

            this._tabbedPage = tabbedPage;
            this.MainPage = tabbedPage;
            System.Diagnostics.Debug.WriteLine("Inside app");
            tabbedPage.CurrentPageChanged += async (sender, e) =>
            {
                if(tabbedPage.CurrentPage.Equals(topPageNavigation))
                {
                    System.Diagnostics.Debug.WriteLine("Testing");
                    await topPage.GetTopList();
                }
                if (tabbedPage.CurrentPage.Equals(popPageNavigation))
                {
                    System.Diagnostics.Debug.WriteLine("PopPage");
                    await popPage.GetPopularList();
                }
            };
            }

        

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            System.Diagnostics.Debug.WriteLine("Inside app");
            // Handle when your app resumes
        }
    }
}
