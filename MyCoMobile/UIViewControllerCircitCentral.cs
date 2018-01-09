using CoreAnimation;
using CoreGraphics;
using Foundation;
using System;
using System.Drawing;
using System.Threading.Tasks;
using UIKit;
using ObjCRuntime;

namespace CircIt
{

    public partial class UIViewControllerCircitCentral : UIViewController
    {

        public CGPath circlepath { set; get; }
        public UILabel sectorLabel { get; set; }


        public UIViewControllerCircitCentral(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UIImageView bg = new UIImageView(View.Frame);
            bg.Image = UIImage.FromFile("images/MainCircItBg.jpg");
     
            View.AddSubview(bg);
        
            RotaryControl spheres = new RotaryControl(new CGRect(-14, this.View.Frame.Height / 2 - 200, 400, 400), Self, 6, bg, this);
            spheres.Layer.ZPosition = 3;

            View.AddSubview(spheres);
        }

        private void navigateToCircit()
        {
            Console.WriteLine("Testing lngpress");
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            if (segue.Identifier == "splashTransition")
            {
                var SendToVC = segue.DestinationViewController;

                UIViewControllerProfile transitionToCtrl = new UIViewControllerProfile(this.Handle);
                {
                    ModalPresentationStyle = UIModalPresentationStyle.Custom;

                };

                PresentViewController(transitionToCtrl, true, null);
            };

        }


    }

    public class RotaryControl : UIControl {

        UIView container;
        int numberOfSections;
        static float deltaAngle;
        private NSObject self;
        float degrees;
        CGPoint bottomCenter;
        public CGAffineTransform startTransform { get; set; }
        public CGPoint point { get; set; }
        UIView bottomCenterCircit;
        public UIViewController parent { get; set; }
        public UIView snapshot { get; set; }
        UITapGestureRecognizer lngPressCircit;
        bool userIsDragging = true;

        public float endingRadians { get; set; }

        public string getSelectedSphere(int position)
        {
            string res = "";
            switch (position)
            {
                case 0:
                    res = "Settings"; //Purple
                    break;
                case 1:
                    res = "CircItBoard"; //Red
                    break;
                case 2:
                    res = "Profile"; //Orange
                        break;
                case 3:
                    res = "Contacts"; //Yellow
                        break;
                case 4:
                    res = "MySphere"; //Green
                        break;
                case 5:
                    res = "CreateCircIt"; //Blue
                    break;

                default:
                    break;
            }

            return res;
        }

        private int _wheelDidChangeNewvalue;

            public int WheelDidChangeValue {

            get { return this._wheelDidChangeNewvalue; }
            set { this._wheelDidChangeNewvalue = value; }

        }

        public class RotaryWheel
        {

            public string newvalue
            {
                get { return "Need to add sector.label.text " + newvalue; }
                set { newvalue = value; }

            }

        }

        public UITouch spheretouch { get; set; }

        public NSMutableArray sectors { get; set; }

        public int currentSector { get; set; }

        UIView getSectorByValue()
            {
                UIView res = container.Subviews[currentSector];                   
                
                return res;
            }


        void buildSectorsEven()
        {
            float fanWidth = (float)Math.PI * 2 / numberOfSections;
            float mid = 0f;


            for (int i = 0; i < numberOfSections; i++)
            {
                SMSector sector = new SMSector();
                sector.midValue = mid;
                sector.minValue = mid - (fanWidth / 2);
                sector.maxValue = mid + (fanWidth / 2);
                sector.sector = i;

                if (sector.maxValue - fanWidth < -Math.PI)
                {
                    mid = (float)Math.PI;
                    sector.midValue = mid;
                    sector.minValue = Math.Abs(sector.maxValue);
                }
                mid -= fanWidth;
                sectors.Add(sector);
                //Console.WriteLine(string.Format("Sector {3} {0} {1} {2}", sector.minValue, sector.midValue, sector.maxValue, i));
            }

        }

        void buildSectorsOdd()
        {
            float fanWidth = (float)Math.PI * 2 / numberOfSections;
            float mid = 0f;

            for (int i = 0; i < numberOfSections; i++)
            {
                SMSector sector = new SMSector();
                sector.midValue = mid;
                sector.minValue = mid - (fanWidth / 2);
                sector.maxValue = mid + (fanWidth / 2);
                sector.sector = i;
                mid -= fanWidth;
                if (mid < -Math.PI)
                {
                    mid = -mid;
                    mid -= fanWidth;
                }

                sectors.Add(sector);
                //Console.WriteLine(string.Format("cl is {0}", sector));
            }
        }

