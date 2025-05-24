## Starea Proiectului și Probleme Identificate

**Notă Generală:** Aplicația pornește fără erori de compilare și rulează, dar necesită îmbunătățiri semnificative funcționale, de uzabilitate, și clarificări de roluri.

**Definiții Roluri Utilizatori:**
*   **Administrator (Admin):**
    *   Gestionează melodiile (adăugare, modificare, ștergere).
    *   Gestionează intervievații (persoanele intervievate pe stradă - adăugare, modificare, ștergere).
    *   **Introduce manual predicțiile Top 3 melodii în numele fiecărui intervievat.**
    *   Actualizează clasamentele.
    *   Generează rapoarte.
    *   Nu participă la votul popular pentru melodii.
*   **Utilizator Standard (Votant):**
    *   Utilizator înregistrat al aplicației.
    *   **Unica acțiune principală este de a vota melodiile preferate, alocând un total de 6 puncte.** Acest vot colectiv determină clasamentul real al popularității melodiilor.
    *   **NU este un intervievat.** Nu face predicții pentru concursul de intervievați.
    *   Are acces limitat la funcționalitățile aplicației (doar vot și informații despre aplicație).

**Flux Principal Concurs:**
1.  Adminii adaugă melodiile participante în sistem.
2.  Adminii adaugă "intervievații" (persoane de pe stradă) în sistem.
3.  Adminii introduc în sistem predicțiile fiecărui "intervievat" pentru care cred ei că vor fi Top 3 melodii.
4.  Utilizatorii Standard (Votanții) descarcă aplicația, se înregistrează și votează melodiile preferate (alocând 6 puncte).
5.  Pe baza voturilor cumulate de la toți Utilizatorii Standard, se stabilește clasamentul real al melodiilor (Top N Melodii).
6.  Scorul fiecărui "intervievat" se calculează comparând predicțiile lor (introduse de Admin) cu clasamentul real al melodiilor (stabilit prin vot popular).

---
### **Prioritate MAXIMĂ - Revizuire Urgentă UI/UX & RBAC:**

1.  **Implementare Strictă Control Acces Bazat pe Rol (RBAC) & Reconfigurare UI:** (Fostul punct 5, extins)
    *   **Problemă:** Distincția între roluri este neclară în UI; utilizatorii standard au acces la funcționalități de admin. Conceptul de "Utilizator este Intervievat" este incorect.
    *   **Acțiune:**
        *   **Admin View:**
            *   Acces la tab-urile: "Melodii (Admin)", "Intervievați (Admin)", "Management Predicții" (fostul "Acțiuni Utilizator" reconfigurat), "Administrare Clasamente", "Rapoarte", "Despre".
            *   În tab-ul "Management Predicții": butonul "Înregistrează Predicții Top 3" (pentru a introduce predicțiile intervievaților) este vizibil. Butonul de votare melodii este ascuns.
        *   **Utilizator Standard (Votant) View:**
            *   Acces NUMAI la tab-urile: "Votează Melodii" (fostul "Acțiuni Utilizator" reconfigurat) și "Despre".
            *   În tab-ul "Votează Melodii": butonul "Votează Melodii (6 puncte)" este vizibil. Butonul "Înregistrează Predicții Top 3" este ascuns.
            *   Toate celelalte tab-uri (Melodii Admin, Intervievați Admin, Management Predicții dacă e separat, Administrare Clasamente, Rapoarte) sunt ascunse.
        *   Actualizează `MainForm.cs` (`ConfigureUIForRole`) și `MainForm.Designer.cs` (redenumire tab) pentru a reflecta aceste modificări.
        *   Asigură că logica de login transmite corect rolul către `MainForm`.

2.  **Revizuire Completă Navigare și Uzabilitate (TRECERE LA TABURI):** (Fostul punct 1 din Prioritate MAXIMĂ - REZOLVAT parțial, necesită finalizare RBAC)
    *   **Problemă:** Interfața necesita o structură clară. Trecerea la `TabControl` a fost un pas.
    *   **Acțiune (Parțial Rezolvată):**
        *   Înlocuit `MenuStrip` și ulterior sidebar-ul din `MainForm` cu un `TabControl` pentru navigare.
        *   Implementarea RBAC de la punctul 1 (mai sus) va finaliza structura corectă a tab-urilor vizibile per rol.

