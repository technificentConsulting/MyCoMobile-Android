using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Util;
using Android.Widget;
using Android.Graphics;
using Com.Touchmenotapps.Widget;



namespace MyCoMobile
{
    [Activity(Label = "OptionWheel")]
    public class OptionWheel : Activity
    {

        RadialMenu R;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Wheel);
            
        }


    }


}