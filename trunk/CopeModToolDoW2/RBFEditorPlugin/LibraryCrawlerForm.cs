using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using cope.DawnOfWar2.RelicAttribute;
using cope.Helper;
using ModTool.Core;

namespace RBFPlugin
{
    // Todo: cleanup.
    public partial class LibraryCrawlerForm : Form
    {
        private readonly MethodInvoker m_advanceProgress;
        private readonly Dictionary<string, AttribEntityExt> m_entityExts = new Dictionary<string, AttribEntityExt>();
        private readonly Dictionary<string, AttribSquadExt> m_squadExts = new Dictionary<string, AttribSquadExt>();
        private readonly Dictionary<string, AttribModifier> m_modifiers = new Dictionary<string, AttribModifier>();
        private readonly Dictionary<string, AttribAction> m_actions = new Dictionary<string, AttribAction>();
        private readonly Dictionary<string, AttribBuff> m_buffs = new Dictionary<string, AttribBuff>();
        private readonly Dictionary<string, AttribRequirement> m_requirements = new Dictionary<string, AttribRequirement>();
        private readonly Dictionary<string, AttribExpendableAction> m_expendableActions = new Dictionary<string, AttribExpendableAction>();
        private readonly Dictionary<string, AttribTarget> m_targets = new Dictionary<string, AttribTarget>();
        private readonly List<string> m_keyFilter = new List<string>();
        private RBFCrawler m_crawler;
        private bool m_bIsRunning;

        public LibraryCrawlerForm()
        {
            InitializeComponent();
            m_advanceProgress = new MethodInvoker(m_prgProgress.PerformStep);

            var taggroups = RBFLibrary.GetTagGroupNames();
            foreach (string tag in taggroups)
                m_chklbxFilter.Items.Add(tag);
            if (RBFLibrary.GetTagGroup("modifiers") != null)
                m_tbxModifierTagGroup.Text = @"modifiers";
            if (RBFLibrary.GetTagGroup("actions") != null)
                m_tbxActionTagGroup.Text = @"actions";
            if (RBFLibrary.GetTagGroup("targets") != null)
                m_tbxTargetTagGroup.Text = @"targets";
            if (RBFLibrary.GetTagGroup("buffs") != null)
                m_tbxBuffTagGroup.Text = @"buffs";
            if (RBFLibrary.GetTagGroup("expendable_actions") != null)
                m_tbxExpActionTagGroup.Text = @"expendable_actions";
            if (RBFLibrary.GetTagGroup("requirements") != null)
                m_tbxRequirementTagGroup.Text = @"requirements";
        }

        private void OnCrawlerDone()
        {
            Invoke(new MethodInvoker(OnCrawlerDoneForm));
        }

        private void OnCrawlerDoneForm()
        {
            m_crawler.OnFinished -= OnCrawlerDone;
            m_bIsRunning = false;
            m_btnStartStop.Text = @"Start";
            m_tbxActionTagGroup.Enabled = true;
            m_tbxModifierTagGroup.Enabled = true;

            DialogResult result = UIHelper.ShowYNQuestion("Question",
                                                          "Would you like to add the new entries to the library? " +
                                                          "This might OVERWRITE entries from the current library. " +
                                                          "You currently can't review the entries before adding them, " +
                                                          "it is advisable to create a backup of the library first.");
            if (result == DialogResult.No)
                return;
            AddToLibrary();
        }

        private void AdvanceProgress()
        {
            Invoke(m_advanceProgress);
        }

