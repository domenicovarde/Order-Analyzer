using System.Globalization;
using OrderAnalyzer.Models;

namespace OrderAnalyzer.Utils;

/// <summary>
/// Classe statica di utilità per stampare informazioni su console.
/// Static utility class to print information to the console.
/// </summary>
public static class Printer
{
    // Cultura italiana per la formattazione dei numeri decimali
    // Italian culture for decimal number formatting
    private static readonly CultureInfo It = new("it-IT");

    /// <summary>
    /// Stampa un'intestazione con linee di separazione.
    /// Prints a header with separator lines.
    /// </summary>
    public static void PrintHeader(string title)
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', title.Length)); // linea sopra
        Console.WriteLine(title);                        // titolo
        Console.WriteLine(new string('=', title.Length)); // linea sotto
    }

    /// <summary>
    /// Stampa un record singolo con tutti i dettagli.
    /// Prints a single record with all details.
    /// </summary>
    public static void PrintRecord(OrderRecord r)
    {
        Console.WriteLine($"Id: {r.Id}");
        Console.WriteLine($"Articolo: {r.ArticleName}");  // Nome articolo / Article name
        Console.WriteLine($"Quantità: {r.Quantity}");     // Quantità / Quantity
        Console.WriteLine($"Prezzo unitario: {r.UnitPrice.ToString("0.00", It)}"); // prezzo formattato con 2 decimali
        Console.WriteLine($"Sconto %: {r.PercentageDiscount.ToString("0.##", It)}"); // sconto senza zeri inutili
        Console.WriteLine($"Cliente: {r.Buyer}");         // Nome del cliente / Customer name
        Console.WriteLine($"Totale senza sconto: {r.TotalWithoutDiscount.ToString("0.00", It)}");
        Console.WriteLine($"Totale con sconto:  {r.TotalWithDiscount.ToString("0.00", It)}");
        Console.WriteLine($"Differenza sconto:   {r.DiscountValue.ToString("0.00", It)}");
    }

    /// <summary>
    /// Stampa le istruzioni di utilizzo del programma.
    /// Prints the usage instructions for the program.
    /// </summary>
    public static void PrintUsage()
    {
        Console.WriteLine("Uso: OrderAnalyzer <percorso-file.csv>");
        Console.WriteLine("Esempio: OrderAnalyzer C:\\dati\\ordini.csv");
    }

    /// <summary>
    /// Stampa il menù principale bilingue (Italiano/Inglese).
    /// Prints the main bilingual menu (Italian/English).
    /// </summary>
    public static void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine("============================================================================================================================================================================================================");
        Console.WriteLine("===   BENVENUTO/A NEL MENU                                                                 ==       WELCOME TO THE MENU                                                                                  ===");
        Console.WriteLine("=== 1) Visualizza il contenuto del file CSV                                                ==     1) View CSV file content                                                                               ===");
        Console.WriteLine("=== 2) Visualizza il record con importo totale (scontato) più alto                         ==     2) View the record with the highest (discounted) total amount                                          ===");
        Console.WriteLine("=== 3) Visualizza il record con quantità più alta                                          ==     3) View the record with the highest quantity                                                           ===");
        Console.WriteLine("=== 4) Visualizza il record con maggior differenza tra totale senza sconto e con sconto    ==     4) View the record with the largest difference between total without discount and total with discount  ===");
        Console.WriteLine("=== 5) Esci                                                                                ==     5) Exit                                                                                                ===");
        Console.WriteLine("=== Seleziona un'opzione (1-5):                                                            ==     Select an option (1-5):                                                                                ===");
        Console.WriteLine("============================================================================================================================================================================================================");
    }

    /// <summary>
    /// Stampa una tabella riassuntiva di più record.
    /// Prints a summary table of multiple records.
    /// </summary>
    public static void PrintRecordsTable(IEnumerable<OrderRecord> records)
    {
        Console.WriteLine();
        Console.WriteLine("Id  | Articolo         | Qta | Prezzo  | Sconto% | Cliente");
        Console.WriteLine("----+------------------+-----+---------+---------+-----------------");
        foreach (var r in records)
        {
            Console.WriteLine(
                $"{r.Id,-3} | {Trunc(r.ArticleName, 16),-16} | {r.Quantity,3} | {r.UnitPrice,7:0.00} | {r.PercentageDiscount,7:0.##} | {Trunc(r.Buyer, 15)}");
        }
    }

    /// <summary>
    /// Tronca una stringa se è più lunga della dimensione massima.
    /// Truncates a string if it's longer than the maximum length.
    /// </summary>
    private static string Trunc(string s, int max) => s.Length <= max ? s : s[..max];
}
