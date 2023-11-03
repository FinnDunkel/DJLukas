using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfAppToolBar.Pages
{
    public partial class Vorbereitungskosten : Page
    {

        //Erzeuge Bauteil-Liste
        List<Stöße> stoßListe = new List<Stöße>();

        //Werte aus Seite 1 

        string längebauteilseite1 = Datenspeicher.LängeBauteilWert;
        string profilartgenauseite1 = Datenspeicher.ProfilartGenau;
        string laufmeter = Datenspeicher.Gewicht;
        string aktuellerStahlpreis = Datenspeicher.Stahlpreis;
        string stückzahl = Datenspeicher.Stückzahl;
        string gesamthöhe1 = Datenspeicher.Bauteillänge1;
        string flanschbreite2 = Datenspeicher.Bauteilbreite2;
        string stegbreite3 = Datenspeicher.Flanschbreite3;
        string flanschdicke4 = Datenspeicher.Bauteilhöhe4;
        string querschnitt = Datenspeicher.UmfangBauteil;
        string gesuchterWert; //Excel
        string stirnplattenüberstandobenunten; //Excel
        string stirnplattenüberstandrechtslinks; //Excel
        string laschenabstand; //Excel

        public Vorbereitungskosten()
        {
            InitializeComponent();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            //Pfad zur Excel-Datei
            string projectDirectory = Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName).Parent.FullName;
            string pathToExcel = System.IO.Path.Combine(projectDirectory, "AllgWerte.xlsx");

            if (File.Exists(pathToExcel))
            {
                using (var package = new ExcelPackage(new FileInfo(pathToExcel)))
                {
                    var worksheet = package.Workbook.Worksheets["Tabelle1"];

                    if (worksheet != null)
                    {
                        gesuchterWert = worksheet.Cells["B2"].Value?.ToString();

                        stirnplattenüberstandobenunten = worksheet.Cells["B3"].Value?.ToString();

                        stirnplattenüberstandrechtslinks = worksheet.Cells["B4"].Value?.ToString();

                        laschenabstand = worksheet.Cells["B6"].Value?.ToString();

                    }
                }
            }

            //Zuweisung Liste 
            Stoßart.ItemsSource = stoßListe;
            Stoßart11.ItemsSource = stoßListe;
            Stoßart2.ItemsSource = stoßListe;
            Stoßart22.ItemsSource = stoßListe;
            Stoßart3.ItemsSource = stoßListe;

            //Liste befüllen 
            stoßListe.Add(new Stöße("Stirnplattenstoß"));
            stoßListe.Add(new Stöße("Laschenstoß"));

            //Drop-Down Listen der Stoßarten automatisch setzen:
            Stoßart.SelectedIndex = 0;
            Stoßart11.SelectedIndex = 1;
            Stoßart2.SelectedIndex = 1;
            Stoßart22.SelectedIndex = 1;
            Stoßart3.SelectedIndex = 0;

            //Werte umschreiben, sodass mit diesen gerechnet werden kann
            decimal laufmeterWert = Convert.ToDecimal(laufmeter); //Gewicht
            decimal längeBauteilWert = Convert.ToDecimal(längebauteilseite1);
            decimal stahlpreisWert = Convert.ToDecimal(aktuellerStahlpreis);
            decimal stückzahlwert = Convert.ToDecimal(stückzahl);
            decimal gesamthöhe1weret = Convert.ToDecimal(gesamthöhe1);
            decimal flanschbreite2wert = Convert.ToDecimal(flanschbreite2);
            decimal stegbreite3wert = Convert.ToDecimal(stegbreite3);
            decimal flanschdicke4wert = Convert.ToDecimal(flanschdicke4);
            decimal querschnittwert = Convert.ToDecimal(querschnitt);
            decimal schweißkostenwert = Convert.ToDecimal(gesuchterWert);


            if (längebauteilseite1 != null && aktuellerStahlpreis != null)
            {
                decimal wertAlsDezimal;

                if (decimal.TryParse(längebauteilseite1, out wertAlsDezimal))
                {
                    if (wertAlsDezimal > 14m)
                    {
                        textBoxLänge.Text = "Das Bauteil ist länger als 14 Meter. Somit kann dieses Bauteil aufgrund der Vormateriallänge nicht ohne Stoß ausgeführt werden. Im Folgenden Berechnung mit Schweißstoß.";

                        decimal längeMinus14 = Convert.ToDecimal(längebauteilseite1) - 14m;
                        txt_box_0stoßmaterial.Text = "14 m + " + längeMinus14 + " m à " + stückzahl + " Stück";

                        decimal materialkostengrößer14 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000 + 1 * schweißkostenwert * querschnittwert * stückzahlwert;
                        string materialkostenText = materialkostengrößer14.ToString("N2"); // Zwei Dezimalstellen.
                        txt_box_0stoßkosten.Text = materialkostenText.ToString() + " €";

                        if (längeMinus14 > 14)
                        {
                            decimal längeMinus14größer14 = Convert.ToDecimal(längeMinus14) - 14m;
                            string längeMinus14größer14Text = längeMinus14größer14.ToString("N2"); // Zwei Dezimalstellen.
                            txt_box_0stoßmaterial.Text = "14 m + 14 m + " + längeMinus14größer14Text + " m  à " + stückzahl + " Stück";


                            decimal materialkosten = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000 + 2 * schweißkostenwert * querschnittwert * stückzahlwert;
                            string materialkostengrößer14Text = materialkosten.ToString("N2"); // Zwei Dezimalstellen.
                            txt_box_0stoßkosten.Text = materialkostengrößer14Text.ToString() + " €";
                        }
                    }
                    else if (wertAlsDezimal <= 14m)
                    {
                        textBoxLänge.Text = "Das Bauteil ist kürzer als 14 Meter. Somit kann dieses Bauteil aufgrund der Vormateriallänge ohne Stoß ausgeführt werden.";
                        txt_box_0stoßmaterial.Text = längebauteilseite1 + " m à " + stückzahl + " Stück";

                        decimal materialkostenkleiner14 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        string materialkostenkleiner14Text = materialkostenkleiner14.ToString("N2"); // Zwei Dezimalstellen.
                        txt_box_0stoßkosten.Text = materialkostenkleiner14Text.ToString() + " €";
                    }
                }
            }
        }

        private void btn_berechnen2_Click(object sender, RoutedEventArgs e)
        {
            //Informationen speichern und weitergeben zu Fertigungskosten
            string ausgewählterstoßeins = Stoßart.Text;
            string ausgewählterstoßelf = Stoßart11.Text;
            string ausgewählterstoßzwei = Stoßart2.Text;
            string ausgewählterstoßzweizwei = Stoßart22.Text;
            string ausgewählterstoßdrei = Stoßart3.Text;

            ausgewählterstoßeins = getStoßartText();
            ausgewählterstoßelf = getStoßart11Text();
            ausgewählterstoßzwei = getStoßart2Text();
            ausgewählterstoßzweizwei = getStoßart22Text();
            ausgewählterstoßdrei = getStoßart3Text();

            Datenspeicher.Stoßart = ausgewählterstoßeins;
            Datenspeicher.Stoßart11 = ausgewählterstoßelf;
            Datenspeicher.Stoßart2 = ausgewählterstoßzwei;
            Datenspeicher.Stoßart22 = ausgewählterstoßzweizwei;
            Datenspeicher.Stoßart3 = ausgewählterstoßdrei;



            System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB0C4DE");

            System.Windows.Media.SolidColorBrush brush = new System.Windows.Media.SolidColorBrush(color);

            btn_berechnen2.Background = brush;
            btn_berechnen2.Cursor = Cursors.Hand;

            NavigationService.Navigate(new Uri("/Pages/Fertigungskosten.xaml", UriKind.Relative));
        }
        //Inhalte der ComboBox umschreiben, um auf die Auswahl zugreifen zu können
        #region
        private string getStoßart3Text()
        {
            string ausgewählterStoßdrei = Stoßart3.Text;

            if (Stoßart3.SelectedItem != null)
            {
                //Inhalte aus ComboBox Stoßart umwandeln, sodass auf den Inhalt zugegriffen werden kann
                if (Stoßart3.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Stöße)))
                {
                    Stöße Stoßart3SelectedItem = (Stöße)Stoßart3.SelectedItem;
                    ausgewählterStoßdrei = Stoßart3SelectedItem.Stoss.ToString();
                }
            }
            return ausgewählterStoßdrei;
        }

        private string getStoßart22Text()
        {
            string ausgewählterStoßzweizwei = Stoßart22.Text;

            if (Stoßart22.SelectedItem != null)
            {
                //Inhalte aus ComboBox Stoßart22 umwandeln, sodass auf den Inhalt zugegriffen werden kann
                if (Stoßart22.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Stöße)))
                {
                    Stöße Stoßart22SelectedItem = (Stöße)Stoßart22.SelectedItem;
                    ausgewählterStoßzweizwei = Stoßart22SelectedItem.Stoss.ToString();
                }
            }
            return ausgewählterStoßzweizwei;
        }

        private string getStoßart2Text()
        {
            string ausgewählterStoßzwei = Stoßart2.Text;

            if (Stoßart2.SelectedItem != null)
            {
                //Inhalte aus ComboBox Stoßart umwandeln, sodass auf den Inhalt zugegriffen werden kann
                if (Stoßart2.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Stöße)))
                {
                    Stöße Stoßart2SelectedItem = (Stöße)Stoßart2.SelectedItem;
                    ausgewählterStoßzwei = Stoßart2SelectedItem.Stoss.ToString();
                }
            }
            return ausgewählterStoßzwei;
        }

        private string getStoßart11Text()
        {
            string ausgewählterStoßelf = Stoßart11.Text;

            if (Stoßart11.SelectedItem != null)
            {
                //Inhalte aus ComboBox Stoßart umwandeln, sodass auf den Inhalt zugegriffen werden kann
                if (Stoßart11.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Stöße)))
                {
                    Stöße Stoßart11SelectedItem = (Stöße)Stoßart11.SelectedItem;
                    ausgewählterStoßelf = Stoßart11SelectedItem.Stoss.ToString();
                }
            }
            return ausgewählterStoßelf;
        }

        private string getStoßartText()
        {
            string ausgewählterStoßeins = Stoßart.Text;

            if (Stoßart.SelectedItem != null)
            {
                //Inhalte aus ComboBox Stoßart umwandeln, sodass auf den Inhalt zugegriffen werden kann
                if (Stoßart.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Stöße)))
                {
                    Stöße StoßartSelectedItem = (Stöße)Stoßart.SelectedItem;
                    ausgewählterStoßeins = StoßartSelectedItem.Stoss.ToString();
                }
            }
            return ausgewählterStoßeins;
        }
        #endregion

        private void Stoßart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region
            if (Stoßart.SelectedItem is Stöße selectedStoß)
            {

                string selectedStoßName = selectedStoß.Stoss;
                decimal längeBauteilWert = Convert.ToDecimal(längebauteilseite1);
                decimal stückzahlwert = Convert.ToDecimal(stückzahl);
                decimal laufmeterWert = Convert.ToDecimal(laufmeter); //Gewicht in kg/m
                decimal stahlpreisWert = Convert.ToDecimal(aktuellerStahlpreis);
                decimal gesamthöhe1wert = Convert.ToDecimal(gesamthöhe1);
                decimal flanschbreite2wert = Convert.ToDecimal(flanschbreite2);
                decimal stegbreite3wert = Convert.ToDecimal(stegbreite3);
                decimal flanschdicke4wert = Convert.ToDecimal(flanschdicke4);
                decimal stirnplattenüberstandobenuntenwert = Convert.ToDecimal(stirnplattenüberstandobenunten);
                decimal stirnplattenüberstandrechtslinkswert = Convert.ToDecimal(stirnplattenüberstandrechtslinks);
                decimal querschnittwert = Convert.ToDecimal(querschnitt);
                decimal schweißkostenwert = Convert.ToDecimal(gesuchterWert);
                decimal laschenabstandwert = Convert.ToDecimal(laschenabstand);

                if (selectedStoßName == "Stirnplattenstoß")
                {
                    decimal stoßmaterial1 = Convert.ToDecimal(längeBauteilWert) / 2;

                    if (stoßmaterial1 > 14m)
                    {
                        string stoßmaterial1Text = stoßmaterial1.ToString("N2"); // Zwei Dezimalstellen.
                        txt_box_1stoßmaterial.Text = stoßmaterial1Text + " m + " + stoßmaterial1Text + " m à " + stückzahl + " Stück";

                        decimal stirnplattenkosten = ((((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (flanschdicke4wert + stegbreite3wert) * 7850 / 1000000000) / 1000 * stahlpreisWert) * 2) * stückzahlwert;
                        decimal materialkosten1 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben1 = stirnplattenkosten / 2;
                        decimal schweißkosten1 = querschnittwert / 2 * schweißkostenwert * stückzahlwert * 2;
                        decimal schweißkostenlänger14 = schweißkostenwert * querschnittwert * stückzahlwert * 2;
                        decimal Summe1 = materialkosten1 + stirnplattenkosten + stoßkostenschrauben1 + schweißkosten1 + schweißkostenlänger14;
                        string Summe1Text = Summe1.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_1stoßkosten.Text = Summe1Text.ToString() + " €";

                        //Kosten Rohmaterial unten rechts 
                        decimal materialkostenunten = materialkosten1 / stückzahlwert;
                        string materialkosten1text = materialkostenunten.ToString("N2");
                        txt_box_KostenRohmaterial.Text = materialkosten1text.ToString() + " €";

                        //Gewicht Rohmaterial 
                        decimal gewichtrohmaterialobenlinks = laufmeterWert * längeBauteilWert;
                        string gewichtrohmaterialobenlinkstext = gewichtrohmaterialobenlinks.ToString("N2");
                        txt_box_GewichtRohmaterial.Text = gewichtrohmaterialobenlinkstext.ToString() + " kg";

                        //Gewicht Stirnstoß 
                        decimal gewichtstirnobenlinks = (((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (flanschdicke4wert + stegbreite3wert) * 7850 / 1000000000) / 2 * 2) + (((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (flanschdicke4wert + stegbreite3wert) * 7850 / 1000000000) * 2); //Gewicht Schrauben + Gewicht Platten
                        string gewichtstirnobenlinkstext = gewichtstirnobenlinks.ToString("N2");
                        txt_box_GewichtStirn.Text = gewichtstirnobenlinkstext.ToString() + " kg";

                        //Kosten Stirnstoß unten rechts 
                        decimal materialkosten = gewichtstirnobenlinks / 1000 * stahlpreisWert;
                        decimal schweißkosten = querschnittwert / 2 * schweißkostenwert * 2;
                        decimal stirnkostenunten = materialkosten + schweißkosten;
                        string stirnplattenkostentext = stirnkostenunten.ToString("N2");
                        txt_box_KostenStirn.Text = stirnplattenkostentext.ToString() + " €";
                    }
                    else
                    {
                        string stoßmaterial1Text = stoßmaterial1.ToString("N2"); // Zwei Dezimalstellen.
                        txt_box_1stoßmaterial.Text = stoßmaterial1Text + " m + " + stoßmaterial1Text + " m à " + stückzahl + " Stück";

                        decimal stirnplattenkosten = ((((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (flanschdicke4wert + stegbreite3wert) * 7850 / 1000000000) / 1000 * stahlpreisWert) * 2) * stückzahlwert;
                        decimal materialkosten1 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben1 = stirnplattenkosten / 2;
                        decimal schweißkosten1 = querschnittwert / 2 * schweißkostenwert * stückzahlwert * 2;
                        decimal Summe1 = materialkosten1 + stirnplattenkosten + stoßkostenschrauben1 + schweißkosten1;
                        string Summe1Text = Summe1.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_1stoßkosten.Text = Summe1Text.ToString() + " €";

                        //Kosten Rohmaterial unten rechts 
                        decimal materialkostenunten = materialkosten1 / stückzahlwert;
                        string materialkosten1text = materialkostenunten.ToString("N2");
                        txt_box_KostenRohmaterial.Text = materialkosten1text.ToString() + " €";

                        //Gewicht Rohmaterial 
                        decimal gewichtrohmaterialobenlinks = laufmeterWert * längeBauteilWert;
                        string gewichtrohmaterialobenlinkstext = gewichtrohmaterialobenlinks.ToString("N2");
                        txt_box_GewichtRohmaterial.Text = gewichtrohmaterialobenlinkstext.ToString() + " kg";

                        //Gewicht Stirnstoß 
                        decimal gewichtstirnobenlinks = (((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (flanschdicke4wert + stegbreite3wert) * 7850 / 1000000000) / 2 * 2) + (((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (flanschdicke4wert + stegbreite3wert) * 7850 / 1000000000) * 2); //Gewicht Schrauben + Gewicht Platten
                        string gewichtstirnobenlinkstext = gewichtstirnobenlinks.ToString("N2");
                        txt_box_GewichtStirn.Text = gewichtstirnobenlinkstext.ToString() + " kg";

                        //Kosten Stirnstoß unten rechts 
                        decimal materialkosten = gewichtstirnobenlinks / 1000 * stahlpreisWert;
                        decimal schweißkosten = querschnittwert / 2 * schweißkostenwert * 2;
                        decimal stirnkostenunten = materialkosten + schweißkosten; 
                        string stirnplattenkostentext = stirnkostenunten.ToString("N2");
                        txt_box_KostenStirn.Text = stirnplattenkostentext.ToString() + " €";

                    }
                }
                else if (selectedStoßName == "Laschenstoß")
                {
                    decimal stoßmaterial1 = Convert.ToDecimal(längeBauteilWert) / 2;

                    if (stoßmaterial1 > 14m && gesamthöhe1wert > 600)
                    {
                        string stoßmaterial1Text = stoßmaterial1.ToString("N2"); // Zwei Dezimalstellen.
                        txt_box_1stoßmaterial.Text = stoßmaterial1Text + " m + " + stoßmaterial1Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten1 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten1steg = ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten1 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben1 = laschenkosten1 / 4 + laschenkosten1steg / 4;
                        decimal schweißkostenlänger14 = schweißkostenwert * querschnittwert * stückzahlwert * 2; //*2 da Material zwei mal Schweißgestoßen werden muss
                        decimal Summe1 = materialkosten1 + laschenkosten1steg + laschenkosten1 + stoßkostenschrauben1 + schweißkostenlänger14;
                        string Summe1Text = Summe1.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_1stoßkosten.Text = Summe1Text.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000 * 2;
                        decimal gewichtobensteg = ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000 * 2;
                        decimal gewichtschrauben = (((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000) / 4 + ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000) / 4;
                        decimal gewichtlasche600 = gewichtobenlinks + gewichtobensteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtlasche600.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";

                        //Text Kosten Lasche 
                        decimal laschenkostenunten = gewichtlasche600 / 1000 * stahlpreisWert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";

                    }
                    else if (stoßmaterial1 > 14m && gesamthöhe1wert > 200)
                    {
                        string stoßmaterial1Text = stoßmaterial1.ToString("N2"); // Zwei Dezimalstellen.
                        txt_box_1stoßmaterial.Text = stoßmaterial1Text + " m + " + stoßmaterial1Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten1 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten1steg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten1 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben1 = laschenkosten1 / 2 + laschenkosten1steg / 2;
                        decimal schweißkostenlänger14 = schweißkostenwert * querschnittwert * stückzahlwert * 2; //*2 da Material zwei mal Schweißgestoßen werden muss
                        decimal Summe1 = materialkosten1 + laschenkosten1 + laschenkosten1steg + stoßkostenschrauben1 + schweißkostenlänger14;
                        string Summe1Text = Summe1.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_1stoßkosten.Text = Summe1Text.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * flanschdicke4wert * 7850 / 1000000000 * 2;
                        decimal gewichtobenlinkssteg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * stegbreite3wert * 7850 / 1000000000 * 2;
                        decimal gewichtschrauben = gewichtobenlinks / 2 + gewichtobenlinkssteg / 2; 
                        decimal gewichtLasche201 = gewichtobenlinks + gewichtobenlinkssteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtLasche201.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";

                        //Text Kosten Lasche 
                        decimal laschenkostenunten = gewichtLasche201 / 1000 * stahlpreisWert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";

                    }
                    else if (stoßmaterial1 > 14m && gesamthöhe1wert <= 200)
                    {
                        string stoßmaterial1Text = stoßmaterial1.ToString("N2"); // Zwei Dezimalstellen.
                        txt_box_1stoßmaterial.Text = stoßmaterial1Text + " m + " + stoßmaterial1Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten1 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten1steg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten1 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben1 = laschenkosten1 / 2 + laschenkosten1steg / 2;
                        decimal schweißkostenlänger14 = schweißkostenwert * querschnittwert * stückzahlwert * 2; //*2 da Material zwei mal Schweißgestoßen werden muss
                        decimal Summe1 = materialkosten1 + laschenkosten1 + stoßkostenschrauben1 + schweißkostenlänger14 + laschenkosten1steg;
                        string Summe1Text = Summe1.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_1stoßkosten.Text = Summe1Text.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * flanschdicke4wert * 7850 / 1000000000;
                        decimal gewichtobenlinkssteg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * stegbreite3wert * 7850 / 1000000000;
                        decimal gewichtschrauben = gewichtobenlinks / 2 + gewichtobenlinkssteg / 2; 
                        decimal gewichtlasche200 = gewichtobenlinks + gewichtobenlinkssteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtlasche200.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";

                        //Text Kosten Lasche
                        decimal laschenkostenunten = gewichtlasche200 / 1000 * stahlpreisWert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";

                    }
                    else if (stoßmaterial1 < 14m && gesamthöhe1wert > 600)
                    {
                        string stoßmaterial1Text = stoßmaterial1.ToString("N2"); // Zwei Dezimalstellen.
                        txt_box_1stoßmaterial.Text = stoßmaterial1Text + " m + " + stoßmaterial1Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten11 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten11steg = ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten11 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben11 = laschenkosten11 / 4 + laschenkosten11steg / 4;
                        decimal Summe11 = materialkosten11 + laschenkosten11 + stoßkostenschrauben11 + laschenkosten11steg;
                        string Summe11Text = Summe11.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_1stoßkosten.Text = Summe11Text.ToString() + " €";

                        //Text Kosten Lasche
                        decimal laschenkostenunten = (laschenkosten11 + stoßkostenschrauben11 + laschenkosten11steg) / stückzahlwert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000 * 2;
                        decimal gewichtobensteg = ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000 * 2;
                        decimal gewichtschrauben = (((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000) / 4 + ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000) / 4;
                        decimal gewichtlasche600 = gewichtobenlinks + gewichtobensteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtlasche600.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";
                    }
                    else if (stoßmaterial1 < 14m && gesamthöhe1wert > 200)
                    {
                        string stoßmaterial1Text = stoßmaterial1.ToString("N2"); // Zwei Dezimalstellen.
                        txt_box_1stoßmaterial.Text = stoßmaterial1Text + " m + " + stoßmaterial1Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten1 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten1steg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten1 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben1 = laschenkosten1 / 2 + laschenkosten1steg / 2;
                        decimal Summe1 = materialkosten1 + laschenkosten1 + stoßkostenschrauben1 + laschenkosten1steg;
                        string Summe1Text = Summe1.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_1stoßkosten.Text = Summe1Text.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * flanschdicke4wert * 7850 / 1000000000 * 2;
                        decimal gewichtobenlinkssteg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * stegbreite3wert * 7850 / 1000000000 * 2;
                        decimal gewichtschrauben = gewichtobenlinks / 2 + gewichtobenlinkssteg / 2;
                        decimal gewichtLasche201 = gewichtobenlinks + gewichtobenlinkssteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtLasche201.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";

                        //Text Kosten Laschen
                        decimal laschenkostenunten = gewichtLasche201 / 1000 * stahlpreisWert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";

                    }
                    else if (stoßmaterial1 < 14m && gesamthöhe1wert <= 200)
                    {
                        string stoßmaterial1Text = stoßmaterial1.ToString("N2"); // Zwei Dezimalstellen.
                        txt_box_1stoßmaterial.Text = stoßmaterial1Text + " m + " + stoßmaterial1Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten11 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten1steg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten11 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben11 = laschenkosten11 / 2 + laschenkosten1steg / 2;
                        decimal Summe1 = materialkosten11 + laschenkosten11 + stoßkostenschrauben11 + laschenkosten1steg;
                        string Summe1Text = Summe1.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_1stoßkosten.Text = Summe1Text.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * flanschdicke4wert * 7850 / 1000000000;
                        decimal gewichtobenlinkssteg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * stegbreite3wert * 7850 / 1000000000;
                        decimal gewichtschrauben = gewichtobenlinks / 2 + gewichtobenlinkssteg / 2;
                        decimal gewichtlasche200 = gewichtobenlinks + gewichtobenlinkssteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtlasche200.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";

                        //Text Kosten Lasche
                        decimal laschenkostenunten = gewichtlasche200 / 1000 * stahlpreisWert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";
                    }
                }
            }
            #endregion
        }

        private void Stoßart11_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region
            if (Stoßart11.SelectedItem is Stöße selectedStoß)
            {

                string selectedStoßName = selectedStoß.Stoss;
                decimal längeBauteilWert = Convert.ToDecimal(längebauteilseite1);
                decimal stückzahlwert = Convert.ToDecimal(stückzahl);
                decimal laufmeterWert = Convert.ToDecimal(laufmeter);
                decimal stahlpreisWert = Convert.ToDecimal(aktuellerStahlpreis);
                decimal gesamthöhe1wert = Convert.ToDecimal(gesamthöhe1);
                decimal flanschbreite2wert = Convert.ToDecimal(flanschbreite2);
                decimal stegbreite3wert = Convert.ToDecimal(stegbreite3);
                decimal flanschdicke4wert = Convert.ToDecimal(flanschdicke4);
                decimal stirnplattenüberstandobenuntenwert = Convert.ToDecimal(stirnplattenüberstandobenunten);
                decimal stirnplattenüberstandrechtslinkswert = Convert.ToDecimal(stirnplattenüberstandrechtslinks);
                decimal querschnittwert = Convert.ToDecimal(querschnitt);
                decimal schweißkostenwert = Convert.ToDecimal(gesuchterWert);
                decimal laschenabstandwert = Convert.ToDecimal(laschenabstand);

                if (selectedStoßName == "Stirnplattenstoß")
                {
                    decimal stoßmaterial11 = Convert.ToDecimal(längeBauteilWert) * 2 / 3;
                    decimal stoßmaterial12 = Convert.ToDecimal(längeBauteilWert) * 1 / 3;
                    string stoßmaterial11Text = stoßmaterial11.ToString("N2"); // Zwei Dezimalstellen.
                    string stoßmaterial12Text = stoßmaterial12.ToString("N2"); // Zwei Dezimalstellen.

                    if (stoßmaterial11 > 14m)
                    {
                        txt_box_11stoßmaterial.Text = stoßmaterial11Text + " m + " + stoßmaterial12Text + " m à " + stückzahl + " Stück";

                        decimal stirnplattenkosten11 = ((((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (flanschdicke4wert + stegbreite3wert) * 7850 / 1000000000) / 1000 * stahlpreisWert) * 2) * stückzahlwert;
                        decimal materialkosten11 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben11 = stirnplattenkosten11 / 2;
                        decimal schweißkosten11 = querschnittwert / 2 * schweißkostenwert * stückzahlwert * 2;
                        decimal schweißkostenlänger14 = schweißkostenwert * querschnittwert * stückzahlwert * 1;
                        decimal Summe11 = materialkosten11 + stirnplattenkosten11 + stoßkostenschrauben11 + schweißkosten11 + schweißkostenlänger14;
                        string Summe11Text = Summe11.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_11stoßkosten.Text = Summe11Text.ToString() + " €";
                    }
                    else
                    {
                        txt_box_11stoßmaterial.Text = stoßmaterial11Text + " m + " + stoßmaterial12Text + " m à " + stückzahl + " Stück";

                        decimal stirnplattenkosten11 = ((((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (flanschdicke4wert + stegbreite3wert) * 7850 / 1000000000) / 1000 * stahlpreisWert) * 2) * stückzahlwert;
                        decimal stoßkosten11 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben11 = stirnplattenkosten11 / 2;
                        decimal schweißkosten11 = querschnittwert / 2 * schweißkostenwert * stückzahlwert * 2;
                        decimal Summe11 = stoßkosten11 + stirnplattenkosten11 + stoßkostenschrauben11 + schweißkosten11;
                        string Summe11Text = Summe11.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_11stoßkosten.Text = Summe11Text.ToString() + " €";
                    }
                }
                else if (selectedStoßName == "Laschenstoß")
                {
                    decimal stoßmaterial11 = Convert.ToDecimal(längeBauteilWert) * 2 / 3;
                    decimal stoßmaterial12 = Convert.ToDecimal(längeBauteilWert) * 1 / 3;
                    string stoßmaterial11Text = stoßmaterial11.ToString("N2"); // Zwei Dezimalstellen.
                    string stoßmaterial12Text = stoßmaterial12.ToString("N2"); // Zwei Dezimalstellen.

                    if (stoßmaterial11 > 14m && gesamthöhe1wert > 600)
                    {
                        txt_box_11stoßmaterial.Text = stoßmaterial11Text + " m + " + stoßmaterial12Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten11 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten11steg = ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten11 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben11 = laschenkosten11 / 4 + laschenkosten11steg / 4;
                        decimal schweißkostenlänger14 = schweißkostenwert * querschnittwert * stückzahlwert * 1;
                        decimal Summe11 = materialkosten11 + laschenkosten11 + laschenkosten11steg + stoßkostenschrauben11 + schweißkostenlänger14;
                        string Summe11Text = Summe11.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_11stoßkosten.Text = Summe11Text.ToString() + " €";

                        //Text Kosten Lasche
                        decimal laschenkostenunten = (laschenkosten11 + stoßkostenschrauben11 + laschenkosten11steg) / stückzahlwert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000 * 2;
                        decimal gewichtobensteg = ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000 * 2;
                        decimal gewichtschrauben = (((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000) / 4 + ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000) / 4;
                        decimal gewichtlasche600 = gewichtobenlinks + gewichtobensteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtlasche600.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";
                    }
                    else if (stoßmaterial11 > 14m && gesamthöhe1wert > 200)
                    {
                        txt_box_11stoßmaterial.Text = stoßmaterial11Text + " m + " + stoßmaterial12Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten1 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten1steg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten1 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben1 = laschenkosten1 / 2 + laschenkosten1steg / 2;
                        decimal schweißkostenlänger14 = schweißkostenwert * querschnittwert * stückzahlwert * 2;
                        decimal Summe1 = materialkosten1 + laschenkosten1 + laschenkosten1steg + stoßkostenschrauben1 + schweißkostenlänger14;
                        string Summe1Text = Summe1.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_11stoßkosten.Text = Summe1Text.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * flanschdicke4wert * 7850 / 1000000000 * 2;
                        decimal gewichtobenlinkssteg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * stegbreite3wert * 7850 / 1000000000 * 2;
                        decimal gewichtschrauben = gewichtobenlinks / 2 + gewichtobenlinkssteg / 2;
                        decimal gewichtLasche201 = gewichtobenlinks + gewichtobenlinkssteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtLasche201.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";

                        //Text Kosten Lasche
                        decimal laschenkostenunten = gewichtLasche201 / 1000 * stahlpreisWert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";

                    }
                    else if (stoßmaterial11 > 14m && gesamthöhe1wert <= 200)
                    {
                        txt_box_11stoßmaterial.Text = stoßmaterial11Text + " m + " + stoßmaterial12Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten11 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten1steg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten11 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben11 = laschenkosten11 / 2 + laschenkosten1steg / 2;
                        decimal schweißkostenlänger14 = schweißkostenwert * querschnittwert * stückzahlwert * 2;
                        decimal Summe1 = materialkosten11 + laschenkosten11 + stoßkostenschrauben11 + schweißkostenlänger14 + laschenkosten1steg;
                        string Summe1Text = Summe1.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_11stoßkosten.Text = Summe1Text.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * flanschdicke4wert * 7850 / 1000000000;
                        decimal gewichtobenlinkssteg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * stegbreite3wert * 7850 / 1000000000;
                        decimal gewichtschrauben = gewichtobenlinks / 2 + gewichtobenlinkssteg / 2;
                        decimal gewichtlasche200 = gewichtobenlinks + gewichtobenlinkssteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtlasche200.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";

                        //Text Kosten Lasche
                        decimal laschenkostenunten = gewichtlasche200 / 1000 * stahlpreisWert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";
                    }
                    else if (stoßmaterial11 < 14m && gesamthöhe1wert > 600)
                    {
                        txt_box_11stoßmaterial.Text = stoßmaterial11Text + " m + " + stoßmaterial12Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten11 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten11steg = ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten11 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben11 = laschenkosten11 / 4 + laschenkosten11steg / 4;
                        decimal Summe11 = materialkosten11 + laschenkosten11 + stoßkostenschrauben11 + laschenkosten11steg;
                        string Summe11Text = Summe11.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_11stoßkosten.Text = Summe11Text.ToString() + " €";

                        //Text Kosten Lasche
                        decimal laschenkostenunten = (laschenkosten11 + stoßkostenschrauben11 + laschenkosten11steg) / stückzahlwert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000 * 2;
                        decimal gewichtobensteg = ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000 * 2;
                        decimal gewichtschrauben = (((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000) / 4 + ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000) / 4;
                        decimal gewichtlasche600 = gewichtobenlinks + gewichtobensteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtlasche600.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";
                    }
                    else if (stoßmaterial11 < 14m && gesamthöhe1wert > 200)
                    {
                        txt_box_11stoßmaterial.Text = stoßmaterial11Text + " m + " + stoßmaterial12Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten1 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten1steg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten1 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben1 = laschenkosten1 / 2 + laschenkosten1steg / 2;
                        decimal Summe1 = materialkosten1 + laschenkosten1 + stoßkostenschrauben1 + laschenkosten1steg;
                        string Summe1Text = Summe1.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_11stoßkosten.Text = Summe1Text.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * flanschdicke4wert * 7850 / 1000000000 * 2;
                        decimal gewichtobenlinkssteg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * stegbreite3wert * 7850 / 1000000000 * 2;
                        decimal gewichtschrauben = gewichtobenlinks / 2 + gewichtobenlinkssteg / 2;
                        decimal gewichtLasche201 = gewichtobenlinks + gewichtobenlinkssteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtLasche201.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";

                        //Text Kosten Laschen
                        decimal laschenkostenunten = gewichtLasche201 / 1000 * stahlpreisWert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";

                    }
                    else if (stoßmaterial11 < 14m && gesamthöhe1wert <= 200)
                    {
                        txt_box_11stoßmaterial.Text = stoßmaterial11Text + " m + " + stoßmaterial12Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten11 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten1steg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten11 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben11 = laschenkosten11 / 2 + laschenkosten1steg / 2;
                        decimal Summe1 = materialkosten11 + laschenkosten11 + stoßkostenschrauben11 + laschenkosten1steg;
                        string Summe1Text = Summe1.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_11stoßkosten.Text = Summe1Text.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * flanschdicke4wert * 7850 / 1000000000;
                        decimal gewichtobenlinkssteg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * stegbreite3wert * 7850 / 1000000000;
                        decimal gewichtschrauben = gewichtobenlinks / 2 + gewichtobenlinkssteg / 2;
                        decimal gewichtlasche200 = gewichtobenlinks + gewichtobenlinkssteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtlasche200.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";

                        //Text Kosten Lasche
                        decimal laschenkostenunten = gewichtlasche200 / 1000 * stahlpreisWert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";
                    }
                }
                #endregion
            }
        }

        private void Stoßart3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Stoßart3.SelectedItem is Stöße selectedStoß)
            {

                string selectedStoßName = selectedStoß.Stoss;
                decimal längeBauteilWert = Convert.ToDecimal(längebauteilseite1);
                decimal stückzahlwert = Convert.ToDecimal(stückzahl);
                decimal laufmeterWert = Convert.ToDecimal(laufmeter);
                decimal stahlpreisWert = Convert.ToDecimal(aktuellerStahlpreis);
                decimal gesamthöhe1wert = Convert.ToDecimal(gesamthöhe1);
                decimal flanschbreite2wert = Convert.ToDecimal(flanschbreite2);
                decimal stegbreite3wert = Convert.ToDecimal(stegbreite3);
                decimal flanschdicke4wert = Convert.ToDecimal(flanschdicke4);
                decimal stirnplattenüberstandobenuntenwert = Convert.ToDecimal(stirnplattenüberstandobenunten);
                decimal stirnplattenüberstandrechtslinkswert = Convert.ToDecimal(stirnplattenüberstandrechtslinks);
                decimal querschnittwert = Convert.ToDecimal(querschnitt);
                decimal schweißkostenwert = Convert.ToDecimal(gesuchterWert);
                decimal laschenabstandwert = Convert.ToDecimal(laschenabstand);

                if (selectedStoßName == "Stirnplattenstoß")
                {

                    decimal stoßmaterial3 = Convert.ToDecimal(längeBauteilWert) * 1 / 4;
                    string stoßmaterial3Text = stoßmaterial3.ToString("N2"); // Zwei Dezimalstellen.
                    txt_box_3stoßmaterial.Text = stoßmaterial3Text + " m + " + stoßmaterial3Text + " m + " + stoßmaterial3Text + " m + " + stoßmaterial3Text + " m à " + stückzahl + " Stück";

                    decimal stirnplattenkosten3 = ((((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (flanschdicke4wert + stegbreite3wert) * 7850 / 1000000000) / 1000 * stahlpreisWert) * 2) * stückzahlwert;
                    decimal materialkosten3 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                    decimal stoßkostenschrauben3 = (stirnplattenkosten3 / 2);
                    decimal schweißkosten3 = (querschnittwert / 2 * schweißkostenwert * stückzahlwert * 2) * 3;
                    decimal Summe3 = materialkosten3 + stirnplattenkosten3 * 3 + stoßkostenschrauben3 * 3 + schweißkosten3;
                    string Summe3Text = Summe3.ToString("N2"); // Zwei Dezimalstellen.

                    txt_box_3stoßkosten.Text = Summe3Text.ToString() + " €";

                }
                else if (selectedStoßName == "Laschenstoß")
                {
                    decimal stoßmaterial3 = Convert.ToDecimal(längeBauteilWert) * 1 / 4;
                    string stoßmaterial3Text = stoßmaterial3.ToString("N2"); // Zwei Dezimalstellen.
                    txt_box_3stoßmaterial.Text = stoßmaterial3Text + " m + " + stoßmaterial3Text + " m + " + stoßmaterial3Text + " m + " + stoßmaterial3Text + " m à " + stückzahl + " Stk";

                    if (stoßmaterial3 < 14m && gesamthöhe1wert > 600)
                    {
                        txt_box_3stoßmaterial.Text = stoßmaterial3Text + " m + " + stoßmaterial3Text + " m + " + stoßmaterial3Text + " m + " + stoßmaterial3Text + " m à " + stückzahl + " Stk";

                        decimal laschenkosten11 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten11steg = ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten11 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben11 = laschenkosten11 / 4 + laschenkosten11steg / 4;
                        decimal Summe11 = materialkosten11 + laschenkosten11 * 3 + stoßkostenschrauben11 * 3 + laschenkosten11steg * 3;
                        string Summe11Text = Summe11.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_3stoßkosten.Text = Summe11Text.ToString() + " €";

                        //Text Kosten Lasche
                        decimal laschenkostenunten = (laschenkosten11 + stoßkostenschrauben11 + laschenkosten11steg) / stückzahlwert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000 * 2;
                        decimal gewichtobensteg = ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000 * 2;
                        decimal gewichtschrauben = (((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000) / 4 + ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000) / 4;
                        decimal gewichtlasche600 = gewichtobenlinks + gewichtobensteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtlasche600.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";
                    }
                    else if (stoßmaterial3 < 14m && gesamthöhe1wert > 200)
                    {
                        txt_box_3stoßmaterial.Text = stoßmaterial3Text + " m + " + stoßmaterial3Text + " m + " + stoßmaterial3Text + " m + " + stoßmaterial3Text + " m à " + stückzahl + " Stk";

                        decimal laschenkosten3 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten1steg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten3 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben3 = laschenkosten3 / 2 + laschenkosten1steg / 2;
                        decimal Summe1 = materialkosten3 + laschenkosten3 * 3 + stoßkostenschrauben3 * 3 + laschenkosten1steg * 3;
                        string Summe1Text = Summe1.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_3stoßkosten.Text = Summe1Text.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * flanschdicke4wert * 7850 / 1000000000 * 2;
                        decimal gewichtobenlinkssteg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * stegbreite3wert * 7850 / 1000000000 * 2;
                        decimal gewichtschrauben = gewichtobenlinks / 2 + gewichtobenlinkssteg / 2;
                        decimal gewichtLasche201 = gewichtobenlinks + gewichtobenlinkssteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtLasche201.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";

                        //Text Kosten Lasche
                        decimal laschenkostenunten = gewichtLasche201 / 1000 * stahlpreisWert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";

                    }
                    else if (stoßmaterial3 < 14m && gesamthöhe1wert <= 200)
                    {
                        txt_box_3stoßmaterial.Text = stoßmaterial3Text + " m + " + stoßmaterial3Text + " m + " + stoßmaterial3Text + " m + " + stoßmaterial3Text + " m à " + stückzahl + " Stk";

                        decimal laschenkosten11 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten1steg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten11 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben11 = laschenkosten11 / 2 + laschenkosten1steg / 2;
                        decimal Summe1 = materialkosten11 + laschenkosten11 * 3 + stoßkostenschrauben11 * 3 + laschenkosten1steg * 3;
                        string Summe1Text = Summe1.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_3stoßkosten.Text = Summe1Text.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * flanschdicke4wert * 7850 / 1000000000;
                        decimal gewichtobenlinkssteg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * stegbreite3wert * 7850 / 1000000000;
                        decimal gewichtschrauben = gewichtobenlinks / 2 + gewichtobenlinkssteg / 2;
                        decimal gewichtlasche200 = gewichtobenlinks + gewichtobenlinkssteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtlasche200.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";

                        //Text Kosten Lasche
                        decimal laschenkostenunten = gewichtlasche200 / 1000 * stahlpreisWert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";
                    }
                }
            }
        }

        private void Stoßart2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Stoßart2.SelectedItem is Stöße selectedStoß)
            {

                string selectedStoßName = selectedStoß.Stoss;
                decimal längeBauteilWert = Convert.ToDecimal(längebauteilseite1);
                decimal stückzahlwert = Convert.ToDecimal(stückzahl);
                decimal laufmeterWert = Convert.ToDecimal(laufmeter);
                decimal stahlpreisWert = Convert.ToDecimal(aktuellerStahlpreis);
                decimal gesamthöhe1wert = Convert.ToDecimal(gesamthöhe1);
                decimal flanschbreite2wert = Convert.ToDecimal(flanschbreite2);
                decimal stegbreite3wert = Convert.ToDecimal(stegbreite3);
                decimal flanschdicke4wert = Convert.ToDecimal(flanschdicke4);
                decimal stirnplattenüberstandobenuntenwert = Convert.ToDecimal(stirnplattenüberstandobenunten);
                decimal stirnplattenüberstandrechtslinkswert = Convert.ToDecimal(stirnplattenüberstandrechtslinks);
                decimal querschnittwert = Convert.ToDecimal(querschnitt);
                decimal schweißkostenwert = Convert.ToDecimal(gesuchterWert);
                decimal laschenabstandwert = Convert.ToDecimal(laschenabstand);

                if (selectedStoßName == "Stirnplattenstoß")
                {
                    decimal stoßmaterial2 = Convert.ToDecimal(längeBauteilWert) * 1 / 3;
                    string stoßmaterial2Text = stoßmaterial2.ToString("N2"); // Zwei Dezimalstellen.
                    txt_box_2stoßmaterial.Text = stoßmaterial2Text + " m + " + stoßmaterial2Text + " m + " + stoßmaterial2Text + " m à " + stückzahl + " Stück";

                    decimal stirnplattenkosten2 = (((((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (flanschdicke4wert + stegbreite3wert) * 7850 / 1000000000) / 1000 * stahlpreisWert) * 2) * stückzahlwert);
                    decimal materialkosten2 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                    decimal stoßkostenschrauben2 = (stirnplattenkosten2 / 2);
                    decimal schweißkosten2 = (querschnittwert / 2 * schweißkostenwert * stückzahlwert * 2);
                    decimal Summe2 = materialkosten2 + stirnplattenkosten2 * 2 + stoßkostenschrauben2 * 2 + schweißkosten2 * 2;
                    string Summe2Text = Summe2.ToString("N2"); // Zwei Dezimalstellen.

                    txt_box_2stoßkosten.Text = Summe2Text.ToString() + " €";
                }
                else if (selectedStoßName == "Laschenstoß")
                {
                    decimal stoßmaterial2 = Convert.ToDecimal(längeBauteilWert) * 1 / 3;
                    string stoßmaterial2Text = stoßmaterial2.ToString("N2"); // Zwei Dezimalstellen.
                    txt_box_2stoßmaterial.Text = stoßmaterial2Text + " m + " + stoßmaterial2Text + " m + " + stoßmaterial2Text + " m à " + stückzahl + " Stück";

                    if (stoßmaterial2 < 14m && gesamthöhe1wert > 600)
                    {
                        txt_box_2stoßmaterial.Text = stoßmaterial2Text + " m + " + stoßmaterial2Text + " m + " + stoßmaterial2Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten2 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten2steg = ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten2 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben2 = laschenkosten2 / 4 + laschenkosten2steg / 4;
                        decimal Summe11 = materialkosten2 + laschenkosten2 * 2 + stoßkostenschrauben2 * 2 + laschenkosten2steg * 2;
                        string Summe11Text = Summe11.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_2stoßkosten.Text = Summe11Text.ToString() + " €";

                        //Text Kosten Lasche
                        decimal laschenkostenunten = (laschenkosten2 + stoßkostenschrauben2 + laschenkosten2steg) / stückzahlwert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000 * 2;
                        decimal gewichtobensteg = ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000 * 2;
                        decimal gewichtschrauben = (((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000) / 4 + ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000) / 4;
                        decimal gewichtlasche600 = gewichtobenlinks + gewichtobensteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtlasche600.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";
                    }
                    else if (stoßmaterial2 < 14m && gesamthöhe1wert > 200)
                    {
                        txt_box_2stoßmaterial.Text = stoßmaterial2Text + " m + " + stoßmaterial2Text + " m + " + stoßmaterial2Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten2 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten1steg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten2 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben2 = laschenkosten2 / 2 + laschenkosten1steg / 2;
                        decimal Summe1 = materialkosten2 + laschenkosten2 * 2 + stoßkostenschrauben2 * 2 + laschenkosten1steg * 2;
                        string Summe1Text = Summe1.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_2stoßkosten.Text = Summe1Text.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * flanschdicke4wert * 7850 / 1000000000 * 2;
                        decimal gewichtobenlinkssteg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * stegbreite3wert * 7850 / 1000000000 * 2;
                        decimal gewichtschrauben = gewichtobenlinks / 2 + gewichtobenlinkssteg / 2;
                        decimal gewichtLasche201 = gewichtobenlinks + gewichtobenlinkssteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtLasche201.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";

                        //Text Kosten Lasche
                        decimal laschenkostenunten = gewichtLasche201 / 1000 * stahlpreisWert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";

                    }
                    else if (stoßmaterial2 < 14m && gesamthöhe1wert <= 200)
                    {
                        txt_box_2stoßmaterial.Text = stoßmaterial2Text + " m + " + stoßmaterial2Text + " m + " + stoßmaterial2Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten2 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten1steg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten2 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben2 = laschenkosten2 / 2 + laschenkosten1steg / 2;
                        decimal Summe1 = materialkosten2 + laschenkosten2 * 2 + stoßkostenschrauben2 * 2 + laschenkosten1steg * 2;
                        string Summe1Text = Summe1.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_2stoßkosten.Text = Summe1Text.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * flanschdicke4wert * 7850 / 1000000000;
                        decimal gewichtobenlinkssteg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * stegbreite3wert * 7850 / 1000000000;
                        decimal gewichtschrauben = gewichtobenlinks / 2 + gewichtobenlinkssteg / 2;
                        decimal gewichtlasche200 = gewichtobenlinks + gewichtobenlinkssteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtlasche200.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";

                        //Text Kosten Lasche
                        decimal laschenkostenunten = gewichtlasche200 / 1000 * stahlpreisWert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";
                    }
                }
            }
        }

        private void Stoßart22_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Stoßart22.SelectedItem is Stöße selectedStoß)
            {

                string selectedStoßName = selectedStoß.Stoss;
                decimal längeBauteilWert = Convert.ToDecimal(längebauteilseite1);
                decimal stückzahlwert = Convert.ToDecimal(stückzahl);
                decimal laufmeterWert = Convert.ToDecimal(laufmeter);
                decimal stahlpreisWert = Convert.ToDecimal(aktuellerStahlpreis);
                decimal gesamthöhe1wert = Convert.ToDecimal(gesamthöhe1);
                decimal flanschbreite2wert = Convert.ToDecimal(flanschbreite2);
                decimal stegbreite3wert = Convert.ToDecimal(stegbreite3);
                decimal flanschdicke4wert = Convert.ToDecimal(flanschdicke4);
                decimal stirnplattenüberstandobenuntenwert = Convert.ToDecimal(stirnplattenüberstandobenunten);
                decimal stirnplattenüberstandrechtslinkswert = Convert.ToDecimal(stirnplattenüberstandrechtslinks);
                decimal querschnittwert = Convert.ToDecimal(querschnitt);
                decimal schweißkostenwert = Convert.ToDecimal(gesuchterWert);
                decimal laschenabstandwert = Convert.ToDecimal(laschenabstand);

                if (selectedStoßName == "Stirnplattenstoß")
                {
                    decimal stoßmaterial22 = Convert.ToDecimal(längeBauteilWert) * 1 / 4;
                    decimal stoßmaterial221 = Convert.ToDecimal(längeBauteilWert) * 2 / 4;
                    string stoßmaterial22Text = stoßmaterial22.ToString("N2"); // Zwei Dezimalstellen.
                    string stoßmaterial221Text = stoßmaterial221.ToString("N2"); // Zwei Dezimalstellen.
                    txt_box_22stoßmaterial.Text = stoßmaterial22Text + " m + " + stoßmaterial221Text + " m +" + stoßmaterial22Text + " m à " + stückzahl + " Stück";

                    decimal stirnplattenkosten22 = (((((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (flanschdicke4wert + stegbreite3wert) * 7850 / 1000000000) / 1000 * stahlpreisWert) * 2) * stückzahlwert);
                    decimal materialkosten22 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                    decimal stoßkostenschrauben22 = (stirnplattenkosten22 / 2);
                    decimal schweißkosten22 = (querschnittwert / 2 * schweißkostenwert * stückzahlwert * 2);
                    decimal Summe22 = materialkosten22 + stirnplattenkosten22 * 2 + stoßkostenschrauben22 * 2 + schweißkosten22 * 2;
                    string Summe22Text = Summe22.ToString("N2"); // Zwei Dezimalstellen.

                    txt_box_22stoßkosten.Text = Summe22Text.ToString() + " €";
                }
                else if (selectedStoßName == "Laschenstoß")
                {
                    decimal stoßmaterial22 = Convert.ToDecimal(längeBauteilWert) * 1 / 4;
                    decimal stoßmaterial221 = Convert.ToDecimal(längeBauteilWert) * 2 / 4;
                    string stoßmaterial22Text = stoßmaterial22.ToString("N2"); // Zwei Dezimalstellen.
                    string stoßmaterial221Text = stoßmaterial221.ToString("N2"); // Zwei Dezimalstellen.
                    txt_box_22stoßmaterial.Text = stoßmaterial22Text + " m + " + stoßmaterial221Text + " m + " + stoßmaterial22Text + " m à " + stückzahl + " Stück";

                    if (stoßmaterial221 < 14m && gesamthöhe1wert > 600)
                    {
                        txt_box_22stoßmaterial.Text = stoßmaterial22Text + " m + " + stoßmaterial221Text + " m + " + stoßmaterial22Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten2 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten2steg = ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten2 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben2 = laschenkosten2 / 4 + laschenkosten2steg / 4;
                        decimal Summe11 = materialkosten2 + laschenkosten2 * 2 + stoßkostenschrauben2 * 2 + laschenkosten2steg * 2;
                        string Summe11Text = Summe11.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_22stoßkosten.Text = Summe11Text.ToString() + " €";

                        //Text Kosten Lasche
                        decimal laschenkostenunten = (laschenkosten2 + stoßkostenschrauben2 + laschenkosten2steg) / stückzahlwert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000 * 2;
                        decimal gewichtschrauben = (((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000) / 4 + ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * flanschdicke4wert * 7850 / 1000000000) / 4;
                        decimal gewichtobensteg = ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.4m) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 0.2m) * stegbreite3wert * 7850 / 1000000000 * 2;
                        decimal gewichtlasche600 = gewichtobenlinks + gewichtobensteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtlasche600.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";
                    }
                    else if (stoßmaterial221 < 14m && gesamthöhe1wert > 200)
                    {
                        txt_box_22stoßmaterial.Text = stoßmaterial22Text + " m + " + stoßmaterial221Text + " m + " + stoßmaterial22Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten2 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten1steg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten2 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben2 = laschenkosten2 / 2 + laschenkosten1steg / 2;
                        decimal Summe1 = materialkosten2 + laschenkosten2 * 2 + stoßkostenschrauben2 * 2 + laschenkosten1steg * 2;
                        string Summe1Text = Summe1.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_22stoßkosten.Text = Summe1Text.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * flanschdicke4wert * 7850 / 1000000000 * 2;
                        decimal gewichtobenlinkssteg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 1) * stegbreite3wert * 7850 / 1000000000 * 2;
                        decimal gewichtschrauben = gewichtobenlinks / 2 + gewichtobenlinkssteg / 2;
                        decimal gewichtLasche201 = gewichtobenlinks + gewichtobenlinkssteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtLasche201.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";

                        //Text Kosten Lasche
                        decimal laschenkostenunten = gewichtLasche201 / 1000 * stahlpreisWert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";

                    }
                    else if (stoßmaterial221 < 14m && gesamthöhe1wert <= 200)
                    {
                        txt_box_22stoßmaterial.Text = stoßmaterial22Text + " m + " + stoßmaterial221Text + " m + " + stoßmaterial22Text + " m à " + stückzahl + " Stück";

                        decimal laschenkosten2 = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * flanschdicke4wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal laschenkosten1steg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * stegbreite3wert * 7850 / 1000000000 / 1000 * stahlpreisWert * 2 * stückzahlwert;
                        decimal materialkosten2 = stückzahlwert * laufmeterWert * längeBauteilWert * stahlpreisWert / 1000;
                        decimal stoßkostenschrauben2 = laschenkosten2 / 2 + laschenkosten1steg / 2;
                        decimal Summe1 = materialkosten2 + laschenkosten2 * 2 + stoßkostenschrauben2 * 2 + laschenkosten1steg * 2;
                        string Summe1Text = Summe1.ToString("N2"); // Zwei Dezimalstellen.

                        txt_box_22stoßkosten.Text = Summe1Text.ToString() + " €";

                        //Text Gewicht Lasche
                        decimal gewichtobenlinks = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * flanschdicke4wert * 7850 / 1000000000;
                        decimal gewichtobenlinkssteg = (gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke4wert - 2 * laschenabstandwert) * 2) * stegbreite3wert * 7850 / 1000000000;
                        decimal gewichtschrauben = gewichtobenlinks / 2 + gewichtobenlinkssteg / 2;
                        decimal gewichtlasche200 = gewichtobenlinks + gewichtobenlinkssteg + gewichtschrauben;
                        string gewichtobenlinkstext = gewichtlasche200.ToString("N2");
                        txt_box_GewichtLasche.Text = gewichtobenlinkstext.ToString() + " kg";

                        //Text Kosten Lasche
                        decimal laschenkostenunten = gewichtlasche200 / 1000 * stahlpreisWert;
                        string laschenkostenuntentext = laschenkostenunten.ToString("N2");
                        txt_box_KostenLasche.Text = laschenkostenuntentext.ToString() + " €";
                    }

                }
            }
        }
    }

        public class Stöße
        {
            public string Stoss { get; set; }

            public Stöße(string _stirn)
            {
                Stoss = _stirn;
            }
        }
    }





