﻿using System.Collections.Specialized;

namespace CleanArchitecture.Application.Models.Email
{
    public class EmailMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

}
