# Plan Detaliat de Execuție a Proiectului "Melodii" (Pentru AI)

**Atenție Maximă, Inteligență Artificială!**

Acest document, `plan.md`, este ghidul tău suprem și obligatoriu pentru dezvoltarea proiectului "Melodii". Respectarea cu strictețe a fiecărei secțiuni, reguli și etape este esențială pentru a atinge obiectivul: un proiect de calitate excepțională ("notă de 10"), scalabil și ușor de întreținut.

**Filozofia Acestui Plan:**
1.  **Claritate Absolută:** Instrucțiuni precise pentru fiecare pas.
2.  **Calitate Peste Cantitate:** Maximizează calitatea pentru fiecare solicitare; nu te grăbi.
3.  **Modularitate și Scalabilitate:** Scrie codul gândindu-te la viitor.
4.  **Colaborare AI-Utilizator:** Rolurile sunt clar definite, în special pentru testare și conectarea la BD.



---

## Secțiunea 1: Inițializarea Proiectului și Configurarea Mediului (Solicitarea 0 - Acțiuni Utilizator Ghidat de AI)

*Această fază este realizată de utilizatorul uman, dar tu, AI, trebuie să fii conștientă de ea și să poți oferi ghidaj dacă este necesar.*

1.  **Utilizator:** Crearea unui nou proiect C# Windows Forms (.NET Framework) în Visual Studio. Numele proiectului: `MelodiiApp` (sau similar).
2.  **Utilizator:** Organizarea structurii folderelor în Solution Explorer (sugestii de la AI pot fi binevenite, dar nu obligatorii în această fază incipientă, ex: `DataAccessLayer`, `Models`, `UserInterfaceForms`, `Reports`).
3.  **TU (AI):** Pregătește mental structura generală a aplicației și fii gata să populezi aceste foldere.

---

## Secțiunea 2: Proiectarea și Crearea Inițială a Bazei de Date (SQL Server)

*Această secțiune implică generarea de scripturi SQL de către tine (AI) și ghidarea utilizatorului pentru execuția lor.*

**Solicitarea 1 (AI):**
1.  **Task 1:** Definește și documentează entitățile principale (`Melodii`, `Intervievati`, `Voturi`) și atributele lor, conform detaliilor din `sarcina_specifica_de_executat_practica.md`. Include tipurile de date SQL și constrângerile (PK, FK, UNIQUE, CHECK, NOT NULL).
2.  **Task 2:** Generează scriptul SQL complet pentru `CREATE DATABASE MelodiiConcurs;`.
3.  **Task 3:** Generează scripturile `CREATE TABLE` pentru `Melodii`, `Intervievati`, `Voturi`. Include definirea cheilor primare, a constrângerilor `NOT NULL` și a tipurilor de date adecvate. Momentan nu include cheile externe sau constrângerile `CHECK` complexe; acestea vor fi adăugate iterativ.

**Solicitarea 2 (AI):**
1.  **Task 1:** Generează instrucțiuni **extrem de detaliate și clare (pas cu pas)** pentru utilizatorul uman despre cum să:
    *   Deschidă SQL Server Management Studio (SSMS).
    *   Se conecteze la instanța sa locală de SQL Server.
    *   Deschidă o nouă fereastră de interogare (New Query).
    *   Copieze și execute scriptul `CREATE DATABASE` generat de tine.
    *   Verifice crearea bazei de date.
    *   Selecteze noua bază de date (`USE MelodiiConcurs;`).
    *   Copieze și execute scripturile `CREATE TABLE` generate de tine.
    *   Verifice crearea tabelelor (ex: prin expandarea nodului Tables în Object Explorer).
2.  **Task 2:** Adaugă cheile externe (Foreign Keys) la scripturile `CREATE TABLE` (sau prin `ALTER TABLE`) pentru a lega `Voturi` de `Melodii` și `Intervievati`. Asigură-te că specifici `ON DELETE CASCADE` sau `ON DELETE SET NULL` unde este logic (discută implicațiile dacă e cazul, `CASCADE` poate fi periculos dacă nu e dorit). Pentru acest proiect, `ON DELETE CASCADE` pentru `Voturi` la ștergerea unui `Intervievat` sau a unei `Melodii` ar putea fi acceptabil pentru a menține integritatea, dar specifică acest lucru.
3.  **Task 3:** Adaugă constrângeri `CHECK` simple (ex: `Varsta > 0` pentru `Intervievati`, `PozitieOferita IN (1,2,3)` pentru `Voturi`).

