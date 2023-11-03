using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System.Data;
using OfficeOpenXml;
using System.Diagnostics;

namespace WpfAppToolBar.Pages
{
    public partial class Page1 : Page
    {
        //Erzeuge alle Profil Listen:
        #region

        //Erzeuge Stahlgüte-Liste
        List<Stahlgüteclass> stahlgüteListe = new List<Stahlgüteclass>();

        //Erzeuge Bauteil-Liste
        List<Bauteil> bauteileListe = new List<Bauteil>();

        //Erzeuge Ja-Nein-Liste
        List<JaNein> verzinkenListe = new List<JaNein>();

        //Erzeuge Hohlprofil-Liste
        List<Hohlprofil> hohlprofilListe = new List<Hohlprofil>();

        //Erzeuge Zusammengesetzte-Liste
        List<Zusammengesetzte> zusamengesetzteListe = new List<Zusammengesetzte>();

        //Erzeuge IPE-Liste
        List<IPEProfil> ipeListe = new List<IPEProfil>();

        //Erzeuge IPEo-Liste
        List<IPEoProfil> ipeoListe = new List<IPEoProfil>();

        //Erzeuge IPEv-Liste
        List<IPEvProfil> ipevListe = new List<IPEvProfil>();

        //Erzeuge IPEa-Liste
        List<IPEaProfil> ipeaListe = new List<IPEaProfil>();

        //Erzeuge IPE750-Liste
        List<IPE750Profil> ipe750Liste = new List<IPE750Profil>();

        //Erzeuge I-Liste
        List<Iprofil> iListe = new List<Iprofil>();

        //Erzeuge HEA-Liste
        List<HEAprofil> heaListe = new List<HEAprofil>();

        //Erzeuge HEAA-Liste
        List<HEAAprofil> heaaliste = new List<HEAAprofil>();

        //Erzeuge HEB-Liste
        List<HEBprofil> hebListe = new List<HEBprofil>();

        //Erzeuge HEM-Liste
        List<HEMprofil> hemListe = new List<HEMprofil>();

        //Erzeuge HL-Liste
        List<HLprofil> hlListe = new List<HLprofil>();

        //Erzeuge HE-Liste
        List<HEprofil> heliste = new List<HEprofil>();

        //Erzeuge HD-Liste
        List<HDprofil> hdListe = new List<HDprofil>();

        //Erzeuge HP-Liste 
        List<HPprofil> hpListe = new List<HPprofil>();

        //Erzeuge U-Liste
        List<Uprofil> uListe = new List<Uprofil>();

        //Erzeuge UPE-Liste
        List<UPEprofil> upeListe = new List<UPEprofil>();

        //Erzeuge UAP-Liste
        List<UAPprofil> uapListe = new List<UAPprofil>();

        //Erzeuge Ungleichschenklige-Liste
        List<Winkelungleichprofil> ungleichListe = new List<Winkelungleichprofil>();

        //Erzeuge Gleichschenklige-Liste
        List<Winkelgleichprofil> gleicheListe = new List<Winkelgleichprofil>();

        //Erzuege Kreisförmige-Hohlprofile-Liste
        List<Kreis> kreisListe = new List<Kreis>();

        //Erzeuge Quadratische-Hohlprofile-Liste
        List<Quadrat> quadratListe = new List<Quadrat>();

        //Erzeuge Rechteckige-Hohlprofile-Liste
        List<Rechteck> rechteckListe = new List<Rechteck>();

        //Erzeuge Kastenträger-Liste
        List<Kasten> kastenListe = new List<Kasten>();

        //Erzeuge I-Träger-Liste
        List<Itrager> itragerListe = new List<Itrager>();

        #endregion

