using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.DirectInput;

namespace DirectInputNamespace {
    public partial class Canvas : Form {
        private System.Threading.Thread myThread;
        private Joystick joystick;
        public bool czyEdytorGraficzny = false;  // zmienna dzieki ktorej wiemy czy chcemy uzywac edytora graficznego czy nie

        float x = 0, y = 0;
        public bool showCross = false;  // domyslnie wskaznik w ekranie graficznyj jest wylaczony

        private Canvas() {
            InitializeComponent();
        }

        public void dodajJoystick(Joystick joystick) {
            this.joystick = joystick;
        }


        public static Canvas CreateCanvas(bool czyEdytorGraficzny) {  // jako argument podajemy zmienna false albo true
            Canvas canvas = null;
            System.Threading.Thread thread = new System.Threading.Thread(() => {  // watek w ktorym dziala caly edytor graficzny
                canvas = new Canvas();
                canvas.czyEdytorGraficzny = czyEdytorGraficzny;  // wartosc bool bedzie utrzymana przez caly watek
                canvas.ShowDialog();
            });
            thread.Start();
            while(canvas == null) {
                System.Threading.Thread.Sleep(10);
            }
            canvas.myThread = thread;

            return canvas;
        }

        public int SzerokoscOkna { get { return pictureBox1.Width; } }  // zwracamy szerokosc naszego okna graficznego
        public int WysokoscOkna { get { return pictureBox1.Height; } }  // zwracamy wysokosc naszego okna graficznego

        private void Timer1_Tick(object sender, EventArgs e) {  // z kazdym kolejnym tickiem watku, sprawdzamy
            // czy zostalo cos zmienione, jezeli tak to odswiezamy, aby zobaczyc zmiany inaczej to omijamy
            if(updated) {
                updated = false;
                Refresh();
            }

            if(!czyEdytorGraficzny) {  // jezeli nie chcemy uzywac edytora graficznego, wtedy
                                       // zamiast niego pojawiaja sie pola tekstowe i etykiety
                                       // uzywane podczas testowania kontrolera
                if(joystick.GetCurrentState().Buttons[0]) { // jezeli wcisniemy przycisk 1, czyli fire
                    textBox2.Text = "Wciśnięty";
                }
                else {
                    textBox2.Text = "Nie wciśnięty";
                }
                // pokazuje aktualny stan osi Z -> slider
                textBox1.Text = String.Format("{0}%", (float)joystick.GetCurrentState().Z / ((1 << 16) - 1) * 100);
                // pokazuje aktualny stan osi Y
                textBox4.Text = String.Format("{0}%", (float)joystick.GetCurrentState().Y / ((1 << 16) - 1) * 100);
                // pokazuje aktualny stan osi X
                textBox3.Text = String.Format("{0}%", (float)joystick.GetCurrentState().X / ((1 << 16) - 1) * 100);
            }
            else {  // jezeli uzywamy edytora graficznego to chowamy wszystkie niepotrzebne pola
                textBox1.Hide();
                textBox2.Hide();
                textBox3.Hide();
                textBox4.Hide();

                label1.Hide();
                label2.Hide();
                label3.Hide();
                label4.Hide();
            }

        }
        private void PictureBox1_Paint(object sender, PaintEventArgs e) { // metoda dzieki ktorej pojawia sie nam nasz pedzel
            lock(bmp) {
                e.Graphics.DrawImage(bmp, 0, 0);
            }
            using(SolidBrush pedzel = new SolidBrush(Color.Black)) { // nasz kursor jest czarny
                e.Graphics.FillEllipse(pedzel, x - 4, y - 4, 2 * 4, 2 * 4);  // jest mala elipsa, a w tym przypadku kolem
            }
        }

        public void WyczyscEkran() {  // metoda dzieki ktorej czyscimy nasz ekran
            bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);  // tworzymy bitmape naszego okna w ktorym bedziemy pracowali
            lock(bmp) {
                using(Graphics g = Graphics.FromImage(bmp))
                using(SolidBrush pedzel = new SolidBrush(Color.White)) {  // szybko ustawiamy kolor pedzla na bialy
                    g.FillRectangle(pedzel, 0, 0, SzerokoscOkna, WysokoscOkna);  // wypelniamy cale okno bialym kolorem
                }
                updated = true;  // ustawiamy flage aby edytor zauwazyl zmiany i odswiezyl sie
            }
        }
        public void Rysuj(float x, float y, float r) {  // metoda dzieki ktorej mozemy rysowac
            lock(bmp) {
                using(Graphics g = Graphics.FromImage(bmp))
                using(SolidBrush pedzel = new SolidBrush(Color.Green)) {  // pedzel ustawiamy na kolor zielony, aby rysowac na zielono
                    g.FillEllipse(pedzel, x - r, y - r, 2 * r, 2 * r);  //  tym razem nasze kolo/elipsa zalezy od wartosci r
                }
                updated = true;  // dzieki temu edytor sie odswiezy
            }
        }

        public void poruszWskaznikiem(float x, float y) {  // metoda dzieki ktorej mozemy poruszac wskaznikiem
            this.x = x;
            this.y = y;
            updated = true;  // zmiana flagi aby edytor sie odswiezyl
        }

        private void Canvas_Load(object sender, EventArgs e) {
            bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);  // tworzymy bitmape naszego okna w ktorym bedziemy pracowali
        }
    }
}
