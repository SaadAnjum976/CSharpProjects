// C# program to create marksheet for students. 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 

class GFG{ 
	
    static void Main(string[] args) 
    { 
	
        // Declare variables for marks and total 
        int r, marks1, marks2, marks3, total; 
	
        // Declare percentage variable 
        float percentage; 
        string n; 
	
        // Enter student roll number 
        Console.WriteLine("Enter Student Roll Number :"); 
        r = Convert.ToInt32(Console.ReadLine()); 
	
        // Enter student name 
        Console.WriteLine("Enter Student Name :"); 
        n = Console.ReadLine(); 
	
        // Enter student subject 1 marks 
        Console.WriteLine("Enter Subject-1 Marks : "); 
        marks1 = Convert.ToInt32(Console.ReadLine()); 
	
        // Enter student subject 2 marks 
        Console.WriteLine("Enter Subject-2 Marks : "); 
        marks2 = Convert.ToInt32(Console.ReadLine()); 
	
        // Enter student subject 3 marks 
        Console.WriteLine("Enter Subject-3 Marks :"); 
        marks3 = Convert.ToInt32(Console.ReadLine()); 
	
        // Calculate total marks 
        total = marks1 + marks2 + marks3; 
	
        // Calculate percentage 
        percentage = total / 3.0f; 
	
        // Display the final result 
        Console.WriteLine("Final result of {0} is:", n); 
        Console.WriteLine("Total Marks : " + total); 
        Console.WriteLine("Percentage : " + percentage); 
	
        // Calculate grades 
        if (percentage <= 35) 
        { 
            Console.WriteLine("Grade is F"); 
        } 
        else if (percentage >= 34 && percentage <= 39) 
        { 
            Console.WriteLine("Grade is D"); 
        } 
        else if (percentage >= 40 && percentage <= 59) 
        { 
            Console.WriteLine("Grade is C"); 
        } 
        else if (percentage >= 60 && percentage <= 69) 
        { 
            Console.WriteLine("Grade is B"); 
        } 
        else if (percentage >= 70 && percentage <= 79) 
        { 
            Console.WriteLine("Grade is B+"); 
        } 
        else if (percentage >= 80 && percentage <= 90) 
        { 
            Console.WriteLine("Grade is A"); 
        } 
        else if (percentage >= 91) 
        { 
            Console.WriteLine("Grade is A+"); 
        } 
        
    } 
}