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
    class CircleView : View
    {
        static int centerId = 111;
        private int radius;



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

        public CircleView(Context context, int radius, View[] elements)
        {
            // super(context);
            this.radius = radius;

            RelativeLayout.LayoutParams lpView = new RelativeLayout.LayoutParams(
                    RelativeLayout.LayoutParams.MatchParent,
                    RelativeLayout.LayoutParams.MatchParent);
            // this.setLayoutParams(lpView);

            View center = new View(context);
            center.Id = (centerId);
            RelativeLayout.LayoutParams lpcenter = new RelativeLayout.LayoutParams(
                    0, 0);
            lpcenter.AddRule(LayoutRules.CenterHorizontal);
            lpcenter.AddRule(LayoutRules.CenterVertical);
            center.LayoutParameters = (lpcenter);
            this.addView(center);

            this.addView(prepareElementForCircle(elements[0], 0, 0));
            if (elements.Length % 2 == 0)
            {
                this.addView(prepareElementForCircle(elements[elements.Length / 2],
                        0, 2 * radius));
            }
            if (elements.Length > 2)
            {
                for (int i = 1; i <= (elements.Length - 1) / 2; i++)
                {
                    int y = i * 4 * radius / elements.Length;
                    int x = (int)Math.Sqrt(Math.Pow(radius, 2)
                            - Math.Pow((radius - y), 2));
                    this.addView(prepareElementForCircle(elements[i], x, y));
                    this.addView(prepareElementForCircle(elements[elements.Length
                            - i], -x, y));
                }
            }
        }
    }
}