using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateDesignPattern.Logic;

namespace State_Design_Pattern.Logic
{
    class NewState : BookingState
    {
        public override void EnterState(BookingContext booking)
        {
            
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
    }
}