*(Notă: Vom adăuga indecși și viziuni mai târziu, după ce structura de bază și primele funcționalități sunt implementate).*

---

## Secțiunea 3: Dezvoltarea Aplicației - Module de Bază și UI Inițial

*Ne concentrăm pe crearea formularelor și a logicii de bază, respectând cu strictețe regula designului în `Designer.cs`.*

**Solicitarea 3 (AI):**
1.  **Task 1 (UI Design):** Creează fișierul `MainForm.cs`. Adaugă un control `MenuStrip` în `MainForm.Designer.cs`. Definește următoarele elemente de meniu principale:
    *   "Melodii" (cu sub-item: "Adaugă Melodie Nouă", "Vezi Clasament Melodii")
    *   "Intervievați" (cu sub-iteme: "Adaugă Intervievat Nou", "Gestionează Intervievați", "Vezi Clasament Intervievați")
    *   "Votare" (cu sub-item: "Înregistrează Voturi")
    *   "Rapoarte" (cu sub-iteme: "Listă Participanți Concurs", "Export Participanți Sub 18 Ani")
    *   "Ajutor" (cu sub-item: "Despre")
    *   *Stil modern și intuitiv pentru meniu.*
2.  **Task 2 (Clase Model):** Creează fișierele de clasă C# pentru modelele de bază (POCOs):
    *   `Models/Melodie.cs` (proprietăți: `MelodieID`, `Titlu`, `Artist`, `GenMuzical`, `AnLansare`, `PunctajTotal`)
    *   `Models/Intervievat.cs` (proprietăți: `IntervievatID`, `NumeComplet`, `Varsta`, `Localitate`, `ScorTotalConcurs`)
    *   `Models/Vot.cs` (proprietăți: `VotID`, `IntervievatID`, `MelodieID`, `PozitieOferita`)
    *   Adaugă constructori default și comentarii XML sumare pentru fiecare clasă și proprietate.
3.  **Task 3 (Formular Adaugă Melodie - UI):** Creează `UserInterfaceForms/AdaugaMelodieForm.cs`. În `AdaugaMelodieForm.Designer.cs`, adaugă:
    *   `Label` și `TextBox` pentru: Titlu, Artist, Gen Muzical, An Lansare.
    *   Butoane: "Salvează Melodie", "Anulează".
    *   Aranjează controalele estetic și intuitiv. Adaugă nume sugestive controalelor (ex: `txtTitlu`, `btnSalveazaMelodie`).

**Solicitarea 4 (AI):**
1.  **Task 1 (Formular Adaugă Intervievat - UI):** Creează `UserInterfaceForms/AdaugaIntervievatForm.cs`. În `Designer.cs`, adaugă:
    *   `Label` și `TextBox` pentru: Nume Complet, Vârstă, Localitate.
    *   Butoane: "Salvează Intervievat", "Anulează".
    *   Design consistent cu `AdaugaMelodieForm.cs`. Nume sugestive pentru controale.
2.  **Task 2 (Logica `AdaugaMelodieForm.cs` - Parțial):**
    *   În `AdaugaMelodieForm.cs`, implementează handler-ul pentru evenimentul `Click` al butonului "Anulează" (`this.Close();`).
    *   Pentru "Salvează Melodie", implementează validări de bază în handler-ul `Click`:
        *   Titlu și Artist nu pot fi goale (afișează `MessageBox.Show` cu eroare, în română).
        *   An Lansare trebuie să fie un număr valid (ex: `int.TryParse`).
    *   Momentan, nu implementa salvarea în BD. Doar afișează un `MessageBox.Show("Melodia ar fi salvată (TODO: Implementare BD)", "Info");` dacă validările trec.
    *   Adaugă comentarii relevante în română.