        private void BtnStartStopClick(object sender, EventArgs e)
        {
            if (m_bIsRunning)
                m_crawler.Stop();
            else
            {
                if (m_tbxActionTagGroup.Text == string.Empty || RBFLibrary.GetTagGroup(m_tbxActionTagGroup.Text) == null)
                {
                    UIHelper.ShowError("You need to select a valid tag group for actions!");
                    return;
                }
                if (m_tbxModifierTagGroup.Text == string.Empty || RBFLibrary.GetTagGroup(m_tbxModifierTagGroup.Text) == null)
                {
                    UIHelper.ShowError("You need to select a valid tag group for modifiers!");
                    return;
                }
                if (m_tbxRequirementTagGroup.Text == string.Empty || RBFLibrary.GetTagGroup(m_tbxRequirementTagGroup.Text) == null)
                {
                    UIHelper.ShowError("You need to select a valid tag group for requirements!");
                    return;
                }
                if (m_tbxBuffTagGroup.Text == string.Empty || RBFLibrary.GetTagGroup(m_tbxBuffTagGroup.Text) == null)
                {
                    UIHelper.ShowError("You need to select a valid tag group for buffs!");
                    return;
                }
                if (m_tbxExpActionTagGroup.Text == string.Empty || RBFLibrary.GetTagGroup(m_tbxExpActionTagGroup.Text) == null)
                {
                    UIHelper.ShowError("You need to select a valid tag group for expendable actions!");
                    return;
                }
                if (m_tbxTargetTagGroup.Text == string.Empty || RBFLibrary.GetTagGroup(m_tbxTargetTagGroup.Text) == null)
                {
                    UIHelper.ShowError("You need to select a valid tag group for targets!");
                    return;
                }
                if (FileManager.AttribTree == null)
                {
                    UIHelper.ShowError("Could not find the Attrib-Tree! Please ensure that there is a mod loaded");
                    return;
                }
                Start();
            }
                
        }

        private void Start()
        {
            m_btnStartStop.Text = @"Stop";
            m_bIsRunning = true;
            m_tbxActionTagGroup.Enabled = false;
            m_tbxModifierTagGroup.Enabled = false;

            foreach (object o in m_chklbxFilter.CheckedItems)
            {
                string tagGroupName = o as string;
                var tags = RBFLibrary.GetTagGroup(tagGroupName);
                if (tags != null)
                    m_keyFilter.AddRange(tags);
            }

            m_crawler = new RBFCrawler(ScanFile, string.Empty, AdvanceProgress);
            m_crawler.OnFinished += OnCrawlerDone;
            m_crawler.Start();
        }

        private void ScanFile(AttributeStructure data, string pathInFileTree)
        {
            foreach (AttributeValue attribValue in data.Root)
                ScanValue(attribValue);
        }

        private void ScanValue(AttributeValue value)
        {
            if (IsEntityExt(value))
            {
                string refPath = (GetRef(value).Data as string);
                if (!m_entityExts.ContainsKey(refPath))
                    m_entityExts.Add(refPath, new AttribEntityExt { Value = value });
            }
            else if (IsSquadExt(value))
            {
                string refPath = (GetRef(value).Data as string);
                if (!m_squadExts.ContainsKey(refPath))
                    m_squadExts.Add(refPath, new AttribSquadExt { Value = value });
            }
            else if (IsModifier(value))
            {
                string refPath = (GetRef(value).Data as string);
                if (m_modifiers.ContainsKey(refPath))
                    return;
                AttribModifier mod = new AttribModifier
                {
                    Value = value,
                    Category = refPath.SubstringAfterFirst('\\').SubstringBeforeFirst('\\'),
                };
                m_modifiers.Add(refPath, mod);
            }
            else if (IsAction(value))
            {
                string refPath = (GetRef(value).Data as string);
                if (m_actions.ContainsKey(refPath))
                    return;
                AttribAction action = new AttribAction
                {
                    Value = value,
                    Category = refPath.SubstringAfterFirst('\\').SubstringBeforeFirst('\\'),
                };
                m_actions.Add(refPath, action);
            }
            else if (IsBuff(value))
            {
                string refPath = (GetRef(value).Data as string);
                if (m_buffs.ContainsKey(refPath))
                    return;
                AttribBuff buff = new AttribBuff
                {
                    Value = value,
                    Category = refPath.SubstringAfterFirst('\\').SubstringBeforeFirst('\\'),
                };
                m_buffs.Add(refPath, buff);
            }
            else if (IsExpendableAction(value))
            {
                string refPath = (GetRef(value).Data as string);
                if (m_expendableActions.ContainsKey(refPath))
                    return;
                var expendableAction = new AttribExpendableAction
                {
                    Value = value,
                };
                m_expendableActions.Add(refPath, expendableAction);
            }
            else if (IsRequirement(value))
            {
                string refPath = (GetRef(value).Data as string);
                if (m_requirements.ContainsKey(refPath))
                    return;
                AttribRequirement req = new AttribRequirement
                {
                    Value = value,
                };
                m_requirements.Add(refPath, req);
            }
            else if (IsTarget(value))
            {
                string refPath = (GetRef(value).Data as string);
                if (m_targets.ContainsKey(refPath))
                    return;
                AttribTarget target = new AttribTarget
                {
                    Value = value,
                };
                m_targets.Add(refPath, target);
            }
            else return;

            // filter the childs to exclude things such as subactions etc...
            var table = value.Data as AttributeTable;
            foreach (var attribValue in table)
            {
                if (attribValue.DataType == AttributeDataType.Table && m_keyFilter.Contains(attribValue.Key))
                {
                    var childTable = attribValue.Data as AttributeTable;
                    childTable.Clear();
                }
            }
        }

