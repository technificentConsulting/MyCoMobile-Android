﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;


namespace MyCoMobile
{
    [Activity(Label = "MyCoMobile", MainLauncher = false, Theme ="@style/AppTheme", 
        ScreenOrientation = Android.Content.PM.ScreenOrientation.FullUser)]

    public class MainActivity : Activity, View.IOnClickListener, View.IOnTouchListener 
    {
    
        private CircleMenuLayout mCircleMenuLayout;
        private string[] mItemTexts = new string[] { "ShopMyCo", "RootsRUs", "Boutique", "Games", "Videos", "Blog"};
        private int[] mItemImgs = new int[] {Resource.Drawable.shopmyco, Resource.Drawable.rrus,
        Resource.Drawable.boutique, Resource.Drawable.games, Resource.Drawable.videos,
        Resource.Drawable.blog};


        /// WheelMenu wheelMenu;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main2);

            mCircleMenuLayout = (CircleMenuLayout)FindViewById(Resource.Id.menulayout);
            mCircleMenuLayout.setMenuItemIconsAndTexts(mItemImgs, mItemTexts);

            EventArgs e = new EventArgs() { };
            mCircleMenuLayout.CView_Click(this, e); 
            mCircleMenuLayout.SetOnTouchListener(this);


        }


        private void CView_Click(object sender, EventArgs e)
        {
            //"ShopMyCo", "RootsRUs", "Boutique", "Games", "Videos", "Blog"
            string url = string.Empty;
            int imgTag = int.Parse(((View)sender).Tag.ToString());

            switch (imgTag)
            {
                case 1:
                    url = "http://shop.mycocreations.com";
                    break;
                case 2:
                    url = "http://www.roots-r-us.com";
                    break;

                case 3:
                    url = "http://boutique.mycocreations.com";
                    break;


                case 5:
                    url = "http://youtube.com/mycocreations";
                    break;

                case 6:
                    url = "http://blog.mycocreations.com";
                    break;

                default:
                    break;
            }

            Console.WriteLine("Image with tag " + imgTag);

            if (!string.IsNullOrEmpty(url))
            {
                OpenWebActivity(url);
            }

        }

        public void OpenWebActivity(string url)
        {
            Intent i = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));
            MainActivity m = new MainActivity();
            m.OpenWebActivity(url);
        }
        public bool OnTouch(View v, MotionEvent e)
        {
         
            mCircleMenuLayout.dispatchTouchEvent(e);

            return true;
        }

 

        public void OnClick(View v)
        {
            throw new NotImplementedException();
        }
    }
}

