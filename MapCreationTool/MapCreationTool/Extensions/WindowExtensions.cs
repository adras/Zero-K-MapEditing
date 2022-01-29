using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MapCreationTool.Extensions
{
    public static class WindowExtensions
    {
        /// <summary>
        /// Centers the given window in the context of the given parent
        /// </summary>
        /// <param name="window"></param>
        /// <param name="parent"></param>
        public static void Center(this Window window, Window parent)
        {
            // Get center of parent
            double newLeft = parent.Left + parent.Width / 2.0;
            double newTop = parent.Top + parent.Height / 2.0;

            // Move by element size
            newLeft -= window.Width / 2;
            newTop -= window.Height / 2;

            window.SetValue(Window.LeftProperty, newLeft);
            window.SetValue (Window.TopProperty, newTop);
        }
    }
}
