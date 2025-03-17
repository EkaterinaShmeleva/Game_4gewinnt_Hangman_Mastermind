using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Spiele_Hangman_VierGewinnt
{
    class Program
    {
        static void Main(string[] args)
        {
            Hauptmenue();
        }

        static void Hauptmenue()
        {
            int punkte = 0; // Punktestand initialisieren

            while (true)
            {
                Console.WriteLine("=======Hauptmenü=======");
                Console.WriteLine("1. Hangman");
                Console.WriteLine("2. Vier Gewinnt");
                Console.WriteLine("3. Mastermind");
                Console.WriteLine("4. Exit");

                int auswahl = AuswahlEinlesen();
                Console.Clear();
                if (auswahl == 1) //Spiel 1
                {
                    punkte = SpielmenueHangman(punkte);
                }
                else if (auswahl == 2)// Spiel 2
                {
                    punkte = SpielmenueVierGewinnt(punkte);
                }
                else if (auswahl == 3) //Spiel 3
                {
                    punkte = SpielmenueMastermind(punkte);
                }
                else if (auswahl == 4) //Exit
                {
                    Console.WriteLine("Programm wird beendet...");
                    break;
                }
                else
                {
                    Console.WriteLine("Falsche Eingabe. Versuch nochmal!");
                }
            }
        }

        static int SpielmenueHangman(int punkte)
        {
            while (true)
            {
                Console.WriteLine("=======Spielemenü=======");
                Console.WriteLine("1. Neues Spiel starten");
                Console.WriteLine("2. Punktestand anzeigen");
                Console.WriteLine("3. Zurück zum Hauptmenü");

                int auswahl = AuswahlEinlesen();
                Console.Clear();
                if (auswahl == 1)
                {
                    //Console.WriteLine("Neues Spiel wird gestartet");
                    punkte = StarteHangman(punkte);
                }
                else if (auswahl == 2)
                {
                    Console.WriteLine("Punktestand ist " + punkte);

                }
                else if (auswahl == 3)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Falsche Eingabe");
                }
            }
            return punkte;
        }

        static int AuswahlEinlesen()
        {
            Console.WriteLine("Ihre Auswahl: ");
            int auswahl;
            while (!int.TryParse(Console.ReadLine(), out auswahl))
            {
                Console.WriteLine("Ungültige Eingabe. Bitte geben Sie eine Zahl ein.");
            }
            return auswahl;
        }

        static int StarteHangman(int punkte)
        {
            bool nochmalSpielen = true;

            while (nochmalSpielen)
            {
                //Variablen deklarieren
                string wort = "Hangman";
                int versuche = 5;
                char[] wortVerdeckt = new string('_', wort.Length).ToCharArray(); //hier wird das Wort in Unterstrichen dargestellt

                //Darstellung des Spiels
                SpielStart(ref wort, ref wortVerdeckt, ref versuche);

                while (versuche > 0 && new string(wortVerdeckt) != wort) //solange versuche vorhanden und das wort nicht erraten ist
                {
                    //Benutzereingabe
                    string eingabe = Benutzereingabe();

                    #region//prüfen ob Eingabe gültig ist, Fehlermeldung ausgeben
                    if (eingabe.Length != 1) //es wird nur anhand der Anzahl der eingegebenen Zeichen geprüft. Falls eine Ziffer oder ein Sonderzeichen eingegeben wird, wird es nicht erkannt.
                    {
                        Console.WriteLine("Bitte nur einen Buchstaben eingeben!");
                        continue;
                    }
                    #endregion

                    char buchstabe = eingabe[0]; //string 'eingabe' speichern wir als char 'buchstabe'

                    #region //prüfen, ob Buchstabe im Wort enthalten ist
                    if (wort.ToLower().Contains(buchstabe)) //wenn wort enthaltet diese char
                    {
                        Console.WriteLine("Der Buchstabe ist im Wort enthalten.");
                        punkte += 10;// 10 Punkte für erratenen Buchstaben
                        //Aktualisierung des Wortes anzeigen
                        for (int i = 0; i < wort.Length; i++)
                        {
                            if (char.ToLower(wort[i]) == buchstabe) //hier vergleichen wir deshalb ==
                            {
                                wortVerdeckt[i] = wort[i]; //hier zuweisen wir deshalb =
                            }
                        }
                        Console.WriteLine("Hier ist aktualisiertes Wort:" + new string(wortVerdeckt));
                    }
                    else
                    {
                        Console.WriteLine("Der Buchstabe ist nicht im Wort enthalten.");
                        //Buchstabe nicht drin -> versuche reduzieren 
                        versuche--;
                        Console.WriteLine("Du hast noch {0} Versuche.", versuche); // hier geht es nochmal zu Benutzereingabe
                    }
                    #endregion

                    //Gewonnen/Verloren anzeigen
                    SpielGewonnenVerloren(ref wortVerdeckt, ref wort, ref versuche);
                }

                //Spiel nochmal oder zurück zum Spiele-Menü
                SpielNochmal(ref nochmalSpielen);
            }
            return punkte;
        }

        static string Benutzereingabe()
        {
            Console.WriteLine("Gib einen Buchstaben ein: ");
            string eingabe = Console.ReadLine();
            return eingabe.ToLower();
        }

        static void SpielStart(ref string wort, ref char[] wortVerdeckt, ref int versuche)
        {
            Console.WriteLine("=======Willkommen bei Hangman!=======");
            Console.WriteLine("Das Wort hat {0} Buchstaben.", wort.Length);
            Console.WriteLine("Hier das Wort zum Raten:" + new string(wortVerdeckt)); //wortVerdeckt ist ein array von chars und hier machen wir ihn zum string
            Console.WriteLine("Du hast {0} Versuche.", versuche);
        }

        static void SpielNochmal(ref bool nochmalSpielen)
        {
            Console.WriteLine("Nochmal spielen? j/n");
            string nochmal = Console.ReadLine().ToLower();
            Console.Clear();
            if (nochmal != "j")
            {
                nochmalSpielen = false;
            }
        }

        static void SpielGewonnenVerloren(ref char[] wortVerdeckt, ref string wort, ref int versuche)
        {
            if (new string(wortVerdeckt) == wort)
            {
                Console.WriteLine("Herzlichen Glückwunsch! Du hast das Wort erraten.");
            }
            else if (versuche == 0)
            {
                Console.WriteLine("Du hast keine Versuche mehr. Das Spiel ist verloren.");
                Console.WriteLine("Das Wort war: " + wort);
            }
        }

        static int StarteVierGewinnt(int punkte)
        {
            Console.WriteLine("==Willkommen im Spiel==");
            Console.WriteLine("  ==Vier gewinnt==");
            string[,] feld = FeldErstellen();
            FeldDarstellen(feld);

            bool spieler1 = true; //Spieler1 zuerst, dann abwechselnd
            bool gewonnen = false;

            bool nochmalSpielen = true;

            while (nochmalSpielen)
            {
                while (!gewonnen)
                {
                    if (spieler1)
                    {
                        SpielerZug(feld);
                    }
                    else
                    {
                        ComputerZug(feld);
                    }

                    FeldDarstellen(feld); //feld mit dem neuen Stein anzeigen

                    gewonnen = PrüfeDiagonal(feld) || PrüfeDiagonal2(feld) || PrüfeHorizontal(feld) || PrüfeVertikal(feld);

                    if (gewonnen)
                    {
                        Console.WriteLine("Gewonnen!");
                        punkte += 50;
                    }
                }
                SpielNochmal(ref nochmalSpielen);
                spieler1 = !spieler1;
            }
            return punkte;
                
        }
            
        static string[,] FeldErstellen()
        {
            string[,] feld = new string[6, 8];
            for (int zeile = 0; zeile < feld.GetLength(0); zeile++)
            {
                for (int spalte = 0; spalte < feld.GetLength(1); spalte++)
                {
                    feld[zeile, spalte] = " . ";
                }
            }
            return feld;
        }

        static void FeldDarstellen(string[,] feld)
        {
            Console.Clear(); //hier soll den Bildschirm gereinigt werden, damit neuer Spielefeld zu sehen ist
            Console.WriteLine(" 1  2  3  4  5  6  7  8");
            for (int zeile = 0; zeile < feld.GetLength(0); zeile++)
            {
                for (int spalte = 0; spalte < feld.GetLength(1); spalte++)
                {
                    Console.Write(feld[zeile, spalte]);
                }
                Console.WriteLine();
            }
        }

        static int BenutzerEingabe()
        {
            Console.WriteLine("Gib eine Spalte ein, wo der Stein gesetzt wird:");
            int eingabe = int.Parse(Console.ReadLine());
            return eingabe;
        }

        static bool EingabeGültig(int eingabe)
        {
            return eingabe > 0 && eingabe < 9; // eingabe von 1 bis 8 gültig
        }

        static bool PasstRein(string[,] feld, int eingabe)//if Spalte voll?
        {
            return feld[0, eingabe - 1] == " . "; // Zeile 0 ist nicht Punkt
        }
        /// <summary>
        /// Die Methode setzt ein Steil vom Spieler-Eingabe.
        /// </summary>
        /// <param name="feld">Rückgabe Spielefeld</param>
        static void SpielerZug(string[,] feld)
        {
            //spalte holen (eingabeBenutzer/Random)
            //EingabePrüfen (Benutzer: EingabeGültig PasstRein, nicht gültig - neue Eingabe. Computer - PasstRein, nicht gültig - neue Eingabe.
            //stein setzen
            int eingabe = BenutzerEingabe();
            int zeile = 0;
            do
            {
                if (EingabeGültig(eingabe) && PasstRein(feld, eingabe))
                {
                    for (int i = feld.GetLength(0) - 1; i >= 0; i--)
                    {
                        if (feld[i, eingabe - 1] == " . ")
                        {
                            feld[i, eingabe - 1] = " 1 ";
                            zeile = i; //damit ich Zeilennummer für Prüfung habe
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Eingabe ungültig.");
                    eingabe = BenutzerEingabe();
                }
            } while (!EingabeGültig(eingabe) && !PasstRein(feld, eingabe));

        }

        static void ComputerZug(string[,] feld)
        {
            Random rnd = new Random();
            int eingabe = rnd.Next(1, 9);
            int zeile = 0;
            for (int i = feld.GetLength(0) - 1; i >= 0; i--)
            {
                if (feld[i, eingabe - 1] == " . ")
                {
                    feld[i, eingabe - 1] = " 2 ";
                    zeile = i;
                    break;
                }
            }
        }

        static bool PrüfeHorizontal(string[,] feld)
        {
            bool gewonnen = false;

            int treffer1 = 0;
            int treffer2 = 0;

            //prüfen die Zeile wo letzter Zug war - brauchen Zeile - die Variable ist in 
            for (int i = 0; i < feld.GetLength(0); i++)
            {
                for (int j = 0; j < feld.GetLength(1); j++)
                {
                    if (feld[i, j] == " 1 ")
                    {
                        treffer1++;
                        treffer2 = 0;
                    }
                    else if (feld[i, j] == " 2 ")
                    {
                        treffer2++;
                        treffer1 = 0;
                    }
                    else
                    {
                        treffer1 = 0;
                        treffer2 = 0;
                    }

                    if (treffer1 == 4 || treffer2 == 4)
                    {
                        gewonnen = true;
                    }
                }
            }
            return gewonnen;
        }

        static bool PrüfeVertikal(string[,] feld)
        {
            bool gewonnen = false;

            int treffer1 = 0;
            int treffer2 = 0;

            //wir laufen array erst spalte, dann zeile durch
            for (int j = 0; j < feld.GetLength(1); j++)
            {
                for (int i = 0; i < feld.GetLength(0); i++)
                {
                    if (feld[i, j] == " 1 ")
                    {
                        treffer1++;
                        treffer2 = 0;
                    }
                    else if (feld[i, j] == " 2 ")
                    {
                        treffer2++;
                        treffer1 = 0;
                    }
                    else
                    {
                        treffer1 = 0;
                        treffer2 = 0;
                    }
                    if (treffer1 == 4 || treffer2 == 4)
                    {
                        gewonnen = true;
                    }
                }
            }
            return gewonnen;
        }

        static bool PrüfeDiagonal(string[,] feld) //prüft Diagonale von oben links nach unten rechts
        {
            //Kopie vom Spielfeld, ausfüllen mit Diagonal
            //horiz und diagonal abschalten
            //feld = FeldErstellen();
            //feld[0, 4] = " 1 ";
            //feld[1, 5] = " 1 ";
            //feld[2, 6] = " 1 ";
            //feld[3, 7] = " 1 ";

            /*
            0,3  1,2  2,1  3,0
            0,4  1,3  2,2  3,1  4,0
            0,5  1,4  2,3  3,2  4,1  5,0
            0,6  1,5  2,4  3,3  4,2  5,1
            0,7  1,6  2,5  3,4  4,3  5,2
            1,7  2,6  3,5  4,4  5,3
            2,7  3,6  4,5  5,4
            */

            //2 diagonale zum, Testen

            //feld_test[1, 3] = " 1 ";
            //feld_test[2, 2] = " 1 ";
            //feld_test[3, 1] = " 1 ";
            //feld_test[4, 0] = " 1 ";

            //feld[1, 6] = " 1 ";
            //feld[2, 5] = " 1 ";
            //feld[3, 4] = " 1 ";
            //feld[4, 3] = " 1 ";

            //feld_test[0, 6] = " 1 "; //diese Diagonal hat 1 Leerpunkt
            //feld_test[1, 5] = " 1 ";
            //feld_test[3, 3] = " 1 ";
            //feld_test[4, 2] = " 1 ";


            int start_i = 0;            // Mein Startwert für i, der sich ja ändert
            int ende_i = 3;             // Mein End-Wert für i, der sich auch ändert
            int i = start_i;            // Dann kann ich den Startwert auch gleich in i reinwerfen

            int start_j = 3;            // j ändert sich auch
            int j = start_j;            // Wie bei i, ich kann den Wert gleich reinwerfen

            int treffer1 = 0;
            int treffer2 = 0;

            bool gewonnen = false;

            for (int k = 0; k < 7; k++) // 7 Läufe
            {

                for (; i < ende_i; i++)
                {
                    //Console.Write($"{i},{j}  ");

                    if (feld[i, j] == " 1 ")
                    {
                        //Console.WriteLine($"treffer in {i},{j}"); //erkennt wo diagonal ist
                        treffer1++;
                        treffer2 = 0;
                        if (treffer1 == 4)
                        {

                            gewonnen = true;
                        }
                    }
                    if (feld[i, j] == " 2 ")
                    {
                        treffer2++;
                        treffer1 = 0;
                        if (treffer2 == 4)
                        {

                            gewonnen = true;
                        }
                    }
                    else
                    {
                        treffer1 = 0;
                        treffer2 = 0;
                    }
                    j--;
                }


                // Und hier die "Regeln"...

                if (k >= 4) // Also die 5te Runde ... erst ab da wird der Startwert von i größer
                {
                    start_i++;
                }
                if (ende_i < 5) // Der Endwert von i wächst nicht mehr, wenn er 5 erreicht hat
                {
                    ende_i++;
                }
                if (start_j < 7) // Der Startwert von j ändert sich solange, wie j kleiner 7 ist
                {
                    start_j++;
                }
                if (k > 5)      // ab der Runde 6 wird j kleiner 
                {
                    start_j--;
                }
                // Werte anpassen
                i = start_i;
                j = start_j;
            }
            return gewonnen;
        }

        static bool PrüfeDiagonal2(string[,] feld) //von rechts nach links
        {

            int start_i = 0;            // Mein Startwert für i, der sich ja ändert
            int ende_i = 4;             // Mein End-Wert für i, der sich auch ändert

            int i = start_i;            // Dann kann ich den Startwert auch gleich in i reinwerfen

            int start_j = 4;            // j ändert sich auch, aber nur in eine richtung
            int j = start_j;            // Wie bei i, ich kann den Wert gleich reinwerfen

            int treffer1 = 0;
            int treffer2 = 0;

            for (int k = 0; k < 7; k++) // 7 Läufe
            {
                for (; i < ende_i; i++)
                {
                    //Console.Write($"{i},{j}  ");

                    if (feld[i, j] == " 1 ")
                    {
                        treffer1++;
                        treffer2 = 0;
                        if (treffer1 == 4)
                        {
                            Console.WriteLine("Herzlichen Glückwunsch Spieler");
                            return true;
                        }
                    }
                    if (feld[i, j] == " 2 ")
                    {
                        treffer2++;
                        treffer1 = 0;
                        if (treffer2 == 4)
                        {
                            Console.WriteLine("Herzlichen Glückwunsch Computer");
                            return true;
                        }
                    }
                    else
                    {
                        treffer1 = 0;
                        treffer2 = 0;
                    }
                    j++;
                }

                // Und hier die "Regeln" welche vorher erkannt wurden

                if (k >= 4) // Also die 5te Runde ... erst ab da wird der Startwert von i größer
                {
                    start_i++;
                }

                if (ende_i < 6) // Der Endwert von i wächst nicht mehr, wenn er 6 erreicht hat
                {
                    ende_i++;
                }

                if (start_j > 0) // Der Startwert von j ändert sich solange, wie j größer 0 ist
                {
                    start_j--;
                }

                // Da nun meine werte angepasst wurden, kann ich diese für die nächste Runde zuweisen
                i = start_i;
                j = start_j;
            }

            return false;
        }

        static int SpielmenueVierGewinnt(int punkte)
        {
            while (true)
            {
                Console.WriteLine("=======Spielemenü=======");
                Console.WriteLine("1. Neues Spiel starten");
                Console.WriteLine("2. Punktestand anzeigen");
                Console.WriteLine("3. Zurück zum Hauptmenü");

                int auswahl = AuswahlEinlesen();
                Console.Clear();
                if (auswahl == 1)
                {
                    //Console.WriteLine("Neues Spiel wird gestartet");
                    punkte = StarteVierGewinnt(punkte);
                }
                else if (auswahl == 2)
                {
                    Console.WriteLine("Punktestand ist " + punkte);

                }
                else if (auswahl == 3)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Falsche Eingabe");
                }
            }
            return punkte;
        }

        static int StarteMastermind(int punkte)
        {
            bool nochmalSpielen = false;
            do
            {
                Console.WriteLine("====Willkommen im Spiel Mastermind====");
                Console.WriteLine("Zum Raten sind 5 Zahlen: ");
                Console.WriteLine("[_ _ _ _ _]");
                //geheime-Kombi erstellen. Array mit 5 Zahlen von 1 bos 9 ohne doppelten
                int[] geheim = { 1, 3, 5, 7, 9 }; //erstmal fix, danach mit rnd

                //Variablen deklarieren:

                int treffer = 0;
                int vorhanden = 0;
                int runden = 0;
                
                do
                {
                    int[] eingabeArray = BenutzerEingabeMastermind();
                    //int[] eingabeArray = { 1, 4, 6, 8, 3 };
                    runden++;
                    vorhanden = 0;
                    treffer = 0;

                    for (int i = 0; i < eingabeArray.Length; i++)
                    {
                        if (eingabeArray[i] == geheim[i]) // Zahlen auf der richtigen Position = treffer
                        {
                            //Console.WriteLine("Richtige Zahl an der richtigen Position!");
                            treffer++;
                        }
                        else
                        {
                            for (int j = 0; j < geheim.Length; j++)
                            {
                                if (eingabeArray[i] == geheim[j])//Zahlen vorhanden = anzahlVorhanden.
                                {
                                    //Console.WriteLine("Richtig, aber an der falschen Position");
                                    vorhanden++;
                                    break;
                                }
                            }
                        }
                    }

                    Console.WriteLine("Runde {0}: vorhanden = {1}, treffer = {2}", runden, vorhanden, treffer);
                } while (treffer < 5 && runden < 16);

                if (treffer == 5)
                {
                    punkte += 100;
                    Console.WriteLine("Gewonnen!");
                }
                else
                {
                    Console.WriteLine("Game over. Versuch nächstes mal!");
                }

                SpielNochmalMastermind(ref nochmalSpielen);
            } while (nochmalSpielen);
            return punkte;
        }

        static int SpielmenueMastermind(int punkte)
        {
            while (true)
            {
                Console.WriteLine("=======Spielemenü=======");
                Console.WriteLine("1. Neues Spiel starten");
                Console.WriteLine("2. Punktestand anzeigen");
                Console.WriteLine("3. Zurück zum Hauptmenü");

                int auswahl = AuswahlEinlesen();
                Console.Clear();
                if (auswahl == 1)
                {
                    //Console.WriteLine("Neues Spiel wird gestartet");
                    punkte = StarteMastermind(punkte);
                }
                else if (auswahl == 2)
                {
                    Console.WriteLine("Punktestand ist " + punkte);

                }
                else if (auswahl == 3)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Falsche Eingabe");
                }
            }
            return punkte;
        }

        static bool SpielNochmalMastermind(ref bool nochmalSpielen)
        {
            Console.WriteLine("Nochmal spielen? j/n");
            string nochmal = Console.ReadLine().ToLower();
            Console.Clear();
            if (nochmal != "j")
            {
                nochmalSpielen = false;
            }
                return nochmalSpielen;
        }


        static int[] BenutzerEingabeMastermind()
        {
            Console.WriteLine("Gib deine Vermitung ein (die Zahlen von 1 bis 9 mit Leerzeichen getrennt): ");
            string eingabe = Console.ReadLine();
            //string zu einem array umbauen 
            string[] zahlenAsStringArray = eingabe.Split(' ');
            int[] eingabeArray = new int[zahlenAsStringArray.Length];
            for (int i = 0; i < zahlenAsStringArray.Length; i++)
            {
                eingabeArray[i] = int.Parse(zahlenAsStringArray[i]);
            }
            return eingabeArray;
        }


    }
}