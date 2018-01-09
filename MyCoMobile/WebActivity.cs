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
    [Activity(Label = "MyCoMobile - WebView", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class WebActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Web);
            // Create your application here
        }
    }
}