using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video.DirectShow;
using AForge.Video;
using AForge.Imaging.Filters;
using Accord.Video.VFW;
using Accord.Video.FFMPEG;

namespace cameraxd
{
    public partial class Form1 : Form
    {
        FilterInfoCollection videoDevicesList;
        VideoCaptureDevice cameraInput;
        int brightess1 = 0;
        int contrast1 = 0;
        float saturation1 = 0;
        bool isRecording1 = false;
        VideoFileWriter writer;

        private Bitmap oldBitmap;
        bool wykrywajruch = false;
        float pixeldiff = 0.1f;
        int pixelcount = 100;
        int framedelay = 10;
        int framecount = 0;

        public static bool CompareBitmapsFast(Bitmap bmp1, Bitmap bmp2)
        {
            if (bmp1 == null || bmp2 == null)
                return false;
            if (object.Equals(bmp1, bmp2))
                return true;
            if (!bmp1.Size.Equals(bmp2.Size) || !bmp1.PixelFormat.Equals(bmp2.PixelFormat))
                return false;

            int bytes = bmp1.Width * bmp1.Height * (Image.GetPixelFormatSize(bmp1.PixelFormat) / 8);

            bool result = true;
            byte[] b1bytes = new byte[bytes];
            byte[] b2bytes = new byte[bytes];

            BitmapData bitmapData1 = bmp1.LockBits(new Rectangle(0, 0, bmp1.Width, bmp1.Height), ImageLockMode.ReadOnly, bmp1.PixelFormat);
            BitmapData bitmapData2 = bmp2.LockBits(new Rectangle(0, 0, bmp2.Width, bmp2.Height), ImageLockMode.ReadOnly, bmp2.PixelFormat);

            Marshal.Copy(bitmapData1.Scan0, b1bytes, 0, bytes);
            Marshal.Copy(bitmapData2.Scan0, b2bytes, 0, bytes);

            for (int n = 0; n <= bytes - 1; n++)
            {
                if (b1bytes[n] != b2bytes[n])
                {
                    result = false;
                    break;
                }
            }

            bmp1.UnlockBits(bitmapData1);
            bmp2.UnlockBits(bitmapData2);

            return result;
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap1 = (Bitmap)eventArgs.Frame.Clone();
            BrightnessCorrection br = new BrightnessCorrection(brightess1);
            ContrastCorrection cr = new ContrastCorrection(contrast1);
            SaturationCorrection sr = new SaturationCorrection(saturation1);
            bitmap1 = br.Apply((Bitmap)bitmap1.Clone());
            bitmap1 = cr.Apply((Bitmap)bitmap1.Clone());
            bitmap1 = sr.Apply((Bitmap)bitmap1.Clone());

            if (isRecording1)
            {
                writer.WriteVideoFrame(bitmap1);
            }
            else
            {
                if (oldBitmap != null)
                {
                    if (wykrywajruch == true)
                    {
                        framecount++;
                        if (framecount > framedelay)
                        {
                            framecount = 0;
                            if (CompareBitmapsFast(bitmap1, oldBitmap))
                            {
                                // wykrytor.Visible = false;
                            }
                            else
                            {
                                // wykrytor;
                                // Form1.label9.Text = "Wykryto ruch";
                                // MessageBox.Show("Wykryto ruch");
                                // label9.Visible = true;
                            }
                        }
                    }
                }
                oldBitmap = bitmap1;
                camoutput.Image = bitmap1;
            }
        }
        public Form1()
        {
            InitializeComponent();
            startcamera.Enabled = false;
            stopcamera.Enabled = false;
            takephoto.Enabled = false;
            startfilm.Enabled = false;
            stopfilm.Enabled = false;
            detectstart.Enabled = false;
            detectstop.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            stopcamera_Click(sender, e);
            Bitmap picture = (Bitmap)camoutput.Image;
            saveFileDialog1.Filter = "Bitmap Image|*.bmp";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();
            System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
            picture.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
            fs.Close();
            startcamera_Click(sender, e);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            startcamera.Enabled = true;
            camlist.Items.Clear();
            camlist.ResetText();
            videoDevicesList = new FilterInfoCollection(FilterCategory.VideoInputDevice);//
            foreach (FilterInfo videoDevice in videoDevicesList)
            {
                camlist.Items.Add(videoDevice.Name + videoDevice.MonikerString.Substring(62));
            }
        }

