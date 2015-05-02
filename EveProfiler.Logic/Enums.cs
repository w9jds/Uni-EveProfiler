using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveProfiler.BusinessLogic
{
    public class Enums
    {
        public enum SkillAttibutePriority
        {
            NA = -1,
            Primary = 0,
            Secondary = 1
        }

        public enum EventResponses
        {
            Undecided = 0,
            Accepted = 1,
            Declined = 2,
            Tentative = 3
        }

        public enum Attributes
        {
            NA = -1,
            Intelligence = 0,
            Perception = 1,
            Charisma = 2,
            Willpower = 3,
            Memory = 4
        }

        public enum OrderStates
        {
            OpenActive = 0, 
            Closed = 1, 
            Expired_Or_Fulfilled = 2,
            Cancelled = 3,
            Pending = 4, 
            Character_Deleted = 5
        }

        public enum TransactionType
        {
            Buy = 0,
            Sell = 1
        }

        public enum TransactionFor
        {
            Personal = 0,
            Corporation = 0
        }

        public enum WalletRefTypeId
        {
            Player_Trading = 1,
            Market_Transaction = 2,
            Player_Donation = 10,
            Bounty_Prize = 17,
            Insurance = 19,
            CSPA = 35,
            Corp_Account_Withdrawl = 37,
            Broker_Fee = 46,
            Manufacturing = 56,
            Bounty_Prizes = 85
        }
    }
}
