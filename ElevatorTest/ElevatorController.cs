using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorTest
{
    class ElevatorController
    {
        public ElevatorController(int numberOfFloors)
        {
            this.numberOfFloors = numberOfFloors;
            sequencedEvents = new Dictionary<int, List<string>>();
            currentFloor = 1;
        }

        public int NumberOfFloors
        {
            get
            {
                return numberOfFloors;
            }
        }

        public void MoveUp()
        {
            ++currentFloor;
            Console.Write(string.Format("<{0}", currentFloor));
            ++sequenceIndex;
            ExecuteSequenceStep();
        }

        public void MoveDown()
        {
            --currentFloor;
            Console.Write(string.Format(">{0}", currentFloor));
            ++sequenceIndex;
            ExecuteSequenceStep();
        }

        public void Stop()
        {
            Console.Write(string.Format(" [{0}] ", currentFloor));
            ++sequenceIndex;
            ExecuteSequenceStep();
        }

        public event EventHandler<ElevatorControllerEventArgs> InsideButtonPushed;
        public event EventHandler<ElevatorControllerEventArgs> OutsideButtonPushed;

        #region implementation
        private void AddEvent(int index, string ev)
        {
            List<string> eventsAtIndex;
            if(sequencedEvents.ContainsKey(index))
            {
                eventsAtIndex = sequencedEvents[index];
            }
            else
            {
                eventsAtIndex = new List<string>();
                sequencedEvents.Add(index, eventsAtIndex);
            }
            eventsAtIndex.Add(ev);
        }

        internal void ExecuteSequence(string eventSequence)
        {
            Console.WriteLine();
            Console.Write(string.Format("[{0}]", currentFloor));
            // reset events
            sequencedEvents.Clear();
            sequenceIndex = 0;
            prevStart = 0;

            // separate events in steps, based on addded delays
            string[] events = eventSequence.Split(new char[] { ',' });
            int inputIndex = 0;
            foreach(string ev in events)
            {
                if(ev.StartsWith("D"))
                {
                    int steps = 0;
                    if(Int32.TryParse(ev.Substring(1), out steps))
                    {
                        inputIndex += steps;
                    }
                }
                else
                {
                    AddEvent(inputIndex, ev);
                }
            }

            while(sequenceIndex <= inputIndex)
            {
                ExecuteSequenceStep();
                ++sequenceIndex;
            }
        }

        private void ExecuteSequenceStep()
        {
            // execute any events that may exist before
            for(int pastIndex = prevStart; pastIndex < sequenceIndex; pastIndex++)
            {
                if(sequencedEvents.ContainsKey(pastIndex))
                {
                    List<string> eventsAtIndex = sequencedEvents[pastIndex];
                    if(eventsAtIndex.Count > 0)
                    {
                        prevStart = pastIndex + 1;
                        foreach(string ev in eventsAtIndex)
                        {
                            ProcessEvent(ev);
                        }
                    }
                    eventsAtIndex.Clear();
                }
            }

            ExecuteSequenceStep(sequenceIndex);
        }

        private void ProcessEvent(string ev)
        {
            if(ev.StartsWith("O"))
            {
                int floor = 0;
                if(Int32.TryParse(ev.Substring(1), out floor) && null != OutsideButtonPushed)
                {
                    OutsideButtonPushed(this, new ElevatorControllerEventArgs(floor));
                }
            }
            else if(ev.StartsWith("I"))
            {
                int floor = 0;
                if(Int32.TryParse(ev.Substring(1), out floor) && null != InsideButtonPushed)
                {
                    InsideButtonPushed(this, new ElevatorControllerEventArgs(floor));
                }
            }
        }

        private void ExecuteSequenceStep(int executeIndex)
        {
            if(sequencedEvents.ContainsKey(executeIndex))
            {
                List<string> eventsAtIndex = sequencedEvents[executeIndex];
                if(eventsAtIndex.Count > 0)
                {
                    // execute only the first event at this index
                    string ev = eventsAtIndex[0];
                    eventsAtIndex.RemoveAt(0);
                    ProcessEvent(ev);
                }
            }
        }
        #endregion

        private int numberOfFloors;
        private Dictionary<int, List<string>> sequencedEvents;
        private int sequenceIndex;
        private int currentFloor;
        private int prevStart;
    }

    public class ElevatorControllerEventArgs : EventArgs
    {
        public ElevatorControllerEventArgs(int floor)
        {
            this.floor = floor;
        }

        public int Floor
        {
            get
            {
                return floor;
            }
        }

        private int floor;
    }
}
