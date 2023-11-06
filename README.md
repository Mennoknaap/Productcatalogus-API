# Productcatalogus-API
## Keuzes die ik heb gemaakt bij het maken van de API
### 1. De database
Ik heb ervoor gekozen om voor de database een EF-Core ORM database te gebruiken. In het specifiek SQLite. Ik gebruik SQLite, omdat ik hier al ervaring mee heb en omdat het makkelijk is om mee te werken. Ook is het makkelijk om te testen, omdat je geen database hoeft te installeren. Je kan gewoon een database file maken en die gebruiken.

### 2. Filteren
Ik heb ervoor gekozen om de filtervariabelen binnen te krijgen via FromQuery's. Ik heb hiervoor gekozen, omdat het makkelijk is om te gebruiken en omdat het makkelijk is om te testen.

### 3. Duplicate code
Ik heb de filter code van kleur en naam in 1 methode gezet, omdat dit eerst 2 behoorlijk dezelfde soort code was.

### 4. De API
Ik heb de HTTP status codes gebruikt die ik vond dat het beste bij de situatie paste. Op deze manier kan je er makkelijk achter komen wat er mis is gegaan.

## Wat ik zou doen als ik meer tijd erin zou steken
### 1. De database
Als ik meer tijd erin zou steken, zou ik meer onderzoek doen naar welke soort database voor deze applicatie handig zou zijn.

### 2. Authentication en Authorization
Als ik meer tijd erin zou steken, zou ik ook een authentication en authorization systeem maken. Dit zou ik of met JWT tokens of role based accounts. Ik zou dit doen, omdat het dan veiliger is en omdat je dan ook kan zien wie wat heeft gedaan.

### 3. Unit tests
Als laatst zou ik ook nog unit tests toevoegen, om te er zo zeker mogelijk van te zijn dat er geen fouten in de code zitten.