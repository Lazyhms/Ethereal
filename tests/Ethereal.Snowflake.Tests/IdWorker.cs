using System;
using System.Runtime.CompilerServices;

namespace Ethereal.IdWorker.Tests
{
    public sealed class IdWorker
    {
        private const int DataCenterBit = 5;

        private const int DataCenterLeft = SequenceBit + MachingBit;

        private const int MachingBit = 5;

        private const int MachingLeft = SequenceBit;

        private const long MaxDataCenterNum = -1L ^ (-1L << DataCenterBit);

        private const long MaxMachingNum = -1L ^ (-1L << MachingBit);

        private const long MaxSequence = -1L ^ (-1L << SequenceBit);

        private const int SequenceBit = 12;

        /// <summary>
        /// 起始的时间戳:唯一时间，这是一个避免重复的随机量，自行设定不要大于当前时间戳。 一个计时周期表示一百纳秒，即一千万分之一秒。 1 毫秒内有 10,000 个计时周期，即 1
        /// 秒内有 1,000 万个计时周期。
        /// </summary>
        private const long StartTimeStamp = 1_609_430_400_000L;

        /*
         * 每一部分占用的位数
         * 对于移位运算符 << 和 >>，右侧操作数的类型必须为 int，或具有预定义隐式数值转换 为 int 的类型。
         */
        //序列号占用的位数
        //机器标识占用的位数
        //数据中心占用的位数

        /*
         * 每一部分的最大值
         */
        /*
         * 每一部分向左的位移
         */
        private const int TimeStampLeft = DataCenterLeft + DataCenterBit;

        private readonly long dataCenterId;  //数据中心
        private readonly long machineId;     //机器标识
        private long lastTimeStamp = -1L;
        private long sequence; //序列号
                               //上一次时间戳

        /// <summary>
        /// 根据指定的数据中心ID和机器标志ID生成指定的序列号
        /// </summary>
        /// <param name="dataCenterId">数据中心ID</param>
        /// <param name="machineId">   机器标志ID</param>
        public IdWorker(long dataCenterId, long machineId)
        {
            if (dataCenterId > MaxDataCenterNum || dataCenterId < 0)
            {
                throw new ArgumentException("DtaCenterId can't be greater than MAX_DATA_CENTER_NUM or less than 0！");
            }
            if (machineId > MaxMachingNum || machineId < 0)
            {
                throw new ArgumentException("MachineId can't be greater than MAX_MACHINE_NUM or less than 0！");
            }
            this.dataCenterId = dataCenterId;
            this.machineId = machineId;
        }

        /// <summary>
        /// 产生下一个ID
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public long NextId()
        {
            var currTimeStamp = GetNextMill();
            if (currTimeStamp < lastTimeStamp)
            {
                //如果当前时间戳比上一次生成ID时时间戳还小，抛出异常，因为不能保证现在生成的ID之前没有生成过
                throw new Exception("Clock moved backwards.  Refusing to generate id");
            }

            if (currTimeStamp == lastTimeStamp)
            {
                //相同毫秒内，序列号自增
                sequence = (sequence + 1) & MaxSequence;
                //同一毫秒的序列数已经达到最大
                if (sequence == 0L)
                {
                    currTimeStamp = GetNextMill();
                }
            }
            else
            {
                //不同毫秒内，序列号置为0
                sequence = 0L;
            }

            lastTimeStamp = currTimeStamp;

            return (currTimeStamp - StartTimeStamp) << TimeStampLeft //时间戳部分
                    | dataCenterId << DataCenterLeft       //数据中心部分
                    | machineId << MachingLeft             //机器标识部分
                    | sequence;                             //序列号部分
        }

        private long GetNewTimeStamp() => DateTimeOffset.Now.ToUnixTimeMilliseconds();

        private long GetNextMill()
        {
            var mill = GetNewTimeStamp();
            while (mill <= lastTimeStamp)
            {
                mill = GetNewTimeStamp();
            }
            return mill;
        }
    }
}