﻿using System;

namespace Pharmacy.Domain.Common.ValueObjects
{
    public class AuditableEntity
    {
        public string CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