        public void rotate()
        {
            CGAffineTransform t = CGAffineTransform.Rotate(container.Transform, -degrees);
            container.Transform = t;
        }

        public RotaryControl(CGRect frame, NSObject self, int sectionsNumber, UIView theBgContainer, UIViewController ctrl)
        {
            this.self = self;
            this.Frame = frame;
            this.numberOfSections = sectionsNumber;
            this.degrees = 2 * (float)Math.PI / (float)sectionsNumber;
            this.Tag = 97;
            parent = ctrl;
            DrawCircle();
        }

        //public void DrawRect(float rotatevalue)
        //{
        //    UIView vw = new UIView(new CGRect(new CGPoint(container.Center.X-40, container.Center.Y-40), new CGSize(100, 100)));
        //    vw.BackgroundColor = UIColor.Yellow;

        //    UIButton img = new UIButton(new CGRect(10, 10, 80, 90));
        //    img.SetImage(UIImage.FromFile("images/circits/PurpleSphere.png"), UIControlState.Normal);
        //    img.UserInteractionEnabled = true;

        //    img.TouchUpInside += Img_TouchUpInside;
        //    img.Transform = CGAffineTransform.MakeRotation(rotatevalue);
        //    img.Layer.ZPosition = 1;
        //    vw.Add(img);
        //        container.Add(vw);
            
        //    }

        private void Img_TouchUpInside(object sender, EventArgs e)
        {
            Console.WriteLine("In btn click for single tester ");
        }

        public float imgRotation { get; set; }
            // DrawCircle 
        public void DrawCircle()
        {
            container = new UIView(new CGRect(0, 0, this.Frame.Size.Width, this.Frame.Size.Height));
           // container.Transform.Rotate(.52f);
            container.Tag = 96;

            float angle = 2 * (float)Math.PI / numberOfSections;

            // 7.1 - Add images
            for (int i = 0; i < numberOfSections; i++)
            {

                UIButton btnCircitSphere = new UIButton(new CGRect(new CGPoint(0, 0), new CGSize(114, 124)));

                //Position items using the anchor
                btnCircitSphere.Layer.AnchorPoint = new CGPoint(1.25f, 1.25f);
                btnCircitSphere.Layer.Position = new CGPoint(container.Frame.Size.Width / 2.0 -
                container.Frame.X, container.Frame.Size.Height / 2.0 - container.Frame.Y);
                btnCircitSphere.Enabled = true;
                btnCircitSphere.UserInteractionEnabled = true;
                
                btnCircitSphere.Tag = i;
                btnCircitSphere.Alpha = 0.8f;
                btnCircitSphere.Transform = CGAffineTransform.MakeRotation(angle * i);

                //btnCircitSphere.Transform = CGAffineTransform.Rotate(CGAffineTransform.MakeTranslation(btnCircitSphere.Frame.X, btnCircitSphere.Frame.Y), (0));
                btnCircitSphere.Layer.AnchorPoint = new CGPoint(1.25f, 1.25f);
                //   getSectorByValue().Transform =  CGAffineTransform.Scale(CGAffineTransform.MakeTranslation(snapshot.Frame.X+8, snapshot.Frame.Y),1.5f,1.5f);
                UIImageView img = new UIImageView(new CGRect(0,0,btnCircitSphere.Frame.Width, btnCircitSphere.Frame.Height));

                //Purple settings
                 if (i == 0)
                {
                    img.Image = UIImage.FromFile("images/circits/PurpleSphere.png");

                }//Red Circuit Board
                else if (i == 1)
                {

                    img.Image = UIImage.FromFile("images/circits/RedSphere.png");

                }
                //Organge Profile
                else if (i == 2)
                {
                    img.Image = UIImage.FromFile("images/circits/OrangeSphere.png");


                }//Yellow Contacts
                else if (i == 3)
                {
                    img.Image = UIImage.FromFile("images/circits/YellowSphere.png");


                }//Green My Sphere
                else if (i == 4)
                {
                    img.Image = UIImage.FromFile("images/circits/GreenSphere.png");


                }//Blue Create CircIt
                else if (i == 5)
                {
                    img.Image = UIImage.FromFile("images/circits/BlueSphere.png");

                }


                // btnCircitSphere.Layer.Transform = CATransform3D.MakeRotation((nfloat)(0), 0.0f, 0.0f, 0.0f);

                btnCircitSphere.SetImage(img.Image, UIControlState.Normal);

                
                container.Add(btnCircitSphere);
            }

              container.UserInteractionEnabled = false;
                    

                this.AddSubview(container);

                sectors = new NSMutableArray((nuint)numberOfSections);
                if (numberOfSections % 2 == 0)
                {
                    buildSectorsEven();
                }
                else
                {
                    buildSectorsOdd();
                }


         //   DrawRect(0);

            }



