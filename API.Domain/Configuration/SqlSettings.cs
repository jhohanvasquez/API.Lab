using System;
using System.Collections.Generic;
using System.Text;

namespace API.Domain.Configuration
{
    public class SqlSettings
    {
        public string BDUserConnectionString { get; set; }
        public string SPSaveUser { get; set; }
        public string SPGetUser { get; set; }

    }
}
