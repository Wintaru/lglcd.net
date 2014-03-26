using System;
using System.Text;
using System.Runtime.InteropServices;

namespace LgLcd.NET
{
    public static class LgLcd
    {
		public static int LOGI_LCD_TYPE_MONO = 0x00000001;
        public static int LOGI_LCD_TYPE_COLOR = 0x00000002;


        public static int LOGI_LCD_MONO_BUTTON_0 = 0x00000001;
        public static int LOGI_LCD_MONO_BUTTON_1 = 0x00000002;
        public static int LOGI_LCD_MONO_BUTTON_2 = 0x00000004;
        public static int LOGI_LCD_MONO_BUTTON_3 = 0x00000008;

        public static int LOGI_LCD_COLOR_BUTTON_LEFT = 0x00000100;
        public static int LOGI_LCD_COLOR_BUTTON_RIGHT = 0x00000200;
        public static int LOGI_LCD_COLOR_BUTTON_OK = 0x00000400;
        public static int LOGI_LCD_COLOR_BUTTON_CANCEL = 0x00000800;
        public static int LOGI_LCD_COLOR_BUTTON_UP = 0x00001000;
        public static int LOGI_LCD_COLOR_BUTTON_DOWN = 0x00002000;
        public static int LOGI_LCD_COLOR_BUTTON_MENU = 0x00004000;

        const int LOGI_LCD_MONO_WIDTH = 160;
        const int LOGI_LCD_MONO_HEIGHT = 43;

        const int LOGI_LCD_COLOR_WIDTH = 320;
        const int LOGI_LCD_COLOR_HEIGHT = 240;

