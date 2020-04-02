using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateDesignPattern.Logic;

namespace State_Design_Pattern.Logic
{
    class ClosedState : BookingState
    {
        private string _reasonClosed;

        public ClosedState(string reason)
        {
            _reasonClosed = reason;
        }

        public override void EnterState(BookingContext booking)
        {
            booking.ShowState("Closed");
            booking.View.ShowStatusPage(_reasonClosed);
        }

        public override void Cancel(BookingContext booking)
        {
            booking.View.ShowError("Invalid acton for this state", "Closed Booking Error");
        }

        public override void DatePassed(BookingContext booking)
        {
            booking.View.ShowError("Invalid acton for this state", "Closed Booking Error");
        }

        public override void EnterDetails(BookingContext booking, string attendee, int ticketCount)
        {
            booking.View.ShowError("Invalid acton for this state", "Closed Booking Error");
        }
    }
}
