using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SoftRender
{
    public class TimerManager
    {
        private Dictionary<int, TimerEvent> dicTimers = new Dictionary<int, TimerEvent>();
        private List<int> listIDWaitToRemove = new List<int>();
        private int id;

        private static TimerManager m_Instance;
        public static TimerManager Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new TimerManager();
                }
                return m_Instance;
            }
        }

        private void DeleteTimerEvents()
        {
            if(listIDWaitToRemove.Count == 0)
            {
                return;
            }
            for(int i=0;i< listIDWaitToRemove.Count;i++)
            {
                dicTimers.Remove(listIDWaitToRemove[i]);
            }
            listIDWaitToRemove.Clear();
        }

        public void UpdateFunc(float deltaTime)
        {
            DeleteTimerEvents();

            foreach(KeyValuePair<int,TimerEvent> kp in dicTimers)
            {
                TimerEvent timerEvent = kp.Value;
                timerEvent.timeCal += deltaTime;
                if (timerEvent.timeCal > timerEvent.delay && timerEvent.haveRunDelayAction == false)
                {
                    timerEvent.haveRunDelayAction = true;
                    timerEvent.action(timerEvent.parm);
                }

                if (timerEvent.repeatTimes != 1)
                {
                    if (timerEvent.timeCal > timerEvent.delay + (timerEvent.currentTime + 1) * timerEvent.interval)
                    {
                        timerEvent.action(timerEvent.parm);
                        timerEvent.currentTime++;
                    }
                }
                if (timerEvent.repeatTimes != -1)
                {
                    if (timerEvent.haveRunDelayAction == true && timerEvent.currentTime >= timerEvent.repeatTimes - 1)
                    {
                        this.listIDWaitToRemove.Add(timerEvent.id);
                    }
                }
            }

            this.DeleteTimerEvents();
        }

        public int CallActionDelay(System.Action<object> action,float delay,object parm,int repeatTimes,float interval,bool keepAlive)
        {
            if (delay == 0)
            {
                delay = 0.01f;
            }
            if (repeatTimes <= -1)
            {
                repeatTimes = -1;
            }

            if (repeatTimes == 0)
            {
                repeatTimes = 1;
            }

            if (interval <= 0 || interval == null)
            {
                interval = delay;
            }

            TimerEvent timerEvent = new TimerEvent();
            timerEvent.id = this.id;
            timerEvent.action = action;
            timerEvent.delay = delay;
            timerEvent.parm = parm;
            timerEvent.repeatTimes = repeatTimes;
            timerEvent.interval = interval;
            timerEvent.keepAlive = keepAlive;
            dicTimers[timerEvent.id] = timerEvent;
            id++;
            return timerEvent.id;
        }
    }

    class TimerEvent
    {
        public int id;
        public float delay = 0;
        public float interval = 0;
        public System.Action<object> action = null;
        public object parm = null;
        public int repeatTimes = 0;
        //计时器
        public float timeCal = 0;
        //当前repeate次数
        public int currentTime = 0;
        public bool keepAlive = false;
        public bool haveRunDelayAction = false;
    }
}
