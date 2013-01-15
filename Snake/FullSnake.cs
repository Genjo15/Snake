using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    public class FullSnake
    {
        /********************************************* Declaration of variables *********************************************/

        List<SnakePart> _Snake; // List of snake parts.


        /**************************************************** Constructor ****************************************************/

        public FullSnake()
        {
            _Snake = new List<SnakePart>(); // Instanciate the list of snake parts.
            _Snake.Add(new SnakePart());    // Add one part (the first one) of the snake to the list.
        }


        /****************************************************** Methods ******************************************************/

        //////////////////////////////////
        // Method which refresh the panel

        public Panel Render(Panel panel, int direction)
        {
            panel.Controls.Clear(); // Erase the panel.

            foreach (SnakePart element in _Snake) // Do, for each element of SnakePart :
            {
                panel.Controls.Add(element.GetPart(direction));
                
            }
            return panel;
        }
    }
}
