using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace MyCoMobile
{
    [Activity(Label = "MyCoMobile", MainLauncher = true, Theme ="@style/AppTheme")]
    public class MainActivity : Activity
    {
        private Button btnShopMyco;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            btnShopMyco = FindViewById<Button>(Resource.Id.btnShopMyco);
            btnShopMyco.Click += BtnShopMyco_Click;
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

