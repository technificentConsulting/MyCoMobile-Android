using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.OS;

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
        public static int mFlingableValue = FLINGABLE_VALUE;
        private float mPadding;
        private static double mStartAngle = 0;
        public static string[] mItemTexts;
        public int[] mItemImgs;
        private static int mMenuItemCount;
        private float mTmpAngle;
        private long mDownTime;
        public static bool isFling;
        private int mMenuItemLayoutId = Resource.Layout.Main;



        private float mLastX;
        private float mLastY;

        private AutoFlingRunnable mFlingRunnable;


        internal void setOnMenuItemClickListener(MainActivity mainActivity)
        {
            throw new NotImplementedException();
        }

        public CircleMenuLayout(Context context, IAttributeSet attrs) : base(context)
        {
            init(context, attrs);
            SetPadding(0, 0, 0, 0);
            
        }



        protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            int resWidth = 0;
            int resHeight = 0;

            int width = MeasureSpec.GetSize(widthMeasureSpec);
            int widthMode = (int)MeasureSpec.GetMode(widthMeasureSpec);

            int height = MeasureSpec.GetSize(heightMeasureSpec);
            int heightMode = (int)MeasureSpec.GetMode(heightMeasureSpec);

            if (widthMode != (int)MeasureSpecMode.Exactly
                || heightMode != (int)MeasureSpecMode.Exactly)
            {
                resWidth =  getSuggestedMinimumWidth();
                resWidth = resWidth == 0 ? getDefaultWidth() : resWidth;

                resHeight = getSuggestedMinimumHeight();
                resHeight = resHeight == 0 ? getDefaultWidth() : resHeight;
            }
            else
            {
                resWidth = resHeight = Math.Min(width, height);
            }

            SetMeasuredDimension(resWidth, resHeight);

            mRadius = Math.Max(GetMeasuredWidth(), GetMeasuredHeight());

            int count = getChildCount();
            // menu ite
            int childSize = (int)(mRadius * RADIO_DEFAULT_CHILD_DIMENSION);
            // menu item
            int childMode = (int)MeasureSpecMode.Exactly;

            for (int i = 0; i < count; i++)
            {
                View child = GetChildAt(i);

                if (child.Visibility == ViewStates.Gone)
                {
                    continue;
                }

                // menu item
                int makeMeasureSpec = -1;

                if (child.Id == Resource.Id.circle_menu_item_center)
                {
                    makeMeasureSpec = MeasureSpec.MakeMeasureSpec(
                            (int)(mRadius * RADIO_DEFAULT_CENTERITEM_DIMENSION),
                            MeasureSpec.GetMode(childMode));
                }
                else
                {
                    makeMeasureSpec = (int)MeasureSpec.MakeMeasureSpec(childSize,
                            MeasureSpec.GetMode(childMode));
                }
                child.Measure(makeMeasureSpec, makeMeasureSpec);
            }

            mPadding = RADIO_PADDING_LAYOUT * mRadius;
        }

        private OnMenuItemClickListener mOnMenuItemClickListener;
        public interface OnMenuItemClickListener
        {
            void itemClick(View view, int pos);

            void itemCenterClick(View view);
        }

        public void setOnMenuItemClickListener(
            OnMenuItemClickListener mOnMenuItemClickListener)
        {
            this.mOnMenuItemClickListener = mOnMenuItemClickListener;
        }

        private double DegreeToRadian(double angle)
        {
            return System.Math.PI * angle / 180.0;
        }


        protected void onLayout(bool changed, int l, int t, int r, int b)
        {
            int layoutRadius = mRadius;

            // Laying out the child views
            int childCount = GetChildCount();

            int left, top;
            int cWidth = (int)(layoutRadius * RADIO_DEFAULT_CHILD_DIMENSION);

            float angleDelay = 360 / (GetChildCount() - 1);

            for (int i = 0; i < childCount; i++)
            {
                View child = getChildAt(i);

                if (child.Id == Resource.Id.circle_menu_item_center)
                    continue;

                if (child.Visibility == ViewStates.Gone)
                {
                    continue;
                }

                mStartAngle %= 360;

                float tmp = layoutRadius / 2f - cWidth / 2 - mPadding;

                left = layoutRadius
                        / 2
                        + (int)System.Math.Round(tmp
                                * System.Math.Cos(DegreeToRadian(mStartAngle)) - 1 / 2f
                                * cWidth);
                top = layoutRadius
                        / 2
                        + (int)System.Math.Round(tmp
                                * System.Math.Sin(DegreeToRadian(mStartAngle)) - 1 / 2f
                                * cWidth);

                child.Layout(left, top, left + cWidth, top + cWidth);
                mStartAngle += angleDelay;
            }

            View cView = FindViewById(Resource.Id.circle_menu_item_center);
            if (cView != null)
            {
                //        cView.SetOnClickListener(new IOnClickListener(() =>
                //      {

                //           public void onClick(View v)
                //{

                //    if (mOnMenuItemClickListener != null)
                //    {
                //        mOnMenuItemClickListener.itemCenterClick(v);
                //    }


            }


            int cl = LayoutRadius / 2 - cView.GetMeasuredWidth() / 2;
            int cr = cl + cView.GetMeasuredWidth();
            cView.Layout(cl, cl, cr, cr);


        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            throw new NotImplementedException();
        }

        

        public bool dispatchTouchEvent(MotionEvent ev)
        {
            float x = ev.GetX();
            float y = ev.GetY();
            switch (ev.Action())
            {
                case MotionEventActions.Down:

                    mLastX = x;
                    mLastY = y;
                    mDownTime = Java.Lang.JavaSystem.CurrentTimeMillis();
                    mTmpAngle = 0;

                    if (isFling)
                    {
                        RemoveCallbacks(mFlingRunnable);
                        isFling = false;
                        return true;
                    }

                    break;
                case MotionEventActions.Move:


                    float start = getAngle(mLastX, mLastY);

                    float end = getAngle(x, y);

                    // Log.e("TAG", "start = " + start + " , end =" + end);
                    if (getQuadrant(x, y) == 1 || getQuadrant(x, y) == 4)
                    {
                        mStartAngle += end - start;
                        mTmpAngle += end - start;
                    }
                    else
                    {
                        mStartAngle += start - end;
                        mTmpAngle += start - end;
                    }

                    RequestLayout();

                    mLastX = x;
                    mLastY = y;

                    break;
                case MotionEventActions.Up:

                    float anglePerSecond = mTmpAngle * 1000
                            / (Java.Lang.JavaSystem.CurrentTimeMillis() - mDownTime);

                    // Log.e("TAG", anglePrMillionSecond + " , mTmpAngel = " +
                    // mTmpAngle);

                    if (Math.Abs(anglePerSecond) > mFlingableValue && !isFling)
                    {
                        Post(mFlingRunnable = new AutoFlingRunnable(anglePerSecond));

                        return true;
                    }

                    if (Math.Abs(mTmpAngle) > NOCLICK_VALUE)
                    {
                        return true;
                    }

                    break;
            }
            return base.DispatchTouchEvent(ev);
        }
        public bool onTouchEvent(MotionEvent ev)
        {
            return true;
        }


        private float getAngle(float xTouch, float yTouch)
        {
            double x = xTouch - (mRadius / 2d);
            double y = yTouch - (mRadius / 2d);
            //????
            return (float)(Math.Asin(y / (float)Math.Sqrt(x * y) * 180 / Math.PI));
        }


        private int getQuadrant(float x, float y)
        {
            int tmpX = (int)(x - mRadius / 2);
            int tmpY = (int)(y - mRadius / 2);
            if (tmpX >= 0)
            {
                return tmpY >= 0 ? 4 : 1;
            }
            else
            {
                return tmpY >= 0 ? 3 : 2;
            }

        }
        public void setMenuItemIconsAndTexts(int[] resIds, string[] texts)
        {
            mItemImgs = resIds;
            mItemTexts = texts;

            if (resIds == null && texts == null)
            {
                Console.WriteLine("Exception occurred.");
            }

            mMenuItemCount = resIds == null ? texts.Length : resIds.Length;

            if (resIds != null && texts != null)
            {
                mMenuItemCount = (int)System.Math.Min(resIds.Length, texts.Length);
            }

            addMenuItems();

        }


        public void setMenuItemLayoutId(int mMenuItemLayoutId)
        {
            this.mMenuItemLayoutId = mMenuItemLayoutId;
        }


        private void addMenuItems()
        {
            LayoutInflater mInflater = LayoutInflater.From(this.Context);


            for (int i = 0; i < mMenuItemCount; i++)
            {
                int j = i;
                View view = mInflater.Inflate(mMenuItemLayoutId, this, false);
                ImageView iv = (ImageView)view
                        .FindViewById(Resource.Id.circle_menu_item_image);
                TextView tv = (TextView)view
                        .FindViewById(Resource.Id.circle_menu_item_text);

                if (iv != null)
                {
                    iv.Visibility = ViewStates.Visible;
                    iv.SetImageResource(mItemImgs[i]);
                    //            iv.SetOnClickListener(new OnClickListener()
                    //                {


                    //                    public void onClick(View v)
                    //{

                    //                    if (mOnMenuItemClickListener != null)
                    //                    {
                    //                        mOnMenuItemClickListener.itemClick(v, j);
                    //                    }
                    //                }
                    //				});
                }
                if (tv != null)
                {
                    tv.Visibility = ViewStates.Visible;
                    tv.SetText(mItemTexts[i], TextView.BufferType.Normal);
                }


                AddView(view);
            }
        }


        public void setFlingableValue(int mFlingableValue)
        {
            mFlingableValue = mFlingableValue;
        }


        public void setPadding(float mPadding)
        {
            this.mPadding = mPadding;
        }


        private int getDefaultWidth()
        {
            IWindowManager wm = Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            DisplayMetrics outMetrics = new DisplayMetrics();
            wm.DefaultDisplay.GetMetrics(outMetrics);
            return System.Math.Min(outMetrics.WidthPixels, outMetrics.HeightPixels);
        }

   

     internal class AutoFlingRunnable
        {

            public AutoFlingRunnable(float velocity)
            {
                float angelPerSecond;

                angelPerSecond = velocity;


                if ((int)Math.Abs(angelPerSecond) < 20)
                {
                    CircleMenuLayout.isFling = false;
                    return;
                }
                CircleMenuLayout.isFling = true;
                CircleMenuLayout.mStartAngle += (angelPerSecond / 30);
                angelPerSecond /= 1.0666F;
                Handler handler = new Handler();
                Action action = RequestLayout;

                PostDelayed(action, 30);
                RequestLayout();
            }

            private static void RequestLayout()
            {
                RequestLayout();
            }
        }

    }

   
}