        /// <summary>
        /// The LogiLcdInit function makes necessary initializations. 
        /// You must call this function prior to any other function 
        /// in the library.
        /// </summary>
        /// <param name="friendlyName">The name of the applet, this 
        /// cannot be changed after initialization.</param>
        /// <param name="lcdType">Defines the type of your applet 
        /// lcd target, it can be either LOGI_LCD_TYPE_MONO or 
        /// LOGI_LCD_TYPE_COLOR. To do both use LOGI_LCD_TYPE_MONO | LOGI_LCD_TYPE_COLOR.</param>
        /// <returns>True if initialization succeeded, false otherwise.</returns>
		[DllImport("LgLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLcdInit([MarshalAs(UnmanagedType.LPWStr)] string friendlyName, int lcdType);
        /// <summary>
        /// The LogiLcdIsConnected function checks if a device of the 
        /// type specified by the parameters is connected.
        /// </summary>
        /// <param name="lcdType">Defines the type of your applet 
        /// lcd target, it can be either LOGI_LCD_TYPE_MONO or 
        /// LOGI_LCD_TYPE_COLOR. To do both use LOGI_LCD_TYPE_MONO | LOGI_LCD_TYPE_COLOR.</param>
        /// <returns>If a device supporting the lcd type specified 
        /// is found, it returns true. If the device has not been 
        /// found or the LogiLcdInit function has not been called 
        /// before, returns false.</returns>
        [DllImport("LgLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLcdIsConnected(int lcdType);
        /// <summary>
        /// The LogiLcdIsButtonPressed function checks if the button 
        /// specified by the parameter is being pressed. Note: the button 
        /// will be considered pressed only if your applet is the one 
        /// currently in the foreground.
        /// </summary>
        /// <param name="button">This can be any of the button defines in this class (mono or color).</param>
        /// <returns>If the button specified is being pressed it returns true, otherwise false.</returns>
        [DllImport("LgLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLcdIsButtonPressed(int button);
        /// <summary>
        /// The LogiLcdUpdate function updates the lcd display. You 
        /// have to call this function every frame of your main loop to 
        /// keep the lcd updated.
        /// </summary>
        [DllImport("LgLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void LogiLcdUpdate();
        /// <summary>
        /// The LogiLcdShutdown function kills the applet and frees 
        /// memory used by the SDK.
        /// </summary>
        [DllImport("LgLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void LogiLcdShutdown();

        // Monochrome LCD functions
        /// <summary>
        /// The LogiLcdMonoSetBackground function sets the specified 
        /// image as background for the monochrome lcd device connected.
        /// 
        /// The array of pixels is organized as a rectangular area, 160 
        /// bytes wide and 43 bytes high. Despite the display being monochrome, 
        /// 8 bits per pixel are used here for simple manipulation of individual 
        /// pixels. To learn how to use GDI drawing functions efficiently 
        /// with such an arrangement, see the sample code.
        /// 
        /// The pixels are arranged thus:
        /// byte 0(0,0)     byte 1(1,0)     ...     byte 158(158,0)     byte159(159,0)
        /// byte 160(0,1)   byte 161(1,1)   ...     byte 318(158,1)     byte(319,159,0)
        /// ...             ...             ...     ...                 ...
        /// byte 6720(0,42) byte 6721(1,42) ...     byte 6878(158,42)   byte(6879(159,42)
        /// 
        /// Note: The image size must be 160x43 in order to use this function.
        /// The SDK will turn on the piexel on the screen if the value assigned
        /// to that byte is >= 128, it will remain off if the value is 
        /// less than 128.
        /// </summary>
        /// <param name="monoBitmap">The array of pixels that define the 
        /// actual monochrome bitmap.</param>
        /// <returns></returns>
        [DllImport("LgLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLcdMonoSetBackground(byte[] monoBitmap);
        /// <summary>
        /// The LogiLcdMonoSetText function sets the specified text in the
        /// requested line on the monochrome lcd device connected.
        /// </summary>
        /// <param name="lineNumber">The line on the screen  you want the text
        /// to appear. The monochrome lcd display has 4 lines, so this parameter
        /// can be any number from 0 to 3.</param>
        /// <param name="text">Defines the text you want to display.</param>
        /// <returns>True if it succeeds, false otherwise.</returns>
        [DllImport("LgLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLcdMonoSetText(int lineNumber, [MarshalAs(UnmanagedType.LPWStr)] string text);
        
        // Color LCD functions
        /// <summary>
        /// The LogiLcdColorSetBackground function sets the specified image 
        /// as background for the color lcd device connected.
        /// 
        /// The array of pixels is organized as a rectangular area, 160 bytes 
        /// wide and 43 bytes high. Despite the display being monochrome, 8 
        /// bits per pixel are used here for simple manipulation of individual 
        /// pixels. To learn how to use GDI drawing functions efficiently with 
        /// such an arrangement, see the sample code.
        /// 
        /// The pixels are arranged thus:
        /// byte 0-3(0,0)           byte 4-7(1,0)           ...         byte 1272-1275(318,0)       byte 1276-1279(319,0)
        /// byte 1280-1283(0,1)     byte 1284-1287(1,1)     ...         byte 2553-2555(318,1)       byte 2556-2559(319,1)
        /// ...                     ...                     ...         ...                         ...
        /// byte 305920-            byte 305924-            ...         byte 307192-                byte 307196-
        /// 305923(0,239)           305927(1,239)                       307195(318,239)             307199(319,239)
        /// 
        /// 32 bit values are stored in 4 consecutive bytes that represent the 
        /// RGB color values for that pixel. These values use the same top left 
        /// to bottom right raster style transform to the flat character array 
        /// with the exception that each pixel value is specified using 4 
        /// consecutive bytes (BLUE|GREEN|RED|ALPHA). Each of the bytes in the 
        /// RGB quad specify the intensity of the given color. The value ranges 
        /// from 0 (the darkest color value) to 255 (brightest color value)
        /// 
        /// Note: The image size must be 320x240 in order to use this function.
        /// </summary>
        /// <param name="colorBitmap">The array of pixels that define the actual
        /// color bitmap.</param>
        /// <returns>True if it succeeds, false otherwise.</returns>
        [DllImport("LgLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLcdColorSetBackground(byte[] colorBitmap);
        /// <summary>
        /// The LogiLcdColorSetTitle function sets the specified text in the 
        /// first line on the color lcd device connected. The font size that 
        /// will be displayed is bigger than the one used in the other lines, 
        /// so you can use this function to set the title of your applet/page.
        /// </summary>
        /// <param name="text">Defines the text you want to display as title</param>
        /// <param name="red">Value between 0 and 255, default is 255.</param>
        /// <param name="green">Value between 0 and 255, default is 255.</param>
        /// <param name="blue">Value between 0 and 255, default is 255.</param>
        /// <returns>True if it succeeds, false otherwise.</returns>
        [DllImport("LgLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLcdColorSetTitle([MarshalAs(UnmanagedType.LPWStr)] string text, int red = 255, int green = 255, int blue = 255);
        /// <summary>
        /// The LogiLcdColorSetText function sets the specified text in the
        /// requested line on the color lcd device connected.
        /// </summary>
        /// <param name="lineNumber">The line on the screen you want the text
        /// to appear. The color lcd display has 8 lines for standard text, so
        /// this parameter can be any number from 0 to 7.</param>
        /// <param name="text">Defines the text you want to display.</param>
        /// <param name="red">Value between 0 and 255, default is 255.</param>
        /// <param name="green">Value between 0 and 255, default is 255.</param>
        /// <param name="blue">Value between 0 and 255, default is 255.</param>
        /// <returns>True if it succeeds, false otherwise.</returns>
        [DllImport("LgLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLcdColorSetText(int lineNumber, [MarshalAs(UnmanagedType.LPWStr)] string text, int red = 255, int green = 255, int blue = 255);

        // UDK Functions (included for complete-ness)
        [DllImport("LgLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int LogiLcdColorSetBackgroundUDK(byte[] partialBitmap, int arraySize);
        [DllImport("LgLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int LogiLcdColorResetBackgroundUDK();
        [DllImport("LgLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int LogiLcdMonoSetBackgroundUDK(byte[] partialBitmap, int arraySize);
        [DllImport("LgLcd.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int LogiLcdMonoResetBackgroundUDK();
    }
}
