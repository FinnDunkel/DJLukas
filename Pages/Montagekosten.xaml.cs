using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppToolBar.Pages
{
    public partial class Montagekosten : Page
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



        public Montagekosten()
        {
            //Erzeuge Baugeräteliste 
            List<GeräteNamen> geräteliste = new List<GeräteNamen>();

            InitializeComponent();

            //Liste befüllen mit Geräten 
            geräteliste.Add(new GeräteNamen("Manitou"));
            geräteliste.Add(new GeräteNamen("Kran1"));
            geräteliste.Add(new GeräteNamen("Kran2"));
            geräteliste.Add(new GeräteNamen("Kran3"));

            //Zuweisung der Liste zu den Dropboxen
            MontageStoßartnull.ItemsSource = geräteliste; 
            MontageStoßart1.ItemsSource = geräteliste;
            VorMontageStoßart1.ItemsSource = geräteliste; 
            MontageStoßart1.ItemsSource = geräteliste;
            VorMontageStoßart11.ItemsSource = geräteliste; 
            MontageStoßart2.ItemsSource = geräteliste;
            MontageStoßart22.ItemsSource = geräteliste; 
            MontageStoßart3.ItemsSource = geräteliste;
            VorMontageStoßart3.ItemsSource = geräteliste;

            inhalte_der_Textboxen();

            txt_box_einstoß.Text = "Teilung im Mittelpunkt (1/2 ; 1/2) + " + ausgewählterstoßeins;
            txt_box_elfstoß.Text = "Teilung im Drittelspunkt (1/3 ; 2/3) + " + ausgewählterstoßelf;
            txt_box_zweistoß.Text = "Teilung im Drittelspunkt (1/3 ; 1/3 ; 1/3) + " + ausgewählterstoßzwei;
            txt_box_zweizweistoß.Text = "Teilung im Viertelspunkt (1/4 ; 2/4 ; 1/4) + " + ausgewählterstoßzweizwei;
            txt_box_dreistoß.Text = "Teilung im Viertelspunkt (1/4 ; 1/4 ; 1/4 ; 1/4) + " + ausgewählterstoßdrei;
        }

        private void inhalte_der_Textboxen()
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
            decimal längegrößer14m;

            if (decimal.TryParse(längebauteilseite1, out längegrößer14m))
            {

                if (längegrößer14m > 28m)
                {
                    txt_box_nullstoß.Text = "Bauteil wird mit zwei Schweißstößen ausgeführt.";

                }
                else if (längegrößer14m > 14m)
                {
                    txt_box_nullstoß.Text = "Bauteil wird mit einem Schweißstoß ausgeführt.";
                }
            }
        }

            private string KostenMontagegerät(string gerätName)
            {
            string gerätKosten = null;

            // Pfad zur Excel-Datei

            string projectDirectory = Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName).Parent.FullName;
            string pathToExcel = System.IO.Path.Combine(projectDirectory, "Profile.xlsx");

            using (var package = new ExcelPackage(new FileInfo(pathToExcel)))
            {
                var worksheet = package.Workbook.Worksheets["Montagegeräte"];

                if (worksheet != null)
                {
                    // Profilnamen befinden sich in Spalte A, Umfang in Spalte G
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Starte bei Zeile 2, um Header zu überspringen
                    {
                        var gerätNameInTabelle = worksheet.Cells[row, 1].Value?.ToString(); // Wert in Spalte A
                        var gerätKostenInTabelle = worksheet.Cells[row, 2].Value?.ToString(); // Wert in Spalte B

                        if (gerätNameInTabelle == gerätName)
                        {
                            gerätKosten = gerätKostenInTabelle;
                            break; // Gerätename gefunden, Schleife beenden
                        }
                    }
                }
            }

            return gerätKosten;
            }

        private void btn_berechnen2_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB0C4DE");

            System.Windows.Media.SolidColorBrush brush = new System.Windows.Media.SolidColorBrush(color);

            btn_berechnen2.Background = brush;
            btn_berechnen2.Cursor = Cursors.Hand;

            NavigationService.Navigate(new Uri("/Pages/Page2.xaml", UriKind.Relative));
        }

        private void MontageStoßart3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(MontageStoßart3.SelectedItem != null) 
            { 
                
            }
        }
    }
    public class GeräteNamen
    {
        public string Geräte { get; set; }

        public GeräteNamen(string _geräte)
        {
            Geräte = _geräte;
        }
    }
}
