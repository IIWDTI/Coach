using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Coach
{
    public partial class Form1 : Form
    {
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
        bool _alienstatus;
        bool _androidstatus;
        bool _heavyandroidstatus;
        bool _facehuggerstatus;
        bool _riotguardstatus;
        bool _civstatus;
        bool _secguardstatus;

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
                process = proc[0];
                string _aipath = process.Modules[0].FileName.Replace("AI.exe","");
                pathforgamefolder = _aipath;

                if (File.Exists(pathforgamefolder + @"\DATA\UI\MOVIES\AMD_IDENT.USM"))
                {
                    btnDisableIntroMovies.Text = "Disable Intro movies";
                }
                else
                {
                    btnDisableIntroMovies.Text = "Enable Intro movies";
                }


                if (process != null)
                {
                    if (process.ProcessName == "AI")
                    {


                        IntPtr pointer = GetPointerAddress(new int[] { 0x88 }, 0x12F0C88, (IntPtr)0x20);

                        byte[] tempbuffer;
                        int bytesRead;

                        tempbuffer = ReadMemory(process, pointer, 12, out bytesRead);



                        if (tempbuffer[4] == 0 && tempbuffer[8] == 0)
                        {
                            comboBox1.SelectedIndex = 1;

                        }
                        else if (tempbuffer[4] == 1 && tempbuffer[8] == 1)
                        {
                            comboBox1.SelectedIndex = 2;

                        }
                        else if (tempbuffer[4] == 2 && tempbuffer[8] == 2)
                        {
                            comboBox1.SelectedIndex = 3;

                        }
                        else if (tempbuffer[4] == 3 && tempbuffer[8] == 3)
                        {

                            comboBox1.SelectedIndex = 4;
                        }
                        else if (tempbuffer[4] == 4 && tempbuffer[8] == 4)
                        {
                            comboBox1.SelectedIndex = 0;

                        }


                        if (File.Exists(_aipath + @"\AI.exe"))
                        {
                            _alienfile = _aipath + @"\DATA\CHR_INFO\ATTRIBUTES\ALIEN.BML";

                            if (File.Exists(_alienfile))
                            {
                                if (!File.Exists(_alienfile + ".BAK"))
                                {
                                    File.Copy(_alienfile, _alienfile + ".BAK");
                                }

                                byte[] _aliendata = File.ReadAllBytes(_alienfile);
                                string _changedaliendata = System.Text.Encoding.Default.GetString(_aliendata, 8584, _aliendata.Length - 8584);
                                _aliendata = null;
                                if (_changedaliendata.Contains("alien_behave"))
                                {
                                    btnAlien.Text = "Disable Alien";
                                    _alienstatus = true;
                                }
                                else if (_changedaliendata.Contains("NoBehaviour"))
                                {
                                    btnAlien.Text = "Enable Alien";
                                    _alienstatus = false;
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

                                if (!File.Exists(_androidfile + ".BAK"))
                                {
                                    File.Copy(_androidfile, _androidfile + ".BAK");
                                }

                                byte[] _androiddata = File.ReadAllBytes(_androidfile);
                                string _changedandroiddata = System.Text.Encoding.Default.GetString(_androiddata, 9273, _androiddata.Length - 9273);
                                _androiddata = null;
                                if (_changedandroiddata.Contains("android_behave"))
                                {
                                    btnAndroid.Text = "Disable Normal Androids";
                                    _androidstatus = true;
                                }
                                else if (_changedandroiddata.Contains("NoBehaviour"))
                                {
                                    btnAndroid.Text = "Enable Normal Androids";
                                    _androidstatus = false;
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
                                if (!File.Exists(_heavyandroidfile + ".BAK"))
                                {
                                    File.Copy(_heavyandroidfile, _heavyandroidfile + ".BAK");
                                }

                                byte[] _heavyandroiddata = File.ReadAllBytes(_heavyandroidfile);
                                string _changedheavyandroiddata = System.Text.Encoding.Default.GetString(_heavyandroiddata, 8090, _heavyandroiddata.Length - 8090);
                                _heavyandroiddata = null;
                                if (_changedheavyandroiddata.Contains("android_behave"))
                                {
                                    btnHeavyAndroid.Text = "Disable Hazmat Androids";
                                    _heavyandroidstatus = true;
                                }
                                else if (_changedheavyandroiddata.Contains("NoBehaviour"))
                                {
                                    btnHeavyAndroid.Text = "Enable Hazmat Androids";
                                    _heavyandroidstatus = false;
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

                                if (!File.Exists(_facehuggerfile + ".BAK"))
                                {
                                    File.Copy(_facehuggerfile, _facehuggerfile + ".BAK");
                                }

                                byte[] _facehuggerdata = File.ReadAllBytes(_facehuggerfile);
                                string _changedfacehuggerdata = System.Text.Encoding.Default.GetString(_facehuggerdata, 6950, _facehuggerdata.Length - 6950);
                                _facehuggerdata = null;
                                if (_changedfacehuggerdata.Contains("facehugger_behave"))
                                {
                                    btnFaceHugger.Text = "Disable Facehuggers";
                                    _facehuggerstatus = true;
                                }
                                else if (_changedfacehuggerdata.Contains("NoBehaviour"))
                                {
                                    btnFaceHugger.Text = "Enable Facehuggers";
                                    _facehuggerstatus = false;
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
                                if (!File.Exists(_riotguardfile + ".BAK"))
                                {
                                    File.Copy(_riotguardfile, _riotguardfile + ".BAK");
                                }

                                byte[] _riotguarddata = File.ReadAllBytes(_riotguardfile);
                                string _changedriotguarddata = System.Text.Encoding.Default.GetString(_riotguarddata, 6116, 0x11);
                                _riotguarddata = null;
                                if (_changedriotguarddata.Contains("NPC_Human_behave"))
                                {
                                    btnRiotGuards.Text = "Disable Riot Guards";
                                    _riotguardstatus = true;
                                }
                                else if (_changedriotguarddata.Contains("NoBehaviour"))
                                {
                                    btnRiotGuards.Text = "Enable Riot Guards";
                                    _riotguardstatus = false;
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
                                if (!File.Exists(_civfile + ".BAK"))
                                {
                                    File.Copy(_civfile, _civfile + ".BAK");
                                }

                                byte[] _civdata = File.ReadAllBytes(_civfile);
                                string _changedcivdata = System.Text.Encoding.Default.GetString(_civdata, 33283, 0x11);
                                _civdata = null;
                                if (_changedcivdata.Contains("NPC_Human_behave"))
                                {
                                    btnCivilian.Text = "Disable Civilians (hostile)";
                                    _civstatus = true;
                                }
                                else if (_changedcivdata.Contains("NoBehaviour"))
                                {
                                    btnCivilian.Text = "Enable Civilians (hostile)";
                                    _civstatus = false;
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

                                if (!File.Exists(_secguardfile + ".BAK"))
                                {
                                    File.Copy(_secguardfile, _secguardfile + ".BAK");
                                }

                                byte[] _secguarddata = File.ReadAllBytes(_secguardfile);
                                string _changedsecguarddata = System.Text.Encoding.Default.GetString(_secguarddata, 6101, 0x11);
                                _secguarddata = null;
                                if (_changedsecguarddata.Contains("NPC_Human_behave"))
                                {
                                    btnSecGuards.Text = "Disable Security Guards";
                                    _secguardstatus = true;
                                }
                                else if (_changedsecguarddata.Contains("NoBehaviour"))
                                {
                                    btnSecGuards.Text = "Enable Security Guards";
                                    _secguardstatus = false;
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



        }
        private void btnSecGuards_Click(object sender, EventArgs e)
        {
            if (_secguardstatus)
            {
 
                ReplaceData(_secguardfile, 6101, new byte[] { 0x4E, 0x6F, 0x42, 0x65, 0x68, 0x61, 0x76, 0x69, 0x6F, 0x75, 0x72, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                btnSecGuards.Text = "Enable Security Guards";
                _secguardstatus = false;
            }
            else if (!_secguardstatus)
            {

                ReplaceData(_secguardfile, 6101, new byte[] { 0x4E, 0x50, 0x43, 0x5F, 0x48, 0x75, 0x6D, 0x61, 0x6E, 0x5F, 0x62, 0x65, 0x68, 0x61, 0x76, 0x65, 0x00 });
                btnSecGuards.Text = "Disable Security Guards";
                _secguardstatus = true;
            }

            MessageBox.Show(restartmessage);
        }
        private void btnCivilian_Click(object sender, EventArgs e)
        {
            if (_civstatus)
            {

                ReplaceData(_civfile, 33283, new byte[] { 0x4E, 0x6F, 0x42, 0x65, 0x68, 0x61, 0x76, 0x69, 0x6F, 0x75, 0x72, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                btnCivilian.Text = "Enable Civilians (hostile)";
                _civstatus = false;
            }
            else if (!_civstatus)
            {

                ReplaceData(_civfile, 33283, new byte[] { 0x4E, 0x50, 0x43, 0x5F, 0x48, 0x75, 0x6D, 0x61, 0x6E, 0x5F, 0x62, 0x65, 0x68, 0x61, 0x76, 0x65, 0x00 });
                btnCivilian.Text = "Disable Civilians (hostile)";
                _civstatus = true;
            }

            MessageBox.Show(restartmessage);
        }
        private void btnRiotGuards_Click(object sender, EventArgs e)
        {
            if (_riotguardstatus)
            {

                ReplaceData(_riotguardfile, 6116, new byte[] { 0x4E, 0x6F, 0x42, 0x65, 0x68, 0x61, 0x76, 0x69, 0x6F, 0x75, 0x72, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                btnRiotGuards.Text = "Enable Riot Guards";
                _riotguardstatus = false;
            }
            else if (!_riotguardstatus)
            {
 
                ReplaceData(_riotguardfile, 6116, new byte[] { 0x4E, 0x50, 0x43, 0x5F, 0x48, 0x75, 0x6D, 0x61, 0x6E, 0x5F, 0x62, 0x65, 0x68, 0x61, 0x76, 0x65, 0x00 });
                btnRiotGuards.Text = "Disable Riot Guards";
                _riotguardstatus = true;
            }

            MessageBox.Show(restartmessage);
        }
        private void btnFaceHugger_Click(object sender, EventArgs e)
        {
            if (_facehuggerstatus)
            {
 
                ReplaceData(_facehuggerfile, 6950, new byte[] { 0x4E, 0x6F, 0x42, 0x65, 0x68, 0x61, 0x76, 0x69, 0x6F, 0x75, 0x72, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                btnFaceHugger.Text = "Enable Facehuggers";
                _facehuggerstatus = false;
            }
            else if (!_facehuggerstatus)
            {
 
                ReplaceData(_facehuggerfile, 6950, new byte[] { 0x66, 0x61, 0x63, 0x65, 0x68, 0x75, 0x67, 0x67, 0x65, 0x72, 0x5F, 0x62, 0x65, 0x68, 0x61, 0x76, 0x65, 0x00, 0x00 });
                btnFaceHugger.Text = "Disable Facehuggers";
                _facehuggerstatus = true;
            }

            MessageBox.Show(restartmessage);
        }
        private void btnHeavyAndroid_Click(object sender, EventArgs e)
        {
            if (_heavyandroidstatus)
            {

                ReplaceData(_heavyandroidfile, 8090, new byte[] { 0x4E, 0x6F, 0x42, 0x65, 0x68, 0x61, 0x76, 0x69, 0x6F, 0x75, 0x72, 0x00, 0x00, 0x00, });
                btnHeavyAndroid.Text = "Enable Hazmat Androids";
                _heavyandroidstatus = false;
            }
            else if (!_heavyandroidstatus)
            {

                ReplaceData(_heavyandroidfile, 8090, new byte[] { 0x61, 0x6E, 0x64, 0x72, 0x6F, 0x69, 0x64, 0x5F, 0x62, 0x65, 0x68, 0x61, 0x76, 0x65 });
                btnHeavyAndroid.Text = "Disable Hazmat Androids";
                _heavyandroidstatus = true;
            }

            MessageBox.Show(restartmessage);
        }
        private void btnAndroid_Click(object sender, EventArgs e)
        {
            if (_androidstatus)
            {

                ReplaceData(_androidfile, 9273, new byte[] { 0x4E, 0x6F, 0x42, 0x65, 0x68, 0x61, 0x76, 0x69, 0x6F, 0x75, 0x72, 0x00, 0x00, 0x00, });
                btnAndroid.Text = "Enable Normal Androids";
                _androidstatus = false;
            }
            else if (!_androidstatus)
            {
  
                ReplaceData(_androidfile, 9273, new byte[] { 0x61, 0x6E, 0x64, 0x72, 0x6F, 0x69, 0x64, 0x5F, 0x62, 0x65, 0x68, 0x61, 0x76, 0x65 });
                btnAndroid.Text = "Disable Normal Androids";
                _androidstatus = true;
            }

            MessageBox.Show(restartmessage);
        }

        private void btnAlien_Click(object sender, EventArgs e)
        {
            if (_alienstatus)
            {
                ReplaceData(_alienfile, 8584, new byte[] { 0x4E, 0x6F, 0x42, 0x65, 0x68, 0x61, 0x76, 0x69, 0x6F, 0x75, 0x72, 0x00 });
                btnAlien.Text = "Enable Alien";
                _alienstatus = false;
            }
            else if (!_alienstatus)
            {
                ReplaceData(_alienfile, 8584, new byte[] { 0x61, 0x6C, 0x69, 0x65, 0x6E, 0x5F, 0x62, 0x65, 0x68, 0x61, 0x76, 0x65 });
                btnAlien.Text = "Disable Alien";
                _alienstatus = true;
            }

            MessageBox.Show(restartmessage);
 
        }
        public static void ReplaceData(string filename, int position, byte[] data)
        {
            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                stream.Position = position;
                stream.Write(data, 0, data.Length);
            }
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
                    WriteMem(process, new byte[] { 0x3, 0x0,0x0,0x0,0x1 }, GetPointerAddress(new int[] { 0x44 }, 0x12F0C88, (IntPtr)0x39C));
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
                    WriteMem(process, new byte[] { 0x3, 0x0, 0x0, 0x0, 0x1 }, GetPointerAddress(new int[] { 0x44 }, 0x12F0C88, (IntPtr)0x394));
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

                    WriteMem(process, tempbuffer, GetPointerAddress(new int[] { 0x88 }, 0x12F0C88, (IntPtr)0x24));

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
                    WriteMem(process, new byte[] { 0x1 }, GetPointerAddress(new int[] { 0x44 }, 0x12F0C88, (IntPtr)0x390));
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
            if (File.Exists(pathforgamefolder + @"\DATA\UI\MOVIES\AMD_IDENT.USM"))
            {
                File.Move(pathforgamefolder + @"\DATA\UI\MOVIES\AMD_IDENT.USM", pathforgamefolder + @"\DATA\UI\MOVIES\AMD_IDENT.USM.bak");
            }
            else
            {
                File.Move(pathforgamefolder + @"\DATA\UI\MOVIES\AMD_IDENT.USM.bak", pathforgamefolder + @"\DATA\UI\MOVIES\AMD_IDENT.USM");
            }

            if (File.Exists(pathforgamefolder + @"\DATA\UI\MOVIES\CA_IDENT.USM"))
            {
                File.Move(pathforgamefolder + @"\DATA\UI\MOVIES\CA_IDENT.USM", pathforgamefolder + @"\DATA\UI\MOVIES\CA_IDENT.USM.bak");
            }
            else
            {
                File.Move(pathforgamefolder + @"\DATA\UI\MOVIES\CA_IDENT.USM.bak", pathforgamefolder + @"\DATA\UI\MOVIES\CA_IDENT.USM");
            }

            if (File.Exists(pathforgamefolder + @"\DATA\UI\MOVIES\FOX_IDENT.USM"))
            {
                File.Move(pathforgamefolder + @"\DATA\UI\MOVIES\FOX_IDENT.USM", pathforgamefolder + @"\DATA\UI\MOVIES\FOX_IDENT.USM.bak");
            }
            else
            {
                File.Move(pathforgamefolder + @"\DATA\UI\MOVIES\FOX_IDENT.USM.bak", pathforgamefolder + @"\DATA\UI\MOVIES\FOX_IDENT.USM");
            }

            if (btnDisableIntroMovies.Text == "Disable Intro movies")
            {

                btnDisableIntroMovies.Text = "Enable Intro movies";
            }
            else
            {
                btnDisableIntroMovies.Text = "Disable Intro movies";
            }

        }
    }

}
