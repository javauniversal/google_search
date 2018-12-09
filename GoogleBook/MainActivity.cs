using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading;
using Android.Content;
using System.Threading.Tasks;
using GoogleBook.Search.View;

namespace GoogleBook
{
    [Activity(Label = "Google Book", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
   
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


        }

        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            await Task.Delay(6000); // Simulate a bit of startup work.
            StartActivity(new Intent(Application.Context, typeof(SearchBook)));
            Finish();
        }
    }
}