---
### **Prioritate Înaltă - Funcționalități Cheie Blocate sau Incorecte:**

3.  **Sistem de Predicții Top 3 Melodii (Introducere de Admin):** (Fostul punct 3, actualizat)
    *   **Problemă:** Mecanismul de predicții trebuie să fie un instrument pentru Admin, nu pentru utilizatorul standard.
    *   **Acțiune:**
        *   Asigură că `PreziceTopMelodiiControl` (sau `PreziceTopMelodiiForm`) este accesibil și utilizabil de către Admin pentru a selecta un intervievat și a introduce predicțiile acestuia.
        *   Verifică fluxul de salvare și asocierea corectă a predicțiilor cu intervievații.
        *   Calculul scorului în `IntervievatRepository` utilizează aceste predicții introduse de Admin.

4.  **Sistem de Votare (Popularitate Melodii - Contribuție Utilizatori Standard):** (Fostul punct 2 - REZOLVAT funcțional, aliniere la RBAC)
    *   **Problemă:** Sistemul de votare este funcțional dar trebuie accesibil corect conform rolului.
    *   **Acțiune (Rezolvată funcțional):** Sistemul de votare (6 puncte) este implementat. Asigură că este accesibil DOAR Utilizatorilor Standard (Votanți) conform RBAC (Punctul 1).

5.  **Generarea Rapoartelor Eșuează sau Inaccesibilă:** (Fostul punct 4)
    *   **Problemă:** Funcționalitatea de generare a rapoartelor (ex: lista participanților) nu funcționează sau nu e corect expusă.
    *   **Acțiune:** Depanează și asigură funcționarea corectă a rapoartelor. Asigură accesibilitatea lor pentru rolul Admin. Implementează funcționalitatea de bază pentru `btnListaParticipanti` și finalizează `btnExportParticipantiSub18` (CSV este un început bun).

6.  **Securitate Parole Insuficientă:** (Fostul punct 6)
    *   **Problemă:** Aplicația permite parole extrem de slabe.
    *   **Acțiune:** Implementează cerințe minime de complexitate pentru parole la înregistrare/modificare parolă.

---
### **Prioritate Medie - Funcționalități Importante și Probleme Majore de Uzabilitate:**

7.  **Probleme Input "Dată de Naștere":** (Fostul punct 7)
    *   **Problemă:** Câmpul pentru introducerea datei de naștere (dacă este încă relevant și utilizat în formulare pentru Intervievați) prezintă multiple probleme.
    *   **Acțiune:** Corectează controlul pentru data nașterii în formularele de management ale Intervievaților (Admin).

8.  **Management Profil Utilizator și Acces Admin:** (Fostul punct 8)
    *   **Problemă:** Nu există funcționalități clare pentru gestionarea profilului utilizatorului (ex: logout) sau un proces distinct pentru autentificarea ca administrator.
    *   **Acțiune:** Implementează mecanisme de logout. Asigură că procesul de login distinge și setează corect rolurile Admin/Utilizator Standard.

9.  **Restricționare Adăugare Melodii:** (Fostul punct 9 - REITERAT în definiția rolului Admin)
    *   **Problemă:** Necesită confirmare că doar Adminii adaugă melodii.
    *   **Acțiune:** Confirmat și implementat prin RBAC (Punctul 1).

---
### **Prioritate Scăzută - Îmbunătățiri UI/UX și Funcționalități Noi/Secundare:**

