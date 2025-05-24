# Sarcină Specifică Detaliată pentru Stagiul de Practică Tehnologică (Pentru AI)

**Atenție Inteligență Artificială (AI):**

Acest document, `sarcina_specifica_de_executat_practica.md`, reprezintă **sarcina individuală specifică și detaliată** pe care **TU, Inteligența Artificială, trebuie să o execuți integral** pentru stagiul de practică tehnologică "Administrarea Bazelor de Date", sub tema "18. MELODII". Acesta NU este un exemplu, ci **caietul tău de sarcini direct**.

**Obiectivul tău principal:** Realizarea completă a proiectului descris, atingând un nivel de calitate corespunzător notei maxime (10). Aceasta implică:
1.  **Proiectarea și Implementarea Completă a Bazei de Date:** Pe SQL Server.
2.  **Dezvoltarea Aplicației Vizuale:** În C# Windows Forms (.NET Framework).
3.  **Documentarea Exhaustivă:** Atât a bazei de date, cât și a aplicației și a codului sursă.
4.  **Asigurarea Execuției Funcționale:** Pregătirea aplicației pentru testare de către utilizatorul uman.

**Capabilitățile tale și Procesul de Lucru:**
*   **Tu (AI) vei realiza:**
    *   **Codificarea:** Scrierea codului C# și SQL necesar pentru toate funcționalitățile.
    *   **Designul UI:** Crearea interfețelor grafice conform specificațiilor de mai jos.
    *   **Documentarea:** Generarea documentației tehnice (schema bazei de date, descrierea modulelor aplicației, comentarii în cod) și de utilizare (ghid sumar).
    *   **Execuția:** Pregătirea proiectului pentru a putea fi compilat și rulat de utilizatorul uman.
*   **Testarea:** Va fi efectuată de către utilizatorul uman ("elevul" care te utilizează), care îți va furniza feedback pentru iterații și corecții. Tu trebuie să livrezi un produs cât mai robust și complet posibil pentru această fază, cu validări de bază și gestionarea erorilor implementate.

**Cerințe Tehnice și de Design Specifice:**
*   **Tehnologii:** C# Windows Forms, .NET Framework (ultima versiune stabilă accesibilă), SQL Server (o versiune recentă, ex. SQL Server Express).
*   **Design UI (Extrem de Important!):**
    *   Toate elementele vizuale ale interfeței (formulare, butoane, etichete, câmpuri de text, DataGridView, etc.) trebuie create astfel încât să fie **editabile prin designer-ul vizual al Visual Studio** (de ex., `NumeFormular.cs [Design]`).
    *   Logica de creare și inițializare a controalelor UI trebuie să fie generată automat de Visual Studio în fișierul `NumeFormular.Designer.cs`.
    *   **NU crea și NU manipula proprietățile de design ale controalelor programatic direct în fișierul `.cs` principal al formularului** (de ex., `MelodiiForm.cs`) dacă acest lucru poate fi făcut prin designer. Codul din fișierul `.cs` principal trebuie să se concentreze pe logica de business și evenimente.
    *   Această abordare este crucială pentru ca utilizatorul uman să poată ajusta cu ușurință aspectul vizual.
    *   Tu, AI, vei avea acces la toate fișierele proiectului și trebuie să te asiguri că designul facilitează modificările vizuale.
    *   Interfața cu utilizatorul trebuie să fie **modernă, estetică, aerisită și intuitivă.** Utilizează o paletă de culori consistentă și plăcută. Asigură alinierea corectă a elementelor și un flux logic de navigare.
*   **Baza de Date (SQL Server):**
    *   Proiectează o schemă de bază de date robustă și normalizată (minim 3NF, unde este logic).
    *   Definește clar criteriile de integritate (chei primare, chei externe, constrângeri `UNIQUE`, `CHECK`, `NOT NULL`).
    *   Implementează obiecte ale bazei de date: tabele. Consideră utilizarea de viziuni (views) sau proceduri stocate dacă simplifică logica aplicației sau îmbunătățesc performanța pentru interogări complexe.
    *   Generează scripturi SQL pentru crearea bazei de date și a tuturor obiectelor sale.
    *   Documentează schema bazei de date (diagrama Entitate-Relație, descrierea tabelelor și coloanelor).
