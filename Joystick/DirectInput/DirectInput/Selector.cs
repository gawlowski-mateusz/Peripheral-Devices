using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//using Microsoft.DirectX.DirectInput;
using SharpDX.DirectInput;

namespace DirectInputNamespace {
    public partial class Selector : Form {

        SharpDX.DirectInput.DirectInput directInput = new SharpDX.DirectInput.DirectInput();
        List<DeviceInstance> deviceList = new List<DeviceInstance>();  // tworzymy liste znalezionych urzadzen
        public Selector() {
            InitializeComponent();

            var devices = directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AttachedOnly);  // 'zlap' wszystkie urzadzenia typu joystick
            var devices2 = directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly);  // 'zlap' wszystkie urzadzenia typu gamepad

            foreach(DeviceInstance instance in devices) { // dodaj kazdy joystick do listy znalezionych urzadzen
                deviceList.Add(instance);
                listBox1.Items.Add(instance.InstanceName);
            }

            foreach(DeviceInstance instance in devices2) { // dodaj kazdy gamepad do listy znalezionych urzadzen
                deviceList.Add(instance);
                listBox1.Items.Add(instance.InstanceName);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {  // wlacz przyciski tylko wtedy kiedy wybierzemy urzadzenie z listy
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e) {  // wybralismy emulator myszki
            this.Hide();
            int wybranyKontroler = listBox1.SelectedIndex;  // zaznaczone urzadzenie staje sie naszym kontrolerem
            Joystick joystick = new Joystick(directInput, deviceList.ElementAt(wybranyKontroler).InstanceGuid);
            joystick.Acquire();

            EmulatorMyszy emulatorMyszy = new EmulatorMyszy(joystick);  //dzieki temu wlaczamy mozliwosc emulacji myszy
            new Thread(new ThreadStart(emulatorMyszy.WlaczEmulacje)).Start();
            MouseEmulation me = new MouseEmulation();  // dzieki temu otworzy sie okienko mowiace o trwajacej emulacji myszki
            me.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) {  // wybralismy edytor graficzny -> rysowanie
            this.Hide();
            int wybranyKontroler = listBox1.SelectedIndex; // zaznaczone urzadzenie staje sie naszym kontrolerem
            Joystick device = new Joystick(directInput, deviceList.ElementAt(wybranyKontroler).InstanceGuid);
            device.Acquire();
            Canvas canvas = Canvas.CreateCanvas(true);  // dzieki temu otworzy sie okno graficzne w ktorym mozna rysowac
            Paint paint = new Paint(canvas, device); // dzieki temu wlaczamy mozliwosc rysowania za pomoca kontrolera
            paint.InputThread();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e) {  // wybralismy testowanie dzialania kontrolera
            this.Hide();
            int wybranyKontroler = listBox1.SelectedIndex; // zaznaczone urzadzenie staje sie naszym kontrolerem
            Joystick joystick = new Joystick(directInput, deviceList.ElementAt(wybranyKontroler).InstanceGuid);
            joystick.Acquire();
            Canvas canvas = Canvas.CreateCanvas(false);  // dzieki temu otworzy sie okno gdzie gdzie będziemy widzieli zmiany wartości osi X, Y, Z oraz przycisku fire
            canvas.dodajJoystick(joystick);
            TestKontrolera testKontrolera = new TestKontrolera(canvas, joystick);  // dzieki temu wlaczamy mozliwosc testowania kontrolera
            testKontrolera.InputThread();
            this.Close();
        }

        private void Selector_Load(object sender, EventArgs e) {

        }
    }
}
