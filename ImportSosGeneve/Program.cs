using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Vlc.DotNet.Core;
using Vlc.DotNet.Forms;
using Vlc.DotNet.Core.Interops;
using System.IO;
using System.Reflection;


namespace LecteurBandeSon
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {                                              
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            string path = Environment.GetCommandLineArgs()[0];
            string strAppDir = path.Substring(0, path.LastIndexOf('\\')); 

            //Set libvlc.dll and libvlccore.dll directory path
            //VlcContext.LibVlcDllsPath = CommonStrings.LIBVLC_DLLS_PATH_DEFAULT_VALUE_AMD64;
            VlcContext.LibVlcDllsPath = strAppDir;


            //Set the vlc plugins directory path
            //VlcContext.LibVlcPluginsPath = CommonStrings.PLUGINS_PATH_DEFAULT_VALUE_AMD64;
            VlcContext.LibVlcPluginsPath = strAppDir + "\\plugins";

            //Set the startup options
            VlcContext.StartupOptions.IgnoreConfig = true;
            VlcContext.StartupOptions.LogOptions.LogInFile = true;
            VlcContext.StartupOptions.LogOptions.ShowLoggerConsole = true;
            VlcContext.StartupOptions.LogOptions.Verbosity = VlcLogVerbosities.Debug;

            //Initialize the VlcContext
            VlcContext.Initialize(); 


            Application.Run(new Form1());


            //Close the VlcContext
            VlcContext.CloseAll(); 
        }
    }
}
