using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace Coach
{
    public partial class Form1 : Form
    {
        //MD5 hash
        string SteamHash = "bb0d932f267c0ce1b1045e68a3d12ffb"; //Steam Build ID: 4940658 (Tested on August 2. 2024)
        string EpicHash1 = "1d91aa77a1fdb86673a501696c8ad23c"; //EPIC STORE Version (Tested on August 2. 2024)
        string currenthash = "";

        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(IntPtr hProcess);


        Process process;
        string _alienfile;
        string _androidfile;
        string _heavyandroidfile;
        string _facehuggerfile;
        string _riotguardfile;
        string _civfile;
        string _secguardfile;
        string _innocentfile;


        string pathforgamefolder = "";
        string restartmessage = "Any change of AI functionality require a restart of game. Everything in the \"Other\" section can be done realtime.";

        public Form1()
        {
            InitializeComponent();



            Process[] proc = Process.GetProcessesByName("AI");
            if (proc.Length == 0)
            {
                MessageBox.Show("Game process \"AI.exe\" for Alien Isolation not running,\r\nplease start the game.", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            else
            {
                Hash hash = new Hash();

                process = proc[0];
                currenthash = hash.GetHash(process.Modules[0].FileName);

                if (currenthash == SteamHash || currenthash == EpicHash1)
                {

                    string _aipath = process.Modules[0].FileName.Replace("AI.exe", "");
                    pathforgamefolder = _aipath;


                    if (File.Exists(pathforgamefolder + @"\DATA\UI\MOVIES\AMD_IDENT.USM") || File.Exists(pathforgamefolder + @"\DATA\UI\MOVIES\CA_IDENT.USM") || File.Exists(pathforgamefolder + @"\DATA\UI\MOVIES\FOX_IDENT.USM"))
                    {
                        btnDisableIntroMovies.Text = "Disable intro movies";
                    }
                    else
                    {
                        btnDisableIntroMovies.Text = "Enable intro movies";
                    }


                    if (process != null)
                    {
                        if (process.ProcessName == "AI")
                        {
                            IntPtr pointer = (IntPtr)0x0;
                            if (currenthash == SteamHash)
                            {

                                pointer = GetPointerAddress(new int[] { 0x88 }, 0x12F0C88, (IntPtr)0x24);
                            }
                            else if (currenthash == EpicHash1)
                            {
                                pointer = GetPointerAddress(new int[] { 0x88 }, 0x130D1A8, (IntPtr)0x24);
                            }

                            byte[] tempbuffer;
                            int bytesRead;

                            tempbuffer = ReadMemory(process, pointer, 12, out bytesRead);



                            if (tempbuffer[0] == 0 && tempbuffer[4] == 0)
                            {
                                comboBox1.SelectedIndex = 1;

                            }
                            else if (tempbuffer[0] == 1 && tempbuffer[4] == 1)
                            {
                                comboBox1.SelectedIndex = 2;

                            }
                            else if (tempbuffer[0] == 2 && tempbuffer[4] == 2)
                            {
                                comboBox1.SelectedIndex = 3;

                            }
                            else if (tempbuffer[0] == 3 && tempbuffer[4] == 3)
                            {

                                comboBox1.SelectedIndex = 4;
                            }
                            else if (tempbuffer[0] == 4 && tempbuffer[4] == 4)
                            {
                                comboBox1.SelectedIndex = 0;

                            }


                            if (File.Exists(_aipath + @"\AI.exe"))
                            {

                                _innocentfile = _aipath + @"\DATA\CHR_INFO\ATTRIBUTES\INNOCENT.BML";



                                if (File.Exists(_innocentfile))
                                {



                                    string data = GetBehaviorTree(_innocentfile);


                                    if (data.Contains("NPC_innocent_behave"))
                                    {
                                        btnInnocent.Text = "Disable Civilians (innocent)";

                                    }
                                    else if (data.Contains("NoBehaviour"))
                                    {
                                        btnInnocent.Text = "Enable Civilians (innocent)";
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("File \"INNOCENT.BML\" could not be found.");
                                    btnInnocent.Enabled = false;
                                }




                                _alienfile = _aipath + @"\DATA\CHR_INFO\ATTRIBUTES\ALIEN.BML";



                                if (File.Exists(_alienfile))
                                {



                                    string data = GetBehaviorTree(_alienfile);


                                    if (data.Contains("alien_behave"))
                                    {
                                        btnAlien.Text = "Disable Alien";

                                    }
                                    else if (data.Contains("NoBehaviour"))
                                    {
                                        btnAlien.Text = "Enable Alien";
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("File \"ALIEN.BML\" could not be found.");
                                    btnAlien.Enabled = false;
                                }


                                _androidfile = _aipath + @"\DATA\CHR_INFO\ATTRIBUTES\ANDROID.BML";

                                if (File.Exists(_androidfile))
                                {

                                    string data = GetBehaviorTree(_androidfile);


                                    if (data.Contains("android_behave"))
                                    {
                                        btnAndroid.Text = "Disable Normal Androids";

                                    }
                                    else if (data.Contains("NoBehaviour"))
                                    {
                                        btnAndroid.Text = "Enable Normal Androids";

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("File \"ANDROID.BML\" could not be found.");
                                    btnAndroid.Enabled = false;
                                }

                                _heavyandroidfile = _aipath + @"\DATA\CHR_INFO\ATTRIBUTES\ANDROID_HEAVY.BML";

                                if (File.Exists(_heavyandroidfile))
                                {
                                    string data = GetBehaviorTree(_heavyandroidfile);


                                    if (data.Contains("android_behave"))
                                    {
                                        btnHeavyAndroid.Text = "Disable Hazmat Androids";

                                    }
                                    else if (data.Contains("NoBehaviour"))
                                    {
                                        btnHeavyAndroid.Text = "Enable Hazmat Androids";

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("File \"ANDROID_HEAVY.BML\" could not be found.");
                                    btnHeavyAndroid.Enabled = false;
                                }

                                _facehuggerfile = _aipath + @"\DATA\CHR_INFO\ATTRIBUTES\FACEHUGGER.BML";

                                if (File.Exists(_facehuggerfile))
                                {
                                    string data = GetBehaviorTree(_facehuggerfile);


                                    if (data.Contains("facehugger_behave"))
                                    {
                                        btnFaceHugger.Text = "Disable Facehuggers";

                                    }
                                    else if (data.Contains("NoBehaviour"))
                                    {
                                        btnFaceHugger.Text = "Enable Facehuggers";

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("File \"FACEHUGGER.BML\" could not be found.");
                                    btnFaceHugger.Enabled = false;
                                }

                                _riotguardfile = _aipath + @"\DATA\CHR_INFO\ATTRIBUTES\RIOT_GUARD.BML";

                                if (File.Exists(_riotguardfile))
                                {
                                    string data = GetBehaviorTree(_riotguardfile);


                                    if (data.Contains("NPC_Human_behave"))
                                    {
                                        btnRiotGuards.Text = "Disable Riot Guards";

                                    }
                                    else if (data.Contains("NoBehaviour"))
                                    {
                                        btnRiotGuards.Text = "Enable Riot Guards";

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("File \"RIOT_GUARD.BML\" could not be found.");
                                    btnRiotGuards.Enabled = false;
                                }

                                _civfile = _aipath + @"\DATA\CHR_INFO\ATTRIBUTES\CIVILIAN.BML";

                                if (File.Exists(_civfile))
                                {
                                    string data = GetBehaviorTree(_civfile);


                                    if (data.Contains("NPC_Human_behave"))
                                    {
                                        btnCivilian.Text = "Disable Civilians (hostile)";

                                    }
                                    else if (data.Contains("NoBehaviour"))
                                    {
                                        btnCivilian.Text = "Enable Civilians (hostile)";

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("File \"CIVILIAN.BML\" could not be found.");
                                    btnCivilian.Enabled = false;
                                }

                                _secguardfile = _aipath + @"\DATA\CHR_INFO\ATTRIBUTES\SECURITY_GUARD.BML";

                                if (File.Exists(_secguardfile))
                                {

                                    string data = GetBehaviorTree(_secguardfile);


                                    if (data.Contains("NPC_Human_behave"))
                                    {
                                        btnSecGuards.Text = "Disable Security Guards";

                                    }
                                    else if (data.Contains("NoBehaviour"))
                                    {
                                        btnSecGuards.Text = "Enable Security Guards";

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("File \"SECURITY_GUARD.BML\" could not be found.");
                                    btnSecGuards.Enabled = false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Did you delete AI.exe?");
                                Environment.Exit(0);
                            }



                        }
                        else
                        {

                            MessageBox.Show("Not the right process!");
                            Environment.Exit(0);
                        }
                    }

                    Warning war = new Warning();
                    war.Show();

                    this.WindowState = FormWindowState.Minimized;
                }
                else
                {
                    MessageBox.Show("You are running an unsupported version of the game!\r\n\r\nOnly EPIC or Steam version of the game tested before this date is working. (Tested on August 2nd 2024)", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }

            }



        }


        private void btnSecGuards_Click(object sender, EventArgs e)
        {

            string data = GetBehaviorTree(_secguardfile);

            if (data.Contains("NoBehaviour"))
            {
                SetBehaviorTree(_secguardfile, "NPC_Human_behave");
                btnSecGuards.Text = "Disable Security Guards";

            }
            else
            {
                SetBehaviorTree(_secguardfile, "NoBehaviour");
                btnSecGuards.Text = "Enable Security Guards";

            }

            MessageBox.Show(restartmessage);
        }
        private void btnCivilian_Click(object sender, EventArgs e)
        {

            string data = GetBehaviorTree(_civfile);

            if (data.Contains("NoBehaviour"))
            {
                SetBehaviorTree(_civfile, "NPC_Human_behave");
                btnCivilian.Text = "Disable Civilians (hostile)";

            }
            else
            {
                SetBehaviorTree(_civfile, "NoBehaviour");
                btnCivilian.Text = "Enable Civilians (hostile)";

            }

            MessageBox.Show(restartmessage);
        }
        private void btnRiotGuards_Click(object sender, EventArgs e)
        {

            string data = GetBehaviorTree(_riotguardfile);

            if (data.Contains("NoBehaviour"))
            {
                SetBehaviorTree(_riotguardfile, "NPC_Human_behave");
                btnRiotGuards.Text = "Disable Riot Guards";

            }
            else
            {
                SetBehaviorTree(_riotguardfile, "NoBehaviour");
                btnRiotGuards.Text = "Enable Riot Guards";

            }


            MessageBox.Show(restartmessage);
        }
        private void btnFaceHugger_Click(object sender, EventArgs e)
        {
            string data = GetBehaviorTree(_facehuggerfile);

            if (data.Contains("NoBehaviour"))
            {
                SetBehaviorTree(_facehuggerfile, "facehugger_behave");
                btnFaceHugger.Text = "Disable Facehuggers";

            }
            else
            {
                SetBehaviorTree(_facehuggerfile, "NoBehaviour");
                btnFaceHugger.Text = "Enable Facehuggers";

            }




            MessageBox.Show(restartmessage);
        }
        private void btnHeavyAndroid_Click(object sender, EventArgs e)
        {

            string data = GetBehaviorTree(_heavyandroidfile);

            if (data.Contains("NoBehaviour"))
            {
                SetBehaviorTree(_heavyandroidfile, "android_behave");
                btnHeavyAndroid.Text = "Disable Hazmat Androids";

            }
            else
            {
                SetBehaviorTree(_heavyandroidfile, "NoBehaviour");
                btnHeavyAndroid.Text = "Enable Hazmat Androids";

            }


            MessageBox.Show(restartmessage);
        }
        private void btnAndroid_Click(object sender, EventArgs e)
        {

            string data = GetBehaviorTree(_androidfile);

            if (data.Contains("NoBehaviour"))
            {
                SetBehaviorTree(_androidfile, "android_behave");
                btnAndroid.Text = "Disable Normal Androids";

            }
            else
            {
                SetBehaviorTree(_androidfile, "NoBehaviour");
                btnAndroid.Text = "Enable Normal Androids";

            }

            MessageBox.Show(restartmessage);
        }

        private void btnAlien_Click(object sender, EventArgs e)
        {

            string data = GetBehaviorTree(_alienfile);

            if (data.Contains("NoBehaviour"))
            {
                SetBehaviorTree(_alienfile, "alien_behave");
                btnAlien.Text = "Disable Alien";

            }
            else
            {
                SetBehaviorTree(_alienfile, "NoBehaviour");
                btnAlien.Text = "Enable Alien";

            }


            MessageBox.Show(restartmessage);
 
        }


        public IntPtr GetPointerAddress(int[] _offsets, int _pointer, IntPtr _last_offset)
        {
            IntPtr baseaddress = process.Modules[0].BaseAddress;
            IntPtr baseandpointer = IntPtr.Add(baseaddress, _pointer);
            byte[] tempbuffer;
            int bytesRead;

            tempbuffer = ReadMemory(process, baseandpointer, 4, out bytesRead);
            baseandpointer = (IntPtr)BitConverter.ToInt32(tempbuffer, 0);

            foreach (var offset in _offsets)
            {
                baseandpointer = IntPtr.Add(baseandpointer, offset);
                tempbuffer = ReadMemory(process, baseandpointer, 4, out bytesRead);
                baseandpointer = (IntPtr)BitConverter.ToInt32(tempbuffer, 0);
            }


            return IntPtr.Add(baseandpointer, (int)_last_offset);

        }


        public static byte[] ReadMemory(Process process, IntPtr address, int numOfBytes, out int bytesRead)
        {
            IntPtr hProc = OpenProcess(ProcessAccessFlags.All, false, process.Id);
            byte[] buffer = new byte[numOfBytes];

            ReadProcessMemory(hProc, address, buffer, numOfBytes, out bytesRead);
            CloseHandle(hProc);
            return buffer;
        }



        public void WriteMem(Process _process, byte[] _buffer, IntPtr _address)
        {
            IntPtr processHandle = OpenProcess(ProcessAccessFlags.All, false, _process.Id);

            int bytesWritten = 0;

            WriteProcessMemory(processHandle, _address, _buffer, (uint) _buffer.Length, out bytesWritten);

        }


        private void btnHackerTool_Click(object sender, EventArgs e)
        {
            if (process != null)
            {
                if (process.ProcessName == "AI")
                {
                    if (currenthash == SteamHash)
                    {

                        WriteMem(process, new byte[] { 0x3, 0x0, 0x0, 0x0, 0x1 }, GetPointerAddress(new int[] { 0x44 }, 0x12F0C88, (IntPtr)0x39C));
                    }
                    else if (currenthash == EpicHash1)
                    {
                        WriteMem(process, new byte[] { 0x3, 0x0, 0x0, 0x0, 0x1 }, GetPointerAddress(new int[] { 0x18 }, 0x1253E5C, (IntPtr)0xD0C));
                    }
                    btnHackerTool.Text = "Done!";
                }
                else
                {

                    MessageBox.Show("Not the right process!");
                }
            }
            else
            {
                MessageBox.Show("Game Process not running!");

            }
        }

        private void BtnTorch_Click(object sender, EventArgs e)
        {
            if (process != null)
            {
                if (process.ProcessName == "AI")
                {
                    if (currenthash == SteamHash)
                    {
                        WriteMem(process, new byte[] { 0x3, 0x0, 0x0, 0x0, 0x1 }, GetPointerAddress(new int[] { 0x44 }, 0x12F0C88, (IntPtr)0x394));
                    }
                    else if (currenthash == EpicHash1)
                    {
                        WriteMem(process, new byte[] { 0x3, 0x0, 0x0, 0x0, 0x1 }, GetPointerAddress(new int[] { 0x18 }, 0x1253E5C, (IntPtr)0xD04));
                    }
                    BtnTorch.Text = "Done!";
                }
                else
                {

                    MessageBox.Show("Not the right process!");
                }
            }
            else
            {
                MessageBox.Show("Game Process not running!");

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (process != null)
            {
                if (process.ProcessName == "AI")
                {

                    byte[] tempbuffer = null;

                    if (comboBox1.SelectedIndex == 1)
                    {
                        tempbuffer = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0 };
                    }
                    else if (comboBox1.SelectedIndex == 2)
                    {
                        tempbuffer = new byte[] { 0x1, 0x0, 0x0, 0x0, 0x1 };
                    }
                    else if (comboBox1.SelectedIndex == 3)
                    {
                        tempbuffer = new byte[] { 0x2, 0x0, 0x0, 0x0, 0x2 };
                    }
                    else if (comboBox1.SelectedIndex == 4)
                    {
                        tempbuffer = new byte[] { 0x3, 0x0, 0x0, 0x0, 0x3 };
                    }
                    else if (comboBox1.SelectedIndex == 0)
                    {
                        tempbuffer = new byte[] { 0x4, 0x0, 0x0, 0x0, 0x4 };
                    }

                    if (currenthash == SteamHash)
                    {
                        WriteMem(process, tempbuffer, GetPointerAddress(new int[] { 0x88 }, 0x12F0C88, (IntPtr)0x24));
                    }
                    else if (currenthash == EpicHash1)
                    {
                        WriteMem(process, tempbuffer, GetPointerAddress(new int[] { 0x88 }, 0x130D1A8, (IntPtr)0x24));
                    }
                }
                else
                {

                    MessageBox.Show("Not the right process!");
                }
            }
            else
            {
                MessageBox.Show("Game Process not running!");

            }
        }

        private void btnGasmask_Click(object sender, EventArgs e)
        {
            if (process != null)
            {
                if (process.ProcessName == "AI")
                {
                    if(currenthash == SteamHash)
                    {
                        WriteMem(process, new byte[] { 0x1 }, GetPointerAddress(new int[] { 0x44 }, 0x12F0C88, (IntPtr)0x390));
                    }
                    else if(currenthash == EpicHash1)
                    {

                        WriteMem(process, new byte[] { 0x1 }, GetPointerAddress(new int[] { 0x18 }, 0x1253E5C, (IntPtr)0xD00));
                    }

                    
                    btnEnableGasMask.Text = "Done!";
                }
                else
                {

                    MessageBox.Show("Not the right process!");
                }
            }
            else
            {
                MessageBox.Show("Game Process not running!");

            }
        }

        private void btnDisableIntroMovies_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("This will close the game, are you sure you want to do it now?", "Close?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                Process[] proc = Process.GetProcessesByName("AI");

                foreach (var item in proc)
                {
                    item.Kill();
                    item.WaitForExit();
                }


                if (File.Exists(pathforgamefolder + @"\DATA\UI\MOVIES\AMD_IDENT.USM") || File.Exists(pathforgamefolder + @"\DATA\UI\MOVIES\CA_IDENT.USM") || File.Exists(pathforgamefolder + @"\DATA\UI\MOVIES\FOX_IDENT.USM"))
                {

                    try
                    {

                        File.Copy(pathforgamefolder + @"\DATA\UI\MOVIES\AMD_IDENT.USM", pathforgamefolder + @"\DATA\UI\MOVIES\AMD_IDENT.USM.bak", true);
                        File.Delete(pathforgamefolder + @"\DATA\UI\MOVIES\AMD_IDENT.USM");
                    }
                    catch { }



                    try
                    {
                        File.Copy(pathforgamefolder + @"\DATA\UI\MOVIES\CA_IDENT.USM", pathforgamefolder + @"\DATA\UI\MOVIES\CA_IDENT.USM.bak", true);
                        File.Delete(pathforgamefolder + @"\DATA\UI\MOVIES\CA_IDENT.USM");
                    }
                    catch { }



                    try
                    {
                        File.Copy(pathforgamefolder + @"\DATA\UI\MOVIES\FOX_IDENT.USM", pathforgamefolder + @"\DATA\UI\MOVIES\FOX_IDENT.USM.bak", true);
                        File.Delete(pathforgamefolder + @"\DATA\UI\MOVIES\FOX_IDENT.USM");
                    }
                    catch { }


                    btnDisableIntroMovies.Text = "Enable Intro Movies";

                }
                else
                {
                    if (!File.Exists(pathforgamefolder + @"\DATA\UI\MOVIES\AMD_IDENT.USM"))
                    {
                        try
                        {
                            File.Move(pathforgamefolder + @"\DATA\UI\MOVIES\AMD_IDENT.USM.bak", pathforgamefolder + @"\DATA\UI\MOVIES\AMD_IDENT.USM");
                        }
                        catch { }
                    }
                    if (!File.Exists(pathforgamefolder + @"\DATA\UI\MOVIES\CA_IDENT.USM"))
                    {
                        try
                        {
                            File.Move(pathforgamefolder + @"\DATA\UI\MOVIES\CA_IDENT.USM.bak", pathforgamefolder + @"\DATA\UI\MOVIES\CA_IDENT.USM");
                        }
                        catch { }
                    }
                    if (!File.Exists(pathforgamefolder + @"\DATA\UI\MOVIES\FOX_IDENT.USM"))
                    {
                        try
                        {
                            File.Move(pathforgamefolder + @"\DATA\UI\MOVIES\FOX_IDENT.USM.bak", pathforgamefolder + @"\DATA\UI\MOVIES\FOX_IDENT.USM");
                        }
                        catch { }
                    }

                    btnDisableIntroMovies.Text = "Disable Intro Movies";
                }

            }

        }


        public string GetBehaviorTree(string filename)
        {

            FileStream strm = File.OpenRead(filename);
            BinaryReader br = new BinaryReader(strm);
            AlienBML.BML bML = new AlienBML.BML();
            bML.ReadBML(br);
            strm.Close();
            br.Close();

            string xmlout = "";
            bML.ExportXML(ref xmlout);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlout);
            var tag = doc.GetElementsByTagName("Behavior_Tree");
            return tag[0].InnerText;

        }

        public void SetBehaviorTree(string filename, string newbehavior)
        {

            FileStream strm = File.OpenRead(filename);
            BinaryReader br = new BinaryReader(strm);
            AlienBML.BML bML = new AlienBML.BML();
            bML.ReadBML(br);
            strm.Close();
            br.Close();

            string xmlout = "";
            bML.ExportXML(ref xmlout);
            bML = null;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlout);
            var tag = doc.GetElementsByTagName("Behavior_Tree");
            tag[0].InnerText = newbehavior;


            string data = XMLToString(doc);

            Stream stream = GenerateStreamFromString(data);
        
            br = new BinaryReader(stream);

            bML = new AlienBML.BML();
            bML.ReadXML(br);
            stream.Close();
            br.Close();

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            BinaryWriter bw = new BinaryWriter(File.OpenWrite(filename));


            bML.ExportBML(bw);
            
            bw.Close();

        }

        public string XMLToString(XmlDocument xmldata)
        {

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineChars = Environment.NewLine;
            settings.NewLineHandling = NewLineHandling.Replace;

            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter, settings))
            {
                xmldata.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }

        }

        public Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private void btnInnocent_Click(object sender, EventArgs e)
        {
            string data = GetBehaviorTree(_innocentfile);

            if (data.Contains("NoBehaviour"))
            {
                SetBehaviorTree(_innocentfile, "NPC_innocent_behave");
                btnInnocent.Text = "Disable Civilians (innocent)";

            }
            else
            {
                SetBehaviorTree(_innocentfile, "NoBehaviour");
                btnInnocent.Text = "Enable Civilians (innocent)";

            }


            MessageBox.Show(restartmessage);

            MessageBox.Show("Killing innocent civilians will fail the mission.");

        }
    }

}
