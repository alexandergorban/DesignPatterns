using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StateDesignPattern.Logic;

namespace State_Design_Pattern.Logic
{
    class PendingState : BookingState
    {
        private CancellationTokenSource _cancelToken;

        public override void EnterState(BookingContext booking)
        {
            _cancelToken = new CancellationTokenSource();

            booking.ShowState("Pending");
            booking.View.ShowStatusPage("Processing Booking");

            StaticFunctions.ProcessBooking(booking, ProcessingComplete, _cancelToken);
        }

        public override void Cancel(BookingContext booking)
        {
            
        }

        public override void DatePassed(BookingContext booking)
        {
            
        }

        public override void EnterDetails(BookingContext booking, string attendee, int ticketCount)
        {
            
        }

        public void ProcessingComplete(BookingContext booking, ProcessingResult result)
        {
            switch (result)
            {
                case ProcessingResult.Sucess:
                    booking.TransitionToState(new BookedState());
                    break;
                case ProcessingResult.Fail:
                    booking.View.ShowProcessingError();
                    booking.TransitionToState(new NewState());
                    break;
                case ProcessingResult.Cancel:
                    booking.TransitionToState(new ClosedState("Processing Canceled"));
                    break;
            }
        }
    }
}