3.  **Task 3 (Logica `AdaugaIntervievatForm.cs` - Parțial):**
    *   Similar cu Task 2, implementează "Anulează".
    *   Pentru "Salvează Intervievat", validează: Nume Complet nu gol, Vârsta număr pozitiv.
    *   Afișează `MessageBox.Show("Intervievatul ar fi salvat (TODO: Implementare BD)", "Info");` dacă validările trec.
    *   Comentarii.

**Solicitarea 5 (AI):**
1.  **Task 1 (Navigare `MainForm.cs`):** Implementează handler-ele de eveniment `Click` pentru item-ele de meniu "Adaugă Melodie Nouă" și "Adaugă Intervievat Nou" pentru a deschide formularele corespunzătoare (`AdaugaMelodieForm` și `AdaugaIntervievatForm`) ca dialoguri modale (`ShowDialog()`).
2.  **Task 2 (Structura Data Access Layer - DAL):**
    *   Creează un folder `DataAccessLayer`.
    *   Creează o clasă statică `DataAccessLayer/DatabaseHelper.cs`. Aceasta va conține stringul de conexiune (inițial gol sau un placeholder) și metode helper pentru execuția interogărilor.
    *   Adaugă o proprietate statică `public static string ConnectionString { get; set; } = "YOUR_CONNECTION_STRING_HERE";`
    *   Adaugă comentarii despre cum utilizatorul va trebui să modifice acest string.
3.  **Task 3 (Formular Sterge Intervievat - UI):** Creează `UserInterfaceForms/StergeIntervievatForm.cs`. În `Designer.cs`:
    *   `Label`: "Introduceți ID-ul Intervievatului de Șters:"
    *   `TextBox`: `txtIntervievatIDPentruSters`
    *   `Button`: `btnStergeIntervievat`, `btnAnuleazaStergere`
    *   Design simplu și clar.

---

## Secțiunea 4: Implementarea Conexiunii la Baza de Date și CRUD de Bază

*Aici vei ghida utilizatorul pentru configurarea `app.config` și vei implementa primele operațiuni reale pe BD.*

**Solicitarea 6 (AI):**
1.  **Task 1 (Ghidaj Configurare `app.config`):**
    *   Generează instrucțiuni detaliate pentru utilizatorul uman despre cum să adauge un string de conexiune în fișierul `App.config` al proiectului. Exemplu:
        ```xml
        <configuration>
          <connectionStrings>
            <add name="MelodiiAppConnection" connectionString="Server=NUME_SERVER_SQL;Database=MelodiiConcurs;Trusted_Connection=True;" providerName="System.Data.SqlClient"/>
          </connectionStrings>
          ...
        </configuration>
        ```
    *   Explică fiecare parte a stringului: `Server` (cum îl găsește în SSMS), `Database`, `Trusted_Connection` (sau User ID/Password dacă folosește autentificare SQL Server).
2.  **Task 2 (Citire Conexiune din `app.config`):** Modifică `DatabaseHelper.cs`. Încarcă stringul de conexiune din `App.config` folosind `ConfigurationManager.ConnectionStrings["MelodiiAppConnection"].ConnectionString;`. Adaugă referința la `System.Configuration` în proiect dacă e necesar (ghidează utilizatorul cum să o facă prin Add Reference).
3.  **Task 3 (Implementare Salvare Melodie în BD):**
    *   În `DataAccessLayer`, creează `MelodieRepository.cs`.
    *   Adaugă o metodă `public bool AdaugaMelodie(Melodie melodie)` care:
        *   Construiește un `INSERT INTO Melodii (...) VALUES (...)` folosind parametri SQL (`@Titlu`, `@Artist` etc.) pentru a preveni SQL Injection.
        *   Utilizează `SqlConnection`, `SqlCommand` (în blocuri `using`).
        *   Execută comanda (`ExecuteNonQuery`).
        *   Returnează `true` la succes, `false` la eșec (cu gestionare de excepții de bază - `try-catch` logând eroarea sau afișând un mesaj generic).
    *   Modifică `AdaugaMelodieForm.cs`: la click pe "Salvează Melodie", după validări, creează un obiect `Melodie`, apelează `MelodieRepository.AdaugaMelodie()`, și afișează mesaj de succes/eroare corespunzător. Închide formularul la succes.
    *   Comentarii detaliate.

