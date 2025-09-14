namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence.Seed;

using System;
using System.Collections.Generic;
using SmartSolutionsLab.YellowCarRental.Domain;

internal static class CustomerTestData
{
    private static IReadOnlyList<Customer>? _cache;

    public static IReadOnlyList<Customer> GetAll(int capacity = 200)
    {
        if (_cache is not null) return _cache;

        var list = new List<Customer>(capacity);

        string[] salutations = { "Herr", "Frau" };

        string[] firstNames =
        {
            "Lukas","Anna","Felix","Marie","Jonas","Lea","Paul","Sophie","Leon","Emma",
            "Ben","Mia","Noah","Hannah","Finn","Lina","Elias","Laura","Luca","Clara",
            "Julian","Sarah","Nico","Paula","Max","Amelie","Tim","Lara","Moritz","Nina",
            "Simon","Emilia","Fabian","Eva","David","Greta","Tobias","Franziska","Jan","Helena",
            "Milan","Melina","Jannis","Alina","Jonathan","Luisa","Sebastian","Marlene","Karl","Isabell"
        };

        string[] lastNames =
        {
            "Müller","Schmidt","Schneider","Fischer","Weber","Wagner","Becker","Hoffmann","Schäfer","Koch",
            "Bauer","Richter","Klein","Wolf","Schröder","Neumann","Schwarz","Zimmermann","Braun","Krüger",
            "Hofmann","Hartmann","Lange","Schmitt","Werner","Krause","Meier","Lehmann","Schmid","Schulz",
            "Maier","Kaiser","Fuchs","Peters","Lang","Hermann","König","Walter","Mayer","Huber",
            "Keller","Vogel","Scholz","Hauser","Jäger","Lorenz","Arnold","Franke","Pohl","Seidel"
        };

        (string Zip, string City, string StreetBase)[] cityStreet =
        {
            ("10115","Berlin","Alexanderplatz"),
            ("20095","Hamburg","Jungfernstieg"),
            ("80331","München","Marienplatz"),
            ("50667","Köln","Domstraße"),
            ("60549","Frankfurt am Main","Flughafenstraße"),
            ("90402","Nürnberg","Königstraße"),
            ("70173","Stuttgart","Königstraße"),
            ("28195","Bremen","Marktstraße"),
            ("01067","Dresden","Augustusstraße"),
            ("04109","Leipzig","Grimmaische Straße")
        };

        var rnd = new Random(42);

        DateOnly start = new(1955, 1, 1);
        int dayRange = (new DateOnly(2005, 12, 31).DayNumber - start.DayNumber);

        for (int i = 0; i < capacity; i++)
        {
            var salutation = salutations[i % salutations.Length];
            var first = firstNames[i % firstNames.Length];
            var last = lastNames[(i * 3) % lastNames.Length];

            var loc = cityStreet[(i * 7) % cityStreet.Length];
            string street = $"{loc.StreetBase} {(i % 140) + 1}";
            string houseNo = ((i * 5) % 120 + 1).ToString();

            var birthDate = start.AddDays(rnd.Next(dayRange));
            // Ensure adult (>=18)
            if (birthDate.AddYears(18) > DateOnly.FromDateTime(DateTime.Today))
                birthDate = birthDate.AddYears(-18);

            var customer = Customer.From(
                salutation,
                first,
                last,
                birthDate,
                street,
                houseNo,
                loc.Zip,
                loc.City,
                $"{first.ToLower()}.{last.ToLower()}@example.com"
            );

            list.Add(customer);
        }

        _cache = list;
        return _cache;
    }
}
