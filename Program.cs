using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MailingListDB
{
    class Program
    {
        static void Main()
        {
            using (var context = new MailingListContext())
            {
                try
                {
                    AddCountry(context, "Украина");
                    AddCity(context, "Херсон", "Украина");
                    AddSection(context, "Мобильные телефоны");
                    AddPromotion(context, "Мобильные телефоны", "Украина", DateTime.Now, DateTime.Now.AddMonths(1), "Samsung Galaxy S21");

                    AddCustomer(context, "Дмитрий Курман", new DateTime(1978, 11, 13), "М", "kurman@gmail.com", "Украина", "Херсон");

                    DisplayAllCustomers(context);
                    DisplayEmails(context);
                    DisplaySections(context);
                    DisplayPromotions(context);
                    DisplayCities(context);
                    DisplayCountries(context);
                    DisplayCustomersByCity(context, "Херсон");
                    DisplayCustomersByCountry(context, "Украина");
                    DisplayPromotionsByCountry(context, "Украина");

                    UpdateCustomer(context, 1, "Дмитрий Курман");

                    DeleteCustomer(context, 1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static void AddCustomer(MailingListContext context, string fullName, DateTime dob, string gender, string email, string countryName, string cityName)
        {
            try
            {
                var country = context.Countries.FirstOrDefault(c => c.Name == countryName);
                var city = context.Cities.FirstOrDefault(c => c.Name == cityName);

                if (country == null || city == null)
                {
                    Console.WriteLine("Country or City does not exist.");
                    return;
                }

                if (context.Customers.Any(c => c.Email == email))
                {
                    Console.WriteLine("Customer with this email already exists.");
                    return;
                }

                var customer = new Customer
                {
                    FullName = fullName,
                    DateOfBirth = dob,
                    Gender = gender,
                    Email = email,
                    CountryId = country.CountryId,
                    CityId = city.CityId
                };

                context.Customers.Add(customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding customer: {ex.Message}");
            }
        }

        static void AddCountry(MailingListContext context, string name)
        {
            try
            {
                if (context.Countries.Any(c => c.Name == name))
                {
                    Console.WriteLine("Country already exists.");
                    return;
                }

                var country = new Country { Name = name };
                context.Countries.Add(country);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding country: {ex.Message}");
            }
        }

        static void AddCity(MailingListContext context, string name, string countryName)
        {
            try
            {
                if (context.Cities.Any(c => c.Name == name && c.Country.Name == countryName))
                {
                    Console.WriteLine("City already exists.");
                    return;
                }

                var country = context.Countries.FirstOrDefault(c => c.Name == countryName);
                if (country != null)
                {
                    var city = new City { Name = name, CountryId = country.CountryId };
                    context.Cities.Add(city);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Country does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding city: {ex.Message}");
            }
        }

        static void AddSection(MailingListContext context, string name)
        {
            try
            {
                if (context.Sections.Any(s => s.Name == name))
                {
                    Console.WriteLine("Section already exists.");
                    return;
                }

                var section = new Section { Name = name };
                context.Sections.Add(section);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding section: {ex.Message}");
            }
        }

        static void AddPromotion(MailingListContext context, string sectionName, string countryName, DateTime startDate, DateTime endDate, string productName)
        {
            try
            {
                var section = context.Sections.FirstOrDefault(s => s.Name == sectionName);
                var country = context.Countries.FirstOrDefault(c => c.Name == countryName);

                if (section == null || country == null)
                {
                    Console.WriteLine("Section or Country does not exist.");
                    return;
                }

                var promotion = new Promotion
                {
                    SectionId = section.SectionId,
                    CountryId = country.CountryId,
                    StartDate = startDate,
                    EndDate = endDate,
                    ProductName = productName
                };

                context.Promotions.Add(promotion);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding promotion: {ex.Message}");
            }
        }

        static void DisplayAllCustomers(MailingListContext context)
        {
            try
            {
                var customers = context.Customers.ToList();
                foreach (var customer in customers)
                {
                    Console.WriteLine($"Имя: {customer.FullName}, Дата рождения: {customer.DateOfBirth.ToShortDateString()}, Пол: {customer.Gender}, Email: {customer.Email}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying customers: {ex.Message}");
            }
        }

        static void DisplayEmails(MailingListContext context)
        {
            try
            {
                var emails = context.Customers.Select(c => c.Email).ToList();
                foreach (var email in emails)
                {
                    Console.WriteLine($"Email: {email}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying emails: {ex.Message}");
            }
        }

        static void DisplaySections(MailingListContext context)
        {
            try
            {
                var sections = context.Sections.ToList();
                foreach (var section in sections)
                {
                    Console.WriteLine($"Section: {section.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying sections: {ex.Message}");
            }
        }

        static void DisplayPromotions(MailingListContext context)
        {
            try
            {
                var promotions = context.Promotions.ToList();
                foreach (var promo in promotions)
                {
                    Console.WriteLine($"Promotion: {promo.ProductName}, Section: {promo.Section.Name}, Country: {promo.Country.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying promotions: {ex.Message}");
            }
        }

        static void DisplayCities(MailingListContext context)
        {
            try
            {
                var cities = context.Cities.ToList();
                foreach (var city in cities)
                {
                    Console.WriteLine($"City: {city.Name}, Country: {city.Country.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying cities: {ex.Message}");
            }
        }

        static void DisplayCountries(MailingListContext context)
        {
            try
            {
                var countries = context.Countries.ToList();
                foreach (var country in countries)
                {
                    Console.WriteLine($"Country: {country.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying countries: {ex.Message}");
            }
        }

        static void DisplayCustomersByCity(MailingListContext context, string cityName)
        {
            try
            {
                var customers = context.Customers
                    .Where(c => c.City.Name == cityName)
                    .ToList();
                foreach (var customer in customers)
                {
                    Console.WriteLine($"Customer: {customer.FullName}, City: {customer.City.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying customers by city: {ex.Message}");
            }
        }

        static void DisplayCustomersByCountry(MailingListContext context, string countryName)
        {
            try
            {
                var customers = context.Customers
                    .Where(c => c.Country.Name == countryName)
                    .ToList();
                foreach (var customer in customers)
                {
                    Console.WriteLine($"Customer: {customer.FullName}, Country: {customer.Country.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying customers by country: {ex.Message}");
            }
        }

        static void DisplayPromotionsByCountry(MailingListContext context, string countryName)
        {
            try
            {
                var promotions = context.Promotions
                    .Where(p => p.Country.Name == countryName)
                    .ToList();
                foreach (var promo in promotions)
                {
                    Console.WriteLine($"Promotion: {promo.ProductName}, Country: {promo.Country.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying promotions by country: {ex.Message}");
            }
        }

        static void AddSectionToCustomer(MailingListContext context, int customerId, string sectionName)
        {
            try
            {
                var customer = context.Customers.Include(c => c.InterestedSections).FirstOrDefault(c => c.CustomerId == customerId);
                var section = context.Sections.FirstOrDefault(s => s.Name == sectionName);

                if (customer == null || section == null)
                {
                    Console.WriteLine("Customer or Section does not exist.");
                    return;
                }

                if (!customer.InterestedSections.Contains(section))
                {
                    customer.InterestedSections.Add(section);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding section to customer: {ex.Message}");
            }
        }

        static void DisplaySectionsByCustomer(MailingListContext context, int customerId)
        {
            try
            {
                var customer = context.Customers
                    .Include(c => c.InterestedSections)
                    .FirstOrDefault(c => c.CustomerId == customerId);

                if (customer != null)
                {
                    foreach (var section in customer.InterestedSections)
                    {
                        Console.WriteLine($"Section: {section.Name}");
                    }
                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying sections by customer: {ex.Message}");
            }
        }

        static void DisplayPromotionsBySection(MailingListContext context, string sectionName)
        {
            try
            {
                var section = context.Sections
                    .Include(s => s.Promotions)
                    .FirstOrDefault(s => s.Name == sectionName);

                if (section != null)
                {
                    foreach (var promo in section.Promotions)
                    {
                        Console.WriteLine($"Promotion: {promo.ProductName}, Start Date: {promo.StartDate.ToShortDateString()}, End Date: {promo.EndDate.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine("Section not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying promotions by section: {ex.Message}");
            }
        }

        static void DisplayPromotionsByCustomer(MailingListContext context, int customerId)
        {
            try
            {
                var customer = context.Customers
                    .Include(c => c.InterestedSections)
                    .ThenInclude(s => s.Promotions)
                    .FirstOrDefault(c => c.CustomerId == customerId);

                if (customer != null)
                {
                    Console.WriteLine($"Promotions for customer {customer.FullName}:");
                    foreach (var section in customer.InterestedSections)
                    {
                        foreach (var promo in section.Promotions)
                        {
                            Console.WriteLine($"- Product: {promo.ProductName}, Section: {section.Name}, Country: {promo.Country.Name}, Start Date: {promo.StartDate.ToShortDateString()}, End Date: {promo.EndDate.ToShortDateString()}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying promotions by customer: {ex.Message}");
            }
        }

        static void DisplayPromotionsByCity(MailingListContext context, string cityName)
        {
            try
            {
                var city = context.Cities
                    .Include(c => c.Customers)
                    .ThenInclude(c => c.InterestedSections)
                    .ThenInclude(s => s.Promotions)
                    .FirstOrDefault(c => c.Name == cityName);

                if (city != null)
                {
                    Console.WriteLine($"Promotions in city {city.Name}:");
                    var promotions = city.Customers
                        .SelectMany(c => c.InterestedSections)
                        .SelectMany(s => s.Promotions)
                        .Distinct()
                        .ToList();

                    foreach (var promo in promotions)
                    {
                        Console.WriteLine($"- Product: {promo.ProductName}, Section: {promo.Section.Name}, Country: {promo.Country.Name}, Start Date: {promo.StartDate.ToShortDateString()}, End Date: {promo.EndDate.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine("City not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying promotions by city: {ex.Message}");
            }
        }

        static void UpdateCustomer(MailingListContext context, int customerId, string newName)
        {
            try
            {
                var customer = context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                if (customer != null)
                {
                    customer.FullName = newName;
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating customer: {ex.Message}");
            }
        }

        static void DeleteCustomer(MailingListContext context, int customerId)
        {
            try
            {
                var customer = context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                if (customer != null)
                {
                    context.Customers.Remove(customer);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting customer: {ex.Message}");
            }
        }

    }
}
