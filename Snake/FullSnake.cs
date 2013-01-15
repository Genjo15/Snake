using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    public class FullSnake
    {
        List<SnakePart> _Snake;

        public FullSnake()
        {
            _Snake = new List<SnakePart>();
            _Snake.Add(new SnakePart());
        }

        public Panel Render(Panel panel, int direction)
        {
            panel.Controls.Clear();

            foreach (SnakePart element in _Snake) // Pour chaque élément du snakepart dans le snake, faire :
            {
                panel.Controls.Add(element.GetPart(direction));
            }
            return panel;
        }

        


    }
}
