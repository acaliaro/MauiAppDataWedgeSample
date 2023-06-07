using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using MauiAppDataWedgeSample.Platforms.Android.Utilities;

namespace MauiAppDataWedgeSample;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{

    ScanReceiver _scanReceiver;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        System.Diagnostics.Debug.WriteLine("OnCreate", "MainActivity");

        _scanReceiver = new ScanReceiver();

    }

    protected override void OnResume()
    {
        base.OnResume();

        // Register the broadcast receiver
        IntentFilter filterScan = new(ScanReceiver.IntentAction);
        filterScan.AddCategory(ScanReceiver.IntentCategory);
        RegisterReceiver(_scanReceiver, filterScan);
    }

    protected override void OnPause()
    {
        base.OnPause();
        UnregisterReceiver(_scanReceiver);

    }
}
