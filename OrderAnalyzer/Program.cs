using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using OrderAnalyzer.Models;
using OrderAnalyzer.Utils;

/// <summary>
/// Mostra un elenco di file CSV presenti nella cartella "data" accanto all'eseguibile
/// e chiede all'utente quale usare. Consente anche l'inserimento manuale di un percorso.
/// Shows a list of CSV files in the "data" folder next to the executable and
/// asks the user which one to use. Also allows manual path entry.
/// </summary>
static string SelectCsvFromData()
{
    var dataDir = Path.Combine(AppContext.BaseDirectory, "data");

    if (!Directory.Exists(dataDir))
    {
        Console.WriteLine($"La cartella 'data' non esiste: {dataDir}");
        Console.WriteLine("Creala e inserisci uno o più file .csv, poi riavvia.");
        // The 'data' folder does not exist. Create it and put CSV files inside, then restart.
        Environment.Exit(2);
    }

    var files = Directory.GetFiles(dataDir, "*.csv", SearchOption.TopDirectoryOnly)
                         .OrderBy(Path.GetFileName)
                         .ToList();

    if (files.Count == 0)
    {
        Console.WriteLine($"Nessun CSV trovato in: {dataDir}");
        Console.WriteLine("Inserisci un percorso completo di un file CSV o premi INVIO per uscire.");
        Console.Write("Percorso manuale: "); // Manual path
        var manual = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(manual) || !File.Exists(manual))
        {
            Console.WriteLine("Nessun file selezionato. Uscita.");
            Environment.Exit(2);
        }

        return Path.GetFullPath(manual!);
    }

    // Menu di scelta file csv   /       FILE SELECTION MENU 
    Console.WriteLine("========================================================================");
    Console.WriteLine("===  MENU DI SCELTA FILE  ==  FILE SELECTION MENU                    ===");
    Console.WriteLine("===  Seleziona il file CSV su cui lavorare / Select the CSV to use:  ===");
    for (int i = 0; i < files.Count; i++)
    {
        Console.WriteLine($"{i + 1}) {Path.GetFileName(files[i])}");
    }
    Console.WriteLine("0) Inserisci un percorso manuale / Enter a custom path               ===");
    Console.WriteLine("========================================================================");
    Console.Write("Scelta (0-" + files.Count + "): ");
    var input = Console.ReadLine()?.Trim();

    if (int.TryParse(input, out var index))
    {
        if (index == 0)
        {
            Console.Write("Percorso completo del CSV: ");
            var manual = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(manual) || !File.Exists(manual))
            {
                Console.WriteLine("Percorso non valido o file inesistente.");
                Console.WriteLine("Riprova a inserire il percorso, ritorno al menu");
                Console.WriteLine("Invalid path or file does not exist");
                Console.WriteLine("Please re-enter the path, returning to the menu");
                // Ritorna al menu principale di selezione file
                return SelectCsvFromData();

            }
            return Path.GetFullPath(manual!);
        }

        if (index >= 1 && index <= files.Count)
            return files[index - 1];

        Console.WriteLine("Indice non valido. Inserisci un valore valido");
        Console.WriteLine("Invalid index. Please enter a valid value");
        return SelectCsvFromData();
    }
    else
    {
        Console.WriteLine("Input non valido. Inserisci un valore valido");
        Console.WriteLine("Invalid input. Please enter a valid value");
        return SelectCsvFromData();
    }

    // Non raggiunto
  
}


// Codici di uscita: 0 ok; 1 uso errato; 2 file non trovato; 3 errore csv/lettura; 4 nessun dato
// Exit codes: 0 ok; 1 usage error; 2 file not found; 3 csv/read error; 4 no data
var exitCode = 0;

