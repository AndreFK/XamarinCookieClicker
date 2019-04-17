using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Timers;


namespace TestXamarin
{
    [Activity(Label = "\""+"CookieClicker"+"\"", Icon ="@drawable/PerfectCookie",MainLauncher = true)]
    public class MainActivity : Activity
    {
        Timer t1 = new Timer();
        TextView text;
        TextView cookies;
        Button menu;        

        int cursors = 0;
        int grannies = 0;
        int farms = 0;
        int counter = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            t1.Interval = 1000;
            t1.Enabled = true;
            t1.Elapsed += Timer_Elapsed;
            t1.Start();

            base.OnCreate(savedInstanceState);       

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            text = FindViewById<TextView>(Resource.Id.text);
            menu = FindViewById<Button>(Resource.Id.Btn);
            cookies = FindViewById<TextView>(Resource.Id.gps);

            menu.Click += Menu_Show;
            
            FindViewById<ImageButton>(Resource.Id.btn).Click += (o, e) =>
            text.Text = (++counter).ToString();

        }

        private void Menu_Show(object sender, EventArgs e)
        {
            PopupMenu men = new PopupMenu(this, menu);
            men.MenuInflater.Inflate(Resource.Menu.popup, men.Menu);
            men.MenuItemClick += (s, arg) =>
             {
                 string message = arg.Item.TitleFormatted.ToString();
                 if(message == "Cursor")
                 {
                     if (cursors == 0 && counter < 4)
                     {
                         message = "No tienes suficientes galletas! Ocupas: 4 galletas";
                     }
                     else if (counter < 4 * cursors * 40)
                     {
                         message = "No tienes suficientes galletas! Ocupas: " + (4*cursors*40).ToString() + " galletas";
                     }
                     else
                     {
                         counter -= 4;
                         cursors++;
                         message += " produciendo " + cursors.ToString() + " gps";
                     }
                 }
                 else if(message == "Abuela")
                 {
                     if (grannies == 0 && counter < 10)
                     {
                         message = "No tienes suficientes galletas! Ocupas: 10 galletas";
                     }
                     else if (counter < 10 * grannies * 40)
                     {
                         message = "No tienes suficientes galletas! Ocupas: " + (10*grannies*40).ToString() + " galletas";
                     }
                     else
                     {
                         counter -= 10;
                         grannies++;
                         message += " produciendo " + (3*grannies).ToString() + " gps";                                                  
                     } 
                 }
                 else if(message == "Granja")
                 {
                         if (farms == 0 && counter < 20)
                         {
                             message = "No tienes suficientes galletas! Ocupas: 20 galletas";
                         }
                         else if(farms > 0 && counter < 20 * farms * 40)
                         {
                             message = "No tienes suficientes galletas! Ocupas: " + (20*farms*40).ToString() + " galletas";
                         }
                         else
                         {
                             counter -= 20;
                             farms++;
                             message += " produciendo " + (farms*5).ToString() + " gps";
                         }
                 }
                 Toast.MakeText(this, message, ToastLength.Short).Show();
             };
            men.Show();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int number = (cursors) + (grannies * 3) + (farms * 5);
            string x = "Gps = " + number.ToString();
            counter += number ;
            RunOnUiThread(() => { text.Text = counter.ToString(); });
            RunOnUiThread(() => { cookies.Text = x; });
        }
    }
}

