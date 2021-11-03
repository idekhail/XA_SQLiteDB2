using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Android.Content;

using AndroidX.AppCompat.App;

namespace XA_SQLiteDB2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource

            SetContentView(Resource.Layout.activity_login);

            var username = FindViewById<EditText>(Resource.Id.username);
            var password = FindViewById<EditText>(Resource.Id.password);

            var login = FindViewById<Button>(Resource.Id.login);
            var create = FindViewById<Button>(Resource.Id.create);
            var update = FindViewById<Button>(Resource.Id.update);
            var close = FindViewById<Button>(Resource.Id.close);
            
            
            login.Click += delegate
            {
                if (!string.IsNullOrEmpty(username.Text) && !string.IsNullOrEmpty(password.Text))
                {
                    SQLiteDB sq = new SQLiteDB();
                    var user = sq.GetUser(username.Text, password.Text);
                    if (user != null)
                    {
                        Intent i = new Intent(this, typeof(UpdateActivity));
                        i.PutExtra("username", user.Username);
                        StartActivity(i);
                    }
                    else
                        Toast.MakeText(this, "Username or Password is Empty", ToastLength.Long).Show();
                }
                else
                    Toast.MakeText(this, "user not phound!!!!", ToastLength.Long).Show();               
            };


            create.Click += delegate
            {
                Intent i = new Intent(this, typeof(CreateActivity));
                StartActivity(i);
            };

            update.Click += delegate
            {
                Intent i = new Intent(this, typeof(UpdateActivity));
                StartActivity(i);
            };
            close.Click += delegate
            {
                System.Environment.Exit(0);
            };
        }        
    }
}