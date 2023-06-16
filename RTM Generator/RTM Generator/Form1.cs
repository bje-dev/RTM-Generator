using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RTM_Generator
{
    public partial class Form1 : Form
    {
        private ArduinoINOGenerator generator;

        public Form1()
        {
            InitializeComponent();
            generator = new ArduinoINOGenerator();

            int inicio = 0;
            int fin = 255;

            // Agrega los números al ComboBox
            for (int i = inicio; i <= fin; i++)
            {
                comboBox2.Items.Add(i);
                comboBox3.Items.Add(i);
                comboBox4.Items.Add(i);
                comboBox5.Items.Add(i);
                comboBox6.Items.Add(i);
                comboBox7.Items.Add(i);
                comboBox8.Items.Add(i);
                comboBox9.Items.Add(i);
                comboBox10.Items.Add(i);
                comboBox11.Items.Add(i);
                comboBox12.Items.Add(i);
                comboBox13.Items.Add(i);
                comboBox14.Items.Add(i);
                comboBox15.Items.Add(i);
                comboBox16.Items.Add(i);
                comboBox17.Items.Add(i);


            }

            textBox3.Text = "80";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nameNetwork = textBox1.Text as string;
            string passNetwork = textBox2.Text as string;

            string ip1 = comboBox2.SelectedItem as string;
            string ip2 = comboBox3.SelectedItem as string;
            string ip3 = comboBox4.SelectedItem as string;
            string ip4 = comboBox5.SelectedItem as string;

            string mask1 = comboBox6.SelectedItem as string;
            string mask2 = comboBox7.SelectedItem as string;
            string mask3 = comboBox8.SelectedItem as string;
            string mask4 = comboBox9.SelectedItem as string;

            string gate1 = comboBox10.SelectedItem as string;
            string gate2 = comboBox11.SelectedItem as string;
            string gate3 = comboBox12.SelectedItem as string;
            string gate4 = comboBox13.SelectedItem as string;

            string server1 = comboBox14.SelectedItem as string;
            string server2 = comboBox15.SelectedItem as string;
            string server3 = comboBox16.SelectedItem as string;
            string server4 = comboBox17.SelectedItem as string;

            string portServer = textBox3.Text as string;

            string km = textBox4.Text as string;
            string kmDec = textBox5.Text as string;


            if (string.IsNullOrEmpty(nameNetwork))
            {
                MessageBox.Show("Indica el SSID de la red WIFI");
                return;
            }

            if (string.IsNullOrEmpty(passNetwork))
            {
                MessageBox.Show("Introduce la contraseña de la red WIFI");
                return;
            }


            if (string.IsNullOrEmpty(ip1)|| string.IsNullOrEmpty(ip2)|| string.IsNullOrEmpty(ip3)|| string.IsNullOrEmpty(ip4))
            {
                MessageBox.Show("Debe completar correctamente los campos de IP del dispositivo.");
                return;
            }
            if (string.IsNullOrEmpty(mask1) || string.IsNullOrEmpty(mask2) || string.IsNullOrEmpty(mask3) || string.IsNullOrEmpty(mask4))
            {
                MessageBox.Show("Debe completar correctamente los campos de MASCARA del dispositivo.");
                return;
            }
            if (string.IsNullOrEmpty(gate1) || string.IsNullOrEmpty(gate2) || string.IsNullOrEmpty(gate3) || string.IsNullOrEmpty(gate4))
            {
                MessageBox.Show("Debe completar correctamente los campos de PUERTA DE ENLACE del dispositivo.");
                return;
            }
            if (string.IsNullOrEmpty(server1) || string.IsNullOrEmpty(server2) || string.IsNullOrEmpty(server3) || string.IsNullOrEmpty(server4))
            {
                MessageBox.Show("Debe completar correctamente los campos del SERVIDOR donde apunta.");
                return;
            }

            string cppCode = @"
            const char* ssid = ""{paramNameNetwork}"";
            const char* password = ""{paramPassNetwork}"";
            const char* host = ""{paramServ1}.{paramServ2}.{paramServ3}.{paramServ4}"";//poner el Ip del servidor

            IPAddress staticIP({paramIp1}, {paramIp2}, {paramIp3}, paramIp4); // Dirección IP fija deseada
            IPAddress gateway({paramGate1}, {paramGate2}, {paramGate3}, {paramGate4});    // Dirección IP de tu router
            IPAddress subnet({paramMask1}, {paramMask2}, {paramMask3}, {paramMask4});   // Máscara de subred


            /*** Variables para Humedad y Temperatura ****/
            float temperatura = 0;
            float progresiva = {paramKm}.{paramKmDec};
            String url;

            #define DS18B20 5 //DS18B20 esta conectado al pin GPIO D5 del NodeMCU

            WiFiClient client;
            AsyncWebServer server({paramPortServer});
            OneWire ourWire(DS18B20);// Se declara un objeto para la libreria
            DallasTemperature sensor(&ourWire);// Se declara un objeto para la otra libreria
           
            
            ";
           

          


            // Reemplazar ambos valores de delay con el tiempo seleccionado
            string updatedCode = generator.UpdateParamInCPP(cppCode,
                "{nameNetwork}", nameNetwork, 
                "{passNetwork}", passNetwork,
                "{ip1}", ip1,
                "{ip2}", ip2,
                "{ip3}", ip3,
                "{ip4}", ip4,
                "{mask1}", mask1,
                "{mask2}", mask2,
                "{mask3}", mask3,
                "{mask4}", mask4,
                "{gate1}", gate1,
                "{gate2}", gate2,
                "{gate3}", gate3,
                "{gate4}", gate4,
                "{server1}", server1,
                "{server2}", server2,
                "{server3}", server3,
                "{server4}", server4,
                "{portServer}", portServer,
                "{km}", km,
                "{kmDec}", kmDec
                );


            string folderName = "Prueba";
            string folderPath = Path.Combine("C:\\Users\\b-j-e\\Desktop", folderName);

            Directory.CreateDirectory(folderPath);

            // Directorio donde se guardara el archivo .ino
            string filePath = "C:\\Users\\b-j-e\\Desktop\\Prueba\\Prueba.ino";

            //Generador del archivo .ino
            generator.GenerateINOFile(filePath, updatedCode);

            MessageBox.Show("Archivo .ino generado correctamente.");
        }

    }

    public class ArduinoINOGenerator
    {
        public string UpdateParamInCPP(string cppCode, 
            string paramNameNetwork, string valueNameNetwork, 
            string paramPassNetwork, string valuePassNetwork,
            string paramIp1, string valueIp1,
            string paramIp2, string valueIp2,
            string paramIp3, string valueIp3,
            string paramIp4, string valueIp4,
            string paramMask1, string valueMask1,
            string paramMask2, string valueMask2,
            string paramMask3, string valueMask3,
            string paramMask4, string valueMask4,
            string paramGate1, string valueGate1,
            string paramGate2, string valueGate2,
            string paramGate3, string valueGate3,
            string paramGate4, string valueGate4,
            string paramServer1, string valueServer1,
            string paramServer2, string valueServer2,
            string paramServer3, string valueServer3,
            string paramServer4, string valueServer4,
            string paramPortServer, string valuePortServer,
            string paramKm, string valueKm,
            string paramKmDec, string valueKmDec
            )
        {
            // Reemplazar el marcador con el valor proporcionado
            string updatedCode = cppCode.Replace(paramNameNetwork, valueNameNetwork)
                                        .Replace(paramPassNetwork, valuePassNetwork)
                                        .Replace(paramIp1, valueIp1)
                                        .Replace(paramIp2, valueIp2)
                                        .Replace(paramIp3, valueIp3)
                                        .Replace(paramIp4, valueIp4)
                                        .Replace(paramMask1, valueMask1)
                                        .Replace(paramMask2, valueMask2)
                                        .Replace(paramMask3, valueMask3)
                                        .Replace(paramMask4, valueMask4)
                                        .Replace(paramGate1, valueGate1)
                                        .Replace(paramGate2, valueGate2)
                                        .Replace(paramGate3, valueGate3)
                                        .Replace(paramGate4, valueGate4)
                                        .Replace(paramServer1, valueServer1)
                                        .Replace(paramServer2, valueServer2)
                                        .Replace(paramServer3, valueServer3)
                                        .Replace(paramServer4, valueServer4)
                                        .Replace(paramPortServer, valuePortServer)
                                        .Replace(paramKm, valueKm)
                                        .Replace(paramKmDec, valueKmDec)
                                        ;

            return updatedCode;
        }

        public void GenerateINOFile(string filePath, string cppCode)
        {
            // Crea un nuevo archivo .ino
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Escribe el contenido del archivo .ino
                writer.WriteLine("// Código .ino generado por C#");
                writer.WriteLine();

                writer.WriteLine("#include <ESP8266WiFi.h>");
                writer.WriteLine("#include <ESPAsyncWebServer.h>");
                writer.WriteLine("#include <OneWire.h>");
                writer.WriteLine("#include <DallasTemperature.h>");

                writer.WriteLine();

                writer.WriteLine(cppCode);
            }
        }

       
    }
}