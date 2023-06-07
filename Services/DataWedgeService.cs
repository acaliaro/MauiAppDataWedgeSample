#if ANDROID
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Util;
using Java.IO;
#endif

namespace MauiAppDataWedgeSample.Services
{
    public class DataWedgeService : IDataWedgeService
    {

        private const string PROFILE_NAME_SCANNER = "MauiAppDataWedgeSample";

        private const string ACTION_DATAWEDGE_FROM_6_2 = "com.symbol.datawedge.api.ACTION";
        private const string EXTRA_ENABLE_DATAWEDGE = "com.symbol.datawedge.api.ENABLE_DATAWEDGE";
        private const string EXTRA_SWITCH_TO_PROFILE = "com.symbol.datawedge.api.SWITCH_TO_PROFILE";
        private const string EXTRA_GET_VERSION_INFO = "com.symbol.datawedge.api.GET_VERSION_INFO";
        private const string EXTRA_EMPTY = "";
        private const string EXTRA_SEND_RESULT = "SEND_RESULT";

#if ANDROID
        public static void EnableDataWedge(Context context, bool isEnabled)
        {

            Intent i = new();
            i.SetAction(ACTION_DATAWEDGE_FROM_6_2);
            i.PutExtra(EXTRA_ENABLE_DATAWEDGE, isEnabled);
            context.SendBroadcast(i);
        }
#endif
#if ANDROID

        public static void SwitchToProfile(Context context, string profile)
        {
            Intent i = new();
            i.SetAction(ACTION_DATAWEDGE_FROM_6_2);
            i.PutExtra(EXTRA_SWITCH_TO_PROFILE, profile);
            context.SendBroadcast(i);
        }
#endif

#if ANDROID
        public static void GetVersionInfo(Context context)
        {
            SendDataWedgeIntentWithExtra(context, ACTION_DATAWEDGE_FROM_6_2, EXTRA_GET_VERSION_INFO, EXTRA_EMPTY, true);
        }

        public static void SendDataWedgeIntentWithExtra(Context context, string action, string extraKey, Bundle extras)
        {
            Intent dwIntent = new();
            dwIntent.SetAction(action);
            dwIntent.PutExtra(extraKey, extras);
            context.SendBroadcast(dwIntent);
        }
#endif

#if ANDROID
        public static void SendDataWedgeIntentWithExtra(Context context, string action, string extraKey, string extraValue, bool extraSendResult)
        {

            Intent dwIntent = new();
            dwIntent.SetAction(action);
            dwIntent.PutExtra(extraKey, extraValue);
            if (extraSendResult)
                dwIntent.PutExtra(EXTRA_SEND_RESULT, "true");
            context.SendBroadcast(dwIntent);
        }
#endif

#if ANDROID
        public static void CopyFromAssetToDirectory(Context context, string assetFileName)
        {

            // Simulscan e Datawedge hanno directory di profilo diverse
            string autoImportDir = "/enterprise/device/settings/datawedge/autoimport/";
            AssetManager assets = context.Assets;

            // create a File object for the parent directory
            Java.IO.File outputDirectory = new(autoImportDir);

            string temporaryFileName = Path.GetFileNameWithoutExtension(assetFileName) + ".tmp";
            string finalFileName = Path.GetFileName(assetFileName);

            //// create a temporary File object for the output file
            Java.IO.File outputFile = new(outputDirectory, temporaryFileName);
            Java.IO.File finalFile = new(outputDirectory, finalFileName);
            // attach the OutputStream to the file object
            FileOutputStream fos = new(outputFile);

            using (BinaryReader br = new(assets.Open(assetFileName)))
            {

                bool error = false;
                while (error == false)
                {
                    try
                    {
                        var bytes = br.ReadBytes(6000);

                        if (bytes.Length == 0)
                            error = true;
                        else
                            fos.Write(bytes, 0, bytes.Length);
                    }
                    catch (EndOfStreamException)
                    {
                        error = true;
                    }
                }

                br.Close();
                try
                {
                    fos.Close();
                }
                catch
                {
                }
                finally
                {
                    fos = null;
                    //set permission to the file to read, write and exec.
                    outputFile.SetExecutable(true, false);
                    outputFile.SetReadable(true, false);
                    outputFile.SetWritable(true, false);
                    //rename the file
                    outputFile.RenameTo(finalFile);
                }
            }

        }

#endif
        public void EnableProfile()
        {

#if ANDROID
            EnableDataWedge(Platform.CurrentActivity.ApplicationContext, true);
            SwitchToProfile(Platform.CurrentActivity.ApplicationContext, PROFILE_NAME_SCANNER);
#endif
        }

        public void DisableProfile()
        {
#if ANDROID
            EnableDataWedge(Platform.CurrentActivity.ApplicationContext, false);
#endif
        }

        public void CopyFile()
        {
#if ANDROID
            if (Build.Manufacturer.StartsWith("Zebra"))
            {
                try
                {

                    CopyFromAssetToDirectory(Platform.CurrentActivity.ApplicationContext, "DataWedge/dwprofile_" + PROFILE_NAME_SCANNER + ".db");
                }
                catch (Exception ex)
                {
                    Log.Error("DataWedge", ex.Message);
                }
            }
#endif
        }
    }
}