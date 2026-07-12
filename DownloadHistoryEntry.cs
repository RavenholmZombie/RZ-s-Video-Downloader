using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZVD
{
    internal sealed class DownloadHistoryEntry
    {
        public DateTime DownloadedAt { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public string Source { get; set; } = string.Empty;
    }
}
