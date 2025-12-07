using LibraryModels;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Net.Http.Headers;
using System.Text.Json;
using WebApiClient;
 

namespace Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            TestWebClient();
            Console.ReadLine(); 

        }

        static void TestWebClient()
        {
           WebClient<Book> webClient = new WebClient<Book>();
            webClient.Scheme = "http";
            webClient.Host = "localhost";
            webClient.Port = 5185;
            webClient.Path = "api/Guest/GetBook";
            webClient.AddParameter("bookId", "45");
            Book book = webClient.Get();
            Console.WriteLine($"{book.BookName} /r/n {book.BookDescription} ");
        }   
        static string CalculateHash(string password, string salt)
        {
            string s = password + salt;
            byte[] pass = System.Text.Encoding.UTF8.GetBytes(s);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(pass);
                return Convert.ToBase64String(bytes);
            }
        }
        static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
        static void ViewHash()
        {
            string pass = "212730030";
            string salt = GenerateSalt();
            Console.WriteLine(salt);
            string hash = CalculateHash(pass, salt);
            Console.WriteLine(hash);
        }
        static void TestWebService()
        {
            List<Currency> list = CurrencyListTest().Result;
            int count = 1;
            foreach (var currency in list)
            {
                Console.WriteLine($"{count}. {currency.symbol} - {currency.name}");
                count++;
            }
            Console.Write("Select Carrency number from >> ");
            int from = int.Parse(Console.ReadLine());
            Console.Write("Select Carrency number to >> ");
            int to = int.Parse(Console.ReadLine());
            Console.Write("Inter sum >> ");
            int sum = int.Parse(Console.ReadLine());
            ConvertResult result2 = GetResult(list[from - 1].symbol, list[to - 1].symbol, sum).Result;
            Console.WriteLine($"{result2.result.amountToConvert} {result2.result.from} = {Math.Round(result2.result.convertedAmount)} {result2.result.to}");
        }

        static async Task<ConvertResult> GetResult(string from, string to, double amount)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://currency-converter18.p.rapidapi.com/api/v1/convert?from={from}&to={to}&amount={amount}"),
                Headers =
    {
        { "x-rapidapi-key", "f941083369msh224539dcf0b2026p1db183jsna6addf01cf75" },
        { "x-rapidapi-host", "currency-converter18.p.rapidapi.com" },
    },
            }; 
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string? body = await response.Content.ReadAsStringAsync();
                ConvertResult r = JsonSerializer.Deserialize<ConvertResult>(body); 
                return r;
            }
        }

        static async Task<List<Currency>> CurrencyListTest()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-converter18.p.rapidapi.com/api/v1/supportedCurrencies"),
                Headers =
    {
        { "x-rapidapi-key", "f941083369msh224539dcf0b2026p1db183jsna6addf01cf75" },
        { "x-rapidapi-host", "currency-converter18.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Currency>>(body);
            }
        }
        static void ModelValidation()
        {
            Author author = new Author();   
            author.AuthorFirstName = "john";
            author.AuthorLastName = "dw";
            author.AuthorYear = 12;
            author.CountryId = 1;
            author.AuthorPicture = "picture.pdf";

            Dictionary<string, List<string>> errors = author.AllErrors();   
            if(author.IsValid == false)
            {
                foreach(var error in errors)
                {
                    foreach(var errorMsg in error.Value)
                    {
                        Console.WriteLine($"{errorMsg}");
                    }
                }
            }

        }

        static void Hashing()
        {
            string salt = "x8F2aB!";
            string password = "password123";
            string combined = salt + password;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(combined);
                byte[] hash = sha256.ComputeHash(bytes);

                // המרה להקסה לקריאה נוחה
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                    sb.Append(b.ToString("x2"));

                Console.WriteLine("SHA256 Hash: " + sb.ToString());
            }

        }

        static void  Hashing3()
        {
            string salt = CreateSalt(8);
            string password = "password123";
            string combined = salt + password;
            using (SHA256 sha256 = SHA256.Create())
            {
                    byte[] bytes = Encoding.UTF8.GetBytes(combined);
                    byte[] hash = sha256.ComputeHash(bytes);
                    //sha256.
                    // המרה להקסה לקריאה נוחה
                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in hash)
                        sb.Append(b.ToString("x2"));

                    Console.WriteLine("Hash: " + sb.ToString());
                    Console.WriteLine("Salt: " + salt);
            }
            

        }
        public static string CreateSalt(int size = 6) // Default salt size in bytes
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] saltBytes = new byte[size];
                rng.GetBytes(saltBytes);
                return Convert.ToBase64String(saltBytes);
            }
        }
        static void Hashing2()
        {
            string paaword = "alex1234";
            byte[] passwordHash, passwordSalt;
            PasswordHasher.CreatePasswordHash(paaword, out passwordHash, out passwordSalt);
            StringBuilder sb = new StringBuilder();
            StringBuilder sbSalt = new StringBuilder();
            foreach (byte b in passwordHash)
                sb.Append(b.ToString("x2"));
            foreach (byte b in passwordSalt)
                sbSalt.Append(b.ToString("x2"));

            Console.WriteLine("Password Hash: " + sb.ToString());
            Console.WriteLine("Password Salt: " + sbSalt.ToString());
        }
    }


    // Provides methods for creating and verifying password hashes using HMACSHA512.
    public static class PasswordHasher
    {
        // Creates a hashed version of the provided password along with a unique salt.
        // password: The plaintext password to be hashed.
        // passwordHash: Outputs the resulting password hash as a byte array.
        // passwordSalt: Outputs the unique salt used in hashing as a byte array.
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // Instantiate HMACSHA512 to generate a cryptographic hash and a unique key (salt).
            using (var hmac = new HMACSHA256())
            {
                // The Key property of HMACSHA512 provides a randomly generated salt.
                passwordSalt = hmac.Key; // Assign the generated salt to the output parameter.

                // Convert the plaintext password into a byte array using UTF-8 encoding.
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash of the password bytes using the HMACSHA512 instance.
                passwordHash = hmac.ComputeHash(passwordBytes); // Assign the computed hash to the output parameter.
            }
        }

        // Verifies whether the provided password matches the stored hash using the stored salt.
        // password: The plaintext password to verify.
        // storedHash: The stored password hash to compare against.
        // storedSalt: The stored salt used during the original hashing process.
        // Return: True if the password is valid and matches the stored hash; otherwise, false.
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            // Instantiate HMACSHA512 with the stored salt as the key to ensure the same hashing parameters.
            using (var hmac = new HMACSHA256(storedSalt))
            {
                // Convert the plaintext password into a byte array using UTF-8 encoding.
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash of the password bytes using the HMACSHA512 instance initialized with the stored salt.
                byte[] computedHash = hmac.ComputeHash(passwordBytes);

                // Compare the computed hash with the stored hash byte by byte.
                // SequenceEqual ensures that both byte arrays are identical in sequence and value.
                bool hashesMatch = computedHash.SequenceEqual(storedHash);

                // Return the result of the comparison.
                return hashesMatch;
            }
        }
    }

}
