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

        private DilbertStrip GetDilbertStrip(DateTime date) {
            string stripPageUrl = "https://dilbert.com/strip/" + date.ToString("yyyy-MM-dd");
            HtmlDocument document = web.Load(stripPageUrl);
            HtmlNode stripImageNode = document.DocumentNode.SelectSingleNode("//img[contains(@class,'img-comic')]");
            string stripImageUrl = stripImageNode.GetAttributeValue("src", "src attribute not found");
            return new DilbertStrip() {
                Date = date,
                Strip = new BitmapImage(new Uri("https:" + stripImageUrl))
            };
        }
    }

    public sealed class DateConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if(value is DateTime) {
                return ((DateTime) value).ToString("d");
            } else {
                throw new ArgumentException("The passed value can not be converted.");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public sealed class DilbertStrip {
        public DateTime Date { get; set; }
        public BitmapImage Strip { get; set; }
    }
}