*   **Aplicația C# Windows Forms:**
    *   Structurează codul în mod logic (ex: clase separate pentru acces la date, clase pentru entități model, etc.).
    *   Implementează validări pentru datele de intrare (atât la nivel de UI, cât și înainte de inserarea în baza de date).
    *   Gestionează erorile într-un mod prietenos pentru utilizator (ex: mesaje clare, evitarea crash-urilor).
    *   Utilizează comentarii XML în codul C# pentru a explica funcționalitatea claselor și metodelor publice.
    *   Conexiunea la baza de date SQL Server trebuie gestionată eficient (ex: `using` statements pentru obiectele `SqlConnection`, `SqlCommand`, etc.). Stringul de conexiune trebuie să fie configurabil (ex: într-un fișier `app.config`).
*   **Documentație Generală Proiect:**
    *   Conform structurii din Anexa 1 a fișierului `info_despre_practica.md`.
    *   Include un plan sumar de întreținere a bazei de date (ex: sugestii pentru backup, indexare).
    *   Include moduri de securizare a datelor din baza de date (ex: roluri SQL Server, utilizarea parametrilor în interogări pentru a preveni SQL Injection).

---

## Descrierea Proiectului: "18. MELODII"

**Context:** Se organizează un concurs-sondaj pentru a stabili cea mai populară melodie din țară. Informațiile despre melodii și persoanele intervievate sunt înregistrate într-o bază de date, iar gestiunea acestora se va realiza printr-o aplicație vizuală C# Windows Forms.

---

## Pași Detaliați și Funcționalități de Implementat (Pentru AI):

### Partea I: Proiectarea și Crearea Bazei de Date

1.  **Analiza Entităților și Atributelor:**
    *   Identifică entitățile principale: `Melodii`, `Intervievati`, `Voturi`.
    *   Pentru `Melodii`: `MelodieID` (PK), `Titlu`, `Artist`, `GenMuzical`, `AnLansare`, `PunctajTotal` (calculat ulterior).
    *   Pentru `Intervievati`: `IntervievatID` (PK), `NumeComplet`, `Varsta`, `Localitate`, `ScorTotalConcurs` (calculat ulterior).
    *   Pentru `Voturi`: `VotID` (PK), `IntervievatID` (FK), `MelodieID` (FK), `PozitieOferita` (1, 2, sau 3).
2.  **Crearea Schemei Bazei de Date:**
    *   Realizează diagrama Entitate-Relație.
    *   Definește tipurile de date adecvate pentru fiecare coloană.
    *   Stabilește cheile primare, cheile externe și constrângerile de integritate (ex: `Varsta > 0`, `PozitieOferita IN (1,2,3)`).
3.  **Generarea Scriptului SQL:**
    *   Scrie scriptul `CREATE DATABASE MelodiiConcurs;`.
    *   Scrie scripturile `CREATE TABLE` pentru `Melodii`, `Intervievati`, `Voturi`, incluzând toate constrângerile.
4.  **Documentarea Bazei de Date:**
    *   Include diagrama E-R în documentația proiectului.
    *   Descrie fiecare tabelă, coloană, și constrângere.

### Partea II: Dezvoltarea Aplicației Vizuale C# Windows Forms

**Structura Generală a Aplicației:**
*   Un formular principal (ex: `MainForm.cs`) cu un meniu pentru a accesa diferitele funcționalități.
*   Formulare separate pentru fiecare operație majoră (adăugare melodie, adăugare intervievat, votare, afișare clasamente).
*   Clase pentru logica de acces la date (DAL) și, opțional, clase model pentru entități (POCOs).

