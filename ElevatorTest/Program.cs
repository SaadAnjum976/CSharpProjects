using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorTest
{
	class Program
	{
		static void Main(string[] args)
		{
			string promptFloors = "Enter the number of floors (between 2 and 100):";
			string promptSequence = "Enter input sequence (enter EXIT to end the program):";
			Console.Write(promptFloors);
			int numberOfFloors = 0;
			while(!Int32.TryParse(Console.ReadLine(), out numberOfFloors) || numberOfFloors < 2 || numberOfFloors > 100)
			{
				Console.Write(promptFloors);
			}
			
			ElevatorController controller = new ElevatorController(numberOfFloors);
			ElevatorLogic logic = new ElevatorLogic(controller);

			while(true)
			{
				Console.WriteLine(promptSequence);
				string eventSequence = Console.ReadLine();
				if(eventSequence.ToUpper() == "EXIT")
				{
					break;
				}
				else
				{
					controller.ExecuteSequence(eventSequence.ToUpper());
					Console.WriteLine();
				}
			}
		}
	}
}
