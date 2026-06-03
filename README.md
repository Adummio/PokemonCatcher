Architettura con Blazor web app e minimal API per la gestione dei pokèdex degli utenti.

---------------------------------
Struttura progetto:

Pokemon Catcher
- PokemonCatcher.slnx
- BlazorWebApp
  - BlazorWebApp.csproj
- PokemonMinimalApi
  - PokemonMinimalApi.csproj
- Pokemon.Core
  - Pokemon.Core.csproj

---------------------------------

BLAZOR WEB APP:

Esegue GET su PokéApi caricando un pokèmon casuale alla volta.

L’utente loggato può catturare il pokèmon con una certa probabilità.

I pokèmon catturati vengono conservati per ogni utente tramite una POST su PokemonMinimalApi.

Al login, il Pokèdex di un dato utente viene caricato tramite una GET su PokemonMinimalApi.

L’utente loggato può consultare la propria collezione di Pokémon (Pokèdex) tramite una pagina apposita.

Usa un servizio streaming grpc per caricare a chunk i versi dei Pokémon al loro spawn.

---------------------------------

MINIMAL API:

Conserva i dati relativi ai pokèmon per ciascun allenatore.

Interagisce con la BlazorWebApp tramite metodi GET per far sì che quest’ultima carichi i pokèmon per ciascun allenatore.

Interagisce con la BlazorWebApp tramite metodi POST per far sì che quest’ultima salvi i pokèmon catturati per ciascun allenatore.

---------------------------------

PROGETTO CORE:

Contiene le classi atte a deserializzare la risposta json di PokèApi.

Sia BlazorWebApp che PokemonMinimalApi dipendono da Pokemon.Core.