        public float CalculateDistanceFromCenter()
        {

            CGPoint center = new CGPoint(this.Bounds.Size.Width / 2, this.Bounds.Size.Height / 2);
            float dx = (float)(point.X - center.X);
            float dy = (float)(point.Y - center.Y);

            return (float)Math.Sqrt(dx * dx + dy * dy);

        }


        public override bool BeginTracking(UITouch uitouch, UIEvent uievent)
        {

            CGPoint touchPoint = uitouch.LocationInView(container);
            float dist = CalculateDistanceFromCenter();


            ////Handle tap gesture
            if (uitouch.TapCount == 2)  //centerSphereWasSelected(touchPoint, uitouch.View)
            {
                // the touch event happened inside the UIView imgTouchMe.
                // HandleTappedGesture(lngPressCircit);
                HandleTappedEvent(currentSector);
                return false;

            }

          //  ResetInflate();

            nfloat dx = touchPoint.X - container.Center.X;
            nfloat dy = touchPoint.Y - container.Center.Y;

            deltaAngle = (float)Math.Atan2(dy, dx);

            //TODO: Changes angle of image
            startTransform = container.Transform;


            return true;
        }

        private void HandleTappedEvent(nint tag)
        {
            
                UIStoryboard mainboard = UIStoryboard.FromName("Main", null);
                UIViewController ctrl = new UIViewController();

                switch (tag)
                {
                    case 0:  //Purple => Settings
                        Console.WriteLine("In btn click for sphere 1");
                        userIsDragging = false;

                        ctrl = (UIViewController)mainboard.InstantiateViewController("Settings");
                        break;
                    case 1: //Red  => CircItBoard   
                        Console.WriteLine("In btn click for sphere 2");
                        userIsDragging = false;

                        ctrl = (UIViewController)mainboard.InstantiateViewController("CircitBoard");
                        break;
                    case 2:  //Orange => Profile
                        Console.WriteLine("In btn click for sphere 3");
                        userIsDragging = false;

                        ctrl = (UIViewController)mainboard.InstantiateViewController("Profile");
                        break;
                    case 3://Yellow => Contacts
                        Console.WriteLine("In btn click for sphere 4");
                        userIsDragging = false;

                        ctrl = (UIViewController)mainboard.InstantiateViewController("Contacts");
                        break;
                    case 4://Green => MySphere
                        Console.WriteLine("In btn click for sphere 5");
                        userIsDragging = false;

                        ctrl = (UIViewController)mainboard.InstantiateViewController("MySphere");
                        break;
                    case 5: //Blue => CreateCircIt
                        Console.WriteLine("In btn click for sphere 6");
                        userIsDragging = false;

                        ctrl = (UIViewController)mainboard.InstantiateViewController("SendTo");
                    break;

                    default:
                        break;

                }

                parent.PresentViewController(ctrl, true, null);
            
        }

        public override bool ContinueTracking(UITouch uitouch, UIEvent uievent)
        {

            if (uitouch.TapCount == 2)  //centerSphereWasSelected(touchPoint, uitouch.View)
            {
                // the touch event happened inside the UIView imgTouchMe.
                //HandleTappedEvent(currentSector);
                return false;

            }
            float radians = (float)Math.Atan2(container.Transform.yx, container.Transform.xx);
            Console.WriteLine(String.Format("Radian is {0}", radians));

            CGPoint pt = uitouch.LocationInView(this);

            float dist = CalculateDistanceFromCenter();

            //if (dist < 400 || dist > 500)
            //{
            //    Console.WriteLine(String.Format("Ignoring tap {0} {1}", pt.X, pt.Y));
            //    return false;
            //}

            nfloat dx = pt.X - container.Center.X;
            nfloat dy = pt.Y - container.Center.Y;

            float angle = (float)Math.Atan2(dy, dx);
            float angleDiff = deltaAngle - angle;

            container.Transform = CGAffineTransform.Rotate(startTransform, -angleDiff);

            imgRotation += angleDiff;

            return true;

        }