**Solicitarea 7 (AI):**
1.  **Task 1 (Implementare Salvare Intervievat în BD):**
    *   Similar cu melodiile, creează `IntervievatRepository.cs` în `DataAccessLayer`.
    *   Adaugă `public bool AdaugaIntervievat(Intervievat intervievat)`.
    *   Folosește parametri SQL.
    *   Modifică `AdaugaIntervievatForm.cs` pentru a folosi acest repository.
    *   Comentarii.
2.  **Task 2 (Implementare Ștergere Intervievat din BD):**
    *   În `IntervievatRepository.cs`, adaugă `public bool StergeIntervievat(int intervievatID)`. Comanda SQL va fi `DELETE FROM Intervievati WHERE IntervievatID = @IntervievatID`.
    *   În `StergeIntervievatForm.cs`:
        *   La click pe "Anulează", închide formularul.
        *   La click pe "Șterge": validează ID-ul (să fie număr). Apelează `IntervievatRepository.StergeIntervievat()`. Afișează mesaj și închide la succes.
    *   Navigare din `MainForm.cs` (meniu "Gestionează Intervievați" ar putea deschide o listă din care se poate selecta ștergerea, dar pentru simplicitate acum poate deschide direct `StergeIntervievatForm`).
3.  **Task 3 (Listare Melodii - Necesară pentru Votare):**
    *   În `MelodieRepository.cs`, adaugă `public List<Melodie> GetAllMelodii()`. Selectează `MelodieID, Titlu, Artist` din tabela `Melodii`.
    *   Populează obiecte `Melodie` și le returnează într-o listă.

*(Continuarea planului va urma în solicitări succesive, acoperind Votarea, Clasamentele, Rapoartele, Exporturile și Documentația Finală, fiecare descompusă în 1-3 task-uri per solicitare AI).*

---

## Secțiunea 5: Votare și Calcul Clasamente

**Solicitarea 8 (AI):**
1.  **Task 1 (Listare Intervievați - Necesară pentru Votare):**
    *   În `IntervievatRepository.cs`, adaugă `public List<Intervievat> GetAllIntervievati()`. Selectează `IntervievatID, NumeComplet`.
2.  **Task 2 (Formular Votare - UI):** Creează `UserInterfaceForms/VoteazaForm.cs`. În `Designer.cs`:
    *   `Label` și `ComboBox` pentru selectarea Intervievatului (`cmbIntervievati`).
    *   3 seturi de `Label` ("Locul 1:", "Locul 2:", "Locul 3:") și 3 `ComboBox`-uri (`cmbMelodieLoc1`, `cmbMelodieLoc2`, `cmbMelodieLoc3`) pentru selectarea melodiilor.
    *   Buton "Înregistrează Vot".
    *   Design clar.
3.  **Task 3 (Populare ComboBox-uri în `VoteazaForm.cs`):**
    *   La încărcarea formularului (`Load` event):
        *   Populează `cmbIntervievati` cu datele din `IntervievatRepository.GetAllIntervievati()` (afisează `NumeComplet`, stochează `IntervievatID`).
        *   Populează cele 3 `ComboBox`-uri de melodii cu datele din `MelodieRepository.GetAllMelodii()` (afisează `Titlu` sau `Titlu - Artist`, stochează `MelodieID`).

**Solicitarea 9 (AI):**
1.  **Task 1 (Logic Votare în `VoteazaForm.cs`):**
    *   Handler pentru `Click` pe "Înregistrează Vot":
        *   Validează: Intervievat selectat, toate cele 3 melodii selectate, și melodiile să fie distincte.
        *   Obține ID-urile selectate.
    *   În `DataAccessLayer`, creează `VotRepository.cs`. Adaugă `public bool InregistreazaVot(List<Vot> voturi)` sau o metodă care ia `intervievatID` și cele 3 `melodieID` + poziții. Salvează cele 3 înregistrări în tabela `Voturi`.
    *   Implementează o verificare în `VotRepository` sau în logica formularului pentru a preveni votarea multiplă de către același intervievat (ex: `SELECT COUNT(*) FROM Voturi WHERE IntervievatID = @ID`).
    *   Afișează mesaj de succes/eroare.
