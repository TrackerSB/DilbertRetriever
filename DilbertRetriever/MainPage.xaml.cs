using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.Net.Http;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace DilbertRetriever {
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
            string imageName = date.ToString("yyyy-MM-dd");

            // TODO Introduce placeholder DilbertStrip
            string stripPageUrl = "https://dilbert.com/strip/" + imageName;
            BitmapImage stripImage;
            try {
                Debug.WriteLine("Request dilbert strip for " + date);
                HtmlDocument document = web.Load(stripPageUrl);
                HtmlNode stripImageNode = document.DocumentNode.SelectSingleNode("//img[contains(@class,'img-comic')]");
                if (stripImageNode == null) {
                    stripImage = null;
                } else {
                    string stripImageUrl = stripImageNode.GetAttributeValue("src", "src attribute not found");
                    stripImage = new BitmapImage(new Uri("https:" + stripImageUrl));
                }
            } catch (HttpRequestException ex) {
                // TODO Return Dilbert placeholder image
                stripImage = null;
            }


            return new DilbertStrip() {
                Date = date,
                Strip = stripImage
            };
        }
    }
}
