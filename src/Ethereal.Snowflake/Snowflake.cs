// Copyright (c) Ethereal. All rights reserved.
//

namespace System
{
    /// <summary>
    /// Uuid
    /// </summary>
    public sealed class Snowflake
    {
        private readonly object _obj = new object();
        private readonly long _dataCenterId;
        private readonly long _machingId;
        private readonly long _timestamp;

        private long _sequence = 0L;
        private long _lastTimestamp = -1L;

        /// <summary>
        /// Initializes a new instance of the <see cref="Snowflake"/> struct.
        /// </summary>
        public Snowflake(long dataCenterId, long machingId, long? timestamp = default)
        {
            if (dataCenterId > Constants.MaxDataCenterId || dataCenterId < 0)
            {
                throw new ArgumentException($"datacenter can't be greater than {dataCenterId} or less than 0");
            }
            if (machingId > Constants.MaxMachingId || machingId < 0)
            {
                throw new ArgumentException($"maching can't be greater than {machingId} or less than 0 ");
            }

            _dataCenterId = dataCenterId;
            _machingId = machingId;

            _timestamp = timestamp ?? Constants.Timestamp;
        }

        /// <summary>
        /// Generate numerical value 
        /// </summary>
        public long Generate()
        {
            lock (_obj)
            {
                var timestamp = CurrentTimestamp();

                if (timestamp < _lastTimestamp)
                {
                    throw new Exception($"Clock moved backwards. Refusing to generate id.");
                }

                if (_lastTimestamp == timestamp)
                {
                    _sequence = (_sequence + 1) & Constants.MaxSequence;
                    if (_sequence == 0)
                    {
                        timestamp = NextTimestamp(_lastTimestamp);
                    }
                }
                else
                {
                    _sequence = 0;
                }

                _lastTimestamp = timestamp;
                return (timestamp - _timestamp) << Constants.TimestampLeftShift
                    | _dataCenterId << Constants.DateCenterIdShift
                    | _machingId << Constants.MachingIdShift
                    | _sequence;
            }
        }

        private long NextTimestamp(long lastTimestamp)
        {
            var timestamp = CurrentTimestamp();
            while (timestamp <= lastTimestamp)
            {
                timestamp = CurrentTimestamp();
            }
            return timestamp;
        }

        private long CurrentTimestamp() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}
