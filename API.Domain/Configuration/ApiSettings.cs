using System;
using System.Collections.Generic;
using System.Text;

namespace API.Domain.Configuration
{
    public class ApiSettings
    {
        public EnvironmentVariables environmentVariables { get; set; }
    }

    public class EnvironmentVariables
    {
        public SqlSettings SqlSettings { get; set; }
    }

}