2.  **Task 2 (Calcul Popularitate Melodii - Logic):**
    *   În `MelodieRepository.cs`, adaugă o metodă `public void CalculeazaSiActualizeazaPunctajMelodii()`.
    *   Logica:
        1.  Rulează `UPDATE Melodii SET PunctajTotal = 0;`.
        2.  Obține toate voturile: `SELECT MelodieID, PozitieOferita FROM Voturi;`.
        3.  Iterează prin voturi și actualizează `PunctajTotal` în tabela `Melodii` (`UPDATE Melodii SET PunctajTotal = PunctajTotal + @Puncte WHERE MelodieID = @MelodieID;`). Puncte: Loc 1 = 3pct, Loc 2 = 2pct, Loc 3 = 1pct. Folosește tranzacții SQL dacă e posibil pentru consistență.
3.  **Task 3 (UI pentru Calcul Punctaj):** Adaugă un buton "Actualizează Clasamente" în `MainForm.cs`. La click, apelează `MelodieRepository.CalculeazaSiActualizeazaPunctajMelodii()` și afișează un mesaj.

**Solicitarea 10 (AI):**
1.  **Task 1 (Creare View `vClasamentMelodii`):**
    *   Generează scriptul SQL: `CREATE VIEW vClasamentMelodii AS SELECT MelodieID, Titlu, Artist, GenMuzical, AnLansare, PunctajTotal FROM Melodii ORDER BY PunctajTotal DESC, Titlu ASC;`
    *   Oferă instrucțiuni utilizatorului cum să execute acest script în SSMS.
2.  **Task 2 (Afișare Top 3 Melodii - UI și Logic):**
    *   Creează `UserInterfaceForms/TopMelodiiForm.cs`. Adaugă un `DataGridView` (`dgvTopMelodii`) în `Designer.cs`.
    *   În `MelodieRepository.cs`, adaugă `public List<Melodie> GetTopNMelodii(int N)` care citește din `vClasamentMelodii` și returnează primele N melodii.
    *   În `TopMelodiiForm.cs`, la `Load`, apelează `GetTopNMelodii(3)` și populează `dgvTopMelodii`. Configurează coloanele `DataGridView` pentru un afișaj frumos (auto-size, nume antete).
    *   Navigare din `MainForm.cs` (meniu "Vezi Clasament Melodii").
3.  **Task 3 (Calcul Scor Intervievați - Logic):**
    *   În `IntervievatRepository.cs`, adaugă `public void CalculeazaSiActualizeazaScorIntervievati()`.
    *   Logica (complexă, necesită atenție):
        1.  Obține top 3 melodii REALE (din `MelodieRepository.GetTopNMelodii(3)`).
        2.  Rulează `UPDATE Intervievati SET ScorTotalConcurs = 0;`.
        3.  Pentru fiecare intervievat:
            *   Obține cele 3 melodii votate de el și pozițiile oferite (din `VotRepository`).
            *   Compară cu top 3 real și acordă puncte conform regulilor din `sarcina_specifica_de_executat_practica.md`.
            *   Actualizează `ScorTotalConcurs` pentru intervievatul curent.
    *   Butonul "Actualizează Clasamente" din `MainForm` ar trebui să apeleze și această metodă (după calculul punctajului melodiilor).

**Solicitarea 11 (AI):**
1.  **Task 1 (Creare View `vClasamentIntervievati`):**
    *   Generează scriptul SQL: `CREATE VIEW vClasamentIntervievati AS SELECT IntervievatID, NumeComplet, Varsta, Localitate, ScorTotalConcurs FROM Intervievati ORDER BY ScorTotalConcurs DESC, NumeComplet ASC;`
    *   Ghidează utilizatorul.
