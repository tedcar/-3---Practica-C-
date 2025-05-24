# Fișier Temporar (Placeholder) pentru Proiectul Melodii

Acest fișier (`placeholder.md`) este utilizat de AI pentru a stoca temporar fragmente de cod, scripturi, sau alte informații relevante care vor fi utilizate sau referite în etapele ulterioare ale dezvoltării proiectului "Melodii".

Conținutul de aici poate fi mutat, utilizat și apoi marcat ca "folosit" în `changelog.md`, dar nu va fi șters din acest fișier pentru a păstra o istorie a elementelor intermediare.

---

## Scripturi SQL din Solicitarea 1 (plan.md) - Data: 2024-07-28

Aceste scripturi sunt pentru crearea inițială a bazei de date `MelodiiConcurs` și a tabelelor de bază.
**STATUS: Folosit pentru creare BD și tabele în Solicitarea 2.**

**Script pentru Crearea Bazei de Date:**
```sql
CREATE DATABASE MelodiiConcurs;
```

**Scripturi pentru Crearea Tabelelor (Fără FK și CHECK complexe inițial):**
```sql
USE MelodiiConcurs;

CREATE TABLE Melodii (
    MelodieID INT PRIMARY KEY IDENTITY(1,1),
    Titlu NVARCHAR(255) NOT NULL,
    Artist NVARCHAR(255) NOT NULL,
    GenMuzical NVARCHAR(100) NULL,
    AnLansare INT NULL,
    PunctajTotal INT NULL
);

CREATE TABLE Intervievati (
    IntervievatID INT PRIMARY KEY IDENTITY(1,1),
    NumeComplet NVARCHAR(255) NOT NULL,
    Varsta INT NULL,
    Localitate NVARCHAR(100) NULL,
    ScorTotalConcurs INT NULL
);

CREATE TABLE Voturi (
    VotID INT PRIMARY KEY IDENTITY(1,1),
    IntervievatID INT NOT NULL,
    MelodieID INT NOT NULL,
    PozitieOferita INT NOT NULL
);
```

---

## Scripturi SQL din Solicitarea 2 (plan.md) - Data: 2024-07-28

Aceste scripturi sunt pentru adăugarea cheilor externe (Foreign Keys) și a constrângerilor CHECK simple.

**Scripturi pentru Adăugarea Cheilor Externe (Foreign Keys):**
```sql
-- De executat DUPĂ crearea tabelelor de bază.
-- Asigură-te că ești în contextul bazei de date: USE MelodiiConcurs;

ALTER TABLE Voturi
ADD CONSTRAINT FK_Voturi_Intervievati
FOREIGN KEY (IntervievatID) REFERENCES Intervievati(IntervievatID)
ON DELETE CASCADE; -- Dacă un intervievat este șters, toate voturile sale vor fi șterse.

ALTER TABLE Voturi
ADD CONSTRAINT FK_Voturi_Melodii
FOREIGN KEY (MelodieID) REFERENCES Melodii(MelodieID)
ON DELETE CASCADE; -- Dacă o melodie este ștearsă, toate voturile pentru ea vor fi șterse.
```

**Scripturi pentru Adăugarea Constrângerilor CHECK Simple:**
```sql
-- De executat DUPĂ crearea tabelelor de bază.
-- Asigură-te că ești în contextul bazei de date: USE MelodiiConcurs;

ALTER TABLE Intervievati
ADD CONSTRAINT CK_Intervievati_Varsta
CHECK (Varsta > 0 AND Varsta < 150 OR Varsta IS NULL); -- Permite vârsta NULL sau o valoare pozitivă rezonabilă.

ALTER TABLE Voturi
ADD CONSTRAINT CK_Voturi_PozitieOferita
CHECK (PozitieOferita IN (1, 2, 3)); -- Pozitia oferită poate fi doar 1, 2 sau 3.
```
