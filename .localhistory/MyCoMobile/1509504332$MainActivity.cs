using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;


namespace MyCoMobile
{
    [Activity(Label = "MyCoMobile", MainLauncher = false, Theme ="@style/AppTheme", 
        ScreenOrientation = Android.Content.PM.ScreenOrientation.FullUser)]

    public class MainActivity : Activity, IMenuItemOnMenuItemClickListener, View.IOnTouchListener 
    {
    
        private CircleMenuLayout mCircleMenuLayout;
        private string[] mItemTexts = new string[] { "ShopMyCo", "Boutique", "Blog", "Relax/Play", "5", "6"};
        private int[] mItemImgs = new int[] {Resource.Drawable.ani0_logo, Resource.Drawable.home_mbank_2_normal,
        Resource.Drawable.home_mbank_3_normal, Resource.Drawable.home_mbank_4_normal, Resource.Drawable.home_mbank_5_normal,
        Resource.Drawable.home_mbank_6_normal};


        /// WheelMenu wheelMenu;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main2);

            mCircleMenuLayout = (CircleMenuLayout)FindViewById(Resource.Id.menulayout);
            mCircleMenuLayout.setMenuItemIconsAndTexts(mItemImgs, mItemTexts);


            mCircleMenuLayout.MenuItemClicked += MCircleMenuLayout_MenuItem_Clicked;
            mCircleMenuLayout.SetOnTouchListener(this);
           // mCircleMenuLayout.AutoFlingRunnable(20f);

        }

        private void MCircleMenuLayout_MenuItem_Clicked(object sender, CircleMenuEventArgs args)
        {
            Console.WriteLine("Main Menu Item Clicked menu item " + args.position);
        }

               
            public void itemClick(View view, int pos)
        {
            Toast.MakeText(this.ApplicationContext, mItemTexts[pos],
                    ToastLength.Short).Show();

        }


        public void itemCenterClick(View view)
        {
            Toast.MakeText(this.ApplicationContext,
                    "you can do something just like ccb  ",
                    ToastLength.Short).Show();

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public bool OnMenuItemClick(IMenuItem item)
        {
            Toast.MakeText(this.ApplicationContext, mItemTexts[item.ItemId],
          ToastLength.Short).Show();
            return true;
        }

        public bool OnTouch(View v, MotionEvent e)
        {
         
            mCircleMenuLayout.dispatchTouchEvent(e);

            return true;
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

