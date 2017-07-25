using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Lang;

namespace App3
{
    [Activity(Label = "App3", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            Button button2 = FindViewById<Button>(Resource.Id.MyButton2);

            button2.Click += delegate { Click(); };

            //Java.Lang.Thread thread = new Java.Lang.Thread(new Runnable(Click));
        }

        private void Click()
        {
                Java.Lang.Thread.Sleep(10000);
                Java.Lang.Runtime.GetRuntime().Exec("input tap 500 300");
        }

        private string SystemCall()
        {
            Java.Lang.Process process = Java.Lang.Runtime.GetRuntime().Exec("ls");
            StreamReader reader = new StreamReader(process.InputStream);

            return reader.ReadToEnd();
        }
    }
}

