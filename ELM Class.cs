using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.OleDb;

namespace ELM_Anggi
{
    public partial class Form1 : Form
    {
    
        public Form1()
        {
            InitializeComponent();
        }
        
        public void baca_excel()  //membca data  dari excel kemudian di masukkan ke datagrid
        {
            try
            {
                /*OLE DB (Object Linking and Embedding Database) merupakan teknologi database dari Microsoft yang dapat menghubungkan (link) 
                dan melekatkan (embed) data atau bisa juga disebut dengan database temporary (sementara)*/
                /*OLE DB mempunyai kemampuan dalam mengolah database lebih cepat. OLE DB lebih mudah dalam pemakaiannya untuk mengakses suatu data.*/
                OleDbConnection MyConnection; //untuk menghubungkan dengan database
                DataSet DtSet; //untuk mengelola Table 
                OleDbDataAdapter MyCommand; // untuk mempermudah berbagai macam operasi database, misalnya membaca data dari database ke aplikasi
                string namafile = textBox1.Text; //mengambil namaFile dari Texxtbox1
                string SheetName = textBox2.Text; //mengambil nama sheet dari text box2
                MyConnection = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + namafile + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\""); /*Anda bisa menggunakan string koneksi ini untuk menggunakan driver Office untuk menyambungkan ke Excel 97-2003 Excel yang lebih lama.*/
                MyCommand = new OleDbDataAdapter("select * from [" + SheetName + "$]", MyConnection); /*setelah membuat string koneksi, maka melakukan select data di sheet name dgn menggunakan adapter*/
                MyCommand.TableMappings.Add("Table", "TestTable"); //menambahkan tabel data dari yang di select MyCommand sebelumnya
                DtSet = new DataSet(); //inisiasi nama DataSet
                MyCommand.Fill(DtSet); //menambahkan data dari yang di select ke DtSet
                dataGridView1.DataSource = DtSet.Tables["TestTable"]; //Menampilkan di dataGrid1
                setColor(); //membagi warna data Training dan Testing
                MyConnection.Close(); //menutup koneksi database yg telah dibuat pertama
            }
            catch (Exception)
            {
                MessageBox.Show("Data Sheet Tidak Ada"); //jendela jika data sheet tidak ditemukan
            }
        }


