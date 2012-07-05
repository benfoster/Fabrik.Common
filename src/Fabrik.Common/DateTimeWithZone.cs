using System;

namespace Fabrik.Common
{
    /// <summary>
    /// Represents an instance in time, for a specific time zone
    /// </summary>
    public struct DateTimeWithZone
    {
        private readonly DateTime utcDateTime;
        private readonly TimeZoneInfo timeZone;

        public DateTimeWithZone(DateTime dateTime, TimeZoneInfo timeZone, DateTimeKind kind = DateTimeKind.Utc)
        {
            dateTime = DateTime.SpecifyKind(dateTime, kind);

            utcDateTime = dateTime.Kind != DateTimeKind.Utc
                            ? TimeZoneInfo.ConvertTimeToUtc(dateTime, timeZone)
                            : dateTime;

            this.timeZone = timeZone;
        }

        public DateTime UniversalTime { get { return utcDateTime; } }
        public TimeZoneInfo TimeZone { get { return timeZone; } }
        public DateTime LocalTime { get { return TimeZoneInfo.ConvertTime(utcDateTime, timeZone); } }
    }
}
