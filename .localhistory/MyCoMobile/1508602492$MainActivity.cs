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
        ScreenOrientation = Android.Content.PM.ScreenOrientation.FullUser)]

    public class MainActivity : Activity, IMenuItemOnMenuItemClickListener
    {

        private CircleMenuLayout mCircleMenuLayout;
        private string[] mItemTexts = new string[] { "ShopMyCo", "Boutique", "Blog", "Relax/Play", "5", "6"};
        private int[] mItemImgs = new int[] {Resource.Drawable.home_mbank_1_normal, Resource.Drawable.home_mbank_2_normal,
        Resource.Drawable.home_mbank_3_normal, Resource.Drawable.home_mbank_4_normal, Resource.Drawable.home_mbank_5_normal,
        Resource.Drawable.home_mbank_6_normal};

        IMenuItemOnMenuItemClickListener menuclick;
        /// WheelMenu wheelMenu;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main2);

            mCircleMenuLayout = (CircleMenuLayout)FindViewById(Resource.Id.menulayout);
            mCircleMenuLayout.setMenuItemIconsAndTexts(mItemImgs, mItemTexts);

            //shopMyCo.SetOnRadialMenuClickListener(new RadialMenuRenderer.IOnRadailMenuClick()
            //{

            //});
            
        }


        public bool OnMenuItemClick(IMenuItem item)
        {
            throw new NotImplementedException();
        }     

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }


        //private void BtnShopHerbs_Click(object sender, System.EventArgs e)
        //{
        //    string url = "http://roots-r-us.com";
        //    Intent i = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));
        //    StartActivity(i);
        //    Finish();
        //}

        //private void BtnBoutique_Click(object sender, System.EventArgs e)
        //{
        //    string url = "http://boutique.mycocreations.com";
        //    Intent i = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));
        //    StartActivity(i);
        //    Finish();
        //}

        //private void BtnShopMyco_Click(object sender, System.EventArgs e)
        //{
        //    string url = "http://shop.mycocreations.com";
        //    Intent i = new Intent(Intent.ActionView,Android.Net.Uri.Parse(url));
        //    StartActivity(i);
        //    Finish();
        //}


    }
}