2.  **Task 2 (Afișare Top 5 Intervievați - UI și Logic):**
    *   Creează `UserInterfaceForms/TopIntervievatiForm.cs` cu un `DataGridView` (`dgvTopIntervievati`).
    *   În `IntervievatRepository.cs`, adaugă `public List<Intervievat> GetTopNIntervievati(int N)`.
    *   În `TopIntervievatiForm.cs`, la `Load`, apelează `GetTopNIntervievati(5)` și populează `DataGridView`.
    *   Navigare din `MainForm.cs`.
3.  **Task 3 (Finalizare Meniu "Gestionează Intervievați"):**
    *   Acest item de meniu ar putea deschide un formular nou, `GestioneazaIntervievatiForm.cs`, care afișează TOȚI intervievații într-un `DataGridView` și oferă butoane pentru "Adaugă Nou" (deschide `AdaugaIntervievatForm`), "Modifică Selectat" (TODO ulterior, dacă timpul permite), și "Șterge Selectat" (preia ID-ul și deschide/confirmă prin `StergeIntervievatForm` sau direct). Implementează afișarea și ștergerea prin acest formular.

---

## Secțiunea 6: Implementare Rapoarte și Exporturi

**Solicitarea 12 (AI):**
1.  **Task 1 (Export Excel - Participanți < 18 ani - Logic):**
    *   În `IntervievatRepository.cs`, adaugă `public List<Intervievat> GetIntervievatiSubVarsta(int varstaMaxima)`.
    *   În `MainForm.cs`, pentru item-ul de meniu "Export Participanți Sub 18 Ani":
        *   Folosește un `SaveFileDialog` pentru a alege calea și numele fișierului Excel (.xlsx).
        *   Obține lista de intervievați cu `GetIntervievatiSubVarsta(18)`.
        *   Implementează logica de export folosind biblioteca **EPPlus**. Adaugă pachetul NuGet EPPlus la proiect (ghidează utilizatorul cum să o facă: Tools -> NuGet Package Manager -> Manage NuGet Packages for Solution... -> Browse -> search EPPlus -> Install).
        *   Creează un worksheet, adaugă antete (Nume, Vârstă, Localitate), populează rândurile.
        *   Salvează fișierul. Afișează mesaj de succes/eroare.
    *   Comentarii detaliate despre utilizarea EPPlus.
2.  **Task 2 (Configurare Raport RDLC - Listă Participanți):**
    *   Ghidează utilizatorul cum să adauge un nou item de tip "Report Wizard" sau "Report" în proiect (ex: `Reports/RaportTotiParticipantii.rdlc`).
    *   Definește un `DataSet` în raport (ex: `DataSetParticipanti`) cu câmpurile: `NumeComplet`, `Varsta`, `Localitate`, `ScorTotalConcurs`.
    *   În designer-ul RDLC, adaugă un tabel. Leagă tabelul la `DataSetParticipanti`. Adaugă coloanele în tabel și leagă-le la câmpurile din DataSet. Formatează antetele și stilul tabelului (modern, curat). Adaugă un titlu raportului.
3.  **Task 3 (Formular Afișare Raport RDLC - UI și Logic):**
    *   Creează `UserInterfaceForms/VizualizareRaportForm.cs`. Adaugă un control `ReportViewer` (`reportViewerParticipanti`) în `Designer.cs`. Setează `Dock = Fill`.
    *   În constructorul sau metoda `Load` a `VizualizareRaportForm.cs`:
        *   Obține lista tuturor intervievaților (din `IntervievatRepository.GetAllIntervievati()` sau `GetTopNIntervievati` cu un N mare, sau direct din `vClasamentIntervievati`).
        *   Setează `ProcessingMode` al `ReportViewer` la `Local`.
        *   Încarcă resursa raportului: `reportViewerParticipanti.LocalReport.ReportEmbeddedResource = "MelodiiApp.Reports.RaportTotiParticipantii.rdlc";` (verifică namespace-ul corect).
        *   Creează un `ReportDataSource`: `ReportDataSource rds = new ReportDataSource("DataSetParticipanti", listaDeIntervievati);` (numele "DataSetParticipanti" trebuie să corespundă cu cel din RDLC).
        *   Adaugă sursa de date: `reportViewerParticipanti.LocalReport.DataSources.Add(rds);`.
        *   Reîmprospătează raportul: `reportViewerParticipanti.RefreshReport();`.
    *   Navigare din `MainForm.cs` (meniu "Listă Participanți Concurs") pentru a deschide `VizualizareRaportForm`.