10. **Aspect Vizual și Design General (Noua Paletă de Culori):** (Fostul punct 11, actualizat)
    *   **Problemă:** Schema de culori actuală nu este pe placul utilizatorului. Controalele pot avea scheme de culori inconsistente.
    *   **Acțiune:**
        *   Implementează noua paletă de culori specificată: `#003049` (Dark Blue), `#669bbc` (Mid Blue), `#fdf0d5` (Cream), `#c1121f` (Red), `#780000` (Dark Red).
        *   Aspiră la un design "happy" și "light", folosind `#fdf0d5` ca bază.
        *   Aplică schema consistent în `MainForm` (MaterialSkin, fundaluri `TabPage`, butoane custom) și asigură propagarea sau suprascrierea în `UserControl`-uri pentru un aspect unitar.

11. **Intuitivitatea Generală a Aplicației (Post-RBAC):** (Fostul punct 10)
    *   **Problemă:** Fluxul general al aplicației și interacțiunea cu utilizatorul.
    *   **Acțiune:** Revizuiește și îmbunătățește navigarea și fluxurile de lucru.

12. **Adaptabilitatea Ferestrei (Responsiveness - Detaliat):** (Fostul punct 13)
    *   **Problemă:** Elementele UI din UserControl-uri s-ar putea să nu se adapteze corect.
    *   **Acțiune:** Îmbunătățește adaptabilitatea interfeței.

13. **Funcționalitate "Ajutor":** (Fostul punct 14)
    *   **Problemă:** Secțiunea de Ajutor lipsește sau nu funcționează.
    *   **Acțiune:** Implementează o secțiune de "Ajutor" de bază (posibil `btnDespre` actual este suficient pentru moment).

14. **Sistem Cereri Melodii (Funcționalitate Nouă):** (Fostul punct 15)
    *   **Problemă:** Funcționalitatea prin care utilizatorii pot propune melodii nu este implementată.
    *   **Acțiune:** Proiectează și implementează (prioritate scăzută).

---
### **Funcționalități Rezolvate / Pași Anteriori:**

*   **Admin vs User (Meniuri):** Conceptul inițial a evoluat. Acum se definește prin RBAC și vizibilitatea tab-urilor.
*   **Modificarea Intervievaților:** Funcționalitate de bază implementată în `EditIntervievatForm` și `GestioneazaIntervievatiControl` (Admin).
*   **Export CSV Participanți Sub 18 Ani:** Implementat.
*   **Funcționalitate `btnDespre`:** Implementat.
*   **Funcționalitate `btnActualizeazaClasamente` (placeholder & refresh):** Implementat.

---
**Specificații Design UI (NOUĂ PALETĂ):**
*   **Culoare Principală (ex: App Bar Header - text alb pe el):** `#003049` (Dark Blue)
*   **Culoare Secundară (ex: Butoane, Elemente Interactive):** `#669bbc` (Mid Blue)
*   **Fundal Principal (Light Theme Base):** `#fdf0d5` (Cream)
*   **Accent Principal (ex: Ripple la butoane, elemente focusate):** `#c1121f` (Red)
*   **Accent Secundar (ex: Avertismente, erori subtile):** `#780000` (Dark Red)
*   **Text pe Fundal Deschis:** `#003049` (Dark Blue) sau `Color.Black`
*   **Text pe Fundal Închis (ex: pe butoane `#669bbc` sau header `#003049`):** `#fdf0d5` (Cream) sau `Color.White`

---
**Cerințe Generale din Sarcină (pentru referință continuă):**
*   (Lista anterioară de cerințe rămâne validă, dar interpretarea rolurilor și cine efectuează acțiunile este acum conform noilor Definiții de Roluri.)
    *   1. Înregistrare melodie nouă (Admin).
    *   2. Înregistrare intervievat nou (Admin).
    *   3. Excludere intervievat (Admin).
    *   4. Creare clasament melodii (pe baza voturilor Utilizatorilor Standard).
    *   5. Afișare top 3 melodii (pentru toți, după calcul).
    *   6. Calcul punctaj intervievat (pe baza predicțiilor introduse de Admin vs. clasament melodii).
    *   7. Creare clasament intervievați (Admin view).
    *   8. Afișare top 5 intervievați (Admin view, posibil și User).
    *   9. Export Excel/Word participanți <= 18 ani (Admin).
    *   10. Afișare listă participanți concurs (Admin, prin rapoarte).
