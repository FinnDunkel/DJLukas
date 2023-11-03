using OfficeOpenXml;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace WpfAppToolBar.Pages
{
    
    public partial class Fertigungskosten : Page
    {

        //Werte aus Seite Vorbereitungskosten

        string ausgewählterstoßeins = Datenspeicher.Stoßart;
        string ausgewählterstoßelf = Datenspeicher.Stoßart11;
        string ausgewählterstoßzwei = Datenspeicher.Stoßart2;
        string ausgewählterstoßzweizwei = Datenspeicher.Stoßart22;
        string ausgewählterstoßdrei = Datenspeicher.Stoßart3;

        //Werte aus Seite 1

        string längebauteilseite1 = Datenspeicher.LängeBauteilWert;
        string profilartgenauseite1 = Datenspeicher.ProfilartGenau;
        string laufmeter = Datenspeicher.Gewicht;
        string aktuellerStahlpreis = Datenspeicher.Stahlpreis;
        string stückzahl = Datenspeicher.Stückzahl;
        string gesamthöhe1 = Datenspeicher.Bauteillänge1;
        string flanschbreite2 = Datenspeicher.Bauteilbreite2;
        string flanschdicke3 = Datenspeicher.Flanschbreite3;
        string stegbreite4 = Datenspeicher.Bauteilhöhe4;
        string querschnitt = Datenspeicher.UmfangBauteil;
        string lohnkostenproduktion = Datenspeicher.LohnkostenProduktion;
        string stirnplattenüberstandobenunten;
        string stirnplattenüberstandrechtslinks;
        string laschenabstand;

        //Werte aus Excel

        string za_lasche_schneiden;
        string za_lasche_bleche_bohren;
        string za_lasche_trager_bohren;
        string mk_lasche_schneiden;
        string mk_lasche_bleche_bohren;
        string mk_lasche_trager_bohren;
        string za_stirn_schneiden;
        string za_stirn_bleche_bohren;
        string za_stirn_trager_vorbereiten;
        string za_stirn_trager_schweißen;
        string mk_stirn_schneiden;
        string mk_stirn_bleche_bohren;
        string mk_stirn_trager_vorbereiten;
        string mk_stirn_trager_schweißen;
        string za_schweiß_vorbereiten;
        string za_schweiß_schweißen;
        string mk_schweiß_vorbereiten;
        string mk_schweiß_schweißen;

        public Fertigungskosten()
        {
            InitializeComponent();
            //Excel-Werte 
            #region
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
                        za_lasche_schneiden = worksheet.Cells["B13"].Value?.ToString();
                        za_lasche_bleche_bohren = worksheet.Cells["B15"].Value?.ToString();
                        za_lasche_trager_bohren = worksheet.Cells["B17"].Value?.ToString();

                        mk_lasche_schneiden = worksheet.Cells["B14"].Value?.ToString();
                        mk_lasche_bleche_bohren = worksheet.Cells["B16"].Value?.ToString();
                        mk_lasche_trager_bohren = worksheet.Cells["B18"].Value?.ToString();

                        za_stirn_schneiden = worksheet.Cells["B20"].Value?.ToString();
                        za_stirn_bleche_bohren = worksheet.Cells["B22"].Value?.ToString();
                        za_stirn_trager_vorbereiten = worksheet.Cells["B24"].Value?.ToString();
                        za_stirn_trager_schweißen = worksheet.Cells["B26"].Value?.ToString();

                        mk_stirn_schneiden = worksheet.Cells["B21"].Value?.ToString();
                        mk_stirn_bleche_bohren = worksheet.Cells["B23"].Value?.ToString();
                        mk_stirn_trager_vorbereiten = worksheet.Cells["B25"].Value?.ToString();
                        mk_stirn_trager_schweißen = worksheet.Cells["B27"].Value?.ToString();

                        za_schweiß_vorbereiten = worksheet.Cells["B29"].Value?.ToString();
                        za_schweiß_schweißen = worksheet.Cells["B31"].Value?.ToString();

                        mk_schweiß_vorbereiten = worksheet.Cells["B30"].Value?.ToString();
                        mk_schweiß_schweißen = worksheet.Cells["B32"].Value?.ToString();
                    }
                }
            }
            #endregion

            inhalte_der_Textboxen();

            txt_box_einstoß.Text = "Teilung im Mittelpunkt (1/2 ; 1/2) + " + ausgewählterstoßeins;
            txt_box_elfstoß.Text = "Teilung im Drittelspunkt (1/3 ; 2/3) + " + ausgewählterstoßelf;
            txt_box_zweistoß.Text = "Teilung im Drittelspunkt (1/3 ; 1/3 ; 1/3) + " + ausgewählterstoßzwei;
            txt_box_zweizweistoß.Text = "Teilung im Viertelspunkt (1/4 ; 2/4 ; 1/4) + " + ausgewählterstoßzweizwei;
            txt_box_dreistoß.Text = "Teilung im Viertelspunkt (1/4 ; 1/4 ; 1/4 ; 1/4) + " + ausgewählterstoßdrei;
        }


        private void btn_weiter_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB0C4DE");

            System.Windows.Media.SolidColorBrush brush = new System.Windows.Media.SolidColorBrush(color);

            btn_weiter.Background = brush;
            btn_weiter.Cursor = Cursors.Hand;

            NavigationService.Navigate(new Uri("/Pages/Transportkosten.xaml", UriKind.Relative));
        }

        private void inhalte_der_Textboxen()
        {
            if (längebauteilseite1 != null && aktuellerStahlpreis != null)
            {
                decimal stückzahlwert = Convert.ToDecimal(stückzahl);
                decimal gewichtwert = Convert.ToDecimal(laufmeter); //Gewicht in kg/m
                decimal stahlpreisWert = Convert.ToDecimal(aktuellerStahlpreis);
                decimal gesamthöhe1wert = Convert.ToDecimal(gesamthöhe1);
                decimal flanschbreite2wert = Convert.ToDecimal(flanschbreite2);
                decimal flanschdicke3wert = Convert.ToDecimal(flanschdicke3);
                decimal stegbreite4wert = Convert.ToDecimal(stegbreite4);
                decimal stirnplattenüberstandobenuntenwert = Convert.ToDecimal(stirnplattenüberstandobenunten);
                decimal stirnplattenüberstandrechtslinkswert = Convert.ToDecimal(stirnplattenüberstandrechtslinks);
                decimal querschnittwert = Convert.ToDecimal(querschnitt);
                decimal laschenabstandwert = Convert.ToDecimal(laschenabstand);

                decimal längeBauteilWert = Convert.ToDecimal(längebauteilseite1);
                decimal lohnkostenproduktionwert = Convert.ToDecimal(lohnkostenproduktion);
                decimal za_stirn_schneidenwert = Convert.ToDecimal(za_stirn_schneiden);
                decimal mk_stirn_schneidenwert = Convert.ToDecimal(mk_stirn_schneiden);
                decimal mk_stirn_bleche_bohrenwert = Convert.ToDecimal(mk_stirn_bleche_bohren);
                decimal za_stirn_bleche_bohrenwert = Convert.ToDecimal(za_stirn_bleche_bohren);
                decimal mk_stirn_trager_vorbereitenwert = Convert.ToDecimal(mk_stirn_trager_vorbereiten);
                decimal za_stirn_trager_vorbereitenwert = Convert.ToDecimal(za_stirn_trager_vorbereiten);
                decimal mk_stirn_trager_schweißenwert = Convert.ToDecimal(mk_stirn_trager_schweißen);
                decimal za_stirn_trager_schweißenwert = Convert.ToDecimal(za_stirn_trager_schweißen);

                decimal mk_lasche_schneidenwert = Convert.ToDecimal(mk_lasche_schneiden);
                decimal za_lasche_schneidewert = Convert.ToDecimal(za_lasche_schneiden);
                decimal mk_lasche_bleche_bohrenwert = Convert.ToDecimal(mk_lasche_bleche_bohren);
                decimal za_lasche_bleche_bohrenwert = Convert.ToDecimal(za_lasche_bleche_bohren);
                decimal mk_lasche_trager_bohrenwert = Convert.ToDecimal(mk_lasche_trager_bohren);
                decimal za_lasche_trager_bohrenwert = Convert.ToDecimal(za_lasche_trager_bohren);

                decimal mk_schweiß_schweißenwert = Convert.ToDecimal(mk_schweiß_schweißen);
                decimal za_schweiß_schweißenwert = Convert.ToDecimal(za_schweiß_schweißen);
                decimal mk_schweiß_vorbereitenwert = Convert.ToDecimal(mk_schweiß_vorbereiten);
                decimal za_schweiß_vorbereitenwert = Convert.ToDecimal(za_schweiß_vorbereiten);

                //Rechnung für Stoß eins oben rechts
                #region
                if (ausgewählterstoßeins == "Stirnplattenstoß")
                {
                    decimal längebauteil = Convert.ToDecimal(längeBauteilWert) / 2;

                    if (längebauteil > 14m)
                    {
                        //Maschinen- und Lohnkosten 
                        decimal gewichteineplatte = ((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (stegbreite4wert + flanschdicke3wert) * 7850 / 1000000000) / 1000;

                        decimal zuschneiden = mk_stirn_schneidenwert * gewichteineplatte * 2 + za_stirn_schneidenwert * gewichteineplatte * 2 * lohnkostenproduktionwert; //zwei Platten, ein Stoß
                        decimal bohren = mk_stirn_bleche_bohrenwert * gewichteineplatte * 2 + za_stirn_bleche_bohrenwert * gewichteineplatte * 2 * lohnkostenproduktionwert;
                        decimal vorbereiten = mk_stirn_trager_vorbereitenwert * gewichteineplatte * 4 + za_stirn_trager_vorbereitenwert * gewichteineplatte * 4 * lohnkostenproduktionwert; //mal 4 um Gewicht Träger zu berücksichtigen
                        decimal schweißen = mk_stirn_trager_schweißenwert * gewichteineplatte * 4 + za_stirn_trager_schweißenwert * gewichteineplatte * 4 * lohnkostenproduktionwert;
                        decimal schweißenlänger14 = (mk_schweiß_schweißenwert + mk_schweiß_vorbereitenwert) * gewichtwert / 1000 * querschnittwert + (za_schweiß_vorbereitenwert + za_schweiß_schweißenwert) * gewichtwert / 1000 * querschnittwert * lohnkostenproduktionwert;

                        decimal summe = zuschneiden + bohren + vorbereiten + schweißen + schweißenlänger14;
                        string summeText = summe.ToString("N2"); //Zwei Dezimalstellen. 
                        txt_box_1.Text = summeText + " €";

                        //Zeitaufwand 
                        decimal zuschneidenzeit = za_stirn_schneidenwert * gewichteineplatte * 2;
                        decimal bohrenzeit = za_stirn_bleche_bohrenwert * gewichteineplatte * 2;
                        decimal vorbereitenzeit = za_stirn_trager_vorbereitenwert * gewichteineplatte * 4; //mal 4 um Gewicht Träger zu berücksichtigen  
                        decimal schweißenzeit = za_stirn_trager_schweißenwert * gewichteineplatte * 4;
                        decimal schweißenlänger14zeit = (za_schweiß_vorbereitenwert + za_schweiß_schweißenwert) * gewichtwert / 1000 * querschnittwert;

                        decimal summezeit = zuschneidenzeit + bohrenzeit + vorbereitenzeit + schweißenzeit + schweißenlänger14zeit;
                        string summzeitText = summezeit.ToString("N2");
                        txt_box_11.Text = summzeitText + " h";

                        //Summe
                        decimal maschine = summe * stückzahlwert;
                        string maschineText = maschine.ToString("N2");

                        decimal zeit = summezeit * stückzahlwert;
                        string zeitText = zeit.ToString("N2");
                        txt_box_12.Text = maschineText + " €  und  " + zeitText + " h";
                    }
                    else
                    {
                        //Maschinen- und Lohnkosten 
                        decimal gewichteineplatte = ((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (stegbreite4wert + flanschdicke3wert) * 7850 / 1000000000) / 1000;

                        decimal zuschneiden = mk_stirn_schneidenwert * gewichteineplatte * 2 + za_stirn_schneidenwert * gewichteineplatte * 2 * lohnkostenproduktionwert; //zwei Platten, ein Stoß
                        decimal bohren = mk_stirn_bleche_bohrenwert * gewichteineplatte * 2 + za_stirn_bleche_bohrenwert * gewichteineplatte * 2 * lohnkostenproduktionwert;
                        decimal vorbereiten = mk_stirn_trager_vorbereitenwert * gewichteineplatte * 4 + za_stirn_trager_vorbereitenwert * gewichteineplatte * 4 * lohnkostenproduktionwert; //mal 4 um Gewicht Träger zu berücksichtigen
                        decimal schweißen = mk_stirn_trager_schweißenwert * gewichteineplatte * 4 + za_stirn_trager_schweißenwert * gewichteineplatte * 4 * lohnkostenproduktionwert;

                        decimal summe = zuschneiden + bohren + vorbereiten + schweißen;
                        string summeText = summe.ToString("N2"); //Zwei Dezimalstellen. 
                        txt_box_1.Text = summeText + " €";

                        //Zeitaufwand 
                        decimal zuschneidenzeit = za_stirn_schneidenwert * gewichteineplatte * 2;
                        decimal bohrenzeit = za_stirn_bleche_bohrenwert * gewichteineplatte * 2;
                        decimal vorbereitenzeit = za_stirn_trager_vorbereitenwert * gewichteineplatte * 4; //mal 4 um Gewicht Träger zu berücksichtigen  
                        decimal schweißenzeit = za_stirn_trager_schweißenwert * gewichteineplatte * 4;

                        decimal summezeit = zuschneidenzeit + bohrenzeit + vorbereitenzeit + schweißenzeit;
                        string summzeitText = summezeit.ToString("N2");
                        txt_box_11.Text = summzeitText + " h";

                        //Summe
                        decimal maschine = summe * stückzahlwert;
                        string maschineText = maschine.ToString("N2");

                        decimal zeit = summezeit * stückzahlwert;
                        string zeitText = zeit.ToString("N2");
                        txt_box_12.Text = maschineText + " €  und  " + zeitText + " h";
                    }
                }
                else if (ausgewählterstoßeins == "Laschenstoß")
                {
                    decimal stoßmaterial1 = Convert.ToDecimal(längeBauteilWert) / 2;

                    if (stoßmaterial1 > 14m)
                    {
                        //Maschinen- und Lohnkosten
                        decimal gewichteinblech = (gesamthöhe1wert - 2 * flanschdicke3wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke3wert - 2 * laschenabstandwert) * 2) * flanschdicke3wert * 7850 / 1000000000 / 1000;

                        decimal zuschneiden = mk_lasche_schneidenwert * gewichteinblech * 4 + za_lasche_schneidewert * gewichteinblech * 4 * lohnkostenproduktionwert;  //vier Laschen pro Stoß
                        decimal bohren = mk_lasche_bleche_bohrenwert * gewichteinblech * 4 + za_lasche_bleche_bohrenwert * gewichteinblech * 4 * lohnkostenproduktionwert;
                        decimal bohrentrager = mk_lasche_trager_bohrenwert * gewichteinblech * 8 + za_lasche_trager_bohrenwert * gewichteinblech * 8 * lohnkostenproduktionwert;
                        decimal schweißenlänger14 = (mk_schweiß_schweißenwert + mk_schweiß_vorbereitenwert) * gewichtwert / 1000 * querschnittwert + (za_schweiß_vorbereitenwert + za_schweiß_schweißenwert) * gewichtwert / 1000 * querschnittwert * lohnkostenproduktionwert;

                        decimal summe = zuschneiden + bohren + bohrentrager + schweißenlänger14;
                        string summeText = summe.ToString("N2"); //zwei Dezimalstellen
                        txt_box_1.Text = summeText + " €";

                        //Zeitaufwand 
                        decimal zuschneidezeit = za_lasche_schneidewert * gewichteinblech * 4;
                        decimal bohrenzeit = za_lasche_bleche_bohrenwert * gewichteinblech * 4;
                        decimal bohrentragerzeit = za_lasche_trager_bohrenwert * gewichteinblech * 8;
                        decimal schweißenlänger14zeit = (za_schweiß_vorbereitenwert + za_schweiß_schweißenwert) * gewichtwert / 1000 * querschnittwert;

                        decimal summezeit = zuschneidezeit + bohrenzeit + bohrentragerzeit + schweißenlänger14zeit;
                        string summezeitText = summezeit.ToString("N2");
                        txt_box_11.Text = summezeitText + " h";

                        //Summe
                        decimal maschine = summe * stückzahlwert;
                        string maschineText = maschine.ToString("N2");

                        decimal zeit = summezeit * stückzahlwert;
                        string zeitText = zeit.ToString("N2");
                        txt_box_12.Text = maschineText + " €  und  " + zeitText + " h";
                    }
                    else
                    {
                        //Maschinen- und Lohnkosten
                        decimal gewichteinblech = (gesamthöhe1wert - 2 * flanschdicke3wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke3wert - 2 * laschenabstandwert) * 2) * flanschdicke3wert * 7850 / 1000000000 / 1000;

                        decimal zuschneiden = mk_lasche_schneidenwert * gewichteinblech * 4 + za_lasche_schneidewert * gewichteinblech * 4 * lohnkostenproduktionwert;  //vier Laschen pro Stoß
                        decimal bohren = mk_lasche_bleche_bohrenwert * gewichteinblech * 4 + za_lasche_bleche_bohrenwert * gewichteinblech * 4 * lohnkostenproduktionwert;
                        decimal bohrentrager = mk_lasche_trager_bohrenwert * gewichteinblech * 8 + za_lasche_trager_bohrenwert * gewichteinblech * 8 * lohnkostenproduktionwert;

                        decimal summe = zuschneiden + bohren + bohrentrager;
                        string summeText = summe.ToString("N2"); //zwei Dezimalstellen
                        txt_box_1.Text = summeText + " €";

                        //Zeitaufwand 
                        decimal zuschneidezeit = za_lasche_schneidewert * gewichteinblech * 4;
                        decimal bohrenzeit = za_lasche_bleche_bohrenwert * gewichteinblech * 4;
                        decimal bohrentragerzeit = za_lasche_trager_bohrenwert * gewichteinblech * 8; //mal 8 um Mehraufwand für Träger zu berücksichtigen 

                        decimal summezeit = zuschneidezeit + bohrenzeit + bohrentragerzeit;
                        string summezeitText = summezeit.ToString("N2");
                        txt_box_11.Text = summezeitText + " h";

                        //Summe
                        decimal maschine = summe * stückzahlwert;
                        string maschineText = maschine.ToString("N2");

                        decimal zeit = summezeit * stückzahlwert;
                        string zeitText = zeit.ToString("N2");
                        txt_box_12.Text = maschineText + " €  und  " + zeitText + " h";
                    }
                }
                #endregion

                //Rechnung für Stoß elf oben rechts 
                #region
                if (ausgewählterstoßelf == "Stirnplattenstoß")
                {
                    decimal stoßmaterial11 = Convert.ToDecimal(längeBauteilWert) * 2 / 3;
                    decimal stoßmaterial12 = Convert.ToDecimal(längeBauteilWert) * 1 / 3;

                    if (stoßmaterial11 > 14m)
                    {
                        //Maschinen- und Lohnkosten 
                        decimal gewichteineplatte = ((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (stegbreite4wert + flanschdicke3wert) * 7850 / 1000000000) / 1000;

                        decimal zuschneiden = mk_stirn_schneidenwert * gewichteineplatte * 2 + za_stirn_schneidenwert * gewichteineplatte * 2 * lohnkostenproduktionwert; //zwei Platten, ein Stoß
                        decimal bohren = mk_stirn_bleche_bohrenwert * gewichteineplatte * 2 + za_stirn_bleche_bohrenwert * gewichteineplatte * 2 * lohnkostenproduktionwert;
                        decimal vorbereiten = mk_stirn_trager_vorbereitenwert * gewichteineplatte * 4 + za_stirn_trager_vorbereitenwert * gewichteineplatte * 4 * lohnkostenproduktionwert; //mal 4 um Gewicht Träger zu berücksichtigen
                        decimal schweißen = mk_stirn_trager_schweißenwert * gewichteineplatte * 4 + za_stirn_trager_schweißenwert * gewichteineplatte * 4 * lohnkostenproduktionwert;
                        decimal schweißenlänger14 = (mk_schweiß_schweißenwert + mk_schweiß_vorbereitenwert) * gewichtwert / 1000 * querschnittwert + (za_schweiß_vorbereitenwert + za_schweiß_schweißenwert) * gewichtwert / 1000 * querschnittwert * lohnkostenproduktionwert;

                        decimal summe = zuschneiden + bohren + vorbereiten + schweißen + schweißenlänger14;
                        string summeText = summe.ToString("N2"); //Zwei Dezimalstellen. 
                        txt_box_111.Text = summeText + " €";

                        //Zeitaufwand 
                        decimal zuschneidenzeit = za_stirn_schneidenwert * gewichteineplatte * 2;
                        decimal bohrenzeit = za_stirn_bleche_bohrenwert * gewichteineplatte * 2;
                        decimal vorbereitenzeit = za_stirn_trager_vorbereitenwert * gewichteineplatte * 4; //mal 4 um Gewicht Träger zu berücksichtigen  
                        decimal schweißenzeit = za_stirn_trager_schweißenwert * gewichteineplatte * 4;
                        decimal schweißenlänger14zeit = (za_schweiß_vorbereitenwert + za_schweiß_schweißenwert) * gewichtwert / 1000 * querschnittwert;

                        decimal summezeit = zuschneidenzeit + bohrenzeit + vorbereitenzeit + schweißenzeit + schweißenlänger14zeit;
                        string summzeitText = summezeit.ToString("N2");
                        txt_box_112.Text = summzeitText + " h";

                        //Summe
                        decimal maschine = summe * stückzahlwert;
                        string maschineText = maschine.ToString("N2");

                        decimal zeit = summezeit * stückzahlwert;
                        string zeitText = zeit.ToString("N2");
                        txt_box_113.Text = maschineText + " €  und  " + zeitText + " h";
                    }
                    else
                    {
                        //Maschinen- und Lohnkosten 
                        decimal gewichteineplatte = ((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (stegbreite4wert + flanschdicke3wert) * 7850 / 1000000000) / 1000;

                        decimal zuschneiden = mk_stirn_schneidenwert * gewichteineplatte * 2 + za_stirn_schneidenwert * gewichteineplatte * 2 * lohnkostenproduktionwert; //zwei Platten, ein Stoß
                        decimal bohren = mk_stirn_bleche_bohrenwert * gewichteineplatte * 2 + za_stirn_bleche_bohrenwert * gewichteineplatte * 2 * lohnkostenproduktionwert;
                        decimal vorbereiten = mk_stirn_trager_vorbereitenwert * gewichteineplatte * 4 + za_stirn_trager_vorbereitenwert * gewichteineplatte * 4 * lohnkostenproduktionwert; //mal 4 um Gewicht Träger zu berücksichtigen
                        decimal schweißen = mk_stirn_trager_schweißenwert * gewichteineplatte * 4 + za_stirn_trager_schweißenwert * gewichteineplatte * 4 * lohnkostenproduktionwert;

                        decimal summe = zuschneiden + bohren + vorbereiten + schweißen;
                        string summeText = summe.ToString("N2"); //Zwei Dezimalstellen. 
                        txt_box_111.Text = summeText + " €";

                        //Zeitaufwand 
                        decimal zuschneidenzeit = za_stirn_schneidenwert * gewichteineplatte * 2;
                        decimal bohrenzeit = za_stirn_bleche_bohrenwert * gewichteineplatte * 2;
                        decimal vorbereitenzeit = za_stirn_trager_vorbereitenwert * gewichteineplatte * 4; //mal 4 um Gewicht Träger zu berücksichtigen  
                        decimal schweißenzeit = za_stirn_trager_schweißenwert * gewichteineplatte * 4;

                        decimal summezeit = zuschneidenzeit + bohrenzeit + vorbereitenzeit + schweißenzeit;
                        string summzeitText = summezeit.ToString("N2");
                        txt_box_112.Text = summzeitText + " h";

                        //Summe
                        decimal maschine = summe * stückzahlwert;
                        string maschineText = maschine.ToString("N2");

                        decimal zeit = summezeit * stückzahlwert;
                        string zeitText = zeit.ToString("N2");
                        txt_box_113.Text = maschineText + " €  und  " + zeitText + " h";
                    }
                }
                else if (ausgewählterstoßelf == "Laschenstoß")
                {
                    decimal stoßmaterial11 = Convert.ToDecimal(längeBauteilWert) * 2 / 3;
                    decimal stoßmaterial12 = Convert.ToDecimal(längeBauteilWert) * 1 / 3;

                    if (stoßmaterial11 > 14m)
                    {
                        //Maschinen- und Lohnkosten
                        decimal gewichteinblech = (gesamthöhe1wert - 2 * flanschdicke3wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke3wert - 2 * laschenabstandwert) * 2) * flanschdicke3wert * 7850 / 1000000000 / 1000;

                        decimal zuschneiden = mk_lasche_schneidenwert * gewichteinblech * 4 + za_lasche_schneidewert * gewichteinblech * 4 * lohnkostenproduktionwert;  //vier Laschen pro Stoß
                        decimal bohren = mk_lasche_bleche_bohrenwert * gewichteinblech * 4 + za_lasche_bleche_bohrenwert * gewichteinblech * 4 * lohnkostenproduktionwert;
                        decimal bohrentrager = mk_lasche_trager_bohrenwert * gewichteinblech * 8 + za_lasche_trager_bohrenwert * gewichteinblech * 8 * lohnkostenproduktionwert;
                        decimal schweißenlänger14 = (mk_schweiß_schweißenwert + mk_schweiß_vorbereitenwert) * gewichtwert / 1000 * querschnittwert + (za_schweiß_vorbereitenwert + za_schweiß_schweißenwert) * gewichtwert / 1000 * querschnittwert * lohnkostenproduktionwert;

                        decimal summe = zuschneiden + bohren + bohrentrager + schweißenlänger14;
                        string summeText = summe.ToString("N2"); //zwei Dezimalstellen
                        txt_box_111.Text = summeText + " €";

                        //Zeitaufwand 
                        decimal zuschneidezeit = za_lasche_schneidewert * gewichteinblech * 4;
                        decimal bohrenzeit = za_lasche_bleche_bohrenwert * gewichteinblech * 4;
                        decimal bohrentragerzeit = za_lasche_trager_bohrenwert * gewichteinblech * 8;
                        decimal schweißenlänger14zeit = (za_schweiß_vorbereitenwert + za_schweiß_schweißenwert) * gewichtwert / 1000 * querschnittwert;

                        decimal summezeit = zuschneidezeit + bohrenzeit + bohrentragerzeit + schweißenlänger14zeit;
                        string summezeitText = summezeit.ToString("N2");
                        txt_box_112.Text = summezeitText + " h";

                        //Summe
                        decimal maschine = summe * stückzahlwert;
                        string maschineText = maschine.ToString("N2");

                        decimal zeit = summezeit * stückzahlwert;
                        string zeitText = zeit.ToString("N2");
                        txt_box_113.Text = maschineText + " €  und  " + zeitText + " h";
                    }
                    else
                    {
                        //Maschinen- und Lohnkosten
                        decimal gewichteinblech = (gesamthöhe1wert - 2 * flanschdicke3wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke3wert - 2 * laschenabstandwert) * 2) * flanschdicke3wert * 7850 / 1000000000 / 1000;

                        decimal zuschneiden = mk_lasche_schneidenwert * gewichteinblech * 4 + za_lasche_schneidewert * gewichteinblech * 4 * lohnkostenproduktionwert;  //vier Laschen pro Stoß
                        decimal bohren = mk_lasche_bleche_bohrenwert * gewichteinblech * 4 + za_lasche_bleche_bohrenwert * gewichteinblech * 4 * lohnkostenproduktionwert;
                        decimal bohrentrager = mk_lasche_trager_bohrenwert * gewichteinblech * 8 + za_lasche_trager_bohrenwert * gewichteinblech * 8 * lohnkostenproduktionwert;

                        decimal summe = zuschneiden + bohren + bohrentrager;
                        string summeText = summe.ToString("N2"); //zwei Dezimalstellen
                        txt_box_111.Text = summeText + " €";

                        //Zeitaufwand 
                        decimal zuschneidezeit = za_lasche_schneidewert * gewichteinblech * 4;
                        decimal bohrenzeit = za_lasche_bleche_bohrenwert * gewichteinblech * 4;
                        decimal bohrentragerzeit = za_lasche_trager_bohrenwert * gewichteinblech * 8; //mal 8 um Mehraufwand für Träger zu berücksichtigen 

                        decimal summezeit = zuschneidezeit + bohrenzeit + bohrentragerzeit;
                        string summezeitText = summezeit.ToString("N2");
                        txt_box_112.Text = summezeitText + " h";

                        //Summe
                        decimal maschine = summe * stückzahlwert;
                        string maschineText = maschine.ToString("N2");

                        decimal zeit = summezeit * stückzahlwert;
                        string zeitText = zeit.ToString("N2");
                        txt_box_113.Text = maschineText + " €  und  " + zeitText + " h";
                    }
                }
                #endregion

                //Rechnung für Stoß zwei unten links 
                #region
                if (ausgewählterstoßzwei == "Stirnplattenstoß")
                {
                    decimal stoßmaterial2 = Convert.ToDecimal(längeBauteilWert) * 1 / 3;

                    {
                        //Maschinen- und Lohnkosten 
                        decimal gewichteineplatte = ((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (stegbreite4wert + flanschdicke3wert) * 7850 / 1000000000) / 1000;

                        decimal zuschneiden = mk_stirn_schneidenwert * gewichteineplatte * 2 + za_stirn_schneidenwert * gewichteineplatte * 2 * lohnkostenproduktionwert; //zwei Platten, ein Stoß
                        decimal bohren = mk_stirn_bleche_bohrenwert * gewichteineplatte * 2 + za_stirn_bleche_bohrenwert * gewichteineplatte * 2 * lohnkostenproduktionwert;
                        decimal vorbereiten = mk_stirn_trager_vorbereitenwert * gewichteineplatte * 4 + za_stirn_trager_vorbereitenwert * gewichteineplatte * 4 * lohnkostenproduktionwert; //mal 4 um Gewicht Träger zu berücksichtigen
                        decimal schweißen = mk_stirn_trager_schweißenwert * gewichteineplatte * 4 + za_stirn_trager_schweißenwert * gewichteineplatte * 4 * lohnkostenproduktionwert;

                        decimal summe = (zuschneiden + bohren + vorbereiten + schweißen) * 2;
                        string summeText = summe.ToString("N2"); //Zwei Dezimalstellen. 
                        txt_box_2.Text = summeText + " €";

                        //Zeitaufwand 
                        decimal zuschneidenzeit = za_stirn_schneidenwert * gewichteineplatte * 2;
                        decimal bohrenzeit = za_stirn_bleche_bohrenwert * gewichteineplatte * 2;
                        decimal vorbereitenzeit = za_stirn_trager_vorbereitenwert * gewichteineplatte * 4; //mal 4 um Gewicht Träger zu berücksichtigen  
                        decimal schweißenzeit = za_stirn_trager_schweißenwert * gewichteineplatte * 4;

                        decimal summezeit = (zuschneidenzeit + bohrenzeit + vorbereitenzeit + schweißenzeit) * 2;
                        string summzeitText = summezeit.ToString("N2");
                        txt_box_22.Text = summzeitText + " h";

                        //Summe
                        decimal maschine = summe * stückzahlwert;
                        string maschineText = maschine.ToString("N2");

                        decimal zeit = summezeit * stückzahlwert;
                        string zeitText = zeit.ToString("N2");
                        txt_box_23.Text = maschineText + " €  und  " + zeitText + " h";
                    }
                }
                else if (ausgewählterstoßzwei == "Laschenstoß")
                {
                    {
                        //Maschinen- und Lohnkosten
                        decimal gewichteinblech = (gesamthöhe1wert - 2 * flanschdicke3wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke3wert - 2 * laschenabstandwert) * 2) * flanschdicke3wert * 7850 / 1000000000 / 1000;

                        decimal zuschneiden = mk_lasche_schneidenwert * gewichteinblech * 4 + za_lasche_schneidewert * gewichteinblech * 4 * lohnkostenproduktionwert;  //vier Laschen pro Stoß
                        decimal bohren = mk_lasche_bleche_bohrenwert * gewichteinblech * 4 + za_lasche_bleche_bohrenwert * gewichteinblech * 4 * lohnkostenproduktionwert;
                        decimal bohrentrager = mk_lasche_trager_bohrenwert * gewichteinblech * 8 + za_lasche_trager_bohrenwert * gewichteinblech * 8 * lohnkostenproduktionwert;

                        decimal summe = (zuschneiden + bohren + bohrentrager) * 2;
                        string summeText = summe.ToString("N2"); //zwei Dezimalstellen
                        txt_box_2.Text = summeText + " €";

                        //Zeitaufwand 
                        decimal zuschneidezeit = za_lasche_schneidewert * gewichteinblech * 4;
                        decimal bohrenzeit = za_lasche_bleche_bohrenwert * gewichteinblech * 4;
                        decimal bohrentragerzeit = za_lasche_trager_bohrenwert * gewichteinblech * 8; //mal 8 um Mehraufwand für Träger zu berücksichtigen 

                        decimal summezeit = (zuschneidezeit + bohrenzeit + bohrentragerzeit) * 2;
                        string summezeitText = summezeit.ToString("N2");
                        txt_box_22.Text = summezeitText + " h";

                        //Summe
                        decimal maschine = summe * stückzahlwert;
                        string maschineText = maschine.ToString("N2");

                        decimal zeit = summezeit * stückzahlwert;
                        string zeitText = zeit.ToString("N2");
                        txt_box_23.Text = maschineText + " €  und  " + zeitText + " h";
                    }
                }
                #endregion

                //Rechnung für Stoß zweizwei unten links
                #region
                if (ausgewählterstoßzweizwei == "Stirnplattenstoß")
                {
                    decimal stoßmaterial22 = Convert.ToDecimal(längeBauteilWert) * 1 / 4;
                    decimal stoßmaterial221 = Convert.ToDecimal(längeBauteilWert) * 2 / 4;

                    {
                        //Maschinen- und Lohnkosten 
                        decimal gewichteineplatte = ((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (stegbreite4wert + flanschdicke3wert) * 7850 / 1000000000) / 1000;

                        decimal zuschneiden = mk_stirn_schneidenwert * gewichteineplatte * 2 + za_stirn_schneidenwert * gewichteineplatte * 2 * lohnkostenproduktionwert; //zwei Platten, ein Stoß
                        decimal bohren = mk_stirn_bleche_bohrenwert * gewichteineplatte * 2 + za_stirn_bleche_bohrenwert * gewichteineplatte * 2 * lohnkostenproduktionwert;
                        decimal vorbereiten = mk_stirn_trager_vorbereitenwert * gewichteineplatte * 4 + za_stirn_trager_vorbereitenwert * gewichteineplatte * 4 * lohnkostenproduktionwert; //mal 4 um Gewicht Träger zu berücksichtigen
                        decimal schweißen = mk_stirn_trager_schweißenwert * gewichteineplatte * 4 + za_stirn_trager_schweißenwert * gewichteineplatte * 4 * lohnkostenproduktionwert;

                        decimal summe = (zuschneiden + bohren + vorbereiten + schweißen) * 2;
                        string summeText = summe.ToString("N2"); //Zwei Dezimalstellen. 
                        txt_box_222.Text = summeText + " €";

                        //Zeitaufwand 
                        decimal zuschneidenzeit = za_stirn_schneidenwert * gewichteineplatte * 2;
                        decimal bohrenzeit = za_stirn_bleche_bohrenwert * gewichteineplatte * 2;
                        decimal vorbereitenzeit = za_stirn_trager_vorbereitenwert * gewichteineplatte * 4; //mal 4 um Gewicht Träger zu berücksichtigen  
                        decimal schweißenzeit = za_stirn_trager_schweißenwert * gewichteineplatte * 4;

                        decimal summezeit = (zuschneidenzeit + bohrenzeit + vorbereitenzeit + schweißenzeit) * 2;
                        string summzeitText = summezeit.ToString("N2");
                        txt_box_223.Text = summzeitText + " h";

                        //Summe
                        decimal maschine = summe * stückzahlwert;
                        string maschineText = maschine.ToString("N2");

                        decimal zeit = summezeit * stückzahlwert;
                        string zeitText = zeit.ToString("N2");
                        txt_box_224.Text = maschineText + " €  und  " + zeitText + " h";
                    }
                }
                else if (ausgewählterstoßzweizwei == "Laschenstoß")
                {
                    {
                        //Maschinen- und Lohnkosten
                        decimal gewichteinblech = (gesamthöhe1wert - 2 * flanschdicke3wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke3wert - 2 * laschenabstandwert) * 2) * flanschdicke3wert * 7850 / 1000000000 / 1000;

                        decimal zuschneiden = mk_lasche_schneidenwert * gewichteinblech * 4 + za_lasche_schneidewert * gewichteinblech * 4 * lohnkostenproduktionwert;  //vier Laschen pro Stoß
                        decimal bohren = mk_lasche_bleche_bohrenwert * gewichteinblech * 4 + za_lasche_bleche_bohrenwert * gewichteinblech * 4 * lohnkostenproduktionwert;
                        decimal bohrentrager = mk_lasche_trager_bohrenwert * gewichteinblech * 8 + za_lasche_trager_bohrenwert * gewichteinblech * 8 * lohnkostenproduktionwert;

                        decimal summe = (zuschneiden + bohren + bohrentrager) * 2;
                        string summeText = summe.ToString("N2"); //zwei Dezimalstellen
                        txt_box_222.Text = summeText + " €";

                        //Zeitaufwand 
                        decimal zuschneidezeit = za_lasche_schneidewert * gewichteinblech * 4;
                        decimal bohrenzeit = za_lasche_bleche_bohrenwert * gewichteinblech * 4;
                        decimal bohrentragerzeit = za_lasche_trager_bohrenwert * gewichteinblech * 8; //mal 8 um Mehraufwand für Träger zu berücksichtigen 

                        decimal summezeit = (zuschneidezeit + bohrenzeit + bohrentragerzeit) * 2;
                        string summezeitText = summezeit.ToString("N2");
                        txt_box_223.Text = summezeitText + " h";

                        //Summe
                        decimal maschine = summe * stückzahlwert;
                        string maschineText = maschine.ToString("N2");

                        decimal zeit = summezeit * stückzahlwert;
                        string zeitText = zeit.ToString("N2");
                        txt_box_224.Text = maschineText + " €  und  " + zeitText + " h";
                    }
                }
                #endregion

                //Rechnung für Stoß drei unten rechts
                #region
                if (ausgewählterstoßdrei == "Stirnplattenstoß")
                {
                    decimal stoßmaterial3 = Convert.ToDecimal(längeBauteilWert) * 1 / 4;
                    {
                        //Maschinen- und Lohnkosten 
                        decimal gewichteineplatte = ((gesamthöhe1wert + 2 * stirnplattenüberstandobenuntenwert) * (flanschbreite2wert + 2 * stirnplattenüberstandrechtslinkswert) * (stegbreite4wert + flanschdicke3wert) * 7850 / 1000000000) / 1000;

                        decimal zuschneiden = mk_stirn_schneidenwert * gewichteineplatte * 2 + za_stirn_schneidenwert * gewichteineplatte * 2 * lohnkostenproduktionwert; //zwei Platten, ein Stoß
                        decimal bohren = mk_stirn_bleche_bohrenwert * gewichteineplatte * 2 + za_stirn_bleche_bohrenwert * gewichteineplatte * 2 * lohnkostenproduktionwert;
                        decimal vorbereiten = mk_stirn_trager_vorbereitenwert * gewichteineplatte * 4 + za_stirn_trager_vorbereitenwert * gewichteineplatte * 4 * lohnkostenproduktionwert; //mal 4 um Gewicht Träger zu berücksichtigen
                        decimal schweißen = mk_stirn_trager_schweißenwert * gewichteineplatte * 4 + za_stirn_trager_schweißenwert * gewichteineplatte * 4 * lohnkostenproduktionwert;

                        decimal summe = (zuschneiden + bohren + vorbereiten + schweißen) * 2;
                        string summeText = summe.ToString("N2"); //Zwei Dezimalstellen. 
                        txt_box_3.Text = summeText + " €";

                        //Zeitaufwand 
                        decimal zuschneidenzeit = za_stirn_schneidenwert * gewichteineplatte * 2;
                        decimal bohrenzeit = za_stirn_bleche_bohrenwert * gewichteineplatte * 2;
                        decimal vorbereitenzeit = za_stirn_trager_vorbereitenwert * gewichteineplatte * 4; //mal 4 um Gewicht Träger zu berücksichtigen  
                        decimal schweißenzeit = za_stirn_trager_schweißenwert * gewichteineplatte * 4;

                        decimal summezeit = (zuschneidenzeit + bohrenzeit + vorbereitenzeit + schweißenzeit) * 2;
                        string summzeitText = summezeit.ToString("N2");
                        txt_box_31.Text = summzeitText + " h";

                        //Summe
                        decimal maschine = summe * stückzahlwert;
                        string maschineText = maschine.ToString("N2");

                        decimal zeit = summezeit * stückzahlwert;
                        string zeitText = zeit.ToString("N2");
                        txt_box_32.Text = maschineText + " €  und  " + zeitText + " h";
                    }
                }
                else if (ausgewählterstoßdrei == "Laschenstoß")
                {
                    {
                        //Maschinen- und Lohnkosten
                        decimal gewichteinblech = (gesamthöhe1wert - 2 * flanschdicke3wert - 2 * laschenabstandwert) * ((gesamthöhe1wert - 2 * flanschdicke3wert - 2 * laschenabstandwert) * 2) * flanschdicke3wert * 7850 / 1000000000 / 1000;

                        decimal zuschneiden = mk_lasche_schneidenwert * gewichteinblech * 4 + za_lasche_schneidewert * gewichteinblech * 4 * lohnkostenproduktionwert;  //vier Laschen pro Stoß
                        decimal bohren = mk_lasche_bleche_bohrenwert * gewichteinblech * 4 + za_lasche_bleche_bohrenwert * gewichteinblech * 4 * lohnkostenproduktionwert;
                        decimal bohrentrager = mk_lasche_trager_bohrenwert * gewichteinblech * 8 + za_lasche_trager_bohrenwert * gewichteinblech * 8 * lohnkostenproduktionwert;

                        decimal summe = (zuschneiden + bohren + bohrentrager) * 2;
                        string summeText = summe.ToString("N2"); //zwei Dezimalstellen
                        txt_box_3.Text = summeText + " €";

                        //Zeitaufwand 
                        decimal zuschneidezeit = za_lasche_schneidewert * gewichteinblech * 4;
                        decimal bohrenzeit = za_lasche_bleche_bohrenwert * gewichteinblech * 4;
                        decimal bohrentragerzeit = za_lasche_trager_bohrenwert * gewichteinblech * 8; //mal 8 um Mehraufwand für Träger zu berücksichtigen 

                        decimal summezeit = (zuschneidezeit + bohrenzeit + bohrentragerzeit) * 2;
                        string summezeitText = summezeit.ToString("N2");
                        txt_box_31.Text = summezeitText + " h";

                        //Summe
                        decimal maschine = summe * stückzahlwert;
                        string maschineText = maschine.ToString("N2");

                        decimal zeit = summezeit * stückzahlwert;
                        string zeitText = zeit.ToString("N2");
                        txt_box_32.Text = maschineText + " €  und  " + zeitText + " h";
                    }
                }
                #endregion

                decimal längegrößer14m;

                if (decimal.TryParse(längebauteilseite1, out längegrößer14m))
                {
                    if (längegrößer14m > 28m)
                    {
                        nullstoß.Text = "Bauteil wird mit zwei Schweißstößen ausgeführt.";

                        //Maschinen- und Lohnkosten 

                        decimal schweißenlänger14 = (mk_schweiß_schweißenwert + mk_schweiß_vorbereitenwert) * gewichtwert / 1000 * querschnittwert + (za_schweiß_vorbereitenwert + za_schweiß_schweißenwert) * gewichtwert / 1000 * querschnittwert * lohnkostenproduktionwert;

                        decimal summe = schweißenlänger14;
                        string summeText = summe.ToString("N2"); //Zwei Dezimalstellen. 
                        txt_box_1.Text = summeText + " €";

                        //Zeitaufwand 
                        decimal schweißenlänger14zeit = (za_schweiß_vorbereitenwert + za_schweiß_schweißenwert) * gewichtwert / 1000 * querschnittwert;

                        decimal summezeit = schweißenlänger14zeit;
                        string summzeitText = summezeit.ToString("N2");
                        txt_box_11.Text = summzeitText + " h";

                        //Summe
                        decimal maschine = summe * stückzahlwert;
                        string maschineText = maschine.ToString("N2");

                        decimal zeit = summezeit * stückzahlwert;
                        string zeitText = zeit.ToString("N2");
                        txt_box_12.Text = maschineText + " €  und  " + zeitText + " h";
                    }
                    else if (längegrößer14m > 14m)
                    {
                        nullstoß.Text = "Bauteil wird mit einem Schweißstoß ausgeführt.";
                    }
                }
            }
        }
    }
}
