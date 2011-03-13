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
using System.Windows.Forms;
using cope.DawnOfWar2.RelicBinary;
using cope.Helper;
using cope.IO;
using ModTool.Core;
using ModTool.Core.PlugIns;

namespace RBFPlugin
{
    public partial class RBFEditor : FileTool
    {
        #region fields

        RelicBinaryFile m_rbf;

        #endregion fields
        
        #region ctors

        protected RBFEditor()
        {
            InitializeComponent();
        }

        public RBFEditor(UniFile file)
            : this()
        {
            LoadFile(file);
        }

        #endregion ctors

        #region FileTool

        public override bool Close()
        {
            if (m_rbf != null)
                m_rbf.Close();
            m_rbf = null;
            return true;
        }

        public override UniFile File
        {
            get
            {
                return m_rbf;
            }
        }

        public override void SaveFile()
        {
            if (m_rbf.FileExtension == "rbf")
                SaveFile(m_rbf.FilePath);
        }

        #endregion

        #region methods

        protected void LoadFile(UniFile file)
        {
            m_rbf = new RelicBinaryFile(file);
            if (file.FileExtension == "rbf")
            {
                m_rbf.KeyProvider = ModManager.RBFKeyProvider;
                m_rbf.UseKeyProvider = RBFSettings.UseKeyProviderForLoading;
                m_rbf.ReadData();
            }
            file.Close();
            _rbfEditorCore.Analyze(m_rbf.AttributeStructure.Root);
        }

        protected void SaveFile(string path)
        {
            m_rbf.KeyProvider = ModManager.RBFKeyProvider;
            m_rbf.UseKeyProvider = RBFSettings.UseKeyProviderForSaving;
            m_rbf.WriteDataTo(path);
            m_rbf.FilePath = path;
            if (ModManager.RBFKeyProvider.NeedsUpdate())
                ModManager.RBFKeyProvider.Update();
            var e = new FileActionEventArgs(FileActionType.Save, m_rbf);
            InvokeOnSaved(this, e);
            HasChanges = false;
        }

        #endregion methods

        #region Eventhandlers

        private void BtnSaveRBFClick(object sender, EventArgs e)
        {
            SaveFile(m_rbf.FilePath.SubstringBeforeLast('.', true) + "rbf");

            if (RBFSettings.AutoReloadInTestMode && DebugManager.HasClient && ModManager.IsModLoaded)
            {
                string path = DebugManager.SendCommand("!PropertyGroupManager_GetBasePath()");
                if (path == null)
                {
                    LoggingManager.SendMessage("RBFEditor - PropertyGroupManager_GetBasePath returned NULL, can't reload file!");
                    return;
                }
                path = path.SubstringAfterFirst(':');
                string loadPath = m_rbf.FilePath.ToLowerInvariant().SubstringAfterFirst(FileManager.AttribTree.BasePath.ToLowerInvariant())
                                                .SubstringAfterFirst(path.ToLowerInvariant());
                DebugManager.SendCommand("PropertyGroupManager_ReloadGroup(\"" + loadPath + "\")");
            }
        }

        private static void BtnOpenLibraryClick(object sender, EventArgs e)
        {
            RBFLibrary.ShowLibraryForm();
        }

        private void RBFEditorCoreHasChangesChanged(object sender, bool t)
        {
            HasChanges = t;
        }

        #endregion Eventhandlers

        #region events

        public new event KeyEventHandler KeyDown
        {
            add
            {
                _rbfEditorCore.KeyDown += value;
            }
            remove
            {
                _rbfEditorCore.KeyDown -= value;
            }
        }

        #endregion events
    }
}