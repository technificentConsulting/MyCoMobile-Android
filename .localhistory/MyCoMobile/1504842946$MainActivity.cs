﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Views;

namespace MyCoMobile
{
    [Activity(Label = "MyCoMobile", MainLauncher = false, Theme ="@style/AppTheme", 
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class MainActivity : Activity
    {
        private Button btnShopMyco;
        private Button btnShopHerbs;
        private Button btnShopBoutique;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);



            btnShopMyco = FindViewById<Button>(Resource.Id.btnShopMyco);
            btnShopMyco.Click += BtnShopMyco_Click;

            btnShopHerbs = FindViewById<Button>(Resource.Id.btnShopHerbs);
            btnShopHerbs.Click += BtnShopHerbs_Click;

            btnShopBoutique = FindViewById<Button>(Resource.Id.btnBoutique);
            btnShopBoutique.Click += BtnBoutique_Click;
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }


        private void BtnShopHerbs_Click(object sender, System.EventArgs e)
        {
            string url = "http://roots-r-us.com";
            Intent i = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));
            StartActivity(i);
            Finish();
        }

        private void BtnBoutique_Click(object sender, System.EventArgs e)
        {
            string url = "http://boutique.mycocreations.com";
            Intent i = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));
            StartActivity(i);
            Finish();
        }

        private void BtnShopMyco_Click(object sender, System.EventArgs e)
        {
            string url = "http://shop.mycocreations.com";
            Intent i = new Intent(Intent.ActionView,Android.Net.Uri.Parse(url));
            StartActivity(i);
            Finish();
        }


    }
}

