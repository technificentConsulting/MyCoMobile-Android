using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyCoMobile
{
    [Activity(NoHistory = true, MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape, Theme = "@style/Splash", Label = "SplashActivity")]
    [IntentFilter(new[] { "android.intent.action.MAIN" }, Categories = new[] { Intent.CategoryLauncher })]
    public class SplashActivity : Activity
    {
        static int SPLASH_TIME_OUT = 500;

        Action runnable;
        Handler handler;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            handler = new Handler();
            runnable = Callback;

            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();


        }

        void Callback()
        {
            // repost itself
            handler.PostDelayed(runnable, SPLASH_TIME_OUT);
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }

        
    }
}