
using System;
using System.Collections.Generic;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Support.V7.Widget;
using Android.Views.InputMethods;
using Android.Widget;
using GoogleBook.Search.Controllers;
using GoogleBook.Search.Interface;
using GoogleBook.Search.Models;
using Newtonsoft.Json;

namespace GoogleBook.Search.View
{
    [Activity(Label = "Search")]
    public class SearchBook : Activity, IBook
    {
        private List<string> myListTecnology; 
        private AutoCompleteTextView autocompleteTextView;
        private ImageView btnSearch;


        private ControllerSearch controllerSearch;
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private BookAdapter mAdapter;
        private List<Book> listBooks = new List<Book>();
        private ProgressDialog progress;
        private ISharedPreferences prefs;
        private ArrayAdapter arrayAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Searchs);

            myListTecnology = GetCustomersFromPreferences();

            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetMessage("Contacting server. Please wait...");
            progress.SetCancelable(false);

            //Call control
            autocompleteTextView = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextView);
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerBook);
            btnSearch = FindViewById<ImageView>(Resource.Id.btnSearch);

            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new BookAdapter(this, listBooks);
            mRecyclerView.SetAdapter(mAdapter);

            controllerSearch = new ControllerSearch(this);

            fillTechnologiesSpinner();

            setAdapterData();


        }

        private void fillTechnologiesSpinner()
        {
            btnSearch.Click += delegate 
            {

                ISharedPreferencesEditor editor = prefs.Edit();

                if (myListTecnology.Count > 5)
                {
                     myListTecnology.RemoveAt(0);
                     myListTecnology.Add(autocompleteTextView.Text);
                } 
                else
                {
                    myListTecnology.Add(autocompleteTextView.Text);
                }

                setAdapterData();

                var jsonString = JsonConvert.SerializeObject(myListTecnology);
                editor.PutString("listBooks", jsonString);
                editor.Apply();

                progress.Show();
                controllerSearch.callToServiceSearchBook(autocompleteTextView.Text);
            };

        }

        public void IErrorBook(string error)
        {
            Toast.MakeText(this, error, ToastLength.Long).Show();
            progress.Cancel();
        }

        public void IGetListBook(RootObject objects)
        {
            if (objects.books.Count == 0)
            {
                Toast.MakeText(this, "No results found", ToastLength.Long).Show();
            }
            else
            {
                InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
                inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

                listBooks = objects.books;
                mAdapter.Update(listBooks);

                autocompleteTextView.Text = "";

            }

            progress.Cancel();
        }

        private List<String> GetCustomersFromPreferences()
        {
            // get shared preferences
            prefs = Application.Context.GetSharedPreferences("PREFERENCE_BOOK", FileCreationMode.Private);

            // read exisiting value
            var customers = prefs.GetString("listBooks", null);

            // if preferences return null, initialize listOfCustomers
            if (customers == null)
                return new List<String>();

            var listOfCustomers = JsonConvert.DeserializeObject<List<String>>(customers);

            if (listOfCustomers == null)
                return new List<String>();

            return listOfCustomers;
        }

        private void setAdapterData() 
        {
            arrayAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, myListTecnology);
            autocompleteTextView.Adapter = arrayAdapter;
        }

    }
}
