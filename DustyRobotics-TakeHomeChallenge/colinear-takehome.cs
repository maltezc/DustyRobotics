/* 

Given a set of lines, find sets of lines contained in the original
set that are colinear.

Create a way to show the result of your work in a way you see fit
to make the viewer understand which lines are colinear and which are not.

Each line is given as a set of x/y coordinates that mark the two endpoints of
the line. See the Line type.

Assume that lines will be at any imaginable orientation and length. The 
example specifies nice round numbers, but your code should work with any 
floating point numbers.

Fill in the 'colinear' function below, and feel free to create more 
functions if neccessary. Make this program look like code you would write 
for a real application.

Return your work in a single .cs file.

*/

using System;
using System.Collections.Generic;

public struct Line
{
    public Line(double x1, double y1, double x2, double y2)
    {
        X1 = x1;
        Y1 = y1;
        X2 = x2;
        Y2 = y2;
    }

    public double X1 { get; }
    public double Y1 { get; }
    public double X2 { get; }
    public double Y2 { get; }

    public override string ToString() => $"[({X1}, {Y1}), ({X2}, {Y2})]";
}

public class Colinear
{
    static public List<List<Line>> colinear(List<Line> lines)
    {
        /*

        Given a set of lines, find sets of lines where all lines are colinear

        Example:

        lines = [[ (1,1), (2,2)],
         [ (-1,-1), (0,0)],
         [ (2,6), (3,7)],
         [ (0,0), (1,1)],
         [ (4,4), (5,5)],
         [ (-7,3), (7,-3)]]

        should return 3 lists (in any order):

        list 1
         {[(1, 1), (2, 2)], [(-1, -1), (0, 0)], [(0, 0), (1, 1)], [(4, 4), (5, 5)]},
        list 2
         {[(2, 6), (3, 7)]},
        list 3
         {[(-7, 3), (7, -3)]}

        note that lists 2 and 3 only contain a single line

        */

        List<List<Line>> lineGroups = findCoLinearLines(lines)

        /* Implement the algorithm here. */
        return lineGroups
    }

    static public List<List<Line>> findCoLinearLines (List<Line> lines)
    {
        List<List<Line>> groupedLines = new List<List<Line>>();

        /*
        technique: use 2 pointers system. 
        Pseudo code
        1) inintialize linegroup to empty
        2) initialize first line in single lingroup
        3) get y=mx+b from base line
        4) compare baseline mx+b with roverLine point1 and point 2
        5) if yes add to sublinegroup
        6) if not, move rover to next
        7) if rover is at end of linelist, increment base and set rover to base + 1
        9) clear sublist and add newBaseline to it 
        10) if roverIndex = linegroups.count - 1, push linegroup to linegroups
        11) run until baseIndex is linelist - 1
        */

        // code
        int baseIndex = 0;
        int roverIndex = 1;
        List<Line> subLineGroup = new List<Line>(lines[0]);

        while (baseIndex < lines.Count - 1)
        {
            Line baseLine = lines[baseIndex];
            Line roverLine = lines[roverIndex];

            // find mx+b of baseline 
            int slopeY = baseLine.Y2 - baseLine.Y1;
            int slopeX = baseLine.X2 - baseLine.X1;

            int mSlope = slopeY / slopeX;
            int bValue = (mSlope * baseLine.X1) - baseLine.Y1;

            // compare points of roverline and check other points with y = 2x
            bool roverFirstPointColinear = roverLine.Y1 == mSlope(roverLine.X1) + bValue;
            bool roverSecondPointColinear = roverLine.Y2 == mSlope(roverLine.X2) + bValue;

            if (roverFirstPointColinear && roverSecondPointColinear) subLineGroup.Add(roverLine);

            roverIndex++;
            if (roverIndex == lines.Count)
            {
                groupedLines.Add(subLineGroup)
                
                baseIndex++;
                roverIndex = baseIndex + 1;
                subLineGroup.Clear();
                subLineGroup.Add(lines[baseIndex]);
            }
        }

        return groupedLines;
    }

    static public void visualize(List<List<Line>> linegroups)
    {
        // Find a better way to make this useful output.
        for (int i = 0; i < linegroups.Count; i++)
        {
            List<Line> linegroup = linegroups[i];
            Console.Write($"Colinear linegroup {i + 1} with {linegroup.Count} colinear lines: ");

            for (int j = 0; j < linegroup.Count; j++)
            {
                Line line = linegroup[j];
                Console.WriteLine($"Colinear line group {i} - Line {j + 1} starting point: {line.X1},{line.Y1} - endpoint{line.X2},{line.Y2}")
            }
            Console.WriteLine();
        }
    }

    static public void Main()
    {
        List<Line> lines = new List<Line>();

        lines.Add(new Line(1.0, 1.0, 2.0, 2.0));
        lines.Add(new Line(1.0, -1.0, 0.0, 0.0));
        lines.Add(new Line(2.0, 6.0, 3.0, 7.0));
        lines.Add(new Line(0.0, 0.0, 1.0, 1.0));
        lines.Add(new Line(4.0, 4.0, 5.0, 5.0));
        lines.Add(new Line(7.0, 3.0, 7.0, -3.0));

        List<List<Line>> linegroups = colinear(lines);
        visualize(linegroups);
    }

    // TODO: write tests
    // TODO: further decompose findCoLinearLines
    // TODO: optimize for better than 0(n^2) solution
}