**Funcționalități Specifice:**

**1. Înregistrează o nouă melodie:**
    a.  **UI:** Creează un formular `AdaugaMelodieForm.cs`. Utilizează controale standard (`Label`, `TextBox`, `Button`) plasate în `AdaugaMelodieForm.Designer.cs`. Design modern și intuitiv.
    b.  **Input:** Câmpuri pentru Titlu, Artist, Gen Muzical, An Lansare.
    c.  **Logic:**
        *   Validează datele de intrare (ex: Titlu și Artist nu pot fi goale, An Lansare să fie un număr valid).
        *   La apăsarea butonului "Salvează", inserează noua melodie în tabela `Melodii`. `PunctajTotal` va fi inițial 0 sau NULL.
        *   Afișează un mesaj de succes sau eroare.
    d.  **Documentație:** Descrie formularul și funcționalitatea. Comentează codul C#.

**2. Înregistrează un nou intervievat:**
    a.  **UI:** Creează un formular `AdaugaIntervievatForm.cs` (design similar cu cel de la melodii).
    b.  **Input:** Câmpuri pentru Nume Complet, Vârstă, Localitate.
    c.  **Logic:**
        *   Validează datele (ex: Nume nu poate fi gol, Vârsta să fie un număr pozitiv).
        *   Salvează noul intervievat în tabela `Intervievati`. `ScorTotalConcurs` va fi inițial 0 sau NULL.
        *   Afișează mesaj de confirmare/eroare.
    d.  **Documentație:** Descrie formularul și funcționalitatea.

**3. Exclude din concurs intervievatul pentru codul indicat de la tastatură:**
    a.  **UI:** Un mic formular `StergeIntervievatForm.cs` sau un `InputBox` pentru a introduce `IntervievatID`. Un buton de confirmare.
    b.  **Logic:**
        *   Validează ID-ul (să fie număr).
        *   Verifică dacă intervievatul există.
        *   Înainte de ștergere, gestionează înregistrările asociate din `Voturi` (ex: ștergere în cascadă dacă este definită în BD, sau ștergere manuală).
        *   Șterge intervievatul din tabela `Intervievati`.
        *   Afișează mesaj de confirmare/eroare.
    c.  **Documentație:** Descrie procesul.

**4. Management Voturi și Calcul Popularitate Melodii:**
    a.  **UI Votare:** Creează un formular `VoteazaForm.cs`.
        *   Permite selectarea unui intervievat (ex: dintr-un `ComboBox` populat din BD).
        *   Permite selectarea a 3 melodii distincte pentru locurile 1, 2 și 3 (ex: 3 seturi de `ComboBox`-uri cu melodii, asigurând că nu se poate selecta aceeași melodie de mai multe ori).
        *   Buton "Înregistrează Vot".
    b.  **Logic Votare:**
        *   Validează selecțiile.
        *   Salvează cele 3 alegeri în tabela `Voturi`, asociate cu `IntervievatID` și `MelodieID` corespunzătoare, și `PozitieOferita` (1, 2, 3).
        *   Un intervievat poate vota o singură dată (implementează verificare).
    c.  **Calcul Popularitate Melodii:**
        *   Implementează o funcționalitate (poate un buton în `MainForm` numit "Calculează Clasament Melodii") care:
            *   Golește/resetează `PunctajTotal` la 0 pentru toate melodiile în tabela `Melodii`.
            *   Iterează prin tabela `Voturi`. Pentru fiecare vot:
                *   Dacă `PozitieOferita` este 1, adaugă 3 puncte la `PunctajTotal` melodiei corespunzătoare.
                *   Dacă `PozitieOferita` este 2, adaugă 2 puncte.
                *   Dacă `PozitieOferita` este 3, adaugă 1 punct.
            *   Actualizează `PunctajTotal` în tabela `Melodii`.
    d.  **Creare Tabelă/View pentru Clasament Melodii (Cerută ca "o tabelă"):**
        *   Cea mai bună abordare este crearea unei **viziuni SQL (View)** numită `vClasamentMelodii` care selectează `MelodieID`, `Titlu`, `Artist`, `PunctajTotal` din tabela `Melodii`, ordonate descrescător după `PunctajTotal`. Aceasta este dinamică și nu necesită creare/populare manuală repetată.
        *   Alternativ, dacă se cere explicit o tabelă fizică, după calculul punctajelor, poți popula o tabelă `ClasamentMelodiiFizic` cu aceste date. Aceasta ar necesita actualizare după fiecare recalculare a punctajelor. **Preferă viziunea.**
    e.  **Documentație:** Descrie procesul de votare, calculul punctajelor și structura viziunii/tabelei de clasament.