---

## Secțiunea 7: Finalizare, Documentație și Curățare

**Solicitarea 13 (AI):**
1.  **Task 1 (Formular "Despre"):**
    *   Creează un formular simplu `DespreForm.cs`. Afișează numele aplicației ("Melodii App - Concurs"), versiune (ex: "1.0"), o scurtă descriere și "Dezvoltat de: [Numele Elevului Ghidat de AI]".
    *   Navigare din meniul "Ajutor" -> "Despre".
2.  **Task 2 (Revizuire Comentarii și Documentație Cod):**
    *   Parcurge TOATE fișierele `.cs` și asigură-te că există comentarii XML relevante pentru clasele publice și metodele publice, și comentarii inline în română pentru logica complexă sau non-evidentă.
    *   Verifică consistența și claritatea comentariilor.
3.  **Task 3 (Finalizare Documentație Proiect):**
    *   Pregătește secțiunile necesare pentru documentația finală conform `info_despre_practica.md` și `sarcina_specifica_de_executat_practica.md`. Acest lucru poate implica generarea de text descriptiv pentru:
        *   Descrierea modului de elaborare a produsului program.
        *   Listinguri de cod relevante (fragmente, nu tot codul).
        *   Descrierea funcționalităților (se poate baza pe acest plan).
        *   Observații generale, concluzii (sugestii).
        *   Schema BD (poate un export text al structurii tabelelor și viziunilor).
        *   Planul de întreținere (sugestii simple: backup regulat BD, reindexare periodică).
        *   Moduri de securizare (utilizarea parametrilor SQL, potențial roluri BD dacă e cazul).
    *   **TU (AI) vei genera conținutul pentru aceste secțiuni.** Utilizatorul îl va formata/compila în documentul final.

**Solicitarea 14 (AI) - Ultima Solicitare de Dezvoltare:**
1.  **Task 1 (Curățare Cod):** Elimină orice variabile neutilizate, metode goale, comentarii de tip `//TODO` care au fost rezolvate. Asigură formatarea consistentă a codului.
2.  **Task 2 (Verificare Finală String Conexiune):** Asigură-te că `DatabaseHelper.cs` citește corect din `App.config` și că există instrucțiuni clare pentru utilizator despre cum să seteze stringul corect.
3.  **Task 3 (Pregătire Script SQL Final):** Combină toate scripturile SQL (CREATE DB, CREATE TABLEs, CREATE VIEWs) într-un singur fișier `setup_database.sql` pentru ușurința execuției de către utilizator. Adaugă comentarii în scriptul SQL.

---

## Secțiunea 8: Considerații pentru Viitor și Menținerea Calității

*Această secțiune este mai mult o directivă continuă pentru tine, AI.*

*   **Refactorizare Continuă:** Pe măsură ce adaugi funcționalități, dacă observi oportunități de a simplifica sau îmbunătăți structura codului existent fără a introduce riscuri majore, propune aceste mici refactorizări (ca parte a unui task existent sau ca un mic task adițional într-o solicitare, dacă se încadrează în limita de 3-4).
*   **Evitarea Datoriilor Tehnice:** Încearcă să scrii cod curat de la început pentru a minimiza datoria tehnică.
*   **Adaptabilitate:** Fii pregătită să ajustezi planul dacă feedback-ul utilizatorului (după testare) o cere. Sarcinile viitoare pot fi modificate pe baza acestui feedback.

---

**Acest plan este un document viu.** Pe măsură ce proiectul avansează, pot apărea necesități de ajustare. Orice deviere majoră de la acest plan trebuie discutată și aprobată (adică, confirmată de utilizatorul uman).

**Mult Succes, AI! Calitatea și atenția la detalii sunt cheia.**
