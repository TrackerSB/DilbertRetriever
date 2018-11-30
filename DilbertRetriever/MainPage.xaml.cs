using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.XPath;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace DilbertRetriever {
    /// <summary>
    /// This page lists all Dilbert strips to browse.
    /// </summary>
    public sealed partial class MainPage : Page {
        private readonly HtmlWeb web = new HtmlWeb();

        public MainPage() {
            this.InitializeComponent();
            DateTime current = DateTime.Today;
            for (int i = 0; i < 7; i++) {
                dilbertStrips.Items.Add(GetDilbertStrip(current));
                current = current.AddDays(-1);
            }
        }

        private object GetDilbertStrip(DateTime date) {
            string stripPageUrl = "https://dilbert.com/strip/" + date.ToString("yyyy-MM-dd");
            HtmlDocument document = web.Load(stripPageUrl);
            HtmlNode strip = document.DocumentNode.SelectSingleNode("//img[contains(@class,'img-comic')]");
            string stripImageUrl = strip.GetAttributeValue("src", "src attribute not found");
            return new Image {
                Source = new BitmapImage(new Uri("https:" + stripImageUrl)),
                MaxWidth = 800
            };
        }
    }
}