**5. Afişează la ecran lista primelor 3 melodii în ordinea popularităţii:**
    a.  **UI:** În `MainForm` sau un nou formular `TopMelodiiForm.cs`. Utilizează un `DataGridView` pentru afișare (configurat prin `Designer.cs`).
    b.  **Logic:**
        *   Interoghează viziunea `vClasamentMelodii` (sau tabela de clasament) și preia primele 3 înregistrări.
        *   Populează `DataGridView`-ul. Asigură un aspect "modern și frumos".
    c.  **Documentație:** Descrie afișarea.

**6. Calcul Scor Intervievați:**
    a.  **Logic:** Implementează o funcționalitate (poate un buton "Calculează Scoruri Intervievați") care:
        *   Mai întâi, asigură-te că clasamentul melodiilor (top 3) este stabilit (din task 4 și 5). Stochează acest top 3 actual.
        *   Golește/resetează `ScorTotalConcurs` la 0 pentru toți intervievații.
        *   Pentru fiecare `Intervievat`:
            *   Preia voturile sale din tabela `Voturi`.
            *   Compară fiecare din cele 3 melodii votate de el și poziția oferită cu topul real al primelor 3 melodii.
            *   Calculează punctele conform regulilor:
                *   10 puncte pentru fiecare melodie unde poziția ghicită corespunde cu cea din top 3 real.
                *   5 puncte dacă a greșit cu o poziție (ex: a votat melodia X pe locul 1, iar ea e pe 2 în top; sau a votat Y pe 2, iar ea e pe 1 sau 3).
                *   3 puncte dacă a greșit cu 2 poziții (ex: a votat Z pe 1, iar ea e pe 3 în top; sau a votat W pe 3, iar ea e pe 1).
            *   Adună punctele la `ScorTotalConcurs` pentru acel `Intervievat` și actualizează în BD.
    b.  **Documentație:** Detaliază algoritmul complex de calcul al scorului pentru intervievați.

**7. Creează o tabelă (View) pentru Clasamentul Intervievaților:**
    a.  **Creare View:** Similar cu melodiile, creează o viziune SQL `vClasamentIntervievati` care selectează `IntervievatID`, `NumeComplet`, `Varsta`, `Localitate`, `ScorTotalConcurs` din tabela `Intervievati`, ordonată descrescător după `ScorTotalConcurs`.
    b.  **Documentație:** Descrie viziunea.

**8. Să se afişeze la ecran datele despre persoanele care au ocupat primele 5 locuri (intervievați):**
    a.  **UI:** În `MainForm` sau un nou formular `TopIntervievatiForm.cs`. Utilizează un `DataGridView`.
    b.  **Logic:**
        *   Interoghează viziunea `vClasamentIntervievati` și preia primele 5 înregistrări.
        *   Populează `DataGridView`-ul. Aspect "modern și frumos".
    c.  **Documentație:** Descrie afișarea.

