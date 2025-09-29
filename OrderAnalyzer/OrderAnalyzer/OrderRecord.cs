using System;

namespace OrderAnalyzer.Models;

/// <summary>
/// Rappresenta un singolo ordine letto dal file CSV.
/// Represents a single order read from the CSV file.
/// </summary>
public sealed class OrderRecord
{
    // Identificativo numerico dell'ordine
    // Numeric identifier of the order
    public int Id { get; init; }

    // Nome dell'articolo ordinato
    // Name of the ordered article
    public string ArticleName { get; init; } = "";

    // Quantità acquistata
    // Quantity purchased
    public int Quantity { get; init; }

    // Prezzo unitario dell'articolo
    // Unit price of the article
    public decimal UnitPrice { get; init; }

    // Percentuale di sconto applicata (es. 10 = 10%)
    // Discount percentage applied (e.g., 10 = 10%)
    public decimal PercentageDiscount { get; init; }

    // Nome e cognome del cliente
    // Customer's full name
    public string Buyer { get; init; } = "";

    /// <summary>
    /// Totale senza sconto (quantità * prezzo unitario).
    /// Total without discount (quantity * unit price).
    /// </summary>
    public decimal TotalWithoutDiscount => Quantity * UnitPrice;

    /// <summary>
    /// Fattore di sconto calcolato come (1 - %/100).
    /// Garantito tra 0 e 1.
    /// Discount factor calculated as (1 - %/100).
    /// Guaranteed between 0 and 1.
    /// </summary>
    public decimal DiscountFactor
    {
        get
        {
            var factor = 1m - (PercentageDiscount / 100m);
            if (factor < 0m) factor = 0m; // sconto negativo non ha senso
            if (factor > 1m) factor = 1m; // limite massimo 100%
            return factor;
        }
    }

    /// <summary>
    /// Totale con sconto applicato, arrotondato a 2 decimali.
    /// Total with discount applied, rounded to 2 decimals.
    /// </summary>
    public decimal TotalWithDiscount => Math.Round(
        TotalWithoutDiscount * DiscountFactor,
        2,
        MidpointRounding.AwayFromZero);

    /// <summary>
    /// Valore assoluto dello sconto (differenza tra totale senza sconto e con sconto).
    /// Absolute discount value (difference between total without discount and with discount).
    /// </summary>
    public decimal DiscountValue => TotalWithoutDiscount - TotalWithDiscount;
}
