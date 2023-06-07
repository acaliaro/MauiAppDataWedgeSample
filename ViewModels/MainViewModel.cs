using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MauiAppDataWedgeSample.Messages;
using MauiAppDataWedgeSample.Services;

namespace MauiAppDataWedgeSample.ViewModels
{
    public partial class MainViewModel : ObservableObject, IRecipient<DataWedgeMessage>
    {
        private IDataWedgeService _dataWedgeService;

        [ObservableProperty]
        private bool isDataWedgeEnabled;

        [ObservableProperty]
        private string barcode;

        [ObservableProperty]
        private bool isCopying;

        public void Receive(DataWedgeMessage message)
        {
            Barcode = message.Value;
        }

        partial void OnIsDataWedgeEnabledChanged(bool value)
        {
            if (value)
                _dataWedgeService.EnableProfile();
            else
                _dataWedgeService.DisableProfile();
        }

        public MainViewModel(IDataWedgeService dataWedgeService)
        {
            _dataWedgeService = dataWedgeService;
            _dataWedgeService.DisableProfile();
        }

        [RelayCommand]
        void CopyProfileToDevice()
        {
            try
            {
                IsCopying = true;

                _dataWedgeService.CopyFile();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            finally { IsCopying = false; }
        }


        [RelayCommand]
        void Appearing()
        {
            try
            {

                //_dataWedgeService.EnableProfile();

                WeakReferenceMessenger.Default.Register(this);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        [RelayCommand]
        void Disappearing()
        {
            try
            {
                //_dataWedgeService.DisableProfile();

                WeakReferenceMessenger.Default.Unregister<DataWedgeMessage>(this);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }
}
