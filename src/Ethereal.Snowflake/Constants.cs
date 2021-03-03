// Copyright (c) Ethereal. All rights reserved.
//

namespace System
{
    internal class Constants
    {
        /// <summary>
        /// Timestamp, Do not greater than current
        /// </summary>
        public const long Timestamp = 1_609_430_400_000L;

        /// <summary>
        /// DataCenterBit
        /// </summary>
        public const int DataCenterBit = 5;
        /// <summary>
        /// MachingIdBits
        /// </summary>
        public const int MachingIdBits = 5;
        /// <summary>
        /// SequenceBits
        /// </summary>
        public const int SequenceBits = 12;

        /// <summary>
        /// MaxDataCenterId
        /// </summary>
        public const long MaxDataCenterId = -1L ^ (-1L << DataCenterBit);
        /// <summary>
        /// MaxMachingId
        /// </summary>
        public const long MaxMachingId = -1L ^ (-1L << MachingIdBits);
        /// <summary>
        /// MaxSequence
        /// </summary>
        public const long MaxSequence = -1L ^ (-1L << SequenceBits);

        /// <summary>
        /// DateCenterIdShift, 12+5
        /// </summary>
        public const int DateCenterIdShift = SequenceBits + MachingIdBits;
        /// <summary>
        /// MachingIdShift,12
        /// </summary>
        public const int MachingIdShift = SequenceBits;
        /// <summary>
        /// TimestampLeftShift,12+5+5
        /// </summary>
        public const int TimestampLeftShift = SequenceBits + DataCenterBit + MachingIdBits;
    }
}
