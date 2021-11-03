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
            SetContentView(Resource.Layout.activity_update);
            var username = FindViewById<TextView>(Resource.Id.username);
            var show = FindViewById<TextView>(Resource.Id.show);
          
            var allusers = FindViewById<Button>(Resource.Id.allusers);
            var logout = FindViewById<Button>(Resource.Id.logout);

            string name = Intent.GetStringExtra("username");
            SQLiteDB sq = new SQLiteDB();
            var user = sq.GetUser(name);
            username.Text = "Welcome Mr. " + user.Username;
            string data = user.Username + " " + user.Password + " " + user.Mobile + " " + user.Email;

            show.Text = data;

            allusers.Click += delegate
            {

                show.Text = sq.GetAllUsers();

            };


            logout.Click += delegate
            {
                Intent i = new Intent(this, typeof(LoginActivity));
                StartActivity(i);
            };
        }
    }
}