**9. Exportă într-un fișier MS Word sau MS Excel lista participantelor a căror vârsta nu depăşeşte 18 ani:**
    a.  **UI:** Un buton în `MainForm` sau într-un formular de raportare. La click, deschide un `SaveFileDialog` pentru a permite utilizatorului să aleagă locația și numele fișierului (cu extensia .xlsx sau .docx).
    b.  **Logic:**
        *   Interoghează tabela `Intervievati` pentru a selecta persoanele cu `Varsta <= 18`.
        *   Pentru **Excel (preferabil pentru date tabulare):** Utilizează o bibliotecă precum **EPPlus** (este gratuită pentru uz non-comercial și ușor de folosit în .NET Framework, nu necesită Excel instalat). Creează un fișier Excel cu coloanele relevante (Nume, Vârstă, Localitate).
        *   Pentru **Word:** Poți folosi biblioteca **Open-XML-SDK** sau, dacă este permis și Office este instalat pe mașina de test, **Office Interop**. Crearea unui tabel în Word poate fi mai complexă.
        *   **AI, alege o metodă (EPPlus pentru Excel este o recomandare bună) și implementeaz-o.**
        *   Afișează mesaj de succes/eroare la finalizarea exportului.
    c.  **Documentație:** Descrie funcționalitatea de export și biblioteca utilizată, inclusiv cum se adaugă (dacă e cazul, ex: NuGet package).

**10. Afişează la ecran lista participanților la acest concurs. Afișarea la ecran a informațiilor acestora se va realiza prin intermediul rapoartelor:**
    a.  **UI și Logică Raport:**
        *   **Metoda recomandată:** Utilizează **RDLC Reports (Report Definition Language Client-side)**. Acestea se integrează bine cu Windows Forms în .NET Framework și nu necesită SQL Server Reporting Services instalat pe server.
        *   Creează un fișier `.rdlc` în proiectul tău (ex: `RaportParticipanti.rdlc`).
        *   În designerul de raport RDLC, definește un `DataSet` care va fi populat cu datele intervievaților.
        *   Structurează raportul: titlu, antet tabel, coloane pentru `NumeComplet`, `Varsta`, `Localitate`, `ScorTotalConcurs`. Adaugă elemente de design pentru un aspect "modern și frumos".
        *   Creează un nou formular `RaportForm.cs` care va conține un control `ReportViewer`.
        *   În codul `RaportForm.cs`, la încărcare:
            *   Preia datele tuturor intervievaților din baza de date (din `vClasamentIntervievati` sau direct din `Intervievati`).
            *   Creează un `ReportDataSource` cu aceste date.
            *   Încarcă definiția raportului (`.rdlc`) în controlul `ReportViewer` și atașează sursa de date.
        *   Afișează `RaportForm.cs` când utilizatorul solicită raportul.
    b.  **Documentație:** Descrie crearea și afișarea raportului RDLC, inclusiv configurarea `DataSet`-ului și a controlului `ReportViewer`.

### Partea III: Documentație Finală și Pregătire Proiect

1.  **Completarea Documentației Proiectului:**
    *   Revizuiește și finalizează toate secțiunile documentației conform Anexei 1 din `info_despre_practica.md`.
    *   Asigură-te că documentația bazei de date (schema, scripturi, descrieri) este completă.
    *   Asigură-te că documentația aplicației (descrierea modulelor, ghid de utilizare sumar cu capturi de ecran ale formularelor principale, comentarii XML în cod) este la zi.
    *   Include planul de întreținere și securizare a bazei de date.
2.  **Curățarea și Organizarea Codului:**
    *   Verifică dacă codul este bine formatat, lizibil și comentat corespunzător.
    *   Elimină orice cod de testare sau neutilizat.
3.  **Pregătirea pentru Livrare (către utilizatorul uman pentru testare):**
    *   Asigură-te că proiectul compilează fără erori.
    *   Include scriptul SQL pentru crearea bazei de date.
    *   Oferă instrucțiuni clare despre cum se configurează stringul de conexiune la baza de date (dacă este în `app.config`).

**COORDONATORUL STAGIULUI DE PRACTICĂ:** PROF. COVALI EUGENIA (Această informație este pentru contextul original al sarcinii).