        private void startcamera_Click(object sender, EventArgs e)
        {
            startcamera.Enabled = false;
            stopcamera.Enabled = true;
            takephoto.Enabled = true;
            startfilm.Enabled = true;
            detectstart.Enabled = true;
            if (camlist.SelectedIndex >= camlist.Items.Count) return;//dodać okienko powiadomienia, że nie wybrano kamery
            if (camlist.SelectedIndex < 0) return;

            cameraInput = new VideoCaptureDevice(videoDevicesList[camlist.SelectedIndex].MonikerString);
            cameraInput.NewFrame += new NewFrameEventHandler(video_NewFrame);
            cameraInput.Start();
        }

        private void stopcamera_Click(object sender, EventArgs e)
        {
            startcamera.Enabled = true;
            stopcamera.Enabled = false;
            takephoto.Enabled = false;
            startfilm.Enabled = false;
            stopfilm.Enabled = false;
            detectstart.Enabled = false;
            detectstop.Enabled = false;
            if(cameraInput!=null)
                cameraInput.Stop();
        }

        private void startfilm_Click(object sender, EventArgs e)
        {
            startcamera.Enabled = false;
            stopcamera.Enabled = false;
            startfilm.Enabled = false;
            stopfilm.Enabled = true;
            detectstart.Enabled = false;
            detectstop.Enabled = false;
            if (cameraInput != null)
                if (cameraInput.IsRunning)
            {
                try
                {
                    saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Avi Files (*.avi)|*.avi";
                    saveFileDialog1.Title = "Save a Video File";
                    saveFileDialog1.ShowDialog();
                    writer = new VideoFileWriter();
                    writer.Open(saveFileDialog1.FileName, camoutput.Image.Width, camoutput.Image.Height, 30, VideoCodec.MPEG4);
                    isRecording1 = true;
                }
                catch
                {

                }
            }
        }

        private void brightness_Scroll(object sender, EventArgs e)
        {
                brightess1 = brightness.Value;
        }

        private void contrast_Scroll(object sender, EventArgs e)
        {
                contrast1 = contrast.Value;
        }

        private void saturation_Scroll(object sender, EventArgs e)
        {
                saturation1 = saturation.Value/100.0f;
        }

        private void stopfilm_Click(object sender, EventArgs e)
        {
            startcamera.Enabled = false;
            stopcamera.Enabled = true;
            startfilm.Enabled = true;
            stopfilm.Enabled = false;
            detectstart.Enabled = true;
            detectstop.Enabled = false;
            if (cameraInput != null)
            if (cameraInput.IsRunning && isRecording1)
            {
                isRecording1 = false;
                writer.Close();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pixdiff_Scroll(object sender, EventArgs e)
        {
            pixeldiff = pixdiff.Value;
        }

        private void pixcount_Scroll(object sender, EventArgs e)
        {
            pixelcount = pixcount.Value;
        }

        private void delayframe_Scroll(object sender, EventArgs e)
        {
            framedelay = delayframe.Value;
        }

        private void detectstart_Click(object sender, EventArgs e)
        {
            startcamera.Enabled = false;
            stopcamera.Enabled = false;
            startfilm.Enabled = false;
            stopfilm.Enabled = false;
            detectstart.Enabled = false;
            detectstop.Enabled = true;
            wykrywajruch = true;
        }

        private void detectstop_Click(object sender, EventArgs e)
        {
            startcamera.Enabled = false;
            stopcamera.Enabled = true;
            startfilm.Enabled = true;
            stopfilm.Enabled = false;
            detectstart.Enabled = true;
            detectstop.Enabled = false;
            wykrywajruch = false;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e) {

        }
    }
}
