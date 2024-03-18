using exercise_106;
using System;
using System.Collections.Generic;
using System.IO;
using log4net;
using log4net.Config;

class Program
{
    // Define a logger variable for this class
    private static readonly ILog log = LogManager.GetLogger(typeof(Program));

    static void Main(string[] args)
    {
        // Configure log4net
        XmlConfigurator.Configure();

        log.Info("Application started");

        // Handling books
        HandleBooks();

        // Handling payment card
        HandlePaymentCard();

        log.Info("Application ended");
    }

    static void HandleBooks()
    {
        List<Book> books = new List<Book>();
        string filePath = "books.csv";

        log.Info("Handling book input");

        Console.WriteLine("\nBook Input:");
        while (true)
        {
            Console.Write("Name: ");
            string title = Console.ReadLine();
            if (string.IsNullOrEmpty(title))
            {
                log.Info("No more books to add");
                break;
            }

            Console.Write("Pages: ");
            int pages = int.Parse(Console.ReadLine());

            Console.Write("Publication year: ");
            int publicationYear = int.Parse(Console.ReadLine());

            books.Add(new Book(title, pages, publicationYear));
            log.Info($"Added book: {title}");
        }

        // Save books to CSV in append mode
        using (StreamWriter sw = new StreamWriter(filePath, true))
        {
            foreach (var book in books)
            {
                sw.WriteLine($"{book.Title};{book.Pages};{book.PublicationYear}");
                log.Debug($"Written to file: {book.Title}");
            }
        }

        // Ask what to print
        Console.Write("What information will be printed? ");
        string printCommand = Console.ReadLine().ToLower();

        // Read books from CSV
        List<Book> booksFromFile = new List<Book>();
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(';');
                booksFromFile.Add(new Book(parts[0], int.Parse(parts[1]), int.Parse(parts[2])));
            }
        }

        // Print books based on command
        foreach (var book in booksFromFile)
        {
            if (printCommand == "everything")
            {
                Console.WriteLine(book);
                log.Debug($"Printed book: {book}");
            }
            else if (printCommand == "title")
            {
                Console.WriteLine(book.Title);
                log.Debug($"Printed book title: {book.Title}");
            }
        }
    }

    static void HandlePaymentCard()
    {
        Console.WriteLine("\nPayment Card Management:");
        PaymentCard card = new PaymentCard(100);
        Console.WriteLine(card); // The card has a balance of 100 euros
        log.Info("PaymentCard created with balance of 100 euros");

        card.AddMoney(49.99);
        Console.WriteLine(card); // The balance should be 149.99 euros after adding 49.99
        log.Info("Added 49.99 euros to PaymentCard");

        card.AddMoney(10000.0);
        Console.WriteLine(card); // The balance should be capped at 150 euros
        log.Info("Attempted to add 10000 euros to PaymentCard, capped at 150");

        card.AddMoney(-10); // Attempt to add a negative amount, which should have no effect
        Console.WriteLine(card); // The balance remains 150 euros
        log.Info("Attempted to subtract 10 euros from PaymentCard, no effect");
    }
}
