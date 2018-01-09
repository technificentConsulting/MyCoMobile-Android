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
using Android.Views.Animations;
using Android.Media;

namespace MyCoMobile
{
    [Activity(NoHistory = true, MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape, Theme = "@style/AppTheme", Label = "MyCoMobile")]
    [IntentFilter(new[] { "android.intent.action.MAIN" }, Categories = new[] { Intent.CategoryLauncher })]
    public class LauncherActivity : Activity
    {
     
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Launcher);


            OpeningVideo();

            Handler handler = new Handler();
            Action runnable = () =>
            {
                // your code that you want to delay here
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                Finish();
            };

            handler.PostDelayed(runnable, 15000);

            // setDefaults(FindViewById<LinearLayout>(Resource.Id.launchCenterview));
            // openingAnimation();

        }


        void OpeningVideo()
        {
            string APPLICATION_RAW_PATH = "android.resource://com.technificentconsulting.MyCoMobile/";

            VideoView videoView = FindViewById<VideoView>(Resource.Id.vwIntroVideo);
            var Path = (APPLICATION_RAW_PATH + Resource.Raw.opener_vid);
            videoView.SetVideoURI(Android.Net.Uri.Parse(Path));
            videoView.Start();

            Console.WriteLine("Video started");


        }


        public override void OnWindowFocusChanged(bool hasFocus)
    {
        base.OnWindowFocusChanged(hasFocus);
            //(FindViewById<RelativeLayout>(Resource.Id.mainLayout));
            //openingAnimation();
            OpeningVideo();
        }

        ImageView lady;
        ImageView myco;
        ImageView creations;
        ImageView injoy;
        ImageView innerstand;
        ImageView imagine;
        ImageView creating;
        float end_x;
        float end_y;
        float screenwCord;
        float screenhCord;

        public void openingAnimation()
        {

            end_x = myco.Left + 3;
            end_y = myco.Top + 20;
            Animation moveMyco = new TranslateAnimation(0, end_x, 0, end_y);
            moveMyco.Duration = (2600);
            moveMyco.FillAfter = (true);
            myco.StartAnimation(moveMyco);

            end_x = creations.Left;
            end_y = creations.Top;
            Animation moveCreations = new TranslateAnimation(screenwCord, end_x, 0, end_y);
            moveCreations.Duration = (3000);
            moveCreations.FillAfter = (true);
            creations.StartAnimation(moveCreations);

            end_x = creating.Left;
            end_y = creating.Top + 25;
            Animation moveCreating = new TranslateAnimation(0, end_x, 0, end_y);
            moveCreating.Duration = (3000);
            moveCreating.FillAfter = (true);
            creating.StartAnimation(moveCreating);

            end_x = injoy.Left;
            end_y = injoy.Top;
            Animation moveInjoy = new TranslateAnimation(0, end_x, 0, end_y);
            moveInjoy.Duration = (3000);
            moveInjoy.FillAfter = (true);
            injoy.StartAnimation(moveInjoy);

            end_x = imagine.Left;
            end_y = imagine.Top;
            Animation moveImagine = new TranslateAnimation(screenwCord / 2, end_x, screenhCord, end_y);
            moveImagine.Duration = (3000);
            moveImagine.FillAfter = (true);
            imagine.StartAnimation(moveImagine);

            end_x = innerstand.Left;
            end_y = innerstand.Top;
            Animation moveInnerstand = new TranslateAnimation(screenwCord, end_x, 0, end_y);
            moveInnerstand.FillAfter = (true);
            moveInnerstand.Duration = (4000);
            innerstand.StartAnimation(moveInnerstand);


            end_x = lady.Left;
            end_y = lady.Top;

            Animation anim_backward = AnimationUtils.LoadAnimation(this, Resource.Animation.animate_lady);
            anim_backward.FillAfter = (true);
            lady.StartAnimation(anim_backward);

            //  await Task.Delay(8000);

        }


        //public void setDefaults(RelativeLayout mainlayout)
        //{

        //    //LinearLayout l = (LinearLayout)findViewById(R.id.centerview);
        //    //RelativeLayout mainlayout = (RelativeLayout)findViewById(R.id.mainLayout);
        //    screenwCord = mainlayout.Width;
        //    screenhCord = mainlayout.Height;

        //    //l.setPadding(0,(int)screenhCord/3,0,0);

        //    lady = (ImageView)FindViewById(Resource.Id.ani1);
        //    myco = (ImageView)FindViewById(Resource.Id.ani2);
        //    creations = (ImageView)FindViewById(Resource.Id.ani3);
        //    creating = (ImageView)FindViewById(Resource.Id.ani4);
        //    injoy = (ImageView)FindViewById(Resource.Id.ani5);
        //    imagine = (ImageView)FindViewById(Resource.Id.ani6);
        //    innerstand = (ImageView)FindViewById(Resource.Id.ani7);

        //    myco.SetX(0);
        //    myco.SetY(0);
        //    creations.SetX(0);
        //    creations.SetY(0);
        //    //myco.layout(0,30,0,0);
        //    //creations.setPadding(0,30,0,0);
        //    /*lady.setX(0);
        //     lady.setY(0);*/
        //    creating.SetX(0);
        //    creating.SetY(0);
        //    injoy.SetX(0);
        //    injoy.SetY(0);
        //    imagine.SetX(0);
        //    imagine.SetY(0);
        //    innerstand.SetX(0);
        //    innerstand.SetY(0);
        //}
    }
}