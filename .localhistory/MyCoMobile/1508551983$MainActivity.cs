using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Views;

using Android.Graphics;
using System.Collections.Generic;
using MyCoMobile;
using System;

namespace MyCoMobile
{
    [Activity(Label = "MyCoMobile", MainLauncher = false, Theme ="@style/AppTheme", 
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]

    public class MainActivity : Activity, IMenuItemOnMenuItemClickListener
    {

        private CircleMenuLayout mCircleMenuLayout;
        private string[] mItemTexts = new string[] { "ShopMyCo", "Boutique", "Blog", "Relax/Play"};
        private int[] mItemImgs = new int[] {Resource.Drawable.ani0_logo, Resource.Drawable.ani2_myco,
        Resource.Drawable.ani5_injoy, Resource.Drawable.ani6_imagine};
        IMenuItemOnMenuItemClickListener menuclick;
        /// WheelMenu wheelMenu;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main2);

            //RelativeLayout circleMenu = FindViewById<RelativeLayout>(Resource.Id.circleMenu);

            //List<TextView> menuitems = new List<TextView>();
            //menuitems.Add(new TextView(this) { Text = "Menu1" });
            //menuitems.Add(new TextView(this) { Text = "Menu2 " });
            //menuitems.Add(new TextView(this) { Text = "Menu3 " });
            //menuitems.Add(new TextView(this) { Text = "Menu4 " });
            //menuitems.Add(new TextView(this) { Text = "Menu5 " });

            //mCircleMenuLayout = new CircleView(this.ApplicationContext, 120, menuitems);

            mCircleMenuLayout = (CircleMenuLayout)FindViewById(Resource.Id.menulayout);
            mCircleMenuLayout.setMenuItemIconsAndTexts(mItemImgs, mItemTexts);
            mCircleMenuLayout.AutoFlingRunnable(20f);


            //   MCircleMenuLayout_Click(mCircleMenuLayout,new List<string>() { } );

            //LinearLayout circleMenu = FindViewById<LinearLayout>(Resource.Id.radialMenu);

            //wheelMenu = (WheelMenu)FindViewById(Resource.Id.radialMenu);

            //set the no of divisions in the wheel, default is 1
            //wheelMenu.SetDivCount(12);

            //set the drawable to be used as the wheel image. If you
            //don't set this, you'll get a  NullPointerException.
            //wheelMenu.SetWheelImage(Resource.Drawable.wheel);

            //wheelMenu.Click += WheelMenu_Click;
            

            //shopMyCo.SetOnRadialMenuClickListener(new RadialMenuRenderer.IOnRadailMenuClick()
            //{

            //});

            //  shopMyCo.SetOnRadialMenuClickListener(new RadialMenuItem.OnRadailMenuClick { });
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

        public bool OnMenuItemClick(IMenuItem item)
        {
            throw new NotImplementedException();
        }
    }
}