        public void setColor() //mengatur warna data training dan testing
        {          
            if(textBox3.Text == "") //jika textbox3 belum di inputkan ma warna datagrid di set putih
            {
                dataGridView1.BackgroundColor = Color.White;
            }
            else
            {
                double training = System.Math.Round(Convert.ToDouble(label4.Text)-1, 0); //inisialisasi jumlah data training dengan pembulatan dari label4, di kurangi 1 karena datagrid yg di baca mulai dari array 0 jika data training 7 maka akan membaca 0-7                 
                for (int i = 0; i < dataGridView1.Rows.Count; i++) //perulangan datagrid1
                {
                    if (i <= training)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    }
                }
               

            }
            
        }

        public void baca_excelMin() //membca data minimal dari excel kemudian di masukkan ke datagrid
        {
            try
            {
                OleDbConnection MyConnection;
                DataSet DtSet;
                OleDbDataAdapter MyCommand;
                string namafile = textBox1.Text;
                string SheetName = textBox2.Text;
                MyConnection = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + namafile + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"");
                MyCommand = new OleDbDataAdapter("select MIN(Tahun) as Tahun, MIN(LuasLahan) as LuasLahan, MIN(LuasPanen) as LuasPanen, MIN(Bibit) as Bibit, MIN(Produksi) as Produksi from [" + SheetName + "$]", MyConnection);
                MyCommand.TableMappings.Add("Table", "TestTable");
                DtSet = new DataSet();
                MyCommand.Fill(DtSet);
                dataGridView5.DataSource = DtSet.Tables["TestTable"];
                MyConnection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Data Sheet Tidak Ada");
            }
        }

        public void baca_excelMax() //membca data maksimal dari excel kemudian di masukkan ke datagrid
        {
            try
            {
                OleDbConnection MyConnection;
                DataSet DtSet;
                OleDbDataAdapter MyCommand;
                string namafile = textBox1.Text;
                string SheetName = textBox2.Text;
                MyConnection = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + namafile + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"");
                MyCommand = new OleDbDataAdapter("select MAX(Tahun) as Tahun, MAX(LuasLahan) as LuasLahan, MAX(LuasPanen) as LuasPanen, MAX(Bibit) as Bibit, MAX(Produksi) as Produksi from [" + SheetName + "$]", MyConnection);
                MyCommand.TableMappings.Add("Table", "TestTable");
                DtSet = new DataSet();
                MyCommand.Fill(DtSet);
                dataGridView6.DataSource = DtSet.Tables["TestTable"];
                MyConnection.Close();
            }
            catch (Exception )
            {
                MessageBox.Show("Data Sheet Tidak Ada");
            }
        }

        private void button1_Click(object sender, EventArgs e) //fungsi button import data
        {
            OpenFileDialog bukaFile = new OpenFileDialog(); //membuka jendela library
            bukaFile.Filter = "Text File All Files (*.*)|*.*|(*.txt)|*.txt "; //filter file yang di tampilkan
            bukaFile.RestoreDirectory = true; //menyimpan history letak direktory yg terakhir dibuka
            if (DialogResult.OK == bukaFile.ShowDialog())
            {
                if(textBox2.Text == "") //jika nama sheet belum di isi maka akan muncul message box
                {
                    MessageBox.Show("Silahkan Isi Nama Sheet");
                }
                else
                {
                    textBox1.Text = bukaFile.FileName; //membuka file yang di pilih kemudian di olah dengan method BacaExcel, BacaExcelMin, BacaExcelMax
                    baca_excel();
                    baca_excelMin();
                    baca_excelMax();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void clear_datagrid() //menghapus semua isi datagrid ketika button di klik ulang, shg tdk ada penumpukan data 
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            dataGridView3.Rows.Clear();
            dataGridView3.Refresh();
            dataGridView4.Rows.Clear();
            dataGridView4.Refresh();
            dataGridView7.Rows.Clear();
            dataGridView7.Refresh();
            dataGridView8.Rows.Clear();
            dataGridView8.Refresh();
            dataGridView9.Rows.Clear();
            dataGridView9.Refresh();
            dataGridView10.Rows.Clear();
            dataGridView10.Refresh();
        }

        public void fungsi_utama() //fungsi utama ketika button proses ELM di tekan
        {
            //inisialisasi
            int hidden_neuron = Convert.ToInt16(textBox3.Text); //inisialisasi jumlah HN dari textBox
            int input_layer = 4; //Terdiri dari Tahun, Luas Lahan (HA), Luas Panen (HA), Bibit (Kg)
            int output_layer = 1; //Terdiri dari Produksi Padi

            //Inisialisasi matriks utama
            double[,] bobot_input = new double[hidden_neuron, input_layer]; //membentuk array matriks bobot inputan
            double[,] bias = new double[1, hidden_neuron]; //membentuk array matriks bias

            //Normalisasi Data
            double[,] data_normalisasi = new double[dataGridView1.Rows.Count, dataGridView1.Columns.Count];//membuat array matriks normalisasi
            double[,] data_min = new double[dataGridView5.Rows.Count, dataGridView5.Columns.Count]; //membuat array matriks data minimal
            double[,] data_max = new double[dataGridView6.Rows.Count, dataGridView6.Columns.Count]; //membuat array matriks data minimal
            data_normalisasi = normalisasi(data_normalisasi, data_min, data_max); //panggil metod normalisasi dengan variabel data yang dikirm data_normalisasi, data_min, dan data_max
            double[,] beta = new double[hidden_neuron, output_layer]; //membuat array matriks beta
            double[,] MSE = new double[1,1]; //membuat array matriks mse 
  
            
            //Mempersiapkan perbandingan jumlah data latih dan data uji
            double rasio = get_rasio(); //panggil method get_rasio
            double jumlah_training = rasio / 100 * data_normalisasi.GetLength(0); //perhitungan jml data training. misal 12 data dgn rasio 70:30 maka, 70/100*12=8,4 yang nanti akan di round atau di bulatkan shg menjadi 8
            double jumlah_testing = (100 - rasio) / 100 * data_normalisasi.GetLength(0); //perhitungan jml data testing. misal 12 data dgn rasio 70:30 maka, (100 - 70) / 100 * 12 = 3,6 yang nanti akan di round atau di bulatkan shg menjadi 4

            double jumlahbarisdata = dataGridView1.Rows.Count; //menghitung baris data pada datagrid
            double jumtraining = Convert.ToInt16(System.Math.Round(jumlah_training, 0)); //pembulatan data training
            double jumtesting = Convert.ToInt16(System.Math.Round(jumlah_testing, 0)); //pembulatan data testing
            
            //proses pembulatan data ketika data ganjil dibagi menjadi rasio2
            if (jumtraining+jumtesting != jumlahbarisdata) //jika penjumlahan data training dan teating != dengan jumlah baris data maka :
            {                
                if(jumtraining+jumtesting > jumlahbarisdata) //jika penjumlahan data training dan teating lebih dari baris data maka berikut
                {
                    double sisa1 = Math.Abs((jumtraining+jumtesting)- jumlahbarisdata); //menghitung sisa dari pengurangan (jumlahtraining +jumlahtesting) dgn jmlh baris data
                    label4.Text = Convert.ToString(System.Math.Round(jumlah_training, 0)); //menampilkan jumlah data training yg telah di round di label 4
                    label5.Text = Convert.ToString(System.Math.Round(jumlah_testing, 0)-sisa1); //jumlah testing dikurangi sisa

                }else if (jumtraining+jumtesting < jumlahbarisdata) //jika penjumlahan data training dan teating kurang dari baris data maka berikut
                {
                    double sisa2= Math.Abs(jumlahbarisdata - (jumtraining + jumtesting)); //menghitung sisa dari pengurangan jumlahbarisdata - (jumlahtraining +jumlahtesting)
                    label4.Text = Convert.ToString(System.Math.Round(jumlah_training, 0)+ sisa2); //menampilkan jumlah data training yg telah di round di label 5
                    label5.Text = Convert.ToString(System.Math.Round(jumlah_testing, 0)); //jumlah testing dikurangi sisa2
                }
                else
                {
                    label4.Text = Convert.ToString(System.Math.Round(jumlah_training, 0)); //menampilkan jumlah data training yg telah di round di label 4
                    label5.Text = Convert.ToString(System.Math.Round(jumlah_testing, 0));
                }                 
            }else 
            {
                label4.Text = Convert.ToString(System.Math.Round(jumlah_training, 0)); //menampilkan jumlah data training yg telah di round di label 4
                label5.Text = Convert.ToString(System.Math.Round(jumlah_testing, 0)); //menampilkan jumlah data testing yg telah di round di label 5
            }

            labelJumlahDataSemua.Text = Convert.ToString(System.Math.Round(jumlah_training+jumlah_testing, 0)); //menghitung jumlah data training dan testing kemudian di tampilkan pada label

            /*label4.Text = Convert.ToString(System.Math.Round(jumlah_training, 0));
            label5.Text = Convert.ToString(System.Math.Round(jumlah_testing, 0));
            double[,] data_latih = new double[Convert.ToInt16(System.Math.Round(jumlah_training, 0)), data_normalisasi.GetLength(1)];
            double[,] data_uji = new double[Convert.ToInt16(System.Math.Round(jumlah_testing, 0)), data_normalisasi.GetLength(1)];*/

            double[,] data_latih = new double[Convert.ToInt16(label4.Text), data_normalisasi.GetLength(1)]; //inisialisasi array data latih/training
            double[,] data_uji = new double[Convert.ToInt16(label5.Text), data_normalisasi.GetLength(1)]; //inisialisasi array data uji/testing

            data_latih = get_latih(data_normalisasi, data_latih); //mengambil data latih/training dgn method data_latih
            data_uji = get_uji(data_normalisasi, data_uji); //mengambil data uji/testing dgn method data_uji

            //SetColorDataTrainingdanTesting
            setColor();

            //KASIH WAKTU DISINI
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //Generate bobot input dan bias
            bobot_input = generate_bobot(hidden_neuron, input_layer); //panggil method generate bobot dgn variabel yg dikirim HN dan Input Layer
            bias = generate_bias(hidden_neuron); //panggil method generate bias dgn variabel yg dikirim HN

            //FASE TRAINING
            beta = Training(hidden_neuron, input_layer, output_layer, data_normalisasi, data_latih, bobot_input, bias);

            //FASE TESTING
            MSE = Testing(hidden_neuron, input_layer, output_layer, MSE, data_normalisasi, data_uji, beta, bobot_input, bias);

            //Tutup Waktu
            watch.Stop();
            label10.Text = watch.Elapsed.TotalSeconds.ToString() + " DETIK";
           // textBox8.Text = watch.Elapsed.TotalSeconds.ToString();
           
        }

        private void button2_Click(object sender, EventArgs e) //fungsi button proses
        {
            
            if(textBox3.Text == "" && comboBox1.Text == "" || textBox3.Text == "0" || comboBox1.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Silahkan Cek Data, Hidden Neuron dan Rasio Data!");
            }
            else
            {
                clear_datagrid(); //mengahpus isi datagrid
                fungsi_utama(); //Panggil Method Fungsi Inti
                panel1.Controls.Remove(axShockwaveFlash1); //mengahpus isi animasi pada panel
                panel1.Controls.Remove(axShockwaveFlash2);
                //fungsi memanggil animasi
                if (textBox3.Text != "5") //jika HN yg di inputkan 5 maka akan menampilkan animasi dgn HN 5
                {
                    axShockwaveFlash2 = new AxShockwaveFlashObjects.AxShockwaveFlash();
                    axShockwaveFlash2.Dock = DockStyle.Fill;
                    panel1.Controls.Add(axShockwaveFlash2);
                    axShockwaveFlash2.Movie = @"E:\POLITEKNIK NEGERI MALANG\SKRIPSI\Project Skripsi\ELM-Anggi\AnimasiVisualiasasiELM.swf";
                    axShockwaveFlash2.Play();
                }
                else //jika tidak makan HN yg di tampilkan -
                {
                    axShockwaveFlash1 = new AxShockwaveFlashObjects.AxShockwaveFlash();
                    axShockwaveFlash1.Dock = DockStyle.Fill;
                    panel1.Controls.Add(axShockwaveFlash1);
                    axShockwaveFlash1.Movie = @"E:\POLITEKNIK NEGERI MALANG\SKRIPSI\Project Skripsi\ELM-Anggi\AnimasiVisualiasasiELM5HN.swf";
                    axShockwaveFlash1.Play();
                }
                
            }
        }

        public double[,] readDataGrid() //konfersi dari datagrid ke array 
        {
            int banyak_baris = dataGridView1.Rows.Count;
            double[,] data_mentah = new double[banyak_baris, dataGridView1.Columns.Count];

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    if (dataGridView1.Rows[row.Index].Cells[col.Index].Value != null)
                    {
                        string value = dataGridView1.Rows[row.Index].Cells[col.Index].Value.ToString();
                        data_mentah[row.Index, col.Index] = Convert.ToDouble(value);
                    }
                }
            }
            return data_mentah;
        }

        public double[,] readDataGridMin() //konfersi dari datagrid ke array 
        {
            int banyak_baris = dataGridView5.Rows.Count;
            double[,] data_mentah = new double[banyak_baris, dataGridView5.Columns.Count];

            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                foreach (DataGridViewColumn col in dataGridView5.Columns)
                {
                    if (dataGridView5.Rows[row.Index].Cells[col.Index].Value != null)
                    {
                        string value = dataGridView5.Rows[row.Index].Cells[col.Index].Value.ToString();
                        data_mentah[row.Index, col.Index] = Convert.ToDouble(value);
                    }
                }
            }
            return data_mentah;
        }

        public double[,] readDataGridMax() //konfersi dari datagrid ke array 
        {
            int banyak_baris = dataGridView6.Rows.Count;
            double[,] data_mentah = new double[banyak_baris, dataGridView6.Columns.Count];

            foreach (DataGridViewRow row in dataGridView6.Rows)
            {
                foreach (DataGridViewColumn col in dataGridView6.Columns)
                {
                    if (dataGridView6.Rows[row.Index].Cells[col.Index].Value != null)
                    {
                        string value = dataGridView6.Rows[row.Index].Cells[col.Index].Value.ToString();
                        data_mentah[row.Index, col.Index] = Convert.ToDouble(value);
                    }
                }
            }
            return data_mentah;
        }

        public double[,] normalisasi(double[,] data_normalisasi, double[,] data_min, double[,] data_max) //normalisasi data
        {
            data_normalisasi = readDataGrid();
            data_min = readDataGridMin();
            data_max = readDataGridMax();
            double BA = 0.9;
            double BB = 0.1;

            double minT = data_min[0, 0];
            double minLL = data_min[0, 1];
            double minLP = data_min[0, 2];
            double minB = data_min[0, 3];
            double minPro = data_min[0, 4];

            double maxT = data_max[0, 0];
            double maxLL = data_max[0, 1];
            double maxLP = data_max[0, 2];
            double maxB = data_max[0, 3];
            double maxPro = data_max[0, 4];

            int T = 0; int LL = 1; int LP = 2; int B = 3; int Pro = 4;

            for (int z = 0; z < data_normalisasi.GetLength(0); z++) //perulangan tahun
            {
                data_normalisasi[z, T] = (data_normalisasi[z, T] - minT) * (BA - BB) / (maxT - minT) + BB;
            }

            for (int z = 0; z < data_normalisasi.GetLength(0); z++) //perulangan Luas Lahan
            {
               data_normalisasi[z, LL] = (data_normalisasi[z, LL] - minLL) * (BA-BB) / (maxLL - minLL) + BB;
            }

            for (int z = 0; z < data_normalisasi.GetLength(0); z++) //Perulangan Luas Panen
            {
                data_normalisasi[z, LP] = (BA - BB) * ((data_normalisasi[z, LP] - minLP) / (maxLP - minLP)) + BB;
            }

            for (int z = 0; z < data_normalisasi.GetLength(0); z++) //Perulangan Bibit
            {
                data_normalisasi[z, B] = (BA - BB) * ((data_normalisasi[z, B] - minB) / (maxB - minB)) + BB;
            }

            for (int z = 0; z < data_normalisasi.GetLength(0); z++) //Perulangan Produksi
            {
                data_normalisasi[z, Pro] = (BA - BB) * ((data_normalisasi[z, Pro] - minPro) / (maxPro - minPro)) + BB;
            }

            //ngeprint data_normalisasi di table view
            for (int r = 0; r < data_normalisasi.GetLength(0); r++)
            {
                string[] baris = new string[data_normalisasi.GetLength(1)]; //baris disini membaca mulai row ke 1
                for (int c = 0; c < data_normalisasi.GetLength(1); c++)
                {
                    baris[c] = Convert.ToString(data_normalisasi[r, c]);
                }

                dataGridView4.Rows.Add(baris);
            }
            return data_normalisasi;           
        }

        public double[,] generate_bobot(int hidden_neuron, int input_layer) //random bias dengan random double in range c#
        {
            
            double[,] bobot_input = new double[hidden_neuron, input_layer];
            Random random = new Random();
            double BtsAts = 1;
            double BtsBwh = -1;
            double input;
            for (int i = 0; i < bobot_input.GetLength(0); i++)
            {
                for (int j = 0; j < bobot_input.GetLength(1); j++)
                {
                    input = random.NextDouble() * (BtsAts - BtsBwh) + BtsBwh;
                    bobot_input[i, j] = System.Math.Round(input, 2);
                }
            }
            
            /*
            double[,] bobot_input = new double[4, 4] {
                {0.49,-0.56,0.28,0.62},
                {0.12,0.67,-0.94,-0.92},
                {-0.75,0.09,0.12,-0.21},
                {-0.27,0.24,-0.84,0.83}
            };
            */
            
            //ngeprint bobot input di tabel view
           this.dataGridView2.ColumnCount = input_layer;
            for (int r = 0; r < bobot_input.GetLength(0); r++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(this.dataGridView2);
                for (int c = 0; c < bobot_input.GetLength(1); c++)
                {
                    row.Cells[c].Value = bobot_input[r, c];
                }
                this.dataGridView2.Rows.Add(row);
            }

            return bobot_input;
        }

        public double[,] generate_bias(int hidden_neuron) //generate bias dengan random double in range c#
        {       
             double[,] bias = new double[1, hidden_neuron];
             Random rand = new Random();
             double btsats = 1;
             double btsbwh = 0;
             double masukan;
             for (int i = 0; i < hidden_neuron; i++)
             {
                 masukan = rand.NextDouble() * (btsats - btsbwh) + btsbwh;
                 bias[0, i] = Math.Abs(Math.Round(masukan, 2)); //abs dikarenakan tidak boleh negatif
             }
            
             /*
            double[,] bias = new double[1, 4] {
                {0.74,0.22,0.64,0.81}
            }; */
               

            //ngeprint bias di tabel view
            this.dataGridView3.ColumnCount = hidden_neuron;
            for (int r = 0; r < 1; r++)
            {
                string[] baris = new string[hidden_neuron];
                for (int c = 0; c < hidden_neuron; c++)
                {
                    baris[c] = Convert.ToString(bias[r, c]);
                }
                dataGridView3.Rows.Add(baris);
            }

            return bias;
        }

        public double get_rasio() //get rasio dari combo box1
        {
            double rasio = 70;//default
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    rasio = 90;
                    break;

                case 1:
                    rasio = 80;
                    break;

                case 2:
                    rasio = 70;
                    break;

                case 3:
                    rasio = 60;
                    break;

                case 4:
                    rasio = 50;
                    break;

                case 5:
                    rasio = 40;
                    break;

                case 6:
                    rasio = 30;
                    break;

                case 7:
                    rasio = 20;
                    break;

                case 8:
                    rasio = 10;
                    break;

                default://default lagi wkwkwkw
                    rasio = 70; 
                    break;
            }
            return rasio;
        }

        //get data training sesuai rasio
        public double[,] get_latih(double[,] data_normalisasi, double[,] data_latih)
        {
            for (int i = 0; i < data_latih.GetLength(0); i++)
            {
                for (int j = 0; j < data_latih.GetLength(1); j++)
                {
                    data_latih[i, j] = data_normalisasi[i, j];
                }
            }
            //ngeprint data_normalisasi di table view
            /*
            this.dataGridView7.ColumnCount = 5;
            for (int r = 0; r < data_latih.GetLength(0); r++)
            {
                string[] baris = new string[data_latih.GetLength(1)]; //baris disini membaca mulai row ke 1
                for (int c = 0; c < data_latih.GetLength(1); c++)
                {
                    baris[c] = Convert.ToString(data_latih[r, c]);
                }

                dataGridView7.Rows.Add(baris);
            }
            */
            return data_latih;
        }

        //get data testing sesuai rasio
        public double[,] get_uji(double[,] data_normalisasi, double[,] data_uji)
        {
            int mulai_ke = Convert.ToInt16(label4.Text); //ambil dari label data training (dibaca dari array ke 0)
            for (int i = 0; i < data_uji.GetLength(0); i++)
            {
                for (int j = 0; j < data_uji.GetLength(1); j++)
                {
                    data_uji[i, j] = data_normalisasi[mulai_ke, j];
                }
                mulai_ke++;
            }

            //ngeprint data_normalisasi di table view
            /*
            this.dataGridView8.ColumnCount = 5;
            for (int r = 0; r < data_uji.GetLength(0); r++)
            {
                string[] baris = new string[data_uji.GetLength(1)]; //baris disini membaca mulai row ke 1
                for (int c = 0; c < data_uji.GetLength(1); c++)
                {
                    baris[c] = Convert.ToString(data_uji[r, c]);
                }

                dataGridView8.Rows.Add(baris);
            }
            */
            return data_uji;
        }

        //method proses training
        public double[,] Training(int hidden_neuron, int input_layer, int output_layer, double[,] data_normalisasi, double[,] data_latih, double[,] bobot_input, double[,] bias)
        {
            //label4.Text = Convert.ToString(data_latih.GetLength(0)); //mengisi label 4 berisi jumlah data training 
            double[,] X_training = new double[data_latih.GetLength(0), input_layer]; //inisialisasi array X training dgn jumlah baris sesuai jumlah data latih, kolom ada 4 sesuai input layer
            double[,] Y_training = new double[data_latih.GetLength(0), output_layer]; //inisialisasi array Y training 
            double[,] bobot_input_transpose = new double[input_layer, hidden_neuron]; //inisalisasi array bobot input
            double[,] H_init = new double[data_latih.GetLength(0), hidden_neuron]; //inisialisasi array Hinit
            double[,] H = new double[data_latih.GetLength(0), hidden_neuron]; ////inisialisasi array H atau Hexp
            double[,] H_transpose = new double[hidden_neuron, data_latih.GetLength(0)]; //inisialisasi array H traspose
            double[,] HT_H = new double[hidden_neuron, hidden_neuron]; //inisialisasi array hasil matriks invers
            double[,] _HT_H = new double[hidden_neuron, hidden_neuron]; //inisialisasi array dari hasil perkalian H transpose dgn Heksp atau H
            double[,] H_plus = new double[hidden_neuron, data_latih.GetLength(0)]; //inisialisasi array H+ dari hasil perkalian HT_H dengan Htranspose
            double[,] beta = new double[hidden_neuron, output_layer]; //inisialisasi array beta atau hasil training

            data_latih = get_latih(data_normalisasi, data_latih); //Get data latih sesuai rasio
            
            X_training = get_X(data_latih); //Memisah data latih menjadi X
            Y_training = get_Y(data_latih); //Memisah data latih menjadi Y

            //Menghitung H_init
            bobot_input_transpose = matriks_transpose(bobot_input);
            H_init = matriks_perkalian(X_training, bobot_input_transpose);
            H_init = dikali_bias(H_init, bias);
            
            //Menghitung H
            H = hitung_H(H_init, H);
            
            //Menghitung H+
            H_transpose = matriks_transpose(H);
            _HT_H = matriks_perkalian(H_transpose, H);
            HT_H = inverse_matriks(_HT_H);
            H_plus = matriks_perkalian(HT_H, H_transpose);

            //Menghitung beta
            beta = matriks_perkalian(H_plus, Y_training);

            //ngeprint beta di table view
            this.dataGridView7.ColumnCount = beta.GetLength(1);
            for (int r = 0; r < beta.GetLength(0); r++)
            {
                string[] baris1 = new string[beta.GetLength(1)];
                for (int c = 0; c < beta.GetLength(1); c++)
                {
                    baris1[c] = Convert.ToString(beta[r, c]);
                }
                dataGridView7.Rows.Add(baris1);
            }
            
            return beta;
        }

        //method memecah data X training atau X testing
        public double[,] get_X(double[,] matriks)
        {
            int banyak_data = matriks.GetLength(0); //mencari jumlah baris data
            double[,] X_matriks = new double[banyak_data, 4]; //inisialiasi array X matriks dgn baris sesuai banyak data dan kolom 4 sesuai dgn fitur inputan
            for (int i = 0; i < banyak_data; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    X_matriks[i, j] = matriks[i, j]; //pengisian X_matriks
                }
            }

            //ngeprint
            /*
            this.dataGridView7.ColumnCount = 4;
            for (int r = 0; r < matriks.GetLength(0); r++)
            {
                string[] baris = new string[matriks.GetLength(1)]; //baris disini membaca mulai row ke 1
                for (int c = 0; c < matriks.GetLength(1); c++)
                {
                    baris[c] = Convert.ToString(matriks[r, c]);
                }

                dataGridView7.Rows.Add(baris);
            }
            */
            return X_matriks;
        }

        //method memecah data Y training atau Y testing
        public double[,] get_Y(double[,] matriks)
        {
            int banyak_data = matriks.GetLength(0); //dicari jumlah barisnya terlebih dahulu
            double[,] Y_matriks = new double[banyak_data, 1]; //inisialaisasi array Y dgn baris sesuai banyak data dan kolom 1 saja karena target (X) hanya 1 yaitu produksi
            for (int i = 0; i < Y_matriks.GetLength(0); i++)
            {
                int kolom = 4; //sesuai dengan jumlah fitur inputan dan juga inisialisasi mulai mencari Y di kolom ke 4 (dibaca dari arrray 0, yg berarti Y ini ada di kolom ke 5 asli)
                for (int j = 0; j < 1; j++)
                {
                    Y_matriks[i, j] = matriks[i, kolom]; //pengisian Y_matriks
                    kolom++;
                }
            }
            //ngeprint
            /*
            this.dataGridView8.ColumnCount = 1;
            for (int r = 0; r < matriks.GetLength(0); r++)
            {
                string[] baris = new string[matriks.GetLength(1)]; //baris disini membaca mulai row ke 1
                for (int c = 0; c < matriks.GetLength(1); c++)
                {
                    baris[c] = Convert.ToString(matriks[r, c]);
                }

                dataGridView8.Rows.Add(baris);
            }
            */
            return Y_matriks;
        }

        //method transpose matriks
        public double[,] matriks_transpose(double[,] matriks)
        {
            double[,] matriks_transpose = new double[matriks.GetLength(1), matriks.GetLength(0)]; //inisialisasi array

            for (int i = 0; i < matriks_transpose.GetLength(0); i++)
            {
                for (int j = 0; j < matriks_transpose.GetLength(1); j++)
                {
                    matriks_transpose[i, j] = matriks[j, i]; //pengisian kolom menjadi baris, baris menjadi kolom 
                }
            }
            //ngeprint
            /*
            this.dataGridView8.ColumnCount = matriks.GetLength(0);
            for (int r = 0; r < matriks_transpose.GetLength(0); r++)
            {
                string[] baris = new string[matriks_transpose.GetLength(1)]; //baris disini membaca mulai row ke 1
                for (int c = 0; c < matriks_transpose.GetLength(1); c++)
                {
                    baris[c] = Convert.ToString(matriks_transpose[r, c]);
                }

                dataGridView8.Rows.Add(baris);
            }
            */
            return matriks_transpose;
        }

        //method perkalian matriks
        public double[,] matriks_perkalian(double[,] MatriksA, double[,] MatriksB) //method matriks_perkalian dgn mengirim data (X_training, bobot_input_transpose)
        {
            double[,] matriks_hasil = new double[MatriksA.GetLength(0), MatriksB.GetLength(1)]; //inisialisasi array

            for (int i = 0; i < MatriksA.GetLength(0); i++)
            {
                for (int j = 0; j < MatriksB.GetLength(1); j++)
                {
                    matriks_hasil[i, j] = 0;
                    for (int k = 0; k < MatriksA.GetLength(1); k++)
                    {
                        matriks_hasil[i, j] = matriks_hasil[i, j] + MatriksA[i, k] * MatriksB[k, j];
                        matriks_hasil[i, j] = Math.Round(matriks_hasil[i, j], 9);
                    }
                }
            }
            /* PENJELASAN KODING :
            misal terdapat matriks A = 1 2 3
            matriks B = 4
                        5
                        6
             contoh perulangan array ke 0 :
             i=0 j=0 maka mengisi matriks_hasil[0,0]=0
             kemudian looping untuk perkalian array k=0
             maka replace matriks_hasil[0,0] dgn rumus matriks_hasil[i, j] + MatriksA[i, k] * MatriksB[k, j] yg berarti
             matriks_hasil[0,0]= 0+1*4 = 4
             kemudian perulangan lagi k = 1
             matriks_hasil[0,0]= 4+2*5 = 14
             k=2
             matriks_hasil[0,0]= 14+3*6 = 32 ==> maka ini adalah hasil matriks_hasil[0,0] 
             */

            //print
            /*
            this.dataGridView8.ColumnCount = Convert.ToInt16(textBox3.Text);
            for (int r = 0; r < matriks_hasil.GetLength(0); r++)
            {
                string[] baris = new string[matriks_hasil.GetLength(1)]; //baris disini membaca mulai row ke 1
                for (int c = 0; c < matriks_hasil.GetLength(1); c++)
                {
                    baris[c] = Convert.ToString(matriks_hasil[r, c]);
                }

                dataGridView8.Rows.Add(baris);
            }
            */
            return matriks_hasil;
        }

        //method ditambah bias, bukan di kali bias wkkwkw karena lupa dikira di kali hehehe
        public double[,] dikali_bias(double[,] H_init, double[,] bias)
        {
            for (int i = 0; i < H_init.GetLength(0); i++)
            {
                for (int j = 0; j < H_init.GetLength(1); j++)
                {
                    H_init[i, j] = H_init[i, j] + bias[0, j]; //dari matriks hasil Hinit kemudian di tambah dgn bias
                }
            }
            //print
            /*
            this.dataGridView8.ColumnCount = Convert.ToInt16(textBox3.Text);
            for (int r = 0; r < H_init.GetLength(0); r++)
            {
                string[] baris = new string[H_init.GetLength(1)]; //baris disini membaca mulai row ke 1
                for (int c = 0; c < H_init.GetLength(1); c++)
                {
                    baris[c] = Convert.ToString(H_init[r, c]);
                }

                dataGridView8.Rows.Add(baris);
            }
            */
            return H_init;
        }

        //hitung H == Sigmoid Biner, nilai exponensial 2,71828183
        public double[,] hitung_H(double[,] H_init, double[,] H)
        {
            for (int i = 0; i < H_init.GetLength(0); i++)
            {
                for (int j = 0; j < H_init.GetLength(1); j++)
                {
                    H[i, j] = 1 / (1 + Math.Exp(-H_init[i, j]));
                    H[i, j] = Math.Round(H[i, j], 9); //pembulatan
                }
            }
            //print
            /*
            this.dataGridView8.ColumnCount = Convert.ToInt16(textBox3.Text);
            for (int r = 0; r < H.GetLength(0); r++)
            {
                string[] baris = new string[H.GetLength(1)]; //baris disini membaca mulai row ke 1
                for (int c = 0; c < H.GetLength(1); c++)
                {
                    baris[c] = Convert.ToString(H[r, c]);
                }

                dataGridView8.Rows.Add(baris);
            }
            */
            return H;
        }

        //method invers matriks
        public double[,] inverse_matriks(double[,] matriksA) //inverse_matriks(_HT_H); _HT_H = matriks_perkalian(H_transpose, H);
        {
            double[,] matriks = new double[matriksA.GetLength(0), matriksA.GetLength(1)]; //membuat matriks sesuai dengan method matriks_perkalian(H_transpose, H);
            double[,] matriks_invers = new double[matriks.GetLength(0), matriks.GetLength(1)]; //membuat wadah matriks invers atau matriks identitas yg nantinya matriks identitas akan berubah menjadi hasil dari matriks invers

            //proses mengisi matriks dengan nilai yang ada di matriksA
            for (int i = 0; i < matriks.GetLength(0); i++)
            {
                for (int j = 0; j < matriks.GetLength(1); j++)
                {
                    matriks[i, j] = matriksA[i, j];
                }
            }

            //membuat matriks identitas, dimana jika baris dan kolom arraynya sama maka disi 1 [0,0] = 1; [1,1] = 1 dst jika tidak sama [0,1] maka di isi 0
            for (int i = 0; i < matriks.GetLength(0); i++)
            {
                for (int j = 0; j < matriks.GetLength(1); j++)
                {
                    if (i == j)
                    {
                        matriks_invers[i, j] = 1;
                    }
                    else
                    {
                        matriks_invers[i, j] = 0;
                    }
                }
            }

            //proses invers
            for (int i = 0; i < matriks.GetLength(0); i++)
            {
                double t = matriks[i, i]; //mengambil nilai di array yang sama misal pd array [0,0] [1,1] dst
                for (int k = 0; k < matriks.GetLength(1); k++) //proses pada array yang sama [0,0] [1,1] dst yang di bagi dengan matriks t
                {
                    matriks_invers[i, k] = matriks_invers[i, k] / t; //matriks_inverse di isi dengan matriks_invers dibagi dengan t
                    matriks[i, k] = matriks[i, k] / t; //matriks di isi dengan hasil dari matriks / t
                    //matriks_invers[i, k] = Math.Round(matriks_invers[i, k], 9); //pembulatan 9 angka dibelakang koma
                }

                for (int j = 0; j < matriks.GetLength(1); j++)
                {
                    //setalah proses array yg sama [0,0] dst maka proses array yg tdk sama misal [0,1] [0,2] [1,2] dst
                    double c = matriks[j,i];
                    //j =0 maka
                    //pertama variabel c berisi nilai dari matriks yang baris dan kolom yg sama misal [0,0] [1,1] dst. 
                    //kemudian di cek di bagian if (i != j) maka nilai matriks c yang dari array [0,0[1,1] dst itu tidak di pakai
                    //maka dilakukan perulangan for lagi dgn variabel j=1 shg variabel c di isi dengan matriks [1,0]
                    //dan di cek lagi pada if (i != j) maka nilai matriks c akan berguna pada proses selanjutnya
                    //c berisi nilai matriks yg tidak sama [0,0][1,1] dst sesuai dgn perulangan yg disimpan pada variabel. variabel c tdk masuk pada perulangan bawahnya shg nilainya tidak berubah

                    if (i != j) // jika i=0 dan j=0 maka TIDAK BOLEH dan kembali ke for j, JIKA berbeda berarti i=0 dan j=1
                    {
                        for (int l = 0; l < matriks.GetLength(1); l++)
                        {                       
                            // yg pertama di olah berarti [0,1] karena jika sama [0,0] dst tidak di proses fungsi dalam if
                            matriks_invers[j, l] = matriks_invers[j, l] - c*matriks_invers[i, l]; //pengisian matriks invers atau identitas sesuai dgn manualisasi invers matriks. matriks_invers[i, l] == matriks yg baris dan kolom yang sama misal [0,0] [1,1]
                            matriks[j, l] = matriks[j, l] - c*matriks[i, l]; //pengisian matriks asli sesuai dgn manualisasi invers matriks
                            //matriks_invers[j, l] = Math.Round(matriks_invers[j, l], 9); //pembulatan hasil matriks invers pada matriks identitas
                        }
                    }
                    
                }
            }
            //print
            /*
            this.dataGridView11.ColumnCount = Convert.ToInt16(textBox3.Text);
            for (int r = 0; r < matriks_invers.GetLength(0); r++)
            {
                string[] baris = new string[matriks_invers.GetLength(1)];
                for (int c = 0; c < matriks_invers.GetLength(1); c++)
                {
                    baris[c] = Convert.ToString(matriks_invers[r,c]);
                }

                dataGridView11.Rows.Add(baris);
            }*/
            
            return matriks_invers;
        }

        //method proses testing dengan mengirim data hidden_neuron, input_layer, output_layer, MSE, data_normalisasi, data_uji, beta, bobot_input, bias
        public double[,] Testing(int hidden_neuron, int input_layer, int output_layer, double[,] MSE, double[,] data_normalisasi, double[,] data_uji, double[,] beta, double[,] bobot_input, double[,] bias)
        {
            double[,] Y_prediksi = new double[data_uji.GetLength(0), output_layer]; //inisialisasi array Y prediksi dgn baris sesuai data testing dan kolom sesuai output layer yaitu 1 produksi
            double[,] Y_prediksi_denormalisasi = new double[data_uji.GetLength(0), output_layer]; //inisialisasi matriks hasil denormalisasi prediksi
            double[,] X_uji = new double[data_uji.GetLength(0), input_layer]; //inisialisasi array X testing
            double[,] Y_uji = new double[data_uji.GetLength(0), output_layer]; //inisialisasi array Y uji
            double[,] Y_uji_denormalisasi = new double[data_uji.GetLength(0), output_layer]; //inisialisasi array Y uji denormalisasi
            double[,] bobot_input_transpose = new double[input_layer, hidden_neuron]; //inisialisasi array bobot input transpose
            double[,] H_init_uji = new double[data_uji.GetLength(0), hidden_neuron]; //inisialisasi array H init testing
            double[,] H_uji = new double[data_uji.GetLength(0), hidden_neuron]; //inisialisasi array H exp testing
            double[,] data_min = new double[dataGridView5.Rows.Count, dataGridView5.Columns.Count]; //inisialisasi array data minimal utk denormalisasi
            double[,] data_max = new double[dataGridView6.Rows.Count, dataGridView6.Columns.Count]; //inisialisasi array data maximal utk denormalisasi

            //Get data latih sesuai rasio
            data_uji = get_uji(data_normalisasi, data_uji);

            //Memisah data latih menjadi X dan Y
            X_uji = get_X(data_uji);
            Y_uji = get_Y(data_uji);

            //Menghitung H_init
            bobot_input_transpose = matriks_transpose(bobot_input);
            H_init_uji = matriks_perkalian(X_uji, bobot_input_transpose);
            H_init_uji = dikali_bias(H_init_uji, bias);

            //Menghitung H eksponensial
            H_uji = hitung_H(H_init_uji, H_uji);

            //Menghitung Y_prediksi
            Y_prediksi = matriks_perkalian(H_uji, beta);

            //Denormalisasi
            Y_uji_denormalisasi = denormalisasi(Y_uji, data_min, data_max); // sama dengan mengambil data yang testing yg belum di normalisasi sesuai rasio
            Y_prediksi_denormalisasi = denormalisasi(Y_prediksi, data_min, data_max);

            //Menghitung MSE
            MSE = hitung_MSE(Y_prediksi, Y_uji, MSE);

            //ngeprint Y-uji di table view
            //this.dataGridView4.ColumnCount = 1;
            for (int r = 0; r < Y_uji.GetLength(0); r++)
            {
                string[] baris = new string[Y_uji_denormalisasi.GetLength(1)];
                for (int c = 0; c < Y_uji.GetLength(1); c++)
                {
                    baris[c] = Convert.ToString(Math.Round(Y_uji_denormalisasi[r, c],4));
                }
                dataGridView8.Rows.Add(baris);
            }

            //ngeprint Y-prediksi di table view
            //this.dataGridView5.ColumnCount = 1;
            for (int r = 0; r < Y_prediksi.GetLength(0); r++)
            {
                string[] baris = new string[Y_prediksi.GetLength(1)];
                for (int c = 0; c < Y_prediksi.GetLength(1); c++)
                {
                    baris[c] = Convert.ToString(Math.Round(Y_prediksi_denormalisasi[r, c], 4));
                }
                dataGridView9.Rows.Add(baris);
            }

            //ngeprint Nilai MSE di tabel view
            
            this.dataGridView10.ColumnCount = MSE.GetLength(0);
            for (int r = 0; r < MSE.GetLength(0); r++)
            {
                string[] baris = new string[MSE.GetLength(1)];
                for (int c = 0; c < MSE.GetLength(1); c++)
                {
                    baris[c] = Convert.ToString(MSE[r, c]);
                }
                dataGridView10.Rows.Add(baris);
            }
            return MSE;
            
        }

        //method denormalisasi data
        public double[,] denormalisasi(double[,] data, double[,] data_min, double[,] data_max)
        {
            double[,] data_denormalisasi = new double[data.GetLength(0), data.GetLength(1)];
            data_min = readDataGridMin();
            data_max = readDataGridMax();
            double BA = 0.9;
            double BB = 0.1;

            double min = data_min[0, 4];
            double max = data_max[0, 4];

            for (int i = 0; i < data_denormalisasi.GetLength(0); i++)
            {
                for (int j = 0; j < data_denormalisasi.GetLength(1); j++)
                {
                    data_denormalisasi[i, j] = ((data[i, j] - BB) / (BA - BB)) * (max - min) + min; //seuai dengan rumus di manualisasi excel
                }
            }
            return data_denormalisasi;
        }

        //method hitung MSE evaluasi error
        public double[,] hitung_MSE(double[,] Y_prediksi, double[,] Y_uji, double[,] MSE)
        {
            double produksi = 0; //membuat variabel
            double[,] prod = new double[Y_prediksi.GetLength(0), Y_prediksi.GetLength(1)]; //inisialisasi array
            for (int i = 0; i < Y_prediksi.GetLength(0); i++)
            {
                for (int j = 0; j < Y_prediksi.GetLength(1); j++)
                {
                    prod[i, j] = Math.Pow(Y_prediksi[i, j] - Y_uji[i, j], 2); //dihitung Yprediksi - Yuji kemudian di pangkatkan 2
                }
                produksi = produksi + prod[i, 0]; //hasil keseluruhan
            }
            MSE[0, 0] = Math.Round(produksi/Y_prediksi.GetLength(0), 4);//nilai rata2 dari MSE dibagi dengan jumlah data Y_prediksi

            return MSE;
        }

        //method fungsi prediksi yg prosesnya sama dengan testing
        private void fungsi_prediksi()
        {
            //membuat array dari hasil beta training di dataGrid7
            int banyak_baris = dataGridView7.Rows.Count;
            double[,] beta = new double[banyak_baris, dataGridView7.Columns.Count];

            //mengisi array beta dengan nilai beta yang di datagrid
            foreach (DataGridViewRow row in dataGridView7.Rows)
            {
                foreach (DataGridViewColumn col in dataGridView7.Columns)
                {

                    if (dataGridView7.Rows[row.Index].Cells[col.Index].Value != null)
                    {
                        string value = dataGridView7.Rows[row.Index].Cells[col.Index].Value.ToString();
                        beta[row.Index, col.Index] = Convert.ToDouble(value);
                    }
                }
            }

            //membuat array dari hasil bobot di dataGrid2
            double[,] bobot = new double[dataGridView2.Rows.Count, dataGridView2.Columns.Count];
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                foreach (DataGridViewColumn col in dataGridView2.Columns)
                {

                    if (dataGridView2.Rows[row.Index].Cells[col.Index].Value != null)
                    {
                        string value = dataGridView2.Rows[row.Index].Cells[col.Index].Value.ToString();
                        bobot[row.Index, col.Index] = Convert.ToDouble(value);
                    }
                }
            }

            //membuat array dari bias di dataGrid3
            double[,] bias = new double[dataGridView3.Rows.Count, dataGridView3.Columns.Count];
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                foreach (DataGridViewColumn col in dataGridView3.Columns)
                {

                    if (dataGridView3.Rows[row.Index].Cells[col.Index].Value != null)
                    {
                        string value = dataGridView3.Rows[row.Index].Cells[col.Index].Value.ToString();
                        bias[row.Index, col.Index] = Convert.ToDouble(value);
                    }
                }
            }

            //inisialisasi batas bawah, atas, minTahun,LL,LP,B
            double[,] data_min = new double[dataGridView5.Rows.Count, dataGridView5.Columns.Count];
            double[,] data_max = new double[dataGridView6.Rows.Count, dataGridView6.Columns.Count];
            data_min = readDataGridMin();
            data_max = readDataGridMax();
            double BA = 0.9;
            double BB = 0.1;

            double minT = data_min[0, 0];
            double minLL = data_min[0, 1];
            double minLP = data_min[0, 2];
            double minB = data_min[0, 3];
            double minPro = data_min[0, 4];

            double maxT = data_max[0, 0];
            double maxLL = data_max[0, 1];
            double maxLP = data_max[0, 2];
            double maxB = data_max[0, 3];
            double maxPro = data_max[0, 4];

            //normalisasi
            double[,] X_prediksi = new double[1, 4];
            X_prediksi[0, 0] = Convert.ToDouble(textBox4.Text);
            X_prediksi[0, 0] = (X_prediksi[0, 0] - minT) * (BA - BB) / (maxT - minT) + BB;
            X_prediksi[0, 1] = Convert.ToDouble(textBox5.Text);
            X_prediksi[0, 1] = (X_prediksi[0, 1] - minLL) * (BA - BB) / (maxLL - minLL) + BB;
            X_prediksi[0, 2] = Convert.ToDouble(textBox6.Text);
            X_prediksi[0, 2] = (BA - BB) * ((X_prediksi[0, 2] - minLP) / (maxLP - minLP)) + BB;
            X_prediksi[0, 3] = Convert.ToDouble(textBox7.Text);
            X_prediksi[0, 3] = (BA - BB) * ((X_prediksi[0, 3] - minB) / (maxB - minB)) + BB;

            //inisialisasi input layer dan HN
            int input_layer = 4;
            int hidden_neuron = Convert.ToInt16(textBox3.Text);

            //inisialisasi array atau wadah
            double[,] bobot_input_transpose = new double[input_layer, hidden_neuron];
            double[,] H_init_uji = new double[X_prediksi.GetLength(0), hidden_neuron];
            double[,] H_uji = new double[X_prediksi.GetLength(0), hidden_neuron];
            double[,] Y_prediksi = new double[X_prediksi.GetLength(0),1];

            //Menghitung H_init
            bobot_input_transpose = matriks_transpose(bobot);
            H_init_uji = matriks_perkalian(X_prediksi, bobot_input_transpose);
            H_init_uji = dikali_bias(H_init_uji, bias);

            //Menghitung H
            H_uji = hitung_H(H_init_uji, H_uji);

            //Menghitung Y_prediksi
            Y_prediksi = matriks_perkalian(H_uji, beta);

            //Denormalisasi
            Y_prediksi[0, 0] = (Y_prediksi[0, 0] - BB)/(BA - BB)* (maxPro - minPro)+ minPro;

            label15.Text = Convert.ToString(System.Math.Round(Y_prediksi[0, 0], 0)); //label buat ke grafik, tp di hidden

            //label buat pemisah titik 
            int num1 = Convert.ToInt32(System.Math.Round(Y_prediksi[0, 0], 0));
            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
            string res2 = String.Format(elGR, "{0:0,0}", num1);
            label17.Text = res2.ToString() + " TON";

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //memberi nomor di dataGrid
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
           
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        //fungsi button prediksi click
        private void button3_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Silahkan Cek Data, Hidden Neuron dan Rasio Data!");
            }
            else
            {
                fungsi_prediksi();
                generate_chart();
            }
            
        }

        //chart
      
       
        private void generate_chart()
        {
            //menghapus data yang ada di grafik, shg jika di generate lagi maka grafik akan terbarui
            this.chart1.Series["LuasLahan"].Points.Clear();
            this.chart1.Series["LuasPanen"].Points.Clear();
            this.chart1.Series["Bibit"].Points.Clear();
            this.chart1.Series["Produksi"].Points.Clear();

            double[,] dataChart = new double[dataGridView1.Rows.Count, dataGridView1.Columns.Count];
            dataChart = readDataGrid(); //mengisi dari datagrid 1
            
            for (int z = 0; z < dataChart.GetLength(0); z++)
            {
                this.chart1.Series["LuasLahan"].Points.AddXY(dataChart[z, 0], dataChart[z, 1]); //tahun, nilai luas lahan
                this.chart1.Series["LuasPanen"].Points.AddXY(dataChart[z, 0], dataChart[z, 2]);
                this.chart1.Series["Bibit"].Points.AddXY(dataChart[z, 0], dataChart[z, 3]);
                this.chart1.Series["Produksi"].Points.AddXY(dataChart[z, 0], dataChart[z, 4]);
            }

            chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1; //fungsi supaya menampilkan label tahun 1 per 1

            //menambahkan data dari halaman prediksi
            double tahunPred = Convert.ToDouble(textBox4.Text);
            double LLPred = Convert.ToDouble(textBox5.Text);
            double LPPred = Convert.ToDouble(textBox6.Text);
            double bibitPred = Convert.ToDouble(textBox7.Text);
            double prodPred = Convert.ToDouble(label15.Text);

            this.chart1.Series["LuasLahan"].Points.AddXY(tahunPred, LLPred); //tahun, nilai luas lahan dr prediksi
            this.chart1.Series["LuasPanen"].Points.AddXY(tahunPred, LPPred);
            this.chart1.Series["Bibit"].Points.AddXY(tahunPred, bibitPred);
            this.chart1.Series["Produksi"].Points.AddXY(tahunPred, prodPred);

            /* LABEL SETIAP BATANG GRAFIK
            chart1.Series["LuasLahan"].IsValueShownAsLabel = true;
            chart1.Series["LuasPanen"].IsValueShownAsLabel = true;
            chart1.Series["Bibit"].IsValueShownAsLabel = true;
            chart1.Series["Produksi"].IsValueShownAsLabel = true;
            */

            //Mouse Over label pada batang grafik
            chart1.Series["LuasLahan"].ToolTip = "Luas Lahan : #VALY";
            chart1.Series["LuasPanen"].ToolTip = "Luas Panen : #VALY";
            chart1.Series["Bibit"].ToolTip = "Bibit : #VALY";
            chart1.Series["Produksi"].ToolTip = "Produksi Padi : #VALY";
        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }


        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void kuning_TextChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }
    }
}