try
{
    // Richiedi sempre all'utente di scegliere il CSV dalla cartella data
    // Always ask the user to pick the CSV from data folder
    var path = SelectCsvFromData();

    // Configurazione di CsvHelper per leggere il file
    // CsvHelper configuration for reading the file
    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,   // la prima riga contiene intestazioni / first row contains headers
        TrimOptions = TrimOptions.Trim, // rimuove spazi extra / trims extra spaces
        MissingFieldFound = null, // ignora campi mancanti / ignores missing fields
        HeaderValidated = null,   // non valida i nomi delle colonne / does not validate header names
        Delimiter = ",",          // separatore dei campi / field delimiter
        BadDataFound = static ctx => Console.Error.WriteLine(
            $"Attenzione: riga malformata . Record grezzo: {ctx.RawRecord}") // log di errori di parsing / log malformed rows
    };

    List<OrderRecord> records;
    // Apertura e lettura del CSV
    // Open and read the CSV
    using (var reader = new StreamReader(path!))
    using (var csv = new CsvReader(reader, config))
    {
        // Associa la mappa delle colonne con le proprietà della classe
        // Link the CSV columns to the class properties
        csv.Context.RegisterClassMap<OrderRecordMap>();

        // Imposta la cultura per la conversione dei decimali
        // Set culture for decimal conversion
        var decOptions = csv.Context.TypeConverterOptionsCache.GetOptions<decimal>();
        decOptions.CultureInfo = CultureInfo.InvariantCulture;

        // Legge i record e scarta quelli con valori illogici
        // Read records and filter out invalid ones
        records = csv.GetRecords<OrderRecord>()
                     .Where(r => r.Quantity >= 0 && r.UnitPrice >= 0 && r.PercentageDiscount >= 0)
                     .ToList();
    }

    // Se non ci sono record validi
    // If no valid records
    if (records.Count == 0)
    {
        Console.Error.WriteLine("Nessun record valido nel CSV.");
        Environment.Exit(4);
    }

    // Calcoli richiesti: max importo, max quantità, max differenza sconto
    // Required calculations: max total, max quantity, max discount difference
    var maxTotal = records.MaxBy(r => r.TotalWithDiscount)!;
    var maxQty = records.MaxBy(r => r.Quantity)!;
    var maxDiff = records.MaxBy(r => r.DiscountValue)!;

    // Ciclo principale: mostra il menù finché l'utente non sceglie "Esci"
    // Main loop: show the menu until the user chooses "Exit"
    while (true)
    {
        Printer.PrintMenu(); // stampa il menù / print menu
        var choice = Console.ReadLine()?.Trim();

        switch (choice)
        {
            case "1":
                Printer.PrintHeader("===  Contenuto del file CSV  ==  CSV file content  ===");
                Printer.PrintRecordsTable(records);
                break;

            case "2":
                Printer.PrintHeader("===  Record con importo totale (scontato) più alto  ==  Record with the highest (discounted) total  ===");
                Printer.PrintRecord(maxTotal);
                break;

            case "3":
                Printer.PrintHeader("===  Record con quantità più alta  ==  Record with the highest quantity  ===");
                Printer.PrintRecord(maxQty);
                break;

            case "4":
                Printer.PrintHeader("===  Record con maggior differenza tra totale senza sconto e con sconto  ==  Record with the greatest difference between total without discount and with discount  ===");
                Printer.PrintRecord(maxDiff);
                break;

            case "5":
                Console.WriteLine("Arrivederci");
                Console.WriteLine("Goodbye");
                Environment.Exit(0); // uscita pulita / clean exit
                return;

            default:
                Console.WriteLine("Opzione non valida. Scegli un numero da 1 a 5.");
                // Invalid option. Choose a number between 1 and 5.
                break;
        }

        // Attendi che l'utente prema INVIO e poi pulisci la console
        // Wait for user to press ENTER and then clear the console
        Console.WriteLine();
        Console.Write("Premi INVIO per tornare al menù...");
        Console.ReadLine();
        Console.Clear();
    }

}
catch (CsvHelper.TypeConversion.TypeConverterException ex)
{
    // Eccezione di conversione durante il parsing del CSV
    // Conversion exception during CSV parsing
    Console.Error.WriteLine("Errore di conversione dati CSV. Controlla formati numerici e headers.");
    Console.Error.WriteLine(ex.Message);
    exitCode = 3;
}
catch (Exception ex)
{
    // Qualsiasi altra eccezione non prevista
    // Any other unexpected exception
    Console.Error.WriteLine("Errore non previsto:");
    Console.Error.WriteLine(ex);
    exitCode = 3;
}
finally
{
    // Esce con il codice appropriato
    // Exit with the appropriate code
    Environment.Exit(exitCode);
}
