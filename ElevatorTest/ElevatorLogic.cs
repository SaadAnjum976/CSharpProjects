using System.Collections.Generic;
using System.Linq;

namespace ElevatorTest
{
    class ElevatorLogic
    {
        private ElevatorController controller;

        // Constructor for ElevatorLogic, taking an ElevatorController as a parameter
        public ElevatorLogic(ElevatorController controller)
        {
            this.controller = controller;
            
            // Subscribe to the events for inside and outside button pushes
            controller.InsideButtonPushed += InsideButtonPushedEventHandler;
            controller.OutsideButtonPushed += OutsideButtonPushedEventHandler;
        }

        private bool movingUp = true;
        private int currentFloor = 1;
        private bool eventFired = false;
        private List<int> floors = new List<int>();

        // Event handler for outside button push
        private void OutsideButtonPushedEventHandler(object sender, ElevatorControllerEventArgs e)
        {
            // Process the floor request
            ProcessRequests(e.Floor);
        }

        // Event handler for inside button push
        private void InsideButtonPushedEventHandler(object sender, ElevatorControllerEventArgs e)
        {
            // Process the floor request
            ProcessRequests(e.Floor);
        }

        // Process floor requests and add them to the list
        private void ProcessRequests(int floor)
        {
            if (floor >= 1 && floor <= controller.NumberOfFloors)
            {
                floors.Add(floor);
                eventFired = true;
                
                // Perform elevator actions
                ElevatorAction();
            }
        }

        // Set the direction based on the current and requested floors
        private void SetDirection()
        {
            if (floors.Count > 1)
            {
                if (movingUp && currentFloor == floors.Max())
                    movingUp = false;
                else if (!movingUp && currentFloor == floors.Min())
                    movingUp = true;
            }
            else if (floors.Count == 1)
            {
                if (movingUp && currentFloor > floors[0])
                    movingUp = false;
                else if (!movingUp && currentFloor < floors[0])
                    movingUp = true;
            }
        }

        // Perform elevator actions based on the requested floors
        private void ElevatorAction()
        {
            eventFired = false;
            
            // Continue processing until all requested floors are handled
            while (!eventFired && floors.Any())
            {
                // Set the direction
                SetDirection();

                // If the elevator is on a requested floor, stop
                if (floors.Contains(currentFloor))
                {
                    floors.Remove(currentFloor);
                    controller.Stop();
                }
                else if (floors.Any())
                {
                    // Move the elevator up or down based on the direction
                    if (movingUp)
                    {
                        ++currentFloor;
                        controller.MoveUp();
                    }
                    else
                    {
                        --currentFloor;
                        controller.MoveDown();
                    }
                }
            }
        }
    }
}
