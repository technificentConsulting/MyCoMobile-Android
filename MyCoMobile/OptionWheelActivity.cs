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
using Com.Touchmenotapps.Widget.Radialmenu.Menu.V2;

namespace MyCoMobile
{
    [Activity(Label = "OptionWheel")]
    public class OptionWheelActivity : Activity
    {

        RadialMenuView circleMenu;
        RadialMenuRenderer menuRenderer;
        RadialMenuHelperFunctions menuHelpers;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Wheel);

            RadialMenuItem shopMyCo = new RadialMenuItem("1", "shopMyCo");
            RadialMenuItem boutique = new RadialMenuItem("2", "boutique");
            RadialMenuItem blog = new RadialMenuItem("3", "blog");
            RadialMenuItem herbs = new RadialMenuItem("4", "herbs");
            RadialMenuItem games = new RadialMenuItem("2", "mini games");


            circleMenu = new RadialMenuView(this.ApplicationContext, menuRenderer);

    
            menuRenderer = new RadialMenuRenderer(circleMenu, false, 30f, 60f);
            menuRenderer.MenuSelectedColor = Color.Purple;
            menuRenderer.RenderView();
            
        }


    }


}