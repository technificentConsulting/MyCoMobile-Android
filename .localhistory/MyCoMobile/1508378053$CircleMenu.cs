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
    class CircleView : LinearLayout
    {
        static int centerId = 111;
        private int radius;

        Context mContext;
        public CircleView(Context context) :	 base(context)
			{
            init(context);

        }
        public CircleView(Context context, IAttributeSet attrs) : base(context, attrs)
			{
            init(context);
        }

        public CircleView(Context context, IAttributeSet attrs, int defStyle) :	base(context, attrs, defStyle)
			{
            init(context);
        }

        private void init(Context ctx)
        {
            mContext = ctx;
        }

        public CircleView(Context context, int radius, List<View> elements) : base(context)
        {
            init(context);
            this.radius = radius;

            RelativeLayout.LayoutParams lpView = new RelativeLayout.LayoutParams(
                    RelativeLayout.LayoutParams.MatchParent,
                    RelativeLayout.LayoutParams.MatchParent);
            this.LayoutParameters=(lpView);

            View center = new View(context);
            center.Id = (centerId);
            RelativeLayout.LayoutParams lpcenter = new RelativeLayout.LayoutParams(
                    0, 0);
            lpcenter.AddRule(LayoutRules.CenterHorizontal);
            lpcenter.AddRule(LayoutRules.CenterVertical);
            center.LayoutParameters = (lpcenter);
            this.AddView(center);

            this.AddView(prepareElementForCircle(elements[0], 0, 0));
            if (elements.Count % 2 == 0)
            {
                this.AddView(prepareElementForCircle(elements[elements.Count / 2],
                        0, 2 * radius));
            }
            if (elements.Count > 2)
            {
                for (int i = 1; i <= (elements.Count - 1) / 2; i++)
                {
                    int y = i * 4 * radius / elements.Count;
                    int x = (int)Math.Sqrt(Math.Pow(radius, 2)
                            - Math.Pow((radius - y), 2));
                    this.AddView(prepareElementForCircle(elements[i], x, y));
                    this.AddView(prepareElementForCircle(elements[elements.Count
                            - i], -x, y));
                }
            }
        }

        private View prepareElementForCircle(View elem, int distX, int distY)
        {
            // RelativeLayout.LayoutParams lp = createNewRelativeLayoutParams();

            elem.Measure(0, 0);
            int deltaX = elem.Width / 2;
            int deltaY = elem.Height / 2;
            //lp.setMargins(distX - deltaX, 0, 0, radius - distY - deltaY);
            //elem.LayoutParameters. (lp);
            return elem;
        }
    


    }
}