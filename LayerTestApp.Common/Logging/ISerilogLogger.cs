using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerTestApp.Common.Logging
{
    public interface ISerilogLogger
    {
        void LogInformation(LoggingLevels eventLevel, string info, Exception? ex = null, params object[] values);
    }
}
