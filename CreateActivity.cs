using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XA_SQLiteDB2
{
    [Activity(Label = "CreateActivity")]
    public class CreateActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_create);

            var username = FindViewById<EditText>(Resource.Id.username);
            var password = FindViewById<EditText>(Resource.Id.password);
            var mobile = FindViewById<EditText>(Resource.Id.mobile);
            var email = FindViewById<EditText>(Resource.Id.email);

            var create = FindViewById<Button>(Resource.Id.create);
            var cancel = FindViewById<Button>(Resource.Id.cancel);

            create.Click += delegate
            {
                if (username.Text != "" && password.Text != "")
                {
                    var sq = new SQLiteDB();
                    var user = sq.GetUser(username.Text);

                    if (user == null)
                    {
                        var newUser = new SQLiteDB.Users();
                        newUser.Username = username.Text;
                        newUser.Password = password.Text;
                        newUser.Mobile = mobile.Text;
                        newUser.Email = email.Text;

                        sq.InsertUser(newUser);
                        Intent i = new Intent(this, typeof(LoginActivity));
                        StartActivity(i);
                    }
                    else
                    {
                        Toast.MakeText(this, " UserName is found", ToastLength.Short).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, " UserName or Password is empty", ToastLength.Short).Show();
                }
            };


            cancel.Click += delegate
            {
                Intent i = new Intent(this, typeof(LoginActivity));
                StartActivity(i);
            };

        }
    }
}