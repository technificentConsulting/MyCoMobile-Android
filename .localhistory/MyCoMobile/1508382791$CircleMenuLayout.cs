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
using Android.Util;

namespace MyCoMobile
{
    /// <summary>
    /// USAGE
    ///  RelativeLayout circleMenu = FindViewById<RelativeLayout>(Resource.Id.circleMenu);
    ///      List<TextView> menuitems = new List<TextView>();
    ///      menuitems.Add(new TextView(this) { Text = "Menu1" });
    ///       menuitems.Add(new TextView(this) { Text = "Menu2 " });
    ///        menuitems.Add(new TextView(this) { Text = "Menu3 " });
    ///        menuitems.Add(new TextView(this) { Text = "Menu4 " });
    ///        menuitems.Add(new TextView(this) { Text = "Menu5 " });
    ///
    ///        circleMenu = new CircleView(this.ApplicationContext, 120, menuitems);
    ///        SetContentView(circleMenu);
    /// </summary>
    public class CircleMenuLayout : ViewGroup
    {
        private int mRadius;
        private static float RADIO_DEFAULT_CHILD_DIMENSION = 1 / 4f;
        private float RADIO_DEFAULT_CENTERITEM_DIMENSION = 1 / 3f;
        private static float RADIO_PADDING_LAYOUT = 1 / 12f;
        private static int FLINGABLE_VALUE = 300;
        private static int NOCLICK_VALUE = 3;
        private int mFlingableValue = FLINGABLE_VALUE;
        private float mPadding;
        private double mStartAngle = 0;
        private String[] mItemTexts;
        private int[] mItemImgs;
        private int mMenuItemCount;
        private float mTmpAngle;
        private long mDownTime;
        private bool isFling;
        private int mMenuItemLayoutId = Resource.Layout.Main;


        public CircleMenuLayout(Context context, IAttributeSet attrs) :	 base(context)
			{
                init(context, attrs);
                SetPadding(0, 0, 0, 0);

            }


      
        protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec){
            int resWidth = 0;
            int resHeight = 0;

            int width = MeasureSpec.getSize(widthMeasureSpec);
            int widthMode = MeasureSpec.getMode(widthMeasureSpec);

            int height = MeasureSpec.getSize(heightMeasureSpec);
            int heightMode = MeasureSpec.getMode(heightMeasureSpec);

            if (widthMode != MeasureSpec.EXACTLY
                || heightMode != MeasureSpec.EXACTLY)
            {
                resWidth = getSuggestedMinimumWidth();
                resWidth = resWidth == 0 ? getDefaultWidth() : resWidth;

                resHeight = getSuggestedMinimumHeight();
                resHeight = resHeight == 0 ? GetDefaultWidth() : resHeight;
            }
            else
            {
                resWidth = resHeight = Math.Min(width, height);
            }

            setMeasuredDimension(resWidth, resHeight);

            mRadius = Math.max(getMeasuredWidth(), getMeasuredHeight());

            final int count = getChildCount();
            // menu ite
            int childSize = (int)(mRadius * RADIO_DEFAULT_CHILD_DIMENSION);
            // menu item
            int childMode = MeasureSpec.EXACTLY;

            for (int i = 0; i < count; i++)
            {
                final View child = getChildAt(i);

                if (child.getVisibility() == GONE)
                {
                    continue;
                }

                // menu item
                int makeMeasureSpec = -1;

                if (child.getId() == R.id.id_circle_menu_item_center)
                {
                    makeMeasureSpec = MeasureSpec.makeMeasureSpec(
                            (int)(mRadius * RADIO_DEFAULT_CENTERITEM_DIMENSION),
                            childMode);
                }
                else
                {
                    makeMeasureSpec = MeasureSpec.makeMeasureSpec(childSize,
                            childMode);
                }
                child.measure(makeMeasureSpec, makeMeasureSpec);
            }

            mPadding = RADIO_PADDING_LAYOUT * mRadius;
        }


    }
}