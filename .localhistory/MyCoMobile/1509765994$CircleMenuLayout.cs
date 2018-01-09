using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.OS;
using System.Collections.Generic;
using System.Diagnostics;
using Android.Views.Animations;

namespace MyCoMobile
{
    public class CircleMenuEventArgs : EventArgs
    {

        public int position { get; set; }
        public int velocity { get; set; }

    }

    public class CircleMenuLayout : RelativeLayout
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
        private int mMenuItemLayoutId = Resource.Layout.circle_menu_item;
        private float mLastX;
        private float mLastY;


        public CircleMenuLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {

      //      isFling = true;

      //      base.OnMeasure(
      //    MeasureSpec.MakeMeasureSpec(width, MeasureSpecMode.Exactly),
      //    MeasureSpec.MakeMeasureSpec(height, MeasureSpecMode.Exactly)
      //);
        }


        private void OnAutoFlingFinished(object sender, CircleMenuEventArgs args)
        {
            float angelPerSecond=args.velocity;
            if ((int)Math.Abs(angelPerSecond) < 20)
            {
                CircleMenuLayout.isFling = false;

            }

            CircleMenuLayout.isFling = true;
            CircleMenuLayout.mStartAngle += (angelPerSecond / 30);
            angelPerSecond /= 1.0666F;

            AnimationUtils.CurrentAnimationTimeMillis();
            
        }

        private OnItemClickListener mOnItemClickListener;

     
        public void OnItemClickFinished(object sender, CircleMenuEventArgs e)
        {

        }





        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
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
                resWidth = this.SuggestedMinimumWidth;
                resWidth = resWidth == 0 ? getDefaultWidth() : resWidth;

                resHeight = this.SuggestedMinimumHeight;
                resHeight = resHeight == 0 ? getDefaultWidth() : resHeight;
            }
            else
            {
                resWidth = resHeight = Math.Min(width, height);
            }

            SetMeasuredDimension(resWidth, resHeight);

            var metrics = Resources.DisplayMetrics;
            var widthInDp = ConvertPixelsToDp(metrics.WidthPixels);
            var heightInDp = ConvertPixelsToDp(metrics.HeightPixels);

            mRadius = Math.Max(this.MeasuredHeight, this.MeasuredWidth);//Math.Max(widthInDp, heightInDp);

            int count = ChildCount;
            // menu item
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

                base.OnMeasure(
                MeasureSpec.MakeMeasureSpec(width, MeasureSpecMode.Exactly),
                MeasureSpec.MakeMeasureSpec(height, MeasureSpecMode.Exactly)
            );
        }

        private int ConvertPixelsToDp(float pixelValue)
        {
            var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
            return dp;
        }



        private double DegreeToRadian(double angle)
        {
            return angle * (System.Math.PI / 180.0);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {

            SetPadding(0, 0, 0, 0);

            int layoutRadius = mRadius; //1000

            // Laying out the child views
            int childCount = this.ChildCount;

            int left, top;
            int cWidth = (int)(layoutRadius * RADIO_DEFAULT_CHILD_DIMENSION);

            float angleDelay = 360 / (this.ChildCount ); // (this.ChildCount - 1);

            for (int i = 0; i < childCount; i++)
            {
                View child = GetChildAt(i);
              
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
            //View cView = FindViewById(Resource.Id.circle_menu_item_center);
            //       if (cView != null)
            //{

            //    cView.Click += CView_Click;
            //    Console.WriteLine("Image  View was clicked");

            //}

            //int cl = layoutRadius / 2 - cView.MeasuredWidth / 2;
            //int cr = cl + cView.MeasuredWidth;
            //cView.Layout(cl, cl, cr, cr);


       
        }
       

        
        public bool dispatchTouchEvent(MotionEvent ev)
        {
            float x = ev.GetX();
            float y = ev.GetY();


            switch (ev.Action)
            {
                case MotionEventActions.Down:

                    mLastX = x;
                    mLastY = y;
                    mDownTime = Java.Lang.JavaSystem.CurrentTimeMillis();
                    mTmpAngle = 0;

                    if (isFling)
                    {

                        //RemoveCallbacks(mFlingRunnable);
                        isFling = false;
                        return true;
                    }

                    break;
                case MotionEventActions.Move:


                    float start = getAngle(mLastX, mLastY);

                    float end = getAngle(x, y);

                   // Console.WriteLine("TAG", "start = " + start + " , end =" + end);
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

                    float anglePerSecond2 = mTmpAngle * 1000
                            / (Java.Lang.JavaSystem.CurrentTimeMillis() - mDownTime);

                   // Console.WriteLine("TAG anglePrMillionSecond + "+  mTmpAngle +" =  +"+
                  //  mTmpAngle);

                    if (Math.Abs(anglePerSecond2) > mFlingableValue && !isFling)
                    {
                        AutoFlingEvent AutoFlingPost = new AutoFlingEvent();
                        AutoFlingPost.AutoFlingFinished += OnAutoFlingFinished;
                       // new CircleMenuEventArgs() { velocity = anglePerSecond2 };
                       // AutoFlingHandler.Post(mFlingRunnable = (new AutoFlingEvent.AutoFlingDelegate(anglePerSecond2)));

                        return true;
                    }

                    if (Math.Abs(mTmpAngle) > NOCLICK_VALUE)
                    {
                        return true;
                    }

                    break;
            }
           return true;
          // return base.DispatchTouchEvent(ev);
        }

        public bool onTouchEvent(MotionEvent ev)
        {
            return true;
        }

        public static double Hypotenuse(double a, double b)
        {
            return Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
        }

        private float getAngle(float xTouch, float yTouch)
        {
            double x = xTouch - (mRadius / 2d);
            double y = yTouch - (mRadius / 2d);
            
            return (float)(Math.Asin(y / Hypotenuse(x, y)) * 180 / Math.PI);
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

        public void setMenuItemLayoutId(int LayoutId)
        {
            mMenuItemLayoutId = LayoutId;
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
                    iv.Tag = i;
                    //iv.Click +=   ImageView_Click(view, new List<string> { j.ToString() });

                    iv.Click += (object sender, EventArgs e) =>
                    {
                        Console.WriteLine("ImageView clicked");

                            OnItemClick(i);
                         
                    };

                }

                if (tv != null)
                {
                    tv.Visibility = ViewStates.Visible;
                    tv.Text = mItemTexts[i];
                }


                AddView(view);
            }
        }

   
      


 

        public void setFlingableValue(int intFlingableValue)
        {
            mFlingableValue = intFlingableValue;
        }

        public void setPadding(float mPadding)
        {
            this.mPadding = mPadding;
        }


        public int getDefaultWidth()
        {
            IWindowManager wm = Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            DisplayMetrics outMetrics = new DisplayMetrics();
            wm.DefaultDisplay.GetMetrics(outMetrics);
            return System.Math.Min(outMetrics.WidthPixels, outMetrics.HeightPixels);
        }


    }

    //Subscriber

    public class AutoFlingEvent
    {
        
        public delegate void AutoFlingDelegate(object sender, CircleMenuEventArgs args);
        public event AutoFlingDelegate AutoFlingFinished;

        protected virtual void OnAutoFlingEvent(float velocity) {
            if( AutoFlingFinished != null)
            {
              //  AutoFlingFinished(this, new CircleMenuEventArgs() { velocity = velocity});
            }
        }

        

        

    }


}