        #region helpers

        private static bool HasRefWithStart(AttributeValue value, string start)
        {
            var table = value.Data as AttributeTable;
            if (table == null)
                return false;
            return
                table.Any(
                    attrib =>
                    attrib.Key == "$REF" && attrib.DataType == AttributeDataType.String &&
                    (attrib.Data as string).StartsWith(start));
        }

        private static AttributeValue GetRef(AttributeValue value)
        {
            var table = value.Data as AttributeTable;
            return table.First(attrib => attrib.Key == "$REF");
        }

        private static bool IsEntityExt(AttributeValue value)
        {
            return HasRefWithStart(value, "entity_extensions");
        }

        private static bool IsSquadExt(AttributeValue value)
        {
            return HasRefWithStart(value, "squad_extensions");
        }

        private static bool IsModifier(AttributeValue value)
        {
            return HasRefWithStart(value, "modifiers");
        }

        private static bool IsAction(AttributeValue value)
        {
            return HasRefWithStart(value, "actions");
        }

        private static bool IsRequirement(AttributeValue value)
        {
            return HasRefWithStart(value, "requirements");
        }

        private static bool IsTarget(AttributeValue value)
        {
            return HasRefWithStart(value, "types\\targets");
        }

        private static bool IsBuff(AttributeValue value)
        {
            return HasRefWithStart(value, "buffs");
        }

        private static bool IsExpendableAction(AttributeValue value)
        {
            return HasRefWithStart(value, "wargear\\expendable_actions");
        }

        #endregion

        private class AttribEntityExt
        {
            public AttributeValue Value;
        }

        private class AttribSquadExt
        {
            public AttributeValue Value;
        }

        private class AttribModifier
        {
            public AttributeValue Value;
            public string Category;
        }

        private class AttribAction
        {
            public AttributeValue Value;
            public string Category;
        }

        private class AttribBuff
        {
            public AttributeValue Value;
            public string Category;
        }

        private class AttribRequirement
        {
            public AttributeValue Value;
        }

        private class AttribExpendableAction
        {
            public AttributeValue Value;
        }

        private class AttribTarget
        {
            public AttributeValue Value;
        }

