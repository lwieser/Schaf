using Devis.ViewModels;
using Infragistics.Controls.Schedules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devis.Planning
{
    public class PlanningViewModel : NotifyPropertyChangedGeneric
    {
        #region Fields
        private DateTime _today = DateTime.Now;
        private ListScheduleDataConnector _dataConnector;
        private XamScheduleDataManager _dataManager;
        private EnumViewOption _viewOption;
        #endregion

        #region Properties
        public enum EnumViewOption
        {
            Schedule,
            Day,
            Month,
        }

        public EnumViewOption ViewOption
        {
            get { return _viewOption; }
            set { _viewOption = value;
                OnPropertyChanged();
            }
        }

        public XamScheduleDataManager DataManager
        {
            get { return _dataManager; }
            set
            {
                _dataManager = value;
                OnPropertyChanged();
            }
        }

        public ListScheduleDataConnector DataConnector
        {
            get { return _dataConnector; }
            set
            {
                _dataConnector = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public PlanningViewModel()
        {
            ViewOption = EnumViewOption.Schedule;
            Sample();
        }

        public void Sample()
        {

            ObservableCollection<Resource> resources = CreateRessources();
            ObservableCollection<ResourceCalendar> calendars = CreateCalendars();
            ObservableCollection<Appointment> appointments = CreateAppointements();

            _dataConnector =  new ListScheduleDataConnector()
            {
                ResourceItemsSource = resources,
                ResourceCalendarItemsSource = calendars,
                AppointmentItemsSource = appointments,
            };

            _dataManager = new XamScheduleDataManager()
            {
                DataConnector = _dataConnector
            };

            CalendarGroupCollection calGroups = _dataManager.CalendarGroups;
            CalendarGroup calGroup = new CalendarGroup()
            {
                InitialCalendarIds = "ownJN[calJN],ownB[calB],ownL[calL]"
            };
            calGroups.Add(calGroup);
            _dataManager.ColorScheme = new IGColorScheme();
        }

        private ObservableCollection<Appointment> CreateAppointements()
        {
            return new ObservableCollection<Appointment>()
            {
                new Appointment()
                {
                    Id = "t1",
                    OwningResourceId = "ownJN",
                    OwningCalendarId = "calJN",
                    Subject = "Formations Gand",
                    Start = DateTime.Now.AddDays(3).ToUniversalTime(),
                    End = DateTime.Now.AddDays(3).AddHours(2).ToUniversalTime()
                },
                new Appointment()
                {
                    Id = "t2",
                    OwningResourceId = "ownJN",
                    OwningCalendarId = "calJN",
                    Subject = "Présentation Schaffner",
                    Description = "My second appointment",
                    // Convert the date to universal time, because a time zone is not set
                    // in this snippet    
                    Start = DateTime.Now.ToUniversalTime(),
                    End = DateTime.Now.AddHours(2).ToUniversalTime(),
                },

                //Benoit 
                new Appointment()
                {
                    Id="t3",
                    OwningCalendarId="calB",
                    OwningResourceId="ownB",
                    Subject = "Afterwork ENSIIE",
                    Start = DateTime.Now.ToUniversalTime(),
                    End = DateTime.Now.AddHours(1).ToUniversalTime(),
                }
            };
        }

        private ObservableCollection<ResourceCalendar> CreateCalendars()
        {
            return new ObservableCollection<ResourceCalendar>()
            {
                new ResourceCalendar(){Id = "calJN", OwningResourceId = "ownJN"},
                new ResourceCalendar(){Id = "calB", OwningResourceId = "ownB"},
                new ResourceCalendar(){Id = "calL", OwningResourceId = "ownL"},
            };
        }

        private ObservableCollection<Resource> CreateRessources()
        {
            return new ObservableCollection<Resource>() {
                new Resource() { Id = "ownJN", Name = "Jean-Noël" },
                new Resource() { Id = "ownB", Name = "Benoit" },
                new Resource() { Id = "ownL", Name = "Laurent" }
            };
        }
    }
}
