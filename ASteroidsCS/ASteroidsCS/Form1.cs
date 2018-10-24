using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASteroidsCS
{
    public partial class Form1 : Form
    {
        //This is the x and y of the players "centre" point where all calculations are based off of
        int x = 50;
        int y = 50;

        //used to determine tick rate
        static decimal fps = 60;

        //Variables for detecting key presses
        bool wpress;
        bool apress;
        bool spress;
        bool dpress;

        //misc maths variables
        int r = 0;        //Roataion from ghost image
        int rv = 5;       //Rotation multiplier
        //decimal v = 0;    //Velocity
        //decimal maxv = 7; //Max velocity
        // AHH: I changed these to doubles to prevent having to do frequent 
        //      casting gymnastics and just do the cast at the last moment
        double v = 0;    //Velocity
        double maxv = 7; //Max velocity

        int length = 20; //Length of player

        //Drawing stuff
        Graphics formGraphics; //Graphics layer?
        Pen playerpen = new Pen(Color.Blue, 1); //Blue pen
        Pen debugpen = new Pen(Color.Red, 1); //Red pen
        Pen debugpen2 = new Pen(Color.Green, 1); //Green pen
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load graphics
            formGraphics = CreateGraphics();

            //Makes the tickspeed relevant to set fps
            GameTick.Interval = (int)(1 / (fps / 1000));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Getting key presses and storing htem in the respective variables
            switch (e.KeyCode)
            {
                case Keys.W:
                    wpress = true;
                    break;
                case Keys.A:
                    apress = true;
                    break;
                case Keys.S:
                    spress = true;
                    break;
                case Keys.D:
                    dpress = true;
                    break;
            }
            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //Getting key presses and storing htem in the respective variables
            switch (e.KeyCode)
            {
                case Keys.W:
                    wpress = false;
                    break;
                case Keys.A:
                    apress = false;
                    break;
                case Keys.S:
                    spress = false;
                    break;
                case Keys.D:
                    dpress = false;
                    break;
            }
        }

        private void GameTick_Tick(object sender, EventArgs e)
        {
            //Radians stuff
            double ToRadians = Math.PI / 180;
            double ToDegrees = 180 / System.Math.PI;
            double rr;
            //Rotation direction
            int rdir;

            //'Checking w and s
            //If wpress = True And spress = True Then
            //    'Nothing
            //ElseIf wpress = True Then
            //    v = v + 0.1
            //ElseIf spress = True Then
            //    v = v - 0.1
            //End If
            if (wpress == true && spress == true)
            {
                //Nothing
            }
            else if (wpress == true)
            {
                //v = v + (decimal)(0.1); // See comment at the declaration of v
                v = v + 0.1;
            }
            else if (spress == true)
            {
                v = v - 0.1;
            }

            //Keeping v in acceptable values
            //If v > maxv Then v = maxv
            //If v< 0 Then v = 0
            if (v > maxv) v = maxv;
            if (v < 0) v = 0;
            
            //Checking a and d
            //If dpress = True And apress = True Then
            //    rdir = 0
            //ElseIf dpress = True Then
            //    rdir = -1
            //ElseIf apress = True Then
            //    rdir = 1
            //Else
            //    rdir = 0
            //End If
            if (dpress == true && apress == true)
            {
                rdir = 0;
            }
            else if (dpress == true)
            {
                rdir = -1;
            }
            else if (apress == true)
            {
                rdir = 1;
            }
            else
            {
                rdir = 0;
            }

            //Updating rotation value
            //r = r + rdir * rv
            r = r + rdir * rv;

            //Keeping r in acceptable values
            //If r > 360 Then r = r - 360
            //If r< 0 Then r = r + 360
            if (r > 360) r = r - 360;
            if (r < 360) r = r + 360;

            //Making radians r
            //rr = r * ToRadians
            rr = r * ToRadians;

            //Different points used for drawing
            //Dim forward As Point
            //Dim back As Point
            //Dim centre As Point
            //Dim p1 As Point 'These two are used for the left and right point to make the shape
            //Dim p2 As Point
            Point forward;
            Point back;
            Point centre;
            Point p1; //These two are used for the left and right point to make the shape
            Point p2;

            //formGraphics.Clear(Color.White)
            formGraphics.Clear(Color.White);

            //Updating centrepint using v and r
            //x = x + v * Math.Cos(90 * ToRadians - rr)
            //y = y + v * Math.Sin(90 * ToRadians - rr)
            x = (int)(x + v * Math.Cos((90 * ToRadians) - rr));
            y = (int)(y + v * Math.Sin((90 * ToRadians) - rr));

            //Updating labels
            //Label5.Text = wpress
            //Label6.Text = apress
            //Label7.Text = spress
            //Label8.Text = dpress
            //Label4.Text = r
            //Label1.Text = x
            //Label2.Text = y
            Label5.Text = wpress.ToString();
            Label6.Text = apress.ToString();
            Label7.Text = spress.ToString();
            Label8.Text = dpress.ToString();
            Label4.Text = r.ToString();
            Label1.Text = x.ToString();
            Label2.Text = y.ToString();


            label10.Text = v.ToString();


            //'Setting the centre
            //centre.X = x
            //centre.Y = y
            centre = new Point(x, y);

            //'Setting thetopand bottom points of the shape
            //forward.X = x + (0.5 * length * Math.Sin(rr))
            //forward.Y = y + (0.5 * length * Math.Cos(rr))
            //back.X = x - (0.5 * length * Math.Sin(rr))
            //back.Y = y - (0.5 * length * Math.Cos(rr))
            forward = new Point(
                (int)(x + (0.5 * length * Math.Sin(rr))), 
                (int)(y + (0.5 * length * Math.Cos(rr))));
            back = new Point(
                (int)(x - (0.5 * length * Math.Sin(rr))), 
                (int)(y - (0.5 * length * Math.Cos(rr))));

            //Setting the two edge points
            //p1.X = x + length * Math.Sin(rr + 160 * ToRadians)
            //p1.Y = y + length * Math.Cos(rr + 160 * ToRadians)
            //p2.X = x + length * Math.Sin(rr + 200 * ToRadians)
            //p2.Y = y + length * Math.Cos(rr + 200 * ToRadians)
            p1 = new Point(
                (int)(x + length * Math.Sin(rr + 160 * ToRadians)), 
                (int)(y + length * Math.Cos(rr + 160 * ToRadians)));
            p2 = new Point(
                (int)(x + length * Math.Sin(rr + 200 * ToRadians)),
                (int)(y + length * Math.Cos(rr + 200 * ToRadians)));

            //Drawing the shape
            //formGraphics.DrawLine(playerpen, forward, p1)
            //formGraphics.DrawLine(playerpen, p1, back)
            //formGraphics.DrawLine(playerpen, forward, p2)
            //formGraphics.DrawLine(playerpen, p2, back)
            formGraphics.DrawLine(playerpen, forward, p1);
            formGraphics.DrawLine(playerpen, p1, back);
            formGraphics.DrawLine(playerpen, forward, p2);
            formGraphics.DrawLine(playerpen, p2, back);


            //Debug line so i can see the shape if i muck up
            //formGraphics.DrawLine(debugpen, forward, back)
            formGraphics.DrawLine(debugpen, forward, back);
        }
    }
}