        public override void EndTracking(UITouch uitouch, UIEvent uievent)
        {

            if (uitouch.TapCount != 2)  //centerSphereWasSelected(touchPoint, uitouch.View)
            {
         

           
            float radians = (float)Math.Atan2(container.Transform.yx, container.Transform.xx);
            CGPoint touchPoint = uitouch.LocationInView(this);
          


            float newVal = 0.0f;

            for (nuint i = 0; i < sectors.Count; i++)
            {
                var s = sectors.GetItem<SMSector>(i);
                Console.WriteLine("Ending randians value {0} : Sector values {1} ", radians, s.description());

                //CHECK TO SEE IF WHICH SPHERE IS LOCATED IN THE BOTTOM CENTER
                //BY COMPARING SECTOR MIN/MAX


                if (s.minValue >= -2.617994 && s.maxValue <= -1.570796)
                {
                    if ((s.maxValue > radians || s.minValue < radians))
                    {
                        // 5 - Find the quadrant (positive or negative)
                        if (radians > 0)
                        {
                            newVal = radians - (float)Math.PI;

                        }
                        else
                        {
                            newVal = (float)Math.PI + radians;
                        }
                        currentSector = s.sector;

                    }

                    // 6 - All non-anomalous cases
                    else if (radians > s.minValue && radians < s.maxValue)
                    {
                        newVal = radians - s.midValue;
                        currentSector = s.sector;

                    }
                }
                foreach (UIView btnViews in container.Subviews)
                {
                    Console.WriteLine("Tagged with " + btnViews.Tag + " location is X=>" + btnViews.Frame.X + " Y=>" + btnViews.Frame.Y);

                    //btnViews.Transform =  CGAffineTransform.Rotate(CGAffineTransform.MakeTranslation(0, 0),0);
                  }   

            }   //btnCircitSphere.Transform = CGAffineTransform.MakeRotation(newVal);


            // }
            endingRadians = radians;
                //    currentSector = 0;
       


            //    BeginAnimations("rotate");
            //  SetAnimationDuration(0.4);
            WheelDidChangeValue = currentSector;
            snapshot = getSectorByValue();
            getSectorByValue().Alpha = 1.0f;
            //getSectorByValue().Transform =  CGAffineTransform.Scale(CGAffineTransform.MakeTranslation(snapshot.Frame.X+8, snapshot.Frame.Y),1.5f,1.5f);
            //getBottomCenterSphere(touchPoint);
            container.Transform = CGAffineTransform.Rotate(container.Transform, newVal);
            //container.Subviews[0].Transform = CGAffineTransform.MakeRotation(-this.endingRadians);

            // ResetImgRotation();
            //container.Transform = t;

            //Console.WriteLine(string.Format("Currently selected sphere is named {0}", getSelectedSphere(currentSector)));

                //      CommitAnimations();
            }
        }
        //}

        //public override void TouchesMoved(NSSet touches, UIEvent evt)
        //{
        //    userIsDragging = true;


        //    base.TouchesMoved(touches, evt);
        //}

        private void ResetImgRotation()
            {
                for (var r = 0; r < container.Subviews.Length; r++)
                {

                    float radians = (float)Math.Atan2(container.Subviews[r].Subviews[0].Transform.yx, container.Subviews[r].Subviews[0].Transform.xx);
                    float degrees = (float)( radians * (180 / Math.PI) +20);

                    //  UILabel lbl = new UILabel(new CGRect(new CGPoint(10, 0), new CGSize(100, 100)));
                    //   lbl.Text = angle.ToString();

                    //if (container.Subviews[r].Subviews[0].Subviews.Length == 0)
                    //{
                    //    container.Subviews[r].Subviews[0].AddSubview(lbl);
                    //}
                    //else
                    //{
                    //    UILabel lbl0 = (UILabel)container.Subviews[r].Subviews[0].Subviews[0];
                    //    lbl0.Text = angle.ToString();
                    //}
                    float turnAmt = degrees * 2;

                   // container.Subviews[r].Subviews[0].Transform = CGAffineTransform.MakeRotation(-8);


                }
            }

        private void ResetInflate()
        {
            if(snapshot != null) { 
                 container.Subviews[snapshot.Tag] = snapshot;
            }
        }


        public class SMSector : NSObject
        {

            public float minValue { get; set; }
            public float maxValue { get; set; }
            public float midValue { get; set; }

            public int sector { get; set; }

            public string description()
            {
                return string.Format("{0}| {1}, {2}, {3}", this.sector, this.minValue, this.midValue, this.maxValue);
            }


        }

    }

}