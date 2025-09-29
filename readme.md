# OrderAnalyzer

##  Descrizione / Description
**IT 🇮🇹**  
OrderAnalyzer è uno script da linea di comando scritto in **C# (.NET 8)** che permette di analizzare file CSV contenenti ordini di un e-commerce.  
L’applicazione legge i dati, calcola statistiche sugli ordini e presenta un **menù interattivo** per scegliere diverse operazioni.  

**EN 🇬🇧**  
OrderAnalyzer is a **C# (.NET 8)** command-line tool that analyzes CSV files containing e-commerce orders.  
The application reads the data, computes order statistics, and presents an **interactive menu** to perform different operations.  

---

##  Struttura progetto / Project structure
OrderAnalyzer/
├── Program.cs
│ ├── OrderRecord.cs
│ ├── OrderRecordMap.cs
│ └── Printer.cs
├── data/
│ ├── orders.csv
│ ├── orders_supermercato.csv
│ └── orders_elettronica.csv
└── readme.pdf


---

##  Funzionalità / Features
**IT 🇮🇹**
- Lettura e validazione di file CSV con intestazioni standard.
- Menu di scelta file CSV dalla cartella `data/`.
- Menu interattivo con 5 opzioni:
  1. Visualizza il contenuto del file CSV.  
  2. Record con importo totale (scontato) più alto.  
  3. Record con quantità più alta.  
  4. Record con maggior differenza tra totale senza sconto e con sconto.  
  5. Uscita dal programma.  

**EN 🇬🇧**
- Reads and validates CSV files with standard headers.
- File selection menu from the `data/` folder.
- Interactive menu with 5 options:
  1. Display CSV file content.  
  2. Record with the highest discounted total.  
  3. Record with the highest quantity.  
  4. Record with the largest difference between total without discount and with discount.  
  5. Exit program.  

---

##  Esempio file CSV / Example CSV file
```csv
Id,Article Name,Quantity,Unit price,Percentage discount,Buyer
1,Coke,10,1,0,Mario Rossi
2,Coke,15,2,0,Luca Neri
3,Fanta,5,3,2,Luca Neri
4,Water,20,1,10,Mario Rossi
5,Fanta,1,4,15,Andrea Bianchi
```

## inserimento nuovi file CSV /   Adding new CSV files
**IT 🇮🇹** 
se si desidera inserire ulteriori file CSV all'interno dello script basta assicurarsi che rispettino il formato sopra indicato,dopo di che inserirli all'interno della cartella data 
# ATTENZIONE:
  le proprietà del file devono essere le seguenti affinchè il programma le legga e che siano presenti nel menù iniziale :
    - Azione di compilazione (Build Action) = Nessuno
    - Copia nella directory di output (Copy to Output Directory) = Copia se più recente


**EN 🇬🇧**  
If you want to add additional CSV files for the program to work with, simply make sure they follow the same format shown above, then place them inside the `data` folder.  

# WARNING:
In order for the program to correctly detect and list them in the initial menu, the file properties must be set as follows:  
- **Build Action** = None  
- **Copy to Output Directory** = Copy if newer

---

# come testare lo script
## passaggio iniziale 
    scaricare la cartella in formato zip da git e estrarne il contenuto nella posizione che si preferisce sul proprio computer
dopo aver scaricato il progetto e aver estratto il contenuto della cartella ci sono diversi modi in cui si può testare il codice,i quali sono 
## caso 1 : esecuzione diretta (qualsiasi computer)
  
  aprire la cartella del progetto ed eseguire lo script semplicemnte cliccando l'eseguibile chiamato "OrderAnalyzer.exe" che si troverà all'interno del seguente percorso
  ``` Percorso in cui è stato salvato il progetto\OrderAnalyzer\OrderAnalyzer\OrderAnalyzer ```

## caso 2 : esecuzione da terminale con .net 8 installato sul computer
  
  aprire il terminale e inserire i seguenti comandi :
    
    -comando 1
     cd Percorso in cui è stato salvato il progetto\OrderAnalyzer\OrderAnalyzer\OrderAnalyzer 
    -comando 2
     dotnet run 

## caso 3 : esecuzione da terminale con .net 8 installato sul computer
  
  aprire il terminale e inserire i seguenti comandi :
    
    -comando 1
     cd Percorso in cui è stato salvato il progetto\OrderAnalyzer\OrderAnalyzer\OrderAnalyzer 
    -comando 2
     .\OrderAnalyzer.exe 



