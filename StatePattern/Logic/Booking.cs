using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StateDesignPattern.UI;

namespace StateDesignPattern.Logic
{
    public class Booking
    {
        private MainWindow View { get; set; }
        public string Attendee { get; set; }
        public int TicketCount { get; set; }
        public int BookingID { get; set; }

        private CancellationTokenSource _cancelToken;
        private bool _isNew;
        private bool _isPending;
        private bool _isBooked;

        public Booking(MainWindow view)
        {
            _isNew = true;
            View = view;
            BookingID = new Random().Next();
            ShowState("New");
            View.ShowEntryPage();
        }

        public void SubmitDetails(string attendee, int ticketCount)
        {
            if (_isNew)
            {
                _isNew = false;
                _isPending = true;

                Attendee = attendee;
                TicketCount = ticketCount;

                _cancelToken = new CancellationTokenSource();

                StaticFunctions.ProcessBooking(this, ProcessingComplete, _cancelToken);

                ShowState("Pending");
                View.ShowStatusPage("Processing Booking");
            }
        }

        public void Cancel()
        {
            if (_isNew)
            {
                ShowState("Closed");
                View.ShowStatusPage("Canceled by user");
                _isNew = false;
            } else if (_isPending)
            {
                _cancelToken.Cancel();
            }
            else if (_isBooked)
            {
                ShowState("Closed");
                View.ShowStatusPage("Booking canceled: Expect a refund");
                _isBooked = false;
            }
            else
            {
                View.ShowError("Closed booking cannot be canceled");
            }
        }

        public void DatePassed()
        {
            if (_isNew)
            {
                ShowState("Closed");
                View.ShowStatusPage("Booking expired");
                _isNew = false;
            }
            else if (_isBooked)
            {
                ShowState("Closed");
                View.ShowStatusPage("We hope you enjoyed the event");
                _isBooked = false;
            }
        }

        public void ProcessingComplete(Booking booking, ProcessingResult result)
        {
            _isPending = false;

            switch (result)
            {
                case ProcessingResult.Sucess:
                    _isBooked = true;
                    ShowState("Booked");
                    View.ShowStatusPage("Enjoy the Event");
                    break;
                case ProcessingResult.Fail:
                    View.ShowProcessingError();
                    Attendee = string.Empty;
                    BookingID = new Random().Next();
                    _isNew = true;
                    ShowState("New");
                    View.ShowEntryPage();
                    break;
                case ProcessingResult.Cancel:
                    ShowState("Closed");
                    View.ShowStatusPage("Processing Canceled");
                    break;
            }
        }

        public void ShowState(string stateName)
        {
            View.grdDetails.Visibility = System.Windows.Visibility.Visible;
            View.lblCurrentState.Content = stateName;
            View.lblTicketCount.Content = TicketCount;
            View.lblAttendee.Content = Attendee;
            View.lblBookingID.Content = BookingID;
        }



    }
}


