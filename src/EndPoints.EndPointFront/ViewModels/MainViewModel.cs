using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using EndPoints.Dto;
using EndPoints.EndPointFront.Client;
using System.Configuration;
using System.Xml.Linq;

namespace EndPoints.EndPointFront.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Window _window;
        private string _numCommand;
        private EndPointClient _endPointClient;
        private Visibility _tela1;
        private Visibility _tela2;
        private Visibility _tela5;
        private Visibility _tela4;
        private Visibility _tela3;
        private string _meterFirmwareVersion;
        private string _setSerialNumber;
        private string _meterModelIdSelected;
        private string _switchState;
        private int _meterNumber;
        private string _editPoint;
        private EndPointGyrDto endPointUpdate;
        private List<EndPointGyrDto> _endPoints;

        public MainViewModel(Window window)
        {
            _window = window;   
            _endPointClient = new EndPointClient();

            Tela1 = Visibility.Collapsed;
            Tela2 = Visibility.Collapsed;
            Tela3 = Visibility.Collapsed;
            Tela4 = Visibility.Collapsed;
            Tela5 = Visibility.Collapsed;

            SendCommand = new RelayCommand(ChangeScreen, () => true, true);
            SaveCommand = new RelayCommand(Save, () => true, true);
            SearchCommand = new RelayCommand(Search, () => true, true);
            UpdateCommand = new RelayCommand(Update, () => true, true);
            RemoveCommand = new RelayCommand(Remove, () => true, true); 
        }


        public void Save()
        {
            try
            {
                var end = new EndPointGyrDto
                {
                    MeterFirmwareVersion = MeterFirmwareVersion,
                    MeterModelId = (EnumMeterModel)Enum.Parse(typeof(EnumMeterModel), MeterModelIdSelected),
                    MeterNumber = MeterNumber,
                    SerialNumber = SerialNumber,
                    SwitchState = (EnumSwitchState)Enum.Parse(typeof(EnumSwitchState), SwitchStateSelected)
                };

                _endPointClient.Save(end);

                MessageBox.Show("Saved with sucess");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public void Search()
        {
            try
            {
                endPointUpdate = _endPointClient.GetBySerialNumber(EditPoint);
                
                MeterFirmwareVersion = endPointUpdate.MeterFirmwareVersion; 
                SerialNumber = endPointUpdate.SerialNumber;
                MeterModelIdSelected = endPointUpdate.MeterModelId.ToString();  
                MeterNumber = endPointUpdate.MeterNumber;   

                SwitchStateSelected = endPointUpdate.SwitchState.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Update()
        {
            try
            {
                endPointUpdate.SwitchState = (EnumSwitchState)Enum.Parse(typeof(EnumSwitchState), SwitchStateSelected);
                
                _endPointClient.Update(endPointUpdate);

                MessageBox.Show("Updated with sucess");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }   
        }

        public void Remove()
        {
            try
            {
                _endPointClient.Delete(EditPoint);
                MessageBox.Show("Deleted with sucess");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void GetAll()
        {
            try
            {
                EndPoints = _endPointClient.GetAll();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }   
        public void ChangeScreen()
        {
            switch (NumComand)
            {
                case "1":
                    Tela1 = Visibility.Visible;
                    Tela2 = Visibility.Collapsed;
                    Tela3 = Visibility.Collapsed;
                    Tela4 = Visibility.Collapsed;
                    Tela5 = Visibility.Collapsed;
                    break;
                case "2":
                    Tela1 = Visibility.Collapsed;
                    Tela2 = Visibility.Visible;
                    Tela3 = Visibility.Collapsed;
                    Tela4 = Visibility.Collapsed;
                    Tela5 = Visibility.Collapsed;
                    break;
                case "3":
                    Tela1 = Visibility.Collapsed;
                    Tela2 = Visibility.Collapsed;
                    Tela3 = Visibility.Visible;
                    Tela4 = Visibility.Collapsed;
                    Tela5 = Visibility.Collapsed;
                    break;
                case "4":
                    Tela1 = Visibility.Collapsed;
                    Tela2 = Visibility.Collapsed;
                    Tela3 = Visibility.Collapsed;
                    Tela4 = Visibility.Visible;
                    Tela5 = Visibility.Collapsed;
                    GetAll();
                    break;
                case "5":
                    Tela1 = Visibility.Collapsed;
                    Tela2 = Visibility.Collapsed;
                    Tela3 = Visibility.Collapsed;
                    Tela4 = Visibility.Collapsed;
                    Tela5 = Visibility.Visible;
                    break;
                default:
                    Tela1 = Visibility.Collapsed;
                    Tela2 = Visibility.Collapsed;
                    Tela3 = Visibility.Collapsed;
                    Tela4 = Visibility.Collapsed;
                    Tela5 = Visibility.Collapsed;
                    break;
            }

            if(NumComand == "5")
            {
                MeterFirmwareVersion = "";
                SerialNumber = "";
                MeterModelIdSelected = "";
                MeterNumber = 0;

                SwitchStateSelected = "";
            }

            if(NumComand == "6")
            {
                if (MessageBox.Show("Do you want close the application?","Exit",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _window.Close();
                }
            }

        }

        public string NumComand
        {
            get => _numCommand;
            set => Set(ref _numCommand, value);
        }

        public string EditPoint
        {
            get => _editPoint;
            set => Set(ref _editPoint, value);
        }

        public string MeterFirmwareVersion
        {
            get => _meterFirmwareVersion;
            set => Set(ref _meterFirmwareVersion, value);
        }   

        public string SerialNumber
        {
            get => _setSerialNumber;
            set => Set(ref _setSerialNumber, value);
        }   

        public int MeterNumber
        {
            get => _meterNumber;
            set => Set(ref _meterNumber, value);
        }

        public string MeterModelIdSelected
        {
            get => _meterModelIdSelected;
            set => Set(ref _meterModelIdSelected, value);
        }
        public List<string> MeterModelIds
        {
            get
            {
                var list = new List<string>();
                foreach (var item in Enum.GetValues(typeof(EnumMeterModel)))
                {
                    list.Add(item.ToString());
                }

                return list;    
            }
        }

        public string SwitchStateSelected
        {
            get => _switchState;
            set => Set(ref _switchState, value);
        }

        public List<string> SwitchStates
        {
            get
            {
                var list = new List<string>();
                foreach (var item in Enum.GetValues(typeof(EnumSwitchState)))
                {
                    list.Add(item.ToString());
                }

                return list;
            }
        }   

        public List<EndPointGyrDto> EndPoints
        {
            get => _endPoints;
            set => Set(ref _endPoints, value);
        }   
        public Visibility Tela1
        {
            get => _tela1;
            set => Set(ref _tela1, value);
        }

        public Visibility Tela2
        {
            get => _tela2;
            set => Set(ref _tela2, value);
        }

        public Visibility Tela3
        {
            get => _tela3;
            set => Set(ref _tela3, value);
        }

        public Visibility Tela4
        {
            get => _tela4;
            set => Set(ref _tela4, value);
        }

        public Visibility Tela5
        {
            get => _tela5;
            set => Set(ref _tela5, value);
        }

        public ICommand SendCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand RemoveCommand { get; set; }


    }
}
