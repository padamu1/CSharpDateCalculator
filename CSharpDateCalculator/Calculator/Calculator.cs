using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDateCalculator.Calculator
{
    public class Calculator
    {
        private enum CALCULATE_TYPE
        {
            None,
            Minute,
            Hour,
            DayOfWeek,
            Date,
        }

        private CALCULATE_TYPE calculateType = CALCULATE_TYPE.None;
        private DateTime targetDate;
        private DayOfWeek targetDayOfWeek;
        private int targetHour;
        private int targetMinute;

        private DateTime nextTime;
        /// <summary>
        /// 특정 날짜를 설정
        /// targetDayOfWeek, targetHour, targetMinute 가 무시됨
        /// </summary>
        /// <param name="targetDate"></param>
        public void SetTargetDate(DateTime targetDate)
        {
            calculateType = CALCULATE_TYPE.Date;
            this.targetDate = targetDate;
        }
        /// <summary>
        /// 매주의 요일을 설정
        /// </summary>
        /// <param name="targetDayOfWeek"></param>
        public void SetTargetDayOfWeek(DayOfWeek targetDayOfWeek)
        {
            // 요일이 설정되면 타입 지정
            if (calculateType == CALCULATE_TYPE.None || calculateType == CALCULATE_TYPE.Minute || calculateType == CALCULATE_TYPE.Hour)
            {
                calculateType = CALCULATE_TYPE.DayOfWeek;
            }
            this.targetDayOfWeek = targetDayOfWeek;
        }
        /// <summary>
        /// 0 ~ 23 중 목표로 하는 시 설정
        /// </summary>
        /// <param name="targetHour"></param>
        public void SetTargetHour(int targetHour)
        {
            // 타입이 none 이거나, minute 이면 hour로 타입 변경
            if(calculateType == CALCULATE_TYPE.None || calculateType == CALCULATE_TYPE.Minute)
            {
                calculateType= CALCULATE_TYPE.Hour;
            }
            this.targetHour = targetHour;
        }
        /// <summary>
        /// 타겟 분 설정
        /// </summary>
        /// <param name="targetMinute"></param>
        public void SetTargetMinute(int targetMinute)
        {
            // 타입이 none 일 경우 minute로 변경
            if (calculateType == CALCULATE_TYPE.None)
            {
                calculateType = CALCULATE_TYPE.Minute;
            }
            this.targetMinute = targetMinute;
        }
        /// <summary>
        /// 특정 날짜가 아닌 요일을 기준으로 계산
        /// </summary>
        public void Calculate()
        {
            DateTime nowTime = DateTime.Now;
            DateTime nextTime = DateTime.Today;
            switch (calculateType)
            {
                case CALCULATE_TYPE.None:
                    throw new Exception("Type 설정이 올바르게 되지 않음");
                case CALCULATE_TYPE.Minute:
                    nextTime = nextTime.AddHours(nowTime.Hour);
                    if (nowTime.Minute > targetMinute)
                    {
                        // 한시간 뒤로 설정
                        nextTime = nextTime.AddHours(1);
                    }
                    nextTime = nextTime.AddMinutes(targetMinute);
                    break;
                case CALCULATE_TYPE.Hour:
                    if (nowTime.Hour > targetHour || (nowTime.Hour == targetHour && nowTime.Minute > targetMinute))
                    {
                        // 하루 뒤로 설정
                        nextTime = nextTime.AddDays(1);
                    }

                    nextTime = nextTime.AddHours(targetHour);
                    nextTime = nextTime.AddMinutes(targetMinute);
                    break;
                case CALCULATE_TYPE.DayOfWeek:
                    if (nowTime.DayOfWeek > targetDayOfWeek || (nowTime.DayOfWeek == targetDayOfWeek && nowTime.Hour > targetHour) || (nowTime.DayOfWeek == targetDayOfWeek && nowTime.Hour == targetHour && nowTime.Minute > targetMinute))
                    {
                        // 다음주로 설정 -> 일요일로 초기화 시킴
                        nextTime = nextTime.AddDays(7 - (int)nowTime.DayOfWeek);
                    }

                    nextTime = nextTime.AddDays((int)targetDayOfWeek - (int)nowTime.DayOfWeek);
                    nextTime = nextTime.AddHours(targetHour);
                    nextTime = nextTime.AddMinutes(targetMinute);
                    break;
                case CALCULATE_TYPE.Date:
                    nextTime = targetDate;
                    break;
            }
            this.nextTime = nextTime;
        }
        /// <summary>
        /// 남은 일수 반환
        /// </summary>
        /// <returns>int - day</returns>
        public int GetDay()
        {
            switch(calculateType)
            {
                default:
                case CALCULATE_TYPE.None:
                case CALCULATE_TYPE.Minute:
                case CALCULATE_TYPE.Hour:
                    return 0;
                case CALCULATE_TYPE.DayOfWeek:
                case CALCULATE_TYPE.Date:
                    return (nextTime - DateTime.Now).Days;
            }
        }
        /// <summary>
        /// 남은 시 반환
        /// </summary>
        /// <returns>int - hour</returns>
        public int GetHour()
        {
            switch (calculateType)
            {
                default:
                case CALCULATE_TYPE.None:
                case CALCULATE_TYPE.Minute:
                    return 0;
                case CALCULATE_TYPE.Hour:
                case CALCULATE_TYPE.DayOfWeek:
                case CALCULATE_TYPE.Date:
                    return (nextTime - DateTime.Now).Hours;
            }
        }
        /// <summary>
        /// 남은 분 반환
        /// </summary>
        /// <returns>int - minute</returns>
        public int GetMinute()
        {
            switch (calculateType)
            {
                default:
                case CALCULATE_TYPE.None:
                    return 0;
                case CALCULATE_TYPE.Minute:
                case CALCULATE_TYPE.Hour:
                case CALCULATE_TYPE.DayOfWeek:
                case CALCULATE_TYPE.Date:
                    return (nextTime - DateTime.Now).Minutes;
            }
        }
        /// <summary>
        /// 남은 총 밀리초 반환
        /// </summary>
        /// <returns>double - totalmilliseconds</returns>
        public double GetTotalMilliseconds()
        {
            return (nextTime - DateTime.Now).TotalMilliseconds;
        }
    }
}
