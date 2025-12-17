using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Enumerates
{
    /// <summary>
    /// Specifies the status of an email message within the delivery process.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    /// <remarks>Use this enumeration to determine or set the current state of an email, such as whether it is
    /// queued for sending, actively being sent, successfully sent, failed, or deferred for later delivery. The status
    /// can be used to control workflow logic or display progress to users.</remarks>
    public enum EmailStatusEnum
    { 
        Queued = 0, 
        Sending = 1, 
        Sent = 2, 
        Failed = 3,
        Deferred = 4 
    }

    /// <summary>
    /// Specifies the supported email provider types for outbound email delivery.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    /// <remarks>Use this enumeration to select the email service provider when configuring email sending
    /// functionality. The available values correspond to common third-party providers and standard SMTP. The chosen
    /// provider may affect available features, authentication requirements, and integration options.</remarks>
    public enum ProviderTypeEnum
    { 
        Smtp = 0, 
        SendGrid = 1, 
        Ses = 2, 
        Mailgun = 3 
    }

    /// <summary>
    /// Specifies the reason why an email address is suppressed from receiving messages.
    /// User Create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    /// <remarks>Use this enumeration to identify the cause of suppression when managing email delivery or
    /// handling suppression lists. The values correspond to common scenarios such as user-initiated unsubscription,
    /// delivery failures, abuse complaints, or manual suppression by an administrator.</remarks>
    public enum SuppressionReasonEnum
    { 
        Unsubscribed = 0, 
        Bounced = 1, 
        Complaint = 2, 
        Manual = 3 
    }

    /// <summary>
    /// Specifies the type of bounce that occurred for an email message.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    public enum BounceTypeEnum
    { 
        Hard = 0, 
        Soft = 1, 
        Transient = 2 
    }

    /// <summary>
    /// Specifies the processing status of an inbox message.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    public enum InboxProcessStatusEnum
    { 
        Pending = 0, 
        Processed = 1, 
        Failed = 2 
    }
}
