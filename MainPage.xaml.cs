using System.Drawing;
using Color = Microsoft.Maui.Graphics.Color;

namespace MAUI_Citazioni
{
    public partial class MainPage : ContentPage
    {
        Random random = new Random();
        List<string> quotes = new();
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnGenerateQuote_Clicked(object sender, EventArgs e)
        {
            grdBackground.Background = GetGradientBackground();
            lblQuote.Text = GetRandomQuote();
        }

        private void GetBackgroundAndQuote()
        {
            grdBackground.Background = GetGradientBackground();
            lblQuote.Text = GetRandomQuote();
        }

        private LinearGradientBrush GetGradientBackground()
        {
            System.Drawing.Color startColor = System.Drawing.Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            System.Drawing.Color endColor = System.Drawing.Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

            var colors = ColorUtility.ColorControls.GetColorGradient(startColor, endColor, 5);
            float stopOffset = .0f;
            GradientStopCollection stops = new GradientStopCollection();

            foreach (var color in colors)
            {
                stops.Add(new GradientStop(Color.FromArgb(color.Name),
                    stopOffset));
                stopOffset += .2f;
            }

            return new LinearGradientBrush(stops, new Microsoft.Maui.Graphics.Point(0, 0), new Microsoft.Maui.Graphics.Point(1, 1));
        }

        private string GetRandomQuote()
        {
            var index = random.Next(quotes.Count);
            return quotes[index];
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await LoadMauiAsset();
            GetBackgroundAndQuote();
        }

        async Task LoadMauiAsset()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("quotes.txt");
            using var reader = new StreamReader(stream);


            while (reader.Peek() != -1)
            {
                quotes.Add(reader.ReadLine());
            }
        }
    }

}
