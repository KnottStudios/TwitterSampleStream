using System;

namespace TwitterSampleStreamAPI.Models
{
    /// <summary>
    /// Please Provide a Start and End Time without Milliseconds.
    /// </summary>
    public class TimeFrame
    {
        /// <summary>
        /// Start Time Without Milliseconds.
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// End Time Without Milliseconds.
        /// </summary>
        public DateTime EndTime { get; set; } = DateTime.Now;
    }
}
