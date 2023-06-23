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

            int ipComboInicio = 0;
            int ipComboFin = 255;

            // Agrega los números al ComboBox
            for (int i = ipComboInicio; i <= ipComboFin; i++)
            {
                comboBox2.Items.Add(i.ToString());
                comboBox3.Items.Add(i.ToString());
                comboBox4.Items.Add(i.ToString());
                comboBox5.Items.Add(i.ToString());
                comboBox6.Items.Add(i.ToString());
                comboBox7.Items.Add(i.ToString());
                comboBox8.Items.Add(i.ToString());
                comboBox9.Items.Add(i.ToString());
                comboBox10.Items.Add(i.ToString());
                comboBox11.Items.Add(i.ToString());
                comboBox12.Items.Add(i.ToString());
                comboBox13.Items.Add(i.ToString());
                comboBox14.Items.Add(i.ToString());
                comboBox15.Items.Add(i.ToString());
                comboBox16.Items.Add(i.ToString());
                comboBox17.Items.Add(i.ToString());


            }

            comboBox1.Items.Add("Wireless");
            comboBox1.Items.Add("Ethernet");

           

            comboBox18.Items.Add("80");
            comboBox18.Items.Add("443");
            comboBox18.Items.Add("8080");
            comboBox18.Items.Add("8081");
            comboBox18.Items.Add("8082");
            comboBox18.Items.Add("8083");
            comboBox18.Items.Add("8084");
            comboBox18.Items.Add("8085");
            comboBox18.Items.Add("8086");
            comboBox18.Items.Add("8087");
            comboBox18.Items.Add("8088");
            comboBox18.Items.Add("8089");
            comboBox18.Items.Add("8001");
            comboBox18.Items.Add("8002");
            comboBox18.Items.Add("8003");
            comboBox18.Items.Add("8004");
            comboBox18.Items.Add("8005");
            comboBox18.Items.Add("8006");
            comboBox18.Items.Add("8007");
            comboBox18.Items.Add("8008");
            comboBox18.Items.Add("8009");
            comboBox18.Items.Add("8010");
            comboBox18.Items.Add("8020");
            comboBox18.Items.Add("8030");
            comboBox18.Items.Add("8040");
            comboBox18.Items.Add("8050");
            comboBox18.Items.Add("8060");
            comboBox18.Items.Add("8070");
            comboBox18.Items.Add("8080");
            comboBox18.Items.Add("8090");
            comboBox18.Items.Add("8100");
            comboBox18.Items.Add("8200");
            comboBox18.Items.Add("8300");
            comboBox18.Items.Add("8400");
            comboBox18.Items.Add("8500");

            comboBox19.Items.Add("Retiro");
            comboBox19.Items.Add("Saldias");
            comboBox19.Items.Add("Ciudad Universitaria");
            comboBox19.Items.Add("Aristobulo del Valle");
            comboBox19.Items.Add("Padilla");
            comboBox19.Items.Add("Florida");
            comboBox19.Items.Add("Munro");
            comboBox19.Items.Add("Carapachay");
            comboBox19.Items.Add("Villa Adelina");
            comboBox19.Items.Add("Boulogne");
            comboBox19.Items.Add("Vicealmirante Montes");
            comboBox19.Items.Add("Don Torcuato");
            comboBox19.Items.Add("Adolfo Sourdeaux");
            comboBox19.Items.Add("Villa de Mayo");
            comboBox19.Items.Add("Los Polvorines");
            comboBox19.Items.Add("Pablo Nogues");
            comboBox19.Items.Add("Grand Bourg");
            comboBox19.Items.Add("Tierras Altas");
            comboBox19.Items.Add("Tortuguitas");
            comboBox19.Items.Add("Manuel Alberti");
            comboBox19.Items.Add("Del Viso");
            comboBox19.Items.Add("Villa Rosa");

            int kmComboInicio = 0;
            int kmComboFin = 60;

            for (int k = kmComboInicio; k <= kmComboFin; k++)
            {
                comboBox20.Items.Add(k.ToString());
            }

            int decComboInicio = 0;
            int decComboFin = 999;

            for (int d = decComboInicio; d <= decComboFin; d++)
            {
                comboBox21.Items.Add(d.ToString());
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

            string cppCode = @"
            const char* ssid = ""{paramNameNetwork}"";
            const char* password = ""{paramPassNetwork}"";
            const char* host = ""{paramServ1}.{paramServ2}.{paramServ3}.{paramServ4}"";//poner el Ip del servidor
            const char* lugar =""{paramLugar}"";


            IPAddress staticIP({paramIp1}, {paramIp2}, {paramIp3}, paramIp4); // Dirección IP fija deseada
            IPAddress gateway({paramGate1}, {paramGate2}, {paramGate3}, {paramGate4});    // Dirección IP de tu router
            IPAddress subnet({paramMask1}, {paramMask2}, {paramMask3}, {paramMask4});   // Máscara de subred


            /*** Variables para Humedad y Temperatura ****/
            float temperatura = 0;
            float progresiva = {paramKm}.{paramKmDec};
            int dispositivo = {paramDev};
            String url;

            #define DS18B20 5 //DS18B20 esta conectado al pin GPIO D5 del NodeMCU

            WiFiClient client;
            AsyncWebServer server({paramPortServer});
            OneWire ourWire(DS18B20);// Se declara un objeto para la libreria
            DallasTemperature sensor(&ourWire);// Se declara un objeto para la otra libreria
           
            
            ";

            string modo = comboBox1.SelectedItem as string;

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

            string portServer = comboBox18.SelectedItem as string;

            string lugarDev = comboBox19.SelectedItem as string;
            string deviceId = comboBox20.SelectedItem as string + comboBox21.SelectedItem as string;

            string km = comboBox20.SelectedItem as string;
            string kmDec = comboBox21.SelectedItem as string;

           

            if (string.IsNullOrEmpty(modo))
            {
                MessageBox.Show("Selecciona el modo de operacion");
                return;
            }else if (string.IsNullOrEmpty(nameNetwork) && modo == "Wireless")
            {
                MessageBox.Show("Indica el SSID de la red WIFI.");
                return;
            }else if (string.IsNullOrEmpty(passNetwork) && modo == "Wireless")
            {
                MessageBox.Show("Introduce la contraseña de la red WIFI.");
                return;
            }else if (string.IsNullOrEmpty(ip1) || string.IsNullOrEmpty(ip2) || string.IsNullOrEmpty(ip3) || string.IsNullOrEmpty(ip4))
            {
                MessageBox.Show("Debe completar correctamente los campos de IP del dispositivo.");
                return;
            } else if (string.IsNullOrEmpty(mask1) || string.IsNullOrEmpty(mask2) || string.IsNullOrEmpty(mask3) || string.IsNullOrEmpty(mask4))
            {
                MessageBox.Show("Debe completar correctamente los campos de MASCARA del dispositivo.");
                return;
            } else if (string.IsNullOrEmpty(gate1) || string.IsNullOrEmpty(gate2) || string.IsNullOrEmpty(gate3) || string.IsNullOrEmpty(gate4))
            {
                MessageBox.Show("Debe completar correctamente los campos de PUERTA DE ENLACE del dispositivo.");
                return;
            } else if (string.IsNullOrEmpty(server1) || string.IsNullOrEmpty(server2) || string.IsNullOrEmpty(server3) || string.IsNullOrEmpty(server4))
            {
                MessageBox.Show("Debe completar correctamente los campos del SERVIDOR remoto.");
                return;
            }else if (string.IsNullOrEmpty(lugarDev))
            {
                MessageBox.Show("Selecciona el lugar donde se ubica el dispositivo.");
                return;
            }else if (string.IsNullOrEmpty(km) || string.IsNullOrEmpty(kmDec))
            {
                MessageBox.Show("Debe seleccionar la progresiva de ubicacion correspondiente.");
                return;
            }

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
              "{kmDec}", kmDec,
              "{lugarDev}", lugarDev,
              "{deviceId}", deviceId
              );


            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                // Establecer la descripción del diálogo
                folderDialog.Description = "Seleccionar directorio";

                // Mostrar el diálogo y verificar si el usuario hizo clic en "Aceptar"
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string directorioSeleccionado = folderDialog.SelectedPath;
                    // Hacer algo con el directorio seleccionado
                    // Por ejemplo, mostrarlo en un TextBox
                    string directorioConvertido = directorioSeleccionado.Replace("\\", "\\\\");

                    string folderName = "Wireless";
                    string folderPath = Path.Combine(directorioConvertido, folderName);

                    Directory.CreateDirectory(folderPath);

                    // Directorio donde se guardara el archivo .ino
                    string fileName = "Wireless.ino";
                    string filePath = Path.Combine(folderPath, fileName);

                    //Generador del archivo .ino
                    generator.GenerateINOFile(filePath, updatedCode);

                    MessageBox.Show("Archivo .ino generado correctamente.");
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Wireless")
            {
                textBox1.Enabled = Enabled;
                textBox2.Enabled = Enabled;
                label3.Enabled = Enabled;
                label7.Enabled = Enabled;    
            }
            else
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                label3.Enabled = false;
                label7 .Enabled = false;

            }
        }
    }

    public class ArduinoINOGenerator
    {

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
            string paramKmDec, string valueKmDec,
            string paramLugar, string valueLugar,
            string paramDev, string valueDev
            )
        {
            // Reemplazar el marcador con el valor proporcionado
            string updatedCode = cppCode.Replace(paramNameNetwork, valueNameNetwork);
                   updatedCode = updatedCode.Replace(paramPassNetwork, valuePassNetwork);
                   updatedCode = updatedCode.Replace(paramIp1, valueIp1);
                   updatedCode = updatedCode.Replace(paramIp2, valueIp2);
                   updatedCode = updatedCode.Replace(paramIp3, valueIp3);
                   updatedCode = updatedCode.Replace(paramIp4, valueIp4);
                   updatedCode = updatedCode.Replace(paramMask1, valueMask1);
                   updatedCode = updatedCode.Replace(paramMask2, valueMask2);
                   updatedCode = updatedCode.Replace(paramMask3, valueMask3);
                   updatedCode = updatedCode.Replace(paramMask4, valueMask4);
                   updatedCode = updatedCode.Replace(paramGate1, valueGate1);
                   updatedCode = updatedCode.Replace(paramGate2, valueGate2);
                   updatedCode = updatedCode.Replace(paramGate3, valueGate3);
                   updatedCode = updatedCode.Replace(paramGate4, valueGate4);
                   updatedCode = updatedCode.Replace(paramServer1, valueServer1);
                   updatedCode = updatedCode.Replace(paramServer2, valueServer2);
                   updatedCode = updatedCode.Replace(paramServer3, valueServer3);
                   updatedCode = updatedCode.Replace(paramServer4, valueServer4);
                   updatedCode = updatedCode.Replace(paramPortServer, valuePortServer);
                   updatedCode = updatedCode.Replace(paramKm, valueKm);
                   updatedCode = updatedCode.Replace(paramKmDec, valueKmDec);
                   updatedCode = updatedCode.Replace(paramLugar, valueLugar);
                   updatedCode = updatedCode.Replace(paramDev, valueDev);
                   ;

            return updatedCode;
        }
    }
}