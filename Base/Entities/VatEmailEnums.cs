using System;
using System.Collections.Generic;
using System.Text;

namespace EmailEngine.Base.Entities
{
    public enum VatEmailTemplateType
    {
        AccountCreation = 1,
        PasswordReset,
        QuoteRequestAutoResponse
    }

    public enum VatEmailStatus
    {
        Fresh = 1,
        Sent,
        Failed
    }
}

