using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Enumerates
{
    public enum EmailStatusEnum
    { 
        Queued = 0, 
        Sending = 1, 
        Sent = 2, 
        Failed = 3,
        Deferred = 4 }
    public enum ProviderTypeEnum
    { 
        Smtp = 0, 
        SendGrid = 1, 
        Ses = 2, 
        Mailgun = 3 
    }
    public enum SuppressionReasonEnum
    { 
        Unsubscribed = 0, 
        Bounced = 1, 
        Complaint = 2, 
        Manual = 3 
    }
    public enum BounceTypeEnum
    { 
        Hard = 0, 
        Soft = 1, 
        Transient = 2 
    }
    public enum InboxProcessStatusEnum
    { 
        Pending = 0, 
        Processed = 1, 
        Failed = 2 
    }
}
