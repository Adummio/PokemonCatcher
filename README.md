# Pokemon Catcher

Applicazione dimostrativa sviluppata con **ASP.NET Core**, **Blazor Web App**, **Minimal API** e **gRPC Streaming** per la gestione dei Pokédex degli allenatori Pokémon.

## Architettura della soluzione

```text
Pokemon Catcher
│
├── PokemonCatcher.slnx
│
├── BlazorWebApp
│   └── BlazorWebApp.csproj
│
├── PokemonMinimalApi
│   └── PokemonMinimalApi.csproj
│
└── Pokemon.Core
    └── Pokemon.Core.csproj
```

## Descrizione dei progetti

### BlazorWebApp

Applicazione frontend sviluppata con Blazor.

Responsabilità principali:

* Recupera Pokémon casuali dalla PokéAPI tramite richieste HTTP GET.
* Mostra un Pokémon alla volta all'utente.
* Permette all'utente autenticato di tentare la cattura del Pokémon.
* Applica una probabilità di cattura configurabile.
* Salva i Pokémon catturati tramite chiamate POST verso la Minimal API.
* Carica il Pokédex dell'utente tramite chiamate GET verso la Minimal API.
* Fornisce una pagina dedicata alla consultazione della propria collezione.
* Utilizza un servizio gRPC Streaming per ricevere progressivamente i versi del Pokémon durante lo spawn.

### PokemonMinimalApi

Backend sviluppato tramite ASP.NET Core Minimal API.

Responsabilità principali:

* Conserva i Pokémon catturati dagli allenatori.
* Espone endpoint REST per il recupero dei Pokédex.
* Espone endpoint REST per il salvataggio dei Pokémon catturati.
* Gestisce la persistenza dei dati degli utenti.

### Pokemon.Core

Libreria condivisa tra frontend e backend.

Contiene:

* DTO condivisi.
* Modelli di dominio.
* Classi per la deserializzazione delle risposte JSON provenienti dalla PokéAPI.
* Contratti utilizzati dai diversi layer applicativi.

## Flusso applicativo

### 1. Spawn del Pokémon

La Blazor Web App:

1. Effettua una richiesta alla PokéAPI.
2. Recupera un Pokémon casuale.
3. Mostra il Pokémon all'utente.
4. Avvia uno stream gRPC per ricevere i versi del Pokémon.

### 2. Tentativo di cattura

L'utente autenticato:

1. Seleziona l'opzione di cattura.
2. Il sistema calcola la probabilità di successo.
3. In caso di successo:

   * Il Pokémon viene aggiunto al Pokédex dell'utente.
   * Viene inviata una richiesta POST alla Minimal API.

### 3. Caricamento del Pokédex

Durante il login:

1. La Blazor Web App richiede il Pokédex dell'utente.
2. La Minimal API restituisce l'elenco dei Pokémon catturati.
3. La collezione viene caricata nell'interfaccia utente.

### 4. Consultazione del Pokédex

L'utente può:

* Visualizzare tutti i Pokémon catturati.
* Consultare le informazioni principali della propria collezione.

## Comunicazione tra componenti

### PokéAPI

Utilizzata per ottenere:

* Informazioni sui Pokémon.
* Statistiche.
* Immagini.
* Dati descrittivi.

### REST API

Comunicazione tra:

* BlazorWebApp
* PokemonMinimalApi

Endpoint principali:

#### Recupero Pokédex

```http
GET /api/trainers/{trainerId}/pokedex
```

#### Salvataggio Pokémon catturato

```http
POST /api/trainers/{trainerId}/pokemon
```

### gRPC Streaming

Utilizzato per:

* Inviare progressivamente i versi del Pokémon.
* Migliorare l'esperienza utente durante lo spawn.

## Dipendenze

### Frontend

* ASP.NET Core Blazor
* HttpClient
* gRPC Client

### Backend

* ASP.NET Core Minimal API
* gRPC Services

### Shared Library

* System.Text.Json

## Tecnologie utilizzate

* .NET 9
* ASP.NET Core
* Blazor Web App
* Minimal API
* gRPC Streaming
* REST API
* Dependency Injection
* System.Text.Json

## Obiettivi didattici

Questo progetto dimostra:

* Separazione delle responsabilità tra frontend e backend.
* Utilizzo delle Minimal API.
* Consumo di API esterne.
* Comunicazione REST tra servizi.
* Streaming di dati tramite gRPC.
* Riutilizzo di modelli condivisi tramite una libreria Core.
* Gestione di collezioni utente in un'applicazione distribuita.

## Possibili evoluzioni

* Persistenza tramite database SQL Server o PostgreSQL.
* Sistema di livelli per gli allenatori.
* Pokémon shiny.
* Evoluzioni dei Pokémon.
* Scambi tra allenatori.
* Battaglie PvP.
* Cache distribuita tramite Redis.
* Autenticazione JWT o Identity.

```
```