        public Page1()
        {
            InitializeComponent();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            //Drop-Down-Listen standardmäßig setzen
            #region
            Hauptbauteilart.SelectedIndex = 0;
            //Bauteilart.SelectedIndex = 6;
            //ProfilartGenau.SelectedIndex = 4;
            Stahlgüte.SelectedIndex = 0;
            #endregion

            //Eingegebene Daten bleiben beim Wechseln der Seite bestehen: 
            #region
            if (Datenspeicher.LängeBauteilWert != null)
            {
                LängeBauteil.Text = Datenspeicher.LängeBauteilWert;
            }

            if (Datenspeicher.ProfilartIndex >= 0 && Datenspeicher.ProfilartIndex < ProfilartGenau.Items.Count)
            {
                ProfilartGenau.SelectedIndex = Datenspeicher.ProfilartIndex;
            }

            if (Datenspeicher.Bauteilart1 != null)
            {
                Bauteilart.SelectedItem = Datenspeicher.Bauteilart1;
            }

            if (Datenspeicher.Stahlpreis != null)
            {
                Stahlpreis.Text = Datenspeicher.Stahlpreis;
            }

            if (Datenspeicher.Stückzahl != null)
            {
                Stückzahl.Text = Datenspeicher.Stückzahl;
            }

            if (Datenspeicher.Gewicht != null)
            {
                Gewicht.Text = Datenspeicher.Gewicht;
            }

            if (Datenspeicher.Bauteillänge1 != null)
            {
                Bauteillänge1.Text = Datenspeicher.Bauteillänge1;
            }

            if (Datenspeicher.Bauteilbreite2 != null)
            {
                Bauteilbreite2.Text = Datenspeicher.Bauteilbreite2;
            }

            if (Datenspeicher.Flanschbreite3 != null)
            {
                Flanschbreite3.Text = Datenspeicher.Flanschbreite3;
            }

            if (Datenspeicher.Bauteilhöhe4 != null)
            {
                Bauteilhöhe4.Text = Datenspeicher.Bauteilhöhe4;
            }

            if (Datenspeicher.Projektnummer != null)
            {
                Projektnummer.Text = Datenspeicher.Projektnummer;
            }

            if (Datenspeicher.Bauvorhaben != null)
            {
                Bauvorhaben.Text = Datenspeicher.Bauvorhaben;
            }

            if (Datenspeicher.UmfangBauteil != null)
            {
                UmfangBauteil.Text = Datenspeicher.UmfangBauteil;
            }

            #endregion

            //Alle erzeugten Listen mit den jeweiligen Profilen befüllen
            #region
            Verzinken.ItemsSource = verzinkenListe;

            verzinkenListe.Add(new JaNein("Ja"));
            verzinkenListe.Add(new JaNein("Nein"));

            Stahlgüte.ItemsSource = stahlgüteListe;

            stahlgüteListe.Add(new Stahlgüteclass("S 235"));
            stahlgüteListe.Add(new Stahlgüteclass("S 355"));
            stahlgüteListe.Add(new Stahlgüteclass("S 460"));

            Bauteilart.ItemsSource = hohlprofilListe;

            hohlprofilListe.Add(new Hohlprofil("Kreisförmige Hohlprofile"));
            hohlprofilListe.Add(new Hohlprofil("Quadratische Hohlprofile"));
            hohlprofilListe.Add(new Hohlprofil("Rechteckige Hohlprofile"));


            Bauteilart.ItemsSource = zusamengesetzteListe;

            zusamengesetzteListe.Add(new Zusammengesetzte("I-Träger"));
            zusamengesetzteListe.Add(new Zusammengesetzte("Kastenträger"));


            Bauteilart.ItemsSource = bauteileListe;

            //Befülle Liste mit Objekten für Walzprofile
            bauteileListe.Add(new Bauteil("IPE-Profil"));
            bauteileListe.Add(new Bauteil("IPEa-Profil"));
            bauteileListe.Add(new Bauteil("IPEo-Profil"));
            bauteileListe.Add(new Bauteil("IPEv-Profil"));
            bauteileListe.Add(new Bauteil("IPE 750-Profil"));
            bauteileListe.Add(new Bauteil("HEAA-Profil"));
            bauteileListe.Add(new Bauteil("HEA-Profil"));
            bauteileListe.Add(new Bauteil("HEB-Profil"));
            bauteileListe.Add(new Bauteil("HEM-Profil"));
            bauteileListe.Add(new Bauteil("I-Profil"));
            bauteileListe.Add(new Bauteil("U-Profil"));
            bauteileListe.Add(new Bauteil("UPE-Profil"));
            bauteileListe.Add(new Bauteil("UAP-Profil"));
            bauteileListe.Add(new Bauteil("Winkel (gleichschenklig)"));
            bauteileListe.Add(new Bauteil("Winkel (ungleichschenklig)"));
            //bauteileListe.Add(new Bauteil("HSL-Profil"));
            //bauteileListe.Add(new Bauteil("HEC-Profil"));
            bauteileListe.Add(new Bauteil("HE-Profil"));
            bauteileListe.Add(new Bauteil("HL-Profil"));
            bauteileListe.Add(new Bauteil("HD-Profil"));
            bauteileListe.Add(new Bauteil("HP-Profil"));


            //Walzprofile

            ProfilartGenau.ItemsSource = ipeListe;
            #region
            ipeListe.Add(new IPEProfil("IPE 80"));
            ipeListe.Add(new IPEProfil("IPE 100"));
            ipeListe.Add(new IPEProfil("IPE 120"));
            ipeListe.Add(new IPEProfil("IPE 140"));
            ipeListe.Add(new IPEProfil("IPE 160"));
            ipeListe.Add(new IPEProfil("IPE 180"));
            ipeListe.Add(new IPEProfil("IPE 200"));
            ipeListe.Add(new IPEProfil("IPE 220"));
            ipeListe.Add(new IPEProfil("IPE 240"));
            ipeListe.Add(new IPEProfil("IPE 270"));
            ipeListe.Add(new IPEProfil("IPE 300"));
            ipeListe.Add(new IPEProfil("IPE 330"));
            ipeListe.Add(new IPEProfil("IPE 360"));
            ipeListe.Add(new IPEProfil("IPE 400"));
            ipeListe.Add(new IPEProfil("IPE 450"));
            ipeListe.Add(new IPEProfil("IPE 500"));
            ipeListe.Add(new IPEProfil("IPE 550"));
            ipeListe.Add(new IPEProfil("IPE 600"));
            #endregion

            ProfilartGenau.ItemsSource = ipeoListe;
            #region
            ipeoListe.Add(new IPEoProfil("IPEo 180"));
            ipeoListe.Add(new IPEoProfil("IPEo 200"));
            ipeoListe.Add(new IPEoProfil("IPEo 220"));
            ipeoListe.Add(new IPEoProfil("IPEo 240"));
            ipeoListe.Add(new IPEoProfil("IPEo 270"));
            ipeoListe.Add(new IPEoProfil("IPEo 300"));
            ipeoListe.Add(new IPEoProfil("IPEo 330"));
            ipeoListe.Add(new IPEoProfil("IPEo 360"));
            ipeoListe.Add(new IPEoProfil("IPEo 400"));
            ipeoListe.Add(new IPEoProfil("IPEo 450"));
            ipeoListe.Add(new IPEoProfil("IPEo 500"));
            ipeoListe.Add(new IPEoProfil("IPEo 550"));
            ipeoListe.Add(new IPEoProfil("IPEo 600"));
            #endregion

            ProfilartGenau.ItemsSource = ipevListe;
            #region
            ipevListe.Add(new IPEvProfil("IPEv 400"));
            ipevListe.Add(new IPEvProfil("IPEv 450"));
            ipevListe.Add(new IPEvProfil("IPEv 500"));
            ipevListe.Add(new IPEvProfil("IPEv 550"));
            ipevListe.Add(new IPEvProfil("IPEv 600"));
            #endregion

            ProfilartGenau.ItemsSource = ipeaListe;
            #region
            ipeaListe.Add(new IPEaProfil("IPEa 80"));
            ipeaListe.Add(new IPEaProfil("IPEa 100"));
            ipeaListe.Add(new IPEaProfil("IPEa 120"));
            ipeaListe.Add(new IPEaProfil("IPEa 140"));
            ipeaListe.Add(new IPEaProfil("IPEa 160"));
            ipeaListe.Add(new IPEaProfil("IPEa 180"));
            ipeaListe.Add(new IPEaProfil("IPEa 200"));
            ipeaListe.Add(new IPEaProfil("IPEa 220"));
            ipeaListe.Add(new IPEaProfil("IPEa 240"));
            ipeaListe.Add(new IPEaProfil("IPEa 270"));
            ipeaListe.Add(new IPEaProfil("IPEa 300"));
            ipeaListe.Add(new IPEaProfil("IPEa 330"));
            ipeaListe.Add(new IPEaProfil("IPEa 360"));
            ipeaListe.Add(new IPEaProfil("IPEa 400"));
            ipeaListe.Add(new IPEaProfil("IPEa 450"));
            ipeaListe.Add(new IPEaProfil("IPEa 500"));
            ipeaListe.Add(new IPEaProfil("IPEa 550"));
            ipeaListe.Add(new IPEaProfil("IPEa 600"));
            #endregion

            ProfilartGenau.ItemsSource = ipe750Liste;
            #region
            ipe750Liste.Add(new IPE750Profil("IPE 750 x 137"));
            ipe750Liste.Add(new IPE750Profil("IPE 750 x 147"));
            ipe750Liste.Add(new IPE750Profil("IPE 750 x 161"));
            ipe750Liste.Add(new IPE750Profil("IPE 750 x 173"));
            ipe750Liste.Add(new IPE750Profil("IPE 750 x 185"));
            ipe750Liste.Add(new IPE750Profil("IPE 750 x 196"));
            ipe750Liste.Add(new IPE750Profil("IPE 750 x 210"));
            ipe750Liste.Add(new IPE750Profil("IPE 750 x 222"));
            #endregion

            ProfilartGenau.ItemsSource = iListe;
            #region
            iListe.Add(new Iprofil("I 80"));
            iListe.Add(new Iprofil("I 100"));
            iListe.Add(new Iprofil("I 120"));
            iListe.Add(new Iprofil("I 140"));
            iListe.Add(new Iprofil("I 160"));
            iListe.Add(new Iprofil("I 180"));
            iListe.Add(new Iprofil("I 200"));
            iListe.Add(new Iprofil("I 220"));
            iListe.Add(new Iprofil("I 240"));
            iListe.Add(new Iprofil("I 260"));
            iListe.Add(new Iprofil("I 280"));
            iListe.Add(new Iprofil("I 300"));
            iListe.Add(new Iprofil("I 320"));
            iListe.Add(new Iprofil("I 340"));
            iListe.Add(new Iprofil("I 360"));
            iListe.Add(new Iprofil("I 380"));
            iListe.Add(new Iprofil("I 400"));
            iListe.Add(new Iprofil("I 450"));
            iListe.Add(new Iprofil("I 500"));
            iListe.Add(new Iprofil("I 550"));
            iListe.Add(new Iprofil("I 600"));
            #endregion

            ProfilartGenau.ItemsSource = heaListe;
            #region
            heaListe.Add(new HEAprofil("HEA 100"));
            heaListe.Add(new HEAprofil("HEA 120"));
            heaListe.Add(new HEAprofil("HEA 140"));
            heaListe.Add(new HEAprofil("HEA 160"));
            heaListe.Add(new HEAprofil("HEA 180"));
            heaListe.Add(new HEAprofil("HEA 200"));
            heaListe.Add(new HEAprofil("HEA 220"));
            heaListe.Add(new HEAprofil("HEA 240"));
            heaListe.Add(new HEAprofil("HEA 260"));
            heaListe.Add(new HEAprofil("HEA 280"));
            heaListe.Add(new HEAprofil("HEA 300"));
            heaListe.Add(new HEAprofil("HEA 320"));
            heaListe.Add(new HEAprofil("HEA 340"));
            heaListe.Add(new HEAprofil("HEA 360"));
            heaListe.Add(new HEAprofil("HEA 400"));
            heaListe.Add(new HEAprofil("HEA 450"));
            heaListe.Add(new HEAprofil("HEA 500"));
            heaListe.Add(new HEAprofil("HEA 550"));
            heaListe.Add(new HEAprofil("HEA 600"));
            heaListe.Add(new HEAprofil("HEA 650"));
            heaListe.Add(new HEAprofil("HEA 700"));
            heaListe.Add(new HEAprofil("HEA 800"));
            heaListe.Add(new HEAprofil("HEA 900"));
            heaListe.Add(new HEAprofil("HEA 1000"));
            #endregion

            ProfilartGenau.ItemsSource = heaaliste;
            #region
            heaaliste.Add(new HEAAprofil("HEAA 100"));
            heaaliste.Add(new HEAAprofil("HEAA 120"));
            heaaliste.Add(new HEAAprofil("HEAA 140"));
            heaaliste.Add(new HEAAprofil("HEAA 160"));
            heaaliste.Add(new HEAAprofil("HEAA 180"));
            heaaliste.Add(new HEAAprofil("HEAA 200"));
            heaaliste.Add(new HEAAprofil("HEAA 220"));
            heaaliste.Add(new HEAAprofil("HEAA 240"));
            heaaliste.Add(new HEAAprofil("HEAA 260"));
            heaaliste.Add(new HEAAprofil("HEAA 280"));
            heaaliste.Add(new HEAAprofil("HEAA 300"));
            heaaliste.Add(new HEAAprofil("HEAA 320"));
            heaaliste.Add(new HEAAprofil("HEAA 340"));
            heaaliste.Add(new HEAAprofil("HEAA 360"));
            heaaliste.Add(new HEAAprofil("HEAA 400"));
            heaaliste.Add(new HEAAprofil("HEAA 450"));
            heaaliste.Add(new HEAAprofil("HEAA 500"));
            heaaliste.Add(new HEAAprofil("HEAA 550"));
            heaaliste.Add(new HEAAprofil("HEAA 600"));
            heaaliste.Add(new HEAAprofil("HEAA 650"));
            heaaliste.Add(new HEAAprofil("HEAA 700"));
            heaaliste.Add(new HEAAprofil("HEAA 800"));
            heaaliste.Add(new HEAAprofil("HEAA 900"));
            heaaliste.Add(new HEAAprofil("HEAA 1000"));
            #endregion

            ProfilartGenau.ItemsSource = hebListe;
            #region
            hebListe.Add(new HEBprofil("HEB 100"));
            hebListe.Add(new HEBprofil("HEB 120"));
            hebListe.Add(new HEBprofil("HEB 140"));
            hebListe.Add(new HEBprofil("HEB 160"));
            hebListe.Add(new HEBprofil("HEB 180"));
            hebListe.Add(new HEBprofil("HEB 200"));
            hebListe.Add(new HEBprofil("HEB 220"));
            hebListe.Add(new HEBprofil("HEB 240"));
            hebListe.Add(new HEBprofil("HEB 260"));
            hebListe.Add(new HEBprofil("HEB 280"));
            hebListe.Add(new HEBprofil("HEB 300"));
            hebListe.Add(new HEBprofil("HEB 320"));
            hebListe.Add(new HEBprofil("HEB 340"));
            hebListe.Add(new HEBprofil("HEB 360"));
            hebListe.Add(new HEBprofil("HEB 400"));
            hebListe.Add(new HEBprofil("HEB 450"));
            hebListe.Add(new HEBprofil("HEB 500"));
            hebListe.Add(new HEBprofil("HEB 550"));
            hebListe.Add(new HEBprofil("HEB 600"));
            hebListe.Add(new HEBprofil("HEB 650"));
            hebListe.Add(new HEBprofil("HEB 700"));
            hebListe.Add(new HEBprofil("HEB 800"));
            hebListe.Add(new HEBprofil("HEB 900"));
            hebListe.Add(new HEBprofil("HEB 1000"));
            #endregion

            ProfilartGenau.ItemsSource = hemListe;
            #region
            hemListe.Add(new HEMprofil("HEM 100"));
            hemListe.Add(new HEMprofil("HEM 120"));
            hemListe.Add(new HEMprofil("HEM 140"));
            hemListe.Add(new HEMprofil("HEM 160"));
            hemListe.Add(new HEMprofil("HEM 180"));
            hemListe.Add(new HEMprofil("HEM 200"));
            hemListe.Add(new HEMprofil("HEM 220"));
            hemListe.Add(new HEMprofil("HEM 240"));
            hemListe.Add(new HEMprofil("HEM 260"));
            hemListe.Add(new HEMprofil("HEM 280"));
            hemListe.Add(new HEMprofil("HEM 300"));
            hemListe.Add(new HEMprofil("HEM 305"));
            hemListe.Add(new HEMprofil("HEM 320"));
            hemListe.Add(new HEMprofil("HEM 340"));
            hemListe.Add(new HEMprofil("HEM 360"));
            hemListe.Add(new HEMprofil("HEM 400"));
            hemListe.Add(new HEMprofil("HEM 450"));
            hemListe.Add(new HEMprofil("HEM 500"));
            hemListe.Add(new HEMprofil("HEM 550"));
            hemListe.Add(new HEMprofil("HEM 600"));
            hemListe.Add(new HEMprofil("HEM 650"));
            hemListe.Add(new HEMprofil("HEM 700"));
            hemListe.Add(new HEMprofil("HEM 800"));
            hemListe.Add(new HEMprofil("HEM 900"));
            hemListe.Add(new HEMprofil("HEM 1000"));
            #endregion

            ProfilartGenau.ItemsSource = heliste;
            #region
            heliste.Add(new HEprofil("HE 300 C"));
            heliste.Add(new HEprofil("HE 400 x 299"));
            heliste.Add(new HEprofil("HE 400 x 347"));
            heliste.Add(new HEprofil("HE 400 x 403"));
            heliste.Add(new HEprofil("HE 400 x 468"));
            heliste.Add(new HEprofil("HE 450 x 312"));
            heliste.Add(new HEprofil("HE 450 x 368"));
            heliste.Add(new HEprofil("HE 450 x 436"));
            heliste.Add(new HEprofil("HE 450 x 519"));
            heliste.Add(new HEprofil("HE 500 x 320"));
            heliste.Add(new HEprofil("HE 500 x 379"));
            heliste.Add(new HEprofil("HE 500 x 451"));
            heliste.Add(new HEprofil("HE 500 x 534"));
            heliste.Add(new HEprofil("HE 550 x 330"));
            heliste.Add(new HEprofil("HE 550 x 393"));
            heliste.Add(new HEprofil("HE 550 x 466"));
            heliste.Add(new HEprofil("HE 550 x 552"));
            heliste.Add(new HEprofil("HE 600 x 337"));
            heliste.Add(new HEprofil("HE 600 x 340"));
            heliste.Add(new HEprofil("HE 600 x 399"));
            heliste.Add(new HEprofil("HE 600 x 402"));
            heliste.Add(new HEprofil("HE 600 x 477"));
            heliste.Add(new HEprofil("HE 600 x 564"));
            heliste.Add(new HEprofil("HE 650 x 343"));
            heliste.Add(new HEprofil("HE 650 x 347"));
            heliste.Add(new HEprofil("HE 650 x 407"));
            heliste.Add(new HEprofil("HE 650 x 410"));
            heliste.Add(new HEprofil("HE 650 x 487"));
            heliste.Add(new HEprofil("HE 650 x 579"));
            heliste.Add(new HEprofil("HE 700 x 352"));
            heliste.Add(new HEprofil("HE 700 x 356"));
            heliste.Add(new HEprofil("HE 700 x 418"));
            heliste.Add(new HEprofil("HE 700 x 421"));
            heliste.Add(new HEprofil("HE 700 x 500"));
            heliste.Add(new HEprofil("HE 700 x 594"));
            heliste.Add(new HEprofil("HE 800 x 373"));
            heliste.Add(new HEprofil("HE 800 x 377"));
            heliste.Add(new HEprofil("HE 800 x 444"));
            heliste.Add(new HEprofil("HE 800 x 448"));
            heliste.Add(new HEprofil("HE 800 x 531"));
            heliste.Add(new HEprofil("HE 800 x 627"));
            heliste.Add(new HEprofil("HE 900 x 391"));
            heliste.Add(new HEprofil("HE 900 x 396"));
            heliste.Add(new HEprofil("HE 900 x 466"));
            heliste.Add(new HEprofil("HE 900 x 471"));
            heliste.Add(new HEprofil("HE 900 x 557"));
            heliste.Add(new HEprofil("HE 900 x 661"));
            heliste.Add(new HEprofil("HE 1000 x 249"));
            heliste.Add(new HEprofil("HE 1000 x 393"));
            heliste.Add(new HEprofil("HE 1000 x 415"));
            heliste.Add(new HEprofil("HE 1000 x 438"));
            heliste.Add(new HEprofil("HE 1000 x 494"));
            heliste.Add(new HEprofil("HE 1000 x 584"));
            heliste.Add(new HEprofil("HE 1000 x 694"));
            heliste.Add(new HEprofil("HE 1100 A"));
            heliste.Add(new HEprofil("HE 1100 B"));
            heliste.Add(new HEprofil("HE 1100 M"));
            heliste.Add(new HEprofil("HE 1100 R"));
            #endregion

            ProfilartGenau.ItemsSource = hlListe;
            #region
            hlListe.Add(new HLprofil("HSL 100"));
            hlListe.Add(new HLprofil("HL 920 x 342"));
            hlListe.Add(new HLprofil("HL 920 x 344"));
            hlListe.Add(new HLprofil("HL 920 x 365"));
            hlListe.Add(new HLprofil("HL 920 x 368"));
            hlListe.Add(new HLprofil("HL 920 x 387"));
            hlListe.Add(new HLprofil("HL 920 x 390"));
            hlListe.Add(new HLprofil("HL 920 x 417"));
            hlListe.Add(new HLprofil("HL 920 x 420"));
            hlListe.Add(new HLprofil("HL 920 x 446"));
            hlListe.Add(new HLprofil("HL 920 x 449"));
            hlListe.Add(new HLprofil("HL 920 x 488"));
            hlListe.Add(new HLprofil("HL 920 x 491"));
            hlListe.Add(new HLprofil("HL 920 x 534"));
            hlListe.Add(new HLprofil("HL 920 x 537"));
            hlListe.Add(new HLprofil("HL 920 x 585"));
            hlListe.Add(new HLprofil("HL 920 x 588"));
            hlListe.Add(new HLprofil("HL 920 x 653"));
            hlListe.Add(new HLprofil("HL 920 x 656"));
            hlListe.Add(new HLprofil("HL 920 x 725"));
            hlListe.Add(new HLprofil("HL 920 x 784"));
            hlListe.Add(new HLprofil("HL 920 x 787"));
            hlListe.Add(new HLprofil("HL 920 x 967"));
            hlListe.Add(new HLprofil("HL 920 x 970"));
            hlListe.Add(new HLprofil("HL 920 x 1077"));
            hlListe.Add(new HLprofil("HL 920 x 1194"));
            hlListe.Add(new HLprofil("HL 920 x 1269"));
            hlListe.Add(new HLprofil("HL 920 x 1377"));
            hlListe.Add(new HLprofil("HL 1000 AA"));
            hlListe.Add(new HLprofil("HL 1000 A"));
            hlListe.Add(new HLprofil("HL 1000 B"));
            hlListe.Add(new HLprofil("HL 1000 M"));
            hlListe.Add(new HLprofil("HL 1000 x 443"));
            hlListe.Add(new HLprofil("HL 1000 x 477"));
            hlListe.Add(new HLprofil("HL 1000 x 483"));
            hlListe.Add(new HLprofil("HL 1000 x 539"));
            hlListe.Add(new HLprofil("HL 1000 x 554"));
            hlListe.Add(new HLprofil("HL 1000 x 591"));
            hlListe.Add(new HLprofil("HL 1000 x 642"));
            hlListe.Add(new HLprofil("HL 1000 x 748"));
            hlListe.Add(new HLprofil("HL 1000 x 883"));
            hlListe.Add(new HLprofil("HL 1000 x 976"));
            hlListe.Add(new HLprofil("HL 1100 A"));
            hlListe.Add(new HLprofil("HL 1100 B"));
            hlListe.Add(new HLprofil("HL 1100 M"));
            hlListe.Add(new HLprofil("HL 1100 R"));
            hlListe.Add(new HLprofil("HL 1100 x 548"));
            hlListe.Add(new HLprofil("HL 1100 x 607"));
            #endregion

            ProfilartGenau.ItemsSource = uListe;
            #region
            uListe.Add(new Uprofil("UE 50"));
            uListe.Add(new Uprofil("UE 65"));
            uListe.Add(new Uprofil("UE 80"));
            uListe.Add(new Uprofil("UE 100"));
            uListe.Add(new Uprofil("UE 120"));
            uListe.Add(new Uprofil("UE 140"));
            uListe.Add(new Uprofil("UE 160"));
            uListe.Add(new Uprofil("UE 180"));
            uListe.Add(new Uprofil("UE 200"));
            uListe.Add(new Uprofil("UE 220"));
            uListe.Add(new Uprofil("UE 240"));
            uListe.Add(new Uprofil("UE 270"));
            uListe.Add(new Uprofil("UE 300"));
            #endregion

            ProfilartGenau.ItemsSource = upeListe;
            #region
            upeListe.Add(new UPEprofil("UPE 80"));
            upeListe.Add(new UPEprofil("UPE 100"));
            upeListe.Add(new UPEprofil("UPE 120"));
            upeListe.Add(new UPEprofil("UPE 140"));
            upeListe.Add(new UPEprofil("UPE 160"));
            upeListe.Add(new UPEprofil("UPE 180"));
            upeListe.Add(new UPEprofil("UPE 200"));
            upeListe.Add(new UPEprofil("UPE 220"));
            upeListe.Add(new UPEprofil("UPE 240"));
            upeListe.Add(new UPEprofil("UPE 270"));
            upeListe.Add(new UPEprofil("UPE 300"));
            upeListe.Add(new UPEprofil("UPE 330"));
            upeListe.Add(new UPEprofil("UPE 360"));
            upeListe.Add(new UPEprofil("UPE 400"));
            #endregion

            ProfilartGenau.ItemsSource = uapListe;
            #region
            uapListe.Add(new UAPprofil("UAP 80"));
            uapListe.Add(new UAPprofil("UAP 100"));
            uapListe.Add(new UAPprofil("UAP 130"));
            uapListe.Add(new UAPprofil("UAP 150"));
            uapListe.Add(new UAPprofil("UAP 175"));
            uapListe.Add(new UAPprofil("UAP 200"));
            uapListe.Add(new UAPprofil("UAP 220"));
            uapListe.Add(new UAPprofil("UAP 250"));
            uapListe.Add(new UAPprofil("UAP 300"));
            #endregion

            ProfilartGenau.ItemsSource = hdListe;
            #region
            hdListe.Add(new HDprofil("HD 210 x 46"));
            hdListe.Add(new HDprofil("HD 210 x 52"));
            hdListe.Add(new HDprofil("HD 210 x 69"));
            hdListe.Add(new HDprofil("HD 210 x 71"));
            hdListe.Add(new HDprofil("HD 210 x 87"));
            hdListe.Add(new HDprofil("HD 210 x 100"));
            hdListe.Add(new HDprofil("HD 260 x 54,1"));
            hdListe.Add(new HDprofil("HD 260 x 68,2"));
            hdListe.Add(new HDprofil("HD 260 x 93"));
            hdListe.Add(new HDprofil("HD 260 x 114"));
            hdListe.Add(new HDprofil("HD 260 x 142"));
            hdListe.Add(new HDprofil("HD 260 x 172"));
            hdListe.Add(new HDprofil("HD 260 x 225"));
            hdListe.Add(new HDprofil("HD 260 x 299"));
            hdListe.Add(new HDprofil("HD 310 x 143"));
            hdListe.Add(new HDprofil("HD 310 x 179"));
            hdListe.Add(new HDprofil("HD 310 x 227"));
            hdListe.Add(new HDprofil("HD 310 x 283"));
            hdListe.Add(new HDprofil("HD 310 x 343"));
            hdListe.Add(new HDprofil("HD 310 x 415"));
            hdListe.Add(new HDprofil("HD 310 x 454"));
            hdListe.Add(new HDprofil("HD 310 x 500"));
            hdListe.Add(new HDprofil("HD 320 x 74,2"));
            hdListe.Add(new HDprofil("HD 320 x 97,6"));
            hdListe.Add(new HDprofil("HD 320 x 127"));
            hdListe.Add(new HDprofil("HD 320 x 158"));
            hdListe.Add(new HDprofil("HD 320 x 198"));
            hdListe.Add(new HDprofil("HD 320 x 245"));
            hdListe.Add(new HDprofil("HD 320 x 300"));
            hdListe.Add(new HDprofil("HD 320 x 368"));
            hdListe.Add(new HDprofil("HD 320 x 451"));
            hdListe.Add(new HDprofil("HD 360 x 134"));
            hdListe.Add(new HDprofil("HD 360 x 147"));
            hdListe.Add(new HDprofil("HD 360 x 162"));
            hdListe.Add(new HDprofil("HD 360 x 179"));
            hdListe.Add(new HDprofil("HD 360 x 196"));
            hdListe.Add(new HDprofil("HD 400 x 187"));
            hdListe.Add(new HDprofil("HD 400 x 216"));
            hdListe.Add(new HDprofil("HD 400 x 237"));
            hdListe.Add(new HDprofil("HD 400 x 262"));
            hdListe.Add(new HDprofil("HD 400 x 287"));
            hdListe.Add(new HDprofil("HD 400 x 314"));
            hdListe.Add(new HDprofil("HD 400 x 347"));
            hdListe.Add(new HDprofil("HD 400 x 382"));
            hdListe.Add(new HDprofil("HD 400 x 421"));
            hdListe.Add(new HDprofil("HD 400 x 463"));
            hdListe.Add(new HDprofil("HD 400 x 509"));
            hdListe.Add(new HDprofil("HD 400 x 551"));
            hdListe.Add(new HDprofil("HD 400 x 592"));
            hdListe.Add(new HDprofil("HD 400 x 635"));
            hdListe.Add(new HDprofil("HD 400 x 677"));
            hdListe.Add(new HDprofil("HD 400 x 744"));
            hdListe.Add(new HDprofil("HD 400 x 818"));
            hdListe.Add(new HDprofil("HD 400 x 900"));
            hdListe.Add(new HDprofil("HD 400 x 990"));
            hdListe.Add(new HDprofil("HD 400 x 1086"));
            hdListe.Add(new HDprofil("HD 400 x 1202"));
            hdListe.Add(new HDprofil("HD 400 x 1299"));
            #endregion

            ProfilartGenau.ItemsSource = hpListe;
            #region
            hpListe.Add(new HPprofil("HP 200 x 43"));
            hpListe.Add(new HPprofil("HP 200 x 53"));
            hpListe.Add(new HPprofil("HP 220 x 57"));
            hpListe.Add(new HPprofil("HP 250 x 53"));
            hpListe.Add(new HPprofil("HP 250 x 62"));
            hpListe.Add(new HPprofil("HP 250 x 85"));
            hpListe.Add(new HPprofil("HP 260 x 75"));
            hpListe.Add(new HPprofil("HP 260 x 87"));
            hpListe.Add(new HPprofil("HP 305 x 79"));
            hpListe.Add(new HPprofil("HP 305 x 88"));
            hpListe.Add(new HPprofil("HP 305 x 95"));
            hpListe.Add(new HPprofil("HP 305 x 110"));
            hpListe.Add(new HPprofil("HP 305 x 126"));
            hpListe.Add(new HPprofil("HP 305 x 149"));
            hpListe.Add(new HPprofil("HP 305 x 180"));
            hpListe.Add(new HPprofil("HP 305 x 186"));
            hpListe.Add(new HPprofil("HP 305 x 223"));
            hpListe.Add(new HPprofil("HP 310 x 64"));
            hpListe.Add(new HPprofil("HP 310 x 79"));
            hpListe.Add(new HPprofil("HP 310 x 93"));
            hpListe.Add(new HPprofil("HP 310 x 110"));
            hpListe.Add(new HPprofil("HP 310 x 125"));
            hpListe.Add(new HPprofil("HP 320 x 88"));
            hpListe.Add(new HPprofil("HP 320 x 103"));
            hpListe.Add(new HPprofil("HP 320 x 117"));
            hpListe.Add(new HPprofil("HP 320 x 147"));
            hpListe.Add(new HPprofil("HP 320 x 184"));
            hpListe.Add(new HPprofil("HP 360 x 84"));
            hpListe.Add(new HPprofil("HP 360 x 109"));
            hpListe.Add(new HPprofil("HP 360 x 133"));
            hpListe.Add(new HPprofil("HP 360 x 152"));
            hpListe.Add(new HPprofil("HP 360 x 174"));
            hpListe.Add(new HPprofil("HP 360 x 180"));
            hpListe.Add(new HPprofil("HP 370 x 84"));
            hpListe.Add(new HPprofil("HP 370 x 108"));
            hpListe.Add(new HPprofil("HP 370 x 132"));
            hpListe.Add(new HPprofil("HP 370 x 152"));
            hpListe.Add(new HPprofil("HP 370 x 174"));
            hpListe.Add(new HPprofil("HP 400 x 122"));
            hpListe.Add(new HPprofil("HP 400 x 140"));
            hpListe.Add(new HPprofil("HP 400 x 158"));
            hpListe.Add(new HPprofil("HP 400 x 176"));
            hpListe.Add(new HPprofil("HP 400 x 194"));
            hpListe.Add(new HPprofil("HP 400 x 213"));
            hpListe.Add(new HPprofil("HP 400 x 231"));
            hpListe.Add(new HPprofil("HP 410 x 104"));
            hpListe.Add(new HPprofil("HP 410 x 122"));
            hpListe.Add(new HPprofil("HP 410 x 140"));
            hpListe.Add(new HPprofil("HP 410 x 158"));
            hpListe.Add(new HPprofil("HP 410 x 176"));
            hpListe.Add(new HPprofil("HP 410 x 194"));
            hpListe.Add(new HPprofil("HP 410 x 213"));
            hpListe.Add(new HPprofil("HP 410 x 231"));
            #endregion

            ProfilartGenau.ItemsSource = ungleichListe;
            #region
            ungleichListe.Add(new Winkelungleichprofil("L 30 x 20 x 3"));
            ungleichListe.Add(new Winkelungleichprofil("L 30 x 20 x 4"));
            ungleichListe.Add(new Winkelungleichprofil("L 40 x 20 x 3"));
            ungleichListe.Add(new Winkelungleichprofil("L 40 x 20 x 4"));
            ungleichListe.Add(new Winkelungleichprofil("L 40 x 25 x 4"));
            ungleichListe.Add(new Winkelungleichprofil("L 45 x 30 x 3"));
            ungleichListe.Add(new Winkelungleichprofil("L 45 x 30 x 4"));
            ungleichListe.Add(new Winkelungleichprofil("L 45 x 30 x 5"));
            ungleichListe.Add(new Winkelungleichprofil("L 50 x 30 x 4"));
            ungleichListe.Add(new Winkelungleichprofil("L 50 x 30 x 4"));
            ungleichListe.Add(new Winkelungleichprofil("L 50 x 30 x 5"));
            ungleichListe.Add(new Winkelungleichprofil("L 50 x 40 x 4"));
            ungleichListe.Add(new Winkelungleichprofil("L 50 x 40 x 5"));
            ungleichListe.Add(new Winkelungleichprofil("L 60 x 30 x 5"));
            ungleichListe.Add(new Winkelungleichprofil("L 60 x 40 x 5"));
            ungleichListe.Add(new Winkelungleichprofil("L 60 x 40 x 6"));
            ungleichListe.Add(new Winkelungleichprofil("L 60 x 40 x 7"));
            ungleichListe.Add(new Winkelungleichprofil("L 65 x 50 x 5"));
            ungleichListe.Add(new Winkelungleichprofil("L 65 x 50 x 7"));
            ungleichListe.Add(new Winkelungleichprofil("L 65 x 50 x 9"));
            ungleichListe.Add(new Winkelungleichprofil("L 70 x 50 x 6"));
            ungleichListe.Add(new Winkelungleichprofil("L 75 x 50 x 7"));
            ungleichListe.Add(new Winkelungleichprofil("L 75 x 50 x 8"));
            ungleichListe.Add(new Winkelungleichprofil("L 75 x 50 x 9"));
            ungleichListe.Add(new Winkelungleichprofil("L 75 x 55 x 5"));
            ungleichListe.Add(new Winkelungleichprofil("L 75 x 55 x 7"));
            ungleichListe.Add(new Winkelungleichprofil("L 75 x 55 x 9"));
            ungleichListe.Add(new Winkelungleichprofil("L 80 x 40 x 6"));
            ungleichListe.Add(new Winkelungleichprofil("L 80 x 40 x 8"));
            ungleichListe.Add(new Winkelungleichprofil("L 80 x 60 x 7"));
            ungleichListe.Add(new Winkelungleichprofil("L 80 x 65 x 8"));
            ungleichListe.Add(new Winkelungleichprofil("L 90 x 60 x 6"));
            ungleichListe.Add(new Winkelungleichprofil("L 90 x 60 x 8"));
            ungleichListe.Add(new Winkelungleichprofil("L 100 x 50 x 6"));
            ungleichListe.Add(new Winkelungleichprofil("L 100 x 50 x 8"));
            ungleichListe.Add(new Winkelungleichprofil("L 100 x 50 x 10"));
            ungleichListe.Add(new Winkelungleichprofil("L 100 x 65 x 7"));
            ungleichListe.Add(new Winkelungleichprofil("L 100 x 65 x 8"));
            ungleichListe.Add(new Winkelungleichprofil("L 100 x 65 x 9"));
            ungleichListe.Add(new Winkelungleichprofil("L 100 x 65 x 10"));
            ungleichListe.Add(new Winkelungleichprofil("L 100 x 65 x 11"));
            ungleichListe.Add(new Winkelungleichprofil("L 100 x 75 x 7"));
            ungleichListe.Add(new Winkelungleichprofil("L 100 x 75 x 8"));
            ungleichListe.Add(new Winkelungleichprofil("L 100 x 75 x 9"));
            ungleichListe.Add(new Winkelungleichprofil("L 100 x 75 x 10"));
            ungleichListe.Add(new Winkelungleichprofil("L 100 x 75 x 11"));
            ungleichListe.Add(new Winkelungleichprofil("L 100 x 75 x 12"));
            ungleichListe.Add(new Winkelungleichprofil("L 120 x 80 x 8"));
            ungleichListe.Add(new Winkelungleichprofil("L 120 x 80 x 10"));
            ungleichListe.Add(new Winkelungleichprofil("L 120 x 80 x 12"));
            ungleichListe.Add(new Winkelungleichprofil("L 125 x 75 x 8"));
            ungleichListe.Add(new Winkelungleichprofil("L 125 x 75 x 10"));
            ungleichListe.Add(new Winkelungleichprofil("L 125 x 75 x 12"));
            ungleichListe.Add(new Winkelungleichprofil("L 130 x 65 x 8"));
            ungleichListe.Add(new Winkelungleichprofil("L 130 x 65 x 10"));
            ungleichListe.Add(new Winkelungleichprofil("L 130 x 65 x 12"));
            ungleichListe.Add(new Winkelungleichprofil("L 130 x 65 x 12"));
            ungleichListe.Add(new Winkelungleichprofil("L 130 x 90 x 12"));
            ungleichListe.Add(new Winkelungleichprofil("L 135 x 65 x 8"));
            ungleichListe.Add(new Winkelungleichprofil("L 135 x 65 x 10"));
            ungleichListe.Add(new Winkelungleichprofil("L 150 x 75 x 9"));
            ungleichListe.Add(new Winkelungleichprofil("L 150 x 75 x 10"));
            ungleichListe.Add(new Winkelungleichprofil("L 150 x 75 x 11"));
            ungleichListe.Add(new Winkelungleichprofil("L 150 x 75 x 12"));
            ungleichListe.Add(new Winkelungleichprofil("L 150 x 75 x 15"));
            ungleichListe.Add(new Winkelungleichprofil("L 150 x 90 x 10"));
            ungleichListe.Add(new Winkelungleichprofil("L 150 x 90 x 11"));
            ungleichListe.Add(new Winkelungleichprofil("L 150 x 90 x 12"));
            ungleichListe.Add(new Winkelungleichprofil("L 150 x 90 x 15"));
            ungleichListe.Add(new Winkelungleichprofil("L 150 x 100 x 10"));
            ungleichListe.Add(new Winkelungleichprofil("L 150 x 100 x 12"));
            ungleichListe.Add(new Winkelungleichprofil("L 150 x 100 x 14"));
            ungleichListe.Add(new Winkelungleichprofil("L 160 x 80 x 10"));
            ungleichListe.Add(new Winkelungleichprofil("L 160 x 80 x 12"));
            ungleichListe.Add(new Winkelungleichprofil("L 180 x 90 x 10"));
            ungleichListe.Add(new Winkelungleichprofil("L 180 x 90 x 12"));
            ungleichListe.Add(new Winkelungleichprofil("L 200 x 100 x 10"));
            ungleichListe.Add(new Winkelungleichprofil("L 200 x 100 x 12"));
            ungleichListe.Add(new Winkelungleichprofil("L 200 x 100 x 14"));
            ungleichListe.Add(new Winkelungleichprofil("L 200 x 100 x 15"));
            ungleichListe.Add(new Winkelungleichprofil("L 200 x 150 x 12"));
            ungleichListe.Add(new Winkelungleichprofil("L 200 x 150 x 15"));

            #endregion

            ProfilartGenau.ItemsSource = gleicheListe;
            #region
            gleicheListe.Add(new Winkelgleichprofil("L 20 x 3"));
            gleicheListe.Add(new Winkelgleichprofil("L 25 x 3"));
            gleicheListe.Add(new Winkelgleichprofil("L 25 x 4"));
            gleicheListe.Add(new Winkelgleichprofil("L 30 x 3"));
            gleicheListe.Add(new Winkelgleichprofil("L 30 x 4"));
            gleicheListe.Add(new Winkelgleichprofil("L 30 x 5"));
            gleicheListe.Add(new Winkelgleichprofil("L 35 x 4"));
            gleicheListe.Add(new Winkelgleichprofil("L 35 x 5"));
            gleicheListe.Add(new Winkelgleichprofil("L 40 x 4"));
            gleicheListe.Add(new Winkelgleichprofil("L 40 x 5"));
            gleicheListe.Add(new Winkelgleichprofil("L 45 x 4"));
            gleicheListe.Add(new Winkelgleichprofil("L 45 x 4,5"));
            gleicheListe.Add(new Winkelgleichprofil("L 45 x 5"));
            gleicheListe.Add(new Winkelgleichprofil("L 50 x 4"));
            gleicheListe.Add(new Winkelgleichprofil("L 50 x 5"));
            gleicheListe.Add(new Winkelgleichprofil("L 50 x 6"));
            gleicheListe.Add(new Winkelgleichprofil("L 50 x 7"));
            gleicheListe.Add(new Winkelgleichprofil("L 55 x 6"));
            gleicheListe.Add(new Winkelgleichprofil("L 60 x 5"));
            gleicheListe.Add(new Winkelgleichprofil("L 60 x 6"));
            gleicheListe.Add(new Winkelgleichprofil("L 60 x 8"));
            gleicheListe.Add(new Winkelgleichprofil("L 65 x 7"));
            gleicheListe.Add(new Winkelgleichprofil("L 70 x 6"));
            gleicheListe.Add(new Winkelgleichprofil("L 70 x 7"));
            gleicheListe.Add(new Winkelgleichprofil("L 70 x 9"));
            gleicheListe.Add(new Winkelgleichprofil("L 75 x 6"));
            gleicheListe.Add(new Winkelgleichprofil("L 75 x 7"));
            gleicheListe.Add(new Winkelgleichprofil("L 75 x 8"));
            gleicheListe.Add(new Winkelgleichprofil("L 80 x 6"));
            gleicheListe.Add(new Winkelgleichprofil("L 80 x 8"));
            gleicheListe.Add(new Winkelgleichprofil("L 80 x 10"));
            gleicheListe.Add(new Winkelgleichprofil("L 90 x 7"));
            gleicheListe.Add(new Winkelgleichprofil("L 90 x 8"));
            gleicheListe.Add(new Winkelgleichprofil("L 90 x 9"));
            gleicheListe.Add(new Winkelgleichprofil("L 90 x 10"));
            gleicheListe.Add(new Winkelgleichprofil("L 100 x 8"));
            gleicheListe.Add(new Winkelgleichprofil("L 100 x 10"));
            gleicheListe.Add(new Winkelgleichprofil("L 100 x 12"));
            gleicheListe.Add(new Winkelgleichprofil("L 110 x 10"));
            gleicheListe.Add(new Winkelgleichprofil("L 110 x 12"));
            gleicheListe.Add(new Winkelgleichprofil("L 120 x 10"));
            gleicheListe.Add(new Winkelgleichprofil("L 120 x 11"));
            gleicheListe.Add(new Winkelgleichprofil("L 120 x 12"));
            gleicheListe.Add(new Winkelgleichprofil("L 120 x 13"));
            gleicheListe.Add(new Winkelgleichprofil("L 120 x 15"));
            gleicheListe.Add(new Winkelgleichprofil("L 130 x 12"));
            gleicheListe.Add(new Winkelgleichprofil("L 140 x 13"));
            gleicheListe.Add(new Winkelgleichprofil("L 150 x 10"));
            gleicheListe.Add(new Winkelgleichprofil("L 150 x 12"));
            gleicheListe.Add(new Winkelgleichprofil("L 150 x 14"));
            gleicheListe.Add(new Winkelgleichprofil("L 150 x 15"));
            gleicheListe.Add(new Winkelgleichprofil("L 150 x 18"));
            gleicheListe.Add(new Winkelgleichprofil("L 160 x 14"));
            gleicheListe.Add(new Winkelgleichprofil("L 160 x 15"));
            gleicheListe.Add(new Winkelgleichprofil("L 160 x 16"));
            gleicheListe.Add(new Winkelgleichprofil("L 160 x 17"));
            gleicheListe.Add(new Winkelgleichprofil("L 180 x 14"));
            gleicheListe.Add(new Winkelgleichprofil("L 180 x 15"));
            gleicheListe.Add(new Winkelgleichprofil("L 180 x 16"));
            gleicheListe.Add(new Winkelgleichprofil("L 180 x 18"));
            gleicheListe.Add(new Winkelgleichprofil("L 180 x 20"));
            gleicheListe.Add(new Winkelgleichprofil("L 200 x 16"));
            gleicheListe.Add(new Winkelgleichprofil("L 200 x 17"));
            gleicheListe.Add(new Winkelgleichprofil("L 200 x 18"));
            gleicheListe.Add(new Winkelgleichprofil("L 200 x 19"));
            gleicheListe.Add(new Winkelgleichprofil("L 200 x 20"));
            gleicheListe.Add(new Winkelgleichprofil("L 200 x 21"));
            gleicheListe.Add(new Winkelgleichprofil("L 200 x 22"));
            gleicheListe.Add(new Winkelgleichprofil("L 200 x 24"));
            gleicheListe.Add(new Winkelgleichprofil("L 200 x 26"));
            gleicheListe.Add(new Winkelgleichprofil("L 250 x 20"));
            gleicheListe.Add(new Winkelgleichprofil("L 250 x 21"));
            gleicheListe.Add(new Winkelgleichprofil("L 250 x 22"));
            gleicheListe.Add(new Winkelgleichprofil("L 250 x 23"));
            gleicheListe.Add(new Winkelgleichprofil("L 250 x 24"));
            gleicheListe.Add(new Winkelgleichprofil("L 250 x 26"));
            gleicheListe.Add(new Winkelgleichprofil("L 250 x 28"));
            gleicheListe.Add(new Winkelgleichprofil("L 250 x 35"));

            #endregion

            //Hohlprofile

            ProfilartGenau.ItemsSource = kreisListe;
            #region
            kreisListe.Add(new Kreis("RR 21,3 x 2"));
            kreisListe.Add(new Kreis("RR 21,3 x 2,3"));
            kreisListe.Add(new Kreis("RR 21,3 x 2,5"));
            kreisListe.Add(new Kreis("RR 21,3 x 2,6"));
            kreisListe.Add(new Kreis("RR 21,3 x 2,9"));
            kreisListe.Add(new Kreis("RR 21,3 x 3"));
            kreisListe.Add(new Kreis("RR 21,3 x 3,2"));
            kreisListe.Add(new Kreis("RR 21,3 x 3,6"));
            kreisListe.Add(new Kreis("RR 21,3 x 4"));
            kreisListe.Add(new Kreis("RR 21,3 x 4,5"));
            kreisListe.Add(new Kreis("RR 21,3 x 5"));
            kreisListe.Add(new Kreis("RR 26,9 x 2"));
            kreisListe.Add(new Kreis("RR 26,9 x 2,3"));
            kreisListe.Add(new Kreis("RR 26,9 x 2,5"));
            kreisListe.Add(new Kreis("RR 26,9 x 2,6"));
            kreisListe.Add(new Kreis("RR 26,9 x 2,9"));
            kreisListe.Add(new Kreis("RR 26,9 x 3"));
            kreisListe.Add(new Kreis("RR 26,9 x 3,2"));
            kreisListe.Add(new Kreis("RR 26,9 x 3,6"));
            kreisListe.Add(new Kreis("RR 26,9 x 4"));
            kreisListe.Add(new Kreis("RR 26,9 x 4,5"));
            kreisListe.Add(new Kreis("RR 26,9 x 5"));
            kreisListe.Add(new Kreis("RR 26,9 x 5,6"));
            kreisListe.Add(new Kreis("RR 26,9 x 6,3"));
            kreisListe.Add(new Kreis("RR 33,7 x 2"));
            kreisListe.Add(new Kreis("RR 33,7 x 2,3"));
            kreisListe.Add(new Kreis("RR 33,7 x 2,5"));
            kreisListe.Add(new Kreis("RR 33,7 x 2,6"));
            kreisListe.Add(new Kreis("RR 33,7 x 2,9"));
            kreisListe.Add(new Kreis("RR 33,7 x 3"));
            kreisListe.Add(new Kreis("RR 33,7 x 3,2"));
            kreisListe.Add(new Kreis("RR 33,7 x 3,6"));
            kreisListe.Add(new Kreis("RR 33,7 x 4"));
            kreisListe.Add(new Kreis("RR 33,7 x 4,5"));
            kreisListe.Add(new Kreis("RR 33,7 x 5"));
            kreisListe.Add(new Kreis("RR 33,7 x 5,6"));
            kreisListe.Add(new Kreis("RR 33,7 x 6,3"));
            kreisListe.Add(new Kreis("RR 33,7 x 7,1"));
            kreisListe.Add(new Kreis("RR 33,7 x 8"));
            kreisListe.Add(new Kreis("RR 38 x 2,6"));
            kreisListe.Add(new Kreis("RR 38 x 2,9"));
            kreisListe.Add(new Kreis("RR 38 x 3"));
            kreisListe.Add(new Kreis("RR 38 x 3,2"));
            kreisListe.Add(new Kreis("RR 38 x 3,6"));
            kreisListe.Add(new Kreis("RR 38 x 4"));
            kreisListe.Add(new Kreis("RR 38 x 4,5"));
            kreisListe.Add(new Kreis("RR 38 x 5"));
            kreisListe.Add(new Kreis("RR 38 x 5,6"));
            kreisListe.Add(new Kreis("RR 38 x 6,3"));
            kreisListe.Add(new Kreis("RR 38 x 7,1"));
            kreisListe.Add(new Kreis("RR 38 x 8"));
            kreisListe.Add(new Kreis("RR 38 x 8,8"));
            kreisListe.Add(new Kreis("RR 38 x 10"));
            kreisListe.Add(new Kreis("RR 42,4 x 2"));
            kreisListe.Add(new Kreis("RR 42,4 x 2,5"));
            kreisListe.Add(new Kreis("RR 42,4 x 2,6"));
            kreisListe.Add(new Kreis("RR 42,4 x 2,9"));
            kreisListe.Add(new Kreis("RR 42,4 x 3"));
            kreisListe.Add(new Kreis("RR 42,4 x 3,2"));
            kreisListe.Add(new Kreis("RR 42,4 x 3,6"));
            kreisListe.Add(new Kreis("RR 42,4 x 4"));
            kreisListe.Add(new Kreis("RR 42,4 x 4,5"));
            kreisListe.Add(new Kreis("RR 42,4 x 5"));
            kreisListe.Add(new Kreis("RR 42,4 x 5,6"));
            kreisListe.Add(new Kreis("RR 42,4 x 6,3"));
            kreisListe.Add(new Kreis("RR 42,4 x 7,1"));
            kreisListe.Add(new Kreis("RR 42,4 x 8"));
            kreisListe.Add(new Kreis("RR 42,4 x 8,8"));
            kreisListe.Add(new Kreis("RR 42,4 x 10"));
            kreisListe.Add(new Kreis("RR 42,4 x 11"));
            kreisListe.Add(new Kreis("RR 45 x 3"));
            kreisListe.Add(new Kreis("RR 48,3 x 2"));
            kreisListe.Add(new Kreis("RR 48,3 x 2,5"));
            kreisListe.Add(new Kreis("RR 48,3 x 2,6"));
            kreisListe.Add(new Kreis("RR 48,3 x 2,9"));
            kreisListe.Add(new Kreis("RR 48,3 x 3"));
            kreisListe.Add(new Kreis("RR 48,3 x 3,2"));
            kreisListe.Add(new Kreis("RR 48,3 x 3,6"));
            kreisListe.Add(new Kreis("RR 48,3 x 4"));
            kreisListe.Add(new Kreis("RR 48,3 x 4,5"));
            kreisListe.Add(new Kreis("RR 48,3 x 5"));
            kreisListe.Add(new Kreis("RR 48,3 x 5,6"));
            kreisListe.Add(new Kreis("RR 48,3 x 6,3"));
            kreisListe.Add(new Kreis("RR 48,3 x 7,1"));
            kreisListe.Add(new Kreis("RR 48,3 x 8"));
            kreisListe.Add(new Kreis("RR 48,3 x 8,8"));
            kreisListe.Add(new Kreis("RR 48,3 x 10"));
            kreisListe.Add(new Kreis("RR 48,3 x 11"));
            kreisListe.Add(new Kreis("RR 48,3 x 12,5"));
            kreisListe.Add(new Kreis("RR 50 x 3"));
            kreisListe.Add(new Kreis("RR 50 x 4"));
            kreisListe.Add(new Kreis("RR 51 x 2,6"));
            kreisListe.Add(new Kreis("RR 51 x 2,9"));
            kreisListe.Add(new Kreis("RR 51 x 3,2"));
            kreisListe.Add(new Kreis("RR 51 x 3,6"));
            kreisListe.Add(new Kreis("RR 51 x 4"));
            kreisListe.Add(new Kreis("RR 51 x 4,5"));
            kreisListe.Add(new Kreis("RR 51 x 5"));
            kreisListe.Add(new Kreis("RR 51 x 5,6"));
            kreisListe.Add(new Kreis("RR 51 x 6,3"));
            kreisListe.Add(new Kreis("RR 51 x 7,1"));
            kreisListe.Add(new Kreis("RR 51 x 8"));
            kreisListe.Add(new Kreis("RR 51 x 8,8"));
            kreisListe.Add(new Kreis("RR 51 x 10"));
            kreisListe.Add(new Kreis("RR 51 x 11"));
            kreisListe.Add(new Kreis("RR 51 x 12,5"));
            kreisListe.Add(new Kreis("RR 51 x 14,2"));
            kreisListe.Add(new Kreis("RR 55 x 3"));
            kreisListe.Add(new Kreis("RR 55 x 4"));
            kreisListe.Add(new Kreis("RR 57 x 2,9"));
            kreisListe.Add(new Kreis("RR 57 x 3"));
            kreisListe.Add(new Kreis("RR 57 x 3,2"));
            kreisListe.Add(new Kreis("RR 57 x 3,6"));
            kreisListe.Add(new Kreis("RR 57 x 4"));
            kreisListe.Add(new Kreis("RR 57 x 4,5"));
            kreisListe.Add(new Kreis("RR 57 x 5"));
            kreisListe.Add(new Kreis("RR 57 x 5,6"));
            kreisListe.Add(new Kreis("RR 57 x 6,3"));
            kreisListe.Add(new Kreis("RR 57 x 7,1"));
            kreisListe.Add(new Kreis("RR 57 x 8"));
            kreisListe.Add(new Kreis("RR 57 x 8,8"));
            kreisListe.Add(new Kreis("RR 57 x 10"));
            kreisListe.Add(new Kreis("RR 57 x 11"));
            kreisListe.Add(new Kreis("RR 57 x 12,5"));
            kreisListe.Add(new Kreis("RR 57 x 14,2"));
            kreisListe.Add(new Kreis("RR 57 x 16"));
            kreisListe.Add(new Kreis("RR 60,3 x 2"));
            kreisListe.Add(new Kreis("RR 60,3 x 2,5"));
            kreisListe.Add(new Kreis("RR 60,3 x 2,6"));
            kreisListe.Add(new Kreis("RR 60,3 x 2,9"));
            kreisListe.Add(new Kreis("RR 60,3 x 3"));
            kreisListe.Add(new Kreis("RR 60,3 x 3,2"));
            kreisListe.Add(new Kreis("RR 60,3 x 3,6"));
            kreisListe.Add(new Kreis("RR 60,3 x 4"));
            kreisListe.Add(new Kreis("RR 60,3 x 4,5"));
            kreisListe.Add(new Kreis("RR 60,3 x 5"));
            kreisListe.Add(new Kreis("RR 60,3 x 5,6"));
            kreisListe.Add(new Kreis("RR 60,3 x 6,3"));
            kreisListe.Add(new Kreis("RR 60,3 x 7,1"));
            kreisListe.Add(new Kreis("RR 60,3 x 8"));
            kreisListe.Add(new Kreis("RR 60,3 x 8,8"));
            kreisListe.Add(new Kreis("RR 60,3 x 10"));
            kreisListe.Add(new Kreis("RR 60,3 x 11"));
            kreisListe.Add(new Kreis("RR 60,3 x 12,5"));
            kreisListe.Add(new Kreis("RR 60,3 x 14,2"));
            kreisListe.Add(new Kreis("RR 60,3 x 16"));
            kreisListe.Add(new Kreis("RR 60,3 x 17,5"));
            kreisListe.Add(new Kreis("RR 63 x 3"));
            kreisListe.Add(new Kreis("RR 63 x 4"));
            kreisListe.Add(new Kreis("RR 63,5 x 2,9"));
            kreisListe.Add(new Kreis("RR 63,5 x 3,2"));
            kreisListe.Add(new Kreis("RR 63,5 x 3,6"));
            kreisListe.Add(new Kreis("RR 63,5 x 4"));
            kreisListe.Add(new Kreis("RR 63,5 x 4,5"));
            kreisListe.Add(new Kreis("RR 63,5 x 5"));
            kreisListe.Add(new Kreis("RR 63,5 x 5,6"));
            kreisListe.Add(new Kreis("RR 63,5 x 6,3"));
            kreisListe.Add(new Kreis("RR 63,5 x 7,1"));
            kreisListe.Add(new Kreis("RR 63,5 x 8"));
            kreisListe.Add(new Kreis("RR 63,5 x 8,8"));
            kreisListe.Add(new Kreis("RR 63,5 x 10"));
            kreisListe.Add(new Kreis("RR 63,5 x 11"));
            kreisListe.Add(new Kreis("RR 63,5 x 12,5"));
            kreisListe.Add(new Kreis("RR 63,5 x 14,2"));
            kreisListe.Add(new Kreis("RR 63,5 x 16"));
            kreisListe.Add(new Kreis("RR 63,5 x 17,5"));
            kreisListe.Add(new Kreis("RR 70 x 2,9"));
            kreisListe.Add(new Kreis("RR 70 x 3"));
            kreisListe.Add(new Kreis("RR 70 x 3,2"));
            kreisListe.Add(new Kreis("RR 70 x 3,6"));
            kreisListe.Add(new Kreis("RR 70 x 4"));
            kreisListe.Add(new Kreis("RR 70 x 4,5"));
            kreisListe.Add(new Kreis("RR 70 x 5"));
            kreisListe.Add(new Kreis("RR 70 x 5,6"));
            kreisListe.Add(new Kreis("RR 70 x 6,3"));
            kreisListe.Add(new Kreis("RR 70 x 7,1"));
            kreisListe.Add(new Kreis("RR 70 x 8"));
            kreisListe.Add(new Kreis("RR 70 x 8,8"));
            kreisListe.Add(new Kreis("RR 70 x 10"));
            kreisListe.Add(new Kreis("RR 70 x 11"));
            kreisListe.Add(new Kreis("RR 70 x 12,5"));
            kreisListe.Add(new Kreis("RR 70 x 14,2"));
            kreisListe.Add(new Kreis("RR 70 x 16"));
            kreisListe.Add(new Kreis("RR 70 x 17,5"));
            kreisListe.Add(new Kreis("RR 70 x 20"));
            kreisListe.Add(new Kreis("RR 76,1 x 2"));
            kreisListe.Add(new Kreis("RR 76,1 x 2,5"));
            kreisListe.Add(new Kreis("RR 76,1 x 2,6"));
            kreisListe.Add(new Kreis("RR 76,1 x 2,9"));
            kreisListe.Add(new Kreis("RR 76,1 x 3"));
            kreisListe.Add(new Kreis("RR 76,1 x 3,2"));
            kreisListe.Add(new Kreis("RR 76,1 x 3,6"));
            kreisListe.Add(new Kreis("RR 76,1 x 4"));
            kreisListe.Add(new Kreis("RR 76,1 x 4,5"));
            kreisListe.Add(new Kreis("RR 76,1 x 5"));
            kreisListe.Add(new Kreis("RR 76,1 x 5,6"));
            kreisListe.Add(new Kreis("RR 76,1 x 6"));
            kreisListe.Add(new Kreis("RR 76,1 x 6,3"));
            kreisListe.Add(new Kreis("RR 76,1 x 7,1"));
            kreisListe.Add(new Kreis("RR 76,1 x 8"));
            kreisListe.Add(new Kreis("RR 76,1 x 8,8"));
            kreisListe.Add(new Kreis("RR 76,1 x 10"));
            kreisListe.Add(new Kreis("RR 76,1 x 11"));
            kreisListe.Add(new Kreis("RR 76,1 x 12,5"));
            kreisListe.Add(new Kreis("RR 76,1 x 14,2"));
            kreisListe.Add(new Kreis("RR 76,1 x 16"));
            kreisListe.Add(new Kreis("RR 76,1 x 17,5"));
            kreisListe.Add(new Kreis("RR 76,1 x 20"));
            kreisListe.Add(new Kreis("RR 80 x 3"));
            kreisListe.Add(new Kreis("RR 80 x 4"));
            kreisListe.Add(new Kreis("RR 80 x 5"));
            kreisListe.Add(new Kreis("RR 80 x 6"));
            kreisListe.Add(new Kreis("RR 82,5 x 3,2"));
            kreisListe.Add(new Kreis("RR 82,5 x 3,6"));
            kreisListe.Add(new Kreis("RR 82,5 x 4"));
            kreisListe.Add(new Kreis("RR 82,5 x 4,5"));
            kreisListe.Add(new Kreis("RR 82,5 x 5"));
            kreisListe.Add(new Kreis("RR 82,5 x 5,6"));
            kreisListe.Add(new Kreis("RR 82,5 x 6,3"));
            kreisListe.Add(new Kreis("RR 82,5 x 7,1"));
            kreisListe.Add(new Kreis("RR 82,5 x 8"));
            kreisListe.Add(new Kreis("RR 82,5 x 8,8"));
            kreisListe.Add(new Kreis("RR 82,5 x 10"));
            kreisListe.Add(new Kreis("RR 82,5 x 11"));
            kreisListe.Add(new Kreis("RR 82,5 x 12,5"));
            kreisListe.Add(new Kreis("RR 82,5 x 14,2"));
            kreisListe.Add(new Kreis("RR 82,5 x 16"));
            kreisListe.Add(new Kreis("RR 82,5 x 17,5"));
            kreisListe.Add(new Kreis("RR 82,5 x 20"));
            kreisListe.Add(new Kreis("RR 82,5 x 25"));
            kreisListe.Add(new Kreis("RR 83 x 3"));
            kreisListe.Add(new Kreis("RR 83 x 4"));
            kreisListe.Add(new Kreis("RR 88,9 x 2"));
            kreisListe.Add(new Kreis("RR 88,9 x 2,5"));
            kreisListe.Add(new Kreis("RR 88,9 x 3"));
            kreisListe.Add(new Kreis("RR 88,9 x 3,2"));
            kreisListe.Add(new Kreis("RR 88,9 x 3,6"));
            kreisListe.Add(new Kreis("RR 88,9 x 4"));
            kreisListe.Add(new Kreis("RR 88,9 x 4,5"));
            kreisListe.Add(new Kreis("RR 88,9 x 5"));
            kreisListe.Add(new Kreis("RR 88,9 x 5,6"));
            kreisListe.Add(new Kreis("RR 88,9 x 6"));
            kreisListe.Add(new Kreis("RR 88,9 x 6,3"));
            kreisListe.Add(new Kreis("RR 88,9 x 7,1"));
            kreisListe.Add(new Kreis("RR 88,9 x 8"));
            kreisListe.Add(new Kreis("RR 88,9 x 8,8"));
            kreisListe.Add(new Kreis("RR 88,9 x 10"));
            kreisListe.Add(new Kreis("RR 88,9 x 11"));
            kreisListe.Add(new Kreis("RR 88,9 x 12,5"));
            kreisListe.Add(new Kreis("RR 88,9 x 14,2"));
            kreisListe.Add(new Kreis("RR 88,9 x 16"));
            kreisListe.Add(new Kreis("RR 88,9 x 17,5"));
            kreisListe.Add(new Kreis("RR 88,9 x 20"));
            kreisListe.Add(new Kreis("RR 88,9 x 25"));
            kreisListe.Add(new Kreis("RR 95 x 3"));
            kreisListe.Add(new Kreis("RR 95 x 4"));
            kreisListe.Add(new Kreis("RR 100 x 3"));
            kreisListe.Add(new Kreis("RR 100 x 4"));
            kreisListe.Add(new Kreis("RR 100 x 5"));
            kreisListe.Add(new Kreis("RR 100 x 6"));
            kreisListe.Add(new Kreis("RR 100 x 8"));
            kreisListe.Add(new Kreis("RR 101,6 x 2"));
            kreisListe.Add(new Kreis("RR 101,6 x 2,5"));
            kreisListe.Add(new Kreis("RR 101,6 x 3"));
            kreisListe.Add(new Kreis("RR 101,6 x 3,2"));
            kreisListe.Add(new Kreis("RR 101,6 x 3,6"));
            kreisListe.Add(new Kreis("RR 101,6 x 4"));
            kreisListe.Add(new Kreis("RR 101,6 x 4,5"));
            kreisListe.Add(new Kreis("RR 101,6 x 5"));
            kreisListe.Add(new Kreis("RR 101,6 x 5,6"));
            kreisListe.Add(new Kreis("RR 101,6 x 6"));
            kreisListe.Add(new Kreis("RR 101,6 x 6,3"));
            kreisListe.Add(new Kreis("RR 101,6 x 7,1"));
            kreisListe.Add(new Kreis("RR 101,6 x 8"));
            kreisListe.Add(new Kreis("RR 101,6 x 8,8"));
            kreisListe.Add(new Kreis("RR 101,6 x 10"));
            kreisListe.Add(new Kreis("RR 101,6 x 11"));
            kreisListe.Add(new Kreis("RR 101,6 x 12,5"));
            kreisListe.Add(new Kreis("RR 101,6 x 14,2"));
            kreisListe.Add(new Kreis("RR 101,6 x 16"));
            kreisListe.Add(new Kreis("RR 101,6 x 17,5"));
            kreisListe.Add(new Kreis("RR 101,6 x 20"));
            kreisListe.Add(new Kreis("RR 101,6 x 25"));
            kreisListe.Add(new Kreis("RR 101,6 x 30"));
            kreisListe.Add(new Kreis("RR 108 x 3"));
            kreisListe.Add(new Kreis("RR 108 x 3,6"));
            kreisListe.Add(new Kreis("RR 108 x 4"));
            kreisListe.Add(new Kreis("RR 108 x 4,5"));
            kreisListe.Add(new Kreis("RR 108 x 5"));
            kreisListe.Add(new Kreis("RR 108 x 5,6"));
            kreisListe.Add(new Kreis("RR 108 x 6,3"));
            kreisListe.Add(new Kreis("RR 108 x 7,1"));
            kreisListe.Add(new Kreis("RR 108 x 8"));
            kreisListe.Add(new Kreis("RR 108 x 8,8"));
            kreisListe.Add(new Kreis("RR 108 x 10"));
            kreisListe.Add(new Kreis("RR 108 x 11"));
            kreisListe.Add(new Kreis("RR 108 x 12,5"));
            kreisListe.Add(new Kreis("RR 108 x 14,2"));
            kreisListe.Add(new Kreis("RR 108 x 16"));
            kreisListe.Add(new Kreis("RR 108 x 17,5"));
            kreisListe.Add(new Kreis("RR 108 x 20"));
            kreisListe.Add(new Kreis("RR 108 x 25"));
            kreisListe.Add(new Kreis("RR 108 x 30"));
            kreisListe.Add(new Kreis("RR 110 x 3"));
            kreisListe.Add(new Kreis("RR 110 x 4"));
            kreisListe.Add(new Kreis("RR 113 x 3"));
            kreisListe.Add(new Kreis("RR 113 x 4"));
            kreisListe.Add(new Kreis("RR 113 x 5"));
            kreisListe.Add(new Kreis("RR 113 x 6"));
            kreisListe.Add(new Kreis("RR 114,3 x 2,5"));
            kreisListe.Add(new Kreis("RR 114,3 x 3"));
            kreisListe.Add(new Kreis("RR 114,3 x 3,2"));
            kreisListe.Add(new Kreis("RR 114,3 x 3,6"));
            kreisListe.Add(new Kreis("RR 114,3 x 4"));
            kreisListe.Add(new Kreis("RR 114,3 x 4,5"));
            kreisListe.Add(new Kreis("RR 114,3 x 5"));
            kreisListe.Add(new Kreis("RR 114,3 x 5,6"));
            kreisListe.Add(new Kreis("RR 114,3 x 6"));
            kreisListe.Add(new Kreis("RR 114,3 x 6,3"));
            kreisListe.Add(new Kreis("RR 114,3 x 7,1"));
            kreisListe.Add(new Kreis("RR 114,3 x 8"));
            kreisListe.Add(new Kreis("RR 114,3 x 8,8"));
            kreisListe.Add(new Kreis("RR 114,3 x 10"));
            kreisListe.Add(new Kreis("RR 114,3 x 11"));
            kreisListe.Add(new Kreis("RR 114,3 x 12,5"));
            kreisListe.Add(new Kreis("RR 114,3 x 14,2"));
            kreisListe.Add(new Kreis("RR 114,3 x 16"));
            kreisListe.Add(new Kreis("RR 114,3 x 17,5"));
            kreisListe.Add(new Kreis("RR 114,3 x 20"));
            kreisListe.Add(new Kreis("RR 114,3 x 25"));
            kreisListe.Add(new Kreis("RR 114,3 x 30"));
            kreisListe.Add(new Kreis("RR 114,3 x 36"));
            kreisListe.Add(new Kreis("RR 120 x 3"));
            kreisListe.Add(new Kreis("RR 120 x 4"));
            kreisListe.Add(new Kreis("RR 125 x 3"));
            kreisListe.Add(new Kreis("RR 125 x 4"));
            kreisListe.Add(new Kreis("RR 125 x 5"));
            kreisListe.Add(new Kreis("RR 125 x 6"));
            kreisListe.Add(new Kreis("RR 125 x 8"));
            kreisListe.Add(new Kreis("RR 125 x 10"));
            kreisListe.Add(new Kreis("RR 127 x 3"));
            kreisListe.Add(new Kreis("RR 127 x 4"));
            kreisListe.Add(new Kreis("RR 127 x 4,5"));
            kreisListe.Add(new Kreis("RR 127 x 5"));
            kreisListe.Add(new Kreis("RR 127 x 5,6"));
            kreisListe.Add(new Kreis("RR 127 x 6,3"));
            kreisListe.Add(new Kreis("RR 127 x 7,1"));
            kreisListe.Add(new Kreis("RR 127 x 8"));
            kreisListe.Add(new Kreis("RR 127 x 8,8"));
            kreisListe.Add(new Kreis("RR 127 x 10"));
            kreisListe.Add(new Kreis("RR 127 x 11"));
            kreisListe.Add(new Kreis("RR 127 x 12,5"));
            kreisListe.Add(new Kreis("RR 127 x 14,2"));
            kreisListe.Add(new Kreis("RR 127 x 16"));
            kreisListe.Add(new Kreis("RR 127 x 17,5"));
            kreisListe.Add(new Kreis("RR 127 x 20"));
            kreisListe.Add(new Kreis("RR 127 x 25"));
            kreisListe.Add(new Kreis("RR 127 x 30"));
            kreisListe.Add(new Kreis("RR 127 x 36"));
            kreisListe.Add(new Kreis("RR 127 x 40"));
            kreisListe.Add(new Kreis("RR 127 x 45"));
            kreisListe.Add(new Kreis("RR 133 x 3"));
            kreisListe.Add(new Kreis("RR 133 x 4"));
            kreisListe.Add(new Kreis("RR 133 x 5,6"));
            kreisListe.Add(new Kreis("RR 133 x 7,1"));
            kreisListe.Add(new Kreis("RR 139,7 x 3"));
            kreisListe.Add(new Kreis("RR 139,7 x 4"));
            kreisListe.Add(new Kreis("RR 139,7 x 4,5"));
            kreisListe.Add(new Kreis("RR 139,7 x 5"));
            kreisListe.Add(new Kreis("RR 139,7 x 5,6"));
            kreisListe.Add(new Kreis("RR 139,7 x 6"));
            kreisListe.Add(new Kreis("RR 139,7 x 6,3"));
            kreisListe.Add(new Kreis("RR 139,7 x 7,1"));
            kreisListe.Add(new Kreis("RR 139,7 x 8"));
            kreisListe.Add(new Kreis("RR 139,7 x 8,8"));
            kreisListe.Add(new Kreis("RR 139,7 x 10"));
            kreisListe.Add(new Kreis("RR 139,7 x 11"));
            kreisListe.Add(new Kreis("RR 139,7 x 12"));
            kreisListe.Add(new Kreis("RR 139,7 x 12,5"));
            kreisListe.Add(new Kreis("RR 139,7 x 14,2"));
            kreisListe.Add(new Kreis("RR 139,7 x 16"));
            kreisListe.Add(new Kreis("RR 139,7 x 17,5"));
            kreisListe.Add(new Kreis("RR 139,7 x 20"));
            kreisListe.Add(new Kreis("RR 139,7 x 25"));
            kreisListe.Add(new Kreis("RR 139,7 x 30"));
            kreisListe.Add(new Kreis("RR 139,7 x 36"));
            kreisListe.Add(new Kreis("RR 139,7 x 40"));
            kreisListe.Add(new Kreis("RR 139,7 x 45"));
            kreisListe.Add(new Kreis("RR 139,7 x 50"));
            kreisListe.Add(new Kreis("RR 152 x 3"));
            kreisListe.Add(new Kreis("RR 152 x 4"));
            kreisListe.Add(new Kreis("RR 152,4 x 4,5"));
            kreisListe.Add(new Kreis("RR 152,4 x 5"));
            kreisListe.Add(new Kreis("RR 152,4 x 5,6"));
            kreisListe.Add(new Kreis("RR 152,4 x 6,3"));
            kreisListe.Add(new Kreis("RR 152,4 x 7,1"));
            kreisListe.Add(new Kreis("RR 152,4 x 8"));
            kreisListe.Add(new Kreis("RR 152,4 x 8,8"));
            kreisListe.Add(new Kreis("RR 152,4 x 10"));
            kreisListe.Add(new Kreis("RR 152,4 x 11"));
            kreisListe.Add(new Kreis("RR 152,4 x 12,5"));
            kreisListe.Add(new Kreis("RR 152,4 x 14,2"));
            kreisListe.Add(new Kreis("RR 152,4 x 16"));
            kreisListe.Add(new Kreis("RR 152,4 x 17,5"));
            kreisListe.Add(new Kreis("RR 152,4 x 20"));
            kreisListe.Add(new Kreis("RR 152,4 x 25"));
            kreisListe.Add(new Kreis("RR 152,4 x 30"));
            kreisListe.Add(new Kreis("RR 152,4 x 36"));
            kreisListe.Add(new Kreis("RR 152,4 x 40"));
            kreisListe.Add(new Kreis("RR 152,4 x 45"));
            kreisListe.Add(new Kreis("RR 152,4 x 50"));
            kreisListe.Add(new Kreis("RR 159 x 3"));
            kreisListe.Add(new Kreis("RR 159 x 4"));
            kreisListe.Add(new Kreis("RR 159 x 4,5"));
            kreisListe.Add(new Kreis("RR 159 x 5"));
            kreisListe.Add(new Kreis("RR 159 x 5,6"));
            kreisListe.Add(new Kreis("RR 159 x 6"));
            kreisListe.Add(new Kreis("RR 159 x 6,3"));
            kreisListe.Add(new Kreis("RR 159 x 7,1"));
            kreisListe.Add(new Kreis("RR 159 x 8"));
            kreisListe.Add(new Kreis("RR 159 x 8,8"));
            kreisListe.Add(new Kreis("RR 159 x 10"));
            kreisListe.Add(new Kreis("RR 159 x 11"));
            kreisListe.Add(new Kreis("RR 159 x 12,5"));
            kreisListe.Add(new Kreis("RR 159 x 14,2"));
            kreisListe.Add(new Kreis("RR 159 x 16"));
            kreisListe.Add(new Kreis("RR 159 x 17,5"));
            kreisListe.Add(new Kreis("RR 159 x 20"));
            kreisListe.Add(new Kreis("RR 159 x 25"));
            kreisListe.Add(new Kreis("RR 159 x 30"));
            kreisListe.Add(new Kreis("RR 159 x 36"));
            kreisListe.Add(new Kreis("RR 159 x 40"));
            kreisListe.Add(new Kreis("RR 159 x 45"));
            kreisListe.Add(new Kreis("RR 159 x 50"));
            kreisListe.Add(new Kreis("RR 159 x 60"));
            kreisListe.Add(new Kreis("RR 164 x 3"));
            kreisListe.Add(new Kreis("RR 164 x 4"));
            kreisListe.Add(new Kreis("RR 168,3 x 3"));
            kreisListe.Add(new Kreis("RR 168,3 x 4"));
            kreisListe.Add(new Kreis("RR 168,3 x 4,5"));
            kreisListe.Add(new Kreis("RR 168,3 x 5"));
            kreisListe.Add(new Kreis("RR 168,3 x 5,6"));
            kreisListe.Add(new Kreis("RR 168,3 x 6"));
            kreisListe.Add(new Kreis("RR 168,3 x 6,3"));
            kreisListe.Add(new Kreis("RR 168,3 x 7,1"));
            kreisListe.Add(new Kreis("RR 168,3 x 8"));
            kreisListe.Add(new Kreis("RR 168,3 x 8,8"));
            kreisListe.Add(new Kreis("RR 168,3 x 10"));
            kreisListe.Add(new Kreis("RR 168,3 x 11"));
            kreisListe.Add(new Kreis("RR 168,3 x 12"));
            kreisListe.Add(new Kreis("RR 168,3 x 12,5"));
            kreisListe.Add(new Kreis("RR 168,3 x 14,2"));
            kreisListe.Add(new Kreis("RR 168,3 x 16"));
            kreisListe.Add(new Kreis("RR 168,3 x 17,5"));
            kreisListe.Add(new Kreis("RR 168,3 x 20"));
            kreisListe.Add(new Kreis("RR 168,3 x 25"));
            kreisListe.Add(new Kreis("RR 168,3 x 30"));
            kreisListe.Add(new Kreis("RR 168,3 x 36"));
            kreisListe.Add(new Kreis("RR 168,3 x 40"));
            kreisListe.Add(new Kreis("RR 168,3 x 45"));
            kreisListe.Add(new Kreis("RR 168,3 x 50"));
            kreisListe.Add(new Kreis("RR 168,3 x 60"));
            kreisListe.Add(new Kreis("RR 177,8 x 3"));
            kreisListe.Add(new Kreis("RR 177,8 x 4"));
            kreisListe.Add(new Kreis("RR 177,8 x 4,5"));
            kreisListe.Add(new Kreis("RR 177,8 x 5"));
            kreisListe.Add(new Kreis("RR 177,8 x 5,6"));
            kreisListe.Add(new Kreis("RR 177,8 x 6"));
            kreisListe.Add(new Kreis("RR 177,8 x 6,3"));
            kreisListe.Add(new Kreis("RR 177,8 x 7,1"));
            kreisListe.Add(new Kreis("RR 177,8 x 8"));
            kreisListe.Add(new Kreis("RR 177,8 x 8,8"));
            kreisListe.Add(new Kreis("RR 177,8 x 10"));
            kreisListe.Add(new Kreis("RR 177,8 x 11"));
            kreisListe.Add(new Kreis("RR 177,8 x 12"));
            kreisListe.Add(new Kreis("RR 177,8 x 12,5"));
            kreisListe.Add(new Kreis("RR 177,8 x 14,2"));
            kreisListe.Add(new Kreis("RR 177,8 x 16"));
            kreisListe.Add(new Kreis("RR 177,8 x 17,5"));
            kreisListe.Add(new Kreis("RR 177,8 x 20"));
            kreisListe.Add(new Kreis("RR 177,8 x 25"));
            kreisListe.Add(new Kreis("RR 177,8 x 30"));
            kreisListe.Add(new Kreis("RR 177,8 x 36"));
            kreisListe.Add(new Kreis("RR 177,8 x 40"));
            kreisListe.Add(new Kreis("RR 177,8 x 45"));
            kreisListe.Add(new Kreis("RR 177,8 x 50"));
            kreisListe.Add(new Kreis("RR 177,8 x 60"));
            kreisListe.Add(new Kreis("RR 193,7 x 3"));
            kreisListe.Add(new Kreis("RR 193,7 x 4"));
            kreisListe.Add(new Kreis("RR 193,7 x 4,5"));
            kreisListe.Add(new Kreis("RR 193,7 x 5"));
            kreisListe.Add(new Kreis("RR 193,7 x 5,6"));
            kreisListe.Add(new Kreis("RR 193,7 x 6"));
            kreisListe.Add(new Kreis("RR 193,7 x 6,3"));
            kreisListe.Add(new Kreis("RR 193,7 x 7,1"));
            kreisListe.Add(new Kreis("RR 193,7 x 8"));
            kreisListe.Add(new Kreis("RR 193,7 x 8,8"));
            kreisListe.Add(new Kreis("RR 193,7 x 10"));
            kreisListe.Add(new Kreis("RR 193,7 x 11"));
            kreisListe.Add(new Kreis("RR 193,7 x 12"));
            kreisListe.Add(new Kreis("RR 193,7 x 12,5"));
            kreisListe.Add(new Kreis("RR 193,7 x 14,2"));
            kreisListe.Add(new Kreis("RR 193,7 x 16"));
            kreisListe.Add(new Kreis("RR 193,7 x 17,5"));
            kreisListe.Add(new Kreis("RR 193,7 x 20"));
            kreisListe.Add(new Kreis("RR 193,7 x 25"));
            kreisListe.Add(new Kreis("RR 193,7 x 30"));
            kreisListe.Add(new Kreis("RR 193,7 x 36"));
            kreisListe.Add(new Kreis("RR 193,7 x 40"));
            kreisListe.Add(new Kreis("RR 193,7 x 45"));
            kreisListe.Add(new Kreis("RR 193,7 x 50"));
            kreisListe.Add(new Kreis("RR 193,7 x 60"));
            kreisListe.Add(new Kreis("RR 200 x 3"));
            kreisListe.Add(new Kreis("RR 200 x 4"));
            kreisListe.Add(new Kreis("RR 200 x 5"));
            kreisListe.Add(new Kreis("RR 200 x 6"));
            kreisListe.Add(new Kreis("RR 200 x 8"));
            kreisListe.Add(new Kreis("RR 219,1 x 3"));
            kreisListe.Add(new Kreis("RR 219,1 x 4"));
            kreisListe.Add(new Kreis("RR 219,1 x 4,5"));
            kreisListe.Add(new Kreis("RR 219,1 x 5"));
            kreisListe.Add(new Kreis("RR 219,1 x 5,6"));
            kreisListe.Add(new Kreis("RR 219,1 x 6"));
            kreisListe.Add(new Kreis("RR 219,1 x 6,3"));
            kreisListe.Add(new Kreis("RR 219,1 x 7,1"));
            kreisListe.Add(new Kreis("RR 219,1 x 8"));
            kreisListe.Add(new Kreis("RR 219,1 x 8,8"));
            kreisListe.Add(new Kreis("RR 219,1 x 10"));
            kreisListe.Add(new Kreis("RR 219,1 x 11"));
            kreisListe.Add(new Kreis("RR 219,1 x 12"));
            kreisListe.Add(new Kreis("RR 219,1 x 12,5"));
            kreisListe.Add(new Kreis("RR 219,1 x 14,2"));
            kreisListe.Add(new Kreis("RR 219,1 x 16"));
            kreisListe.Add(new Kreis("RR 219,1 x 17,5"));
            kreisListe.Add(new Kreis("RR 219,1 x 20"));
            kreisListe.Add(new Kreis("RR 219,1 x 25"));
            kreisListe.Add(new Kreis("RR 219,1 x 30"));
            kreisListe.Add(new Kreis("RR 219,1 x 36"));
            kreisListe.Add(new Kreis("RR 219,1 x 40"));
            kreisListe.Add(new Kreis("RR 219,1 x 45"));
            kreisListe.Add(new Kreis("RR 219,1 x 50"));
            kreisListe.Add(new Kreis("RR 219,1 x 60"));
            kreisListe.Add(new Kreis("RR 219,1 x 70"));
            kreisListe.Add(new Kreis("RR 244,5 x 4"));
            kreisListe.Add(new Kreis("RR 244,5 x 4,5"));
            kreisListe.Add(new Kreis("RR 244,5 x 5"));
            kreisListe.Add(new Kreis("RR 244,5 x 5,6"));
            kreisListe.Add(new Kreis("RR 244,5 x 6"));
            kreisListe.Add(new Kreis("RR 244,5 x 6,3"));
            kreisListe.Add(new Kreis("RR 244,5 x 7,1"));
            kreisListe.Add(new Kreis("RR 244,5 x 8"));
            kreisListe.Add(new Kreis("RR 244,5 x 8,8"));
            kreisListe.Add(new Kreis("RR 244,5 x 10"));
            kreisListe.Add(new Kreis("RR 244,5 x 11"));
            kreisListe.Add(new Kreis("RR 244,5 x 12"));
            kreisListe.Add(new Kreis("RR 244,5 x 12,5"));
            kreisListe.Add(new Kreis("RR 244,5 x 14,2"));
            kreisListe.Add(new Kreis("RR 244,5 x 16"));
            kreisListe.Add(new Kreis("RR 244,5 x 17,5"));
            kreisListe.Add(new Kreis("RR 244,5 x 20"));
            kreisListe.Add(new Kreis("RR 244,5 x 25"));
            kreisListe.Add(new Kreis("RR 244,5 x 30"));
            kreisListe.Add(new Kreis("RR 244,5 x 36"));
            kreisListe.Add(new Kreis("RR 244,5 x 40"));
            kreisListe.Add(new Kreis("RR 244,5 x 45"));
            kreisListe.Add(new Kreis("RR 244,5 x 50"));
            kreisListe.Add(new Kreis("RR 244,5 x 60"));
            kreisListe.Add(new Kreis("RR 244,5 x 70"));
            kreisListe.Add(new Kreis("RR 244,5 x 80"));
            kreisListe.Add(new Kreis("RR 244,5 x 90"));
            kreisListe.Add(new Kreis("RR 267 x 6,3"));
            kreisListe.Add(new Kreis("RR 267 x 7,1"));
            kreisListe.Add(new Kreis("RR 267 x 8"));
            kreisListe.Add(new Kreis("RR 267 x 8,8"));
            kreisListe.Add(new Kreis("RR 267 x 10"));
            kreisListe.Add(new Kreis("RR 267 x 11"));
            kreisListe.Add(new Kreis("RR 267 x 12,5"));
            kreisListe.Add(new Kreis("RR 267 x 14,2"));
            kreisListe.Add(new Kreis("RR 267 x 16"));
            kreisListe.Add(new Kreis("RR 267 x 17,5"));
            kreisListe.Add(new Kreis("RR 267 x 20"));
            kreisListe.Add(new Kreis("RR 267 x 25"));
            kreisListe.Add(new Kreis("RR 267 x 30"));
            kreisListe.Add(new Kreis("RR 267 x 36"));
            kreisListe.Add(new Kreis("RR 267 x 40"));
            kreisListe.Add(new Kreis("RR 267 x 45"));
            kreisListe.Add(new Kreis("RR 267 x 50"));
            kreisListe.Add(new Kreis("RR 267 x 60"));
            kreisListe.Add(new Kreis("RR 267 x 70"));
            kreisListe.Add(new Kreis("RR 267 x 80"));
            kreisListe.Add(new Kreis("RR 267 x 90"));
            kreisListe.Add(new Kreis("RR 267 x 100"));
            kreisListe.Add(new Kreis("RR 273 x 4"));
            kreisListe.Add(new Kreis("RR 273 x 4,5"));
            kreisListe.Add(new Kreis("RR 273 x 5"));
            kreisListe.Add(new Kreis("RR 273 x 5,6"));
            kreisListe.Add(new Kreis("RR 273 x 6"));
            kreisListe.Add(new Kreis("RR 273 x 6,3"));
            kreisListe.Add(new Kreis("RR 273 x 7,1"));
            kreisListe.Add(new Kreis("RR 273 x 8"));
            kreisListe.Add(new Kreis("RR 273 x 8,8"));
            kreisListe.Add(new Kreis("RR 273 x 10"));
            kreisListe.Add(new Kreis("RR 273 x 11"));
            kreisListe.Add(new Kreis("RR 273 x 12"));
            kreisListe.Add(new Kreis("RR 273 x 12,5"));
            kreisListe.Add(new Kreis("RR 273 x 14,2"));
            kreisListe.Add(new Kreis("RR 273 x 16"));
            kreisListe.Add(new Kreis("RR 273 x 17,5"));
            kreisListe.Add(new Kreis("RR 273 x 20"));
            kreisListe.Add(new Kreis("RR 273 x 25"));
            kreisListe.Add(new Kreis("RR 273 x 30"));
            kreisListe.Add(new Kreis("RR 273 x 36"));
            kreisListe.Add(new Kreis("RR 273 x 40"));
            kreisListe.Add(new Kreis("RR 273 x 45"));
            kreisListe.Add(new Kreis("RR 273 x 50"));
            kreisListe.Add(new Kreis("RR 273 x 60"));
            kreisListe.Add(new Kreis("RR 273 x 70"));
            kreisListe.Add(new Kreis("RR 273 x 80"));
            kreisListe.Add(new Kreis("RR 273 x 90"));
            kreisListe.Add(new Kreis("RR 273 x 100"));
            kreisListe.Add(new Kreis("RR 298,5 x 7,1"));
            kreisListe.Add(new Kreis("RR 298,5 x 8"));
            kreisListe.Add(new Kreis("RR 298,5 x 8,8"));
            kreisListe.Add(new Kreis("RR 298,5 x 10"));
            kreisListe.Add(new Kreis("RR 298,5 x 11"));
            kreisListe.Add(new Kreis("RR 298,5 x 12,5"));
            kreisListe.Add(new Kreis("RR 298,5 x 14,2"));
            kreisListe.Add(new Kreis("RR 298,5 x 16"));
            kreisListe.Add(new Kreis("RR 298,5 x 17,5"));
            kreisListe.Add(new Kreis("RR 298,5 x 20"));
            kreisListe.Add(new Kreis("RR 298,5 x 25"));
            kreisListe.Add(new Kreis("RR 298,5 x 30"));
            kreisListe.Add(new Kreis("RR 298,5 x 36"));
            kreisListe.Add(new Kreis("RR 298,5 x 40"));
            kreisListe.Add(new Kreis("RR 298,5 x 45"));
            kreisListe.Add(new Kreis("RR 298,5 x 50"));
            kreisListe.Add(new Kreis("RR 298,5 x 60"));
            kreisListe.Add(new Kreis("RR 298,5 x 70"));
            kreisListe.Add(new Kreis("RR 298,5 x 80"));
            kreisListe.Add(new Kreis("RR 298,5 x 90"));
            kreisListe.Add(new Kreis("RR 298,5 x 100"));
            kreisListe.Add(new Kreis("RR 323,9 x 4,5"));
            kreisListe.Add(new Kreis("RR 323,9 x 5"));
            kreisListe.Add(new Kreis("RR 323,9 x 5,6"));
            kreisListe.Add(new Kreis("RR 323,9 x 6"));
            kreisListe.Add(new Kreis("RR 323,9 x 6,3"));
            kreisListe.Add(new Kreis("RR 323,9 x 7,1"));
            kreisListe.Add(new Kreis("RR 323,9 x 8"));
            kreisListe.Add(new Kreis("RR 323,9 x 8,8"));
            kreisListe.Add(new Kreis("RR 323,9 x 10"));
            kreisListe.Add(new Kreis("RR 323,9 x 11"));
            kreisListe.Add(new Kreis("RR 323,9 x 12"));
            kreisListe.Add(new Kreis("RR 323,9 x 12,5"));
            kreisListe.Add(new Kreis("RR 323,9 x 14,2"));
            kreisListe.Add(new Kreis("RR 323,9 x 16"));
            kreisListe.Add(new Kreis("RR 323,9 x 17,5"));
            kreisListe.Add(new Kreis("RR 323,9 x 20"));
            kreisListe.Add(new Kreis("RR 323,9 x 25"));
            kreisListe.Add(new Kreis("RR 323,9 x 30"));
            kreisListe.Add(new Kreis("RR 323,9 x 36"));
            kreisListe.Add(new Kreis("RR 323,9 x 40"));
            kreisListe.Add(new Kreis("RR 323,9 x 45"));
            kreisListe.Add(new Kreis("RR 323,9 x 50"));
            kreisListe.Add(new Kreis("RR 323,9 x 60"));
            kreisListe.Add(new Kreis("RR 323,9 x 70"));
            kreisListe.Add(new Kreis("RR 323,9 x 80"));
            kreisListe.Add(new Kreis("RR 323,9 x 90"));
            kreisListe.Add(new Kreis("RR 323,9 x 100"));
            kreisListe.Add(new Kreis("RR 355,6 x 4,5"));
            kreisListe.Add(new Kreis("RR 355,6 x 5"));
            kreisListe.Add(new Kreis("RR 355,6 x 5,6"));
            kreisListe.Add(new Kreis("RR 355,6 x 6"));
            kreisListe.Add(new Kreis("RR 355,6 x 6,3"));
            kreisListe.Add(new Kreis("RR 355,6 x 7,1"));
            kreisListe.Add(new Kreis("RR 355,6 x 8"));
            kreisListe.Add(new Kreis("RR 355,6 x 8,8"));
            kreisListe.Add(new Kreis("RR 355,6 x 10"));
            kreisListe.Add(new Kreis("RR 355,6 x 11"));
            kreisListe.Add(new Kreis("RR 355,6 x 12"));
            kreisListe.Add(new Kreis("RR 355,6 x 12,5"));
            kreisListe.Add(new Kreis("RR 355,6 x 14,2"));
            kreisListe.Add(new Kreis("RR 355,6 x 16"));
            kreisListe.Add(new Kreis("RR 355,6 x 17,5"));
            kreisListe.Add(new Kreis("RR 355,6 x 20"));
            kreisListe.Add(new Kreis("RR 355,6 x 25"));
            kreisListe.Add(new Kreis("RR 355,6 x 30"));
            kreisListe.Add(new Kreis("RR 355,6 x 36"));
            kreisListe.Add(new Kreis("RR 355,6 x 40"));
            kreisListe.Add(new Kreis("RR 355,6 x 45"));
            kreisListe.Add(new Kreis("RR 355,6 x 50"));
            kreisListe.Add(new Kreis("RR 355,6 x 60"));
            kreisListe.Add(new Kreis("RR 355,6 x 70"));
            kreisListe.Add(new Kreis("RR 355,6 x 80"));
            kreisListe.Add(new Kreis("RR 355,6 x 90"));
            kreisListe.Add(new Kreis("RR 355,6 x 100"));
            kreisListe.Add(new Kreis("RR 368 x 8"));
            kreisListe.Add(new Kreis("RR 368 x 8,8"));
            kreisListe.Add(new Kreis("RR 368 x 10"));
            kreisListe.Add(new Kreis("RR 368 x 11"));
            kreisListe.Add(new Kreis("RR 368 x 12,5"));
            kreisListe.Add(new Kreis("RR 368 x 14,2"));
            kreisListe.Add(new Kreis("RR 368 x 16"));
            kreisListe.Add(new Kreis("RR 368 x 17,5"));
            kreisListe.Add(new Kreis("RR 368 x 20"));
            kreisListe.Add(new Kreis("RR 368 x 25"));
            kreisListe.Add(new Kreis("RR 368 x 30"));
            kreisListe.Add(new Kreis("RR 368 x 36"));
            kreisListe.Add(new Kreis("RR 368 x 40"));
            kreisListe.Add(new Kreis("RR 368 x 45"));
            kreisListe.Add(new Kreis("RR 368 x 50"));
            kreisListe.Add(new Kreis("RR 368 x 60"));
            kreisListe.Add(new Kreis("RR 368 x 70"));
            kreisListe.Add(new Kreis("RR 368 x 80"));
            kreisListe.Add(new Kreis("RR 368 x 90"));
            kreisListe.Add(new Kreis("RR 368 x 100"));
            kreisListe.Add(new Kreis("RR 406,4 x 5"));
            kreisListe.Add(new Kreis("RR 406,4 x 5,6"));
            kreisListe.Add(new Kreis("RR 406,4 x 6"));
            kreisListe.Add(new Kreis("RR 406,4 x 6,3"));
            kreisListe.Add(new Kreis("RR 406,4 x 7,1"));
            kreisListe.Add(new Kreis("RR 406,4 x 8"));
            kreisListe.Add(new Kreis("RR 406,4 x 8,8"));
            kreisListe.Add(new Kreis("RR 406,4 x 10"));
            kreisListe.Add(new Kreis("RR 406,4 x 11"));
            kreisListe.Add(new Kreis("RR 406,4 x 12"));
            kreisListe.Add(new Kreis("RR 406,4 x 12,5"));
            kreisListe.Add(new Kreis("RR 406,4 x 14,2"));
            kreisListe.Add(new Kreis("RR 406,4 x 16"));
            kreisListe.Add(new Kreis("RR 406,4 x 17,5"));
            kreisListe.Add(new Kreis("RR 406,4 x 20"));
            kreisListe.Add(new Kreis("RR 406,4 x 25"));
            kreisListe.Add(new Kreis("RR 406,4 x 30"));
            kreisListe.Add(new Kreis("RR 406,4 x 36"));
            kreisListe.Add(new Kreis("RR 406,4 x 40"));
            kreisListe.Add(new Kreis("RR 406,4 x 45"));
            kreisListe.Add(new Kreis("RR 406,4 x 50"));
            kreisListe.Add(new Kreis("RR 406,4 x 60"));
            kreisListe.Add(new Kreis("RR 406,4 x 70"));
            kreisListe.Add(new Kreis("RR 406,4 x 80"));
            kreisListe.Add(new Kreis("RR 406,4 x 90"));
            kreisListe.Add(new Kreis("RR 406,4 x 100"));
            kreisListe.Add(new Kreis("RR 419 x 10"));
            kreisListe.Add(new Kreis("RR 419 x 11"));
            kreisListe.Add(new Kreis("RR 419 x 12,5"));
            kreisListe.Add(new Kreis("RR 419 x 14,2"));
            kreisListe.Add(new Kreis("RR 419 x 16"));
            kreisListe.Add(new Kreis("RR 419 x 17,5"));
            kreisListe.Add(new Kreis("RR 419 x 20"));
            kreisListe.Add(new Kreis("RR 419 x 25"));
            kreisListe.Add(new Kreis("RR 419 x 30"));
            kreisListe.Add(new Kreis("RR 419 x 36"));
            kreisListe.Add(new Kreis("RR 419 x 40"));
            kreisListe.Add(new Kreis("RR 419 x 45"));
            kreisListe.Add(new Kreis("RR 419 x 50"));
            kreisListe.Add(new Kreis("RR 419 x 60"));
            kreisListe.Add(new Kreis("RR 419 x 70"));
            kreisListe.Add(new Kreis("RR 419 x 80"));
            kreisListe.Add(new Kreis("RR 419 x 90"));
            kreisListe.Add(new Kreis("RR 419 x 100"));
            kreisListe.Add(new Kreis("RR 457 x 5"));
            kreisListe.Add(new Kreis("RR 457 x 5,6"));
            kreisListe.Add(new Kreis("RR 457 x 6"));
            kreisListe.Add(new Kreis("RR 457 x 6,3"));
            kreisListe.Add(new Kreis("RR 457 x 7,1"));
            kreisListe.Add(new Kreis("RR 457 x 8"));
            kreisListe.Add(new Kreis("RR 457 x 8,8"));
            kreisListe.Add(new Kreis("RR 457 x 10"));
            kreisListe.Add(new Kreis("RR 457 x 11"));
            kreisListe.Add(new Kreis("RR 457 x 12"));
            kreisListe.Add(new Kreis("RR 457 x 12,5"));
            kreisListe.Add(new Kreis("RR 457 x 14,2"));
            kreisListe.Add(new Kreis("RR 457 x 16"));
            kreisListe.Add(new Kreis("RR 457 x 17,5"));
            kreisListe.Add(new Kreis("RR 457 x 20"));
            kreisListe.Add(new Kreis("RR 457 x 25"));
            kreisListe.Add(new Kreis("RR 457 x 30"));
            kreisListe.Add(new Kreis("RR 457 x 36"));
            kreisListe.Add(new Kreis("RR 457 x 40"));
            kreisListe.Add(new Kreis("RR 457 x 45"));
            kreisListe.Add(new Kreis("RR 457 x 50"));
            kreisListe.Add(new Kreis("RR 457 x 60"));
            kreisListe.Add(new Kreis("RR 457 x 70"));
            kreisListe.Add(new Kreis("RR 457 x 80"));
            kreisListe.Add(new Kreis("RR 457 x 90"));
            kreisListe.Add(new Kreis("RR 457 x 100"));
            kreisListe.Add(new Kreis("RR 508 x 6"));
            kreisListe.Add(new Kreis("RR 508 x 6,3"));
            kreisListe.Add(new Kreis("RR 508 x 7,1"));
            kreisListe.Add(new Kreis("RR 508 x 8"));
            kreisListe.Add(new Kreis("RR 508 x 8,8"));
            kreisListe.Add(new Kreis("RR 508 x 10"));
            kreisListe.Add(new Kreis("RR 508 x 11"));
            kreisListe.Add(new Kreis("RR 508 x 12"));
            kreisListe.Add(new Kreis("RR 508 x 12,5"));
            kreisListe.Add(new Kreis("RR 508 x 14,2"));
            kreisListe.Add(new Kreis("RR 508 x 16"));
            kreisListe.Add(new Kreis("RR 508 x 17,5"));
            kreisListe.Add(new Kreis("RR 508 x 20"));
            kreisListe.Add(new Kreis("RR 508 x 25"));
            kreisListe.Add(new Kreis("RR 508 x 30"));
            kreisListe.Add(new Kreis("RR 508 x 36"));
            kreisListe.Add(new Kreis("RR 508 x 40"));
            kreisListe.Add(new Kreis("RR 508 x 45"));
            kreisListe.Add(new Kreis("RR 508 x 50"));
            kreisListe.Add(new Kreis("RR 508 x 60"));
            kreisListe.Add(new Kreis("RR 508 x 70"));
            kreisListe.Add(new Kreis("RR 508 x 80"));
            kreisListe.Add(new Kreis("RR 508 x 90"));
            kreisListe.Add(new Kreis("RR 508 x 100"));
            kreisListe.Add(new Kreis("RR 559 x 12,5"));
            kreisListe.Add(new Kreis("RR 559 x 14,2"));
            kreisListe.Add(new Kreis("RR 559 x 16"));
            kreisListe.Add(new Kreis("RR 559 x 17,5"));
            kreisListe.Add(new Kreis("RR 559 x 20"));
            kreisListe.Add(new Kreis("RR 559 x 25"));
            kreisListe.Add(new Kreis("RR 559 x 30"));
            kreisListe.Add(new Kreis("RR 559 x 36"));
            kreisListe.Add(new Kreis("RR 559 x 40"));
            kreisListe.Add(new Kreis("RR 559 x 45"));
            kreisListe.Add(new Kreis("RR 559 x 50"));
            kreisListe.Add(new Kreis("RR 559 x 60"));
            kreisListe.Add(new Kreis("RR 559 x 70"));
            kreisListe.Add(new Kreis("RR 559 x 80"));
            kreisListe.Add(new Kreis("RR 559 x 90"));
            kreisListe.Add(new Kreis("RR 559 x 100"));
            kreisListe.Add(new Kreis("RR 610 x 6"));
            kreisListe.Add(new Kreis("RR 610 x 6,3"));
            kreisListe.Add(new Kreis("RR 610 x 8"));
            kreisListe.Add(new Kreis("RR 610 x 10"));
            kreisListe.Add(new Kreis("RR 610 x 12"));
            kreisListe.Add(new Kreis("RR 610 x 12,5"));
            kreisListe.Add(new Kreis("RR 610 x 14,2"));
            kreisListe.Add(new Kreis("RR 610 x 16"));
            kreisListe.Add(new Kreis("RR 610 x 17,5"));
            kreisListe.Add(new Kreis("RR 610 x 20"));
            kreisListe.Add(new Kreis("RR 610 x 25"));
            kreisListe.Add(new Kreis("RR 610 x 30"));
            kreisListe.Add(new Kreis("RR 610 x 36"));
            kreisListe.Add(new Kreis("RR 610 x 40"));
            kreisListe.Add(new Kreis("RR 610 x 45"));
            kreisListe.Add(new Kreis("RR 610 x 50"));
            kreisListe.Add(new Kreis("RR 610 x 60"));
            kreisListe.Add(new Kreis("RR 610 x 70"));
            kreisListe.Add(new Kreis("RR 610 x 80"));
            kreisListe.Add(new Kreis("RR 610 x 90"));
            kreisListe.Add(new Kreis("RR 610 x 100"));
            kreisListe.Add(new Kreis("RR 660 x 14,2"));
            kreisListe.Add(new Kreis("RR 660 x 20"));
            kreisListe.Add(new Kreis("RR 660 x 25"));
            kreisListe.Add(new Kreis("RR 660 x 30"));
            kreisListe.Add(new Kreis("RR 660 x 36"));
            kreisListe.Add(new Kreis("RR 660 x 40"));
            kreisListe.Add(new Kreis("RR 660 x 45"));
            kreisListe.Add(new Kreis("RR 660 x 50"));
            kreisListe.Add(new Kreis("RR 660 x 60"));
            kreisListe.Add(new Kreis("RR 660 x 70"));
            kreisListe.Add(new Kreis("RR 660 x 80"));
            kreisListe.Add(new Kreis("RR 660 x 90"));
            kreisListe.Add(new Kreis("RR 660 x 100"));
            kreisListe.Add(new Kreis("RR 711 x 6"));
            kreisListe.Add(new Kreis("RR 711 x 6,3"));
            kreisListe.Add(new Kreis("RR 711 x 8"));
            kreisListe.Add(new Kreis("RR 711 x 10"));
            kreisListe.Add(new Kreis("RR 711 x 12"));
            kreisListe.Add(new Kreis("RR 711 x 12,5"));
            kreisListe.Add(new Kreis("RR 711 x 16"));
            kreisListe.Add(new Kreis("RR 711 x 20"));
            kreisListe.Add(new Kreis("RR 711 x 25"));
            kreisListe.Add(new Kreis("RR 711 x 30"));
            kreisListe.Add(new Kreis("RR 711 x 36"));
            kreisListe.Add(new Kreis("RR 711 x 40"));
            kreisListe.Add(new Kreis("RR 711 x 45"));
            kreisListe.Add(new Kreis("RR 711 x 50"));
            kreisListe.Add(new Kreis("RR 711 x 60"));
            kreisListe.Add(new Kreis("RR 711 x 70"));
            kreisListe.Add(new Kreis("RR 711 x 80"));
            kreisListe.Add(new Kreis("RR 711 x 90"));
            kreisListe.Add(new Kreis("RR 711 x 100"));
            kreisListe.Add(new Kreis("RR 762 x 6"));
            kreisListe.Add(new Kreis("RR 762 x 6,3"));
            kreisListe.Add(new Kreis("RR 762 x 8"));
            kreisListe.Add(new Kreis("RR 762 x 10"));
            kreisListe.Add(new Kreis("RR 762 x 12"));
            kreisListe.Add(new Kreis("RR 762 x 12,5"));
            kreisListe.Add(new Kreis("RR 762 x 16"));
            kreisListe.Add(new Kreis("RR 762 x 20"));
            kreisListe.Add(new Kreis("RR 762 x 25"));
            kreisListe.Add(new Kreis("RR 762 x 30"));
            kreisListe.Add(new Kreis("RR 762 x 40"));
            kreisListe.Add(new Kreis("RR 762 x 50"));
            kreisListe.Add(new Kreis("RR 813 x 8"));
            kreisListe.Add(new Kreis("RR 813 x 10"));
            kreisListe.Add(new Kreis("RR 813 x 12"));
            kreisListe.Add(new Kreis("RR 813 x 12,5"));
            kreisListe.Add(new Kreis("RR 813 x 16"));
            kreisListe.Add(new Kreis("RR 813 x 20"));
            kreisListe.Add(new Kreis("RR 813 x 25"));
            kreisListe.Add(new Kreis("RR 813 x 30"));
            kreisListe.Add(new Kreis("RR 914 x 8"));
            kreisListe.Add(new Kreis("RR 914 x 10"));
            kreisListe.Add(new Kreis("RR 914 x 12"));
            kreisListe.Add(new Kreis("RR 914 x 12,5"));
            kreisListe.Add(new Kreis("RR 914 x 16"));
            kreisListe.Add(new Kreis("RR 914 x 20"));
            kreisListe.Add(new Kreis("RR 914 x 25"));
            kreisListe.Add(new Kreis("RR 914 x 30"));
            kreisListe.Add(new Kreis("RR 1016 x 8"));
            kreisListe.Add(new Kreis("RR 1016 x 10"));
            kreisListe.Add(new Kreis("RR 1016 x 12"));
            kreisListe.Add(new Kreis("RR 1016 x 12,5"));
            kreisListe.Add(new Kreis("RR 1016 x 16"));
            kreisListe.Add(new Kreis("RR 1016 x 20"));
            kreisListe.Add(new Kreis("RR 1016 x 25"));
            kreisListe.Add(new Kreis("RR 1016 x 30"));
            kreisListe.Add(new Kreis("RR 1067 x 10"));
            kreisListe.Add(new Kreis("RR 1067 x 12"));
            kreisListe.Add(new Kreis("RR 1067 x 12,5"));
            kreisListe.Add(new Kreis("RR 1067 x 16"));
            kreisListe.Add(new Kreis("RR 1067 x 20"));
            kreisListe.Add(new Kreis("RR 1067 x 25"));
            kreisListe.Add(new Kreis("RR 1067 x 30"));
            kreisListe.Add(new Kreis("RR 1168 x 10"));
            kreisListe.Add(new Kreis("RR 1168 x 12"));
            kreisListe.Add(new Kreis("RR 1168 x 12,5"));
            kreisListe.Add(new Kreis("RR 1168 x 16"));
            kreisListe.Add(new Kreis("RR 1168 x 20"));
            kreisListe.Add(new Kreis("RR 1168 x 25"));
            kreisListe.Add(new Kreis("RR 1219 x 10"));
            kreisListe.Add(new Kreis("RR 1219 x 12"));
            kreisListe.Add(new Kreis("RR 1219 x 12,5"));
            kreisListe.Add(new Kreis("RR 1219 x 16"));
            kreisListe.Add(new Kreis("RR 1219 x 20"));
            kreisListe.Add(new Kreis("RR 1219 x 25"));

            #endregion

            ProfilartGenau.ItemsSource = rechteckListe;
            #region
            rechteckListe.Add(new Rechteck("FRR warm 40 x 30 x 3"));
            rechteckListe.Add(new Rechteck("FRR warm 50 x 25 x 2,5"));
            rechteckListe.Add(new Rechteck("FRR warm 50 x 25 x 3"));
            rechteckListe.Add(new Rechteck("FRR warm 50 x 30 x 2,5"));
            rechteckListe.Add(new Rechteck("FRR warm 50 x 30 x 2,9"));
            rechteckListe.Add(new Rechteck("FRR warm 50 x 30 x 3"));
            rechteckListe.Add(new Rechteck("FRR warm 50 x 30 x 3,2"));
            rechteckListe.Add(new Rechteck("FRR warm 50 x 30 x 3,6"));
            rechteckListe.Add(new Rechteck("FRR warm 50 x 30 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 50 x 30 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 50 x 30 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 50 x 30 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 50 x 30 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 50 x 30 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 60 x 40 x 2,5"));
            rechteckListe.Add(new Rechteck("FRR warm 60 x 40 x 2,9"));
            rechteckListe.Add(new Rechteck("FRR warm 60 x 40 x 3"));
            rechteckListe.Add(new Rechteck("FRR warm 60 x 40 x 3,2"));
            rechteckListe.Add(new Rechteck("FRR warm 60 x 40 x 3,6"));
            rechteckListe.Add(new Rechteck("FRR warm 60 x 40 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 60 x 40 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 60 x 40 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 60 x 40 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 60 x 40 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 60 x 40 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 60 x 40 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 40 x 2,9"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 40 x 3,2"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 40 x 3,6"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 40 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 40 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 40 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 40 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 40 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 40 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 40 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 40 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 40 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 50 x 3,2"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 50 x 3,6"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 50 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 50 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 50 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 50 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 50 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 50 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 50 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 50 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 50 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 50 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 70 x 50 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 40 x 2,9"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 40 x 3"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 40 x 3,2"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 40 x 3,6"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 40 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 40 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 40 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 40 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 40 x 6"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 40 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 40 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 40 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 40 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 40 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 40 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 40 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 50 x 3,2"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 50 x 3,6"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 50 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 50 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 50 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 50 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 50 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 50 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 50 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 50 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 50 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 50 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 50 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 60 x 3,2"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 60 x 3,6"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 60 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 60 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 60 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 60 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 60 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 60 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 60 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 60 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 60 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 60 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 80 x 60 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 90 x 50 x 3"));
            rechteckListe.Add(new Rechteck("FRR warm 90 x 50 x 3,2"));
            rechteckListe.Add(new Rechteck("FRR warm 90 x 50 x 3,6"));
            rechteckListe.Add(new Rechteck("FRR warm 90 x 50 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 90 x 50 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 90 x 50 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 90 x 50 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 90 x 50 x 6"));
            rechteckListe.Add(new Rechteck("FRR warm 90 x 50 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 90 x 50 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 90 x 50 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 90 x 50 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 90 x 50 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 90 x 50 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 90 x 50 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 50 x 3"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 50 x 3,6"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 50 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 50 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 50 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 50 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 50 x 6"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 50 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 50 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 50 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 50 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 50 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 50 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 50 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 60 x 3"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 60 x 3,6"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 60 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 60 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 60 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 60 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 60 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 60 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 60 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 60 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 60 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 60 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 60 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 80 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 80 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 80 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 80 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 80 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 80 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 80 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 80 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 80 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 80 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 100 x 80 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 110 x 60 x 3,6"));
            rechteckListe.Add(new Rechteck("FRR warm 110 x 60 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 110 x 60 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 110 x 60 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 110 x 60 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 110 x 60 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 110 x 60 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 110 x 60 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 110 x 60 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 110 x 60 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 110 x 60 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 110 x 60 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 60 x 3,6"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 60 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 60 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 60 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 60 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 60 x 6"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 60 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 60 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 60 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 60 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 60 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 60 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 60 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 80 x 3,6"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 80 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 80 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 80 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 80 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 80 x 6"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 80 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 80 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 80 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 80 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 80 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 80 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 80 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 120 x 80 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 70 x 3,6"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 70 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 70 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 70 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 70 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 70 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 70 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 70 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 70 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 70 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 70 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 70 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 70 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 80 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 80 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 80 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 80 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 80 x 6"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 80 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 80 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 80 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 80 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 80 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 80 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 80 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 140 x 80 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 50 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 50 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 50 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 50 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 50 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 50 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 50 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 100 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 100 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 100 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 100 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 100 x 6"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 100 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 100 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 100 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 100 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 100 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 100 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 100 x 12"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 100 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 100 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 150 x 100 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 80 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 80 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 80 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 80 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 80 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 80 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 80 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 80 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 80 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 80 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 80 x 12"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 80 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 80 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 80 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 90 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 90 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 90 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 90 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 90 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 90 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 90 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 90 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 90 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 90 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 90 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 90 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 160 x 90 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 60 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 60 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 60 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 60 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 60 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 80 x 4,5"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 80 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 80 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 80 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 80 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 80 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 80 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 80 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 80 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 80 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 80 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 80 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 100 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 100 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 100 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 100 x 6"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 100 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 100 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 100 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 100 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 100 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 100 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 100 x 12"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 100 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 100 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 100 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 180 x 100 x 17,5"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 100 x 4"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 100 x 5"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 100 x 5,6"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 100 x 6"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 100 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 100 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 100 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 100 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 100 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 100 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 100 x 12"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 100 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 100 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 100 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 100 x 17,5"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 120 x 6"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 120 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 120 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 120 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 120 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 120 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 120 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 120 x 12"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 120 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 120 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 120 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 120 x 17,5"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 150 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 150 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 150 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 150 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 150 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 150 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 150 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 150 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 200 x 150 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 220 x 120 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 220 x 120 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 220 x 120 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 220 x 120 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 220 x 120 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 220 x 120 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 220 x 120 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 220 x 120 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 220 x 120 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 220 x 120 x 17,5"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 100 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 100 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 100 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 100 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 100 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 100 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 100 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 100 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 100 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 150 x 6"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 150 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 150 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 150 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 150 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 150 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 150 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 150 x 12"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 150 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 150 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 250 x 150 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 140 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 140 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 140 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 140 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 140 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 140 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 140 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 140 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 140 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 180 x 6"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 180 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 180 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 180 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 180 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 180 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 180 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 180 x 12"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 180 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 180 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 260 x 180 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 280 x 180 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 280 x 180 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 280 x 180 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 280 x 220 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 280 x 220 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 280 x 220 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 100 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 100 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 100 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 100 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 100 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 100 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 100 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 100 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 100 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 150 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 150 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 150 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 150 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 150 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 150 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 200 x 6"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 200 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 200 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 200 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 200 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 200 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 200 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 200 x 12"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 200 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 200 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 200 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 300 x 200 x 17,5"));
            rechteckListe.Add(new Rechteck("FRR warm 320 x 180 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 320 x 180 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 320 x 180 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 320 x 220 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 320 x 220 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 320 x 220 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 350 x 250 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 350 x 250 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 350 x 250 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 350 x 250 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 350 x 250 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 350 x 250 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 350 x 250 x 12"));
            rechteckListe.Add(new Rechteck("FRR warm 350 x 250 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 350 x 250 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 350 x 250 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 360 x 220 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 360 x 220 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 360 x 220 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 200 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 200 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 200 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 200 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 200 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 200 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 200 x 12"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 200 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 200 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 200 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 300 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 300 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 300 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 300 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 300 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 300 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 300 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 300 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 300 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 260 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 260 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 400 x 260 x 17,5"));
            rechteckListe.Add(new Rechteck("FRR warm 450 x 250 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 450 x 250 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 450 x 250 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 450 x 250 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 450 x 250 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 450 x 250 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 450 x 250 x 12"));
            rechteckListe.Add(new Rechteck("FRR warm 450 x 250 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 450 x 250 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 450 x 250 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 200 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 200 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 200 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 200 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 200 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 200 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 200 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 200 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 200 x 16"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 300 x 6,3"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 300 x 7,1"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 300 x 8"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 300 x 8,8"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 300 x 10"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 300 x 11"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 300 x 12"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 300 x 12,5"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 300 x 14,2"));
            rechteckListe.Add(new Rechteck("FRR warm 500 x 300 x 16"));
            #endregion

            ProfilartGenau.ItemsSource = quadratListe;
            #region
            quadratListe.Add(new Quadrat("FRQ warm 30 x 2"));
            quadratListe.Add(new Quadrat("FRQ warm 30 x 2,5"));
            quadratListe.Add(new Quadrat("FRQ warm 30 x 3"));
            quadratListe.Add(new Quadrat("FRQ warm 40 x 2,5"));
            quadratListe.Add(new Quadrat("FRQ warm 40 x 2,9"));
            quadratListe.Add(new Quadrat("FRQ warm 40 x 3"));
            quadratListe.Add(new Quadrat("FRQ warm 40 x 3,2"));
            quadratListe.Add(new Quadrat("FRQ warm 40 x 3,6"));
            quadratListe.Add(new Quadrat("FRQ warm 40 x 4,5"));
            quadratListe.Add(new Quadrat("FRQ warm 40 x 5"));
            quadratListe.Add(new Quadrat("FRQ warm 40 x 5,6"));
            quadratListe.Add(new Quadrat("FRQ warm 40 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 40 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 50 x 2,5"));
            quadratListe.Add(new Quadrat("FRQ warm 50 x 2,9"));
            quadratListe.Add(new Quadrat("FRQ warm 50 x 3"));
            quadratListe.Add(new Quadrat("FRQ warm 50 x 3,2"));
            quadratListe.Add(new Quadrat("FRQ warm 50 x 3,6"));
            quadratListe.Add(new Quadrat("FRQ warm 50 x 4"));
            quadratListe.Add(new Quadrat("FRQ warm 50 x 4,5"));
            quadratListe.Add(new Quadrat("FRQ warm 50 x 5"));
            quadratListe.Add(new Quadrat("FRQ warm 50 x 5,6"));
            quadratListe.Add(new Quadrat("FRQ warm 50 x 6"));
            quadratListe.Add(new Quadrat("FRQ warm 50 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 50 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 50 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 60 x 2,5"));
            quadratListe.Add(new Quadrat("FRQ warm 60 x 2,9"));
            quadratListe.Add(new Quadrat("FRQ warm 60 x 3"));
            quadratListe.Add(new Quadrat("FRQ warm 60 x 3,2"));
            quadratListe.Add(new Quadrat("FRQ warm 60 x 3,6"));
            quadratListe.Add(new Quadrat("FRQ warm 60 x 4"));
            quadratListe.Add(new Quadrat("FRQ warm 60 x 4,5"));
            quadratListe.Add(new Quadrat("FRQ warm 60 x 5"));
            quadratListe.Add(new Quadrat("FRQ warm 60 x 5,6"));
            quadratListe.Add(new Quadrat("FRQ warm 60 x 6"));
            quadratListe.Add(new Quadrat("FRQ warm 60 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 60 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 60 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 60 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 60 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 60 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 60 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 70 x 3"));
            quadratListe.Add(new Quadrat("FRQ warm 70 x 3,2"));
            quadratListe.Add(new Quadrat("FRQ warm 70 x 3,6"));
            quadratListe.Add(new Quadrat("FRQ warm 70 x 4"));
            quadratListe.Add(new Quadrat("FRQ warm 70 x 4,5"));
            quadratListe.Add(new Quadrat("FRQ warm 70 x 5"));
            quadratListe.Add(new Quadrat("FRQ warm 70 x 5,6"));
            quadratListe.Add(new Quadrat("FRQ warm 70 x 6"));
            quadratListe.Add(new Quadrat("FRQ warm 70 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 70 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 70 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 70 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 70 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 70 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 70 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 80 x 3"));
            quadratListe.Add(new Quadrat("FRQ warm 80 x 3,6"));
            quadratListe.Add(new Quadrat("FRQ warm 80 x 4"));
            quadratListe.Add(new Quadrat("FRQ warm 80 x 4,5"));
            quadratListe.Add(new Quadrat("FRQ warm 80 x 5"));
            quadratListe.Add(new Quadrat("FRQ warm 80 x 5,6"));
            quadratListe.Add(new Quadrat("FRQ warm 80 x 6"));
            quadratListe.Add(new Quadrat("FRQ warm 80 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 80 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 80 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 80 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 80 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 80 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 80 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 90 x 3,6"));
            quadratListe.Add(new Quadrat("FRQ warm 90 x 4"));
            quadratListe.Add(new Quadrat("FRQ warm 90 x 4,5"));
            quadratListe.Add(new Quadrat("FRQ warm 90 x 5"));
            quadratListe.Add(new Quadrat("FRQ warm 90 x 5,6"));
            quadratListe.Add(new Quadrat("FRQ warm 90 x 6"));
            quadratListe.Add(new Quadrat("FRQ warm 90 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 90 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 90 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 90 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 90 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 90 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 90 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 100 x 3,6"));
            quadratListe.Add(new Quadrat("FRQ warm 100 x 4"));
            quadratListe.Add(new Quadrat("FRQ warm 100 x 4,5"));
            quadratListe.Add(new Quadrat("FRQ warm 100 x 5"));
            quadratListe.Add(new Quadrat("FRQ warm 100 x 5,6"));
            quadratListe.Add(new Quadrat("FRQ warm 100 x 6"));
            quadratListe.Add(new Quadrat("FRQ warm 100 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 100 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 100 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 100 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 100 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 100 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 100 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 100 x 14,2"));
            quadratListe.Add(new Quadrat("FRQ warm 110 x 4"));
            quadratListe.Add(new Quadrat("FRQ warm 110 x 4,5"));
            quadratListe.Add(new Quadrat("FRQ warm 110 x 5"));
            quadratListe.Add(new Quadrat("FRQ warm 110 x 5,6"));
            quadratListe.Add(new Quadrat("FRQ warm 110 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 110 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 110 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 110 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 110 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 110 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 110 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 110 x 14,2"));
            quadratListe.Add(new Quadrat("FRQ warm 120 x 4"));
            quadratListe.Add(new Quadrat("FRQ warm 120 x 4,5"));
            quadratListe.Add(new Quadrat("FRQ warm 120 x 5"));
            quadratListe.Add(new Quadrat("FRQ warm 120 x 5,6"));
            quadratListe.Add(new Quadrat("FRQ warm 120 x 6"));
            quadratListe.Add(new Quadrat("FRQ warm 120 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 120 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 120 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 120 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 120 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 120 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 120 x 12"));
            quadratListe.Add(new Quadrat("FRQ warm 120 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 120 x 14,2"));
            quadratListe.Add(new Quadrat("FRQ warm 120 x 16"));
            quadratListe.Add(new Quadrat("FRQ warm 130 x 5"));
            quadratListe.Add(new Quadrat("FRQ warm 130 x 5,6"));
            quadratListe.Add(new Quadrat("FRQ warm 130 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 130 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 130 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 130 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 130 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 130 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 130 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 130 x 14,2"));
            quadratListe.Add(new Quadrat("FRQ warm 130 x 16"));
            quadratListe.Add(new Quadrat("FRQ warm 140 x 5"));
            quadratListe.Add(new Quadrat("FRQ warm 140 x 5,6"));
            quadratListe.Add(new Quadrat("FRQ warm 140 x 6"));
            quadratListe.Add(new Quadrat("FRQ warm 140 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 140 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 140 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 140 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 140 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 140 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 140 x 12"));
            quadratListe.Add(new Quadrat("FRQ warm 140 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 140 x 14,2"));
            quadratListe.Add(new Quadrat("FRQ warm 140 x 16"));
            quadratListe.Add(new Quadrat("FRQ warm 140 x 17,5"));
            quadratListe.Add(new Quadrat("FRQ warm 150 x 5"));
            quadratListe.Add(new Quadrat("FRQ warm 150 x 5,6"));
            quadratListe.Add(new Quadrat("FRQ warm 150 x 6"));
            quadratListe.Add(new Quadrat("FRQ warm 150 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 150 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 150 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 150 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 150 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 150 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 150 x 12"));
            quadratListe.Add(new Quadrat("FRQ warm 150 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 150 x 14,2"));
            quadratListe.Add(new Quadrat("FRQ warm 150 x 16"));
            quadratListe.Add(new Quadrat("FRQ warm 150 x 17,5"));
            quadratListe.Add(new Quadrat("FRQ warm 160 x 5"));
            quadratListe.Add(new Quadrat("FRQ warm 160 x 6"));
            quadratListe.Add(new Quadrat("FRQ warm 160 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 160 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 160 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 160 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 160 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 160 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 160 x 12"));
            quadratListe.Add(new Quadrat("FRQ warm 160 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 160 x 14,2"));
            quadratListe.Add(new Quadrat("FRQ warm 160 x 16"));
            quadratListe.Add(new Quadrat("FRQ warm 160 x 17,5"));
            quadratListe.Add(new Quadrat("FRQ warm 180 x 5"));
            quadratListe.Add(new Quadrat("FRQ warm 180 x 5,6"));
            quadratListe.Add(new Quadrat("FRQ warm 180 x 6"));
            quadratListe.Add(new Quadrat("FRQ warm 180 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 180 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 180 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 180 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 180 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 180 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 180 x 12"));
            quadratListe.Add(new Quadrat("FRQ warm 180 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 180 x 14,2"));
            quadratListe.Add(new Quadrat("FRQ warm 180 x 16"));
            quadratListe.Add(new Quadrat("FRQ warm 200 x 5"));
            quadratListe.Add(new Quadrat("FRQ warm 200 x 5,6"));
            quadratListe.Add(new Quadrat("FRQ warm 200 x 6"));
            quadratListe.Add(new Quadrat("FRQ warm 200 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 200 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 200 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 200 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 200 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 200 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 200 x 12"));
            quadratListe.Add(new Quadrat("FRQ warm 200 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 200 x 14,2"));
            quadratListe.Add(new Quadrat("FRQ warm 200 x 16"));
            quadratListe.Add(new Quadrat("FRQ warm 220 x 6"));
            quadratListe.Add(new Quadrat("FRQ warm 220 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 220 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 220 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 220 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 220 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 220 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 220 x 12"));
            quadratListe.Add(new Quadrat("FRQ warm 220 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 220 x 14,2"));
            quadratListe.Add(new Quadrat("FRQ warm 220 x 16"));
            quadratListe.Add(new Quadrat("FRQ warm 250 x 5"));
            quadratListe.Add(new Quadrat("FRQ warm 250 x 5,6"));
            quadratListe.Add(new Quadrat("FRQ warm 250 x 6"));
            quadratListe.Add(new Quadrat("FRQ warm 250 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 250 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 250 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 250 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 250 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 250 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 250 x 12"));
            quadratListe.Add(new Quadrat("FRQ warm 250 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 250 x 14,2"));
            quadratListe.Add(new Quadrat("FRQ warm 250 x 16"));
            quadratListe.Add(new Quadrat("FRQ warm 250 x 17,5"));
            quadratListe.Add(new Quadrat("FRQ warm 260 x 6"));
            quadratListe.Add(new Quadrat("FRQ warm 260 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 260 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 260 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 260 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 260 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 260 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 260 x 12"));
            quadratListe.Add(new Quadrat("FRQ warm 260 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 260 x 14,2"));
            quadratListe.Add(new Quadrat("FRQ warm 260 x 16"));
            quadratListe.Add(new Quadrat("FRQ warm 260 x 17,5"));
            quadratListe.Add(new Quadrat("FRQ warm 300 x 6"));
            quadratListe.Add(new Quadrat("FRQ warm 300 x 6,3"));
            quadratListe.Add(new Quadrat("FRQ warm 300 x 7,1"));
            quadratListe.Add(new Quadrat("FRQ warm 300 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 300 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 300 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 300 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 300 x 12"));
            quadratListe.Add(new Quadrat("FRQ warm 300 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 300 x 14,2"));
            quadratListe.Add(new Quadrat("FRQ warm 300 x 16"));
            quadratListe.Add(new Quadrat("FRQ warm 300 x 17,5"));
            quadratListe.Add(new Quadrat("FRQ warm 350 x 8"));
            quadratListe.Add(new Quadrat("FRQ warm 350 x 8,8"));
            quadratListe.Add(new Quadrat("FRQ warm 350 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 350 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 350 x 12"));
            quadratListe.Add(new Quadrat("FRQ warm 350 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 350 x 14,2"));
            quadratListe.Add(new Quadrat("FRQ warm 350 x 16"));
            quadratListe.Add(new Quadrat("FRQ warm 350 x 17,5"));
            quadratListe.Add(new Quadrat("FRQ warm 400 x 10"));
            quadratListe.Add(new Quadrat("FRQ warm 400 x 11"));
            quadratListe.Add(new Quadrat("FRQ warm 400 x 12"));
            quadratListe.Add(new Quadrat("FRQ warm 400 x 12,5"));
            quadratListe.Add(new Quadrat("FRQ warm 400 x 14,2"));
            quadratListe.Add(new Quadrat("FRQ warm 400 x 16"));
            #endregion

            //Zusammengesetzte Querschnitte

            ProfilartGenau.ItemsSource = kastenListe;
            #region
            kastenListe.Add(new Kasten("KASTEN 200/5/290/12/500/8"));
            kastenListe.Add(new Kasten("KASTEN 200/5/290/15/500/8"));
            kastenListe.Add(new Kasten("KASTEN 200/5/290/15/500/10"));
            kastenListe.Add(new Kasten("KASTEN 200/5/290/20/500/10"));
            kastenListe.Add(new Kasten("KASTEN 200/5/290/20/500/12"));
            kastenListe.Add(new Kasten("KASTEN 200/5/290/25/500/12"));
            kastenListe.Add(new Kasten("KASTEN 200/5/290/25/500/15"));
            kastenListe.Add(new Kasten("KASTEN 200/5/290/30/500/15"));
            kastenListe.Add(new Kasten("KASTEN 200/5/290/30/500/20"));
            kastenListe.Add(new Kasten("KASTEN 200/5/290/35/500/20"));
            kastenListe.Add(new Kasten("KASTEN 200/5/290/40/500/20"));
            kastenListe.Add(new Kasten("KASTEN 200/5/290/40/500/25"));
            kastenListe.Add(new Kasten("KASTEN 265/6/290/15/500/10"));
            kastenListe.Add(new Kasten("KASTEN 265/6/290/20/500/10"));
            kastenListe.Add(new Kasten("KASTEN 265/6/290/20/500/12"));
            kastenListe.Add(new Kasten("KASTEN 265/6/290/25/500/12"));
            kastenListe.Add(new Kasten("KASTEN 265/6/290/25/500/15"));
            kastenListe.Add(new Kasten("KASTEN 265/6/290/30/500/15"));
            kastenListe.Add(new Kasten("KASTEN 265/6/290/30/500/20"));
            kastenListe.Add(new Kasten("KASTEN 265/6/290/35/500/20"));
            kastenListe.Add(new Kasten("KASTEN 265/6/290/40/500/20"));
            kastenListe.Add(new Kasten("KASTEN 265/6/290/35/500/25"));
            kastenListe.Add(new Kasten("KASTEN 265/6/290/40/500/25"));
            kastenListe.Add(new Kasten("KASTEN 320/8/290/20/500/12"));
            kastenListe.Add(new Kasten("KASTEN 320/8/290/25/500/12"));
            kastenListe.Add(new Kasten("KASTEN 320/8/290/25/500/15"));
            kastenListe.Add(new Kasten("KASTEN 320/8/290/30/500/15"));
            kastenListe.Add(new Kasten("KASTEN 320/8/290/30/500/20"));
            kastenListe.Add(new Kasten("KASTEN 320/8/290/35/500/20"));
            kastenListe.Add(new Kasten("KASTEN 320/8/290/40/500/20"));
            kastenListe.Add(new Kasten("KASTEN 320/8/290/35/500/25"));
            kastenListe.Add(new Kasten("KASTEN 320/8/290/40/500/25"));
            kastenListe.Add(new Kasten("KASTEN 400/10/290/25/500/15"));
            kastenListe.Add(new Kasten("KASTEN 400/10/290/30/500/15"));
            kastenListe.Add(new Kasten("KASTEN 400/10/290/30/500/20"));
            kastenListe.Add(new Kasten("KASTEN 400/10/290/35/500/20"));
            kastenListe.Add(new Kasten("KASTEN 400/10/290/40/500/20"));
            kastenListe.Add(new Kasten("KASTEN 400/10/290/35/500/25"));
            kastenListe.Add(new Kasten("KASTEN 400/10/290/40/500/25"));
            kastenListe.Add(new Kasten("KASTEN ugls. 200/5/240/10/365/10"));
            kastenListe.Add(new Kasten("KASTEN ugls. 200/5/240/15/365/10"));
            kastenListe.Add(new Kasten("KASTEN ugls. 200/5/240/15/365/12"));
            kastenListe.Add(new Kasten("KASTEN ugls. 200/5/240/20/365/12"));
            kastenListe.Add(new Kasten("KASTEN ugls. 200/5/240/20/365/15"));
            kastenListe.Add(new Kasten("KASTEN ugls. 200/5/240/25/365/12"));
            kastenListe.Add(new Kasten("KASTEN ugls. 200/5/240/25/365/15"));
            kastenListe.Add(new Kasten("KASTEN ugls. 200/5/240/30/365/15"));
            kastenListe.Add(new Kasten("KASTEN ugls. 200/5/240/30/365/20"));
            kastenListe.Add(new Kasten("KASTEN ugls. 200/5/240/35/365/20"));
            kastenListe.Add(new Kasten("KASTEN ugls. 200/5/240/40/365/20"));
            kastenListe.Add(new Kasten("KASTEN ugls. 200/5/240/40/365/25"));
            kastenListe.Add(new Kasten("KASTEN ugls. 265/5/290/15/415/10"));
            kastenListe.Add(new Kasten("KASTEN ugls. 265/5/290/20/415/12"));
            kastenListe.Add(new Kasten("KASTEN ugls. 265/5/290/20/415/15"));
            kastenListe.Add(new Kasten("KASTEN ugls. 265/5/290/25/415/12"));
            kastenListe.Add(new Kasten("KASTEN ugls. 265/5/290/25/415/15"));
            kastenListe.Add(new Kasten("KASTEN ugls. 265/5/290/30/415/15"));
            kastenListe.Add(new Kasten("KASTEN ugls. 265/5/290/30/415/20"));
            kastenListe.Add(new Kasten("KASTEN ugls. 265/5/290/35/415/20"));
            kastenListe.Add(new Kasten("KASTEN ugls. 265/5/290/35/415/25"));
            kastenListe.Add(new Kasten("KASTEN ugls. 265/5/290/40/415/20"));
            kastenListe.Add(new Kasten("KASTEN ugls. 265/5/290/40/415/25"));
            kastenListe.Add(new Kasten("KASTEN ugls. 320/5/290/20/415/12"));
            kastenListe.Add(new Kasten("KASTEN ugls. 320/5/290/25/415/12"));
            kastenListe.Add(new Kasten("KASTEN ugls. 320/5/290/25/415/15"));
            kastenListe.Add(new Kasten("KASTEN ugls. 320/5/290/30/415/15"));
            kastenListe.Add(new Kasten("KASTEN ugls. 320/5/290/30/415/20"));
            kastenListe.Add(new Kasten("KASTEN ugls. 320/5/290/35/415/20"));
            kastenListe.Add(new Kasten("KASTEN ugls. 320/5/290/35/415/25"));
            kastenListe.Add(new Kasten("KASTEN ugls. 320/5/290/40/415/20"));
            kastenListe.Add(new Kasten("KASTEN ugls. 320/5/290/40/415/25"));
            kastenListe.Add(new Kasten("KASTEN ugls. 400/5/290/25/407,5/15"));
            kastenListe.Add(new Kasten("KASTEN ugls. 400/5/290/30/407,5/15"));
            kastenListe.Add(new Kasten("KASTEN ugls. 400/5/290/30/407,5/20"));
            kastenListe.Add(new Kasten("KASTEN ugls. 400/5/290/35/407,5/20"));
            kastenListe.Add(new Kasten("KASTEN ugls. 400/5/290/35/407,5/25"));
            kastenListe.Add(new Kasten("KASTEN ugls. 400/5/290/40/407,5/20"));
            kastenListe.Add(new Kasten("KASTEN ugls. 400/5/290/40/407,5/25"));
            #endregion

            ProfilartGenau.ItemsSource = itragerListe;
            #region
            itragerListe.Add(new Itrager("HEA 180 & BL 400x10"));
            itragerListe.Add(new Itrager("HEB 180 + BL 400x10"));
            itragerListe.Add(new Itrager("HEB 180 + BL 400x12"));
            itragerListe.Add(new Itrager("HEM 160 + BL 400x10"));
            itragerListe.Add(new Itrager("HEM 160 + BL 400x12"));
            itragerListe.Add(new Itrager("HEM 160 + BL 400x15"));
            itragerListe.Add(new Itrager("HEM 160 + BL 400x20"));
            itragerListe.Add(new Itrager("HEA 200 + BL 400x10"));
            itragerListe.Add(new Itrager("HEB 200 + BL 400x10"));
            itragerListe.Add(new Itrager("HEB 200 + BL 400x12"));
            itragerListe.Add(new Itrager("HEB 200 + BL 400x15"));
            itragerListe.Add(new Itrager("HEM 180 + BL 400x10"));
            itragerListe.Add(new Itrager("HEM 180 + BL 400x12"));
            itragerListe.Add(new Itrager("HEM 180 + BL 400x15"));
            itragerListe.Add(new Itrager("HEM 180 + BL 400x20"));
            itragerListe.Add(new Itrager("HEA 260 + BL 450x12"));
            itragerListe.Add(new Itrager("HEB 260 + BL 450x12"));
            itragerListe.Add(new Itrager("HEB 260 + BL 450x15"));
            itragerListe.Add(new Itrager("HEM 240 + BL 450x12"));
            itragerListe.Add(new Itrager("HEM 240 + BL 450x15"));
            itragerListe.Add(new Itrager("HEM 240 + BL 450x20"));
            itragerListe.Add(new Itrager("HEM 240 + BL 450x25"));
            itragerListe.Add(new Itrager("HEA 320 + BL 500x12"));
            itragerListe.Add(new Itrager("HEA 320 + BL 500x15"));
            itragerListe.Add(new Itrager("HEB 320 + BL 500x12"));
            itragerListe.Add(new Itrager("HEB 320 + BL 500x15"));
            itragerListe.Add(new Itrager("HEB 320 + BL 500x20"));
            itragerListe.Add(new Itrager("HEC 300 + BL 500x12"));
            itragerListe.Add(new Itrager("HEC 300 + BL 500x15"));
            itragerListe.Add(new Itrager("HEM 280 + BL 500x12"));
            itragerListe.Add(new Itrager("HEM 280 + BL 500x15"));
            itragerListe.Add(new Itrager("HEC 300 + BL 500x20"));
            itragerListe.Add(new Itrager("HEM 280 + BL 500x20"));
            itragerListe.Add(new Itrager("HEC 300 + BL 500x25"));
            itragerListe.Add(new Itrager("HEM 280 + BL 500x25"));
            itragerListe.Add(new Itrager("HEA 400 + BL 500x15"));
            itragerListe.Add(new Itrager("HEA 400 + BL 500x20"));
            itragerListe.Add(new Itrager("HEB 400 + BL 500x15"));
            itragerListe.Add(new Itrager("HEB 400 + BL 500x20"));
            itragerListe.Add(new Itrager("HEB 400 + BL 500x25"));
            itragerListe.Add(new Itrager("HEM 360 + BL 500x15"));
            itragerListe.Add(new Itrager("HEM 360 + BL 500x20"));
            itragerListe.Add(new Itrager("HEM 360 + BL 500x25"));
            itragerListe.Add(new Itrager("1/2 HEA 300 + BL 500x10"));
            itragerListe.Add(new Itrager("1/2 HEB 300 + BL 500x10"));
            itragerListe.Add(new Itrager("1/2 HEB 300 + BL 500x12"));
            itragerListe.Add(new Itrager("1/2 HEC 300 + BL 500x15"));
            itragerListe.Add(new Itrager("1/2 HEC 300 + BL 500x20"));
            itragerListe.Add(new Itrager("1/2 HEM 300 + BL 500x20"));
            itragerListe.Add(new Itrager("1/2 HEM 300 + BL 500x25"));
            itragerListe.Add(new Itrager("1/2 HEA 360 + BL 500x10"));
            itragerListe.Add(new Itrager("1/2 HEA 360 + BL 500x12"));
            itragerListe.Add(new Itrager("1/2 HEB 360 + BL 500x12"));
            itragerListe.Add(new Itrager("1/2 HEB 360 + BL 500x15"));
            itragerListe.Add(new Itrager("1/2 HEM 320 + BL 500x20"));
            itragerListe.Add(new Itrager("1/2 HEM 320 + BL 500x25"));
            itragerListe.Add(new Itrager("1/2 HEA 400 + BL 500x10"));
            itragerListe.Add(new Itrager("1/2 HEA 400 + BL 500x12"));
            itragerListe.Add(new Itrager("1/2 HEB 400 + BL 500x12"));
            itragerListe.Add(new Itrager("1/2 HEB 400 + BL 500x15"));
            itragerListe.Add(new Itrager("1/2 HEM 400 + BL 500x20"));
            itragerListe.Add(new Itrager("1/2 HEM 400 + BL 500x25"));
            itragerListe.Add(new Itrager("1/2 HEA 500 + BL 500x12"));
            itragerListe.Add(new Itrager("1/2 HEA 500 + BL 500x15"));
            itragerListe.Add(new Itrager("1/2 HEB 500 + BL 500x15"));
            itragerListe.Add(new Itrager("1/2 HEB 500 + BL 500x20"));
            itragerListe.Add(new Itrager("1/2 HEM 500 + BL 500x20"));
            itragerListe.Add(new Itrager("1/2 HEM 500 + BL 500x25"));
            itragerListe.Add(new Itrager("1/2 HEA 650 + BL 500x15"));
            itragerListe.Add(new Itrager("1/2 HEB 650 + BL 500x15"));
            itragerListe.Add(new Itrager("1/2 HEB 650 + BL 500x20"));
            itragerListe.Add(new Itrager("1/2 HEM 650 + BL 500x20"));
            itragerListe.Add(new Itrager("1/2 HEM 650 + BL 500x25"));
            itragerListe.Add(new Itrager("1/2 HEA 800 + BL 500x15"));
            itragerListe.Add(new Itrager("1/2 HEA 800 + BL 500x20"));
            itragerListe.Add(new Itrager("1/2 HEB 800 + BL 500x20"));
            itragerListe.Add(new Itrager("1/2 HEM 800 + BL 500x20"));
            itragerListe.Add(new Itrager("1/2 HEM 800 + BL 500x25"));
            itragerListe.Add(new Itrager("1/2 IPE 500 + BL 400x10"));
            itragerListe.Add(new Itrager("1/2 IPEo 500 + BL 400x10"));
            itragerListe.Add(new Itrager("1/2 IPEv 500 + BL 400x10"));
            itragerListe.Add(new Itrager("1/2 IPEv 500 + BL 400x12"));
            itragerListe.Add(new Itrager("1/2 IPE 600 + BL 450x10"));
            itragerListe.Add(new Itrager("1/2 IPEo 600 + BL 450x10"));
            itragerListe.Add(new Itrager("1/2 IPEo 600 + BL 450x12"));
            #endregion

            #endregion

        }


        //Werte aus Excel-Liste suchen und den Textfeldern zuweisen
        #region
        private string SucheUmfangInExcel(string profilName)
        {
            string umfang = null;

            // Pfad zur Excel-Datei

            string projectDirectory = Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName).Parent.FullName;
            string pathToExcel = System.IO.Path.Combine(projectDirectory, "Profile.xlsx");

            using (var package = new ExcelPackage(new FileInfo(pathToExcel)))
            {
                var worksheet = package.Workbook.Worksheets["Tabelle1"];

                if (worksheet != null)
                {
                    // Profilnamen befinden sich in Spalte A, Umfang in Spalte G
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Starte bei Zeile 2, um Header zu überspringen
                    {
                        var profilInTabelle = worksheet.Cells[row, 1].Value?.ToString(); // Wert in Spalte A
                        var umfangInTabelle = worksheet.Cells[row, 7].Value?.ToString(); // Wert in Spalte G

                        if (profilInTabelle == profilName)
                        {
                            umfang = umfangInTabelle;
                            break; // Profil gefunden, Schleife beenden
                        }
                    }
                }
            }

            return umfang;
        }
        private string SucheGewichtInExcel(string profilName)
        {
            string gewicht = null;

            // Pfad zur Excel-Datei
            string projectDirectory = Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName).Parent.FullName;
            string pathToExcel = System.IO.Path.Combine(projectDirectory, "Profile.xlsx");

            using (var package = new ExcelPackage(new FileInfo(pathToExcel)))
            {
                var worksheet = package.Workbook.Worksheets["Tabelle1"];

                if (worksheet != null)
                {
                    // Profilnamen befinden sich in Spalte A, Gewicht in Spalte F
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Starte bei Zeile 2, um Header zu überspringen
                    {
                        var profilInTabelle = worksheet.Cells[row, 1].Value?.ToString(); // Wert in Spalte A
                        var GewichtInTabelle = worksheet.Cells[row, 6].Value?.ToString(); // Wert in Spalte F

                        if (profilInTabelle == profilName)
                        {
                            gewicht = GewichtInTabelle;
                            break; // Profil gefunden, Schleife beenden
                        }
                    }
                }
            }

            return gewicht;
        }

        private string SucheHöheInExcel(string profilName)
        {
            string höhe = null;

            // Pfad zur Excel-Datei
            string projectDirectory = Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName).Parent.FullName;
            string pathToExcel = System.IO.Path.Combine(projectDirectory, "Profile.xlsx");

            using (var package = new ExcelPackage(new FileInfo(pathToExcel)))
            {
                var worksheet = package.Workbook.Worksheets["Tabelle1"];

                if (worksheet != null)
                {
                    // Profilnamen befinden sich in Spalte A, Höhe in Spalte B
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Starte bei Zeile 2, um Header zu überspringen
                    {
                        var profilInTabelle = worksheet.Cells[row, 1].Value?.ToString(); // Wert in Spalte A
                        var höheInTabelle = worksheet.Cells[row, 2].Value?.ToString(); // Wert in Spalte B

                        if (profilInTabelle == profilName)
                        {
                            höhe = höheInTabelle;
                            break; // Profil gefunden, Schleife beenden
                        }
                    }
                }
            }

            return höhe;
        }

        private string SucheBreiteInExcel(string profilName)
        {
            string breite = null;

            // Pfad zur Excel-Datei
            string projectDirectory = Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName).Parent.FullName;
            string pathToExcel = System.IO.Path.Combine(projectDirectory, "Profile.xlsx");

            using (var package = new ExcelPackage(new FileInfo(pathToExcel)))
            {
                var worksheet = package.Workbook.Worksheets["Tabelle1"];

                if (worksheet != null)
                {
                    // Annahme: Profilnamen befinden sich in Spalte A, Breite in Spalte C
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Starte bei Zeile 2, um Header zu überspringen
                    {
                        var profilInTabelle = worksheet.Cells[row, 1].Value?.ToString(); // Wert in Spalte A
                        var breiteInTabelle = worksheet.Cells[row, 3].Value?.ToString(); // Wert in Spalte C

                        if (profilInTabelle == profilName)
                        {
                            breite = breiteInTabelle;
                            break; // Profil gefunden, Schleife beenden
                        }
                    }
                }
            }

            return breite;
        }

        private string SucheStegbreiteInExcel(string profilName)
        {
            string stegbreite = null;

            // Pfad zur Excel-Datei
            string projectDirectory = Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName).Parent.FullName;
            string pathToExcel = System.IO.Path.Combine(projectDirectory, "Profile.xlsx");

            using (var package = new ExcelPackage(new FileInfo(pathToExcel)))
            {
                var worksheet = package.Workbook.Worksheets["Tabelle1"];

                if (worksheet != null)
                {
                    // Annahme: Profilnamen befinden sich in Spalte A, Stegbreite in Spalte D
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Starte bei Zeile 2, um Header zu überspringen
                    {
                        var profilInTabelle = worksheet.Cells[row, 1].Value?.ToString(); // Wert in Spalte A
                        var stegbreiteInTabelle = worksheet.Cells[row, 4].Value?.ToString(); // Wert in Spalte D

                        if (profilInTabelle == profilName)
                        {
                            stegbreite = stegbreiteInTabelle;
                            break; // Profil gefunden, Schleife beenden
                        }
                    }
                }
            }

            return stegbreite;
        }

        private string SucheBauteilhöhe4InExcel(string profilName)
        {
            string bauteil4 = null;

            // Pfad zur Excel-Datei
            string projectDirectory = Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName).Parent.FullName;
            string pathToExcel = System.IO.Path.Combine(projectDirectory, "Profile.xlsx");

            using (var package = new ExcelPackage(new FileInfo(pathToExcel)))
            {
                var worksheet = package.Workbook.Worksheets["Tabelle1"];

                if (worksheet != null)
                {
                    // Annahme: Profilnamen befinden sich in Spalte A, Bauteilhöhe in Spalte E
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Starte bei Zeile 2, um Header zu überspringen
                    {
                        var profilInTabelle = worksheet.Cells[row, 1].Value?.ToString(); // Wert in Spalte A
                        var bauteil4InTabelle = worksheet.Cells[row, 5].Value?.ToString(); // Wert in Spalte E

                        if (profilInTabelle == profilName)
                        {
                            bauteil4 = bauteil4InTabelle;
                            break; // Profil gefunden, Schleife beenden
                        }
                    }
                }
            }

            return bauteil4;
        }
        #endregion


        private void ReturnToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            // Zurück zum MainWindow navigieren
            ((MainWindow)Application.Current.MainWindow).NavigateToMainWindow();
        }

        private void Berechnen_Click(object sender, RoutedEventArgs e)
        {
            //Informationen speichern und weitergeben: 
            string längebauteil = LängeBauteil.Text;
            string laufmeter = Gewicht.Text;
            string stahlpreis = Stahlpreis.Text;
            string stückzahl = Stückzahl.Text;
            string breite = Bauteilbreite2.Text;
            string höhe = Bauteilhöhe4.Text;
            string flansch = Flanschbreite3.Text;
            string bauteillänge = Bauteillänge1.Text;
            string bauvorhaben = Bauvorhaben.Text;
            string projektnummer = Projektnummer.Text;
            string umfang = UmfangBauteil.Text;
            string lohnkostenproduktion = LohnkostenProduktion.Text;
            string lohnkostenmontage = LohnkostenMontage.Text;
            string bauteilart1 = Bauteilart.Text;
            string profilartgenauseite1 = ProfilartGenau.Text;
            string stahlgüte1 = Stahlgüte.Text;
                        
            profilartgenauseite1 = getProfilartGenauText();
            bauteilart1 = getBauteilartText();
            stahlgüte1 = getStahlgüteText();

            int bauteilartIndex = Bauteilart.SelectedIndex;
            int profilartIndex = ProfilartGenau.SelectedIndex;
            int stahlgüteIndex = Stahlgüte.SelectedIndex;

            Datenspeicher.BauteilartIndex = bauteilartIndex;
            Datenspeicher.ProfilartIndex = profilartIndex;
            Datenspeicher.StahlgüteIndex = stahlgüteIndex;
            
            Datenspeicher.ProfilartGenau = profilartgenauseite1;
            Datenspeicher.Bauteilart1 = bauteilart1;
            Datenspeicher.Stahlgüte1 = stahlgüte1;
                       
            Datenspeicher.LängeBauteilWert = längebauteil;
            Datenspeicher.Gewicht = laufmeter;
            Datenspeicher.Stahlpreis = stahlpreis;
            Datenspeicher.Stückzahl = stückzahl;
            Datenspeicher.Bauteilbreite2 = breite;
            Datenspeicher.Bauteilhöhe4 = höhe;
            Datenspeicher.Flanschbreite3 = flansch;
            Datenspeicher.Bauteillänge1 = bauteillänge;
            Datenspeicher.Bauvorhaben = bauvorhaben;
            Datenspeicher.Projektnummer = projektnummer;
            Datenspeicher.UmfangBauteil = umfang;
            Datenspeicher.LohnkostenProduktion = lohnkostenproduktion;
            Datenspeicher.LohnkostenMontage = lohnkostenmontage;

            //Messageboxen bei leeren Feldern: 
            #region

            bool alleBedingungenErfuellt = true;

            if (string.IsNullOrEmpty(Gewicht.Text))
            {
                MessageBox.Show("Bitte geben Sie das Laufmetergewicht ein.");
                alleBedingungenErfuellt = false; // Eine Bedingung wurde nicht erfüllt
            }
            else if (double.TryParse(Gewicht.Text, out double gewichtWert))
            {
                if (gewichtWert > 500)
                {
                    MessageBox.Show("Das eingegebene Laufmetergewicht übersteigt die Kapazitäten der Produktion. Die Obergrenze liegt bei 500 kg/m.");
                    alleBedingungenErfuellt = false; // Eine Bedingung wurde nicht erfüllt
                }
            }
            else
            {
                MessageBox.Show("Bitte geben Sie eine gültige Zahl für das Laufmetergewicht ein.");
                alleBedingungenErfuellt = false; // Eine Bedingung wurde nicht erfüllt
            }

            if (string.IsNullOrEmpty(Bauteillänge1.Text))
            {
                MessageBox.Show("Bitte geben Sie die Bauteilhöhe ein.");
                alleBedingungenErfuellt = false; // Eine Bedingung wurde nicht erfüllt
            }
            else if (double.TryParse(Bauteillänge1.Text, out double bauteilLänge) && bauteilLänge <= 1)
            {
                MessageBox.Show("Bitte geben Sie eine gültige Zahl größer als 1 für die Bauteilhöhe an.");
                alleBedingungenErfuellt = false; // Eine Bedingung wurde nicht erfüllt
            }
            if (string.IsNullOrEmpty(Bauteilbreite2.Text))
            {
                MessageBox.Show("Bitte geben Sie die Flanschbreite / Breite ein.");
                alleBedingungenErfuellt = false; // Eine Bedingung wurde nicht erfüllt
            }
            else if (double.TryParse(Bauteilbreite2.Text, out double bauteilbreite) && bauteilbreite <= 1)
            {
                MessageBox.Show("Bitte geben Sie eine gültige Zahl größer als 1 für die Flanschbreite / Breite an.");
                alleBedingungenErfuellt = false; // Eine Bedingung wurde nicht erfüllt
            }
            if (string.IsNullOrEmpty(Flanschbreite3.Text))
            {
                MessageBox.Show("Bitte geben Sie die Stegbreite / Profildicke ein.");
                alleBedingungenErfuellt = false; // Eine Bedingung wurde nicht erfüllt
            }
            else if (double.TryParse(Flanschbreite3.Text, out double flanschbreite) && flanschbreite <= 1)
            {
                MessageBox.Show("Bitte geben Sie eine gültige Zahl größer als 1 für die Stegbreite / Profildicke an.");
                alleBedingungenErfuellt = false; // Eine Bedingung wurde nicht erfüllt
            }
            if (string.IsNullOrEmpty(Bauteilhöhe4.Text))
            {
                MessageBox.Show("Bitte geben Sie die Flansch-/Profildicke ein.");
                alleBedingungenErfuellt = false; // Eine Bedingung wurde nicht erfüllt
            }
            else if (double.TryParse(Bauteilhöhe4.Text, out double bauteilhöhe) && bauteilhöhe <= 1)
            {
                MessageBox.Show("Bitte geben Sie eine gültige Zahl größer als 1 für die Flansch-/Profildicke an.");
                alleBedingungenErfuellt = false; // Eine Bedingung wurde nicht erfüllt
            }
            if (string.IsNullOrEmpty(Stahlpreis.Text))
            {
                MessageBox.Show("Bitte geben Sie den aktuellen Stahlpreis ein.");
                alleBedingungenErfuellt = false; // Eine Bedingung wurde nicht erfüllt
            }
            if (string.IsNullOrEmpty(LängeBauteil.Text))
            {
                MessageBox.Show("Bitte geben Sie eine gültige Bauteillänge ein.");
                alleBedingungenErfuellt = false; // Eine Bedingung wurde nicht erfüllt
            }
            if (string.IsNullOrEmpty(Stückzahl.Text))
            {
                MessageBox.Show("Bitte geben Sie eine gültige Stückzahl ein.");
                alleBedingungenErfuellt = false; // Eine Bedingung wurde nicht erfüllt
            }

            if (alleBedingungenErfuellt)
            {
                System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB0C4DE");

                System.Windows.Media.SolidColorBrush brush = new System.Windows.Media.SolidColorBrush(color);

                Berechnen.Background = brush;
                Berechnen.Cursor = Cursors.Hand;

                //navframe
                NavigationService.Navigate(new Uri("/Pages/Page2.xaml", UriKind.Relative));

            }
        }
        #endregion

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            imagePopup.IsOpen = false;

            HohlPopup.IsOpen = false;

            KastenbildPopup.IsOpen = false;

            ZusammengesetztePopup.IsOpen = false;

            WinkelPopup.IsOpen = false;

            KreisPopup.IsOpen = false;
        }

