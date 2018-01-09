﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Views;
using Com.Touchmenotapps.Widget.Radialmenu.Menu.V2;
using Android.Graphics;
using System.Collections.Generic;
using Memorygame;

namespace MyCoMobile
{
    [Activity(Label = "MyCoMobile", MainLauncher = false, Theme ="@style/AppTheme", 
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class MainActivity : Activity
    {
        private Button btnShopMyco;
        private Button btnShopHerbs;
        private Button btnShopBoutique;

        RadialMenuRenderer menuRenderer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            FrameLayout circleMenu = FindViewById<FrameLayout>(Resource.Id.radialLayout);
            menuRenderer = new RadialMenuRenderer(circleMenu, true, 30f, 60f);
            menuRenderer.MenuSelectedColor = Color.Purple;
            menuRenderer.MenuBackgroundColor = Color.Yellow;


            // Set our view from the "main" layout resource
           
            List<RadialMenuItem> menuItems = new List<RadialMenuItem>();

            RadialMenuItem shopMyCo = new RadialMenuItem("1", "shopMyCo");
            RadialMenuItem boutique = new RadialMenuItem("2", "boutique");
            RadialMenuItem blog = new RadialMenuItem("3", "blog");
            RadialMenuItem herbs = new RadialMenuItem("4", "herbs");
            RadialMenuItem games = new RadialMenuItem("5", "mini games");

            menuItems.Add(shopMyCo);
            menuItems.Add(boutique);
            menuItems.Add(blog);
            menuItems.Add(herbs);
            menuItems.Add(games);
   

            menuRenderer.RadialMenuContent = menuItems;

            circleMenu.AddView(menuRenderer.RenderView());

            Memorygame.TileControl mem = new Memorygame.TileControl();
            mem.InitShuffle4x4Tiles();


            //btnShopMyco = FindViewById<Button>(Resource.Id.btnShopMyco);
            //btnShopMyco.Click += BtnShopMyco_Click;

            //btnShopHerbs = FindViewById<Button>(Resource.Id.btnShopHerbs);
            //btnShopHerbs.Click += BtnShopHerbs_Click;

            //btnShopBoutique = FindViewById<Button>(Resource.Id.btnBoutique);
            //btnShopBoutique.Click += BtnBoutique_Click;
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

