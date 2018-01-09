using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Views;
using Com.Touchmenotapps.Widget.Radialmenu.Menu.V2;
using Android.Graphics;
using System.Collections.Generic;
//using Com.Anupcowkur.Wheelmenu;



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
       /// WheelMenu wheelMenu;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            //LinearLayout circleMenu = FindViewById<LinearLayout>(Resource.Id.radialMenu);

            //wheelMenu = (WheelMenu)FindViewById(Resource.Id.radialMenu);

            //set the no of divisions in the wheel, default is 1
            wheelMenu.SetDivCount(12);

            //set the drawable to be used as the wheel image. If you
            //don't set this, you'll get a  NullPointerException.
            wheelMenu.SetWheelImage(Resource.Drawable.wheel);

            wheelMenu.Click += WheelMenu_Click;



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

        private void WheelMenu_Click(object sender, System.EventArgs e)
        {
         //   Debug.write wheelMenu.SelectedPosition;
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