        private void rb_walz_Checked(object sender, RoutedEventArgs e)
        {
            if (rb_walz.IsChecked == true)
            {
                Bauteilart.ItemsSource = bauteileListe;

                XYLänge.Content = "Gesamthöhe h [mm]:";
                XYBreite.Content = "Flanschbreite b [mm]:";
                XYFlanschbreite.Content = "Stegbreite s [mm]:";
                XYHöhe.Content = "Flanschdicke t [mm]:";
            }
            else if (rb_hohl.IsChecked == true)
            {
                Bauteilart.ItemsSource = hohlprofilListe;
            }

            else if (rb_zusammen.IsChecked == true)
            {
                Bauteilart.ItemsSource = zusamengesetzteListe;

                XYLänge.Content = "Gesamthöhe h [mm]:";
                XYBreite.Content = "Flanschbreite b [mm]:";
                XYFlanschbreite.Content = "Stegbreite s [mm]:";
                XYHöhe.Content = "Flanschdicke t [mm]:";
            }

            else if (rb_frei.IsChecked == true)
            {
                XYLänge.Content = "Gesamthöhe h [mm]:";
                XYBreite.Content = "Flanschbreite b [mm]:";
                XYFlanschbreite.Content = "Stegbreite s [mm]:";
                XYHöhe.Content = "Flanschdicke t [mm]:";

                if (decimal.TryParse(Bauteillänge1.Text, out decimal höhenwert) &&
                  decimal.TryParse(Bauteilbreite2.Text, out decimal breitenwert) &&
                  decimal.TryParse(Flanschbreite3.Text, out decimal stegbreitewert) &&
                  decimal.TryParse(Bauteilhöhe4.Text, out decimal flanschdickewert))
                  
                {

                    decimal fläche = (((flanschdickewert * breitenwert) * 2) + (stegbreitewert * (höhenwert - 2 * flanschdickewert)));              
                    decimal gewicht = fläche / 1000000 * 7850; //mal Dichte 
                    string gewichtText = gewicht.ToString("N2");
                    Gewicht.Text = gewichtText.ToString();

                    decimal querschnitt = (höhenwert - 2 * flanschdickewert) * 2 + 2 * breitenwert + 2 * flanschdickewert + 2 * (breitenwert - stegbreitewert);
                    decimal querschnittm2 = querschnitt / 1000000 * 1000; //mm2 in m2 * laufenden Meter (1000mm)
                    string querschnittText = querschnittm2.ToString("N2");
                    UmfangBauteil.Text = querschnittText.ToString();
                }
                else
                {
                    Gewicht.Text = "Zum Aktualisieren erneut den Button 'Freie Form' wählen";
                }
            }


        }
        private string getProfilartGenauText()
        {
            string ausgewähltesProfil = ProfilartGenau.Text;

            if (ProfilartGenau.SelectedItem != null)
            {
                //Inhalte aus ComboBox Profilartgenau umwandeln, sodass auf den Inhalt zugegriffen werden kann
                #region
                if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.IPEProfil)))
                {
                    IPEProfil ProfilartGenauSelectedItem = (IPEProfil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.IPEaProfil)))
                {
                    IPEaProfil ProfilartGenauSelectedItem = (IPEaProfil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.IPEoProfil)))
                {
                    IPEoProfil ProfilartGenauSelectedItem = (IPEoProfil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.IPEvProfil)))
                {
                    IPEvProfil ProfilartGenauSelectedItem = (IPEvProfil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.IPE750Profil)))
                {
                    IPE750Profil ProfilartGenauSelectedItem = (IPE750Profil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.HEAAprofil)))
                {
                    HEAAprofil ProfilartGenauSelectedItem = (HEAAprofil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.HEAprofil)))
                {
                    HEAprofil ProfilartGenauSelectedItem = (HEAprofil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.HEBprofil)))
                {
                    HEBprofil ProfilartGenauSelectedItem = (HEBprofil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.HEMprofil)))
                {
                    HEMprofil ProfilartGenauSelectedItem = (HEMprofil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Iprofil)))
                {
                    Iprofil ProfilartGenauSelectedItem = (Iprofil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Uprofil)))
                {
                    Uprofil ProfilartGenauSelectedItem = (Uprofil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.UPEprofil)))
                {
                    UPEprofil ProfilartGenauSelectedItem = (UPEprofil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.UAPprofil)))
                {
                    UAPprofil ProfilartGenauSelectedItem = (UAPprofil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Winkelgleichprofil)))
                {
                    Winkelgleichprofil ProfilartGenauSelectedItem = (Winkelgleichprofil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Winkelungleichprofil)))
                {
                    Winkelungleichprofil ProfilartGenauSelectedItem = (Winkelungleichprofil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.HEprofil)))
                {
                    HEprofil ProfilartGenauSelectedItem = (HEprofil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.HLprofil)))
                {
                    HLprofil ProfilartGenauSelectedItem = (HLprofil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.HDprofil)))
                {
                    HDprofil ProfilartGenauSelectedItem = (HDprofil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.HPprofil)))
                {
                    HPprofil ProfilartGenauSelectedItem = (HPprofil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Kreis)))
                {
                    Kreis ProfilartGenauSelectedItem = (Kreis)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Quadrat)))
                {
                    Quadrat ProfilartGenauSelectedItem = (Quadrat)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Rechteck)))
                {
                    Rechteck ProfilartGenauSelectedItem = (Rechteck)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Iprofil)))
                {
                    Iprofil ProfilartGenauSelectedItem = (Iprofil)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
                else if (ProfilartGenau.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Kasten)))
                {
                    Kasten ProfilartGenauSelectedItem = (Kasten)ProfilartGenau.SelectedItem;
                    ausgewähltesProfil = ProfilartGenauSelectedItem.Genau.ToString();
                }
            }
            return ausgewähltesProfil;
            #endregion

        }
        private string getBauteilartText()
        {
            string ausgewähltesBauteil = Bauteilart.Text;

            if (Bauteilart.SelectedItem != null)
            {
                //Inhalte aus ComboBox Bauteilart umwandeln, sodass auf den Inhalt zugegriffen werden kann
                #region
                if (Bauteilart.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Bauteil)))
                {
                    Bauteil BauteilartSelectedItem = (Bauteil)Bauteilart.SelectedItem;
                    ausgewähltesBauteil = BauteilartSelectedItem.Name.ToString();
                }
                else if (Bauteilart.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Hohlprofil)))
                {
                    Hohlprofil BauteilartSelectedItem = (Hohlprofil)Bauteilart.SelectedItem;
                    ausgewähltesBauteil = BauteilartSelectedItem.Name.ToString();
                }
                else if (Bauteilart.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Zusammengesetzte)))
                {
                    Zusammengesetzte BauteilartSelectedItem = (Zusammengesetzte)Bauteilart.SelectedItem;
                    ausgewähltesBauteil = BauteilartSelectedItem.Name.ToString();
                }
            }

            return ausgewähltesBauteil;
            #endregion
        }

        private string getStahlgüteText()
        {
            string ausgewählteStahlgüte = Stahlgüte.Text;

            if(Stahlgüte.SelectedItem != null)
            {
                //Inhalte aus ComboBox Bauteilart umwandeln, sodass auf den Inhalt zugegriffen werden kann
                #region
                if (Stahlgüte.SelectedItem.GetType().Equals(typeof(WpfAppToolBar.Pages.Stahlgüteclass)))
                {
                    Stahlgüteclass StahlgüteSelectedItem = (Stahlgüteclass)Stahlgüte.SelectedItem;
                    ausgewählteStahlgüte = StahlgüteSelectedItem.Stahl.ToString();
                }
            }
            return ausgewählteStahlgüte;
            #endregion
        }

        private void ProfilartGenau_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProfilartGenau.SelectedItem != null)
            {

                //Inhalte aus ComboBox Profilartgenau umwandeln, sodass auf den Inhalt zugegriffen werden kann
                string ausgewähltesProfil = getProfilartGenauText();

                // Hier kannst du den ausgewählten Profilnamen verwenden, um die passenden Werte aus der Excel-Tabelle zu suchen und anzuzeigen
                string höhe = SucheHöheInExcel(ausgewähltesProfil);
                string breite = SucheBreiteInExcel(ausgewähltesProfil);
                string stegbreite = SucheStegbreiteInExcel(ausgewähltesProfil);
                string bauteil4 = SucheBauteilhöhe4InExcel(ausgewähltesProfil);
                string gewicht = SucheGewichtInExcel(ausgewähltesProfil);
                string umfang = SucheUmfangInExcel(ausgewähltesProfil);

                // Die gefundenen Werte in den Textboxen anzeigen
                Bauteillänge1.Text = höhe;
                Bauteilbreite2.Text = breite;
                Flanschbreite3.Text = stegbreite;
                Bauteilhöhe4.Text = bauteil4;
                Gewicht.Text = gewicht;
                UmfangBauteil.Text = umfang;




                string ausgewähltesBauteil = getBauteilartText();


                if (rb_walz.IsChecked == true && (ausgewähltesBauteil == "Winkel (gleichschenklig)" || ausgewähltesBauteil == "Winkel (ungleichschenklig)"))
                {
                    WinkelPopup.IsOpen = true;
                }

                else if (rb_walz.IsChecked == true)
                {
                    imagePopup.IsOpen = true;
                }

                else if (rb_hohl.IsChecked == true && (ausgewähltesBauteil == "Kreisförmige Hohlprofile"))
                {
                    KreisPopup.IsOpen = true;
                }

                else if (rb_hohl.IsChecked == true && (ausgewähltesBauteil == "Quadratische Hohlprofile" || ausgewähltesBauteil == "Rechteckige Hohlprofile"))
                {
                    HohlPopup.IsOpen = true;
                }

                else if (rb_zusammen.IsChecked == true && ausgewähltesBauteil == "Kastenträger")
                {
                    KastenbildPopup.IsOpen = true;
                }

                else if (rb_zusammen.IsChecked == true && ausgewähltesBauteil == "I-Träger")
                {
                    ZusammengesetztePopup.IsOpen = true;
                }
            }
        }

        private void Bauteilart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Bauteilart.SelectedItem != null)
            {
                #region

                if (Bauteilart.SelectedItem is Bauteil selectedBauteil)
                {
                    string selectedItemName = selectedBauteil.Name;


                    if (selectedItemName == "IPE-Profil")
                    {
                        ProfilartGenau.ItemsSource = ipeListe;
                    }

                    else if (selectedItemName == "IPEo-Profil")
                    {
                        ProfilartGenau.ItemsSource = ipeoListe;
                    }

                    else if (selectedItemName == "IPEa-Profil")
                    {
                        ProfilartGenau.ItemsSource = ipeaListe;
                    }

                    else if (selectedItemName == "IPEv-Profil")
                    {
                        ProfilartGenau.ItemsSource = ipevListe;
                    }

                    else if (selectedItemName == "IPE 750-Profil")
                    {
                        ProfilartGenau.ItemsSource = ipe750Liste;
                    }

                    else if (selectedItemName == "I-Profil")
                    {
                        ProfilartGenau.ItemsSource = iListe;
                    }

                    else if (selectedItemName == "HEA-Profil")
                    {
                        ProfilartGenau.ItemsSource = heaListe;
                    }

                    else if (selectedItemName == "HEAA-Profil")
                    {
                        ProfilartGenau.ItemsSource = heaaliste;
                    }

                    else if ((selectedItemName == "HEB-Profil"))
                    {
                        ProfilartGenau.ItemsSource = hebListe;
                    }

                    else if (selectedItemName == "HEM-Profil")
                    {
                        ProfilartGenau.ItemsSource = hemListe;

                    }

                    else if (selectedItemName == "HE-Profil")
                    {
                        ProfilartGenau.ItemsSource = heliste;
                    }

                    else if (selectedItemName == "HL-Profil")
                    {
                        ProfilartGenau.ItemsSource = hlListe;
                    }

                    else if (selectedItemName == "UPE-Profil")
                    {
                        ProfilartGenau.ItemsSource = upeListe;
                    }

                    else if (selectedItemName == "U-Profil")
                    {
                        ProfilartGenau.ItemsSource = uListe;
                    }

                    else if (selectedItemName == "UAP-Profil")
                    {
                        ProfilartGenau.ItemsSource = uapListe;
                    }
                    else if (selectedItemName == "HD-Profil")
                    {
                        ProfilartGenau.ItemsSource = hdListe;
                    }

                    else if (selectedItemName == "HP-Profil")
                    {
                        ProfilartGenau.ItemsSource = hpListe;
                    }

                    else if (selectedItemName == "Winkel (gleichschenklig)")
                    {
                        ProfilartGenau.ItemsSource = gleicheListe;

                        XYLänge.Content = "Höhe a [mm]:";
                        XYBreite.Content = "Breite a [mm]";
                        XYFlanschbreite.Content = "Profildicke t [mm]:";
                        XYHöhe.Content = "Innenradius r1:";
                    }

                    else if (selectedItemName == "Winkel (ungleichschenklig)")
                    {
                        ProfilartGenau.ItemsSource = ungleichListe;

                        XYLänge.Content = "Höhe a [mm]:";
                        XYBreite.Content = "Breite b [mm]";
                        XYFlanschbreite.Content = "Profildicke t [mm]:";
                        XYHöhe.Content = "Innenradius r1:";
                    }
                }

                else if (Bauteilart.SelectedItem is Hohlprofil selectedHohlprofil)
                {
                    string selectedItemName = selectedHohlprofil.Name;

                    if (selectedItemName == "Kreisförmige Hohlprofile")
                    {
                        ProfilartGenau.ItemsSource = kreisListe;

                        XYLänge.Content = "Durchmesser D [mm]:";
                        XYBreite.Content = "Durchmesser D [mm]";
                        XYFlanschbreite.Content = "Profildicke t [mm]:";
                        XYHöhe.Content = "Profildicke t [mm]:";
                    }

                    else if (selectedItemName == "Quadratische Hohlprofile")
                    {
                        ProfilartGenau.ItemsSource = quadratListe;

                        XYLänge.Content = "Gesamthöhe h [mm]:";
                        XYBreite.Content = "Gesamtbreite b [mm]:";
                        XYFlanschbreite.Content = "Profildicke t links [mm]:";
                        XYHöhe.Content = "Profildicke t rechts [mm]:";
                    }

                    else if (selectedItemName == "Rechteckige Hohlprofile")
                    {
                        ProfilartGenau.ItemsSource = rechteckListe;

                        XYLänge.Content = "Gesamthöhe h [mm]:";
                        XYBreite.Content = "Gesamtbreite b [mm]:";
                        XYFlanschbreite.Content = "Profildicke t links [mm]:";
                        XYHöhe.Content = "Profildicke t rechts [mm]:";
                    }

                }

                else if (Bauteilart.SelectedItem is Zusammengesetzte selectedZusammen)
                {
                    string selectedItemName = selectedZusammen.Name;

                    if (selectedItemName == "Kastenträger")
                    {
                        ProfilartGenau.ItemsSource = kastenListe;

                        XYLänge.Content = "Gesamthöhe h [mm]:";
                        XYBreite.Content = "Breite unten bu [mm]:";
                        XYFlanschbreite.Content = "Stegbreite tw [mm]:";
                        XYHöhe.Content = "Flanschdicke unten tu [mm]:";

                    }

                    else if (selectedItemName == "I-Träger")
                    {
                        ProfilartGenau.ItemsSource = itragerListe;

                        XYLänge.Content = "Gesamthöhe h [mm]:";
                        XYBreite.Content = "Breite unten bu [mm]:";
                        XYFlanschbreite.Content = "Stegbreite tw [mm]:";
                        XYHöhe.Content = "Flanschdicke unten tu [mm]:";
                    }

                }

            }
        }

        #endregion

        private void Zurücksetzen_Click(object sender, RoutedEventArgs e)
        {
            //Inhalte des Datenspeichers leeren über Zurücksetzten Button
            #region
            if (Datenspeicher.LängeBauteilWert != null)
            {
                Datenspeicher.LängeBauteilWert = null;
                LängeBauteil.Text = string.Empty;
            }
            if (Datenspeicher.Stahlpreis != null)
            {
                Datenspeicher.Stahlpreis = null;
                Stahlpreis.SelectedText = string.Empty;
            }
            if (Datenspeicher.Bauteillänge1 != null)
            {
                Datenspeicher.Bauteillänge1 = null;
                Bauteillänge1.Text = string.Empty;
            }
            if (Datenspeicher.Bauteilbreite2 != null)
            {
                Datenspeicher.Bauteilbreite2 = null;
                Bauteilbreite2.Text = string.Empty;
            }
            if (Datenspeicher.Flanschbreite3 != null)
            {
                Datenspeicher.Flanschbreite3 = null;
                Flanschbreite3.Text = string.Empty;
            }
            if (Datenspeicher.Bauteilhöhe4 != null)
            {
                Datenspeicher.Bauteilhöhe4 = null;
                Bauteilhöhe4.Text = string.Empty;
            }
            if (Datenspeicher.Stahlpreis != null)
            {
                Datenspeicher.Stahlpreis = null;
                Stahlpreis.Text = string.Empty;
            }
            if (Datenspeicher.Gewicht != null)
            {
                Datenspeicher.Gewicht = null;
                Gewicht.Text = string.Empty;
            }
            if (Datenspeicher.UmfangBauteil != null)
            {
                Datenspeicher.UmfangBauteil = null;
                UmfangBauteil.Text = string.Empty;
            }
            if (Datenspeicher.Stückzahl != null)
            {
                Datenspeicher.Stückzahl = null;
                Stückzahl.Text = string.Empty;
            }
            #endregion
        }

        private void Stahlgüte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Stahlpreis ändert sich bei Änderung der Stahlgüte
            #region

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Pfad zur Excel-Datei
            string projectDirectory = Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName).Parent.FullName;
            string pathToExcel = System.IO.Path.Combine(projectDirectory, "AllgWerte.xlsx");

            string erhöhung15 = "1.15"; // Standardwert, falls die Zelle in der Excel-Tabelle leer ist oder einen Fehler enthält.
            string erhöhung30 = "1.30"; // Standardwert, falls die Zelle in der Excel-Tabelle leer ist oder einen Fehler enthält.
            string grundpreiss235 = Stahlpreis.Text;

            if (File.Exists(pathToExcel))
            {
                using (var package = new ExcelPackage(new FileInfo(pathToExcel)))
                {
                    var worksheet = package.Workbook.Worksheets["Tabelle1"];

                    if (worksheet != null)
                    {
                        var cellB8 = worksheet.Cells["B8"].Value;
                        if (cellB8 != null)
                        {
                            erhöhung15 = cellB8.ToString();
                        }

                        var cellB9 = worksheet.Cells["B9"].Value;
                        if (cellB9 != null)
                        {
                            erhöhung30 = cellB9.ToString();
                        }
                        var cellB10 = worksheet.Cells["B10"].Value;
                        if (cellB10 != null)
                        {
                            grundpreiss235 = cellB10.ToString();
                        }
                    }
                }
            }

            if (Stahlgüte.SelectedItem != null)
            {
                if (string.IsNullOrEmpty(Stahlpreis.Text))
                {
                    MessageBox.Show("Bitte geben Sie den aktuellen Stahlpreis ein.");
                }
                else if (Stahlgüte.SelectedItem is Stahlgüteclass selectedBauteilStahl)
                {
                    string selectedItemNameStahl = selectedBauteilStahl.Stahl;
                    decimal stahlpreiswert = Convert.ToDecimal(grundpreiss235);
                    decimal erhöhung15wert = Convert.ToDecimal(erhöhung15);
                    decimal erhöhung30wert = Convert.ToDecimal(erhöhung30);

                    if (selectedItemNameStahl == "S 235")
                    {
                        Stahlpreis.Text = stahlpreiswert.ToString();
                    }
                    else if (selectedItemNameStahl == "S 355")
                    {
                        decimal stahlpreiswerts355 = erhöhung15wert * stahlpreiswert;
                        string stahlpreiswerts355Text = stahlpreiswerts355.ToString("N2"); // Zwei Dezimalstellen.
                        Stahlpreis.Text = stahlpreiswerts355Text;
                    }
                    else if (selectedItemNameStahl == "S 460")
                    {
                        decimal stahlpreiswerts460 = erhöhung30wert * stahlpreiswert;
                        string stahlpreiswerts460Text = stahlpreiswerts460.ToString("N2"); // Zwei Dezimalstellen.
                        Stahlpreis.Text = stahlpreiswerts460Text;
                    }
                }
            }

            #endregion
        }
    }

    //Deklarieren aller notwendiger Klassen
    #region


    //Walzprofile
    public class Bauteil
    {
        public string Name { get; set; }

        public Bauteil(string _name)
        {
            Name = _name;
        }
    }

    //IPE-Profile

    public class IPEProfil
    {
        public string Genau { get; set; }

        public IPEProfil(string _ipe)
        {
            Genau = _ipe;
        }
    }

    //IPEo-Profile

    public class IPEoProfil
    {
        public string Genau { get; set; }

        public IPEoProfil(string _ipeo)
        {
            Genau = _ipeo;
        }
    }

    //IPEv-Profile

    public class IPEvProfil
    {
        public string Genau { get; set; }

        public IPEvProfil(string _ipev)
        {
            Genau = _ipev;
        }
    }

    //IPEa-Profile

    public class IPEaProfil
    {
        public string Genau { get; set; }

        public IPEaProfil(string _ipea)
        {
            Genau = _ipea;
        }
    }

    //IPE750-Profile

    public class IPE750Profil
    {
        public string Genau { get; set; }

        public IPE750Profil(string _ipe750)
        {
            Genau = _ipe750;
        }
    }

    //I-Profile
    public class Iprofil
    {
        public string Genau { get; set; }

        public Iprofil(string _i)
        {
            Genau = _i;
        }
    }

    //U-Profile
    public class Uprofil
    {
        public string Genau { get; set; }

        public Uprofil(string _u)
        {
            Genau = _u;
        }
    }

    //HEB-Profile
    public class HEBprofil
    {
        public string Genau { get; set; }

        public HEBprofil(string _heb)
        {
            Genau = _heb;
        }
    }

    //HEA-Profile
    public class HEAprofil
    {
        public string Genau { get; set; }

        public HEAprofil(string _hea)
        {
            Genau = _hea;
        }
    }

    //HEAA-Profile
    public class HEAAprofil
    {
        public string Genau { get; set; }

        public HEAAprofil(string _heaa)
        {
            Genau = _heaa;
        }
    }

    //HL-Profile
    public class HLprofil
    {
        public string Genau { get; set; }

        public HLprofil(string _hl)
        {
            Genau = _hl;
        }
    }

    //HE-Profile
    public class HEprofil
    {
        public string Genau { get; set; }

        public HEprofil(string _he)
        {
            Genau = _he;
        }
    }

    //HD-Profile
    public class HDprofil
    {
        public string Genau { get; set; }

        public HDprofil(string _hd)
        {
            Genau = _hd;
        }
    }

    //HP-Profile
    public class HPprofil
    {
        public string Genau { get; set; }

        public HPprofil(string _hp)
        {
            Genau = _hp;
        }
    }

    //UPE-Profile
    public class UPEprofil
    {
        public string Genau { get; set; }

        public UPEprofil(string _upe)
        {
            Genau = _upe;
        }
    }

    //UAP-Profile
    public class UAPprofil
    {
        public string Genau { get; set; }

        public UAPprofil(string _uap)
        {
            Genau = _uap;
        }
    }

    //HEM-Profile
    public class HEMprofil
    {
        public string Genau { get; set; }

        public HEMprofil(string _hem)
        {
            Genau = _hem;
        }
    }

    //Winkel gleichschenklig-Profile
    public class Winkelgleichprofil
    {
        public string Genau { get; set; }

        public Winkelgleichprofil(string _gleich)
        {
            Genau = _gleich;
        }
    }

    //Winkel ungleichschenklig-Profile
    public class Winkelungleichprofil
    {
        public string Genau { get; set; }

        public Winkelungleichprofil(string _ungleich)
        {
            Genau = _ungleich;
        }
    }


    //Hohlprofile
    public class Hohlprofil
    {
        public string Name { get; set; }

        public Hohlprofil (string _hohl)
        {
            Name = _hohl;
        }
    }

    //Kreisförmige Hohlprofile
    public class Kreis
    {
        public string Genau { get; set; }

        public Kreis (string _kreis)
        {
            Genau = _kreis;
        }
    }

    //Quadratische Hohlprofile
    public class Quadrat
    {
        public string Genau { get; set; }

        public Quadrat(string _quadrat)
        {
            Genau = _quadrat;
        }
    }

    //Rechteckige Hohlprofile
    public class Rechteck
    {
        public string Genau { get; set; }

        public Rechteck(string _rechteck)
        {
            Genau = _rechteck;
        }
    }

    //Zusammengesetzte Querschnitte 
    public class Zusammengesetzte
    {
        public string Name { get; set; }

        public Zusammengesetzte (string _zusammen)
        {
            Name = _zusammen;
        }
    }

    //Kastenträger

    public class Kasten
    {
        public string Genau { get; set; }

        public Kasten(string _kasten)
        {
            Genau = _kasten;
        }
    }

    //I-Träger

    public class Itrager
    {
        public string Genau { get; set; }

        public Itrager(string _itrager)
        {
            Genau = _itrager;
        }
    }

    //Stahlgüte
    public class Stahlgüteclass
    {
        public string Stahl { get; set; }

        public Stahlgüteclass (string _stahl)
        {
            Stahl = _stahl;
        }
    }


    //Verzinken 
    public class JaNein
    {
        public string No { get; set; }

        public JaNein(string _yes)
        {
            No = _yes;
        }
    }

    #endregion

}
