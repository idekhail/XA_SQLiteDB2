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
    [Activity(Label = "UpdateActivity")]
    public class UpdateActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_update);
            var uid = FindViewById<TextView>(Resource.Id.uid);
            var username = FindViewById<EditText>(Resource.Id.username);
            var password = FindViewById<EditText>(Resource.Id.password);
            var mobile = FindViewById<EditText>(Resource.Id.mobile);
            var email = FindViewById<EditText>(Resource.Id.email);

            var update = FindViewById<Button>(Resource.Id.update);
            var logout = FindViewById<Button>(Resource.Id.logout);

            string name = Intent.GetStringExtra("username");
            SQLiteDB sq = new SQLiteDB();
            var user = sq.GetUser(name);
            uid.Text = user.UId + "";
            username.Text = user.Username;
            password.Text = user.Password;
            mobile.Text = user.Mobile;
            email.Text = user.Email;

            update.Click += delegate
            {
                if (username.Text != "")
                {
                    user.Username = username.Text;
                    user.Password = password.Text;
                    user.Mobile = mobile.Text;
                    user.Email = email.Text;
                }

                sq.UpdateUser(user);
                Intent i = new Intent(this, typeof(LoginActivity));
                StartActivity(i);

            };


            logout.Click += delegate
            {
                Intent i = new Intent(this, typeof(LoginActivity));
                StartActivity(i);
            };

        }
    }
}