        private void AddToLibrary()
        {
            foreach (var entityExt in m_entityExts.Values)
            {
                RBFLibEntry entry = new RBFLibEntry();
                entry.Values = new List<AttributeValue>();
                entry.Submenu = "entity_extensions";
                entry.TagGroups = new string[] { };
                entry.Tags = new[] { "GameData" };
                entry.Values.Add(entityExt.Value);
                entry.Name = entityExt.Value.Key;
                RBFLibrary.RemoveEntry(entry.Name);
                RBFLibrary.AddEntry(entry);
            }

            foreach (var squadExt in m_squadExts.Values)
            {
                RBFLibEntry entry = new RBFLibEntry();
                entry.Values = new List<AttributeValue>();
                entry.Submenu = "squad_extensions";
                entry.TagGroups = new string[] { };
                entry.Tags = new[] { "GameData" };
                entry.Values.Add(squadExt.Value);
                entry.Name = squadExt.Value.Key;
                RBFLibrary.RemoveEntry(entry.Name);
                RBFLibrary.AddEntry(entry);
            }

            foreach (var modifier in m_modifiers.Values)
            {
                RBFLibEntry entry = new RBFLibEntry();
                entry.Values = new List<AttributeValue>();
                entry.Submenu = modifier.Category;
                entry.TagGroups = new[] { m_tbxModifierTagGroup.Text };
                entry.Tags = new string[] { };
                entry.Values.Add(modifier.Value);
                entry.Name = modifier.Value.Key;
                RBFLibrary.RemoveEntry(entry.Name);
                RBFLibrary.AddEntry(entry);
            }

            foreach (var action in m_actions.Values)
            {
                RBFLibEntry entry = new RBFLibEntry();
                entry.Values = new List<AttributeValue>();
                entry.Submenu = action.Category + "_actions";
                entry.TagGroups = new[] { m_tbxActionTagGroup.Text };
                entry.Tags = new string[] { };
                entry.Values.Add(action.Value);
                entry.Name = action.Value.Key;
                RBFLibrary.RemoveEntry(entry.Name);
                RBFLibrary.AddEntry(entry);
            }

            foreach (var req in m_requirements.Values)
            {
                RBFLibEntry entry = new RBFLibEntry();
                entry.Values = new List<AttributeValue>();
                entry.Submenu = "requirements";
                entry.TagGroups = new[] { m_tbxActionTagGroup.Text };
                entry.Tags = new string[] { };
                entry.Values.Add(req.Value);
                entry.Name = req.Value.Key;
                RBFLibrary.RemoveEntry(entry.Name);
                RBFLibrary.AddEntry(entry);
            }

            foreach (var buff in m_buffs.Values)
            {
                RBFLibEntry entry = new RBFLibEntry();
                entry.Values = new List<AttributeValue>();
                entry.Submenu = buff.Category + "_buffs";
                entry.TagGroups = new[] { m_tbxActionTagGroup.Text };
                entry.Tags = new string[] { };
                entry.Values.Add(buff.Value);
                entry.Name = buff.Value.Key;
                RBFLibrary.RemoveEntry(entry.Name);
                RBFLibrary.AddEntry(entry);
            }

            foreach (var target in m_targets.Values)
            {
                RBFLibEntry entry = new RBFLibEntry();
                entry.Values = new List<AttributeValue>();
                entry.Submenu = "targets";
                entry.TagGroups = new[] { m_tbxActionTagGroup.Text };
                entry.Tags = new string[] { };
                entry.Values.Add(target.Value);
                entry.Name = target.Value.Key;
                RBFLibrary.RemoveEntry(entry.Name);
                RBFLibrary.AddEntry(entry);
            }

            foreach (var expendable in m_expendableActions.Values)
            {
                RBFLibEntry entry = new RBFLibEntry();
                entry.Values = new List<AttributeValue>();
                entry.Submenu = "expendable_actions";
                entry.TagGroups = new[] { m_tbxActionTagGroup.Text };
                entry.Tags = new string[] { };
                entry.Values.Add(expendable.Value);
                entry.Name = expendable.Value.Key;
                RBFLibrary.RemoveEntry(entry.Name);
                RBFLibrary.AddEntry(entry);
            }
        }
    }
}
