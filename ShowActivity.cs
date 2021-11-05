using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XA_SQLiteDB2
{
    [Activity(Label = "ShowActivity")]
    public class ShowActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_show);

            var username = FindViewById<TextView>(Resource.Id.username);
            var showme = FindViewById<TextView>(Resource.Id.showme);

            var callme = FindViewById<Button>(Resource.Id.callme);
            var mailme = FindViewById<Button>(Resource.Id.mailme);
            var allusers = FindViewById<Button>(Resource.Id.allusers);
            var logout = FindViewById<Button>(Resource.Id.logout);

            string name = Intent.GetStringExtra("username");
            username.Text = "Welcome Mr. " + name;

            SQLiteDB sq = new SQLiteDB();
            var user = sq.GetUser(name);

            if (user != null)
            {
                 showme.Text = user.UId + "\t\t" + user.Username + "\t\t" + user.Password + "\t\t" + user.Mobile + "\t\t" + user.Email;
            }
            else
                Toast.MakeText(this, "user is null ", ToastLength.Long).Show();

           // make call on user Mobile
            callme.Click += delegate
            {
                var url = Android.Net.Uri.Parse("tel:" + user.Mobile);
                var intent = new Intent(Intent.ActionDial, url);
                StartActivity(intent);
            };

           // send email to user Email
            mailme.Click += delegate
            {
                var email = new Intent(Android.Content.Intent.ActionSend);
                email.PutExtra(Android.Content.Intent.ExtraEmail, new string[] {
                    user.Email, "idekhail@google.com"
                });
                email.PutExtra(Android.Content.Intent.ExtraCc, new string[] {
                        "stdbct@outlook.sa"
                });
                email.PutExtra(Android.Content.Intent.ExtraSubject, "SQLiteDB2");
                email.PutExtra(Android.Content.Intent.ExtraText, "Send Email with Xamarin Android!");
                email.SetType("message/rfc822");
                StartActivity(email);             
            };

           // Display all users in db
            allusers.Click += delegate
            {
                showme.Text = sq.GetAllUsers();
            };

           // Logout to Login Screen
            logout.Click += delegate
            {
                Intent i = new Intent(this, typeof(LoginActivity));
                StartActivity(i);
            };
        }
    }
}