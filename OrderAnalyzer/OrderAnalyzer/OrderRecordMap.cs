using CsvHelper.Configuration;

namespace OrderAnalyzer.Models;

/// <summary>
/// Mappa per CsvHelper: definisce come collegare le colonne del file CSV
/// con le proprietà della classe OrderRecord.
/// CsvHelper mapping: defines how to link CSV file columns
/// with the properties of the OrderRecord class.
/// </summary>
public sealed class OrderRecordMap : ClassMap<OrderRecord>
{
    public OrderRecordMap()
    {
        // Mappa esattamente le intestazioni del CSV ai campi della classe
        // Maps exactly the CSV headers to the class properties

        Map(m => m.Id).Name("Id");
        // Colonna "Id" -> proprietà Id
        // Column "Id" -> property Id

        Map(m => m.ArticleName).Name("Article Name");
        // Colonna "Article Name" -> proprietà ArticleName
        // Column "Article Name" -> property ArticleName

        Map(m => m.Quantity).Name("Quantity");
        // Colonna "Quantity" -> proprietà Quantity
        // Column "Quantity" -> property Quantity

        Map(m => m.UnitPrice).Name("Unit price");
        // Colonna "Unit price" -> proprietà UnitPrice
        // Column "Unit price" -> property UnitPrice

        Map(m => m.PercentageDiscount).Name("Percentage discount");
        // Colonna "Percentage discount" -> proprietà PercentageDiscount
        // Column "Percentage discount" -> property PercentageDiscount

        Map(m => m.Buyer).Name("Buyer");
        // Colonna "Buyer" -> proprietà Buyer
        // Column "Buyer" -> property Buyer
    }
}
