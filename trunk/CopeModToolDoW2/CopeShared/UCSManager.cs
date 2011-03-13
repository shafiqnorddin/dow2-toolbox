/*
Copyright (c) 2011 Sebastian Schoener

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */

using System;
using System.IO;
using cope.DawnOfWar2;
using cope.Helper;

namespace ModTool.Core
{
    static public class UCSManager
    {
        #region fields

        const string DOW2_UCS_FILE_NAME = "DOW2.ucs";

        static UCSFile s_dow2UCS;
        static UCSFile s_modUCS;
        static uint s_nextIndex;

        #endregion fields

        #region methods

        public static void Init()
        {
            LoggingManager.SendMessage("UCSManager - Initializing...");
            ModManager.ModLoaded += ModManagerModLoaded;
            ModManager.ModUnloaded += ModManagerModUnloaded;
            ModManager.ModLanguageChanged += ModManagerModLanguageChanged;
        }

        public static bool HasString(uint index)
        {
            if (s_modUCS != null && s_modUCS.DoesStringExist(index))
                return true;
            return s_dow2UCS == null ? false : s_dow2UCS.DoesStringExist(index);
        }

        public static string GetString(uint index)
        {
            if (s_modUCS.DoesStringExist(index))
                return s_modUCS[index];
            if (s_dow2UCS != null && s_dow2UCS.DoesStringExist(index))
                return s_dow2UCS[index];
            return null;
        }

        public static void AddString(string text)
        {
            s_modUCS.AddUCSString(text);
        }

        public static bool AddString(string text, uint index)
        {
            return s_modUCS.AddUCSString(text, index);
        }

        public static void SaveUCS()
        {
            if (s_modUCS == null || s_modUCS.StringCount <= 0)
            {
                LoggingManager.SendMessage("UCSManager - UCS file is empty, nothing to save.");
                return;
            }
            LoggingManager.SendMessage("UCSManager - Saving UCS file for current mod...");
            try
            {
                s_modUCS.WriteDataTo(s_modUCS.FilePath);
            }
            catch (Exception ex)
            {
                LoggingManager.SendMessage("UCSManager - Failed to save UCS file!");
                LoggingManager.HandleException(ex);
                 UIHelper.ShowError("Failed to save UCS file! See the log for more details!");
                s_modUCS.Close();
                return;
            }
            LoggingManager.SendMessage("UCSManager - Successfully saved UCS file!");
            s_modUCS.Close();
        }

        public static void ReloadModUCS()
        {
            LoggingManager.SendMessage("UCSManager - Reloading Mod-UCS file");
            s_nextIndex = 0;
            LoadModUCS();
            LoggingManager.SendMessage("UCSManager - Finished reloading");
        }

        static void LoadModUCS()
        {
            string path = ModManager.GameDirectory + "GameAssets\\Locale\\" + ModManager.Language + '\\';
            string modUCS = path + ModManager.ModName + ".ucs";
            LoggingManager.SendMessage(File.Exists(modUCS)
                                           ? "UCSManager - UCS file for current mod found."
                                           : "UCSManager - UCS file for current mod not found, will create a new one.");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            try
            {
                s_modUCS = new UCSFile(modUCS);
            }
            catch (CopeDoW2Exception ex)
            {
                LoggingManager.HandleException(ex);
                 UIHelper.ShowError(ex.Message);
                s_modUCS.Close();
                ModManager.RequestAppExit(ex.Message);
            }

            if (s_modUCS.NextIndex > s_nextIndex)
                s_nextIndex = s_modUCS.NextIndex;
            else
                s_modUCS.NextIndex = s_nextIndex;

            s_modUCS.Close();  
        }

        static void LoadDoW2UCS()
        {
            string path = ModManager.GameDirectory + "GameAssets\\Locale\\" + ModManager.Language + '\\';
            string dow2UCS = path + DOW2_UCS_FILE_NAME;
            if (File.Exists(dow2UCS))
            {
                LoggingManager.SendMessage("UCSManager - UCS file for vanilla DoW2 found.");
                if (s_dow2UCS == null)
                {
                    try
                    {
                        s_dow2UCS = new UCSFile(dow2UCS);
                    }
                    catch (CopeDoW2Exception ex)
                    {
                        LoggingManager.HandleException(ex);
                         UIHelper.ShowError(ex.Message);
                        ModManager.RequestAppExit(ex.Message);
                    }
                    finally
                    {
                        s_dow2UCS.Close();
                    }
                }
            }
            else
                LoggingManager.SendMessage("UCSManager - UCS file for vanilla DoW2 not found; we might operate in some strange place other than the DoW2 directory.");
        }

        #endregion methods

        #region properties

        public static uint NextIndex
        {
            get
            {
                if (s_modUCS != null)
                    return s_modUCS.NextIndex;
                return s_nextIndex;
            }
            set
            {
                if (s_modUCS != null)
                {
                    s_modUCS.NextIndex = value;
                    s_nextIndex = s_modUCS.NextIndex;
                }
                else
                    s_nextIndex = value;
            }
        }

        public static string ModUCSPath
        {
            get { return s_modUCS.FilePath; }
        }

        public static string DoW2UCSPath
        {
            get { return s_dow2UCS.FilePath; }
        }

        #endregion properties

        #region eventhandlers

        static void ModManagerModUnloaded()
        {
            SaveUCS();
            if (s_modUCS != null)
            {
                s_modUCS.Close();
                s_modUCS = null;
            }
            s_nextIndex = 0;
        }

        static void ModManagerModLoaded()
        {
            LoggingManager.SendMessage("UCSManager - Trying to load UCS files...");
            LoadModUCS();
            LoadDoW2UCS();
            LoggingManager.SendMessage("UCSManager - Done loading UCS files!");
        }

        static void ModManagerModLanguageChanged(object sender, string t)
        {
            s_dow2UCS = null;
        }

        #endregion eventhandlers
    }
}