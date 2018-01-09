using Android.App;
using Android.Widget;
using Android.OS;

namespace MyCoMobile
{
    [Activity(Label = "MyCoMobile", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private Button btnShopMyco;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

          //  btnShopMyco = (Button)FindViewById(Resource.)
        }

    }
}

