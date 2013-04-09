using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices; //
using System.Drawing.Text;            // To use for the fonts
using System.Drawing;                 //

namespace Snake
{
    class PersonalFont
    {
        /********************************************* Declaration of variables *********************************************/

        [DllImport("gdi32.dll")] // Make a call to gdi32.dll
        private static extern System.IntPtr AddFontMemResourceEx(System.IntPtr pbFont, uint cbFont, System.IntPtr pdv, [In] ref uint pcFonts);
        FontFamily kraboudja;

        /**************************************************** Constructor ****************************************************/

        public PersonalFont()
        {
            // Create the byte array and get its length
            byte[] fontArray = global::Snake.Properties.Resources.Kraboudja;
            int dataLength = global::Snake.Properties.Resources.Kraboudja.Length;

            // Assign memory and copy byte[] on that memory address 
            System.IntPtr ptrData = Marshal.AllocCoTaskMem(dataLength); //(the common language runtime must marshal ptrdata which will be passed as a parameter with AddFontMemRessourceEx).
            Marshal.Copy(fontArray, 0, ptrData, dataLength);

            uint cFonts = 0;
            AddFontMemResourceEx(ptrData, (uint)fontArray.Length, System.IntPtr.Zero, ref cFonts); // Adds the font resource from a memory image to the system (from gdi32.dll)

            PrivateFontCollection pfc = new PrivateFontCollection();
            // Pass the font to the privatefontcollection object
            pfc.AddMemoryFont(ptrData, dataLength);

            // Free the "unsafe" memory
            Marshal.FreeCoTaskMem(ptrData);

            kraboudja = (FontFamily)pfc.Families[0];
        }

        /****************************************************** Methods ******************************************************/

        ///////////////////
        // Return the font

        public FontFamily getPersonalFont()
        {
            return kraboudja;
        }
    }
}
