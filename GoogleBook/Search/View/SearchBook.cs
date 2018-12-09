
using System;

using Android.App;
using Android.OS;
using Android.Widget;
using GoogleBook.Search.Controllers;

namespace GoogleBook.Search.View
{
    [Activity(Label = "Search")]
    public class SearchBook : Activity
    {
        private string[] items = { "Tecnología ", "JAVA", "PYTHON", "JAVASCRIPT", "C++", "C#", "PHP", "PERL", "SWIFT", "RUST" };
        private Spinner spinnerTechnologies;
        private ControllerSearch controllerSearch;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Searchs);

            //Call control
            spinnerTechnologies = FindViewById<Spinner>(Resource.Id.technologiesSpinner);
            controllerSearch = new ControllerSearch(this);

            fillTechnologiesSpinner();

        }

        private void fillTechnologiesSpinner()
        {
            ArrayAdapter ad = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, items);
            ad.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerTechnologies.Adapter = ad;
            spinnerTechnologies.ItemSelected += (sender, e) => {
                var s = sender as Spinner;
                if (e.Position != 0) 
                {
                    controllerSearch.callToServiceSearchBook((string)s.GetItemAtPosition(e.Position));

                    //Toast.MakeText(this, "My favorite is " + s.GetItemAtPosition(e.Position), ToastLength.Short).Show();
                }
            };
        }
    }
}
